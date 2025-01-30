using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FInalDemo.Handler
{
    public class JwtAuthAttribute : AuthorizationFilterAttribute
    {
        private const string _secretKey = "4905a344929d3ec3aaf74194a9c84ce2852a848a7cec22e0c3ef0d194a6980da";
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.Authorization;
            if (authHeader == null || authHeader.Scheme != "Bearer" || String.IsNullOrEmpty(authHeader.Parameter))
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(HttpStatusCode.Unauthorized);
                return;
            }
            var token = authHeader.Parameter;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                //var username = jwtToken.Claims.First(x => x.Type == ClaimTypes.Name).Value;
                //Debug.WriteLine(username);

                // Set the user identity
                var identity = new ClaimsIdentity(jwtToken.Claims, "JWT");
                actionContext.RequestContext.Principal = new ClaimsPrincipal(identity);
            }
            catch
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

            }
        }
    }
}