using MiniShop.Api.Domain.Abstractions;

namespace MiniShop.Api.Application.Services.Interfaces
{
    public interface IPriceCalculator
    {
        (decimal discount, decimal tax, decimal total) Calculate(decimal subtotal, PriceContext ctx);
    }
}
