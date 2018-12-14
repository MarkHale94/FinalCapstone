using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmaQueue.Models.PrescriptionViewModels
{
    public class PrescriptionSoldViewModel
    {
        public int CurrentUserTypeId { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
    }
}
