using System.Linq;
using Microsoft.EntityFrameworkCore;
using SportsStore.DAL.Context;
using SportsStore.Domain.Models;
using SportsStore.Interfaces.Products;
using SportsStore.Services.Base.InSQL;

namespace SportsStore.Services.Products.Repositories
{
    internal class ProductsRepositoryInSQL : RepositoryInSQL<Product>, IProductsRepository
    {
        public override IQueryable<Product> Query => base.Query
           .Include(product => product.Category);

        public ProductsRepositoryInSQL(SportStoreDB Context) : base(Context) { }

        protected override void Update(Product DbItem, Product Item)
        {
            DbItem.Name = Item.Name;
            DbItem.CategoryId = Item.CategoryId;
            DbItem.PurchasePrice = Item.PurchasePrice;
            DbItem.RetailPrice = Item.RetailPrice;
        }
    }
}
