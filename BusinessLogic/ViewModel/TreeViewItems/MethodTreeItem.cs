﻿using System.Collections.ObjectModel;
using BusinessLogic.Model;

namespace BusinessLogic.ViewModel.TreeViewItems
{
    public class MethodTreeItem : ITreeViewItemBuilder
    {
        public string Name { get; set; }
        public MethodModel MethodModel { get; set; }
        public MethodTreeItem(MethodModel methodModel)
        {
            Name = methodModel.Name;
            MethodModel = methodModel;
        }

        public void BuildTreeView(ObservableCollection<TreeViewItem> children)
        {
            if (MethodModel.GenericArguments != null)
            {
                foreach (TypeModel genericArgument in MethodModel.GenericArguments)
                {
                    children.Add(new TreeViewItem(genericArgument.Name, ItemTypeEnum.GenericArgument, new TypeTreeItem(TypeModel.TypeDictionary[genericArgument.Name])));
                }
            }

            if (MethodModel.Parameters != null)
            {
                foreach (ParameterModel parameter in MethodModel.Parameters)
                {
                    children.Add(new TreeViewItem(parameter.Name, ItemTypeEnum.Parameter, new ParameterTreeItem(parameter)));
                }
            }

            if (MethodModel.ReturnType != null)
            {
                children.Add(new TreeViewItem(MethodModel.ReturnType.Name, ItemTypeEnum.ReturnType, new TypeTreeItem(TypeModel.TypeDictionary[MethodModel.ReturnType.Name])));
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