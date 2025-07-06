using System.Collections.Frozen;

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
        var evento = EventoFactory.CriarEvento(status: StatusEvento.EmAndamento);
        var ingresso = IngressoFactory.CriarIngresso();
        evento.AdicionarIngresso(ingresso);
        var sessaoId = Guid.NewGuid();
        var usuarioId = Guid.NewGuid();
        var itensCompra = new Dictionary<Guid, int> { { ingresso.Id, 1 }, }.ToFrozenDictionary();

        _compraIngressoRepositoryMock.ObterQuantidadeIngressosVendidosPorSessao(sessaoId)
            .Returns(Task.FromResult(new Dictionary<Guid, int>()));
        var retorno = await _service.RealizarCompra(evento, sessaoId, usuarioId, itensCompra);

        retorno.IsError.Should().BeFalse();
        var compraIngressos = retorno.Value;

        compraIngressos.SessaoId.Should().Be(sessaoId);
        compraIngressos.UsuarioId.Should().Be(usuarioId);
    }

    [Fact]
    public async Task RealizarCompra_ComErro_EventoNaoPermiteCompra()
    {
        var evento = EventoFactory.CriarEvento(status: StatusEvento.Pendente);
        var ingresso = IngressoFactory.CriarIngresso();
        var sessaoId = Guid.NewGuid();
        var usuarioId = Guid.NewGuid();
        var itensCompra = new Dictionary<Guid, int> { { ingresso.Id, 1 } }
            .ToFrozenDictionary();

        _compraIngressoRepositoryMock.ObterQuantidadeIngressosVendidosPorSessao(sessaoId)
            .Returns(Task.FromResult(new Dictionary<Guid, int>()));
        var retorno = await _service.RealizarCompra(evento, sessaoId, usuarioId, itensCompra);

        retorno.IsError.Should().BeTrue();

        retorno.Errors.Should().NotBeEmpty()
            .And.Satisfy(a => a.Description == ErrosCompras.EventoNaoPermiteCompra);
    }

    [Fact]
    public async Task RealizarCompra_ComErro_IngressoNaoEncontrado()
    {
        var evento = EventoFactory.CriarEvento(status: StatusEvento.EmAndamento);
        var ingresso = IngressoFactory.CriarIngresso();
        var sessaoId = Guid.NewGuid();
        var usuarioId = Guid.NewGuid();
        var itensCompra = new Dictionary<Guid, int> { { ingresso.Id, 1 }, }.ToFrozenDictionary();

        _compraIngressoRepositoryMock.ObterQuantidadeIngressosVendidosPorSessao(sessaoId)
            .Returns(Task.FromResult(new Dictionary<Guid, int>()));
        var retorno = await _service.RealizarCompra(evento, sessaoId, usuarioId, itensCompra);

        retorno.IsError.Should().BeTrue();

        retorno.Errors.Should().NotBeEmpty()
            .And.Satisfy(a => a.Description == string.Format(ErrosCompras.IngressoNaoEncontrado, ingresso.Id));
    }

    [Fact]
    public async Task RealizarCompra_ComErro_QuantidadeIngressoIndisponivelAtualIngresso()
    {
        var evento = EventoFactory.CriarEvento(status: StatusEvento.EmAndamento);
        var ingresso = IngressoFactory.CriarIngresso(quantidade: 5);
        evento.AdicionarIngresso(ingresso);
        var sessaoId = Guid.NewGuid();
        var usuarioId = Guid.NewGuid();
        var itensCompra = new Dictionary<Guid, int> { { ingresso.Id, ingresso.Quantidade + 1 }, }.ToFrozenDictionary();

        _compraIngressoRepositoryMock.ObterQuantidadeIngressosVendidosPorSessao(sessaoId)
            .Returns(Task.FromResult(new Dictionary<Guid, int>()));
        var retorno = await _service.RealizarCompra(evento, sessaoId, usuarioId, itensCompra);

        retorno.IsError.Should().BeTrue();

        retorno.Errors.Should().NotBeEmpty()
            .And.Satisfy(a => a.Description == ErrosCompras.QuantidadeIngressoIndisponivel);
    }

    [Fact]
    public async Task RealizarCompra_ComErro_QuantidadeIngressoIndisponivelSomadoComSalvos()
    {
        var evento = EventoFactory.CriarEvento(status: StatusEvento.EmAndamento);
        var ingresso = IngressoFactory.CriarIngresso(quantidade: 5);
        evento.AdicionarIngresso(ingresso);
        var sessaoId = Guid.NewGuid();
        var usuarioId = Guid.NewGuid();
        var itensCompra = new Dictionary<Guid, int> { { ingresso.Id, 1 }, }.ToFrozenDictionary();
        var retornoRepository = new Dictionary<Guid, int>() { { ingresso.Id, ingresso.Quantidade } };

        _compraIngressoRepositoryMock.ObterQuantidadeIngressosVendidosPorSessao(sessaoId)
            .Returns(Task.FromResult(retornoRepository));
        var retorno = await _service.RealizarCompra(evento, sessaoId, usuarioId, itensCompra);

        retorno.IsError.Should().BeTrue();

        retorno.Errors.Should().NotBeEmpty()
            .And.Satisfy(a => a.Description == ErrosCompras.QuantidadeIngressoIndisponivel);
    }
}