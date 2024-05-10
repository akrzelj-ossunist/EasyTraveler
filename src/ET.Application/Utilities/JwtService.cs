using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;

namespace ET.Application.Utilities
{
    public class JwtService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;


        public JwtService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static class SecretKeyGenerator
        {
            public static string GenerateRandomSecretKey(int keySize = 32)
            {
                byte[] keyBytes = new byte[keySize];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(keyBytes);
                }
                return Convert.ToBase64String(keyBytes);
            }
        }

        public string GenerateJwtToken(string role, string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKeyGenerator.GenerateRandomSecretKey());

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                new System.Security.Claims.Claim("role", role),
                new System.Security.Claims.Claim("id", userId)
            }),
                Expires = DateTime.UtcNow.AddDays(7), // Token expiration time
                Issuer = "EasyTraveler",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GetJwtTokenFromSession()
        {
            // Retrieve JWT token from session
            return _httpContextAccessor.HttpContext.Session.GetString("_JwtToken");
        }
    }
}
