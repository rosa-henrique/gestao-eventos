using EventFlow.Eventos.Domain;

using Microsoft.EntityFrameworkCore;

namespace EventFlow.Eventos.Infrastructure.Persistence;

public class EventosDbContext(DbContextOptions<EventosDbContext> options) : DbContext(options)
{
    public DbSet<Evento> Eventos { get; set; } = null!;

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        NormalizeDateTimes();
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventosDbContext).Assembly);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties()
                         .Where(p => p.ClrType == typeof(DateTime)))
            {
                property.SetColumnType("timestamp without time zone");
            }
        }

        base.OnModelCreating(modelBuilder);
    }

    private void NormalizeDateTimes()
    {
        foreach (var entry in ChangeTracker.Entries()
                     .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
        {
            foreach (var prop in entry.Properties
                         .Where(p => p.CurrentValue is DateTime))
            {
                var date = (DateTime)prop.CurrentValue!;
                prop.CurrentValue = date.Kind == DateTimeKind.Utc
                    ? date
                    : DateTime.SpecifyKind(date, DateTimeKind.Utc);
            }
        }
    }
}