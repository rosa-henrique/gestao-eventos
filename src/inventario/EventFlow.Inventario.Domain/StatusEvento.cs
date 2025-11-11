using Ardalis.SmartEnum;

namespace EventFlow.Inventario.Domain;

public abstract class StatusEvento : SmartEnum<StatusEvento>
{
    public abstract string NomeExibicao { get; }

    public static readonly StatusEvento Pendente = new StatusPendente();
    public static readonly StatusEvento Confirmado = new StatusConfirmado();
    public static readonly StatusEvento Cancelado = new StatusCancelado();
    public static readonly StatusEvento EmAndamento = new StatusEmAndamento();
    public static readonly StatusEvento Concluido = new StatusConcluido();

    public static readonly IReadOnlyCollection<StatusEvento> StatusPermiteAlteracaoIngresso = [Pendente, Confirmado, EmAndamento];

    private StatusEvento(string name, int value)
        : base(name, value)
    {
    }

    private sealed class StatusPendente : StatusEvento
    {
        public StatusPendente()
            : base("Pendente", 1) { }

        public override string NomeExibicao => "Pendente";
    }

    private sealed class StatusConfirmado : StatusEvento
    {
        public StatusConfirmado()
            : base("Confirmado", 2) { }

        public override string NomeExibicao => "Confirmado";
    }

    private sealed class StatusCancelado : StatusEvento
    {
        public StatusCancelado()
            : base("Cancelado", 3) { }

        public override string NomeExibicao => "Cancelado";
    }

    private sealed class StatusEmAndamento : StatusEvento
    {
        public StatusEmAndamento()
            : base("EmAndamento", 4) { }

        public override string NomeExibicao => "EmAndamento";
    }

    private sealed class StatusConcluido : StatusEvento
    {
        public StatusConcluido()
            : base("Concluido", 5) { }

        public override string NomeExibicao => "Concluído";
    }
}