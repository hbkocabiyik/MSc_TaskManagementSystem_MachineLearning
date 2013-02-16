using System.IO;
using System.Xml.Serialization;

namespace Agh.DecisionTree.Serialization
{
    public static class DataSerializer
    {
        public static void Serialize<T>(T objectToSerialize, string fileName)
        {
            var fileInfo = new FileInfo(fileName);

            if (fileInfo.Exists)
                fileInfo.Delete();

            using (var fileStream = fileInfo.OpenWrite())
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(fileStream, objectToSerialize);
            }
        }

        public static T Deserialize<T>(string fileName) where T : class
        {
            var fileInfo = new FileInfo(fileName);

            if (!fileInfo.Exists)
                return null;

            using (var fileStream = fileInfo.OpenRead())
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(fileStream);
            }
        }
    }
}
