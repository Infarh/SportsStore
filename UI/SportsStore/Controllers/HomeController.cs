using System;
using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Clear(string ReturnUrl = null)
        {
            ReturnUrl ??= Request.Headers["Referer"].ToString();
            Console.Clear();
            Console.WriteLine("Return to {0}", ReturnUrl);
            return Redirect(ReturnUrl);
        }
    }
}