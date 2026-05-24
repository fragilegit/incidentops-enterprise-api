using IncidentOps.Domain.Entities;

namespace IncidentOps.Application.Interfaces;

public interface IIncidentRepository
{
    Task AddAsync(Incident incident);
    Task<List<Incident>> GetAllAsync();
    Task SaveChangesAsync();
}
