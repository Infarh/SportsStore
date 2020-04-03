using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index() => View();
    }
}