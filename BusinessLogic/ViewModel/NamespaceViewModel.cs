using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Model;

namespace BusinessLogic.ViewModel
{
    public class NamespaceViewModel : BaseTreeViewModel, ITreeViewItemBuilder
    {
        public List<TypeModel> Types { get; set; }
        public NamespaceViewModel(NamespaceModel namespaceModel) : base(namespaceModel.Name)
        {
            Types = namespaceModel.Types;
        }

        public void BuildTreeView(ObservableCollection<TreeViewItem> children)
        {
            if (Types != null)
            {
                foreach (TypeModel typeModel in Types)
                {
                    children.Add(new TreeViewItem(TypeViewModel.GetModifiers(typeModel) + typeModel.Name, ItemTypeEnum.Type, new TypeViewModel(TypeModel.TypeDictionary[typeModel.Name])));
                }
            }
        }
    }
}
