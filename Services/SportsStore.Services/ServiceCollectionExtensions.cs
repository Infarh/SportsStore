using Microsoft.Extensions.DependencyInjection;
using SportsStore.Interfaces.Products;
using SportsStore.Services.Data;
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

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            //services.AddSingleton<IProductsRepository, ProductsRepositoryInMemory>();
            services.AddTransient<IProductsRepository, ProductsRepositoryInSQL>();

            return services;
        }
    }
}
