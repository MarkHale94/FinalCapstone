using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmaQueue.Models.PrescriptionViewModels
{
    public class PrescriptionCreateViewModel
    {
        public string UserId { get; set; }
        public Prescription Prescription { get; set; }
        public Status Status { get; set; }
    }
}
