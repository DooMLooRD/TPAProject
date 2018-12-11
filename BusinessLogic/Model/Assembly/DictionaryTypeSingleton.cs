using System.Collections.Generic;

namespace BusinessLogic.Model.Assembly
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
