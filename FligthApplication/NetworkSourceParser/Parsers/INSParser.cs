using FligthApplication.Models.Interfaces;
using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FligthApplication.NetworkSourceParser.Parsers
{
    public interface INSParser
    {
        IFTRObject? ParseFTRObject(Message message);
    }
}
