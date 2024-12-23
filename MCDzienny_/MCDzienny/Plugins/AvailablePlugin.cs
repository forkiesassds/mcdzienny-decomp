namespace MCDzienny.Plugins
{
    public class AvailablePlugin
    {

        string myAssemblyPath = string.Empty;

        public Plugin Instance { get; set; }

        public string AssemblyPath { get { return myAssemblyPath; } set { myAssemblyPath = value; } }

        public bool IsCore { get; set; }
    }
}