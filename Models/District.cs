using System;
using System.Collections.Generic;

#nullable disable

namespace BloodKoshh.Models
{
    public class District
    {
        public int Id { get; set; }
        public string Districts { get; set; }
        public byte Municipality { get; set; }
        public short TotalLocalBodies { get; set; }
    }
}
