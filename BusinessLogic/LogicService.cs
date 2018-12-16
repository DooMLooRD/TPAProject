using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using BusinessLogic.Mapper;
using BusinessLogic.Model.Assembly;
using DataLayer;
using DataLayer.DataModel;

namespace BusinessLogic
{
    [Export(typeof(LogicService))]
    public class LogicService
    {
        [ImportMany(typeof(ISerializer))]
        public IEnumerable<ISerializer> Serializer { get; set; }

        [Import(typeof(BaseAssemblyModel))]
        public BaseAssemblyModel AssemblyModel { get; set; }



        public void Save(AssemblyModel model, string path)
        {

            Serializer.ToList().FirstOrDefault()?.Save(AssemblyModelMapper.MapDown(model, AssemblyModel.GetType()), path);
        }

        public AssemblyModel Load(string path)
        {
            return AssemblyModelMapper.MapUp(Serializer.ToList().FirstOrDefault()?.Read(path));
        }
    }
}
