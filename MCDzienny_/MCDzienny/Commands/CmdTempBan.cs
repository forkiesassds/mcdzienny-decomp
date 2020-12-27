using System;

namespace MCDzienny
{
    // Token: 0x02000100 RID: 256
    public class CmdTempBan : Command
    {
        // Token: 0x1700037C RID: 892
        // (get) Token: 0x060007D2 RID: 2002 RVA: 0x00027B74 File Offset: 0x00025D74
        public override string name
        {
            get { return "tempban"; }
        }

        // Token: 0x1700037D RID: 893
        // (get) Token: 0x060007D3 RID: 2003 RVA: 0x00027B7C File Offset: 0x00025D7C
        public override string shortcut
        {
            get { return "tb"; }
        }

        // Token: 0x1700037E RID: 894
        // (get) Token: 0x060007D4 RID: 2004 RVA: 0x00027B84 File Offset: 0x00025D84
        public override string type
        {
            get { return "moderation"; }
        }

        // Token: 0x1700037F RID: 895
        // (get) Token: 0x060007D5 RID: 2005 RVA: 0x00027B8C File Offset: 0x00025D8C
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000380 RID: 896
        // (get) Token: 0x060007D6 RID: 2006 RVA: 0x00027B90 File Offset: 0x00025D90
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Builder; }
        }

        // Token: 0x060007D7 RID: 2007 RVA: 0x00027B94 File Offset: 0x00025D94
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            if (message.IndexOf(' ') == -1) message += " 30";
            var player = Player.Find(message.Split(' ')[0]);
            if (player == null)
            {
                Player.SendMessage(p, "Could not find player");
                return;
            }

            if (p != null && player.group.Permission >= p.group.Permission)
            {
                Player.SendMessage(p, "Cannot ban someone of the same rank");
                return;
            }

            int num;
            try
            {
                num = int.Parse(message.Split(' ')[1]);
            }
            catch
            {
                Player.SendMessage(p, "Invalid minutes");
                return;
            }

            if (num > 60)
            {
                Player.SendMessage(p, "Cannot ban for more than an hour");
            }
            else
            {
                if (num < 1)
                {
                    Player.SendMessage(p, "Cannot ban someone for less than a minute");
                    return;
                }

                Server.TempBan item;
                item.name = player.name;
                item.allowedJoin = DateTime.Now.AddMinutes(num);
                Server.tempBans.Add(item);
                player.Kick(string.Format("Banned for {0} minutes!", num));
            }
        }

        // Token: 0x060007D8 RID: 2008 RVA: 0x00027CC4 File Offset: 0x00025EC4
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/tempban <name> <minutes> - Bans <name> for <minutes>");
            Player.SendMessage(p, "Max time is 60. Default is 30");
            Player.SendMessage(p, "Temp bans will reset on server restart");
        }
    }
}