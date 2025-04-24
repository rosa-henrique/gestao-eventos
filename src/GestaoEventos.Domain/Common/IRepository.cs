using System.Linq.Expressions;

namespace GestaoEventos.Domain.Common;

public interface IRepository<TEntity>
    where TEntity : Entity
{
    void Adicionar(TEntity entidade);
    void Alterar(TEntity entidade);
    void Deletar(TEntity entidade);

    Task<IEnumerable<TResult>> GetListAsync<TResult>(
        Expression<Func<TEntity, bool>>? predicate = null,
        Expression<Func<TEntity, TResult>>? selector = null,
        bool tracked = false);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}