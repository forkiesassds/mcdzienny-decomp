using System;

namespace MCDzienny
{
    // Token: 0x020002D7 RID: 727
    public class CmdFollow : Command
    {
        // Token: 0x170007F4 RID: 2036
        // (get) Token: 0x06001494 RID: 5268 RVA: 0x00071A70 File Offset: 0x0006FC70
        public override string name
        {
            get { return "follow"; }
        }

        // Token: 0x170007F5 RID: 2037
        // (get) Token: 0x06001495 RID: 5269 RVA: 0x00071A78 File Offset: 0x0006FC78
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170007F6 RID: 2038
        // (get) Token: 0x06001496 RID: 5270 RVA: 0x00071A80 File Offset: 0x0006FC80
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x170007F7 RID: 2039
        // (get) Token: 0x06001497 RID: 5271 RVA: 0x00071A88 File Offset: 0x0006FC88
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170007F8 RID: 2040
        // (get) Token: 0x06001498 RID: 5272 RVA: 0x00071A8C File Offset: 0x0006FC8C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x170007F9 RID: 2041
        // (get) Token: 0x06001499 RID: 5273 RVA: 0x00071A90 File Offset: 0x0006FC90
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x0600149B RID: 5275 RVA: 0x00071A9C File Offset: 0x0006FC9C
        public override void Use(Player p, string message)
        {
            if (!p.canBuild)
            {
                Player.SendMessage(p, string.Format("You're currently being &4possessed{0}!", Server.DefaultColor));
                return;
            }

            try
            {
                var flag = false;
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
                        if (p.hidden) flag = true;
                        message = message.Split(' ')[1];
                    }
                }

                var player = Player.Find(message);
                if (message == "" && p.following == "")
                {
                    Help(p);
                }
                else
                {
                    if (message == "" && p.following != "" || message == p.following)
                    {
                        player = Player.Find(p.following);
                        p.following = "";
                        if (p.hidden)
                        {
                            if (player != null)
                                p.SendSpawn(player.id, player.color + player.name, player.ModelName, player.pos[0],
                                    player.pos[1], player.pos[2], player.rot[0], player.rot[1]);
                            if (!flag)
                                all.Find("hide").Use(p, "");
                            else if (player != null)
                                Player.SendMessage(p,
                                    string.Format("You have stopped following {0} and remained hidden.",
                                        player.color + player.name + Server.DefaultColor));
                            else
                                Player.SendMessage(p, "Following stopped.");
                            return;
                        }
                    }

                    if (player == null)
                    {
                        Player.SendMessage(p, "Could not find player.");
                    }
                    else if (player == p)
                    {
                        Player.SendMessage(p, "Cannot follow yourself.");
                    }
                    else if (player.group.Permission >= p.group.Permission)
                    {
                        Player.SendMessage(p, "Cannot follow someone of equal or greater rank.");
                    }
                    else if (player.following != "")
                    {
                        Player.SendMessage(p,
                            string.Format("{0} is already following {1}", player.name, player.following));
                    }
                    else
                    {
                        if (!p.hidden) all.Find("hide").Use(p, "");
                        if (p.level != player.level) all.Find("tp").Use(p, player.name);
                        if (p.following != "")
                        {
                            player = Player.Find(p.following);
                            if (player != null)
                                p.SendSpawn(player.id, player.color + player.name, player.ModelName, player.pos[0],
                                    player.pos[1], player.pos[2], player.rot[0], player.rot[1]);
                        }

                        player = Player.Find(message);
                        p.following = player.name;
                        Player.SendMessage(p, string.Format("Following {0}. Use \"/follow\" to stop.", player.name));
                        p.SendDie(player.id);
                    }
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
                Player.SendMessage(p, "Error occured");
            }
        }

        // Token: 0x0600149C RID: 5276 RVA: 0x00071E3C File Offset: 0x0007003C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/follow <name> - Follows <name> until the command is cancelled");
            Player.SendMessage(p, "/follow # <name> - Will cause /hide not to be toggled");
        }
    }
}