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

    public void Adicionar(T entidade)
    {
        _dbSet.Add(entidade);
    }

    public void Alterar(T entidade)
    {
        _dbSet.Update(entidade);
    }

    public void Deletar(T entidade)
    {
        _dbSet.Remove(entidade);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}