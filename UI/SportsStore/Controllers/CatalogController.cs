using Microsoft.AspNetCore.Mvc;
using SportsStore.Domain.Models;
using SportsStore.Infrastructure.Extensions;
using SportsStore.Interfaces.Products;

namespace SportsStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductsRepository _Products;
        private readonly ICategoriesRepository _Categories;

        public CatalogController(IProductsRepository Products, ICategoriesRepository Categories)
        {
            _Products = Products;
            _Categories = Categories;
        }

        public IActionResult Index(QueryOptions Query) => View(_Products.GetQueryItems(Query));

        public IActionResult Index1(int Page = 0, int Size = 10) => View(_Products.Items.Page(Page, Size));

        [HttpPost]
        public IActionResult AddProduct(Product product) => this
           .With(product, _Products.Add)
           .RedirectToAction(nameof(Index));

        public IActionResult UpdateProduct(long? Id) => this
           .With(c => c.ViewBag.Catigories = c._Categories.Items)
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