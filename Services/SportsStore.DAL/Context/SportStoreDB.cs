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
    }
}
