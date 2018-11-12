using System.Collections.Generic;
using System.Collections.ObjectModel;
using BusinessLogic.Model;
using BusinessLogic.Reflection;

namespace BusinessLogic.ViewModel.TreeViewItems
{
    public class NamespaceTreeItem : TreeViewItem
    {
        public List<TypeModel> Types { get; set; }
        public NamespaceTreeItem(NamespaceModel namespaceModel):base(namespaceModel.Name)
        { 
            Types = namespaceModel.Types;
        }

        protected override void BuildTreeView(ObservableCollection<TreeViewItem> children)
        {
            if (Types != null)
            {
                foreach (TypeModel typeModel in Types)
                {
                    children.Add(new TypeTreeItem(DictionaryTypeSingleton.Instance.Get(typeModel.Name)));
                }
            }
        }
    }
}
