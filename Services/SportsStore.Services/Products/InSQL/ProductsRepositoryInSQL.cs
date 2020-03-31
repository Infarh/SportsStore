using SportsStore.DAL.Context;
using SportsStore.Domain.Models;
using SportsStore.Interfaces.Products;
using SportsStore.Services.Base.InSQL;

namespace SportsStore.Services.Products.InSQL
{
    internal class ProductsRepositoryInSQL : RepositoryInSQL<Product>, IProductsRepository
    {
        public ProductsRepositoryInSQL(SportStoreDB Context) : base(Context) { }

        protected override void Update(Product DbItem, Product Item)
        {
            DbItem.Name = Item.Name;
            DbItem.Category = Item.Category;
            DbItem.PurchasePrice = Item.PurchasePrice;
            DbItem.RetailPrice = Item.RetailPrice;
        }
    }
}
