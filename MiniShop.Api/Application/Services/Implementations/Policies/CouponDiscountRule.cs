using MiniShop.Api.Domain.Abstractions;

namespace MiniShop.Api.Application.Services.Implementations.Policies
{
    public class CouponDiscountRule : IPriceRule
    {
        public decimal Apply(decimal subtotal, PriceContext ctx)
            => ctx.CouponCode?.Equals("OFF50", StringComparison.OrdinalIgnoreCase)== true
            ? Math.Min(subtotal * 0.5m, 500_000m) * -1m : 0m;
    }
}
