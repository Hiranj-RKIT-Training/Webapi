using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace basicAuth
{
    /// <summary>
    /// Custom authorization attribute for implementing basic authentication.
    /// </summary>
    public class BasicAuthAttribute : AuthorizationFilterAttribute
    {
        /// <summary>
        /// Performs authorization by checking the Authorization header in the request.
        /// </summary>
        /// <param name="actionContext">The HTTP action context for the current request.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // Check if the Authorization header is present.
            if (actionContext.Request.Headers.Authorization == null)
            {
                // If no Authorization header, respond with Unauthorized status.
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                // Extract the Base64 encoded credentials from the Authorization header.
                string authToken = actionContext.Request.Headers.Authorization.Parameter;

                // Decode the Base64 string to get the credentials in UTF-8 format.
                string utf8AuthToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));

                // Split the credentials into username and password.
                string[] creds = utf8AuthToken.Split(':');
                string userName = creds[0];
                string password = creds[1];

                // Validate the credentials.
                if (IsAuthorized(userName, password))
                {
                    // If authorized, set the current principal for the thread.
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(userName), null);
                }
                else
                {
                    // If not authorized, respond with Unauthorized status.
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }

        /// <summary>
        /// Validates the provided username and password.
        /// </summary>
        /// <param name="username">The username to validate.</param>
        /// <param name="password">The password to validate.</param>
        /// <returns>True if the credentials are valid; otherwise, false.</returns>
        private bool IsAuthorized(string username, string password)
        {
            // Hardcoded authentication for demonstration purposes.
            return username == "hk8880" && password == "hk8880";
        }
    }
}
