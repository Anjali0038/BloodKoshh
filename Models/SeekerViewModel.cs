using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BloodKoshh.Models
{
    public class SeekerViewModel
    {
        [Key]
        public int Seeker_Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Phone]
        public double PhoneNo { get; set; }
        [Required]
        public string BloodGroup { get; set; }
        [Required]
        public string RequestReason { get; set; }
        [Required(ErrorMessage = "Please Enter Email...")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public bool Requeststatus { get; set; }
        public string UserId { get; set; }
        public List<SeekerViewModel> SeekerList { get; set; }
        public SeekerViewModel()
        {
            SeekerList = new List<SeekerViewModel>();
        }

    }
}
