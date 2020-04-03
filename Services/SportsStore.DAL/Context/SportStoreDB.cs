using Microsoft.EntityFrameworkCore;
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
    }
}
