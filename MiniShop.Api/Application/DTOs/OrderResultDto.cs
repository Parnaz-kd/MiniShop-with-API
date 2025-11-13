namespace MiniShop.Api.Application.DTOs
{
    public record OrderResultDto(int Id, decimal Subtotal, decimal Discount, decimal Tax, decimal Total);
}
