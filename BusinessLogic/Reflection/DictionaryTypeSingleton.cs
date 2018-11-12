using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Model;

namespace BusinessLogic.Reflection
{
    public sealed class DictionaryTypeSingleton
    {

        public static DictionaryTypeSingleton Instance { get; } = new DictionaryTypeSingleton();

        private readonly Dictionary<string, TypeModel> _data = new Dictionary<string, TypeModel>();
        private DictionaryTypeSingleton()
        {
        }

        public void Add(string name, TypeModel type)
        {
            _data.Add(name, type);
        }

        public bool ContainsKey(string name)
        {
            return _data.ContainsKey(name);
        }

        public TypeModel Get(string key)
        {
            _data.TryGetValue(key, out TypeModel value);
            return value;
        }
    }
}
