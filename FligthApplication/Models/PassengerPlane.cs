using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FligthApplication.Models.Interfaces;

namespace FligthApplication.Models
{
    public class PassengerPlane : IFTRObject
    {
        public ulong ID { get; set; }
        public string Identifier => "PP";
        public string Serial { get; set; } = "";
        public string CountryISO { get; set; } = "";
        public string Model { get; set; } = "";
        public ushort FirstClassSize { get; set; }
        public ushort BusinessClassSize { get; set; }
        public ushort EconomyClassSize { get; set; }

    }
}
