using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Domain.Models;
using SportsStore.Interfaces.Products;

namespace SportsStore.Controllers
{
    [Route("api/catalog")]
    [ApiController]
    [Produces("application/json")]
    public class ApiCatalogController : ControllerBase
    {
        private readonly IProductsRepository _Products;
        private readonly ICategoriesRepository _Categories;

        public ApiCatalogController(IProductsRepository Products, ICategoriesRepository Categories)
        {
            _Products = Products;
            _Categories = Categories;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get() => _Products.Items.ToArray();

        [HttpGet("page/{Page}({PageSize})")]
        public ActionResult<IEnumerable<Product>> Get(int Page, int PageSize) => _Products.Get(Page, PageSize).ToArray();

        [HttpGet("count")]
        public int ProductsCount() => _Products.Query.Count();

        [HttpGet("category")]
        public ActionResult<IEnumerable<Category>> GetCategories() => _Categories.Items.ToArray();

        [HttpGet("category/page/{Page}({PageSize})")]
        public ActionResult<IEnumerable<Category>> GetCategories(int Page, int PageSize) => _Categories.Get(Page, PageSize).ToArray();

        [HttpGet("category/count")]
        public int CategoriesCount() => _Categories.Query.Count();

        /// <summary>Получить товар по указанному идентификатору</summary>
        /// <param name="Id">Идентификатор товара, который требуется получить</param>
        /// <returns>Товар с указанным идентификатором</returns>
        /// <response code="200">Code 200 if product found</response>
        /// <response code="404">Code 404 if product not found</response>
        [HttpGet("{Id}")]
        public ActionResult<Product> Get(long Id)
        {
            var product = _Products[Id];
            return product ?? (ActionResult<Product>)NotFound();
        }

        [HttpGet("category/{Id}")]
        public ActionResult<Category> GetCategory(long Id)
        {
            var category = _Categories[Id];
            return category ?? (ActionResult<Category>) NotFound();
        }

        [HttpPost]
        public ActionResult<Product> Post([Bind("Name,Category,PurchasePrice,RetailPrice"), FromBody] Product Product)
        {
            _Products.Add(Product);
            return CreatedAtAction(nameof(Get), new { Product.Id }, Product);
        }

        [HttpPut("{Id}")]
        public IActionResult Put(long Id, [Bind("Id,Name,Category,PurchasePrice,RetailPrice"), FromBody] Product Product)
        {
            if (Id != Product.Id)
                return BadRequest();

            _Products.Update(Product);
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public ActionResult<Product> Delete(long Id)
        {
            var product = _Products[Id];
            if (product is null) return NotFound();
            _Products.Delete(product);
            return product;
        }
    }
}