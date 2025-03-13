using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faces.Application.DTOs
{
    public class EmployeeUpdateDto
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string DocNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public string JobFunctionCode { get; set; }

        public int? ManagerID { get; set; }

        public List<PhoneDto> Phones { get; set; }
    }
}
