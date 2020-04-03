using System.Linq;
using Microsoft.EntityFrameworkCore;
using SportsStore.DAL.Context;
using SportsStore.Domain.Models.Orders;
using SportsStore.Interfaces.Products;
using SportsStore.Services.Base.InSQL;

namespace SportsStore.Services.Products.InSQL
{
    internal class OrdersRepositoryInSQL : RepositoryInSQL<Order>, IOrdersRepository
    {
        public override IQueryable<Order> Query => base.Query
           .Include(order => order.Lines).ThenInclude(line => line.Product);

        public OrdersRepositoryInSQL(SportStoreDB Context) : base(Context) { }

        public override void Update(Order item)
        {
            ItemsSet.Update(item);
            SaveChanges();
        }
    }
}
