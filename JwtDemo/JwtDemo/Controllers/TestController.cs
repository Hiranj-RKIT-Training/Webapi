using System;
using System.Collections.Generic;
using System.Web.Http;

namespace JwtDemo.Controllers
{
    public class TestController : ApiController
    {

        [JwtAuthorize]
        [HttpGet]
        public IHttpActionResult get()
        {
            return Ok(new List<String>() {
            "value 1",
            "value 2",
            });
        }

        [HttpPost]
        public IHttpActionResult login([FromUri] String userName, [FromUri] String Password)
        {
            if (userName == "admin" && Password == "admin1")
            {
                var token = JwtHelper.GenerateToken(userName, "49c34b4c475346e6a1c59c71e23512c00e5eeb7f874f966c9212ed1fabe9ae6e");
                return Ok(new
                {
                    Token = token
                });
            }
            return Unauthorized();
        }
    }
}
