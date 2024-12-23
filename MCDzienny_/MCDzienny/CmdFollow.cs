using System;

namespace MCDzienny
{
    public class CmdFollow : Command
    {
        public override string name { get { return "follow"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (!p.canBuild)
            {
                Player.SendMessage(p, string.Format("You're currently being &4possessed{0}!", Server.DefaultColor));
                return;
            }
            try
            {
                bool flag = false;
                if (message != "")
                {
                    if (message == "#")
                    {
                        if (!(p.following != ""))
                        {
                            Help(p);
                            return;
                        }
                        flag = true;
                        message = "";
                    }
                    else if (message.IndexOf(' ') != -1 && message.Split(' ')[0] == "#")
                    {
                        if (p.hidden)
                        {
                            flag = true;
                        }
                        message = message.Split(' ')[1];
                    }
                }
                Player player = Player.Find(message);
                if (message == "" && p.following == "")
                {
                    Help(p);
                    return;
                }
                if (message == "" && p.following != "" || message == p.following)
                {
                    player = Player.Find(p.following);
                    p.following = "";
                    if (p.hidden)
                    {
                        if (player != null)
                        {
                            p.SendSpawn(player.id, player.color + player.name, player.ModelName, player.pos[0], player.pos[1], player.pos[2], player.rot[0],
                                        player.rot[1]);
                        }
                        if (!flag)
                        {
                            all.Find("hide").Use(p, "");
                        }
                        else if (player != null)
                        {
                            Player.SendMessage(p, string.Format("You have stopped following {0} and remained hidden.", player.color + player.name + Server.DefaultColor));
                        }
                        else
                        {
                            Player.SendMessage(p, "Following stopped.");
                        }
                        return;
                    }
                }
                if (player == null)
                {
                    Player.SendMessage(p, "Could not find player.");
                    return;
                }
                if (player == p)
                {
                    Player.SendMessage(p, "Cannot follow yourself.");
                    return;
                }
                if (player.group.Permission >= p.group.Permission)
                {
                    Player.SendMessage(p, "Cannot follow someone of equal or greater rank.");
                    return;
                }
                if (player.following != "")
                {
                    Player.SendMessage(p, string.Format("{0} is already following {1}", player.name, player.following));
                    return;
                }
                if (!p.hidden)
                {
                    all.Find("hide").Use(p, "");
                }
                if (p.level != player.level)
                {
                    all.Find("tp").Use(p, player.name);
                }
                if (p.following != "")
                {
                    player = Player.Find(p.following);
                    if (player != null)
                    {
                        p.SendSpawn(player.id, player.color + player.name, player.ModelName, player.pos[0], player.pos[1], player.pos[2], player.rot[0], player.rot[1]);
                    }
                }
                player = Player.Find(message);
                p.following = player.name;
                Player.SendMessage(p, string.Format("Following {0}. Use \"/follow\" to stop.", player.name));
                p.SendDie(player.id);
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
                Player.SendMessage(p, "Error occured");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/follow <name> - Follows <name> until the command is cancelled");
            Player.SendMessage(p, "/follow # <name> - Will cause /hide not to be toggled");
        }
    }
}