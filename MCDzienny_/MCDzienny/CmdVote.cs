namespace MCDzienny
{
    public class CmdVote : Command
    {
        public override string name { get { return "vote"; } }

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
            if (!stopIt)
            {
                if (VotingSystem.votingInProgress)
                {
                    Player.SendMessage(p, "Please wait till the end of the current voting!");
                    return;
                }
                Player.GlobalMessage("---------------- Vote ----------------");
                Player.GlobalMessage(string.Format(message));
                Player.GlobalMessage(string.Format("Type &cY {0}or &cN {1}to vote.", Server.DefaultColor, Server.DefaultColor));
                VotingSystem.StartVote(null, message, VotingSystem.TypeOfVote.Vote, 30000);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/vote <message> - Calls a 30sec vote.");
        }
    }
}