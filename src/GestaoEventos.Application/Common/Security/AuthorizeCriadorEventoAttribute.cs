namespace GestaoEventos.Application.Common.Security;

[AttributeUsage(AttributeTargets.Class)]
public sealed class AuthorizeCriadorEventoAttribute(string propriedadeRequestIdEvento) : Attribute
{
    public string PropriedadeRequestIdEvento { get; } = propriedadeRequestIdEvento;
}