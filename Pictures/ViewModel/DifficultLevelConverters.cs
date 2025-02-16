using System.Globalization;
using System.Windows.Data;

namespace Pictures.ViewModel
{
    public class DifficultLevelConverters : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is BoardViewModel.DifficultLevels actualDifficultyLevel &&
                parameter is BoardViewModel.DifficultLevels difficultyLevel)
            {
                return actualDifficultyLevel == difficultyLevel;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
