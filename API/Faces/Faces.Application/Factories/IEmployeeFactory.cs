using Faces.Application.DTOs;
using Faces.Database.Repositories;
using Faces.Domain.Entities;


namespace Faces.Application.Factories
{
    internal interface IEmployeeFactory
    {

        Employee CreateFrom(EmployeeUpdateDto employeeUpdateDto);

    }

    internal class EmployeeFactory : IEmployeeFactory
    {

        private readonly IJobFunctionRepository _jobFunctionRepository;

        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeFactory(IJobFunctionRepository jobFunctionRepository,
            IEmployeeRepository employeeRepository)
        {
            _jobFunctionRepository = jobFunctionRepository;
            _employeeRepository = employeeRepository;
        }


        public Employee CreateFrom(EmployeeUpdateDto employeeUpdateDto)
        {
            var employee = new Employee()
            {
                ID = employeeUpdateDto.ID,
                FirstName = employeeUpdateDto.FirstName,
                LastName = employeeUpdateDto.LastName,
                DocNumber = employeeUpdateDto.DocNumber,
                BirthDate = employeeUpdateDto.BirthDate,
                Email = employeeUpdateDto.Email,
                JobFunction = _jobFunctionRepository.GetByCode(employeeUpdateDto.JobFunctionCode),
            };

            if (employeeUpdateDto.ManagerID.HasValue)
                employee.Manager = _employeeRepository.GetById(employeeUpdateDto.ManagerID.Value);


            foreach (var phone in employeeUpdateDto.Phones)
            {
                employee.Phones.Add(new Phone(phone.Label, phone.Number));
            }


            return employee;
        }
    }
}
