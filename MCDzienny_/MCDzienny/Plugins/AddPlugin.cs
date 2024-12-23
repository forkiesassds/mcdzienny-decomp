using System.Windows.Forms;

namespace MCDzienny.Plugins
{
    public class AddPlugin : Plugin
    {

        readonly string myAuthor = "Dzienny";

        readonly string myDescription = "A plugin that lets you add more plugins.";

        readonly UserControl myMainInterface = new PluginControlAddPlugin();
        readonly string myName = "Add Plugin";

        readonly string myVersion = "1.0";

        public override string Description { get { return myDescription; } }

        public override string Author { get { return myAuthor; } }

        public override string Name { get { return myName; } }

        public override UserControl MainInterface { get { return myMainInterface; } }

        public override string Version { get { return myVersion; } }

        public override int VersionNumber { get { return 1; } }

        public override void Initialize() {}

        public override void Terminate() {}
    }
}