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
    public class NewPassengerParser : INSParser
    {
        public IFTRObject? ParseFTRObject(Message message)
        {
            byte[] byteMessage = message.MessageBytes;
            int byteMessageLen = byteMessage.Length;

            if (byteMessageLen < 42)
            {
                return null;
            }

            string identifier = Encoding.ASCII.GetString(byteMessage, 0, 3);

            if (identifier != "NPA")
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
            UInt16 emailLen = BitConverter.ToUInt16(byteMessage, 31 + nameLen);
            string email = Encoding.ASCII.GetString(byteMessage, 33 + nameLen, emailLen);
            string pclass = Encoding.ASCII.GetString(byteMessage, 33 + nameLen + emailLen, 1);
            UInt64 miles = BitConverter.ToUInt64(byteMessage, 34 + nameLen + emailLen);

            return new Passenger
            {
                ID = id,
                Name = name,
                Age = age,
                Phone = phone,
                Email = email,
                Class = pclass,
                Miles = miles
            };
        }
    }
}
