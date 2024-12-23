using System.ComponentModel;
using System.Windows.Forms;

namespace StarterMain
{
	public class Form1 : Form
	{
		private IContainer components;

		public Form1()
		{
			InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			components = new Container();
            AutoScaleMode = (AutoScaleMode)1;
            Text = "Form1";
		}
	}
}
