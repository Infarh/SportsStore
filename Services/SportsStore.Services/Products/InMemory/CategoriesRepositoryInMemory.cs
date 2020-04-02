using SportsStore.Domain.Models;
using SportsStore.Interfaces.Products;
using SportsStore.Services.Base.InMemory;
using SportsStore.Services.Mapping;

namespace SportsStore.Services.Products.InMemory
{
    internal class CategoriesRepositoryInMemory : RepositoryInMemory<Category>, ICategoriesRepository
    {
        protected override void Update(Category DbItem, Category Item) => Item.CopyTo(DbItem);
    }
}