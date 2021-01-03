using System;
using System.Globalization;
using System.Windows.Data;

namespace MCDzienny.Plugins
{
    // Token: 0x0200004B RID: 75
    public class WidthAdjustmentConverter : IValueConverter
    {
        // Token: 0x1700006F RID: 111
        // (get) Token: 0x060001B2 RID: 434 RVA: 0x00009A78 File Offset: 0x00007C78
        // (set) Token: 0x060001B3 RID: 435 RVA: 0x00009A80 File Offset: 0x00007C80
        public double WidthDelta { get; set; }

        // Token: 0x060001B4 RID: 436 RVA: 0x00009A8C File Offset: 0x00007C8C
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double) value + WidthDelta;
        }

        // Token: 0x060001B5 RID: 437 RVA: 0x00009AA0 File Offset: 0x00007CA0
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double) value - WidthDelta;
        }
    }
}