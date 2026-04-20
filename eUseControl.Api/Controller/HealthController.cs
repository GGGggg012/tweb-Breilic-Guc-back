using Microsoft.AspNetCore.Mvc;

namespace eUseControl.Api.Controller
{
    [Route("api/health")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok(new { status = "ok", message = "pong" });
        }

        [HttpGet("version")]
        public IActionResult Version()
        {
            return Ok(new { version = "v1.0", name = "eAviaSales API" });
        }
    }
}
