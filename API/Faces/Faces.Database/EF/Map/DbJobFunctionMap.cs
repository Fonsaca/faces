using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Faces.Database.EF.Map
{
    internal class DbJobFunctionMap : IEntityTypeConfiguration<DbJobFunction>
    {
        public void Configure(EntityTypeBuilder<DbJobFunction> builder)
        {
            builder.HasKey(c => c.Code);

            builder.Property(c => c.Code)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.HierarchyLevel)
                .IsRequired();
        }
    }

}
