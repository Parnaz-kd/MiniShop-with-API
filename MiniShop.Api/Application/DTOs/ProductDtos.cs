namespace MiniShop.Api.Application.DTOs
{
    public record ProductDto(int Id, string Name, decimal UnitPrice, bool IsActive);
}
