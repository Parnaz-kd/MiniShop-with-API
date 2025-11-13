namespace MiniShop.Api.Application.DTOs
{
    public record CreateOrderDto(List<CreateOrderItemDto> Items, string? CouponCode);
}
