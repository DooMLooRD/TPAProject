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
    [ValueConversion(typeof(TreeViewItem), typeof(ConsoleColor))]
    public class ItemTypeToConsoleColorConverter : IValueConverter
    {
        public static ItemTypeToConsoleColorConverter Instance = new ItemTypeToConsoleColorConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type type = value?.GetType();
            return type == typeof(AssemblyTreeItem) ? ConsoleColor.DarkBlue :
                type == typeof(MethodTreeItem) ? ConsoleColor.Magenta :
                type == typeof(NamespaceTreeItem) ? ConsoleColor.Blue :
                type == typeof(ParameterTreeItem) ? ConsoleColor.DarkCyan :
                type == typeof(PropertyTreeItem) ? ConsoleColor.DarkGreen :
                type == typeof(TypeTreeItem) ? ConsoleColor.DarkMagenta :
                ConsoleColor.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
