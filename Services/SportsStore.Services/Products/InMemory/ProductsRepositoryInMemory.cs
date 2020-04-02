using SportsStore.Domain.Models;
using SportsStore.Interfaces.Products;
using SportsStore.Services.Base.InMemory;
using SportsStore.Services.Mapping;

namespace SportsStore.Services.Products.InMemory
{
    internal class ProductsRepositoryInMemory : RepositoryInMemory<Product>, IProductsRepository
    {
        protected override void Update(Product DbItem, Product Item) => Item.CopyTo(DbItem);
    }
}
