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
            await _Context.Database.MigrateAsync(cancellationToken: Cancel).ConfigureAwait(false);

            await InitializeStandartData(Cancel).ConfigureAwait(false);
        }

        private async Task InitializeStandartData(CancellationToken Cancel = default)
        {
            if (!await _Context.Products.AnyAsync(cancellationToken: Cancel).ConfigureAwait(false))
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
                await _Context.SaveChangesAsync(Cancel).ConfigureAwait(false);
            }
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
