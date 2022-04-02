using BloodKoshh.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodKoshh.Models
{
    public class Watchlist
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public string UserId { get; set; }
        public BloodKoshhUser BloodKoshhUser { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
