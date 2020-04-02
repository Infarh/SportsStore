using System;
using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsoleController : ControllerBase
    {
        [HttpGet("Clear")]
        public IActionResult Clear()
        {
            Console.Clear();
            return Ok();
        }

        [HttpGet("Write/{Text}")]
        public IActionResult Write(string Text)
        {
            Console.Write(Text);
            return Ok();
        }

        [HttpGet("WriteLine/{Text?}")]
        public IActionResult WriteLine(string Text)
        {
            Console.WriteLine(Text);
            return Ok();
        }
    }
}