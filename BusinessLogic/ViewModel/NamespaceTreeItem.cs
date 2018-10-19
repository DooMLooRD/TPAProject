using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Model;

namespace BusinessLogic.ViewModel
{
    public class NamespaceTreeItem : BaseTreeViewModel, ITreeViewItemBuilder
    {
        public List<TypeModel> Types { get; set; }
        public NamespaceTreeItem(NamespaceModel namespaceModel) : base(namespaceModel.Name)
        {
            Types = namespaceModel.Types;
        }

        public void BuildTreeView(ObservableCollection<TreeViewItem> children)
        {
            if (Types != null)
            {
                foreach (TypeModel typeModel in Types)
                {
                    ItemTypeEnum typeEnum = typeModel.Type == TypeEnum.Class ?
                        ItemTypeEnum.Class : typeModel.Type == TypeEnum.Enum ?
                            ItemTypeEnum.Enum : typeModel.Type == TypeEnum.Interface ?
                                ItemTypeEnum.Interface : ItemTypeEnum.Struct;

                    children.Add(new TreeViewItem(TypeTreeItem.GetModifiers(typeModel) + typeModel.Name, typeEnum, new TypeTreeItem(TypeModel.TypeDictionary[typeModel.Name])));
                }
            }
        }
    }
}
