using EventFlow.Eventos.Domain;
using EventFlow.Shared.Domain;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace EventFlow.Eventos.Infrastructure.Persistence;

public class EventosDbContext(DbContextOptions<EventosDbContext> options, IPublisher publisher) : DbContext(options)
{
    public DbSet<Evento> Eventos { get; set; } = null!;

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        NormalizeDateTimes();

        var result = await base.SaveChangesAsync(cancellationToken);

        var domainEvents = ChangeTracker.Entries<Entity>()
            .SelectMany(entry => entry.Entity.PopDomainEvents())
            .ToList();

        await PublishDomainEvents(domainEvents);

        return result;
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
                     .Where(e => e.State is EntityState.Added or EntityState.Modified))
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

    private async Task PublishDomainEvents(List<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent);
        }
    }
}