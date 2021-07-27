using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SES.Models
{
    public enum FamilyStatus
    {
        [Display(Name = "Состоит в браке")]
        Married,
        [Display(Name = "В браке не состоял(а)")]
        Single,
        [Display(Name = "Разведен(а)")]
        Divorced,
        [Display(Name = "Вдовец/вдова")]
        Widow
    }
}
