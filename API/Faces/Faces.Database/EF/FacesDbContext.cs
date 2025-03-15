using Faces.Database.EF.Map;
using Faces.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Faces.Database.EF
{
    internal class FacesDbContext : DbContext
    {

        public DbSet<DbJobFunction> JobFunctions { get; set; }
        public DbSet<DbPhone> Phones { get; set; }
        public DbSet<DbEmployee> Employees { get; set; }

        public FacesDbContext(DbContextOptions<FacesDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DbEmployeeMap());
            modelBuilder.ApplyConfiguration(new DbJobFunctionMap());
            modelBuilder.ApplyConfiguration(new DbPhoneMap());



            var rhFunction = new JobFunction("0001", "RH Manager", JobFunction.HIGH_HIERARCHY);

            var functions = new List<DbJobFunction> {
                        rhFunction.ConvertToDbModel(),
                        new JobFunction("0002", "Tech Leader", 1000).ConvertToDbModel(),
                        new JobFunction("0003", "Analyst", 100).ConvertToDbModel(),
                        new JobFunction("0004", "Intern", 1).ConvertToDbModel()
                    };

            var adminEmployee = new Employee()
            {
                FirstName = "admin",
                LastName = "Rh",
                Email = "admin.rh@faces.com",
                BirthDate = DateOnly.Parse("1990-01-01"),
                DocNumber = "99999999999",
                JobFunction = rhFunction
            };

            adminEmployee.SetPasswordHash("i+rcXhRFTdUTkNVTJ07ydQ==.HXcBql16eQsd/uam7bZdKvPhAIAotA8kbwx7FBsrRBc=");
            var dbAdminEmployee = adminEmployee.ConvertToDbModel();
            dbAdminEmployee.ID = -1;
            dbAdminEmployee.SetCreated();

            dbAdminEmployee.CreationDate = new DateTime(
                dbAdminEmployee.CreationDate.Year,
                dbAdminEmployee.CreationDate.Month,
                dbAdminEmployee.CreationDate.Day,
                dbAdminEmployee.CreationDate.Hour,
                dbAdminEmployee.CreationDate.Minute,
                0);

            modelBuilder.Entity<DbJobFunction>()
                .HasData(functions);

            modelBuilder.Entity<DbEmployee>()
                .HasData(dbAdminEmployee);
        }
    }
}
