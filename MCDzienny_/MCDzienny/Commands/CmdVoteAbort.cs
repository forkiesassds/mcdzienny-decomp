namespace MCDzienny.Commands
{
    public class CmdVoteAbort : Command
    {
        public override string name { get { return "voteabort"; } }

        public override string shortcut { get { return "vabort"; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            if (VotingSystem.votingInProgress)
            {
                VotingSystem.EndVote();
                Player.SendMessage(p, "@The voting was aborted.");
                Player.GlobalChatWorld(p, "%4The voting was aborted.", false);
            }
            else
            {
                Player.SendMessage(p, "@There's no voting at the moment.");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "");
        }
    }
}