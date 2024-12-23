using System.Windows.Forms;

namespace MCDzienny.Plugins
{
    public abstract class Plugin
    {
        public abstract string Name { get; }

        public abstract string Description { get; }

        public abstract string Author { get; }

        public abstract string Version { get; }

        public abstract int VersionNumber { get; }

        public abstract UserControl MainInterface { get; }

        public abstract void Initialize();

        public abstract void Terminate();
    }
}