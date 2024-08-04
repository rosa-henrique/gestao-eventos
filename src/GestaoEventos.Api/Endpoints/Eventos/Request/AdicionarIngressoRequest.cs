namespace GestaoEventos.Api.Endpoints.Eventos.Request;

public record AdicionarIngressoRequest(string Nome, string Descricao, decimal Preco, int Quantidade)
{
}