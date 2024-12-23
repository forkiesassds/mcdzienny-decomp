namespace MCDzienny
{
    public class CmdLimit : Command
    {
        public override string name { get { return "limit"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override void Use(Player p, string message)
        {
            if (message.Split(' ').Length != 2)
            {
                Help(p);
                return;
            }
            int num;
            try
            {
                num = int.Parse(message.Split(' ')[1]);
            }
            catch
            {
                Player.SendMessage(p, "Invalid limit amount");
                return;
            }
            if (num < 1)
            {
                Player.SendMessage(p, "Cannot set below 1.");
                return;
            }
            Group group = Group.Find(message.Split(' ')[0]);
            if (group != null)
            {
                group.maxBlocks = num;
                Player.GlobalChat(null, string.Format("{0}'s building limits were set to &b{1}", group.color + group.name + Server.DefaultColor, num), showname: false);
                Group.saveGroups(Group.groupList);
                return;
            }
            switch (message.Split(' ')[0].ToLower())
            {
                case "rp":
                case "restartphysics":
                    Server.rpLimit = num;
                    Player.GlobalMessage(string.Format("Custom /rp's limit was changed to &b{0}", num.ToString()));
                    break;
                case "rpnorm":
                case "rpnormal":
                    Server.rpNormLimit = num;
                    Player.GlobalMessage(string.Format("Normal /rp's limit was changed to &b{0}", num.ToString()));
                    break;
                default:
                    Player.SendMessage(p, "No supported /limit");
                    break;
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/limit <type> <amount> - Sets the limit for <type>");
            Player.SendMessage(p, "<types> - " + Group.concatList(includeColor: true, skipExtra: true) + ", RP, RPNormal");
        }
    }
}