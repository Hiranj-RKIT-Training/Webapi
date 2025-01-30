using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace JwtDemo
{
    /// <summary>
    /// Custom authorization filter that validates a JWT token in the request headers.
    /// </summary>
    public class JwtAuthorizeAttribute : AuthorizationFilterAttribute
    {
        // Secret key for validating JWT token, should be stored securely.
        private const string _secretKey = "49c34b4c475346e6a1c59c71e23512c00e5eeb7f874f966c9212ed1fabe9ae6e";

        /// <summary>
        /// Validates the JWT token passed in the Authorization header of the request.
        /// </summary>
        /// <param name="actionContext">The action context that provides information about the current HTTP request.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // Check if the authorization header exists and is of the "Bearer" scheme
            var authHeader = actionContext.Request.Headers.Authorization;
            if (authHeader == null || authHeader.Scheme != "Bearer" || string.IsNullOrEmpty(authHeader.Parameter))
            {
                // If not valid, return Unauthorized response
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                return;
            }

            // Extract the JWT token from the Authorization header
            var token = authHeader.Parameter;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            try
            {
                // Validate the JWT token using the secret key
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,  // Issuer validation can be enabled if needed
                    ValidateAudience = false // Audience validation can be enabled if needed
                }, out SecurityToken validatedToken);

                Debug.WriteLine("Token is valid.");

                // Parse the JWT token
                var jwtToken = (JwtSecurityToken)validatedToken;

                // Output all claims for debugging purposes
                foreach (var claim in jwtToken.Claims)
                {
                    Debug.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
                }

                // Extract the username from the "unique_name" claim
                var userName = jwtToken.Claims.First(x => x.Type == "unique_name").Value;

                // Create ClaimsIdentity from JWT claims and set the principal
                var identity = new ClaimsIdentity(jwtToken.Claims, "JWT");
                actionContext.RequestContext.Principal = new ClaimsPrincipal(identity);
            }
            catch
            {
                // If the token is invalid, return Unauthorized response
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
        }
    }
}
