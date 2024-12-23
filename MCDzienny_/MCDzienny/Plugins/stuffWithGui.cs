using System;
using System.Windows.Forms;

namespace MCDzienny.Plugins
{
    public class stuffWithGui
    {

        public TextBox intervalTextBox;

        public TextBox msgTextBox;

        public string stringOfmsgTextBox;
        public Timer timer;

        public stuffWithGui(Timer timerOfTextbox, TextBox text1, TextBox interval)
        {
            timer = timerOfTextbox;
            msgTextBox = text1;
            intervalTextBox = interval;
        }

        public void setInterval(int interval, bool turnOn)
        {
            timer.Interval = interval;
            if (turnOn)
            {
                timer.Enabled = true;
            }
        }

        public void enableTimer(bool onOff)
        {
            if (onOff)
            {
                timer.Enabled = true;
            }
            else
            {
                timer.Enabled = false;
            }
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            displayText();
        }

        public bool displayText(bool dontDoThat = false)
        {
            if (!dontDoThat)
            {
                return true;
            }
            return false;
        }

        void msgTextBox_TextChanged(object sender, EventArgs e)
        {
            stringOfmsgTextBox = msgTextBox.Text;
        }
    }
}