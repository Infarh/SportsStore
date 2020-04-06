﻿using System.Linq;
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

        public CatalogController(IProductsRepository Products, ICategoriesRepository Categories)
        {
            _Products = Products;
            _Categories = Categories;
        }

        public IActionResult Index(int Page = 0, int Size = 10) => View(_Products.GetPagedItems(new PageOptions { Page = Page, Size = Size }));

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _Products.Add(product);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateProduct(long? Id)
        {
            ViewBag.Categories = _Categories.Items;
            return View(Id > 0 ? _Products[(long)Id] : new Product());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult UpdateProduct(Product Product)
        {
            if (Product.Id < 0) return BadRequest();
            if (!ModelState.IsValid) return View(Product);

            if (Product.Id == 0)
                _Products.Add(Product);
            else
                _Products.Update(Product);

            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(Product product)
        {
            _Products.Delete(product);
            return RedirectToAction(nameof(Index));
        }
    }
}