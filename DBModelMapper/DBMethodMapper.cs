using System.Linq;
using BusinessLogic.Model.Assembly;
using DBData.Entities;

namespace DBModelMapper
{
    public class DBMethodMapper
    {

        public MethodModel MapUp(DBMethodModel model)
        {
            MethodModel methodModel = new MethodModel();
            methodModel.Name = model.Name;
            methodModel.Extension = model.Extension;
            if (model.GenericArguments != null)
                methodModel.GenericArguments = model.GenericArguments.Select(g=> DBTypeMapper.EmitType((DBTypeModel)g)).ToList();
            methodModel.Modifiers = model.Modifiers;
            if (model.Parameters != null)
                methodModel.Parameters = model.Parameters.Select(p => new DBParameterMapper().MapUp((DBParameterModel)p)).ToList();
            if (model.ReturnType != null)
                methodModel.ReturnType = DBTypeMapper.EmitType((DBTypeModel)model.ReturnType);
            return methodModel;
        }

        public DBMethodModel MapDown(MethodModel model)
        {
            DBMethodModel methodModel = new DBMethodModel();
            methodModel.Name = model.Name;
            methodModel.Extension = model.Extension;
            if (model.GenericArguments != null)
                methodModel.GenericArguments = model.GenericArguments.Select(t=>DBTypeMapper.EmitDBType(t)).ToList();
            methodModel.Modifiers = model.Modifiers;
            if (model.Parameters != null)
                methodModel.Parameters = model.Parameters.Select(p => new DBParameterMapper().MapDown(p)).ToList();
            if (model.ReturnType != null)
                methodModel.ReturnType = DBTypeMapper.EmitDBType(model.ReturnType);
            return methodModel;
        }
    }
}
