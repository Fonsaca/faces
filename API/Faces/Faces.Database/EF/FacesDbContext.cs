using Faces.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Faces.Database.EF
{
    internal class FacesDbContext : DbContext
    {

        public DbSet<DbJobFunction> JobFunctions { get; set; }
        public DbSet<DbPhone> Phones { get; set; }
        public DbSet<DbEmployee> Employees { get; set; }

        public FacesDbContext(DbContextOptions<FacesDbContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DbEmployeeMap());
            modelBuilder.ApplyConfiguration(new DbJobFunctionMap());
            modelBuilder.ApplyConfiguration(new DbPhoneMap());

            //For the test purpose only
            modelBuilder.Entity<DbJobFunction>()
                .HasData(
                    new JobFunction("0001", "Analyst", 10),
                    new JobFunction("0002", "CEO", JobFunction.HIGH_HIERARCHY),
                    new JobFunction("0003", "Tech Leader", 100));
        }
    }
}
