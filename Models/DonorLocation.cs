using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BloodKoshh.Models
{
    public class DonorLocation
    {
        [Key]
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
