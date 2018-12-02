using System.Collections.Generic;
using System.Collections.ObjectModel;
using BusinessLogic.Model.Assembly;

namespace ViewModels.TreeViewItems
{
    public class NamespaceTreeItem : TreeViewItem
    {
        public NamespaceModel NamespaceModel { get; set; }
        public IEnumerable<TypeModel> Types { get; set; }
        public NamespaceTreeItem(NamespaceModel namespaceModel)
        {
            NamespaceModel = namespaceModel;
            Types = namespaceModel.Types;
        }

        protected override void BuildTreeView(ObservableCollection<TreeViewItem> children)
        {
            if (Types != null)
            {
                foreach (TypeModel typeModel in Types)
                {
                    children.Add(new TypeTreeItem(typeModel));
                }
            }
        }
        public override string ToString()
        {
            return NamespaceModel.Name;
        }
    }
}
