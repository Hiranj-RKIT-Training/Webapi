using System.Web.Http;

namespace RoleBasedAuthentication.Controllers
{
    /// <summary>
    /// This controller allows access to everyone, regardless of authentication or authorization.
    /// </summary>
    [AllowAnonymous] // Allows access without requiring authentication or roles
    public class AllController : ApiController
    {
        /// <summary>
        /// This method handles GET requests and returns a message that is accessible by all users.
        /// </summary>
        /// <returns>An IHttpActionResult containing a success message that can be accessed by anyone.</returns>
        public IHttpActionResult Get()
        {
            // Returns a message accessible to all users without authentication or authorization
            return Ok("All can access");
        }
    }
}
