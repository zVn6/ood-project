using FligthApplication.Models.Interfaces;
using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FligthApplication.NetworkSourceParser.Parsers
{
    public class NSParserBuilder
    {
        public static IFTRObject? ParseIFTRObject(Message message)
        {
            string identifier = Encoding.ASCII.GetString(message.MessageBytes, 0, 3);
            INSParser factory = NSFactory.GetParser(identifier);
            return factory.ParseFTRObject(message);
        }
    }
}
