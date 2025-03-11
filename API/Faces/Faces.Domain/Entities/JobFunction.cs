using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faces.Domain.Entities
{
    public class JobFunction
    {
        public const short HIGH_HIERARCHY = 10000;


        public string Code { get; set; }

        public string Name { get; set; }

        public short HierarchyLevel { get; set; }

        public bool IsHighestHierarchy
        {
            get
            {
                return HierarchyLevel == HIGH_HIERARCHY;
            }
        }
    }
}
