using GestaoEventos.Domain.Common;

using Microsoft.EntityFrameworkCore;

namespace GestaoEventos.Infrastructure.Persistence.Repositories;

public abstract class Repository<T> : IRepository<T>
                                where T : Entity
{
    protected readonly DbSet<T> _dbSet;
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task Adicionar(T entidade)
    {
        _dbSet.Add(entidade);
        await _context.SaveChangesAsync();
    }

    public async Task Alterar(T entidade)
    {
        _dbSet.Update(entidade);
        await _context.SaveChangesAsync();
    }
}