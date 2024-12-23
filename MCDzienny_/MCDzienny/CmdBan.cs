using System;

namespace MCDzienny
{
    public class CmdBan : Command
    {
        public override string name { get { return "ban"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override string CustomName { get { return Lang.Command.BanName; } }

        public override void Use(Player p, string message)
        {
            try
            {
                if (message == "")
                {
                    Help(p);
                    return;
                }
                bool flag = false;
                bool flag2 = false;
                if (message[0] == '#')
                {
                    message = message.Remove(0, 1).Trim();
                    flag = true;
                    Server.s.Log("Stealth Ban Attempted");
                }
                else if (message[0] == '@')
                {
                    flag2 = true;
                    message = message.Remove(0, 1).Trim();
                }
                Player player = Player.Find(message);
                if (player == null)
                {
                    if (!Player.ValidName(message))
                    {
                        Player.SendMessage(p, string.Format(Lang.Command.BanMessage, message));
                        return;
                    }
                    Group group = Group.findPlayerGroup(message);
                    if (group.Permission >= LevelPermission.Operator)
                    {
                        Player.SendMessage(p, string.Format(Lang.Command.BanMessage1, group.name));
                        return;
                    }
                    if (group.Permission == LevelPermission.Banned)
                    {
                        Player.SendMessage(p, string.Format(Lang.Command.BanMessage2, message));
                        return;
                    }
                    group.playerList.Remove(message);
                    group.playerList.Save();
                    Player.GlobalMessage(string.Format(Lang.Command.BanMessage3, message, Server.DefaultColor));
                    Group.findPerm(LevelPermission.Banned).playerList.Add(message);
                }
                else
                {
                    if (!Player.ValidName(player.name))
                    {
                        Player.SendMessage(p, string.Format(Lang.Command.BanMessage, player.name));
                        return;
                    }
                    if (player.group.Permission >= LevelPermission.Operator)
                    {
                        Player.SendMessage(p, string.Format(Lang.Command.BanMessage1, player.group.name));
                        return;
                    }
                    if (player.group.Permission == LevelPermission.Banned)
                    {
                        Player.SendMessage(p, string.Format(Lang.Command.BanMessage2, message));
                        return;
                    }
                    player.group.playerList.Remove(message);
                    player.group.playerList.Save();
                    if (flag)
                    {
                        Player.GlobalMessageOps(string.Format(Lang.Command.BanMessage4, player.color + player.name + Server.DefaultColor, Server.DefaultColor));
                    }
                    else
                    {
                        Player.GlobalChat(player, string.Format(Lang.Command.BanMessage5, player.color + player.name + Server.DefaultColor, Server.DefaultColor),
                                          showname: false);
                    }
                    player.group = Group.findPerm(LevelPermission.Banned);
                    player.color = player.group.color;
                    Player.GlobalDie(player, self: false);
                    Player.GlobalSpawn(player, player.pos[0], player.pos[1], player.pos[2], player.rot[0], player.rot[1], self: false);
                    Group.findPerm(LevelPermission.Banned).playerList.Add(player.name.ToLower());
                }
                Group.findPerm(LevelPermission.Banned).playerList.Save();
                Player.IRCSay(string.Format(Lang.Command.BanMessage6, message));
                Server.s.Log("BANNED: " + message.ToLower());
                if (flag2)
                {
                    all.Find("undo").Use(p, message + " 0");
                    all.Find("banip").Use(p, "@ " + message);
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.BanHelp);
            Player.SendMessage(p, Lang.Command.BanHelp1);
            Player.SendMessage(p, Lang.Command.BanHelp2);
        }
    }
}