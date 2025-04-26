using GestaoEventos.Domain.Compras;

namespace GestaoEventos.Domain.Tests.Compras;

public class IngressoCompradoTests
{
    [Fact]
    public void IngressoComprado_ComSucesso()
    {
        var ingressoId = Guid.NewGuid();
        var ingressoCompra = new IngressoComprado(ingressoId, 10, 10);

        ingressoCompra.Should().NotBeNull();
        ingressoCompra.IngressoId.Should().Be(ingressoId);
        ingressoCompra.ValorTotal.Should().Be(100);
    }
}