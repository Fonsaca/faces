using Faces.Application.DTOs;
using Faces.Application.Factories;
using Faces.Database.Repositories;
using Faces.Domain.Entities;

namespace Faces.Application.Services
{
    public interface IEmployeeService
    {

        List<Employee> GetAll(); //TODO pagination

        Employee? GetByDocument(string document);

        void Create(EmployeeUpdateDto employeeDto);
        
        void Update(EmployeeUpdateDto employeeDto);

        void Delete(int id);
    
    }

    internal class EmployeeService : IEmployeeService
    {
        private readonly IAuthenticatedEmployee _authenticatedEmployee;

        private readonly IEmployeeFactory _employeeFactory;

        private readonly IEmployeeRepository _employeeRepository;



        public EmployeeService(IAuthenticatedEmployee authenticatedEmployee,
            IEmployeeFactory employeeFactory, IEmployeeRepository employeeRepository)
        {
            _authenticatedEmployee = authenticatedEmployee;
            _employeeFactory = employeeFactory;
            _employeeRepository = employeeRepository;
        }

        //todo unit tests
        public void Create(EmployeeUpdateDto employeeDto)
        {
            var employee = _employeeFactory.CreateFrom(employeeDto);
            employee.SetPassword(employeeDto.Password);

            var authEmployeeCreating = _authenticatedEmployee.Employee!;

            employee.ValidateToCreateOrUpdate(authEmployeeCreating);

            _employeeRepository.Create(employee);
        }

        public void Update(EmployeeUpdateDto employeeDto)
        {
            var employee = _employeeFactory.CreateFrom(employeeDto);

            var authEmployeeCreating = _authenticatedEmployee.Employee!;

            employee.ValidateToCreateOrUpdate(authEmployeeCreating);

            _employeeRepository.Update(employee);
        }

        public void Delete(int id) => _employeeRepository.Delete(id);

        public List<Employee> GetAll() => _employeeRepository.GetAll();

        public Employee? GetByDocument(string document) => _employeeRepository.GetByDocument(document);

       
    }
}
