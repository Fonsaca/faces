using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faces.Domain.Entities
{
    public class Phone
    {
        [Required(ErrorMessage="Phone label is required")]
        public string Label { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        public string Number { get; set; } //TODO validate with google/libphonenumber 

        public Phone(string label, string number)
        {
            Label = label;
            Number = number;
        }
    }
}
