using System.Xml.Serialization;

namespace QuickSurpriseRemake.Helpers
{
    public static class XmlHelper<T>
    {
        public static void SerializeToXmlFile(string filePath, T data)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occur during XML Serialization :{ex.Message}");
            }
        }

        public static T? DeserializeFromXml(string filePath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (StreamReader reader = new StreamReader(filePath))
                {
                    return (T)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occur during XML Deserialization :{ex.Message}");
            }
            return default(T);
        }
    }
}
