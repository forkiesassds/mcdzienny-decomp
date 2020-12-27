using MCDzienny.Settings;

namespace MCDzienny
{
    // Token: 0x02000080 RID: 128
    internal class CmdWomid : Command
    {
        // Token: 0x170000FC RID: 252
        // (get) Token: 0x0600035E RID: 862 RVA: 0x00012640 File Offset: 0x00010840
        public override string name
        {
            get { return "womid"; }
        }

        // Token: 0x170000FD RID: 253
        // (get) Token: 0x0600035F RID: 863 RVA: 0x00012648 File Offset: 0x00010848
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170000FE RID: 254
        // (get) Token: 0x06000360 RID: 864 RVA: 0x00012650 File Offset: 0x00010850
        public override string type
        {
            get { return ""; }
        }

        // Token: 0x170000FF RID: 255
        // (get) Token: 0x06000361 RID: 865 RVA: 0x00012658 File Offset: 0x00010858
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000100 RID: 256
        // (get) Token: 0x06000362 RID: 866 RVA: 0x0001265C File Offset: 0x0001085C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x17000101 RID: 257
        // (get) Token: 0x06000363 RID: 867 RVA: 0x00012660 File Offset: 0x00010860
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06000364 RID: 868 RVA: 0x00012664 File Offset: 0x00010864
        public override void Use(Player p, string message)
        {
            if (message.Length >= 5)
            {
                if (GeneralSettings.All.KickWomUsers && !message.ToLower().Contains("xwom") &&
                    message.ToLower().Contains("wom")) p.Kick("Upgrade your WOM client to XWOM!");
                if (message.Substring(message.Length - 5) == "2.0.5" || message.Substring(message.Length - 5) == "2.0.6"
                ) p.Kick("Upgrade your WOM client to the newest version!");
                p.IsUsingWom = true;
            }
        }

        // Token: 0x06000365 RID: 869 RVA: 0x00012700 File Offset: 0x00010900
        public override void Help(Player p)
        {
        }
    }
}