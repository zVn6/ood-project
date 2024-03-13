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
    public class NewFlightParser: INSParser
    {
        private static readonly DateTime EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public IFTRObject? ParseFTRObject(Message message)
        {
            byte[] byteMessage = message.MessageBytes;
            int byteMessageLen = byteMessage.Length;

            if (byteMessageLen < 59)
            {
                return null;
            }

            string identifier = Encoding.ASCII.GetString(byteMessage, 0, 3);

            if (identifier != "NFL")
            {
                return null;
            }

            UInt32 followingMsgLen = BitConverter.ToUInt32(byteMessage, 3);

            if (followingMsgLen + 7 != byteMessageLen)
            {
                return null;
            }

            UInt64 id = BitConverter.ToUInt64(byteMessage, 7);
            UInt64 originAsId = BitConverter.ToUInt64(byteMessage, 15);
            UInt64 targetAsId = BitConverter.ToUInt64(byteMessage, 23);

            Int64 takeOffTime = BitConverter.ToInt64(byteMessage, 31);
            string takeOffTimeStr = EPOCH.AddMilliseconds(takeOffTime).ToString("HH:mm");

            Int64 landingTime = BitConverter.ToInt64(byteMessage, 39);
            string landingTimeStr = EPOCH.AddMilliseconds(landingTime).ToString("HH:mm");

            UInt64 planeId = BitConverter.ToUInt64(byteMessage, 47);
            UInt16 crewCount = BitConverter.ToUInt16(byteMessage, 55);

            UInt64[] crew = new ulong[crewCount];

            for(int i = 0; i < crewCount; i++)
            {
                crew[i] = BitConverter.ToUInt64(byteMessage, 57 + i * 8);
            }

            UInt16 loadCount = BitConverter.ToUInt16(byteMessage, 57 + 8 * crewCount);
            UInt64[] load = new ulong[loadCount];
            for (int i = 0; i < crewCount; i++)
            {
                load[i] = BitConverter.ToUInt64(byteMessage, 59 + 8*crewCount + i * 8);
            }

            return new Flight
            {
               ID = id,
               OriginAsID = originAsId,
               TargetAsID = targetAsId,
               TakeoffTime = takeOffTimeStr,
               LandingTime = landingTimeStr,
               PlaneID = planeId,
               CrewIDs = crew,
               Load = load
            };
        }
    }
}
