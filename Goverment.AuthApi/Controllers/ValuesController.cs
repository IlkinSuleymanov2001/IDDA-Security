using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Goverment.AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        [HttpGet]
        public void GET() 
        {
            throw new NotImplementedException();
        }
    }
}
