using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Model;

namespace BusinessLogic.ViewModel
{
    public class ParameterViewModel : BaseTreeViewModel, ITreeViewItemBuilder
    {
        public ParameterModel ParameterModel { get; set; }
        public ParameterViewModel(ParameterModel parameterModel) : base(parameterModel.Name)
        {
            ParameterModel = parameterModel;
        }
        public void BuildTreeView(ObservableCollection<TreeViewItem> children)
        {
            if (ParameterModel.Type != null)
            {
                children.Add(new TreeViewItem(ParameterModel.Type.Name, ItemTypeEnum.Type , new TypeViewModel(TypeModel.TypeDictionary[ParameterModel.Type.Name])));
            }
        }

        
    }
}
