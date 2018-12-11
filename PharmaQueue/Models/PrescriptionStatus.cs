using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmaQueue.Models
{
    public class PrescriptionStatus
    {
        [Required]
        public int PrescriptionStatusId { get; set; }

        [Required]
        public string PrescriptionId { get; set; }

        public Prescription Prescription { get; set; }

        [Required]
        public int StatusId { get; set; }

        public Status Status { get; set; }
    }
}
