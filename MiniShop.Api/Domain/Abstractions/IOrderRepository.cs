using MiniShop.Api.Domain.Entities;

namespace MiniShop.Api.Domain.Abstractions
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order, CancellationToken ct);
        Task<Order?> GetAsync(int id, CancellationToken ct);
    }
}
