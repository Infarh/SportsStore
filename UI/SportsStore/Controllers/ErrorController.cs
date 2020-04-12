using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(string Code) => Code switch
        {
            "404" => View("Error404"),
            _ => View(model: Code)
        };
    }
}