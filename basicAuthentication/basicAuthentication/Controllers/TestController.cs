using basicAuth;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace basicAuthentication.Controllers
{
    /// <summary>
    /// A test controller to demonstrate basic authentication.
    /// </summary>
    [BasicAuth]
    public class TestController : ApiController
    {
        /// <summary>
        /// Retrieves the username of the authenticated user.
        /// </summary>
        /// <returns>An HTTP response containing the username of the authenticated user.</returns>
        public HttpResponseMessage Get()
        {
            // Retrieve the username from the current thread's principal identity.
            string username = Thread.CurrentPrincipal.Identity.Name;

            // Return the username in the response with an HTTP 200 status code.
            return Request.CreateResponse(HttpStatusCode.OK, username);
        }
    }
}
