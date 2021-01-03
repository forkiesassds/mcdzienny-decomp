using System;

namespace MCDzienny
{
    // Token: 0x02000103 RID: 259
    public class CmdTempMute : Command
    {
        // Token: 0x1700038C RID: 908
        // (get) Token: 0x060007EB RID: 2027 RVA: 0x000280A0 File Offset: 0x000262A0
        public override string name
        {
            get { return "tempmute"; }
        }

        // Token: 0x1700038D RID: 909
        // (get) Token: 0x060007EC RID: 2028 RVA: 0x000280A8 File Offset: 0x000262A8
        public override string shortcut
        {
            get { return "tmute"; }
        }

        // Token: 0x1700038E RID: 910
        // (get) Token: 0x060007ED RID: 2029 RVA: 0x000280B0 File Offset: 0x000262B0
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x1700038F RID: 911
        // (get) Token: 0x060007EE RID: 2030 RVA: 0x000280B8 File Offset: 0x000262B8
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000390 RID: 912
        // (get) Token: 0x060007EF RID: 2031 RVA: 0x000280BC File Offset: 0x000262BC
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x060007F0 RID: 2032 RVA: 0x000280C0 File Offset: 0x000262C0
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            var num = message.Split(' ').Length;
            var name = message.Trim();
            var text = "";
            var num2 = 0;
            if (num != 1)
            {
                if (num == 2)
                {
                    name = message.Split(' ')[0];
                    if (!int.TryParse(message.Split(' ')[1], out num2)) text = message.Split(' ')[1];
                }
                else if (num > 2)
                {
                    name = message.Split(' ')[0];
                    if (int.TryParse(message.Split(' ')[1], out num2))
                        text = message.Substring(message.IndexOf(' ', message.IndexOf(' ') + 1) + 1);
                    else
                        text = message.Substring(message.IndexOf(' ') + 1);
                }
            }

            var player = Player.Find(name);
            if (player == null)
            {
                Player.SendMessage(p, "The player wasn't found.");
                return;
            }

            if (p != null && p.group.Permission <= player.group.Permission)
            {
                Player.SendMessage(p, "You can't tempmute a player with equal or higher rank than you.");
                return;
            }

            if (num2 == 0) num2 = 60;
            player.muteTime = DateTime.Now.AddSeconds(num2);
            var text2 = p == null ? "<Console>" : p.name;
            if (num2 == 60 && text == "")
            {
                Player.SendMessage(player, "You were muted for 60 seconds.");
                Player.SendMessage(p, string.Format("Player {0} was muted for 60 seconds.", player.PublicName));
                Server.s.Log("Player " + player.name + " was muted for 60 seconds by " + text2);
                return;
            }

            if (num2 == 60 && text != "")
            {
                Player.SendMessage(player, string.Format("You were muted for 60 seconds. Reason: {0}", text));
                Player.SendMessage(p,
                    string.Format("Player {0} was muted for 60 seconds. Reason: {1}", player.PublicName, text));
                Server.s.Log(string.Concat("Player ", player.name, " was muted for 60 seconds by ", text2, " Reason: ",
                    text));
                return;
            }

            if (text == "")
            {
                Player.SendMessage(player, string.Format("You were muted for {0} seconds.", num2));
                Player.SendMessage(p, string.Format("Player {0} was muted for {1} seconds.", player.PublicName, num2));
                Server.s.Log(string.Concat("Player ", player.name, " was muted for ", num2, " seconds by ", text2));
                return;
            }

            Player.SendMessage(player, string.Format("You were muted for {0} seconds. Reason: {1}", num2, text));
            Player.SendMessage(p,
                string.Format("Player {0} was muted for {1} seconds. Reason: {2}", player.PublicName, num2, text));
            Server.s.Log(string.Concat("Player ", player.name, " was muted for ", num2, " seconds by ", text2,
                " Reason: ", text));
        }

        // Token: 0x060007F1 RID: 2033 RVA: 0x00028458 File Offset: 0x00026658
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/tempmute [player] - mutes [player] for 60 seconds.");
            Player.SendMessage(p, "/tempmute [player] [time] - mutes [player] for [time] seconds.");
            Player.SendMessage(p, "/tempmute [player] [reason] - mutes [player] due to [reason] for 60 seconds.");
            Player.SendMessage(p, "/tempmute [player] [time] [reason]");
        }
    }
}