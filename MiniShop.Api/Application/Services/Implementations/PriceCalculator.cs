using MiniShop.Api.Application.Services.Interfaces;
using MiniShop.Api.Domain.Abstractions;

namespace MiniShop.Api.Application.Services.Implementations
{
    public sealed class PriceCalculator : IPriceCalculator
    {
        private readonly IEnumerable<IPriceRule> _rules;
        private readonly ITaxPolicy _taxPolicy;
        public PriceCalculator(IEnumerable<IPriceRule> rules, ITaxPolicy taxPolicy)
        {
            _rules = rules;
            _taxPolicy = taxPolicy;
        }

        public (decimal discount, decimal tax, decimal total) Calculate(decimal subtotal, PriceContext ctx)
        {
            var totalDiscount = _rules.Sum(r => r.Apply(subtotal, ctx));
            var afterDiscount = subtotal + totalDiscount;
            var tax = _taxPolicy.Calculate(afterDiscount);
            var total = afterDiscount + tax;
            return (afterDiscount, tax, total);
        }
    }
}
