using System;
using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsoleController : ControllerBase
    {
        public IActionResult Clear()
        {
            Console.Clear();
            return Ok();
        }

        public IActionResult Write(string Text)
        {
            Console.Write(Text);
            return Ok();
        }

        [Route("WriteLine/{Text?}")]
        public IActionResult WriteLine(string Text)
        {
            Console.WriteLine(Text);
            return Ok();
        }
    }
}