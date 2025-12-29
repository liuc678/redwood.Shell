namespace BaseClass
{
    public interface ISerializer
    {
        T DeserializeFromXml<T>(string filename) where T : class;
        void SerializeToXml(string filename, object data);
    }
}