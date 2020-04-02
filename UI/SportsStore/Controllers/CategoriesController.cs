using Microsoft.AspNetCore.Mvc;
using SportsStore.Domain.Models;
using SportsStore.Interfaces.Products;

namespace SportsStore.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoriesRepository _CategoriesRepository;

        public CategoriesController(ICategoriesRepository CategoriesRepository) => _CategoriesRepository = CategoriesRepository;

        public IActionResult Index() => View(_CategoriesRepository.Items);

        [HttpPost]
        public IActionResult AddCategory(Category Category)
        {
            _CategoriesRepository.Add(Category);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditCategory(long Id)
        {
            ViewBag.EditId = Id;
            return View(nameof(Index), _CategoriesRepository.Items);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category Category)
        {
            _CategoriesRepository.Update(Category);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteCategory(Category Category)
        {
            _CategoriesRepository.Delete(Category);
            return RedirectToAction(nameof(Index));
        }
    }
}