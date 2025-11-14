using MiniShop.Api.Application.DTOs;
using MiniShop.Api.Application.Services.Interfaces;
using MiniShop.Api.Domain.Abstractions;
using MiniShop.Api.Domain.Entities;
using MiniShop.Api.Infrastructure.Persistence;

namespace MiniShop.Api.Application.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly MiniShopDbContext _db;
        private readonly IPriceCalculator _calculator;
        public OrderService(MiniShopDbContext db, IPriceCalculator calculator)
        {
            _db = db;
            _calculator = calculator;
        }

        public async Task<OrderResultDto> CreateAsync(CreateOrderDto dto, CancellationToken cancellationToken)
        {
            var items = new List<OrderItem>();
            foreach (var i in dto.Items)
            {
                var p = await _db.Products.FindAsync(new object?[] { i.ProductId }, cancellationToken)
                    ?? throw new KeyNotFoundException($"Product {i.ProductId} not found");

                items.Add(new OrderItem
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    Quantity = i.Quantity,
                    UnitPrice = p.UnitPrice
                });
            }

            Order order = new Order { Items = items };
            var ctx = new PriceContext { CouponCode = dto.CouponCode, ItemsCount = items.Sum(x => x.Quantity) };
            var (discount, tax, total) = _calculator.Calculate(order.Subtotal, ctx);

            order.Discount = discount;
            order.Tax = tax;

            _db.Orders.Add(order);
            await _db.SaveChangesAsync(cancellationToken);

            return new OrderResultDto(order.Id, order.Subtotal, order.Discount, order.Tax, order.Total);
        }
    }
}
