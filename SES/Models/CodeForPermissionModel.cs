using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Models
{
    public class CodeForPermissionModel
    {
        [Display(Name = "ID запроса")]
        public string RequestId { get; set; }
        [Display(Name = "Код из СМС")]
        public string Code { get; set; }

    }
}
