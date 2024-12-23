using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MCDzienny.Misc
{
    class DataGridViewEnumerated : DataGridView
    {
        protected override void OnRowPostPaint(DataGridViewRowPostPaintEventArgs e)
        {
            base.OnRowPostPaint(e);
            int length = RowCount.ToString().Length;
            StringBuilder stringBuilder = new StringBuilder(length);
            stringBuilder.Append(e.RowIndex + 1);
            SizeF sizeF = e.Graphics.MeasureString(stringBuilder.ToString(), Font);
            if (RowHeadersWidth < (int)(sizeF.Width + 20f))
            {
                RowHeadersWidth = (int)(sizeF.Width + 20f);
            }
            e.Graphics.DrawString(stringBuilder.ToString(), Font, SystemBrushes.ControlText, e.RowBounds.Location.X + 15,
                                  e.RowBounds.Location.Y + (e.RowBounds.Height - sizeF.Height) / 2f);
        }
    }
}