using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using BusinessLogic.ViewModel.TreeViewItems;

namespace WPFApplication.Converters
{
    [ValueConversion(typeof(ItemTypeEnum), typeof(Brush))]
    public class ItemTypeToBrushConverter : IValueConverter
    {
        public static ItemTypeToBrushConverter Instance = new ItemTypeToBrushConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            switch ((ItemTypeEnum)value)
            {
                case ItemTypeEnum.Assembly:
                    return new SolidColorBrush(Colors.DarkBlue);
                
                case ItemTypeEnum.Namespace:
                    return new SolidColorBrush(Colors.CornflowerBlue);

                case ItemTypeEnum.Constructor:
                    return new SolidColorBrush(Colors.CadetBlue);

                case ItemTypeEnum.ExtensionMethod:
                    return new SolidColorBrush(Colors.Purple);

                case ItemTypeEnum.GenericArgument:
                    return new SolidColorBrush(Colors.Plum);

                case ItemTypeEnum.Field:
                case ItemTypeEnum.Parameter:
                case ItemTypeEnum.Property:
                    return new SolidColorBrush(Colors.MediumPurple);

                case ItemTypeEnum.Method:
                    return new SolidColorBrush(Colors.DarkViolet);

                case ItemTypeEnum.NestedEnum:
                case ItemTypeEnum.Enum:
                case ItemTypeEnum.InmplementedInterface:
                case ItemTypeEnum.Interface:
                    return new SolidColorBrush(Colors.DarkSeaGreen);
                
                case ItemTypeEnum.Type:
                case ItemTypeEnum.ReturnType:
                case ItemTypeEnum.NestedType:
                    return new SolidColorBrush(Colors.DodgerBlue);

                case ItemTypeEnum.BaseType:
                case ItemTypeEnum.NestedClass:
                case ItemTypeEnum.Class:
                case ItemTypeEnum.Struct:
                case ItemTypeEnum.NestedStructure:
                    return new SolidColorBrush(Colors.Teal);                
                default:
                    return new SolidColorBrush(Colors.Black);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
