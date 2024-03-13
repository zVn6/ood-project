using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FligthApplication.Models.Interfaces
{
    public interface IFTRObject
    {
        ulong ID { get; set; }
        string Identifier { get; }
    }
}
