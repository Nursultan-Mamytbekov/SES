using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Models
{
    public class RequestPermissionModel
    {
        [Display(Name = "ПИН")]
        public string Pin { get; set; }
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }
        
        public string OrganizationId { get; set; }
        [Display(Name = "Дата окончания выдачи разрешения")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Дата рождения")]
        public DateTime BirthDate { get; set; }
        [Display(Name = "Адрес прописки")]
        public string PassportAddress { get; set; }
        [Display(Name = "Фактический адрес")]
        public string FactAddress { get; set; }
        [Display(Name = "Серия и номер паспорта")]
        public string PassportNumber { get; set; }
        [Display(Name = "Дата окончания действия паспорта")]
        public DateTime PassportIssuedDate { get; set; }
        [Display(Name = "Место выдачи паспорта")]
        public string PassportIssuedBy { get; set; }
        [Display(Name = "Электронный адрес")]
        public string Email { get; set; }

    }

}
