using System.Collections.ObjectModel;

namespace ViewModels.TreeViewItems
{
    public abstract class TreeViewItem 
    {
        private bool _wasBuilt;
        private bool _isExpanded;
        public ObservableCollection<TreeViewItem> Children { get; set; }

        protected TreeViewItem()
        {
            Children = new ObservableCollection<TreeViewItem>() { null };
            this._wasBuilt = false;
        }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                if (_wasBuilt)
                    return;
                Children.Clear();
                BuildTreeView(Children);
                _wasBuilt = true;
            }
        }

        protected abstract void BuildTreeView(ObservableCollection<TreeViewItem> children);

    }
}
