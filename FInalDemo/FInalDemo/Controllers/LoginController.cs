using FInalDemo.Handler;
using FInalDemo.Models;
using System.Diagnostics;
using System.Web.Http;

namespace FInalDemo.Controllers
{
    [AllowAnonymous]
    public class LoginController : ApiController
    {
        private const string SecretKey = "4905a344929d3ec3aaf74194a9c84ce2852a848a7cec22e0c3ef0d194a6980da";

        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult Login([FromBody] LoginModel user)
        {
            // Validate input
            Debug.WriteLine(user.Password);
            if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Invalid username or password.");
            }

            Debug.WriteLine($"User attempted login: {user.Username}");

            // Authenticate user (use database or secure validation in production)
            if (user.Username == "admin@gmail.com" && user.Password == "admin1")
            {
                // Generate JWT token
                var token = JwtHelper.GenerateToken(user.Username, SecretKey);
                return Ok(new { Token = token });
            }

            // Authentication failed
            return Unauthorized();
        }
    }
}
