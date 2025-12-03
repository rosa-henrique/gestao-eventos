using Ardalis.SmartEnum;

namespace EventFlow.Compras.Domain;

public abstract class StatusCompra : SmartEnum<StatusCompra>
{
    public abstract string NomeExibicao { get; }

    public static readonly StatusCompra Pendente = new StatusPendente();
    public static readonly StatusCompra Pago = new StatusPago();
    public static readonly StatusCompra Cancelado = new StatusCancelado();
    public static readonly StatusCompra Reembolsado = new StatusReembolsado();

    private StatusCompra(string name, int value)
        : base(name, value)
    {
    }

    private sealed class StatusPendente : StatusCompra
    {
        public StatusPendente()
            : base("Pendente", 1) { }

        public override string NomeExibicao => "Pendente";
    }

    private sealed class StatusPago : StatusCompra
    {
        public StatusPago()
            : base("Pago", 2) { }

        public override string NomeExibicao => "Pago";
    }

    private sealed class StatusCancelado : StatusCompra
    {
        public StatusCancelado()
            : base("Cancelado", 3) { }

        public override string NomeExibicao => "Cancelado";
    }

    private sealed class StatusReembolsado : StatusCompra
    {
        public StatusReembolsado()
            : base("Reembolsado", 3) { }

        public override string NomeExibicao => "Reembolsado";
    }
}