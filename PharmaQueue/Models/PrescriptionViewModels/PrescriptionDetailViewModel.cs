using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmaQueue.Models.PrescriptionViewModels
{
    public class PrescriptionDetailViewModel
    {
        public int CurrentUserTypeId { get; set; }
        public Prescription Prescription { get; set; }
    }
}
