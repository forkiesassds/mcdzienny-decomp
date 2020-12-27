using System.IO;

namespace MCDzienny
{
    // Token: 0x02000105 RID: 261
    public class CmdTips : Command
    {
        // Token: 0x17000396 RID: 918
        // (get) Token: 0x060007FB RID: 2043 RVA: 0x0002852C File Offset: 0x0002672C
        public override string name
        {
            get { return "tips"; }
        }

        // Token: 0x17000397 RID: 919
        // (get) Token: 0x060007FC RID: 2044 RVA: 0x00028534 File Offset: 0x00026734
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000398 RID: 920
        // (get) Token: 0x060007FD RID: 2045 RVA: 0x0002853C File Offset: 0x0002673C
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000399 RID: 921
        // (get) Token: 0x060007FE RID: 2046 RVA: 0x00028544 File Offset: 0x00026744
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700039A RID: 922
        // (get) Token: 0x060007FF RID: 2047 RVA: 0x00028548 File Offset: 0x00026748
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x1700039B RID: 923
        // (get) Token: 0x06000800 RID: 2048 RVA: 0x0002854C File Offset: 0x0002674C
        public override CommandScope Scope
        {
            get { return CommandScope.Lava; }
        }

        // Token: 0x06000802 RID: 2050 RVA: 0x00028558 File Offset: 0x00026758
        public override void Use(Player p, string message)
        {
            Player player = null;
            if (message.IndexOf(' ') != -1)
            {
                player = Player.Find(message.Split(' ')[message.Split(' ').Length - 1]);
                if (player != null) message = message.Substring(0, message.LastIndexOf(' '));
            }

            if (player == null) player = p;
            if (File.Exists("text/tips.txt"))
                try
                {
                    var array = File.ReadAllLines("text/tips.txt");
                    if (array[0][0] == '#')
                    {
                        if (Group.Find(array[0].Substring(1)).Permission <= p.group.Permission)
                            for (var i = 1; i < array.Length; i++)
                                Player.SendMessage(player, array[i]);
                        else
                            Player.SendMessage(p, "You cannot view this file");
                    }
                    else
                    {
                        for (var j = 0; j < array.Length; j++) Player.SendMessage(player, array[j]);
                    }

                    return;
                }
                catch
                {
                    Player.SendMessage(p, "An error occurred when retrieving the file");
                    return;
                }

            Player.SendMessage(p, "File specified doesn't exist");
        }

        // Token: 0x06000803 RID: 2051 RVA: 0x00028674 File Offset: 0x00026874
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/tips - Don't make me explain this.");
        }
    }
}