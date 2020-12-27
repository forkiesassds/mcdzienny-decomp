namespace MCDzienny
{
    // Token: 0x02000101 RID: 257
    public class CmdSlap : Command
    {
        // Token: 0x17000381 RID: 897
        // (get) Token: 0x060007DA RID: 2010 RVA: 0x00027CF0 File Offset: 0x00025EF0
        public override string name
        {
            get { return "slap"; }
        }

        // Token: 0x17000382 RID: 898
        // (get) Token: 0x060007DB RID: 2011 RVA: 0x00027CF8 File Offset: 0x00025EF8
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000383 RID: 899
        // (get) Token: 0x060007DC RID: 2012 RVA: 0x00027D00 File Offset: 0x00025F00
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000384 RID: 900
        // (get) Token: 0x060007DD RID: 2013 RVA: 0x00027D08 File Offset: 0x00025F08
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000385 RID: 901
        // (get) Token: 0x060007DE RID: 2014 RVA: 0x00027D0C File Offset: 0x00025F0C
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x17000386 RID: 902
        // (get) Token: 0x060007DF RID: 2015 RVA: 0x00027D10 File Offset: 0x00025F10
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x060007E1 RID: 2017 RVA: 0x00027D1C File Offset: 0x00025F1C
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            var player = Player.Find(message);
            if (player == null)
            {
                Player.SendMessage(p, "Could not find player specified");
                return;
            }

            var x = (ushort) (player.pos[0] / 32);
            var num = (ushort) (player.pos[1] / 32);
            var z = (ushort) (player.pos[2] / 32);
            ushort num2 = 0;
            for (var num3 = num; num3 <= 1000; num3 = (ushort) (num3 + 1))
                if (!Block.Walkthrough(p.level.GetTile(x, num3, z)) && p.level.GetTile(x, num3, z) != byte.MaxValue)
                {
                    num2 = (ushort) (num3 - 1);
                    player.level.ChatLevel(string.Format("{0} was slapped into the roof by {1}",
                        player.color + player.PublicName + Server.DefaultColor, p.color + p.PublicName));
                    break;
                }

            if (num2 == 0)
            {
                player.level.ChatLevel(string.Format("{0} was slapped sky high by {1}",
                    player.color + player.PublicName + Server.DefaultColor, p.color + p.PublicName));
                num2 = 1000;
            }

            player.SendPos(byte.MaxValue, player.pos[0], (ushort) (num2 * 32), player.pos[2], player.rot[0],
                player.rot[1]);
        }

        // Token: 0x060007E2 RID: 2018 RVA: 0x00027E8C File Offset: 0x0002608C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/slap <name> - Slaps <name>, knocking them into the air");
        }
    }
}