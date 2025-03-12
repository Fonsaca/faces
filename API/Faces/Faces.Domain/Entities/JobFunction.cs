using System.ComponentModel.DataAnnotations;

namespace Faces.Domain.Entities
{
    public class JobFunction
    {
        public const short HIGH_HIERARCHY = 1000;

        [Required(AllowEmptyStrings = false,ErrorMessage = "Job function code is required")]
        [MaxLength(20,ErrorMessage = "Job function code must not be greater than 20 characters")]
        public string Code { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Job function name is required")]
        [MaxLength(50, ErrorMessage = "Job function code must not be greater than 50 characters")]
        public string Name { get; set; }


        [MinLength(1, ErrorMessage = "Job function hierarchy level must not be zero or negative")]
        [MaxLength(HIGH_HIERARCHY, ErrorMessage = $"Job function hierarchy level must not be greater than 1000")]
        public short HierarchyLevel { get; set; }


        public bool IsHighestHierarchy
        {
            get
            {
                return HierarchyLevel == HIGH_HIERARCHY;
            }
        }

        public JobFunction(string code, string name, short hierarchyLevel)
        {
            Code = code;
            Name = name;
            HierarchyLevel = hierarchyLevel;
        }

    }
}
