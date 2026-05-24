using IncidentOps.Domain.Enums;

namespace IncidentOps.Domain.Entities;

public class Incident
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Severity { get; private set; } = string.Empty;
    public IncidentStatus Status { get; private set; } = IncidentStatus.Open;
    public Guid? AssignedToUserId { get; private set; }
    public string? RootCause { get; private set; }
    public string? Resolution { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    private Incident() { }

    public Incident(string title, string description, string severity)
    {
        Title = title;
        Description = description;
        Severity = severity;
    }

    public void Assign(Guid userId)
    {
        AssignedToUserId = userId;
        Status = IncidentStatus.Investigating;
    }

    public void Resolve(string rootCause, string resolution)
    {
        RootCause = rootCause;
        Resolution = resolution;
        Status = IncidentStatus.Resolved;
    }

    public void Close()
    {
        Status = IncidentStatus.Closed;
    }
}
