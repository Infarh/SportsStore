using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SportsStore.Domain.Models;
using SportsStore.Interfaces.Products;

namespace SportsStore.Controllers
{
    [Route("api/Catalog")]
    [ApiController]
    [Produces("application/json")]
    public class CatalogApiController : ControllerBase
    {
        private readonly IProductsRepository _ProductsRepository;

        public CatalogApiController(IProductsRepository ProductsRepository) => _ProductsRepository = ProductsRepository;

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get() => _ProductsRepository.Items.ToArray();

        /// <summary>Получить товар по указанному идентификатору</summary>
        /// <param name="Id">Идентификатор товара, который требуется получить</param>
        /// <returns>Товар с указанным идентификатором</returns>
        /// <response code="200">Code 200 if product found</response>
        /// <response code="404">Code 404 if product not found</response>
        [HttpGet("{Id}")]
        public ActionResult<Product> Get(long Id)
        {
            var product = _ProductsRepository[Id];
            return product ?? (ActionResult<Product>)NotFound();
        }

        [HttpPost]
        public ActionResult<Product> Post([Bind("Name,Category,PurchasePrice,RetailPrice"), FromBody] Product Product)
        {
            _ProductsRepository.Add(Product);
            return CreatedAtAction(nameof(Get), new { Product.Id }, Product);
        }

        [HttpPut("{Id}")]
        public IActionResult Put(long Id, [Bind("Id,Name,Category,PurchasePrice,RetailPrice"), FromBody] Product Product)
        {
            if (Id != Product.Id)
                return BadRequest();

            _ProductsRepository.Update(Product);
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public ActionResult<Product> Delete(long Id)
        {
            var product = _ProductsRepository[Id];
            if (product is null) return NotFound();
            _ProductsRepository.Delete(product);
            return product;
        }
    }
}