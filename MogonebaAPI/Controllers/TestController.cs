using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MogonebaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("message")]
        public IActionResult GetMessage()
        {
            return Ok(new { message = "Hello from ASP.NET Core API!" });
        }
    }
}
