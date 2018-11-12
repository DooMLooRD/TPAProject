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
    [ValueConversion(typeof(TreeViewItem), typeof(Brush))]
    public class ItemTypeToBrushConverter : IValueConverter
    {
        public static ItemTypeToBrushConverter Instance = new ItemTypeToBrushConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type type = value?.GetType();
            return type == typeof(AssemblyTreeItem) ? new SolidColorBrush(Colors.DarkBlue) :
                type == typeof(MethodTreeItem) ? new SolidColorBrush(Colors.DarkViolet) :
                type == typeof(NamespaceTreeItem) ? new SolidColorBrush(Colors.CornflowerBlue) :
                type == typeof(ParameterTreeItem) ? new SolidColorBrush(Colors.MediumPurple) :
                type == typeof(PropertyTreeItem) ? new SolidColorBrush(Colors.DarkSeaGreen) :
                type == typeof(TypeTreeItem) ? new SolidColorBrush(Colors.Teal) :
                new SolidColorBrush(Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
