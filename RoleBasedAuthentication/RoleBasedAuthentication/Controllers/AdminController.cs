using System.Web.Http;

namespace RoleBasedAuthentication.Controllers
{
    /// <summary>
    /// This controller is accessible only to users with the "Admin" role.
    /// </summary>
    [Auth] // Custom authorization filter to perform basic authentication
    [Authorize(Roles = "Admin")] // Ensures that only users with the "Admin" role can access the controller's actions
    public class AdminController : ApiController
    {
        /// <summary>
        /// This method handles GET requests and returns a message that is accessible only to users with the "Admin" role.
        /// </summary>
        /// <returns>An IHttpActionResult containing a success message for "Admin" role users.</returns>
        public IHttpActionResult Get()
        {
            // Returns a success message for "Admin" role users
            return Ok("This is admin only");
        }
    }
}
