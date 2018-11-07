using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BusinessLogic.Settings.Helper
{
    public class SettingsSerializer
    {
        public async void Serialize<T>(T _object, string path)
        {
            string json = JsonConvert.SerializeObject(_object, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

            using (FileStream fileStream = File.Create(path))
            {
                using (StreamWriter writer = new StreamWriter(fileStream))
                {
                    await writer.WriteAsync(json);
                }

            }
        }

        public async Task<T> Deserialize<T>(string path)
        {
            using (FileStream fileStream = File.OpenRead(path))
            {
                using (StreamReader file = new StreamReader(fileStream))
                {
                    string all = await file.ReadToEndAsync();
                    T deserialized = JsonConvert.DeserializeObject<T>(all, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });
                    return deserialized;
                }
            }
        }
    }
}
