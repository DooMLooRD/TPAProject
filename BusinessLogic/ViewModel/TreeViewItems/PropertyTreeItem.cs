using System.Collections.ObjectModel;
using BusinessLogic.Model;

namespace BusinessLogic.ViewModel.TreeViewItems
{
    public class PropertyTreeItem : ITreeViewItemBuilder
    {
        public string Name { get; set; }
        public PropertyModel PropertyModel { get; set; }
        public PropertyTreeItem(PropertyModel type)
        {
            Name = type.Name;
            PropertyModel = type;
        }

        public void BuildTreeView(ObservableCollection<TreeViewItem> children)
        {
            if (PropertyModel.Type != null)
            {
                children.Add(new TreeViewItem(PropertyModel.Type.Name, ItemTypeEnum.Type ,new TypeTreeItem(TypeModel.TypeDictionary[PropertyModel.Type.Name])));
            }
        }
    }
}
