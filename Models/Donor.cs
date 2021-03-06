using BloodKoshh.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace BloodKoshh.Models
{
    public class Donor
    {
        [Key]
        public int Donor_id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Enter Email...")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public string BloodGroup { get; set; }
        [Required]
        [Phone]
        public double PhoneNo { get; set; }
        [Required]
        public string  District { get; set; }
        public string Address { get; set; }
        [Required]
        public char Gender { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public string Dob { get; set; }

        [Required]
        public int Gender_Id { get; set; }
        public DateTime Added_Date { get; set; }
        //if has any disease
        public string HealthInfo { get; set; }
        public bool ApprovedStatus { get; set; }
        public bool RequestStatus { get; set; }
        public int Count { get; set; }
        public DateTime LastDonated { get; set; }
        public string UserId { get; set; }
        public BloodKoshhUser bloodKoshhUser { get; set; }
        public int LocationId { get; set; }
        public DonorLocation DonorLocation { get; set; }

    }
}
