using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Areas.Admin.Models
{
    public class CreateUserViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Текущий пароль")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите новый пароль")]
        [Compare("NewPassword", ErrorMessage = "Новый пароль не совпадает с подтверждением")]
        public string ConfirmPassword { get; set; }
    }
}
