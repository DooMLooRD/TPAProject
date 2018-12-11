using System.Collections.Generic;
using BusinessLogic.Model.Assembly;
using DataLayer;

namespace BusinessLogic
{

    public interface IReflectionService 
    {
        IEnumerable<ISerializer> Serializer { get; set; }
        IMapper Mapper { get; set; }
        void Save(AssemblyModel model, string path);
        AssemblyModel Load(string path);
    }
}
