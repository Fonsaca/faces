using Faces.Application.Services;
using Faces.Domain.Entities;

namespace Faces.Api
{
    public class ApiAuthenticatedEmployee : IAuthenticatedEmployee
    {
        public bool IsAuthenticated { get; private set; }

        public Employee? Employee { get; private set; }

        public ApiAuthenticatedEmployee(IEmployeeService employeeService)
        {
            //TODO get from header jwt
            IsAuthenticated = true;

            Employee = employeeService.GetById(1);
        }
    }
}
