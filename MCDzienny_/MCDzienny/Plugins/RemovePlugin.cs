using System.Windows.Forms;

namespace MCDzienny.Plugins
{
    public class RemovePlugin : Plugin
    {

        readonly string myAuthor = "Dzienny";

        readonly string myDescription = "A plugin that allows you to remove plugins.";

        readonly UserControl myMainInterface = new PluginControlRemovePlugin();
        readonly string myName = "Remove Plugin";

        readonly string myVersion = "1.0";

        readonly int versionNumber = 1;

        public override string Description { get { return myDescription; } }

        public override string Author { get { return myAuthor; } }

        public override string Name { get { return myName; } }

        public override UserControl MainInterface { get { return myMainInterface; } }

        public override string Version { get { return myVersion; } }

        public override int VersionNumber { get { return versionNumber; } }

        public override void Initialize() {}

        public override void Terminate() {}
    }
}