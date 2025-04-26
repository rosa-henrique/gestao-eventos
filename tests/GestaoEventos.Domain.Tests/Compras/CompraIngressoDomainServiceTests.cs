using GestaoEventos.Domain.Compras;
using GestaoEventos.Domain.Eventos;
using GestaoEventos.TestCommon.Eventos;

using NSubstitute;

namespace GestaoEventos.Domain.Tests.Compras;

public class CompraIngressoDomainServiceTests
{
    private readonly CompraIngressoDomainService _service;
    private readonly ICompraIngressoRepository _compraIngressoRepositoryMock;

    public CompraIngressoDomainServiceTests()
    {
        _compraIngressoRepositoryMock = Substitute.For<ICompraIngressoRepository>();
        _service = new CompraIngressoDomainService(_compraIngressoRepositoryMock);
    }

    [Fact]
    public async Task RealizarCompra_ComSucesso()
    {
        var ingresso = IngressoFactory.CriarIngresso();
        var sessaoId = Guid.NewGuid();
        var usuarioId = Guid.NewGuid();
        var itensCompra = new Dictionary<Guid, int> { { ingresso.Id, 1 }, };

        _compraIngressoRepositoryMock.ObterQuantidadeIngressosVendidosPorSessao(sessaoId)
            .Returns(Task.FromResult(new Dictionary<Guid, int>()));
        var retorno = await _service.RealizarCompra([ingresso], sessaoId, usuarioId, itensCompra);

        retorno.IsError.Should().BeFalse();
        var compraIngressos = retorno.Value;

        compraIngressos.SessaoId.Should().Be(sessaoId);
        compraIngressos.UsuarioId.Should().Be(usuarioId);
    }
}