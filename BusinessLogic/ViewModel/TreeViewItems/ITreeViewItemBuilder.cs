using System.Collections.ObjectModel;

namespace BusinessLogic.ViewModel.TreeViewItems
{
    public interface ITreeViewItemBuilder
    {
        void BuildTreeView(ObservableCollection<TreeViewItem> children);
    }
}