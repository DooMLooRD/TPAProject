using System;
using System.Collections.ObjectModel;
using BusinessLogic.Model;
using BusinessLogic.Reflection;

namespace BusinessLogic.ViewModel.TreeViewItems
{
    public class TypeTreeItem : TreeViewItem
    {
        public TypeModel TypeData { get; set; }
        public TypeTreeItem(TypeModel typeModel) : base(GetModifiers(typeModel) + typeModel.Name)
        {
            TypeData = typeModel;
        }


        public static string GetModifiers(TypeModel model)
        {
            if (model.Modifiers != null)
            {
                string type = null;
                type += model.Modifiers.Item1.ToString().ToLower() + " ";
                type += model.Modifiers.Item2 == SealedEnum.Sealed ? SealedEnum.Sealed.ToString().ToLower() + " " : String.Empty;
                type += model.Modifiers.Item3 == AbstractEnum.Abstract ? AbstractEnum.Abstract.ToString().ToLower() + " " : String.Empty;
                type += model.Modifiers.Item4 == StaticEnum.Static ? StaticEnum.Static.ToString().ToLower() + " " : String.Empty;
                return type;
            }

            return null;
        }

        protected override void BuildTreeView(ObservableCollection<TreeViewItem> children)
        {
            if (TypeData.BaseType != null)
            {
                children.Add(new TypeTreeItem(DictionaryTypeSingleton.Instance.Get(TypeData.BaseType.Name)));
            }
            if (TypeData.DeclaringType != null)
            {
                children.Add(new TypeTreeItem(DictionaryTypeSingleton.Instance.Get(TypeData.DeclaringType.Name)));
            }
            if (TypeData.Properties != null)
            {
                foreach (PropertyModel propertyModel in TypeData.Properties)
                {
                    children.Add(new PropertyTreeItem(propertyModel, GetModifiers(propertyModel.Type) + propertyModel.Type.Name + " " + propertyModel.Name));
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
                    children.Add(new TypeTreeItem(DictionaryTypeSingleton.Instance.Get(typeModel.Name)));
                }
            }
            if (TypeData.ImplementedInterfaces != null)
            {
                foreach (TypeModel typeModel in TypeData.ImplementedInterfaces)
                {
                    children.Add(new TypeTreeItem(DictionaryTypeSingleton.Instance.Get(typeModel.Name)));
                }
            }
            if (TypeData.NestedTypes != null)
            {
                foreach (TypeModel typeModel in TypeData.NestedTypes)
                {
                    children.Add(new TypeTreeItem(DictionaryTypeSingleton.Instance.Get(typeModel.Name)));
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
    }
}
