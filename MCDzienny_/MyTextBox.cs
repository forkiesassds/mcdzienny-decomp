using System;
using System.Drawing;
using System.Windows.Forms;

public class MyTextBox : TextBox
{
    public MyTextBox()
    {
        //IL_0017: Unknown result type (might be due to invalid IL or missing references)
        SetStyle((ControlStyles)2, true);
        BorderStyle = 0;
        Margin = new Padding(5);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        HighlightControl(e.Graphics);
        base.OnPaint(e);
    }

    void HighlightControl(Graphics graphics)
    {
        for (int i = 1; i <= Lines.Length; i++)
        {
            graphics.DrawString(i.ToString(), Font, SystemBrushes.ControlText, Location.X, ClientRectangle.Top + this.FontHeight * (i - 1));
        }
        ControlPaint.DrawBorder(graphics, DisplayRectangle, Color.LawnGreen, (ButtonBorderStyle)3);
    }

    protected override void OnTextChanged(EventArgs e)
    {
        base.OnTextChanged(e);
        Invalidate();
    }
}