using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eUseControl.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("pong");
        }
    }
}
