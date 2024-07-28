namespace GestaoEventos.Domain.Common;

public interface IRepository<T>
    where T : Entity
{
    Task Adicionar(T evento);
}