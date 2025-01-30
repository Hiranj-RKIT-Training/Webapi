using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace ExceptionDemo
{
    /// <summary>
    /// Custom exception filter to handle unexpected errors globally or at the controller/action level.
    /// </summary>
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Overrides the base method to handle exceptions and generate a custom error response.
        /// </summary>
        /// <param name="actionExecutedContext">The context for the executed action where the exception occurred.</param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            // Create a custom response for unhandled exceptions
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("An unexpected error occurred. Please try again later."),
                ReasonPhrase = "Internal Server Error",
            };

            // Assign the custom response to the context
            actionExecutedContext.Response = response;
        }
    }
}
