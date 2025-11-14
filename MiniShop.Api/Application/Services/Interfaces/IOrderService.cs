using MiniShop.Api.Application.DTOs;

namespace MiniShop.Api.Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResultDto> CreateAsync(CreateOrderDto dto, CancellationToken cancellationToken);
    }
}
