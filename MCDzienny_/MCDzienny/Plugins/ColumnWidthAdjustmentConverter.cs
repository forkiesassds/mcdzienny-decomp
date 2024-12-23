using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace MCDzienny.Plugins
{
    public class ColumnWidthAdjustmentConverter : IValueConverter
    {
        public readonly double WidthDelta = -210.0;

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