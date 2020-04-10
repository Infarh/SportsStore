using System;
using System.Threading;
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

        public async Task InitializeAsync(CancellationToken Cancel = default)
        {
            await _Context.Database.MigrateAsync(Cancel).ConfigureAwait(false);

            await InitializeStandardData(Cancel).ConfigureAwait(false);
        }

        private async Task InitializeStandardData(CancellationToken Cancel = default)
        {
            if (await _Context.Products.AnyAsync(Cancel).ConfigureAwait(false)) return;

            await _Context.Categories
               .AddRangeAsync(
                    new Category
                    {
                        Name = "Водный спорт",
                        Description = "Наделай шуму",
                        Products = new[]
                        {
                            new Product
                            {
                                Name = "Каяк",
                                Description = "Одноместная лодка",
                                PurchasePrice = 200,
                                RetailPrice = 275
                            },
                            new Product
                            {
                                Name = "Спасательный жилет",
                                Description = "Защитный и модный",
                                PurchasePrice = 30,
                                RetailPrice = 48.95m
                            },
                        }
                    },
                    new Category
                    {
                        Name = "Футбол",
                        Description = "Любимая игра в мире",
                        Products = new[]
                        {
                            new Product
                            {
                                Name = "Футбольный мяч",
                                Description = "Одобренный ФИФА размер и вес",
                                PurchasePrice = 18,
                                RetailPrice = 19.50m
                            },
                            new Product
                            {
                                Name = "Угловые флажки",
                                Description = "Сделай ваше игровое поле профессиональным",
                                PurchasePrice = 52.50m,
                                RetailPrice = 34.95m
                            },
                            new Product
                            {
                                Name = "Стадион",
                                Description = "Компактно упакованный стадион на 35 000 мест",
                                PurchasePrice = 75000,
                                RetailPrice = 79500
                            },
                        }
                    },
                    new Category
                    {
                        Name = "Шахматы",
                        Description = "Умственная игра",
                        Products = new []
                        {
                            new Product
                            {
                                Name = "Мыслящая шапка",
                                Description = "Усиливает умственные способности на 75%",
                                PurchasePrice = 10,
                                RetailPrice = 16
                            },
                            new Product
                            {
                                Name = "Неустойчивый стул",
                                Description = "Незаметно создаёт противнику неудобства",
                                PurchasePrice = 28,
                                RetailPrice = 29.95m
                            },
                            new Product
                            {
                                Name = "Шахматная доска",
                                Description = "Весёлая игра для всей семьи",
                                PurchasePrice = 68.50m,
                                RetailPrice = 75
                            },
                            new Product
                            {
                                Name = "Блестящий король",
                                Description = "Позолоченный и усыпанный бриллиантами король",
                                PurchasePrice = 800,
                                RetailPrice = 1200
                            },
                        }
                    })
               .ConfigureAwait(false);
            await _Context.SaveChangesAsync(Cancel).ConfigureAwait(false);
        }


        public void SeedTestData(int Count)
        {
            ClearDatabase();

            var db = _Context.Database;
            db.SetCommandTimeout(TimeSpan.FromMinutes(10));
            using var transaction = db.BeginTransaction();
            db.ExecuteSqlRaw("EXEC [CreateSeedData] @RowCount = {0}", Count);

            transaction.Commit();
        }

        public async Task SeedTestDataAsync(int Count, CancellationToken Cancel = default)
        {
            await ClearDatabaseAsync(Cancel).ConfigureAwait(false);

            var db = _Context.Database;
            db.SetCommandTimeout(TimeSpan.FromMinutes(10));
            await using var transaction = await db.BeginTransactionAsync(Cancel).ConfigureAwait(false);
            await db.ExecuteSqlRawAsync("EXEC [CreateSeedData] @RowCount = {0}", Count).ConfigureAwait(false);

            await transaction.CommitAsync(Cancel).ConfigureAwait(false);
        }

        private const string __DeleteCommand = @"DELETE FROM [Orders]; DELETE FROM [Products]; DELETE FROM [Categories]";

        public void ClearDatabase()
        {
            var db = _Context.Database;
            var old_timeout = db.GetCommandTimeout();

            db.SetCommandTimeout(TimeSpan.FromMinutes(10));
            using var transaction = db.BeginTransaction();
            db.ExecuteSqlRaw(__DeleteCommand);
            transaction.Commit();

            db.SetCommandTimeout(old_timeout);
        }

        public async Task ClearDatabaseAsync(CancellationToken Cancel = default)
        {
            var db = _Context.Database;
            var old_timeout = db.GetCommandTimeout();

            db.SetCommandTimeout(TimeSpan.FromMinutes(10));
            await using var transaction = await db.BeginTransactionAsync(Cancel).ConfigureAwait(false);
            await db.ExecuteSqlRawAsync(__DeleteCommand, Cancel).ConfigureAwait(false);
            await transaction.CommitAsync(Cancel).ConfigureAwait(false);

            db.SetCommandTimeout(old_timeout);
        }
    }
}
