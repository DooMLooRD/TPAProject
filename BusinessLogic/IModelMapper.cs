using BusinessLogic.Model.Assembly;
using DataLayer.DataModel;

namespace BusinessLogic
{
    public interface IMapper
    {
        AssemblyModel MapUp(IAssemblyModel model);
        IAssemblyModel MapDown(AssemblyModel model);
    }
}
