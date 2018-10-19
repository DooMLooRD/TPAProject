using System.Collections.Generic;
using System.Collections.ObjectModel;
using BusinessLogic.Model;

namespace BusinessLogic.ViewModel.TreeViewItems
{
    public class NamespaceTreeItem : ITreeViewItemBuilder
    {
        public string Name { get; set; }
        public List<TypeModel> Types { get; set; }
        public NamespaceTreeItem(NamespaceModel namespaceModel)
        {
            Name = namespaceModel.Name;
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
