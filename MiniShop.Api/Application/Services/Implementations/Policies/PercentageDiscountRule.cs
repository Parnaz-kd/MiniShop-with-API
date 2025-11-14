using MiniShop.Api.Domain.Abstractions;

namespace MiniShop.Api.Application.Services.Implementations.Policies
{
    public class PercentageDiscountRule : IPriceRule
    {
        private readonly decimal _percent;
        public PercentageDiscountRule(decimal percent) => _percent = percent;

        public decimal Apply(decimal subtotal, PriceContext ctx) => subtotal * _percent * -1m;
    }
}
