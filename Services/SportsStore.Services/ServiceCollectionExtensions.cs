using Microsoft.Extensions.DependencyInjection;
using SportsStore.Interfaces.Products;
using SportsStore.Services.Data;
using SportsStore.Services.Products.Repositories;

namespace SportsStore.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSportStoreServices(this IServiceCollection services) => services
           .AddTransient<SportStoreDBInitializer>()
           .AddRepositories();

        private static IServiceCollection AddRepositories(this IServiceCollection services) => services
           .AddRepositoriesInSQL();

        private static IServiceCollection AddRepositoriesInSQL(this IServiceCollection services) => services
           .AddTransient<IProductsRepository, ProductsRepositoryInSQL>()
           .AddTransient<ICategoriesRepository, CategoriesRepositoryInSQL>();
    }
}
