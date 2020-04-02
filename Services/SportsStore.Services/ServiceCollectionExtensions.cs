using Microsoft.Extensions.DependencyInjection;
using SportsStore.Interfaces.Products;
using SportsStore.Services.Data;
using SportsStore.Services.Products.InMemory;
using SportsStore.Services.Products.InSQL;

namespace SportsStore.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSportStoreServices(this IServiceCollection services)
        {
            services.AddTransient<SportStoreDBInitializer>();
            services.AddRepositories();
            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services) =>
            //services.AddRepositoriesInMemory();
            services.AddRepositoriesInSQL();

        private static IServiceCollection AddRepositoriesInMemory(this IServiceCollection services)
        {
            services.AddSingleton<IProductsRepository, ProductsRepositoryInMemory>();
            services.AddSingleton<ICategoriesRepository, CategoriesRepositoryInMemory>();
            return services;
        }

        private static IServiceCollection AddRepositoriesInSQL(this IServiceCollection services)
        {
            services.AddTransient<IProductsRepository, ProductsRepositoryInSQL>();
            services.AddTransient<ICategoriesRepository, CategoriesRepositoryInSQL>();
            return services;
        }
    }
}
