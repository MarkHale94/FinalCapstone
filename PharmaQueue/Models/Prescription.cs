using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PharmaQueue.Models
{
    public class Prescription
    {
        [Key]
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
        public double Price { get; set; }

        [Required]
        public bool IsSold { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int StatusId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateCreated { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        public virtual Status Status { get; set; }
    }
}
