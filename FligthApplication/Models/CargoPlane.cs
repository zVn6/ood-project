using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FligthApplication.Models.Interfaces;

namespace FligthApplication.Models
{
    public class CargoPlane : IFTRObject
    {
        public ulong ID { get; set; }

        public string Identifier => "CP";
        public string Serial { get; set; } = "";        
        public string CountryISO { get; set; } = "";
        public string Model { get; set; } = "";     
        public float MaxLoad { get; set; }
    }
}
