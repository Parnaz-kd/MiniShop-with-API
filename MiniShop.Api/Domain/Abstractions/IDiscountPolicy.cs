namespace MiniShop.Api.Domain.Abstractions
{
    public interface IDiscountPolicy
    {
        decimal Calculate(decimal baseAmount, PriceContext ctx);
    }
}
