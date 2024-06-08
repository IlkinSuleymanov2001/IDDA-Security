using Goverment.AuthApi.Business.Dtos.Request.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Goverment.AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post() 
        {
            return Ok("salam");
        }

        [HttpPost("number")]
        public IActionResult Post2([FromBody] UserLoginRequest loginRequest)
        {
            return Ok(loginRequest);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginRequest loginRequest)
        {
            //var tokens = await _authService.Login(loginRequest);
            //Task.Run(() => Console.WriteLine("feewg4g")).Wait();
            return Ok(loginRequest);
        }
    }
}
