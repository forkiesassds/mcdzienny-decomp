using System.Windows.Forms;

namespace MCDzienny.Plugins
{
    public class ImportFromDat : Plugin
    {

        readonly string author = "Dzienny";

        readonly string description = "Imports a map that is a dat file and saves it as MCDzienny map type.";

        readonly UserControl gui = new GuiImportFromDat();
        readonly string name = "Import From Dat";

        readonly string version = "1.0";

        readonly int versionNumber = 1;

        public override string Description { get { return description; } }

        public override string Author { get { return author; } }

        public override string Name { get { return name; } }

        public override UserControl MainInterface { get { return gui; } }

        public override string Version { get { return version; } }

        public override int VersionNumber { get { return versionNumber; } }

        public override void Initialize() {}

        public override void Terminate() {}
    }
}