using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FligthApplication.Models.Interfaces;

namespace FligthApplication.Models
{
    public class Passenger : IFTRObject
    {
        public ulong ID { get; set; }
        public string Identifier => "P";
        public string Name { get; set; } = "";
        public ulong Age { get; set; }
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";
        public string Class { get; set; } = "";
        public ulong Miles { get; set; }
    }
}
