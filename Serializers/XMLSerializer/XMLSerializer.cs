using System.ComponentModel.Composition;
using System.IO;
using System.Runtime.Serialization;
using BusinessLogic.Serialization;

namespace XMLSerializer
{
    [Export(typeof(ISerializer))]
    public class XMLSerializer : ISerializer
    {
        public void Serialize<T>(T _object, string path)
        {
            DataContractSerializer dataContractSerializer =
                new DataContractSerializer(typeof(T));

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {       
                dataContractSerializer.WriteObject(fileStream, _object);
            }
        }

        public T Deserialize<T>(string path)
        {
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(T));
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                return (T) dataContractSerializer.ReadObject(fileStream);
            }
        }
    }
}
