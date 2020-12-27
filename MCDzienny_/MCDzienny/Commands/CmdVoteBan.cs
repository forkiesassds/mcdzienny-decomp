namespace MCDzienny
{
    // Token: 0x0200012E RID: 302
    public class CmdVoteBan : Command
    {
        // Token: 0x17000429 RID: 1065
        // (get) Token: 0x06000903 RID: 2307 RVA: 0x0002CD80 File Offset: 0x0002AF80
        public override string name
        {
            get { return "voteban"; }
        }

        // Token: 0x1700042A RID: 1066
        // (get) Token: 0x06000904 RID: 2308 RVA: 0x0002CD88 File Offset: 0x0002AF88
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700042B RID: 1067
        // (get) Token: 0x06000905 RID: 2309 RVA: 0x0002CD90 File Offset: 0x0002AF90
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x1700042C RID: 1068
        // (get) Token: 0x06000906 RID: 2310 RVA: 0x0002CD98 File Offset: 0x0002AF98
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x1700042D RID: 1069
        // (get) Token: 0x06000907 RID: 2311 RVA: 0x0002CD9C File Offset: 0x0002AF9C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Builder; }
        }

        // Token: 0x06000909 RID: 2313 RVA: 0x0002CDA8 File Offset: 0x0002AFA8
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            if (p != null && (p.muted || p.IsTempMuted))
            {
                Player.SendMessage(p, "You can't use this command while muted.");
                return;
            }

            var flag = false;
            Player.OnPlayerChatEvent(p, ref message, ref flag);
            if (flag) return;
            var name = message.Trim();
            var text = "";
            if (message.Split(' ').Length >= 2)
            {
                name = message.Split(' ')[0];
                text = message.Substring(message.IndexOf(' ') + 1);
            }

            if (VotingSystem.votingInProgress)
            {
                Player.SendMessage(p, "Please wait till the end of the current voting!");
                return;
            }

            var player = Player.Find(name);
            if (player == null)
            {
                Player.SendMessage(p, "Could not find player specified!");
                return;
            }

            if (p != null && player.group.Permission > p.group.Permission)
            {
                Player.GlobalChat(p,
                    string.Format("{0} {1}tried to voteban {2} {3}and failed!", p.color + p.PublicName,
                        Server.DefaultColor, player.color + player.name, Server.DefaultColor), false);
                return;
            }

            var text2 = p == null
                ? Server.ConsoleRealName.Substring(0, Server.ConsoleRealName.Length - 1)
                : p.color + p.PublicName;
            if (text == "")
            {
                Server.s.Log("Vote to tempban " + player.name + " was called by " + text2);
                Player.GlobalMessage(string.Format("&cA vote to temp-ban {0} {1}has been called by {2}!",
                    player.color + player.PublicName, Server.DefaultColor, text2 + Server.DefaultColor));
                Player.GlobalMessage(string.Format("Type &cY {0}or &cN {1}to vote.", Server.DefaultColor,
                    Server.DefaultColor));
            }
            else
            {
                Server.s.Log(
                    string.Concat("Vote to tempban ", player.name, " was called by ", text2, " reason: ", text));
                Player.GlobalMessage(string.Concat("&cA vote to temp-ban ", player.color, player.PublicName, " ",
                    Server.DefaultColor, "has been called by ", text2, Server.DefaultColor, "!"));
                Player.GlobalMessage("%cGiven reason: " + Server.DefaultColor + text);
                Player.GlobalMessage(string.Concat("Type &cY ", Server.DefaultColor, "or &cN ", Server.DefaultColor,
                    "to vote."));
            }

            VotingSystem.StartVote(player, "", VotingSystem.TypeOfVote.VoteBan, 30000);
        }

        // Token: 0x0600090A RID: 2314 RVA: 0x0002D0AC File Offset: 0x0002B2AC
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/voteban <player> - Calls a 30sec vote to tempban <player>");
            Player.SendMessage(p, "/voteban <player> [reason]");
        }
    }
}