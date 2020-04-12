using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SportsStore.Domain.Models;
using SportsStore.Infrastructure.Extensions;
using SportsStore.Interfaces.Products;

namespace SportsStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductsRepository _Products;
        private readonly ICategoriesRepository _Categories;
        private readonly ILogger<CatalogController> _Logger;

        public CatalogController(IProductsRepository Products, ICategoriesRepository Categories, ILogger<CatalogController> Logger)
        {
            _Products = Products;
            _Categories = Categories;
            _Logger = Logger;
        }

        public IActionResult Index(QueryOptions Query)
        {
            _Logger.LogInformation("Запрос товаров из каталога: страница {0}, выборка {1} штук", Query.Page, Query.Size);
            var timer = Stopwatch.StartNew();
            var page = _Products.GetQuery(Query);
            timer.Stop();
            _Logger.LogInformation("Страница сформирована за {0:0.##}с", timer.Elapsed.TotalSeconds);
            return View(page);
        }

        public IActionResult Index1(int Page = 0, int Size = 10) => View(_Products.Items.Page(Page, Size));

        [HttpPost]
        public IActionResult AddProduct(Product product) => this
           .With(product, _Products.Add)
           .RedirectToAction(nameof(Index));

        public IActionResult UpdateProduct(long? Id) => this
           .With(c => c.ViewBag.Categories = c._Categories.Items)
           .View(Id > 0 ? _Products[(long)Id] : new Product());

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult UpdateProduct(Product Product)
        {
            if (Product.Id < 0) return BadRequest();
            if (!ModelState.IsValid) return View(Product);

            if (Product.Id == 0)
                _Products.Add(Product);
            else
                _Products.Update(Product);

            return this.RedirectToIndex();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(Product product) => this
           .With(product, _Products.Delete)
           .RedirectToIndex();
    }
}