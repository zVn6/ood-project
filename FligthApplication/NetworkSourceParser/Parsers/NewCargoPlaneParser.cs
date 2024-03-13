using FligthApplication.Models.Interfaces;
using FligthApplication.Models;
using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FligthApplication.NetworkSourceParser.Parsers
{
    public class NewCargoPlaneParser : INSParser
    {
        public IFTRObject? ParseFTRObject(Message message)
        {
            byte[] byteMessage = message.MessageBytes;
            int byteMessageLen = byteMessage.Length;

            if (byteMessageLen < 34)
            {
                return null;
            }

            string identifier = Encoding.ASCII.GetString(byteMessage, 0, 3);

            if (identifier != "NCP")
            {
                return null;
            }

            UInt32 followingMsgLen = BitConverter.ToUInt32(byteMessage, 3);

            if (followingMsgLen + 7 != byteMessageLen)
            {
                return null;
            }

            UInt64 id = BitConverter.ToUInt64(byteMessage, 7);
            string serial = Encoding.ASCII.GetString(byteMessage, 15, 10);
            string iso = Encoding.ASCII.GetString(byteMessage, 25, 3);
            UInt16 modelLen = BitConverter.ToUInt16(byteMessage, 28);
            string model = Encoding.ASCII.GetString(byteMessage, 30, modelLen);
            Single maxLoad = BitConverter.ToSingle(byteMessage, 30 + modelLen);

            return new CargoPlane
            {
                ID = id,
                Serial = serial,
                CountryISO = iso,
                Model = model,
                MaxLoad = maxLoad
            };
        }
    }
}
