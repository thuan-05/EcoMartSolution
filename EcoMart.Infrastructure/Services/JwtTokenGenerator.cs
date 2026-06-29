using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EcoMart.Core.Entities;
using EcoMart.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EcoMart.Infrastructure.Services
{
    
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            string secretKey = _configuration["JwtSettings:SecretKey"]!;
            string issuer    = _configuration["JwtSettings:Issuer"]!;
            string audience  = _configuration["JwtSettings:Audience"]!;
            int    expMinutes = int.Parse(_configuration["JwtSettings:ExpirationMinutes"]!);

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Account!.Role)
            };

            var token = new JwtSecurityToken(
                issuer:             issuer,      
                audience:           audience,    
                claims:             claims,         
                expires:            DateTime.UtcNow.AddMinutes(expMinutes), 
                signingCredentials: credentials  
            );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
