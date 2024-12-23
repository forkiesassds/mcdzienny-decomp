namespace MCDzienny
{
    public class CmdIronman : Command
    {
        public override string name { get { return "ironman"; } }

        public override string shortcut { get { return "im"; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override bool ConsoleAccess { get { return false; } }

        public override CommandScope Scope { get { return CommandScope.Lava; } }

        public override void Use(Player p, string message)
        {
            if (p.IronChallenge != 0)
            {
                Player.SendMessage(p, "You already accepted Iron Man challenge!");
            }
            else if (LavaSystem.time > 0)
            {
                p.IronChallenge = IronChallengeType.IronMan;
                Player.GlobalChatLevel(p, p.color + p.PublicName + " %atakes on Iron Man challenge!", showname: false);
                Player.SendMessage(p, "Good luck!");
            }
            else
            {
                Player.SendMessage(p, "Sorry, you can take up Iron Man challenge only before the flood starts.");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/ironman - accepts the challenge that you will not die once");
            Player.SendMessage(p, "during this round.");
            Player.SendMessage(p, "If you meet this condition your award in " + Server.moneys);
            Player.SendMessage(p, "will be twice as high as normally. But if you fail you will");
            Player.SendMessage(p, "lose three times what you could gain.");
        }
    }
}