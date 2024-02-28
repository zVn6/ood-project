using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FligthApplication.Models.Interfaces;

namespace FligthApplication.Utils
{
    public static class JSONHandler
    {
        public static string SerializeObjectsToJson(List<IFTRObject> objects)
        {
            var temp = objects.Select(x => (object)x);
            return JsonSerializer.Serialize(temp, new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }

        public static void SaveJsonToFile(string json, string filePath)
        {
            try
            {
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving JSON to file: {ex.Message}");
            }
        }
    }
}
