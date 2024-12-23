namespace MCDzienny
{
    public class CmdClick : Command
    {
        public override string name { get { return "click"; } }

        public override string shortcut { get { return "x"; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override CommandScope Scope { get { return CommandScope.Lava; } }

        public override void Use(Player p, string message)
        {
            string[] array = message.Split(' ');
            ushort[] lastClick = p.lastClick;
            if (message.IndexOf(' ') != -1)
            {
                if (array.Length != 3)
                {
                    Help(p);
                    return;
                }
                for (int i = 0; i < 3; i++)
                {
                    if (array[i].ToLower() == "x" || array[i].ToLower() == "y" || array[i].ToLower() == "z")
                    {
                        lastClick[i] = p.lastClick[i];
                        continue;
                    }
                    if (isValid(array[i], i, p))
                    {
                        lastClick[i] = ushort.Parse(array[i]);
                        continue;
                    }
                    Player.SendMessage(p, string.Format("\"{0}\" was not valid", array[i]));
                    return;
                }
            }
            p.manualChange(lastClick[0], lastClick[1], lastClick[2], 0, 1);
            Player.SendMessage(p, string.Format("Clicked &b({0}, {1}, {2})", lastClick[0], lastClick[1], lastClick[2]));
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/click [x y z] - Fakes a click");
            Player.SendMessage(p, "If no xyz is given, it uses the last place clicked");
            Player.SendMessage(p, "/click 200 y 200 will cause it to click at 200x, last y and 200z");
        }

        bool isValid(string message, int dimension, Player p)
        {
            ushort num;
            try
            {
                num = ushort.Parse(message);
            }
            catch
            {
                return false;
            }
            if (num < 0)
            {
                return false;
            }
            if (num >= p.level.width && dimension == 0)
            {
                return false;
            }
            if (num >= p.level.height && dimension == 1)
            {
                return false;
            }
            if (num >= p.level.depth && dimension == 2)
            {
                return false;
            }
            return true;
        }
    }
}