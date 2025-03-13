using System.ComponentModel.DataAnnotations;

namespace Faces.Domain.Entities
{
    public class Phone
    {

        public int ID { get; set; }

        [Required(ErrorMessage="Phone label is required")]
        public string Label { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        public string Number { get; set; } //TODO validate with google/libphonenumber 

        public Phone(int id, string label, string number)
        {
            ID = id;
            Label = label;
            Number = number;
        }
    }


}
