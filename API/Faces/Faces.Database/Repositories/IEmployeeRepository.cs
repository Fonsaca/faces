using Faces.Database.EF;
using Faces.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faces.Database.Repositories
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAll();

        Employee? GetById(int id);

        Employee? GetByDocument(string document);

        void Create(Employee employee);

        void Update(Employee employee);

        void Delete(int id);
    }

    internal class EmployeeRepository : IEmployeeRepository
    {

        private readonly FacesDbContext _context;

        public EmployeeRepository(FacesDbContext context)
        {
            _context = context;
        }


        public List<Employee> GetAll()
        {
            return _context.Employees
                .Include(x => x.Phones)
                .Include(x => x.JobFunction)
                .Include(x => x.Manager)
                .Where(x => !x.IsDeleted)
                .Select(x => x.ConvertToDomain())
                .ToList();
        }

        public Employee? GetById(int id)
        {
            return _context.Employees
                .Include(x => x.Phones)
                .Include(x => x.JobFunction)
                .Include(x => x.Manager)
                .Where(x => x.ID == id && !x.IsDeleted)
                .Select(x => x.ConvertToDomain())
                .FirstOrDefault();
        }

        public void Create(Employee employee)
        {
            var dbEmployee = employee.ConvertToDbModel();
            dbEmployee.SetCreated();
            _context.Employees.Add(dbEmployee);
            _context.SaveChanges();
        }
        public void Update(Employee employee)
        {
            var dbEmployee = _context.Employees
                .Include(x => x.Phones)
                .FirstOrDefault(x => x.ID == employee.ID);

            if (dbEmployee == null || dbEmployee.IsDeleted)
                throw new KeyNotFoundException("Employee not found");

            var dbUpdateEmployee = employee.ConvertToDbModel();
            dbUpdateEmployee.CreationDate = dbEmployee.CreationDate;
            dbUpdateEmployee.IsDeleted = dbEmployee.IsDeleted;
            dbUpdateEmployee.PasswordHash = dbEmployee.PasswordHash;

            var phones = dbUpdateEmployee.Phones;

            dbUpdateEmployee.Phones = null;

            var deletePhones = dbEmployee.Phones
                .Where(p => phones.TrueForAll(x => x.ID != p.ID))
                .ToList();


            _context.Entry(dbEmployee).State = EntityState.Detached;
            _context.Attach(dbUpdateEmployee).State = EntityState.Modified;

            foreach (var phone in deletePhones)
            {
                phone.Employee = null;
                _context.Attach(phone).State = EntityState.Deleted;
            }

            var addPhones = phones
               .Where(p => p.ID == 0)
               .ToList();

            foreach (var phone in addPhones)
            {
                phone.EmployeeID = dbUpdateEmployee.ID;
                _context.Phones.Add(phone);
            }


            _context.SaveChanges();

        }

        public void Delete(int id)
        {
            var employee = _context.Employees
                .FirstOrDefault(x => x.ID == id && !x.IsDeleted);

            if (employee == null)
                throw new KeyNotFoundException("Employee not found");

            employee.SetDeleted();

            _context.Entry(employee).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Employee? GetByDocument(string document)
        {
            return _context.Employees
               .Include(x => x.Phones)
               .Include(x => x.JobFunction)
               .Include(x => x.Manager)
               .FirstOrDefault(x => x.DocNumber == document && !x.IsDeleted)
               ?.ConvertToDomain();
        }
    }
}
