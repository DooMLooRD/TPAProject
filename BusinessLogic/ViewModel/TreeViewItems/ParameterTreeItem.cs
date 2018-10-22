using System.Collections.ObjectModel;
using BusinessLogic.Model;

namespace BusinessLogic.ViewModel.TreeViewItems
{
    public class ParameterTreeItem :  TreeViewItem
    {
        public ParameterModel ParameterModel { get; set; }
        public ParameterTreeItem(ParameterModel parameterModel):base(parameterModel.Name,ItemTypeEnum.Field)
        {
            ParameterModel = parameterModel;
        }
        protected override void BuildTreeView(ObservableCollection<TreeViewItem> children)
        {
            if (ParameterModel.Type != null)
            {
                children.Add(new TypeTreeItem(TypeModel.TypeDictionary[ParameterModel.Type.Name], ItemTypeEnum.Type));
            }
        }

    }
}
