namespace IncidentOps.Domain.Entities;

public class AuditLog
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid IncidentId { get; private set; }
    public string Action { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    private AuditLog() { }

    public AuditLog(Guid incidentId, string action)
    {
        IncidentId = incidentId;
        Action = action;
    }
}
