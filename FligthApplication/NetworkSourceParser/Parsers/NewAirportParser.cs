using FligthApplication.Models;
using FligthApplication.Models.Interfaces;
using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FligthApplication.NetworkSourceParser.Parsers
{
    public class NewAirportParser : INSParser
    {
        public IFTRObject? ParseFTRObject(Message message)
        {
            byte[] byteMessage = message.MessageBytes;
            int byteMessageLen = byteMessage.Length;

            if (byteMessageLen < 35)
            {
                return null;
            }

            string identifier = Encoding.ASCII.GetString(byteMessage, 0, 3);

            if( identifier != "NAI")
            {
                return null;
            }

            UInt32 followingMsgLen  = BitConverter.ToUInt32(byteMessage, 3);

            if(followingMsgLen + 7 != byteMessageLen) 
            {
                return null;
            }

            UInt64 id = BitConverter.ToUInt64(byteMessage, 7);
            UInt16 nameLen = BitConverter.ToUInt16(byteMessage, 15);
            string name = Encoding.ASCII.GetString(byteMessage, 17, nameLen);
            string code = Encoding.ASCII.GetString(byteMessage, 17 + nameLen,3);
            Single longitude = BitConverter.ToSingle(byteMessage, 20 + nameLen);
            Single latitude = BitConverter.ToSingle(byteMessage, 24 + nameLen);
            Single amsl = BitConverter.ToSingle(byteMessage, 28 + nameLen);
            string iso = Encoding.ASCII.GetString(byteMessage, 32 + nameLen,3);

            return new Airport
            {
                ID = id,
                Name = name,
                Code = code,
                Longitude = longitude,
                Latitude = latitude,
                AMSL = amsl,
                CountryISO = iso,
            };
        }
    }
}
