using SportsStore.Domain.Models.Base;

namespace SportsStore.Domain.Models.Orders
{
    public class OrderLine : BaseEntity
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public long OrderId { get; set; }
        public Order Order { get; set; }
    }
}