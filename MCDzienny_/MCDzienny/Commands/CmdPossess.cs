using System;

namespace MCDzienny
{
    // Token: 0x020000DE RID: 222
    public class CmdPossess : Command
    {
        // Token: 0x17000330 RID: 816
        // (get) Token: 0x06000729 RID: 1833 RVA: 0x000246A4 File Offset: 0x000228A4
        public override string name
        {
            get { return "possess"; }
        }

        // Token: 0x17000331 RID: 817
        // (get) Token: 0x0600072A RID: 1834 RVA: 0x000246AC File Offset: 0x000228AC
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000332 RID: 818
        // (get) Token: 0x0600072B RID: 1835 RVA: 0x000246B4 File Offset: 0x000228B4
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x17000333 RID: 819
        // (get) Token: 0x0600072C RID: 1836 RVA: 0x000246BC File Offset: 0x000228BC
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000334 RID: 820
        // (get) Token: 0x0600072D RID: 1837 RVA: 0x000246C0 File Offset: 0x000228C0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x17000335 RID: 821
        // (get) Token: 0x0600072E RID: 1838 RVA: 0x000246C4 File Offset: 0x000228C4
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06000730 RID: 1840 RVA: 0x000246D0 File Offset: 0x000228D0
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
                var a = message.Split(' ').Length == 2 ? message.Split(' ')[1] : "";
                message = message.Split(' ')[0];
                if (message == "")
                {
                    if (p.possess == "")
                    {
                        Help(p);
                    }
                    else
                    {
                        var player = Player.Find(p.possess);
                        if (player == null)
                        {
                            p.possess = "";
                            Player.SendMessage(p, "Possession disabled.");
                        }
                        else
                        {
                            player.following = "";
                            player.canBuild = true;
                            p.possess = "";
                            if (player.MarkPossessed())
                            {
                                p.invincible = false;
                                all.Find("hide").Use(p, "");
                                Player.SendMessage(p,
                                    string.Format("Stopped possessing {0}.",
                                        player.color + player.name + Server.DefaultColor));
                            }
                        }
                    }
                }
                else if (message == p.possess)
                {
                    var player2 = Player.Find(p.possess);
                    if (player2 == null)
                    {
                        p.possess = "";
                        Player.SendMessage(p, "Possession disabled.");
                    }
                    else if (player2 == p)
                    {
                        Player.SendMessage(p, "Cannot possess yourself!");
                    }
                    else
                    {
                        player2.following = "";
                        player2.canBuild = true;
                        p.possess = "";
                        if (player2.MarkPossessed())
                        {
                            p.invincible = false;
                            all.Find("hide").Use(p, "");
                            Player.SendMessage(p,
                                string.Concat("Stopped possessing ", player2.color, player2.name, Server.DefaultColor,
                                    "."));
                        }
                    }
                }
                else
                {
                    var player3 = Player.Find(message);
                    if (player3 == null)
                    {
                        Player.SendMessage(p, "Could not find player.");
                    }
                    else if (player3.group.Permission >= p.group.Permission)
                    {
                        Player.SendMessage(p, "Cannot possess someone of equal or greater rank.");
                    }
                    else if (player3.possess != "")
                    {
                        Player.SendMessage(p, "That player is currently possessing someone!");
                    }
                    else if (player3.following != "")
                    {
                        Player.SendMessage(p, "That player is either following someone or already possessed.");
                    }
                    else
                    {
                        if (p.possess != "")
                        {
                            var player4 = Player.Find(p.possess);
                            if (player4 != null)
                            {
                                player4.following = "";
                                player4.canBuild = true;
                                if (!player4.MarkPossessed()) return;
                            }
                        }

                        all.Find("tp").Use(p, player3.name);
                        if (!p.hidden) all.Find("hide").Use(p, "");
                        p.possess = player3.name;
                        player3.following = p.name;
                        if (!p.invincible) p.invincible = true;
                        if (a == "#" ? player3.MarkPossessed() : player3.MarkPossessed(p.name))
                        {
                            p.SendDie(player3.id);
                            player3.canBuild = false;
                            Player.SendMessage(p,
                                string.Format("Successfully possessed {0}.",
                                    player3.color + player3.name + Server.DefaultColor));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
                Player.SendMessage(p, "There was an error.");
            }
        }

        // Token: 0x06000731 RID: 1841 RVA: 0x00024AF8 File Offset: 0x00022CF8
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/possess <player> [skin as #] - DEMONIC POSSESSION HUE HUE");
            Player.SendMessage(p,
                "Using # after player name makes possessed keep their custom skin during possession.");
            Player.SendMessage(p,
                "Not using it makes them lose their skin, and makes their name show as \"Player (YourName)\".");
        }
    }
}