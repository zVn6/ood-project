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
    public class NewCargoParser : INSParser
    {
        public IFTRObject? ParseFTRObject(Message message)
        {
            byte[] byteMessage = message.MessageBytes;
            int byteMessageLen = byteMessage.Length;

            if (byteMessageLen < 27)
            {
                return null;
            }

            string identifier = Encoding.ASCII.GetString(byteMessage, 0, 3);

            if (identifier != "NCA")
            {
                return null;
            }

            UInt32 followingMsgLen = BitConverter.ToUInt32(byteMessage, 3);

            if (followingMsgLen + 7 != byteMessageLen)
            {
                return null;
            }

            UInt64 id = BitConverter.ToUInt64(byteMessage, 7);
            Single weight = BitConverter.ToSingle(byteMessage, 15);
            string code = Encoding.ASCII.GetString(byteMessage, 19, 6);
            UInt16 descLen = BitConverter.ToUInt16(byteMessage, 25);
            string desc = Encoding.ASCII.GetString(byteMessage, 27, descLen);

            return new Cargo
            {
                ID = id,
                Weight = weight,
                Code = code,
                Description = desc
            };
        }
    }
}
