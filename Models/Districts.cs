using System;
using System.Collections.Generic;

#nullable disable

namespace BloodKoshh.Models
{
    public class Districts
    {
        public int Id { get; set; }
        public string District { get; set; }
        public byte Municipality { get; set; }
        public short Total_Local_Bodies { get; set; }
    }
}
