using System;

namespace MCDzienny
{
    // Token: 0x02000252 RID: 594
    public class CmdBan : Command
    {
        // Token: 0x17000633 RID: 1587
        // (get) Token: 0x0600113D RID: 4413 RVA: 0x0005DE68 File Offset: 0x0005C068
        public override string name
        {
            get { return "ban"; }
        }

        // Token: 0x17000634 RID: 1588
        // (get) Token: 0x0600113E RID: 4414 RVA: 0x0005DE70 File Offset: 0x0005C070
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000635 RID: 1589
        // (get) Token: 0x0600113F RID: 4415 RVA: 0x0005DE78 File Offset: 0x0005C078
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x17000636 RID: 1590
        // (get) Token: 0x06001140 RID: 4416 RVA: 0x0005DE80 File Offset: 0x0005C080
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000637 RID: 1591
        // (get) Token: 0x06001141 RID: 4417 RVA: 0x0005DE84 File Offset: 0x0005C084
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x17000638 RID: 1592
        // (get) Token: 0x06001142 RID: 4418 RVA: 0x0005DE88 File Offset: 0x0005C088
        public override string CustomName
        {
            get { return Lang.Command.BanName; }
        }

        // Token: 0x06001143 RID: 4419 RVA: 0x0005DE90 File Offset: 0x0005C090
        public override void Use(Player p, string message)
        {
            try
            {
                if (message == "")
                {
                    Help(p);
                }
                else
                {
                    var flag = false;
                    var flag2 = false;
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

                    var player = Player.Find(message);
                    if (player == null)
                    {
                        if (!Player.ValidName(message))
                        {
                            Player.SendMessage(p, string.Format(Lang.Command.BanMessage, message));
                            return;
                        }

                        var group = Group.findPlayerGroup(message);
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
                            Player.GlobalMessageOps(string.Format(Lang.Command.BanMessage4,
                                player.color + player.name + Server.DefaultColor, Server.DefaultColor));
                        else
                            Player.GlobalChat(player,
                                string.Format(Lang.Command.BanMessage5,
                                    player.color + player.name + Server.DefaultColor, Server.DefaultColor), false);
                        player.group = Group.findPerm(LevelPermission.Banned);
                        player.color = player.group.color;
                        Player.GlobalDie(player, false);
                        Player.GlobalSpawn(player, player.pos[0], player.pos[1], player.pos[2], player.rot[0],
                            player.rot[1], false);
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
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x06001144 RID: 4420 RVA: 0x0005E1D8 File Offset: 0x0005C3D8
        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.BanHelp);
            Player.SendMessage(p, Lang.Command.BanHelp1);
            Player.SendMessage(p, Lang.Command.BanHelp2);
        }
    }
}