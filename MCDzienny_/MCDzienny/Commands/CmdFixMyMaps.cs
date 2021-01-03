using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MCDzienny
{
    // Token: 0x0200005E RID: 94
    public class CmdFixMyMaps : Command
    {
        // Token: 0x04000171 RID: 369
        private readonly string MyMapDirectory = "maps" + Path.DirectorySeparatorChar + "mymaps";

        // Token: 0x1700008F RID: 143
        // (get) Token: 0x0600023E RID: 574 RVA: 0x0000CC18 File Offset: 0x0000AE18
        public override string name
        {
            get { return "fixmymap"; }
        }

        // Token: 0x17000090 RID: 144
        // (get) Token: 0x0600023F RID: 575 RVA: 0x0000CC20 File Offset: 0x0000AE20
        public override string shortcut
        {
            get { return "fixmymaps"; }
        }

        // Token: 0x17000091 RID: 145
        // (get) Token: 0x06000240 RID: 576 RVA: 0x0000CC28 File Offset: 0x0000AE28
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000092 RID: 146
        // (get) Token: 0x06000241 RID: 577 RVA: 0x0000CC30 File Offset: 0x0000AE30
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000093 RID: 147
        // (get) Token: 0x06000242 RID: 578 RVA: 0x0000CC34 File Offset: 0x0000AE34
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Nobody; }
        }

        // Token: 0x17000094 RID: 148
        // (get) Token: 0x06000243 RID: 579 RVA: 0x0000CC38 File Offset: 0x0000AE38
        public override bool ConsoleAccess
        {
            get { return true; }
        }

        // Token: 0x06000244 RID: 580 RVA: 0x0000CC3C File Offset: 0x0000AE3C
        public override void Use(Player p, string message)
        {
            var num = 0;
            foreach (var path in GetFolders(MyMapDirectory))
            foreach (var text in GetFolders(path))
            {
                foreach (var text2 in GetFolders(text))
                    if (text2.Length == text2.LastIndexOf(Path.DirectorySeparatorChar) + 4)
                    {
                        num++;
                        var c = text2[text2.Length - 1];
                        Directory.Move(text2, text2.Substring(0, text2.Length - 3) + c);
                    }

                if (text.Length == text.LastIndexOf(Path.DirectorySeparatorChar) + 3)
                {
                    num++;
                    var c2 = text[text.Length - 1];
                    Directory.Move(text, text.Substring(0, text.Length - 2) + c2);
                }
            }

            Player.SendMessage(p, string.Format("All {0} mymaps folders were fixed.", num));
        }

        // Token: 0x06000245 RID: 581 RVA: 0x0000CDE8 File Offset: 0x0000AFE8
        private List<string> GetFolders(string path)
        {
            return Directory.GetDirectories(path).ToList();
        }

        // Token: 0x06000246 RID: 582 RVA: 0x0000CDF8 File Offset: 0x0000AFF8
        public override void Help(Player p)
        {
        }
    }
}