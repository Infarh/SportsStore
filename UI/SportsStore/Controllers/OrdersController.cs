using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Domain.Models.Orders;
using SportsStore.Interfaces.Products;

namespace SportsStore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository _Orders;
        private readonly IProductsRepository _Products;

        public OrdersController(IOrdersRepository Orders, IProductsRepository Products)
        {
            _Orders = Orders;
            _Products = Products;
        }

        public IActionResult Index() => View(_Orders.Items);

        public IActionResult Edit(long Id)
        {
            var order = Id == 0 ? new Order() : _Orders[Id];
            if (order is null)
                return NotFound();

            var lines = order.Lines?.ToDictionary(line => line.ProductId) ?? new Dictionary<long, OrderLine>();
            ViewBag.Lines = _Products.Items.Select(product => lines.TryGetValue(product.Id, out var line) 
                ? line 
                : new OrderLine
                {
                    Product = product, 
                    ProductId = product.Id,
                    Quantity = 0
                });
            return View(order);
        }

        [HttpPost]
        public IActionResult Edit(Order Order)
        {
            if (!ModelState.IsValid)
                return View(nameof(Edit), Order);

            Order.Lines = Order.Lines.Where(line => line.Id > 0 || line.Id == 0 && line.Quantity > 0).ToArray();
            if(Order.Id == 0)
                _Orders.Add(Order);
            else
                _Orders.Update(Order);

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(Order Order)
        {
            _Orders.Delete(Order);
            return RedirectToAction(nameof(Index));
        }

    }
}