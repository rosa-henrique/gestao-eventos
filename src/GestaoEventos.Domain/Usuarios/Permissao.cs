using Ardalis.SmartEnum;

using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.Domain.Usuarios;

public class Permissao(string name, int value) : SmartEnum<Permissao>(name, value)
{
    public static readonly Permissao CriarEvento = new("CriarEvento", 1);
    public static readonly Permissao Administrador = new("Administrador", 0);
}