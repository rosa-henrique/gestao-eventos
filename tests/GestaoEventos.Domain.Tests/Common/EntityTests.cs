using GestaoEventos.Domain.Common;

namespace GestaoEventos.Domain.Tests.Common;

public class EntityTests
{
    [Fact]
    public void Construtor_ComSucesso()
    {
        // Act
        var entity = new Entidade();

        // Assert
        Assert.NotEqual(entity.Id, Guid.Empty);
    }

    [Fact]
    public void ListarEventos_ComSucesso_DeveRetornarVazia()
    {
        // Act
        var entity = new Entidade();

        // Assert
        Assert.Empty(entity.PopDomainEvents());
    }
}

public class Entidade : Entity
{
    public Entidade()
        : base(Guid.NewGuid())
    {
    }
}