using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Faces.Database.EF.Map
{
    internal class DbPhoneMap : IEntityTypeConfiguration<DbPhone>
    {
        public void Configure(EntityTypeBuilder<DbPhone> builder)
        {
            builder.HasKey(c => c.ID);

            builder.Property(c => c.Label)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Number)
                .IsRequired()
                .HasMaxLength(30);

            builder.HasOne(x => x.Employee)
                .WithMany(e => e.Phones)
                .HasForeignKey(e => e.EmployeeID);
        }
    }

}
