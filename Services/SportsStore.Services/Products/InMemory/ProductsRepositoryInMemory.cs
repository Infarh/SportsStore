using SportsStore.Domain.Models;
using SportsStore.Interfaces.Products;
using SportsStore.Services.Base.InMemory;

namespace SportsStore.Services.Products.InMemory
{
    internal class ProductsRepositoryInMemory : RepositoryInMemory<Product>, IProductsRepository
    {
        protected override void Update(Product DbItem, Product item)
        {
            DbItem.Name = item.Name;
            DbItem.Category = item.Category;
            DbItem.PurchasePrice = item.PurchasePrice;
            DbItem.RetailPrice = item.RetailPrice;
        }
    }
}
