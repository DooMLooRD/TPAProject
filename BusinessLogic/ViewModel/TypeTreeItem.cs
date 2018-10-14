using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Model;

namespace BusinessLogic.ViewModel
{
    public class TypeTreeItem : BaseTreeViewModel, ITreeViewItemBuilder
    {
        public TypeModel TypeData { get; set; }
        public TypeTreeItem(TypeModel typeModel) : base(typeModel.Name)
        {
            TypeData = typeModel;
        }

        public void BuildTreeView(ObservableCollection<TreeViewItem> children)
        {
            if (TypeData.BaseType != null)
            {
                children.Add(new TreeViewItem(TypeData.BaseType.Name, ItemTypeEnum.Type, new TypeTreeItem(TypeModel.TypeDictionary[TypeData.BaseType.Name])));
            }
            if (TypeData.DeclaringType != null)
            {
                children.Add(new TreeViewItem(TypeData.DeclaringType.Name, ItemTypeEnum.Type, new TypeTreeItem(TypeModel.TypeDictionary[TypeData.DeclaringType.Name])));
            }
            if (TypeData.Properties != null)
            {
                foreach (PropertyModel propertyModel in TypeData.Properties)
                {
                    children.Add(new TreeViewItem(GetModifiers(propertyModel.Type) + propertyModel.Type.Name+" "+ propertyModel.Name, ItemTypeEnum.Property, new PropertyTreeItem(propertyModel)));
                }
            }
            if (TypeData.Fields != null)
            {
                foreach (ParameterModel parameterModel in TypeData.Fields)
                {
                    children.Add(new TreeViewItem(parameterModel.Name, ItemTypeEnum.Field, new ParameterTreeItem(parameterModel)));
                }
            }
            if (TypeData.GenericArguments != null)
            {
                foreach (TypeModel typeModel in TypeData.GenericArguments)
                {
                    children.Add(new TreeViewItem(GetModifiers(typeModel) + typeModel.Name, ItemTypeEnum.GenericArgument, new TypeTreeItem(TypeModel.TypeDictionary[typeModel.Name])));
                }
            }
            if (TypeData.ImplementedInterfaces != null)
            {
                foreach (TypeModel typeModel in TypeData.ImplementedInterfaces)
                {
                    children.Add(new TreeViewItem(GetModifiers(typeModel) + typeModel.Name, ItemTypeEnum.InmplementedInterface, new TypeTreeItem(TypeModel.TypeDictionary[typeModel.Name])));
                }
            }
            if (TypeData.NestedTypes != null)
            {
                foreach (TypeModel typeModel in TypeData.NestedTypes)
                {
                    children.Add(new TreeViewItem(GetModifiers(typeModel) + typeModel.Name, ItemTypeEnum.NestedType, new TypeTreeItem(TypeModel.TypeDictionary[typeModel.Name])));
                }
            }
            if (TypeData.Methods != null)
            {
                foreach (MethodModel methodModel in TypeData.Methods)
                {
                    children.Add(new TreeViewItem(MethodTreeItem.GetModifiers(methodModel) + methodModel.Name, ItemTypeEnum.Method, new MethodTreeItem(methodModel)));
                }
            }
            if (TypeData.Constructors != null)
            {
                foreach (MethodModel methodModel in TypeData.Constructors)
                {
                    children.Add(new TreeViewItem(MethodTreeItem.GetModifiers(methodModel) + methodModel.Name, ItemTypeEnum.Constructor, new MethodTreeItem(methodModel)));
                }
            }
        }

        public static string GetModifiers(TypeModel model)
        {
            if (model.Modifiers != null)
            {
                string type = null;
                type += model.Modifiers.Item1.ToString().ToLower() + " ";
                type += model.Modifiers.Item2 == SealedEnum.Sealed ? SealedEnum.Sealed.ToString().ToLower() + " " : "";
                type += model.Modifiers.Item3 == AbstractEnum.Abstract ? AbstractEnum.Abstract.ToString().ToLower() + " " : "";
                return type;
            }

            return null;
        }
    }
}
