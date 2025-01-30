using FInalDemo.Handler;
using System.Web.Http;

namespace FInalDemo.Controllers
{
    [JwtAuth]
    public class DataController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetData()
        {
            return Ok("data");
        }
    }
}
