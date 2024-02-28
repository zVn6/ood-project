using FligthApplication.Models.Interfaces;
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
            string outputFilePath = Path.Combine(Environment.CurrentDirectory, @"../../../Output/output.json");

            IParser<IFTRObject> parser = new FTRParser();

            List<IFTRObject> objects = parser.Parse(filePath);

            if (objects != null && objects.Count > 0)
            {
                string json = JSONHandler.SerializeObjectsToJson(objects);

                JSONHandler.SaveJsonToFile(json, outputFilePath);

                Console.WriteLine("JSON serialization completed.");
            }
            else
            {
                Console.WriteLine("No objects were parsed from the FTR file.");
            }
        }
    }
}
