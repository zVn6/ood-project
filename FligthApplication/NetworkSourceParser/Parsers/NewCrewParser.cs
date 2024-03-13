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
    public class NewCrewParser : INSParser
    {
        public IFTRObject? ParseFTRObject(Message message)
        {
            byte[] byteMessage = message.MessageBytes;
            int byteMessageLen = byteMessage.Length;

            if (byteMessageLen < 36)
            {
                return null;
            }

            string identifier = Encoding.ASCII.GetString(byteMessage, 0, 3);

            if (identifier != "NCR")
            {
                return null;
            }

            UInt32 followingMsgLen = BitConverter.ToUInt32(byteMessage, 3);

            if (followingMsgLen + 7 != byteMessageLen)
            {
                return null;
            }

            UInt64 id = BitConverter.ToUInt64(byteMessage, 7);
            UInt16 nameLen = BitConverter.ToUInt16(byteMessage, 15);
            string name = Encoding.ASCII.GetString(byteMessage, 17, nameLen);
            UInt16 age = BitConverter.ToUInt16(byteMessage, 17 + nameLen);
            string phone = Encoding.ASCII.GetString(byteMessage, 19 + nameLen, 12);
            UInt16 emailLen = BitConverter.ToUInt16(byteMessage,31 + nameLen);
            string email = Encoding.ASCII.GetString(byteMessage, 33 + nameLen, emailLen);
            UInt16 practice = BitConverter.ToUInt16(byteMessage, 33 + emailLen + nameLen);
            string role = Encoding.ASCII.GetString(byteMessage, 35 + nameLen + emailLen, 1);

            return new Crew
            {
                ID = id,
                Name = name,
                Age = age,
                Phone = phone,
                Email = email,
                Practice = practice,
                Role = role
            };
        }
    }
}
