using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Models
{
    public class PinRequestModel
    {
        [Display(Name = "ПИН")]
        public string Pin { get; set; }

    }
}
