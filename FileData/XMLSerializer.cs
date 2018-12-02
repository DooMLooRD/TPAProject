using System.ComponentModel.Composition;
using System.IO;
using System.Runtime.Serialization;
using BusinessLogic.Model.Assembly;
using FileData.XMLModel;
using MEF;

namespace FileData
{
    [Export(typeof(ISerializer))]
    public class XMLSerializer : ISerializer
    {
        public void Save(AssemblyModel _object, string path)
        {
            XMLAssemblyModel assembly = new XMLAssemblyModel().MapDown(_object);
            DataContractSerializer dataContractSerializer =
                new DataContractSerializer(typeof(XMLAssemblyModel));

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                dataContractSerializer.WriteObject(fileStream, assembly);
            }
        }

        public AssemblyModel Read(string path)
        {
            XMLAssemblyModel model;
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(XMLAssemblyModel));
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                model = (XMLAssemblyModel)dataContractSerializer.ReadObject(fileStream);
            }

            return model.MapUp(model);
        }
    }
}
