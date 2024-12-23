using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MCDzienny.Plugins
{
    public class WidthAdjustmentConverter : IValueConverter
    {
        public double WidthDelta { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value + WidthDelta;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value - WidthDelta;
        }
    }
}