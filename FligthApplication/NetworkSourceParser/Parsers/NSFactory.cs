using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FligthApplication.NetworkSourceParser.Parsers
{
    public class NSFactory
    {
        private static readonly Dictionary<string, INSParser> parsers = new Dictionary<string, INSParser>
        {
            {"NCR", new NewCrewParser()},
            {"NPA", new NewPassengerParser()},
            {"NCA", new NewCargoParser()},
            {"NCP", new NewCargoPlaneParser()},
            {"NPP", new NewPassengerPlaneParser()},
            {"NAI", new NewAirportParser()},                
            {"NFL", new NewFlightParser()}
        };

        public static INSParser GetParser(string id)
        {
            return parsers.TryGetValue(id, out INSParser? parser)
                ? parser
                : throw new ArgumentException($"Unknown class identifier: {id}");

        }
    }
}
