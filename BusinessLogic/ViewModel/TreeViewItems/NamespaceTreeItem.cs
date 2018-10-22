using System.Collections.Generic;
using System.Collections.ObjectModel;
using BusinessLogic.Model;

namespace BusinessLogic.ViewModel.TreeViewItems
{
    public class NamespaceTreeItem : TreeViewItem
    {
        public List<TypeModel> Types { get; set; }
        public NamespaceTreeItem(NamespaceModel namespaceModel):base(namespaceModel.Name, ItemTypeEnum.Namespace)
        { 
            Types = namespaceModel.Types;
        }

        protected override void BuildTreeView(ObservableCollection<TreeViewItem> children)
        {
            if (Types != null)
            {
                foreach (TypeModel typeModel in Types)
                {
                    ItemTypeEnum typeEnum = typeModel.Type == TypeEnum.Class ?
                        ItemTypeEnum.Class : typeModel.Type == TypeEnum.Enum ?
                            ItemTypeEnum.Enum : typeModel.Type == TypeEnum.Interface ?
                                ItemTypeEnum.Interface : ItemTypeEnum.Struct;

                    children.Add(new TypeTreeItem(TypeModel.TypeDictionary[typeModel.Name],typeEnum));
                }
            }
        }
    }
}
