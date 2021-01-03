namespace MCDzienny
{
    // Token: 0x02000144 RID: 324
    public class CmdRankMsg : Command
    {
        // Token: 0x17000474 RID: 1140
        // (get) Token: 0x06000997 RID: 2455 RVA: 0x0002F03C File Offset: 0x0002D23C
        public override string name
        {
            get { return "rankmsg"; }
        }

        // Token: 0x17000475 RID: 1141
        // (get) Token: 0x06000998 RID: 2456 RVA: 0x0002F044 File Offset: 0x0002D244
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000476 RID: 1142
        // (get) Token: 0x06000999 RID: 2457 RVA: 0x0002F04C File Offset: 0x0002D24C
        public override string type
        {
            get { return ""; }
        }

        // Token: 0x17000477 RID: 1143
        // (get) Token: 0x0600099A RID: 2458 RVA: 0x0002F054 File Offset: 0x0002D254
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000478 RID: 1144
        // (get) Token: 0x0600099B RID: 2459 RVA: 0x0002F058 File Offset: 0x0002D258
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x0600099D RID: 2461 RVA: 0x0002F064 File Offset: 0x0002D264
        public override void Use(Player p, string message)
        {
            if (message.Split(' ').Length < 2)
            {
                Help(p);
                return;
            }

            var grp = Group.Find(message.Split(' ')[0].ToLower());
            if (grp == null)
            {
                Player.SendMessage(p, "The group wasn't found.");
                return;
            }

            var msg = message.Substring(message.IndexOf(' ') + 1);
            var playerName = p == null ? Server.ConsoleRealName : p.PublicName;
            var count = 0;
            Player.players.ForEach(delegate(Player pl)
            {
                if (pl.group == grp)
                {
                    Player.SendMessage(pl,
                        string.Concat(grp.color, "(", playerName, " to ", grp.name, ") ", Server.DefaultColor, msg));
                    count++;
                }
            });
            if (count == 0)
            {
                Player.SendMessage(p,
                    string.Format("There is no player online with a rank of {0}", message.Split(' ')[0]));
                return;
            }

            if (count == 1)
            {
                Player.SendMessage(p,
                    string.Format("The message was sent to {0} player with a rank of {1}", count,
                        message.Split(' ')[0]));
                Server.s.Log(string.Concat("(", playerName, " to ", grp.name, ") ", msg));
                return;
            }

            Player.SendMessage(p,
                string.Format("The message was sent to {0} players with a rank of {1}", count, message.Split(' ')[0]));
            Server.s.Log(string.Concat("(", playerName, " to ", grp.name, ") ", msg));
        }

        // Token: 0x0600099E RID: 2462 RVA: 0x0002F26C File Offset: 0x0002D46C
        public override void Help(Player p)
        {
            Player.SendMessage(p,
                "/rankmsg [rank] [message] - sends to all players that have a certain [rank] a [message].");
        }
    }
}