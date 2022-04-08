using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodKoshh.Models
{
    public class UserViewModel
    {
        public Donor Donor { get; set; }
        public int DonorId { get; set; }
        public DonorLocation DonorLocation { get; set; }
        public int LocationId { get; set; }
        public DonorViewModel donormodel { get; set; }
    }
}
