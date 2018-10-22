using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using BusinessLogic.ViewModel;
using BusinessLogic.ViewModel.TreeViewItems;


namespace WPFApplication.Converters
{
    [ValueConversion(typeof(ItemTypeEnum), typeof(string))]
    public class ItemTypeEnumToStringConverter : IValueConverter
    {
        public static ItemTypeEnumToStringConverter Instance = new ItemTypeEnumToStringConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "["+(ItemTypeEnum)value+"]";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
