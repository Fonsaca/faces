using Faces.Authentication.DTOs;
using Faces.Database.Repositories;
using Faces.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Faces.Authentication.Services
{
    public interface IAuthenticateService
    {
        string AuthenticateByCredentials(Credentials credentials);
    }

    public class AuthenticateService : IAuthenticateService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IJwtService _jwtService;


        public AuthenticateService(IEmployeeRepository employeeRepository, IJwtService jwtService)
        {
            _employeeRepository = employeeRepository;
            _jwtService = jwtService;
        }

        public string AuthenticateByCredentials(Credentials credentials)
        {
            var employee = _employeeRepository.GetByDocument(credentials.Document);
            if (employee == null)
                throw new KeyNotFoundException("Employee not found");

            if (!employee.HasValidPassword(credentials.Password))
                throw new UnauthorizedAccessException("Invalid password");

            return _jwtService.GenerateToken(employee);

        }

        

    }

    
}
