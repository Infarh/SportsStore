using SportsStore.Domain.Models;
using SportsStore.Interfaces.Products;
using SportsStore.Services.Base.InMemory;

namespace SportsStore.Services.Products.InMemory
{
    public class ProductsRepositoryInMemory : RepositoryInMemory<Product>, IProductsRepository
    {
    }
}
