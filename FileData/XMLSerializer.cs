using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Runtime.Serialization;
using DataLayer;
using DataLayer.DataModel;
using FileData.XMLModel;
using Newtonsoft.Json;


namespace FileData
{
    [Export(typeof(ISerializer))]
    public class XMLSerializer : ISerializer
    {
        public void Save(BaseAssemblyModel _object, string path)
        {
            
            XMLAssemblyModel assembly = (XMLAssemblyModel)_object;
            string name = JsonConvert.SerializeObject(assembly, Formatting.Indented,
                new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });

            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(path, true))
            {
                file.Write(name);
            }

        }

        public BaseAssemblyModel Read(string path)
        {
            XMLAssemblyModel model;
            if (!File.Exists(path))
                throw new ArgumentException("File not exist");
            using (System.IO.StreamReader file =
                new System.IO.StreamReader(path, true))
            {
                var reader = file.ReadToEnd();
                model= JsonConvert.DeserializeObject<XMLAssemblyModel>(reader,
                    new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            }

            return model;
        }
    }
}
