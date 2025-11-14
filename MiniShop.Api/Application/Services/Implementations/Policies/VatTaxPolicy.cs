using MiniShop.Api.Domain.Abstractions;

namespace MiniShop.Api.Application.Services.Implementations.Policies
{
    public class VatTaxPolicy : ITaxPolicy
    {
        private readonly decimal _rate;
        public VatTaxPolicy(decimal rate) => _rate = rate;
        public decimal Calculate(decimal baseAmount) => baseAmount * _rate;
    }
}
