using SportsStore.Domain.Models;
using SportsStore.Interfaces.Products;
using SportsStore.Services.Base.InMemory;

namespace SportsStore.Services.Products.InMemory
{
    internal class ProductsRepositoryInMemory : RepositoryInMemory<Product>, IProductsRepository
    {
        protected override void Update(Product DbItem, Product Item)
        {
            DbItem.Name = Item.Name;
            DbItem.Category = Item.Category;
            DbItem.PurchasePrice = Item.PurchasePrice;
            DbItem.RetailPrice = Item.RetailPrice;
        }
    }
}
