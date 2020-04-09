using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SportsStore.Domain.Models;
using SportsStore.Domain.Models.Orders;

namespace SportsStore.DAL.Context
{
    public class SportStoreDB : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderLine> OrderLines { get; set; }

        public SportStoreDB(DbContextOptions<SportStoreDB> Options) : base(Options) { }

        protected override void OnModelCreating(ModelBuilder db)
        {
            db.Entity<Product>().HasIndex(p => p.Name);
            db.Entity<Product>().HasIndex(p => p.PurchasePrice);
            db.Entity<Product>().HasIndex(p => p.RetailPrice);
            db.Entity<Category>().HasIndex(c => c.Name);
            db.Entity<Category>().HasIndex(c => c.Description);
        }

        public void SeedTestData(int Count)
        {
            ClearDatabase();

            var db = Database;
            db.SetCommandTimeout(TimeSpan.FromMinutes(10));
            using var transaction = db.BeginTransaction();
            db.ExecuteSqlRaw("EXEC [CreateSeedData] @RowCount = {0}", Count);

            transaction.Commit();
        }

        public async Task SeedTestDataAsync(int Count, CancellationToken Cancel = default)
        {
            await ClearDatabaseAsync(Cancel).ConfigureAwait(false);

            var db = Database;
            db.SetCommandTimeout(TimeSpan.FromMinutes(10));
            await using var transaction = await db.BeginTransactionAsync(Cancel).ConfigureAwait(false);
            await db.ExecuteSqlRawAsync("EXEC [CreateSeedData] @RowCount = {0}", Count).ConfigureAwait(false);

            await transaction.CommitAsync(Cancel).ConfigureAwait(false);
        }

        private const string __DeleteCommand = @"DELETE FROM [Orders]; DELETE FROM [Products]; DELETE FROM [Categories]";

        public void ClearDatabase()
        {
            var db = Database;
            var old_timeout = db.GetCommandTimeout();

            db.SetCommandTimeout(TimeSpan.FromMinutes(10));
            using var transaction = db.BeginTransaction();
            db.ExecuteSqlRaw(__DeleteCommand);
            transaction.Commit();

            db.SetCommandTimeout(old_timeout);
        }

        public async Task ClearDatabaseAsync(CancellationToken Cancel = default)
        {
            var db = Database;
            var old_timeout = db.GetCommandTimeout();

            db.SetCommandTimeout(TimeSpan.FromMinutes(10));
            await using var transaction = await db.BeginTransactionAsync(Cancel).ConfigureAwait(false);
            await db.ExecuteSqlRawAsync(__DeleteCommand, Cancel).ConfigureAwait(false);
            await transaction.CommitAsync(Cancel).ConfigureAwait(false);

            db.SetCommandTimeout(old_timeout);
        }
    }
}
