using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Controllers
{
    public class CatalogController : Controller
    {
        public IActionResult Index() => View();
    }
}