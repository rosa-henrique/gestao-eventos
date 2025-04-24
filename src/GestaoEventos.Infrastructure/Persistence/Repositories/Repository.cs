using System.Linq.Expressions;

using GestaoEventos.Domain.Common;

using Microsoft.EntityFrameworkCore;

namespace GestaoEventos.Infrastructure.Persistence.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity>
    where TEntity : Entity
{
    protected readonly DbSet<TEntity> _dbSet;
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public void Adicionar(TEntity entidade)
    {
        _dbSet.Add(entidade);
    }

    public void Alterar(TEntity entidade)
    {
        _dbSet.Update(entidade);
    }

    public void Deletar(TEntity entidade)
    {
        _dbSet.Remove(entidade);
    }

    public async Task<IEnumerable<TResult>> GetListAsync<TResult>(
        Expression<Func<TEntity, bool>>? predicate = null,
        Expression<Func<TEntity, TResult>>? selector = null,
        bool tracked = false)
    {
        var query = _dbSet.AsQueryable();

        if (!tracked)
        {
            query = query.AsNoTracking(); // Desabilita tracking
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (selector == null)
        {
            return (IEnumerable<TResult>)await query.ToListAsync();
        }

        IQueryable<TResult> resultQuery = query.Select(selector);
        return await resultQuery.ToListAsync();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}