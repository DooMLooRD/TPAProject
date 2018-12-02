using BusinessLogic.Model.Assembly;

namespace MEF
{
    public interface ISerializer
    {
        void Save(AssemblyModel _object, string path);
        AssemblyModel Read(string path);
    }
}
