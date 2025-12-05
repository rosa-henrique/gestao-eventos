namespace EventFlow.Compras.Application.Dtos;

public record ProcessarItensResponseDto(Guid IngressoId, decimal ValorUnitario, Guid EventoId);