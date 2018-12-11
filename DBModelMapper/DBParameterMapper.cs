using BusinessLogic.Model.Assembly;
using DBData.Entities;

namespace DBModelMapper
{
    public class DBParameterMapper 
    {
        public ParameterModel MapUp(DBParameterModel model)
        {
            ParameterModel parameterModel = new ParameterModel();
            parameterModel.Name = model.Name;
            if (model.Type != null)
                parameterModel.Type = DBTypeMapper.EmitType((DBTypeModel)model.Type);
            return parameterModel;
        }

        public DBParameterModel MapDown(ParameterModel model)
        {
            DBParameterModel parameterModel = new DBParameterModel();
            parameterModel.Name = model.Name;
            if (model.Type != null)
                parameterModel.Type = DBTypeMapper.EmitDBType(model.Type);
            return parameterModel;
        }
    }
}
