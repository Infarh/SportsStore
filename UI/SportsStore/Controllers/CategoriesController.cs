using Microsoft.AspNetCore.Mvc;
using SportsStore.Domain.Models;
using SportsStore.Infrastructure.Extensions;
using SportsStore.Interfaces.Products;

namespace SportsStore.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoriesRepository _CategoriesRepository;

        public CategoriesController(ICategoriesRepository CategoriesRepository) => _CategoriesRepository = CategoriesRepository;

        public IActionResult Index() => View(_CategoriesRepository.Items);

        [HttpPost]
        public IActionResult AddCategory(Category Category) => this
           .With(Category, _CategoriesRepository.Add)
           .RedirectToIndex();

        public IActionResult EditCategory(long Id) => this
           .With(Id, (c, id) => c.ViewBag.EditId = id)
           .ViewIndex(_CategoriesRepository.Items);

        [HttpPost]
        public IActionResult UpdateCategory(Category Category) => this
           .With(Category, _CategoriesRepository.Update)
           .RedirectToIndex();

        [HttpPost]
        public IActionResult DeleteCategory(Category Category) => this
           .With(Category, _CategoriesRepository.Delete)
           .RedirectToIndex();
    }
}