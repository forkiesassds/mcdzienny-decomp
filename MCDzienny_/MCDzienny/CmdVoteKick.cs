namespace MCDzienny
{
    public class CmdVoteKick : Command
    {
        public override string name { get { return "votekick"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }

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
            bool stopIt = false;
            Player.OnPlayerChatEvent(p, ref message, ref stopIt);
            if (stopIt)
            {
                return;
            }
            string text = message.Trim();
            string text2 = "";
            if (message.Split(' ').Length >= 2)
            {
                text = message.Split(' ')[0];
                text2 = message.Substring(message.IndexOf(' ') + 1);
            }
            if (VotingSystem.votingInProgress)
            {
                Player.SendMessage(p, "The voting is in progress. Please wait till the end of the current voting before starting another one.");
                return;
            }
            Player player = Player.Find(text);
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
            string text3 = p == null ? Server.ConsoleRealName.Substring(0, Server.ConsoleRealName.Length - 1) : p.color + p.PublicName;
            if (text2 == "")
            {
                Server.s.Log("Votekick of " + player.name + " was called by " + text3);
                Player.GlobalMessage(string.Format("%cVotekick was called by {0}", text3));
                Player.GlobalMessage(string.Format("Do you want {0} to be kicked?", player.PublicName));
                Player.GlobalMessage(string.Format("Write %cY{0} to vote yes or %cN{1} to vote no", Server.DefaultColor, Server.DefaultColor));
            }
            else
            {
                Server.s.Log("Votekick of " + player.name + " was called by " + text3 + " reason: " + text2);
                Player.GlobalMessage(string.Format("%cVotekick was called by {0}", text3));
                Player.GlobalMessage(string.Format("%cGiven reason: {0}", Server.DefaultColor + text2));
                Player.GlobalMessage(string.Format("Do you want {0} to be kicked?", player.PublicName));
                Player.GlobalMessage(string.Format("Write %cY{0} to vote yes or %cN{1} to vote no", Server.DefaultColor, Server.DefaultColor));
            }
            VotingSystem.StartVote(player, "", VotingSystem.TypeOfVote.VoteKick, 30000);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/votekick <player> - calls a vote on kicking <player>.");
            Player.SendMessage(p, "/votekick <player> [reason] - calls a vote on kicking <player> for [reason]");
        }
    }
}