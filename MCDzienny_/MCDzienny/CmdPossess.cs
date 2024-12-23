using System;

namespace MCDzienny
{
    public class CmdPossess : Command
    {
        public override string name { get { return "possess"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (message.Split(' ').Length > 2)
            {
                Help(p);
                return;
            }
            if (p == null)
            {
                Player.SendMessage(p, "Console possession?  Nope.avi.");
                return;
            }
            try
            {
                string text = message.Split(' ').Length == 2 ? message.Split(' ')[1] : "";
                message = message.Split(' ')[0];
                if (message == "")
                {
                    if (p.possess == "")
                    {
                        Help(p);
                        return;
                    }
                    Player player = Player.Find(p.possess);
                    if (player == null)
                    {
                        p.possess = "";
                        Player.SendMessage(p, "Possession disabled.");
                        return;
                    }
                    player.following = "";
                    player.canBuild = true;
                    p.possess = "";
                    if (player.MarkPossessed())
                    {
                        p.invincible = false;
                        all.Find("hide").Use(p, "");
                        Player.SendMessage(p, string.Format("Stopped possessing {0}.", player.color + player.name + Server.DefaultColor));
                    }
                    return;
                }
                if (message == p.possess)
                {
                    Player player2 = Player.Find(p.possess);
                    if (player2 == null)
                    {
                        p.possess = "";
                        Player.SendMessage(p, "Possession disabled.");
                        return;
                    }
                    if (player2 == p)
                    {
                        Player.SendMessage(p, "Cannot possess yourself!");
                        return;
                    }
                    player2.following = "";
                    player2.canBuild = true;
                    p.possess = "";
                    if (player2.MarkPossessed())
                    {
                        p.invincible = false;
                        all.Find("hide").Use(p, "");
                        Player.SendMessage(p, "Stopped possessing " + player2.color + player2.name + Server.DefaultColor + ".");
                    }
                    return;
                }
                Player player3 = Player.Find(message);
                if (player3 == null)
                {
                    Player.SendMessage(p, "Could not find player.");
                    return;
                }
                if (player3.group.Permission >= p.group.Permission)
                {
                    Player.SendMessage(p, "Cannot possess someone of equal or greater rank.");
                    return;
                }
                if (player3.possess != "")
                {
                    Player.SendMessage(p, "That player is currently possessing someone!");
                    return;
                }
                if (player3.following != "")
                {
                    Player.SendMessage(p, "That player is either following someone or already possessed.");
                    return;
                }
                if (p.possess != "")
                {
                    Player player4 = Player.Find(p.possess);
                    if (player4 != null)
                    {
                        player4.following = "";
                        player4.canBuild = true;
                        if (!player4.MarkPossessed())
                        {
                            return;
                        }
                    }
                }
                all.Find("tp").Use(p, player3.name);
                if (!p.hidden)
                {
                    all.Find("hide").Use(p, "");
                }
                p.possess = player3.name;
                player3.following = p.name;
                if (!p.invincible)
                {
                    p.invincible = true;
                }
                if (text == "#" ? player3.MarkPossessed() : player3.MarkPossessed(p.name))
                {
                    p.SendDie(player3.id);
                    player3.canBuild = false;
                    Player.SendMessage(p, string.Format("Successfully possessed {0}.", player3.color + player3.name + Server.DefaultColor));
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
                Player.SendMessage(p, "There was an error.");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/possess <player> [skin as #] - DEMONIC POSSESSION HUE HUE");
            Player.SendMessage(p, "Using # after player name makes possessed keep their custom skin during possession.");
            Player.SendMessage(p, "Not using it makes them lose their skin, and makes their name show as \"Player (YourName)\".");
        }
    }
}