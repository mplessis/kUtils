using System;
using Windows.UI.Xaml.Data;
using Kopigi.Portable.Class;

namespace Kopigi.Portable.Converters.Format
{
    /// <summary>
    /// Returns String.Format (in uppercase) method result - use format string set in parameter.
    /// Réalise la même chose que le StringFormatConverter mais retourne le résultat tout en majuscules.
    /// </summary>
    public class StringFormatUCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter == null)
            {
                return value;
            }
            return StringFormatUseCase.Format(true, (String)parameter, value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
