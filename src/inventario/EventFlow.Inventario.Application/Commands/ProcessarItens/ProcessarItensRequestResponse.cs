namespace EventFlow.Inventario.Application.Commands.ProcessarItens;

public record ProcessarItensRequestResponse(Guid Id, decimal ValorUnitario, Guid EventoId);