namespace MCDzienny
{
    // Token: 0x02000128 RID: 296
    public class CmdVoteKick : Command
    {
        // Token: 0x17000410 RID: 1040
        // (get) Token: 0x060008D9 RID: 2265 RVA: 0x0002C710 File Offset: 0x0002A910
        public override string name
        {
            get { return "votekick"; }
        }

        // Token: 0x17000411 RID: 1041
        // (get) Token: 0x060008DA RID: 2266 RVA: 0x0002C718 File Offset: 0x0002A918
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000412 RID: 1042
        // (get) Token: 0x060008DB RID: 2267 RVA: 0x0002C720 File Offset: 0x0002A920
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x17000413 RID: 1043
        // (get) Token: 0x060008DC RID: 2268 RVA: 0x0002C728 File Offset: 0x0002A928
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000414 RID: 1044
        // (get) Token: 0x060008DD RID: 2269 RVA: 0x0002C72C File Offset: 0x0002A92C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Builder; }
        }

        // Token: 0x060008DF RID: 2271 RVA: 0x0002C738 File Offset: 0x0002A938
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
                Player.SendMessage(p,
                    "The voting is in progress. Please wait till the end of the current voting before starting another one.");
                return;
            }

            var player = Player.Find(name);
            if (player == null)
            {
                Player.SendMessage(p, "The player wasn't found.");
                return;
            }

            if (p != null && p.group.Permission < player.group.Permission)
            {
                Player.SendMessage(p, "You can't votekick a player with a higher rank than your.");
                return;
            }

            var text2 = p == null
                ? Server.ConsoleRealName.Substring(0, Server.ConsoleRealName.Length - 1)
                : p.color + p.PublicName;
            if (text == "")
            {
                Server.s.Log("Votekick of " + player.name + " was called by " + text2);
                Player.GlobalMessage(string.Format("%cVotekick was called by {0}", text2));
                Player.GlobalMessage(string.Format("Do you want {0} to be kicked?", player.PublicName));
                Player.GlobalMessage(string.Format("Write %cY{0} to vote yes or %cN{1} to vote no", Server.DefaultColor,
                    Server.DefaultColor));
            }
            else
            {
                Server.s.Log(string.Concat("Votekick of ", player.name, " was called by ", text2, " reason: ", text));
                Player.GlobalMessage(string.Format("%cVotekick was called by {0}", text2));
                Player.GlobalMessage(string.Format("%cGiven reason: {0}", Server.DefaultColor + text));
                Player.GlobalMessage(string.Format("Do you want {0} to be kicked?", player.PublicName));
                Player.GlobalMessage(string.Format("Write %cY{0} to vote yes or %cN{1} to vote no", Server.DefaultColor,
                    Server.DefaultColor));
            }

            VotingSystem.StartVote(player, "", VotingSystem.TypeOfVote.VoteKick, 30000);
        }

        // Token: 0x060008E0 RID: 2272 RVA: 0x0002C984 File Offset: 0x0002AB84
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/votekick <player> - calls a vote on kicking <player>.");
            Player.SendMessage(p, "/votekick <player> [reason] - calls a vote on kicking <player> for [reason]");
        }
    }
}