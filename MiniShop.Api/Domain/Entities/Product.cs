namespace MiniShop.Api.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal UnitPrice { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
