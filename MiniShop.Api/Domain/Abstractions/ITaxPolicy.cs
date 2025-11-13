namespace MiniShop.Api.Domain.Abstractions
{
    public interface ITaxPolicy
    {
        decimal Calculate(decimal baseAmount);
    }
}
