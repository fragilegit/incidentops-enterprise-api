using IncidentOps.Api.Contracts.Incidents;
using IncidentOps.Application.Interfaces;
using IncidentOps.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace IncidentOps.Api.Controllers;

[ApiController]
[Route("api/incidents")]
public class IncidentsController : ControllerBase
{
    private readonly IIncidentRepository _repository;
    private readonly IHttpClientFactory _httpClientFactory;

    public IncidentsController(IIncidentRepository repository,
    IHttpClientFactory httpClientFactory)
    {
        _repository = repository;
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateIncidentRequest request)
    {
        var incident = new Incident(
            request.Title,
            request.Description,
            request.Severity
        );

        await _repository.AddAsync(incident);
        await _repository.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetAll),
            new { id = incident.Id },
            incident
        );
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var incidents = await _repository.GetAllAsync();

        return Ok(incidents);
    }

    [HttpPut("{id}/assign")]
    public async Task<IActionResult> Assign(
        Guid id,
        AssignIncidentRequest request)
    {
        var incident = await _repository.GetByIdAsync(id);

        if (incident is null)
        {
            return NotFound();
        }

        incident.Assign(request.UserId);

        await _repository.AddAuditLogAsync(
            new AuditLog(
                incident.Id,
                $"Incident assigned to user {request.UserId}"
            )
        );

        await _repository.SaveChangesAsync();

        var httpClient = _httpClientFactory.CreateClient();

        var payload = new
        {
            incidentId = incident.Id,
            title = incident.Title,
            assignedTo = request.UserId,
            status = incident.Status.ToString()
        };

        var content = new StringContent(
            JsonSerializer.Serialize(payload),
            Encoding.UTF8,
            "application/json"
        );

        await httpClient.PostAsync(
            "http://node-notification-service:4000/notify",
            content
        );

        return Ok(incident);
    }
}
