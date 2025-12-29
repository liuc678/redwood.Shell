using System.IO;
using System.Xml.Serialization;

namespace BaseClass
{
    public class SerializerXML : ISerializer
    {
        public void SerializeToXml(string filename,object data)
        {
            using (TextWriter writer = new StreamWriter(filename))
            {
                XmlSerializer serializer = new XmlSerializer(data.GetType());
                serializer.Serialize(writer, data);
            }
        }

        public T DeserializeFromXml<T>(string filename) where T :class
        {
            if (!File.Exists(filename))
                return null;
            using (TextReader reader = new StreamReader(filename))
            {
                try {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return (T)serializer.Deserialize(reader);
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
