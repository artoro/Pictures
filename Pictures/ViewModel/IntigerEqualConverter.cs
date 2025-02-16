using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Pictures.ViewModel
{
    public class IntigerEqualConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 4 &&
                values[0] is int value1 &&
                values[1] is int value2 &&
                values[2] is SolidColorBrush brushTrue &&
                values[3] is SolidColorBrush brushFalse)
            {
                return value1 == value2 ? brushTrue : brushFalse;
            }
            return Brushes.White;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
