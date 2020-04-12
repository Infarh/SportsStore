using System.Linq;
using SportsStore.Domain.Models;
using SportsStore.Interfaces.Base;

namespace SportsStore.Interfaces.Products
{
    public interface IProductsRepository : IRepository<Product>
    {
        QueryResult<Product> GetQuery(QueryOptions Options, long CategoryId) => CategoryId == 0
            ? GetQuery(Options)
            : new QueryResult<Product>(Query.Where(product => product.CategoryId == CategoryId), Options);
    }
}
