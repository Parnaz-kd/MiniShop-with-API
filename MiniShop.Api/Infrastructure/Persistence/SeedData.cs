using MiniShop.Api.Domain.Entities;

namespace MiniShop.Api.Infrastructure.Persistence
{
    public static class SeedData
    {
        public static async Task EnsureSeedAsync(MiniShopDbContext db)
        {
            if (!db.Products.Any())
            {
                db.Products.AddRange(
                new Product { Name = "Keyboard", UnitPrice = 1_200_000 },
                new Product { Name = "Mouse", UnitPrice = 800_000 },
                new Product { Name = "Headset", UnitPrice = 3_500_000 }
                );
                await db.SaveChangesAsync();
            }
        }
    }
}
