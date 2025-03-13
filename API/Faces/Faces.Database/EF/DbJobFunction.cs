using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faces.Domain.Entities;

namespace Faces.Database.EF
{
    internal class DbJobFunction
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public short HierarchyLevel { get; set; }
    }

    internal static class JobFunctionConverter
    {
        public static JobFunction ConvertToDomain(this DbJobFunction entity)
        {
            return new JobFunction(entity.Code, entity.Name, entity.HierarchyLevel);
        }

        public static DbJobFunction ConvertToDbModel(this JobFunction domainModel)
        {
            return new DbJobFunction()
            {
                Code = domainModel.Code,
                Name = domainModel.Name,
                HierarchyLevel = domainModel.HierarchyLevel
            };
        }
    }

}
