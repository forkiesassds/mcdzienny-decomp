using System;
using System.Globalization;
using System.Windows.Data;

namespace MCDzienny.Plugins
{
    // Token: 0x0200004A RID: 74
    public class ColumnWidthAdjustmentConverter : IValueConverter
    {
        // Token: 0x04000105 RID: 261
        public readonly double WidthDelta = -210.0;

        // Token: 0x060001AF RID: 431 RVA: 0x00009A38 File Offset: 0x00007C38
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double) value + WidthDelta;
        }

        // Token: 0x060001B0 RID: 432 RVA: 0x00009A4C File Offset: 0x00007C4C
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double) value - WidthDelta;
        }
    }
}