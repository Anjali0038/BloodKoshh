using BloodKoshh.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BloodKoshh.Models
{
    public class NotificationUser
    {
        [Key]
            public int NotificationId { get; set; }
            public Notification Notification { get; set; }
            public string UserId { get; set; }
            public BloodKoshhUser BloodKoshhUser { get; set; }
            public bool IsRead { get; set; } = false;
    }
}
