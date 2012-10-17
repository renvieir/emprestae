using System;
using System.Windows.Data;
using Microsoft.Phone.Controls;
using System.Diagnostics;
using System.Globalization;

namespace INdT.Views.Converters
{
    public class ApplicationOrientationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            PageOrientation orientation = (PageOrientation)value;
            switch (orientation)
            {
                case PageOrientation.Portrait:
                case PageOrientation.PortraitDown:
                case PageOrientation.PortraitUp:
                    {
                        if (CultureInfo.CurrentCulture.Name == "pt-BR")
                        {
                            return "Este App funciona apenas na orientação vertical.";
                        }
                        else if (CultureInfo.CurrentCulture.Name.Contains("es"))
                        {
                            return "Esta aplicación sólo funciona en orientación vertical.";
                        }
                        else
                        {
                            return "This App will only run on portrait mode.";
                        }
                    }
                case PageOrientation.Landscape:
                case PageOrientation.LandscapeLeft:
                case PageOrientation.LandscapeRight:
                    return "Este App funciona apenas na orientação horizontal.";
                default:
                    return "Este App funciona na orientação vertical e horizontal.";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
