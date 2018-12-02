﻿using System;
using System.Collections.ObjectModel;
using BusinessLogic.Model.Assembly;
using BusinessLogic.Model.Enums;

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
            string type = String.Empty;
            type += MethodModel.Modifiers.AccessLevel.ToString().ToLower() + " ";
            type += MethodModel.Modifiers.AbstractEnum == AbstractEnum.Abstract ? AbstractEnum.Abstract.ToString().ToLower() + " " : String.Empty;
            type += MethodModel.Modifiers.StaticEnum == StaticEnum.Static ? StaticEnum.Static.ToString().ToLower() + " " : String.Empty;
            type += MethodModel.Modifiers.VirtualEnum == VirtualEnum.Virtual ? VirtualEnum.Virtual.ToString().ToLower() + " " : String.Empty;
            type += MethodModel.ReturnType != null ? MethodModel.ReturnType.Name +" " : String.Empty;
            type += MethodModel.Name;
            type += MethodModel.Extension ? " :Extension method" : String.Empty;
            return type;
        }
    }
}