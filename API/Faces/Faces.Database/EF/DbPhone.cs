using Faces.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Faces.Database.EF
{
    internal class DbPhone
    {
        public int ID { get; set; }

        public string Label { get; set; }

        public string Number { get; set; }

        public int EmployeeID { get; set; }

        public DbEmployee Employee { get; set; }

    }

    internal static class PhoneConverter
    {
        public static Phone ConvertToDomain(this DbPhone entity)
        {
            return new Phone(entity.ID, entity.Label, entity.Number);
        }

        public static DbPhone ConvertToDbModel(this Phone domainModel)
        {
            return new DbPhone()
            {
                ID = domainModel.ID,
                Label = domainModel.Label,
                Number = domainModel.Number
            };
        }
    }

}
