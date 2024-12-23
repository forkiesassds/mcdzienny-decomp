namespace MCDzienny.Plugins
{
    public class versionInfo
    {

        public versionInfo(string nameOfAssembly, string description, string author, string version, int versionNumber)
        {
            name = nameOfAssembly;
            descr = description;
            auth = author;
            ver = version;
            verNumber = versionNumber;
        }
        public string name { get; set; }

        public string descr { get; set; }

        public string auth { get; set; }

        public string ver { get; set; }

        public int verNumber { get; set; }
    }
}