using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace RoleBasedAuthentication
{
    /// <summary>
    /// Custom authorization filter for handling basic authentication and role-based authorization.
    /// </summary>
    public class AuthAttribute : AuthorizationFilterAttribute
    {
        /// <summary>
        /// This method overrides the OnAuthorization method to handle basic authentication and assign roles to the user.
        /// </summary>
        /// <param name="actionContext">The context for the current HTTP request.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // Retrieve the Authorization header from the request
            var authHeader = actionContext.Request.Headers.Authorization;

            // If no authorization header or if the scheme is not "Basic", return Unauthorized
            if (authHeader == null || authHeader.Scheme != "Basic")
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return;
            }

            // Decode the credentials from the Authorization header
            var creds = Encoding.ASCII.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');
            var userName = creds[0];
            var password = creds[1];

            // Validate the user's credentials
            if (!ValidateUserCredentials(userName, password))
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return;
            }

            // Get the roles for the user and create a principal
            var userRoles = GetUserRoles(userName);
            var principal = new System.Security.Principal.GenericPrincipal(new System.Security.Principal.GenericIdentity(userName), userRoles);

            // Set the principal for the current thread
            Thread.CurrentPrincipal = principal;

            // Set the user in the HttpContext for ASP.NET compatibility
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }

        /// <summary>
        /// Validates the user's credentials against a hardcoded set of values.
        /// </summary>
        /// <param name="username">The username of the user attempting to authenticate.</param>
        /// <param name="password">The password of the user attempting to authenticate.</param>
        /// <returns>True if the credentials are valid, otherwise false.</returns>
        private bool ValidateUserCredentials(string username, string password)
        {
            // Example: Validating hardcoded username and password
            return (username == "admin" || username == "manager") && password == "password";
        }

        /// <summary>
        /// Retrieves the roles assigned to a user based on their username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>An array of roles assigned to the user.</returns>
        private string[] GetUserRoles(string username)
        {
            // Example: Hardcoded roles for the user
            if (username == "admin")
                return new string[] { "Admin", "Manager" };
            else if (username == "manager")
                return new string[] { "Manager" };

            // Return an empty array if no roles are found
            return new string[] { };
        }
    }
}
