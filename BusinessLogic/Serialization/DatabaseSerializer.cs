using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLogic.Serialization
{
    public class DataBaseSerializer : ISerializer
    {
        Repository repository = new Repository();
        public void Serialize<T>(T _object, string path)
        {
            DataContractSerializer dataContractSerializer =
                new DataContractSerializer(typeof(T));

            using (MemoryStream stream = new MemoryStream())
            {

                dataContractSerializer.WriteObject(stream, _object);

                repository.Serialize(new SerializationEntity()
                {
                    SerializedObject = stream.ToArray()
                });


            }
        }

        public T Deserialize<T>(string path)
        {
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(T));

            using (MemoryStream stream = new MemoryStream(repository.Deserialize()[0].SerializedObject))
            {
                return (T)dataContractSerializer.ReadObject(stream);
            }


        }
    }
}
