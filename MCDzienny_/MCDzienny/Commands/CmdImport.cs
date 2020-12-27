using System.IO;
using MCDzienny.Misc;

namespace MCDzienny
{
    // Token: 0x02000112 RID: 274
    public class CmdImport : Command
    {
        // Token: 0x040003D1 RID: 977
        private static readonly string ImportDirectory = "extra/import";

        // Token: 0x170003C0 RID: 960
        // (get) Token: 0x06000842 RID: 2114 RVA: 0x0002A55C File Offset: 0x0002875C
        public override string name
        {
            get { return "import"; }
        }

        // Token: 0x170003C1 RID: 961
        // (get) Token: 0x06000843 RID: 2115 RVA: 0x0002A564 File Offset: 0x00028764
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170003C2 RID: 962
        // (get) Token: 0x06000844 RID: 2116 RVA: 0x0002A56C File Offset: 0x0002876C
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x170003C3 RID: 963
        // (get) Token: 0x06000845 RID: 2117 RVA: 0x0002A574 File Offset: 0x00028774
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170003C4 RID: 964
        // (get) Token: 0x06000846 RID: 2118 RVA: 0x0002A578 File Offset: 0x00028778
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x06000847 RID: 2119 RVA: 0x0002A57C File Offset: 0x0002877C
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            DirectoryUtil.CreateIfNotExists(ImportDirectory);
            var path = ImportDirectory + "/" + message + ".dat";
            if (!File.Exists(path))
            {
                Player.SendMessage(p, "Could not find .dat file");
                return;
            }

            using (var fileStream = File.OpenRead(path))
            {
                if (ConvertDat.Load(fileStream, message) == null)
                {
                    Player.SendMessage(p, "The map conversion failed.");
                    return;
                }

                Player.SendMessage(p, "Converted map!");
            }

            all.Find("load").Use(p, message);
        }

        // Token: 0x06000848 RID: 2120 RVA: 0x0002A630 File Offset: 0x00028830
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/import [.dat file] - Imports the .dat file given");
            Player.SendMessage(p, ".dat files should be located in the /extra/import/ folder");
        }
    }
}