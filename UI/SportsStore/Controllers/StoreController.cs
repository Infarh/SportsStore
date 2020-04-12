using Microsoft.AspNetCore.Mvc;
using SportsStore.Domain.Models;
using SportsStore.Infrastructure.Extensions;
using SportsStore.Interfaces.Products;

namespace SportsStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly IProductsRepository _Products;
        private readonly ICategoriesRepository _Categories;

        public StoreController(IProductsRepository Products, ICategoriesRepository Categories)
        {
            _Products = Products;
            _Categories = Categories;
        }

        public IActionResult Index(
            [FromQuery(Name = "options")] QueryOptions ProductOptions,
            [FromQuery(Name = "category")] QueryOptions CategoryOptions,
            long CategoryId
        ) => this
           .With(CategoryOptions, (c, opt) => c.ViewBag.Categories = _Categories.Get(opt))
           .With(CategoryId, (c, id) => c.ViewBag.SelectedCategoryId = id)
           .View(_Products.Get(ProductOptions, CategoryId));
    }
}