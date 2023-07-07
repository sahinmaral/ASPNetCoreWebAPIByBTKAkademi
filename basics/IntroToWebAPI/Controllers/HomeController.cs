using IntroToWebAPI.Models;

using Microsoft.AspNetCore.Mvc;

using System.Net;

namespace IntroToWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetMessage()
        {
            return Ok(new ResponseModel()
                {
                    Message = "Hello ASP.NET Core Web API",
                    StatusCode = (int)HttpStatusCode.OK
                });
        }
    }
}
