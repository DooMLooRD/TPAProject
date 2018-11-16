using System;
using System.Collections.ObjectModel;
using BusinessLogic.Model;
using BusinessLogic.Reflection;

namespace BusinessLogic.ViewModel.TreeViewItems
{
    public class PropertyTreeItem : TreeViewItem
    {
        public PropertyModel PropertyModel { get; set; }
        public PropertyTreeItem(PropertyModel type)
        {
            PropertyModel = type;
        }


        protected override void BuildTreeView(ObservableCollection<TreeViewItem> children)
        {
            if (PropertyModel.Type != null)
            {
                children.Add(new TypeTreeItem(PropertyModel.Type));
            }
        }
        public override string ToString()
        {
            return PropertyModel.Type.Name + " " + PropertyModel.Name;


        }
    }
}
