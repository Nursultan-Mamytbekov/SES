using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Data.Entities
{
    public enum PensionState
    {
        [Display(Name = "Не пенсионер")]
        NotRetired,
        [Display(Name = "Пенсионер")]
        Retired,
        [Display(Name = "Умер")]
        Died,
        [Display(Name = "Недействующий ПИН")]
        InactivePin,
        [Display(Name = "ПИН не найден")]
        NotFoundPin
    }
}
