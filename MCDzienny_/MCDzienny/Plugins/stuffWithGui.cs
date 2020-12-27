using System;
using System.Windows.Forms;

namespace MCDzienny.Plugins
{
    // Token: 0x020001F4 RID: 500
    public class stuffWithGui
    {
        // Token: 0x04000742 RID: 1858
        public TextBox intervalTextBox;

        // Token: 0x04000741 RID: 1857
        public TextBox msgTextBox;

        // Token: 0x04000743 RID: 1859
        public string stringOfmsgTextBox;

        // Token: 0x04000740 RID: 1856
        public Timer timer;

        // Token: 0x06000DD1 RID: 3537 RVA: 0x0004DD24 File Offset: 0x0004BF24
        public stuffWithGui(Timer timerOfTextbox, TextBox text1, TextBox interval)
        {
            timer = timerOfTextbox;
            msgTextBox = text1;
            intervalTextBox = interval;
        }

        // Token: 0x06000DD2 RID: 3538 RVA: 0x0004DD44 File Offset: 0x0004BF44
        public void setInterval(int interval, bool turnOn)
        {
            timer.Interval = interval;
            if (turnOn) timer.Enabled = true;
        }

        // Token: 0x06000DD3 RID: 3539 RVA: 0x0004DD64 File Offset: 0x0004BF64
        public void enableTimer(bool onOff)
        {
            if (onOff)
            {
                timer.Enabled = true;
                return;
            }

            timer.Enabled = false;
        }

        // Token: 0x06000DD4 RID: 3540 RVA: 0x0004DD84 File Offset: 0x0004BF84
        public void timer_Tick(object sender, EventArgs e)
        {
            displayText();
        }

        // Token: 0x06000DD5 RID: 3541 RVA: 0x0004DD90 File Offset: 0x0004BF90
        public bool displayText(bool dontDoThat = false)
        {
            return !dontDoThat;
        }

        // Token: 0x06000DD6 RID: 3542 RVA: 0x0004DD98 File Offset: 0x0004BF98
        private void msgTextBox_TextChanged(object sender, EventArgs e)
        {
            stringOfmsgTextBox = msgTextBox.Text;
        }
    }
}