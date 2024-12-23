namespace MCDzienny
{
    public class CmdMode : Command
    {
        public override string name { get { return "mode"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                if (p.modeType != 0)
                {
                    Player.SendMessage(
                        p,
                        string.Format("&b{0} mode: &cOFF",
                                      Block.Name(p.modeType)[0].ToString().ToUpper() + Block.Name(p.modeType).Remove(0, 1).ToLower() + Server.DefaultColor));
                    p.modeType = 0;
                    p.BlockAction = 0;
                }
                else
                {
                    Help(p);
                }
                return;
            }
            byte b = Block.Byte(message);
            switch (b)
            {
                case byte.MaxValue:
                    Player.SendMessage(p, "Could not find block given.");
                    return;
                case 0:
                    Player.SendMessage(p, "Cannot use Air Mode.");
                    return;
            }
            if (!Block.canPlace(p, b))
            {
                Player.SendMessage(p, "Cannot place this block at your rank.");
            }
            else if (p.modeType == b)
            {
                Player.SendMessage(
                    p,
                    string.Format("&b{0} mode: &cOFF",
                                  Block.Name(p.modeType)[0].ToString().ToUpper() + Block.Name(p.modeType).Remove(0, 1).ToLower() + Server.DefaultColor));
                p.modeType = 0;
                p.BlockAction = 0;
            }
            else
            {
                p.BlockAction = 6;
                p.modeType = b;
                Player.SendMessage(
                    p,
                    string.Format("&b{0} mode: &aON",
                                  Block.Name(p.modeType)[0].ToString().ToUpper() + Block.Name(p.modeType).Remove(0, 1).ToLower() + Server.DefaultColor));
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/mode [block] - Makes every block placed into [block].");
            Player.SendMessage(p, "/[block] also works");
        }
    }
}