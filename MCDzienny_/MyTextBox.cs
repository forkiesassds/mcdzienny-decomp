using System;
using System.Drawing;
using System.Windows.Forms;

// Token: 0x020001E8 RID: 488
public class MyTextBox : TextBox
{
    // Token: 0x06000D89 RID: 3465 RVA: 0x0004CF3C File Offset: 0x0004B13C
    public MyTextBox()
    {
        SetStyle(ControlStyles.UserPaint, true);
        BorderStyle = BorderStyle.None;
        Margin = new Padding(5);
    }

    // Token: 0x06000D8A RID: 3466 RVA: 0x0004CF60 File Offset: 0x0004B160
    protected override void OnPaint(PaintEventArgs e)
    {
        HighlightControl(e.Graphics);
        base.OnPaint(e);
    }

    // Token: 0x06000D8B RID: 3467 RVA: 0x0004CF78 File Offset: 0x0004B178
    private void HighlightControl(Graphics graphics)
    {
        for (var i = 1; i <= Lines.Length; i++)
            graphics.DrawString(i.ToString(), Font, SystemBrushes.ControlText, Location.X,
                ClientRectangle.Top + FontHeight * (i - 1));
        ControlPaint.DrawBorder(graphics, DisplayRectangle, Color.LawnGreen, ButtonBorderStyle.Solid);
    }

    // Token: 0x06000D8C RID: 3468 RVA: 0x0004CFEC File Offset: 0x0004B1EC
    protected override void OnTextChanged(EventArgs e)
    {
        base.OnTextChanged(e);
        Invalidate();
    }
}