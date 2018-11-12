using System.Collections.ObjectModel;
using BusinessLogic.Model;
using BusinessLogic.Reflection;

namespace BusinessLogic.ViewModel.TreeViewItems
{
    public class ParameterTreeItem : TreeViewItem
    {
        public ParameterModel ParameterModel { get; set; }
        public ParameterTreeItem(ParameterModel parameterModel) : base(parameterModel.Name)
        {
            ParameterModel = parameterModel;
        }
        protected override void BuildTreeView(ObservableCollection<TreeViewItem> children)
        {
            if (ParameterModel.Type != null)
            {
                children.Add(new TypeTreeItem(DictionaryTypeSingleton.Instance.Get(ParameterModel.Type.Name)));
            }
        }

    }
}
