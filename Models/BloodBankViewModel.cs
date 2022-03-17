using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodKoshh.Models
{
    public class BloodBankViewModel
    {
        public int Id { get; set; }
        public string BloodBankName { get; set; }
        public string Location { get; set; }
        public double Phone_No { get; set; }
        public bool ApprovedStatus { get; set; }
        public bool RequestStatus { get; set; }

        public List<BloodBankViewModel> BankList { get; set; }
        public BloodBankViewModel()
        {
            BankList = new List<BloodBankViewModel>();
        }
    }
}
