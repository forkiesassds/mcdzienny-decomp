using MCDzienny.Cpe;

namespace MCDzienny
{
    // Token: 0x02000013 RID: 19
    public class CmdExample : Command
    {
        // Token: 0x17000018 RID: 24
        // (get) Token: 0x0600006B RID: 107 RVA: 0x00004CC4 File Offset: 0x00002EC4
        public override string name
        {
            get { return "example"; }
        }

        // Token: 0x17000019 RID: 25
        // (get) Token: 0x0600006C RID: 108 RVA: 0x00004CCC File Offset: 0x00002ECC
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700001A RID: 26
        // (get) Token: 0x0600006D RID: 109 RVA: 0x00004CD4 File Offset: 0x00002ED4
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x1700001B RID: 27
        // (get) Token: 0x0600006E RID: 110 RVA: 0x00004CDC File Offset: 0x00002EDC
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700001C RID: 28
        // (get) Token: 0x0600006F RID: 111 RVA: 0x00004CE0 File Offset: 0x00002EE0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x1700001D RID: 29
        // (get) Token: 0x06000070 RID: 112 RVA: 0x00004CE4 File Offset: 0x00002EE4
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06000071 RID: 113 RVA: 0x00004CE8 File Offset: 0x00002EE8
        public override void Use(Player p, string message)
        {
            if (p.Cpe.EnvWeatherType == 1)
            {
                V1.EnvSetWeatherType(p, 1);
                return;
            }

            Player.SendMessage(p, "Sorry, this command isn't compatible with your game client.");
        }

        // Token: 0x06000072 RID: 114 RVA: 0x00004D0C File Offset: 0x00002F0C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "example - changes the weather.");
        }
    }
}