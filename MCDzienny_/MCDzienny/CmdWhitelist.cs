namespace MCDzienny
{
    public class CmdWhitelist : Command
    {
        public override string name { get { return "whitelist"; } }

        public override string shortcut { get { return "w"; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            int num = message.IndexOf(' ');
            if (num != -1)
            {
                string text = message.Substring(0, num);
                string text2 = message.Substring(num + 1);
                switch (text)
                {
                    case "add":
                        if (Server.whiteList.Contains(text2))
                        {
                            Player.SendMessage(p, "&f" + text2 + Server.DefaultColor + " is already on the whitelist!");
                            break;
                        }
                        Server.whiteList.Add(text2);
                        Player.GlobalMessageOps(p.color + p.prefix + p.name + Server.DefaultColor + " added &f" + text2 + Server.DefaultColor + " to the whitelist.");
                        Server.whiteList.Save("whitelist.txt");
                        Server.s.Log("WHITELIST: Added " + text2);
                        break;
                    case "del":
                        if (!Server.whiteList.Contains(text2))
                        {
                            Player.SendMessage(p, "&f" + text2 + Server.DefaultColor + " is not on the whitelist!");
                            break;
                        }
                        Server.whiteList.Remove(text2);
                        Player.GlobalMessageOps(p.color + p.prefix + p.name + Server.DefaultColor + " removed &f" + text2 + Server.DefaultColor + " from the whitelist.");
                        Server.whiteList.Save("whitelist.txt");
                        Server.s.Log("WHITELIST: Removed " + text2);
                        break;
                    case "list":
                    {
                        string text3 = "Whitelist:&f";
                        foreach (string item in Server.whiteList.All())
                        {
                            text3 = text3 + " " + item + ",";
                        }
                        text3 = text3.Substring(0, text3.Length - 1);
                        Player.SendMessage(p, text3);
                        break;
                    }
                    default:
                        Help(p);
                        break;
                }
            }
            else if (message == "list")
            {
                string text4 = "Whitelist:&f";
                foreach (string item2 in Server.whiteList.All())
                {
                    text4 = text4 + " " + item2 + ",";
                }
                text4 = text4.Substring(0, text4.Length - 1);
                Player.SendMessage(p, text4);
            }
            else
            {
                Help(p);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/whitelist <add/del/list> [player] - Handles whitelist entry for [player], or lists all entries.");
        }
    }
}