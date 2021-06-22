using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Data.Entities
{
    public class LogEntity
    {
        public int Id { get; set; }
        public string Pin { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public bool Status { get; set; }
        public string Message { get; set; }
        public MethodEnum Method { get; set; }
    }
}
