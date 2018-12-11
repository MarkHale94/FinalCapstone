using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmaQueue.Models
{
    public class Status
    {
        [Required]
        public int StatusId { get; set; }

        [Required]
        public string QueueStatus { get; set; }
    }
}
