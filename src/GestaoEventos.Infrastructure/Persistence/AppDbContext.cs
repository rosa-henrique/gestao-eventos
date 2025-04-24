using GestaoEventos.Domain.Compras;
using GestaoEventos.Domain.Eventos;
using GestaoEventos.Domain.Usuarios;

using Microsoft.EntityFrameworkCore;

namespace GestaoEventos.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Evento> Eventos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<CompraIngresso> ComprasIngressos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}