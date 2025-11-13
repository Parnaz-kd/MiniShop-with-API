namespace MiniShop.Api.Domain.ValueObjects
{
    public readonly record struct Money(decimal Amount,string Currency)
    {
        public static Money Zero(string currency = "IRR") => new(0m, currency);

        public Money Add(Money other)
            => Currency == other.Currency ? new(Amount + other.Amount, Currency)
            : throw new InvalidOperationException("Currency mismatch");
    }
}
