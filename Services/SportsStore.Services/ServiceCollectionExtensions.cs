using Microsoft.Extensions.DependencyInjection;
using SportsStore.Interfaces.Products;
using SportsStore.Services.Data;
using SportsStore.Services.Products.InMemory;

namespace SportsStore.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSportStoreServices(this IServiceCollection services)
        {
            services.AddTransient<SportStoreDBInitializer>();

            services.AddSingleton<IProductsRepository, ProductsRepositoryInMemory>();

            return services;
        }
    }
}
