using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BusinessLogic.Model;

namespace BusinessLogic.Serialization
{
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
