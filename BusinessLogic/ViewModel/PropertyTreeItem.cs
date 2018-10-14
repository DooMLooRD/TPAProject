using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Model;

namespace BusinessLogic.ViewModel
{
    public class PropertyTreeItem : BaseTreeViewModel, ITreeViewItemBuilder
    {
        public PropertyModel PropertyModel { get; set; }
        public PropertyTreeItem(PropertyModel type) : base(type.Name)
        {
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
