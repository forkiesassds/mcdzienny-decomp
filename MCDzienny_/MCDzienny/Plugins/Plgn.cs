using System.Windows.Forms;

namespace MCDzienny.Plugins
{
    public class Plgn : Plugin
    {

        readonly mainGUI gui = new mainGUI();
        readonly versionInfo info = new versionInfo("Repeater", "A simple text repeater for all your daily messages.(test plugin)", "joppiesaus", "1.0", 1);

        public override string Description { get { return info.descr; } }

        public override string Author { get { return info.auth; } }

        public override string Name { get { return info.name; } }

        public override UserControl MainInterface { get { return gui; } }

        public override string Version { get { return info.ver; } }

        public override int VersionNumber { get { return info.verNumber; } }

        public override void Initialize()
        {
            //IL_0034: Unknown result type (might be due to invalid IL or missing references)
            //IL_003e: Expected O, but got Unknown
            mainGUI mg = gui;
            mg.timer.Start();
            mg.msgTextBox.Invoke((MethodInvoker)delegate { mg.msgTextBox.Text = "My text"; });
        }

        public override void Terminate() {}
    }
}