using Microsoft.AspNetCore.Mvc;
using SportsStore.Interfaces.Products;

namespace SportsStore.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersApiController : ControllerBase
    {
        private readonly IOrdersRepository _Orders;
        private readonly IProductsRepository _Products;

        public OrdersApiController(IOrdersRepository Orders, IProductsRepository Products)
        {
            _Orders = Orders;
            _Products = Products;
        }
    }
}