using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Domain.Models;
using SportsStore.Infrastructure.Extensions;
using SportsStore.Interfaces.Products;

namespace SportsStore.Controllers
{
    [Route("api/seed")]
    [ApiController]
    public class ApiSeedDbDataController : ControllerBase
    {
        private readonly IProductsRepository _Products;
        private readonly ICategoriesRepository _Categories;
        private readonly Random _Random = new Random();

        public ApiSeedDbDataController(IProductsRepository Products, ICategoriesRepository Categories)
        {
            _Products = Products;
            _Categories = Categories;
        }

        [HttpPost("products/{count}")]
        public IActionResult SeedProducts(int Count)
        {
            var categories_ids = _Categories.Query.Select(c => c.Id).ToArray();
            var product_max_id = _Products.Query.Max(p => p.Id);

            _Products.AddRange(Enumerable.Range(1, Count).Select(i =>
            {
                var price = 1 + _Random.NextDouble() * 100;
                return new Product
                {
                    Name = $"Product {i + product_max_id}",
                    CategoryId = categories_ids[_Random.Next(categories_ids.Length)],
                    PurchasePrice = (decimal)price,
                    RetailPrice = (decimal)(price * 1.1)
                };
            }));

            return Ok();
        }

        [HttpPost("categories/{count}")]
        public IActionResult SeedCategories(int Count)
        {
            var max_id = _Categories.Query.Max(p => p.Id);

            _Categories.AddRange(Enumerable.Range(1, Count).Select(i => new Category { Name = $"Category {i + max_id}" }));

            return Ok();
        }

        [HttpPost("products/clear")]
        public IActionResult ClearProducts() => this.With(c => c._Products.Clear()).Ok();

        [HttpPost("categories/clear")]
        public IActionResult ClearCategories() => this.With(c => c._Categories.Clear()).Ok();
    }
}