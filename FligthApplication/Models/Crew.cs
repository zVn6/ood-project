using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FligthApplication.Models.Interfaces;

namespace FligthApplication.Models
{
    public class Crew : IFTRObject
    {
        public string Identifier => "C";
        public ulong ID { get; set; }
        public string Name { get; set; } = "";
        public ulong Age { get; set; }
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";
        public ushort Practice { get; set; }
        public string Role { get; set; } = "";

    }
}
