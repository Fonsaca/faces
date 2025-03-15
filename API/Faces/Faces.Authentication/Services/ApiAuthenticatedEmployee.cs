using Faces.Application.Services;
using Faces.Database.Repositories;
using Faces.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faces.Authentication.Services
{
    public class ApiAuthenticatedEmployee : IAuthenticatedEmployee
    {
        public bool IsAuthenticated {
            get
            {
                return this.Employee != null;
            }
        }

        public Employee? Employee { get; private set; }

        public ApiAuthenticatedEmployee(IHttpContextAccessor httpContextAccessor, IEmployeeRepository employeeRepository, IJwtService jwtService)
        {
            var token = httpContextAccessor
                .HttpContext?
                .Request
                .Headers["Authorization"]
                .ToString()
                .Split(" ")
                .Last();

            if (!string.IsNullOrEmpty(token))
            {
                Employee = employeeRepository.GetByDocument(jwtService.GetEmployeeDocument(token));
            }
        }
    }
}
