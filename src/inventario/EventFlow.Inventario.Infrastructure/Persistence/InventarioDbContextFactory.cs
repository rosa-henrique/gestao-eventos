using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EventFlow.Inventario.Infrastructure.Persistence;

public class InventarioDbContextFactory : IDesignTimeDbContextFactory<InventarioDbContext>
{
    public InventarioDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<InventarioDbContext>();

        // Connection string usada SOMENTE no design-time
        builder.UseSqlServer(
            "Server=localhost,1433;Database=eventos;User Id=sa;Password=Your_password123;TrustServerCertificate=True");

        var publisher = new NoOpPublisher();

        return new InventarioDbContext(builder.Options, publisher);
    }

    private sealed class NoOpPublisher : IPublisher
    {
        public Task Publish(object notification, CancellationToken cancellationToken = default(CancellationToken))
            => Task.CompletedTask;

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken))
            where TNotification : INotification
            => Task.CompletedTask;
    }
}