namespace GestaoEventos.Domain.Common;

public interface IRepository<T>
    where T : Entity
{
    Task Adicionar(T evento);
    Task Alterar(T entidade);
    Task Deletar(T entidade);
}