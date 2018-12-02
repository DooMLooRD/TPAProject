using System.Collections.ObjectModel;
using BusinessLogic.Model.Assembly;

namespace ViewModels.TreeViewItems
{
    public class ParameterTreeItem : TreeViewItem
    {
        public ParameterModel ParameterModel { get; set; }
        public ParameterTreeItem(ParameterModel parameterModel)
        {
            ParameterModel = parameterModel;
        }
        protected override void BuildTreeView(ObservableCollection<TreeViewItem> children)
        {
            if (ParameterModel.Type != null)
            {
                children.Add(new TypeTreeItem(ParameterModel.Type));
            }
        }
        public override string ToString()
        {
            return ParameterModel.Type.Name + " " + ParameterModel.Name;
        }
    }
}
