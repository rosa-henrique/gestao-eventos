using System.Reflection;

using ErrorOr;

using GestaoEventos.Application.Common.Interfaces;
using GestaoEventos.Application.Common.Security;
using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Common.Behaviors;

public class AuthorizeCriadorEventoBehavior<TRequest, TResponse>(
    IEventoRepository eventoRepository,
    IAuthorizationService authorizationService)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var authorizationAttribute = request.GetType()
            .GetCustomAttribute<AuthorizeCriadorEventoAttribute>();

        if (authorizationAttribute is null)
        {
            return await next();
        }

        var entityIdProperty = request.GetType().GetProperties()
            .FirstOrDefault(p => p.Name == authorizationAttribute.PropriedadeRequestIdEvento);

        if (entityIdProperty == null)
        {
            throw new InvalidOperationException("Não foi possível encontrar o ID da entidade no comando");
        }

        var eventoId = (Guid)entityIdProperty.GetValue(request)!;

        var evento = await eventoRepository.BuscarPorId(eventoId, cancellationToken);

        if (evento == null)
        {
            return await next();
        }

        var resultValidation = authorizationService.AuthorizeCriadorEvento(evento);

        return resultValidation.IsError
            ? (dynamic)resultValidation.Errors
            : await next();
    }
}