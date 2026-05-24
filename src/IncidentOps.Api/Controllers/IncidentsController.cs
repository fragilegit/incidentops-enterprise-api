using IncidentOps.Api.Contracts.Incidents;
using IncidentOps.Application.Interfaces;
using IncidentOps.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IncidentOps.Api.Controllers;

[ApiController]
[Route("api/incidents")]
public class IncidentsController : ControllerBase
{
    private readonly IIncidentRepository _repository;

    public IncidentsController(IIncidentRepository repository)
    {
        _repository = repository;
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

        return Ok(incident);
    }
}
