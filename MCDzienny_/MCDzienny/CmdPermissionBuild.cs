namespace MCDzienny
{
    public class CmdPermissionBuild : Command
    {
        public override string name { get { return "perbuild"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            int num = message.Split(' ').Length;
            if (!(message == ""))
            {
                switch (num)
                {
                    case 1:
                    {
                        LevelPermission levelPermission2 = Level.PermissionFromName(message);
                        if (levelPermission2 == LevelPermission.Null)
                        {
                            Player.SendMessage(p, "Not a valid rank");
                            return;
                        }
                        p.level.permissionbuild = levelPermission2;
                        Server.s.Log(p.level.name + " build permission changed to " + message + ".");
                        Player.GlobalMessageLevel(p.level, string.Format("build permission changed to {0}.", message));
                        return;
                    }
                    case 2:
                    {
                        int num2 = message.IndexOf(' ');
                        string text = message.Substring(0, num2).ToLower();
                        string text2 = message.Substring(num2 + 1).ToLower();
                        LevelPermission levelPermission = Level.PermissionFromName(text2);
                        if (levelPermission == LevelPermission.Null)
                        {
                            Player.SendMessage(p, "Not a valid rank");
                            return;
                        }
                        Level level = Level.Find(text);
                        if (level != null)
                        {
                            level.permissionbuild = levelPermission;
                            Server.s.Log(level.name + " build permission changed to " + text2 + ".");
                            Player.GlobalMessageLevel(level, string.Format("build permission changed to {0}.", text2));
                            if (p != null && p.level != level)
                            {
                                Player.SendMessage(p, string.Format("build permission changed to {0} on {1}.", text2, level.name));
                            }
                        }
                        else
                        {
                            Player.SendMessage(p, string.Format("There is no level \"{0}\" loaded.", text));
                        }
                        return;
                    }
                }
            }
            Help(p);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/PerBuild <map> <rank> - Sets build permission for a map.");
        }
    }
}