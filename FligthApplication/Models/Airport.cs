using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FligthApplication.Models.Interfaces;

namespace FligthApplication.Models
{
    public class Airport : IFTRObject
    {
        public ulong ID { get; set; }

        public string Identifier => "AI";
        public string Name { get; set; } = "";
        public string Code { get; set; } = "";
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public float AMSL { get; set; }
        public string CountryISO { get; set; } = "";
    }
}
