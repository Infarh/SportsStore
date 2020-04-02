using Microsoft.EntityFrameworkCore;
using SportsStore.Domain.Models;

namespace SportsStore.DAL.Context
{
    public class SportStoreDB : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public SportStoreDB(DbContextOptions<SportStoreDB> Options) : base(Options) { }
    }
}
