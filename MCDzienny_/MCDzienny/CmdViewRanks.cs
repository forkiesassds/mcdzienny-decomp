namespace MCDzienny
{
    public class CmdViewRanks : Command
    {
        public override string name { get { return "viewranks"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "information"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            Group group = Group.Find(message);
            if (group == null)
            {
                Player.SendMessage(p, "Could not find group");
                return;
            }
            string text = "";
            foreach (string item in group.playerList.All())
            {
                text = text + ", " + item;
            }
            if (text == "")
            {
                Player.SendMessage(p, string.Format("No one has the rank of {0}", group.color + group.name));
                return;
            }
            Player.SendMessage(p, string.Format("People with the rank of {0}:", group.color + group.name));
            Player.SendMessage(p, text.Remove(0, 2));
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/viewranks [rank] - Shows all users who have [rank]");
            Player.SendMessage(p, "Available ranks: " + Group.concatList());
        }
    }
}