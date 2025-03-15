using Faces.Database.Repositories;
using Faces.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Faces.Authentication.Services
{
    public interface IJwtService
    {
        string GenerateToken(Employee employee);

        string GetEmployeeDocument(string token);
    }

    public class JwtService : IJwtService
    {
        private readonly IEmployeeRepository _employeeRepository;

        private readonly string _jwtSecretKey;

        private readonly string _issuer;

        private readonly string _audience;

        public JwtService(IEmployeeRepository employeeRepository, IConfiguration config)
        {
            _employeeRepository = employeeRepository;
            _jwtSecretKey = config["Jwt:Key"]!;
            _issuer = config["Jwt:Issuer"]!;
            _audience = config["Jwt:Audience"]!;
        }
        public string GenerateToken(Employee employee)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, employee.DocNumber),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, employee.Email),
            new Claim(JwtRegisteredClaimNames.GivenName, employee.FullName),
            new Claim(ClaimTypes.Role, "Employee")
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GetEmployeeDocument(string token)
        {

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            return jwtToken.Subject;
        }
    }
}
