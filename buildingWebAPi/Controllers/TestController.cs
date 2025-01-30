using System;
using System.Web.Http;

namespace buildingWebAPi.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        public String get()
        {
            return "hello world";
        }
        [HttpGet]
        public string Get(string id)
        {
            Console.WriteLine(id);
            return id;
        }
        public String post([FromBody] String text)
        {
            Console.WriteLine(text);
            return text;
        }

    }
}
