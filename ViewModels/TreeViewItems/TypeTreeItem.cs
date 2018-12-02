using System;
using System.Collections.ObjectModel;
using BusinessLogic.Model.Assembly;
using BusinessLogic.Model.Enums;

namespace ViewModels.TreeViewItems
{
    public class TypeTreeItem : TreeViewItem
    {
        public TypeModel TypeData { get; set; }
        public TypeTreeItem(TypeModel typeModel)
        {
            TypeData = typeModel;
        }

        protected override void BuildTreeView(ObservableCollection<TreeViewItem> children)
        {
            if (TypeData.BaseType != null)
            {
                children.Add(new TypeTreeItem(TypeData.BaseType));
            }
            if (TypeData.DeclaringType != null)
            {
                children.Add(new TypeTreeItem(TypeData.DeclaringType));
            }
            if (TypeData.Properties != null)
            {
                foreach (PropertyModel propertyModel in TypeData.Properties)
                {
                    children.Add(new PropertyTreeItem(propertyModel));
                }
            }
            if (TypeData.Fields != null)
            {
                foreach (ParameterModel parameterModel in TypeData.Fields)
                {
                    children.Add(new ParameterTreeItem(parameterModel));
                }
            }
            if (TypeData.GenericArguments != null)
            {
                foreach (TypeModel typeModel in TypeData.GenericArguments)
                {
                    children.Add(new TypeTreeItem(typeModel));
                }
            }
            if (TypeData.ImplementedInterfaces != null)
            {
                foreach (TypeModel typeModel in TypeData.ImplementedInterfaces)
                {
                    children.Add(new TypeTreeItem(typeModel));
                }
            }
            if (TypeData.NestedTypes != null)
            {
                foreach (TypeModel typeModel in TypeData.NestedTypes)
                {
                    children.Add(new TypeTreeItem(typeModel));
                }
            }
            if (TypeData.Methods != null)
            {
                foreach (MethodModel methodModel in TypeData.Methods)
                {
                    children.Add(new MethodTreeItem(methodModel));
                }
            }
            if (TypeData.Constructors != null)
            {
                foreach (MethodModel methodModel in TypeData.Constructors)
                {
                    children.Add(new MethodTreeItem(methodModel));
                }
            }
        }
        public override string ToString()
        {
            string type = String.Empty;
            if (TypeData.Modifiers != null)
            {

                type += TypeData.Modifiers.AccessLevel.ToString().ToLower() + " ";
                type += TypeData.Modifiers.SealedEnum == SealedEnum.Sealed ? SealedEnum.Sealed.ToString().ToLower() + " " : String.Empty;
                type += TypeData.Modifiers.AbstractEnum == AbstractEnum.Abstract ? AbstractEnum.Abstract.ToString().ToLower() + " " : String.Empty;
                type += TypeData.Modifiers.StaticEnum == StaticEnum.Static ? StaticEnum.Static.ToString().ToLower() + " " : String.Empty;

            }
            type += TypeData.Type != TypeEnum.None ? TypeData.Type.ToString().ToLower() + " " : String.Empty;
            type += TypeData.Name;
            if (TypeData.IsGeneric)
                type += " - generic type";
            else if (TypeData.IsExternal)
                type += " - external assembly: " + TypeData.AssemblyName;

            return type;
        }
    }
}
