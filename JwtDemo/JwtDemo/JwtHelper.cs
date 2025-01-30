using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtDemo
{
    /// <summary>
    /// Helper class for generating and handling JWT tokens.
    /// </summary>
    public class JwtHelper
    {
        /// <summary>
        /// Generates a JWT token for a user with specified roles and expiration time.
        /// </summary>
        /// <param name="userName">The username of the user for whom the token is being generated.</param>
        /// <param name="secretKey">The secret key used to sign the JWT token.</param>
        /// <param name="expiryMins">The expiration time of the token in minutes. Default is 60 minutes.</param>
        /// <returns>The generated JWT token as a string.</returns>
        public static string GenerateToken(string userName, string secretKey, int expiryMins = 60)
        {
            // Create a new JwtSecurityTokenHandler to handle token creation
            var tokenHandler = new JwtSecurityTokenHandler();

            // Convert the secret key to a byte array
            var key = Encoding.ASCII.GetBytes(secretKey);

            // Create the token descriptor with claims, expiration time, and signing credentials
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Add claims (name and role) to the token
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, "User") // Assign a role, can be modified as needed
                }),

                // Set the expiration time of the token
                Expires = DateTime.UtcNow.AddMinutes(expiryMins),

                // Set the signing credentials (using HMACSHA256 algorithm)
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Create the token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Return the token as a string
            return tokenHandler.WriteToken(token);
        }
    }
}
