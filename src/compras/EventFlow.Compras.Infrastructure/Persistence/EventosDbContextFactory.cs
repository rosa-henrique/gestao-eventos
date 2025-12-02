using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EventFlow.Compras.Infrastructure.Persistence;

public class EventosDbContextFactory : IDesignTimeDbContextFactory<ComprasDbContext>
{
    public ComprasDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<ComprasDbContext>();

        // Connection string usada SOMENTE no design-time
        builder.UseSqlServer(
            "Server=localhost,1433;Database=eventos;User Id=sa;Password=Your_password123;TrustServerCertificate=True");

        var publisher = new NoOpPublisher();

        return new ComprasDbContext(builder.Options, publisher);
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