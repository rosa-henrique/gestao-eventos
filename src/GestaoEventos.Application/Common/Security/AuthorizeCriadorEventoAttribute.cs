namespace GestaoEventos.Application.Common.Security;

public class AuthorizeCriadorEventoAttribute(string propriedadeRequestIdEvento) : Attribute
{
    public string PropriedadeRequestIdEvento { get; } = propriedadeRequestIdEvento;
}