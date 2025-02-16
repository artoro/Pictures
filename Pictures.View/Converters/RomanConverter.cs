using System.Globalization;
using System.Windows.Data;

namespace Pictures.View.Converters
{
    public class RomanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int number)
            {
                if (number < 1 || number > 3999)
                {
                    throw new ArgumentOutOfRangeException(nameof(number), "Number must be between 1 to 3999");
                }

                string[] thousands = { "", "M", "MM", "MMM" };
                string[] hundreds = { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" };
                string[] tens = { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" };
                string[] ones = { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" };

                int thousandsDigit = number / 1000;
                int hundredsDigit = (number % 1000) / 100;
                int tensDigit = (number % 100) / 10;
                int onesDigit = number % 10;

                return thousands[thousandsDigit] + hundreds[hundredsDigit] + tens[tensDigit] + ones[onesDigit];
            }
            else throw new ArgumentException("Value is not an integer", nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
