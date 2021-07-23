using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Data.Entities
{
    public enum MethodEnum
    {
        [Display(Name = "Сведения о пенсии")]
        GetPensionInfo,
        [Display(Name = "Сведения о зар. плате")]
        GetWorkPeriodInfo,
        [Display(Name = "Сведения из ГРС")]
        GetBankPinInfo,
        [Display(Name = "Запрос разрешения")]
        InitialyzeRequestForPermission,
        [Display(Name = "Отправка кода на разрешение")]
        SendCodeForPermission
    }
}
