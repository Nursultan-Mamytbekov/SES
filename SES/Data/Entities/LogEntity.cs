using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Data.Entities
{
    public class LogEntity
    {
        public int Id { get; set; }
        [Display(Name = "ПИН")]
        public string Pin { get; set; }
        [Display(Name = "Дата запроса")]
        public DateTime Date { get; set; } = DateTime.Now;
        [Display(Name = "Статус")]
        [UIHint("BoolStatus")]
        public bool Status { get; set; }
        [Display(Name = "Сообщение")]
        public string Message { get; set; }
        [Display(Name = "Метод")]
        public MethodEnum Method { get; set; }
    }
}
