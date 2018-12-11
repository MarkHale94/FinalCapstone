using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmaQueue.Models
{
    public class Prescription
    {
        [Required]
        public int PrescriptionId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Strength { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int Refills { get; set; }

        [Required]
        public double price { get; set; }

        [Required]
        public bool isSold { get; set; }

        [Required]
        public int UserId { get; set; }

    }
}
