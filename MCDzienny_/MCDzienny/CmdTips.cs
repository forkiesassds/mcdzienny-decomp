using System.IO;

namespace MCDzienny
{
    public class CmdTips : Command
    {
        public override string name { get { return "tips"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override CommandScope Scope { get { return CommandScope.Lava; } }

        public override void Use(Player p, string message)
        {
            Player player = null;
            if (message.IndexOf(' ') != -1)
            {
                player = Player.Find(message.Split(' ')[message.Split(' ').Length - 1]);
                if (player != null)
                {
                    message = message.Substring(0, message.LastIndexOf(' '));
                }
            }
            if (player == null)
            {
                player = p;
            }
            if (File.Exists("text/tips.txt"))
            {
                try
                {
                    string[] array = File.ReadAllLines("text/tips.txt");
                    if (array[0][0] == '#')
                    {
                        if (Group.Find(array[0].Substring(1)).Permission <= p.group.Permission)
                        {
                            for (int i = 1; i < array.Length; i++)
                            {
                                Player.SendMessage(player, array[i]);
                            }
                        }
                        else
                        {
                            Player.SendMessage(p, "You cannot view this file");
                        }
                    }
                    else
                    {
                        for (int j = 0; j < array.Length; j++)
                        {
                            Player.SendMessage(player, array[j]);
                        }
                    }
                    return;
                }
                catch
                {
                    Player.SendMessage(p, "An error occurred when retrieving the file");
                    return;
                }
            }
            Player.SendMessage(p, "File specified doesn't exist");
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/tips - Don't make me explain this.");
        }
    }
}