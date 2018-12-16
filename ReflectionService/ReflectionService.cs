using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using BusinessLogic;
using BusinessLogic.Model.Assembly;
using DataLayer;

namespace ReflectionService
{
    [Export(typeof(IReflectionService))]
    public class ReflectionService : IReflectionService
    {
        [ImportMany(typeof(ISerializer))]
        public IEnumerable<ISerializer> Serializer { get; set; }

        [Import(typeof(IMapper))]
        public IMapper Mapper { get; set; }

        public void Save(AssemblyModel model, string path)
        {
            Serializer.ToList().FirstOrDefault()?.Save(Mapper.MapDown(model), path);
        }

        public AssemblyModel Load(string path)
        {
            return Mapper.MapUp(Serializer.ToList().FirstOrDefault()?.Read(path));
        }
    }
}
