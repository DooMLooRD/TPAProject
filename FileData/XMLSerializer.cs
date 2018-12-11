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
        public void Save(IAssemblyModel _object, string path)
        {
            XMLAssemblyModel assembly = (XMLAssemblyModel)_object;
            List<Type> knownTypes = new List<Type>
            {
                typeof(XMLTypeModel),
                typeof(XMLNamespaceModel),
                typeof(XMLMethodModel),
                typeof(XMLParameterModel),
                typeof(XMLPropertyModel)
            };

            DataContractSerializer dataContractSerializer =
                new DataContractSerializer(typeof(XMLAssemblyModel),knownTypes);

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                dataContractSerializer.WriteObject(fileStream, assembly);
            }
        }

        public IAssemblyModel Read(string path)
        {
            XMLAssemblyModel model;
            List<Type> knownTypes = new List<Type>
            {
                typeof(XMLTypeModel),
                typeof(XMLNamespaceModel),
                typeof(XMLMethodModel),
                typeof(XMLParameterModel),
                typeof(XMLPropertyModel)
            };

            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(XMLAssemblyModel),knownTypes);
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                model = (XMLAssemblyModel)dataContractSerializer.ReadObject(fileStream);
            }

            return model;
        }
    }
}
