using IncidentOps.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IncidentOps.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Incident> Incidents => Set<Incident>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Incident>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(2000);

            entity.Property(x => x.Severity)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(x => x.RootCause)
                .HasMaxLength(2000);

            entity.Property(x => x.Resolution)
                .HasMaxLength(2000);

            entity.Property(x => x.Status)
                .HasConversion<string>()
                .IsRequired();
        });
    }
}
