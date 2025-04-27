using GestaoEventos.Domain.Common;
using GestaoEventos.Domain.Compras;
using GestaoEventos.Domain.Eventos;
using GestaoEventos.Domain.Usuarios;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace GestaoEventos.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options, IPublisher publisher) : DbContext(options)
{
    public DbSet<Evento> Eventos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<CompraIngresso> ComprasIngressos { get; set; }

    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = ChangeTracker.Entries<Entity>()
            .SelectMany(entry => entry.Entity.PopDomainEvents())
            .ToList();

        await PublishDomainEvents(domainEvents);
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    private async Task PublishDomainEvents(List<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent);
        }
    }
}