using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmaQueue.Models
{
    public class UserType
    {
        [Key]
        public int UserTypeId { get; set; }

        [Required]
        public string UserRole { get; set; }
    }
}
