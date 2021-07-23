using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SES.Models
{
    public class BankPinRequestModel
    {
        [Required]
        [Display(Name = "ПИН")]
        public string Pin { get; set; }
        [Required]
        [Display(Name = "Серия паспорта")]
        public string Series { get; set; }
        [Required]
        [Display(Name = "Номер паспорта")]
        public string Number { get; set; }
    }
}
