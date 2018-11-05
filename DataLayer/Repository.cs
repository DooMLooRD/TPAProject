using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Repository
    {
        public void Log(LogEntity entity)
        {
            using (TPADataContext context = new TPADataContext())
            {
                context.LogEntities.Add(entity);
                context.SaveChanges();
            }
        }

        public void Serialize(SerializationEntity entity)
        {
            using (TPADataContext context = new TPADataContext())
            {
                context.SerializationEntities.ToList().ForEach(x=> context.SerializationEntities.Remove(x));
                context.SerializationEntities.Add(entity);
                context.SaveChanges();
            }
        }

        public List<SerializationEntity> Deserialize()
        {
            List<SerializationEntity> entities = new List<SerializationEntity>();
            using (TPADataContext context = new TPADataContext())
            {
                foreach (SerializationEntity serializationEntity in context.SerializationEntities)
                {
                    entities.Add(serializationEntity);
                }
            }

            return entities;
        }
    }
}
