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
        [Required(ErrorMessage = "Please Enter Username..")]
        [Display(Name = "UserName")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please Enter Password...")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Pwd { get; set; }

        [Required(ErrorMessage = "Please Enter the Confirm Password...")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Pwd")]
        public string Confirmpwd { get; set; }

        [Required(ErrorMessage = "Please Enter Email...")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        public string BloodGroup { get; set; }
        [Required]
        [Phone]
        public double PhoneNo { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public char Gender { get; set; }
        public DateTime Added_Date { get; set; }
        //reason to search blood or case
        [Required]
        public string RequestReason { get; set; }
        //description of situation
        public string Message { get; set; }
        public List<SeekerViewModel> SeekerList { get; set; }
        public SeekerViewModel()
        {
            SeekerList = new List<SeekerViewModel>();
        }

    }
}
