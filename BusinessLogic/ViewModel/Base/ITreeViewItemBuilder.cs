using System.Collections.ObjectModel;

namespace BusinessLogic.ViewModel
{
    public interface ITreeViewItemBuilder
    {
        void BuildTreeView(ObservableCollection<TreeViewItem> children);
    }
}