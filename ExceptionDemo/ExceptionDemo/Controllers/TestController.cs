using System;
using System.Diagnostics;
using System.Web.Http;

namespace ExceptionDemo.Controllers
{
    /// <summary>
    /// Controller to demonstrate exception handling with custom exception filters.
    /// </summary>
    [CustomExceptionFilter]
    public class TestController : ApiController
    {
        /// <summary>
        /// A test endpoint that deliberately throws an exception to demonstrate custom exception handling.
        /// </summary>
        /// <returns>
        /// Returns an <see cref="IHttpActionResult"/> indicating an unauthorized response.
        /// </returns>
        [CustomExceptionFilter]
        public IHttpActionResult Get()
        {
            try
            {
                // Intentionally throwing an exception to test exception handling.
                throw new Exception("Test Execution");
            }
            catch (Exception ex)
            {
                // Logs the exception for debugging purposes.
                Debug.WriteLine(ex, "An exception occurred.");
            }

            // Return an unauthorized response as a fallback.
            return Unauthorized();
        }
    }
}
