using System;
using System.Collections.ObjectModel;
using BusinessLogic.Model;

namespace BusinessLogic.ViewModel.TreeViewItems
{
    public class TypeTreeItem : TreeViewItem
    {
        public TypeModel TypeData { get; set; }
        public TypeTreeItem(TypeModel typeModel, ItemTypeEnum type) : base(GetModifiers(typeModel) + typeModel.Name, type)
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
                children.Add(new TypeTreeItem(TypeModel.TypeDictionary[TypeData.BaseType.Name], ItemTypeEnum.BaseType));
            }
            if (TypeData.DeclaringType != null)
            {
                children.Add(new TypeTreeItem(TypeModel.TypeDictionary[TypeData.DeclaringType.Name], ItemTypeEnum.Type));
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
                    children.Add(new ParameterTreeItem(parameterModel, ItemTypeEnum.Field));
                }
            }
            if (TypeData.GenericArguments != null)
            {
                foreach (TypeModel typeModel in TypeData.GenericArguments)
                {
                    children.Add(new TypeTreeItem(TypeModel.TypeDictionary[typeModel.Name], ItemTypeEnum.GenericArgument));
                }
            }
            if (TypeData.ImplementedInterfaces != null)
            {
                foreach (TypeModel typeModel in TypeData.ImplementedInterfaces)
                {
                    children.Add(new TypeTreeItem(TypeModel.TypeDictionary[typeModel.Name], ItemTypeEnum.InmplementedInterface));
                }
            }
            if (TypeData.NestedTypes != null)
            {
                foreach (TypeModel typeModel in TypeData.NestedTypes)
                {
                    ItemTypeEnum type = typeModel.Type == TypeEnum.Class ? ItemTypeEnum.NestedClass :
                        typeModel.Type == TypeEnum.Struct ? ItemTypeEnum.NestedStructure :
                        typeModel.Type == TypeEnum.Enum ? ItemTypeEnum.NestedEnum : ItemTypeEnum.NestedType;
                    children.Add(new TypeTreeItem(TypeModel.TypeDictionary[typeModel.Name], type));
                }
            }
            if (TypeData.Methods != null)
            {
                foreach (MethodModel methodModel in TypeData.Methods)
                {
                    children.Add(new MethodTreeItem(methodModel, methodModel.Extension ? ItemTypeEnum.ExtensionMethod : ItemTypeEnum.Method));
                }
            }
            if (TypeData.Constructors != null)
            {
                foreach (MethodModel methodModel in TypeData.Constructors)
                {
                    children.Add(new MethodTreeItem(methodModel, ItemTypeEnum.Constructor));
                }
            }
        }
    }
}
