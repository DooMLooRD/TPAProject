using System.Collections.Generic;
using System.Collections.ObjectModel;
using BusinessLogic.Model.Assembly;

namespace ViewModels.TreeViewItems
{
    public class AssemblyTreeItem :  TreeViewItem
    {
        public AssemblyModel Assembly { get; set; }
        public IEnumerable<NamespaceModel> Namespaces { get; set; }
        public AssemblyTreeItem(AssemblyModel assembly)
        {
            Assembly = assembly;
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

        public override string ToString()
        {
            return Assembly.Name;
        }
    }
}
