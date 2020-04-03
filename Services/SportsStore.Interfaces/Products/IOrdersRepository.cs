using SportsStore.Domain.Models.Orders;
using SportsStore.Interfaces.Base;

namespace SportsStore.Interfaces.Products
{
    public interface IOrdersRepository : IRepository<Order>
    {
    }
}