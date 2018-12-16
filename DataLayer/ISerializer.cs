using DataLayer.DataModel;

namespace DataLayer
{
    public interface ISerializer
    {
        void Save(BaseAssemblyModel _object, string path);
        BaseAssemblyModel Read(string path);
    }
}
