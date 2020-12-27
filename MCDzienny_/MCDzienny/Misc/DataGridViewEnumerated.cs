using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MCDzienny.Misc
{
    // Token: 0x020001B8 RID: 440
    internal class DataGridViewEnumerated : DataGridView
    {
        // Token: 0x06000C83 RID: 3203 RVA: 0x00048ADC File Offset: 0x00046CDC
        protected override void OnRowPostPaint(DataGridViewRowPostPaintEventArgs e)
        {
            base.OnRowPostPaint(e);
            var length = RowCount.ToString().Length;
            var stringBuilder = new StringBuilder(length);
            stringBuilder.Append(e.RowIndex + 1);
            var sizeF = e.Graphics.MeasureString(stringBuilder.ToString(), Font);
            if (RowHeadersWidth < (int) (sizeF.Width + 20f)) RowHeadersWidth = (int) (sizeF.Width + 20f);
            e.Graphics.DrawString(stringBuilder.ToString(), Font, SystemBrushes.ControlText,
                e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + (e.RowBounds.Height - sizeF.Height) / 2f);
        }
    }
}