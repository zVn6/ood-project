using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FligthApplication.Models.Interfaces;

namespace FligthApplication.Models
{
    public class Flight : IFTRObject
    {
        public ulong ID { get; set; }
        public string Identifier => "FL";
        public ulong OriginAsID { get; set; }
        public ulong TargetAsID { get; set; }
        public string TakeoffTime { get; set; } = "";
        public string LandingTime { get; set; } = "";
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public float AMSL { get; set; }
        public ulong PlaneID { get; set; }
        public ulong[]? CrewIDs { get; set; } 
        public ulong[]? Load { get; set; }
    }
}
