using Faces.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Faces.Database.EF
{
    internal class DbEmployee
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string DocNumber { get; set; }

        public DateOnly BirthDate { get; set; }

        public string JobFunctionCode { get; set; }

        public DbJobFunction JobFunction { get; set; }

        public int? ManagerID { get; set; }

        public DbEmployee? Manager { get; set; }

        public List<DbPhone> Phones { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletionDate { get; set; }

        public string PasswordHash { get; set; }

        internal void SetCreated()
        {
            this.IsDeleted = false;
            CreationDate = DateTime.Now;
        }

        internal void SetDeleted()
        {
            this.IsDeleted = true;
            DeletionDate = DateTime.Now;
        }


    }

    internal static class EmployeeConverter
    {
        public static Employee ConvertToDomain(this DbEmployee entity)
        {
            var employee = new Employee()
            {
                ID = entity.ID,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                DocNumber = entity.DocNumber,
                BirthDate = entity.BirthDate,
                JobFunction = entity.JobFunction.ConvertToDomain(),
            };

            employee.Phones.AddRange(entity.Phones.Select(p => p.ConvertToDomain()));

            if (entity.Manager != null)
                employee.Manager = entity.Manager!.ConvertToDomain();

            employee.SetPasswordHash(entity.PasswordHash);

            return employee;
        }

        public static DbEmployee ConvertToDbModel(this Employee domainModel)
        {
            return new DbEmployee()
            {
                ID = domainModel.ID,
                BirthDate = domainModel.BirthDate,
                Email = domainModel.Email,
                DocNumber = domainModel.DocNumber,
                FirstName = domainModel.FirstName,
                LastName = domainModel.LastName,
                JobFunctionCode = domainModel.JobFunction.Code,
                ManagerID = domainModel.Manager?.ID,
                Phones = domainModel.Phones.Select(phone => phone.ConvertToDbModel()).ToList(),
                PasswordHash = domainModel.GetPasswordHash()
            };

        }
    }

}
