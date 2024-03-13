using FligthApplication.Models.Interfaces;
using FligthApplication.NetworkSourceParser;
using FligthApplication.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace FligthApplication
{
    class Program
    {
        static void Main()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, @"../../../Data/FTRfile.txt");

            var retrieval = new NSRetriever(filePath);
            bool close = false;
            bool simStarted = false;

            while(!close)
            {
                Console.WriteLine("\nSelect appropriate option:");
                Console.WriteLine("1: Start simulation");
                Console.WriteLine("2: Take a snapshot");
                Console.WriteLine("3: Exit the simulation");
                Console.Write("Choose option: ");

                string? option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        if (simStarted == false) {
                            retrieval.Run();
                            simStarted = true;
                        } else {
                            Console.WriteLine("Simulation already started!");
                        }
                        break;
                    case "2":
                        if (simStarted == false)
                        {
                            Console.WriteLine("Start a simulation firstly!");
                        }
                        else
                        {
                            retrieval.CreateSnapshot();
                            Console.WriteLine("Snapshot created!");
                        }         
                        break;
                    case "3":
                        retrieval.Exit();
                        close = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option selected. Try again ...");
                        break;
                }
            }
        }
    }
}
