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

        public IActionResult Index() => View(_Products.Items.ToArray());

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _Products.Add(product);
            return RedirectToAction(nameof(Index));
        }
    }
}