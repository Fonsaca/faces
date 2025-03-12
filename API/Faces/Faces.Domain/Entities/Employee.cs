using Faces.Domain.Util;
using System.ComponentModel.DataAnnotations;

namespace Faces.Domain.Entities
{
    public class Employee
    {

        public const int MIN_BIRTH_YEAR = 1925;

        public const int AGE_OF_MAJORITY = 18;


        private int _id;

        [MinLength(1,ErrorMessage = "Employee ID can't be a negative number")]
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                if(_id != 0)
                {
                    throw new InvalidOperationException("ID already set");
                }
                _id = value;
            }
        }

        [Required(ErrorMessage = "Employee first name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Employee last name is required")]
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        [Required(ErrorMessage = "Employee email is required")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "Employee document number is required")]
        public string DocNumber { get; set; }


        private DateOnly _birthDate;
        public DateOnly BirthDate
        {
            get
            {
                return _birthDate;
            }
            set
            {
                if (value.Year < MIN_BIRTH_YEAR)
                    throw new ArgumentException($"Birth date before year {MIN_BIRTH_YEAR} is not allowed", "Birth Date");

                if (value.ToDateTime(TimeOnly.MinValue) > DateTime.Today)
                    throw new ArgumentException("Future birth date is not correct", "Birth Date");

                _birthDate = value;
            }
        }

        public int Age
        {
            get
            {
                int yearsOld = DateTime.Today.Year - BirthDate.Year;
                if (DateTime.Today.DayOfYear < BirthDate.DayOfYear)
                    yearsOld--;

                return yearsOld;
            }
        }

        public bool IsAgeOfMajority
        {
            get
            {
                return Age >= AGE_OF_MAJORITY;
            }
        }

        [Required(ErrorMessage = "Job function is required")]
        public JobFunction JobFunction { get; set; }

        public Employee? Manager { get; set; }

        public ICollection<Phone> Phones { get; } = new List<Phone>();




        public (bool isValid, string message) HasRequiredFields()
        {
            var validations = new Dictionary<string, bool>
            {
                { "first name", Validations.IsDefault(this, x => x.FirstName) },
                { "last name", Validations.IsDefault(this, x => x.LastName) },
                { "email", Validations.IsDefault(this, x => x.Email) },
                { "document number", Validations.IsDefault(this, x => x.DocNumber) },
                { "birth date", Validations.IsDefault(this, x => x.BirthDate) },
                { "job function", Validations.IsDefault(this, x => x.JobFunction) },
            };

            if (validations.All(v => !v.Value))
                return (true, "All required fields are set");

            var failedValidations = validations.Where(v => v.Value);

            string fieldsFailed = string.Join(", ", failedValidations.Select(v => v.Key));

            return (false, $"The following fields are missing: {fieldsFailed}");
        }


        public (bool isValid, string message) HasManagerOrIsHighestJobFunction()
        {

            if (Manager != null && JobFunction.IsHighestHierarchy)
                return (false, "The highest job function can not have a Manager");

            if(Manager == null && !JobFunction.IsHighestHierarchy)
                return (false, "A manager is required");

            return (true, "Manager and Job function is correctly set");
        }


        public bool HasGreaterHierarchyJobFunction(Employee employeeToCreate)
        {
            return this.JobFunction.HierarchyLevel > employeeToCreate.JobFunction.HierarchyLevel;
        }

        public void ValidateToCreateOrUpdate(Employee authEmployeeCreating)
        {
            if (authEmployeeCreating.HasGreaterHierarchyJobFunction(this))
                throw new UnauthorizedAccessException($"You can't create an employee with high priveleges");

            bool isValid;
            string message;

            (isValid, message) = HasRequiredFields();

            if (!isValid)
                throw new MissingFieldException(message);


            (isValid, message) = HasManagerOrIsHighestJobFunction();

            if(!isValid)
                throw new MissingFieldException(message);
        }

    }
}
