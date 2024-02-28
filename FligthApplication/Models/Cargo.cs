using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FligthApplication.Models.Interfaces;

namespace FligthApplication.Models
{
    public class Cargo : IFTRObject
    {
        public ulong ID { get; set; }

        public string Identifier => "CA";
        public float Weight { get; set; }
        public string Code { get; set; } = "";
        public string Description { get; set; } = "";
    }
}
