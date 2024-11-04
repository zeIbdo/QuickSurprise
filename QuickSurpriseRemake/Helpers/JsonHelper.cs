using System.Text.Json;
using System.Xml.Serialization;

namespace QuickSurpriseRemake.Helpers
{
    using System.Text.Json;

    public static class JsonHelper<T>
    {
        public static void SerializeToJsonFile(string filePath, T data)
        {
            try
            {
                var json = JsonSerializer.Serialize(data);

                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred during JSON serialization: {ex.Message}");
            }
        }

        public static T? DeserializeFromJsonFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("File not found.");
                    return default;
                }

                var json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<T>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred during JSON deserialization: {ex.Message}");
                return default;
            }
        }
    }

}
