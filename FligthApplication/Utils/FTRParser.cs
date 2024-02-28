using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using FligthApplication.Models;
using FligthApplication.Models.Interfaces;

namespace FligthApplication.Utils
{
    public class FTRParser : IParser<IFTRObject>
    {
        public List<IFTRObject> Parse(string filePath)
        {
            List<IFTRObject> result = new List<IFTRObject>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (var line in lines)
                {

                    var values = line.Split(",");

                    if (values.Length > 0)
                    {
                        string identifier = values[0].Trim();
                        IFTRObject obj = CreateObject(identifier, values.Skip(1).ToArray());
                        if (obj != null)
                        {
                            result.Add(obj);
                        }
                        else
                        {
                            Console.WriteLine($"Failed to create object for line: {line}");
                            throw new ArgumentException();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading the file: {ex.Message}");
            }

            return result;
        }

        private IFTRObject CreateObject(string identifier, string[] values)
        {
            Dictionary<string, Func<string[], IFTRObject>> objectCreationMethods = new Dictionary<string, Func<string[], IFTRObject>>
            {
                { "P", CreatePassenger },
                { "C", CreateCrew },
                { "CA", CreateCargo },
                { "CP", CreateCargoPlane },
                { "PP", CreatePassengerPlane },
                { "AI", CreateAirport },
                { "FL", CreateFlight }
            };

            if (objectCreationMethods.TryGetValue(identifier, out var creationMethod))
            {
                return creationMethod(values);
            }
            else
            {
                Console.WriteLine($"Unknown class identifier: {identifier}");
                return null;
            }
        }

        private IFTRObject CreatePassenger(string[] values)
        {
            if (values.Length >= 7)
            {
                try
                {
                    var passenger = new Passenger
                    {
                        ID = ulong.Parse(values[0]),
                        Name = values[1],
                        Age = ulong.Parse(values[2]),
                        Phone = values[3],
                        Email = values[4],
                        Class = values[5],
                        Miles = ulong.Parse(values[6])
                    };

                    return passenger;

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating Passenger object: {ex.Message}");
                }
            }

            return null;
        }
        private IFTRObject CreateCrew(string[] values)
        {
            if (values.Length >= 7)
            {
                try
                {
                    var crew = new Crew
                    {
                        ID = ulong.Parse(values[0]),
                        Name = values[1],
                        Age = ulong.Parse(values[2]),
                        Phone = values[3],
                        Email = values[4],
                        Practice = ushort.Parse(values[5]),
                        Role = values[6]
                    };

                    return crew;

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating Crew object: {ex.Message}");
                }
            }

            return null;
        }
        private IFTRObject CreateCargo(string[] values)
        {
            if (values.Length >= 4)
            {
                try
                {
                    var cargo = new Cargo
                    {
                        ID = ulong.Parse(values[0]),
                        Weight = float.Parse(values[1]),
                        Code = values[2],
                        Description = values[3]
                    };

                    return cargo;

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating Cargo object: {ex.Message}");
                }
            }
            return null;
        }
        private IFTRObject CreateCargoPlane(string[] values)
        {
            if (values.Length >= 5)
            {
                try
                {
                    var cargoPlane = new CargoPlane
                    {
                        ID = ulong.Parse(values[0]),
                        Serial = values[1],
                        CountryISO = values[2],
                        Model = values[3],
                        MaxLoad = float.Parse(values[4])
                    };

                    return cargoPlane;

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating CargoPlane object: {ex.Message}");
                }
            }
            return null;
        }
        private IFTRObject CreatePassengerPlane(string[] values)
        {
            if (values.Length >= 6)
            {
                try
                {
                    var passengerPlane = new PassengerPlane
                    {
                        ID = ulong.Parse(values[0]),
                        Serial = values[1],
                        CountryISO = values[2],
                        Model = values[3],
                        FirstClassSize = ushort.Parse(values[4]),
                        BusinessClassSize = ushort.Parse(values[5]),
                        EconomyClassSize = ushort.Parse(values[6])
                    };

                    return passengerPlane;

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating PassengerPlane object: {ex.Message}");
                }
            }
            return null;
        }
        private IFTRObject CreateAirport(string[] values)
        {
            if (values.Length >= 7)
            {
                try
                {
                    var airport = new Airport
                    {
                        ID = ulong.Parse(values[0]),
                        Name = values[1],
                        Code = values[2],
                        Longitude = float.Parse(values[3]),
                        Latitude = float.Parse(values[4]),
                        AMSL = float.Parse(values[5]),
                        CountryISO = values[6]
                    };

                    return airport;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating Airport object: {ex.Message}");
                }
            }
            return null;
        }
        private IFTRObject CreateFlight(string[] values)
        {
            if (values.Length >= 11)
            {
                try
                {
                    var flight = new Flight
                    {
                        ID = ulong.Parse(values[0]),
                        OriginAsID = ulong.Parse(values[1]),
                        TargetAsID = ulong.Parse(values[2]),
                        TakeoffTime = values[3],
                        LandingTime = values[4],
                        Longitude = float.Parse(values[5]),
                        Latitude = float.Parse(values[6]),
                        AMSL = float.Parse(values[7]),
                        PlaneID = ulong.Parse(values[8]),
                        CrewIDs = values[9][1..^2].Split(';').Select(x => ulong.Parse(x)).ToArray(),
                        Load = values[10][1..^2].Split(';').Select(x => ulong.Parse(x)).ToArray(),
                    };

                    return flight;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating Flight object: {ex.Message}");
                }
            }
            return null;
        }
    }
}
