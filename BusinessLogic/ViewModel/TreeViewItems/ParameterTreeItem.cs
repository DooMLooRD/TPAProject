using System.Collections.ObjectModel;
using BusinessLogic.Model;

namespace BusinessLogic.ViewModel.TreeViewItems
{
    public class ParameterTreeItem :  ITreeViewItemBuilder
    {
        public string Name { get; set; }
        public ParameterModel ParameterModel { get; set; }
        public ParameterTreeItem(ParameterModel parameterModel)
        {
            Name = parameterModel.Name;
            ParameterModel = parameterModel;
        }
        public void BuildTreeView(ObservableCollection<TreeViewItem> children)
        {
            if (ParameterModel.Type != null)
            {
                children.Add(new TreeViewItem(ParameterModel.Type.Name, ItemTypeEnum.Type , new TypeTreeItem(TypeModel.TypeDictionary[ParameterModel.Type.Name])));
            }
        }

        
    }
}
