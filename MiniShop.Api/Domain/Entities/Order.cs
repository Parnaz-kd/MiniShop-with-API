namespace MiniShop.Api.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<OrderItem> Items { get; set; } = new();
        public decimal Subtotal => Items.Sum(i => i.UnitPrice * i.Quantity);
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal Total => Subtotal - Discount + Tax;
    }
}
