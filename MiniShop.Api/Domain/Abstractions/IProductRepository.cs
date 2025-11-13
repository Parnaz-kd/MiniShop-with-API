using MiniShop.Api.Domain.Entities;

namespace MiniShop.Api.Domain.Abstractions
{
    public interface IProductRepository
    {
        Task<Product?> GetAsync(int id, CancellationToken ct);
        Task<List<Product>> GetAllAsync(CancellationToken ct);
        Task AddAsync(Product product, CancellationToken ct);
    }
}
