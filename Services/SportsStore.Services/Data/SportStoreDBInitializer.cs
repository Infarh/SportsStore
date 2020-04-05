using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportsStore.DAL.Context;
using SportsStore.Domain.Models;

namespace SportsStore.Services.Data
{
    public class SportStoreDBInitializer
    {
        private readonly SportStoreDB _Context;

        public SportStoreDBInitializer(SportStoreDB Context) => _Context = Context;

        public void Initialize() => InitializeAsync().GetAwaiter().GetResult();

        public async Task InitializeAsync()
        {
            await _Context.Database.MigrateAsync().ConfigureAwait(false);

            if (!await _Context.Products.AnyAsync().ConfigureAwait(false))
            {
                var water_sport = new Category { Name = "Водный спорт" };
                var soccer = new Category { Name = "Футбол" };
                await _Context.Products.AddRangeAsync(
                    new Product
                    {
                        Name = "Каяк",
                        Category = water_sport,
                        PurchasePrice = 200,
                        RetailPrice = 275
                    },
                    new Product
                    {
                        Name = "Спасательный жилет",
                        Category = water_sport,
                        PurchasePrice = 30,
                        RetailPrice = 48.95m
                    },
                    new Product
                    {
                        Name = "Футбольный мяч",
                        Category = soccer,
                        PurchasePrice = 17,
                        RetailPrice = 19.50m
                    })
                   .ConfigureAwait(false);
                await _Context.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }
}
