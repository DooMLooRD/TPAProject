using System.Collections.ObjectModel;
using BusinessLogic.Model;
using BusinessLogic.Reflection;

namespace BusinessLogic.ViewModel.TreeViewItems
{
    public class PropertyTreeItem : TreeViewItem
    {
        public PropertyModel PropertyModel { get; set; }
        public PropertyTreeItem(PropertyModel type,string name): base(name)
        {
            PropertyModel = type;
        }


        protected override void BuildTreeView(ObservableCollection<TreeViewItem> children)
        {
            if (PropertyModel.Type != null)
            {
                children.Add(new TypeTreeItem(DictionaryTypeSingleton.Instance.Get(PropertyModel.Type.Name)));
            }
        }
    }
}
