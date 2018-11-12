using System.Collections.Generic;
using System.Collections.ObjectModel;
using BusinessLogic.Model;

namespace BusinessLogic.ViewModel.TreeViewItems
{
    public class AssemblyTreeItem :  TreeViewItem
    {

        public List<NamespaceModel> Namespaces { get; set; }
        public AssemblyTreeItem(AssemblyModel assembly) : base(assembly.Name)
        {
            Namespaces = assembly.NamespaceModels;
        }


        protected override void BuildTreeView(ObservableCollection<TreeViewItem> children)
        {
            if (Namespaces != null)
            {
                foreach (NamespaceModel namespaceModel in Namespaces)
                {
                    children.Add(new NamespaceTreeItem(namespaceModel));
                }
            }
        }
    }
}
