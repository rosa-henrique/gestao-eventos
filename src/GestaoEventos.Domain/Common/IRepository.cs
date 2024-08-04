namespace GestaoEventos.Domain.Common;

public interface IRepository<T>
    where T : Entity
{
    void Adicionar(T entidade);
    void Alterar(T entidade);
    void Deletar(T entidade);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}