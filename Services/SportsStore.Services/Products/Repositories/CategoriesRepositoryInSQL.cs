using SportsStore.DAL.Context;
using SportsStore.Domain.Models;
using SportsStore.Interfaces.Products;
using SportsStore.Services.Base.InSQL;
using SportsStore.Services.Mapping;

namespace SportsStore.Services.Products.Repositories
{
    internal class CategoriesRepositoryInSQL : RepositoryInSQL<Category>, ICategoriesRepository
    {
        public CategoriesRepositoryInSQL(SportStoreDB Context) : base(Context) { }

        protected override void Update(Category DbItem, Category Item) => Item.CopyTo(DbItem);
    }
}