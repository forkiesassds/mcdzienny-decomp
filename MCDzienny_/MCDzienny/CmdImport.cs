using System.IO;
using MCDzienny.Misc;

namespace MCDzienny
{
    public class CmdImport : Command
    {
        static readonly string ImportDirectory = "extra/import";

        public override string name { get { return "import"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            DirectoryUtil.CreateIfNotExists(ImportDirectory);
            string path = ImportDirectory + "/" + message + ".dat";
            if (!File.Exists(path))
            {
                Player.SendMessage(p, "Could not find .dat file");
                return;
            }
            using (FileStream lvlStream = File.OpenRead(path))
            {
                if (ConvertDat.Load(lvlStream, message) == null)
                {
                    Player.SendMessage(p, "The map conversion failed.");
                    return;
                }
                Player.SendMessage(p, "Converted map!");
            }
            all.Find("load").Use(p, message);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/import [.dat file] - Imports the .dat file given");
            Player.SendMessage(p, ".dat files should be located in the /extra/import/ folder");
        }
    }
}