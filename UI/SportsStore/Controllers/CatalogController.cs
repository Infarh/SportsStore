using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Domain.Models;
using SportsStore.Interfaces.Products;

namespace SportsStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductsRepository _Products;

        public CatalogController(IProductsRepository Products) => _Products = Products;

        public IActionResult Index() => View(_Products.Items as IQueryable<Product>);

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _Products.Add(product);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateProduct(long Id) => View(_Products[Id]);

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult UpdateProduct(Product Product)
        {
            if (!ModelState.IsValid) return View(Product);
            _Products.Update(Product);
            return RedirectToAction("Index");
        }

        public IActionResult UpdateAll()
        {
            ViewBag.UpdateAll = true;
            return View(nameof(Index), _Products.Items as IQueryable<Product>);
        }

        [HttpPost]
        public IActionResult UpdateAll(Product[] Products)
        {
            _Products.UpdateAll(Products);
            return RedirectToAction(nameof(Index));
        }
    }
}