using Faces.Application.Services;
using Faces.Database.Repositories;
using Faces.Domain.Entities;

namespace Faces.Api
{
    public class ApiAuthenticatedEmployee : IAuthenticatedEmployee
    {
        public bool IsAuthenticated { get; private set; }

        public Employee? Employee { get; private set; }

        public ApiAuthenticatedEmployee(IEmployeeRepository employeeRepository)
        {
            //TODO get from header jwt
            IsAuthenticated = true;

            Employee = employeeRepository.GetById(4);
        }
    }
}
