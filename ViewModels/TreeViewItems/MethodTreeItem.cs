using System;
using System.Collections.ObjectModel;
using BusinessLogic.Model.Assembly;

namespace ViewModels.TreeViewItems
{
    public class MethodTreeItem : TreeViewItem
    {
        public MethodModel MethodModel { get; set; }
        public MethodTreeItem(MethodModel methodModel) 
        {
            MethodModel = methodModel;
        }


        protected override void BuildTreeView(ObservableCollection<TreeViewItem> children)
        {

            if (MethodModel.GenericArguments != null)
            {
                foreach (TypeModel genericArgument in MethodModel.GenericArguments)
                {
                    children.Add(new TypeTreeItem(genericArgument));
                }
            }

            if (MethodModel.Parameters != null)
            {
                foreach (ParameterModel parameter in MethodModel.Parameters)
                {
                    children.Add(new ParameterTreeItem(parameter));
                }
            }

            if (MethodModel.ReturnType != null)
            {
                children.Add(new TypeTreeItem(MethodModel.ReturnType));
            }
        }
        public override string ToString()
        {
            return MethodModel.ToString();
        }
    }
}
