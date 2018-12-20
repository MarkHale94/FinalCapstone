using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmaQueue.Models.HomeViewModel
{
    public class HomeIndexViewModel
    {
        public int UserTypeId { get; set; }
        public string UserId { get; set; }
        public ICollection<Prescription> EnteredPrescriptions { get; set; }
        public ICollection<Prescription> ReviewedPrescriptions { get; set; }
        public ICollection<Prescription> FilledPrescriptions { get; set; }
        public ICollection<Prescription> ReadyPrescriptions { get; set; }
        public ICollection<Prescription> SoldPrescriptions { get; set; }
    }
}
