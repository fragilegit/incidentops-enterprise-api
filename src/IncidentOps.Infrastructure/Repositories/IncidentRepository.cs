using IncidentOps.Application.Interfaces;
using IncidentOps.Domain.Entities;
using IncidentOps.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IncidentOps.Infrastructure.Repositories;

public class IncidentRepository : IIncidentRepository
{
    private readonly AppDbContext _context;

    public IncidentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Incident incident)
    {
        await _context.Incidents.AddAsync(incident);
    }

    public async Task<List<Incident>> GetAllAsync()
    {
        return await _context.Incidents
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

     public async Task<Incident?> GetByIdAsync(Guid id)
    {
        return await _context.Incidents
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAuditLogAsync(AuditLog log)
    {
        await _context.AuditLogs.AddAsync(log);
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
