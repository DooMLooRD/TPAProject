using DataLayer.DataModel;

namespace DataLayer
{
    public interface ISerializer
    {
        void Save(IAssemblyModel _object, string path);
        IAssemblyModel Read(string path);
    }
}
