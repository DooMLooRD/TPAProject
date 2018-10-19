using System.Collections.Generic;
using System.Collections.ObjectModel;
using BusinessLogic.Model;

namespace BusinessLogic.ViewModel.TreeViewItems
{
    public class AssemblyTreeItem :  ITreeViewItemBuilder
    {
        public string Name { get; set; }
        public List<NamespaceModel> Namespaces { get; set; }
        public AssemblyTreeItem(AssemblyModel assembly)
        {
            Name = assembly.Name;
            Namespaces = assembly.NamespaceModels;
        }

        public void BuildTreeView(ObservableCollection<TreeViewItem> children)
        {
            if (Namespaces != null)
            {
                foreach (NamespaceModel namespaceModel in Namespaces)
                {
                    children.Add(new TreeViewItem(namespaceModel.Name, ItemTypeEnum.Namespace, new NamespaceTreeItem(namespaceModel)));
                }
            }
        }
    }
}
