using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BloodKoshh.Models
{
    public class EventViewModel
    {
        public int EventId { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        [Required]
        public string EventName { get; set; }
        [Required]
        [Phone]
        public double PhoneNo { get; set; }
        public List<EventViewModel> EventList { get; set; }
        public EventViewModel()
        {
            EventList = new List<EventViewModel>();
        }
    }
}
