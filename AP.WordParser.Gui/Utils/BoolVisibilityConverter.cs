using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AP.WordParser.Gui.Utils
{
    internal class BoolVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool input)
            {
                if (parameter?.ToString() == "Inverted")
                {
                    input = !input;
                }
                return input ? Visibility.Collapsed : Visibility.Visible;
            }

            return default;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}