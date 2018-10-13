using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Model;

namespace BusinessLogic.ViewModel
{
    public class AssemblyViewModel : BaseTreeViewModel, ITreeViewItemBuilder
    {
        public List<NamespaceModel> Namespaces { get; set; }
        public AssemblyViewModel(AssemblyModel assembly) : base(assembly.Name)
        {
            Namespaces = assembly.NamespaceModels;
        }

        public void BuildTreeView(ObservableCollection<TreeViewItem> children)
        {
            if (Namespaces != null)
            {
                foreach (NamespaceModel namespaceModel in Namespaces)
                {
                    children.Add(new TreeViewItem(namespaceModel.Name, ItemTypeEnum.Namespace, new NamespaceViewModel(namespaceModel)));
                }
            }
        }
    }
}
