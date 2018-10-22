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
                case ItemTypeEnum.Constructor:                               
                case ItemTypeEnum.ExtensionMethod:
                case ItemTypeEnum.Field:
                case ItemTypeEnum.GenericArgument:
                case ItemTypeEnum.Parameter:
                case ItemTypeEnum.Type:
                case ItemTypeEnum.Method:
                    return new SolidColorBrush(Colors.Cyan);

                case ItemTypeEnum.NestedEnum:
                case ItemTypeEnum.Enum:
                case ItemTypeEnum.InmplementedInterface:
                case ItemTypeEnum.Interface:
                    return new SolidColorBrush(Colors.DarkSeaGreen);
                case ItemTypeEnum.Namespace:
                case ItemTypeEnum.Property:              
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
