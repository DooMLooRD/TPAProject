using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Runtime.Serialization;
using DataLayer;
using DataLayer.DataModel;
using FileData.XMLModel;


namespace FileData
{
    [Export(typeof(ISerializer))]
    public class XMLSerializer : ISerializer
    {
        public void Save(BaseAssemblyModel _object, string path)
        {
            XMLAssemblyModel assembly = (XMLAssemblyModel)_object;
            DataContractSerializer dataContractSerializer =
                new DataContractSerializer(typeof(XMLAssemblyModel));

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                dataContractSerializer.WriteObject(fileStream, assembly);
            }
        }

        public BaseAssemblyModel Read(string path)
        {
            XMLAssemblyModel model;

            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(XMLAssemblyModel));
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                model = (XMLAssemblyModel)dataContractSerializer.ReadObject(fileStream);
            }

            return model;
        }
    }
}
