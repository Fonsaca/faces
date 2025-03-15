using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Faces.Database.EF.Map
{
    internal class DbEmployeeMap : IEntityTypeConfiguration<DbEmployee>
    {
        public void Configure(EntityTypeBuilder<DbEmployee> builder)
        {
            builder.HasKey(c => c.ID);


            builder.Property(u => u.ID)
                .ValueGeneratedOnAdd();


            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.DocNumber)
                .IsRequired()
                .HasMaxLength(20);


            builder.HasIndex(c => c.DocNumber)
                .IsUnique();

            builder.Property(c => c.BirthDate)
                .IsRequired()
                .HasConversion(
                    v => v.ToString("yyyy-MM-dd"),
                    v => DateOnly.Parse(v)
                );

            builder.Property(c => c.CreationDate)
                .IsRequired()
                .HasConversion(
                    v => v.ToUniversalTime(),
                    v => v.ToLocalTime()
                );

            builder.Property(c => c.DeletionDate)
                .HasConversion(
                    v => v.HasValue ? (DateTime?)v.Value.ToUniversalTime() : (DateTime?)null,
                    v => v.HasValue ? (DateTime?)v.Value.ToLocalTime() : (DateTime?)null
                );


            builder.HasOne(x => x.Manager).WithMany().HasForeignKey(x => x.ManagerID);

            builder.HasOne(x => x.JobFunction).WithMany().HasForeignKey(x => x.JobFunctionCode);
        }
    }

}
