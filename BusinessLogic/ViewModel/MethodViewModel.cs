using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Model;

namespace BusinessLogic.ViewModel
{
    public class MethodViewModel : BaseTreeViewModel, ITreeViewItemBuilder
    {
        public MethodModel MethodModel { get; set; }
        public MethodViewModel(MethodModel methodModel) : base(methodModel.Name)
        {
            MethodModel = methodModel;
        }

        public void BuildTreeView(ObservableCollection<TreeViewItem> children)
        {
            if (MethodModel.GenericArguments != null)
            {
                foreach (TypeModel genericArgument in MethodModel.GenericArguments)
                {
                    children.Add(new TreeViewItem(genericArgument.Name, ItemTypeEnum.GenericArgument, new TypeViewModel(TypeModel.TypeDictionary[genericArgument.Name])));
                }
            }

            if (MethodModel.Parameters != null)
            {
                foreach (ParameterModel parameter in MethodModel.Parameters)
                {
                    children.Add(new TreeViewItem(parameter.Name, ItemTypeEnum.Parameter, new ParameterViewModel(parameter)));
                }
            }

            if (MethodModel.ReturnType != null)
            {
                children.Add(new TreeViewItem(MethodModel.ReturnType.Name, ItemTypeEnum.ReturnType, new TypeViewModel(TypeModel.TypeDictionary[MethodModel.ReturnType.Name])));
            }
        }

        public static string GetModifiers(MethodModel model)
        {
            string type = null;
            type += model.Modifiers.Item1.ToString().ToLower() + " ";
            type += model.Modifiers.Item2 == AbstractEnum.Abstract ? AbstractEnum.Abstract.ToString().ToLower() + " " : "";
            type += model.Modifiers.Item3 == StaticEnum.Static ? StaticEnum.Static.ToString().ToLower() + " " : "";
            type += model.Modifiers.Item4 == VirtualEnum.Virtual ? VirtualEnum.Virtual.ToString().ToLower() + " " : "";
            return type;
        }
    }
}
