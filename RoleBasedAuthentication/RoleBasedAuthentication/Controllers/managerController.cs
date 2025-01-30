using System.Web.Http;

namespace RoleBasedAuthentication.Controllers
{
    /// <summary>
    /// This controller handles requests that are authorized for users with the "Manager" role.
    /// </summary>
    [Auth] // Custom authorization filter attribute for basic authentication
    [Authorize(Roles = "Manager")] // Restricts access to users with the "Manager" role
    public class ManagerController : ApiController
    {
        /// <summary>
        /// This method handles GET requests and returns a message that is accessible only to users with the "Manager" role.
        /// </summary>
        /// <returns>An IHttpActionResult containing a success message for "Manager" role users.</returns>
        public IHttpActionResult Get()
        {
            // Returns a success message for manager-only access
            return Ok("This is manager only");
        }
    }
}
