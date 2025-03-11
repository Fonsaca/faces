using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faces.Domain.Entities
{
    public class Employee
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public string Email { get; set; }

        public string DocNumber { get; set; }

        public DateOnly BirthDate { get; set; }

        public int Age { get; } = 0;

        public JobFunction JobFunction { get; set; }

        public List<Phone> Phones { get; set; }

        public Employee? Manager { get; set; }
    }
}
