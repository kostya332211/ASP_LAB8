using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LabCore.Models
{
    public class PersonValidationModel
    {
        [DisplayName("Person Name")]
        [Required]
        public string PersonName { get; set; }
        [DisplayName("Person Number")]
        [Required]
        public string PhoneNumber { get; set; }

    }
}
