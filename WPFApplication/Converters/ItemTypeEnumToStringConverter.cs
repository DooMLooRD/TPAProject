using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using ViewModels.TreeViewItems;


namespace WPFApplication.Converters
{
    [ValueConversion(typeof(TreeViewItem), typeof(string))]
    public class ItemTypeEnumToStringConverter : IValueConverter
    {
        public static ItemTypeEnumToStringConverter Instance = new ItemTypeEnumToStringConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type type = value?.GetType();
            string typeString = type == typeof(AssemblyTreeItem) ? "Assembly" :
                type == typeof(MethodTreeItem) ? "Method" :
                type == typeof(NamespaceTreeItem) ? "Namespace" :
                type == typeof(ParameterTreeItem) ? "Parameter" :
                type == typeof(PropertyTreeItem) ? "Property" :
                type == typeof(TypeTreeItem) ? "Type" : "";
            return '[' + typeString + ']';
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
