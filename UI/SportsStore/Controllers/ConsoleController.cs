using System;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Infrastructure.Extensions;

namespace SportsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsoleController : ControllerBase
    {
        [HttpGet("Clear")]
        public IActionResult Clear() => this.Execute(Console.Clear).Ok();

        [HttpGet("Write/{Text}")]
        public IActionResult Write(string Text) => this.With(Text, Console.Write).Ok();

        [HttpGet("WriteLine/{Text?}")]
        public IActionResult WriteLine(string Text) => this.With(Text, Console.WriteLine).Ok();
    }
}