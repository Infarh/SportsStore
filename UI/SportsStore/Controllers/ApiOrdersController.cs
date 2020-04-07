using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Domain.Models.Orders;
using SportsStore.Infrastructure.Extensions;
using SportsStore.Interfaces.Products;

namespace SportsStore.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class ApiOrdersController : ControllerBase
    {
        private readonly IOrdersRepository _Orders;

        private static readonly Expression<Func<Order, Order>> __OrderMapper = o => new Order
        {
            Id = o.Id,
            CustomerName = o.CustomerName,
            Address = o.Address,
            ZipCode = o.ZipCode,
            State = o.State,
            Shipped = o.Shipped,
            Lines = o.Lines.Select(l => new OrderLine
            {
                Id = l.Id,
                ProductId = l.ProductId,
                Quantity = l.Quantity
            })
        };

        public ApiOrdersController(IOrdersRepository Orders) => _Orders = Orders;

        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get() => _Orders.Query.Select(__OrderMapper).ToArray();

        [HttpGet("{Id}")]
        public ActionResult<Order> Get(long Id) => _Orders.Query.Select(__OrderMapper).FirstOrDefault(o => o.Id == Id) ?? (ActionResult<Order>)NotFound();

        [HttpGet("page/{Page}({PageSize})")]
        public ActionResult<IEnumerable<Order>> Get(int Page, int PageSize) => _Orders.Get(Page, PageSize).Select(__OrderMapper).ToArray();

        [HttpPost]
        public ActionResult<Order> Post(Order Order) => this
           .With(Order, _Orders.Add)
           .CreatedAtAction(nameof(Get), new {Order.Id}, Order);

        [HttpPut("{Id}")]
        public IActionResult Put(long Id, Order Order)
        {
            if (Id != Order.Id) 
                return BadRequest();

            _Orders.Update(Order);
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public ActionResult<Order> Delete(long Id)
        {
            var order = _Orders.Query.Select(__OrderMapper).FirstOrDefault(o => o.Id == Id);
            if (order is null) return NotFound();

            _Orders.Delete(order);
            return order;
        }
    }
}