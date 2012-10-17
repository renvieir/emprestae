using System;
using System.Windows.Data;
using System.Globalization;

namespace INdT.Views.Converters
{
    public class VersionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double version = (double)value;
            return string.Format(CultureInfo.InvariantCulture, "{0:.0}", version);

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
