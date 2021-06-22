using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Data.Entities.Abstract
{
    public abstract class BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public DateTime RequestDate { get; set; } = DateTime.Now;
        [Required]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
