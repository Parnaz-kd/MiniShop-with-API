namespace MiniShop.Api.Domain.Abstractions
{
    public interface IPriceRule
    {
        decimal Apply(decimal subtotal, PriceContext ctx);
    }

    public sealed class PriceContext
    {
        public string? CouponCode { get; init; }
        public int ItemsCount { get; init; }
    }
}
