using System;

namespace MCDzienny
{
    // Token: 0x02000266 RID: 614
    public class CmdHelp : Command
    {
        // Token: 0x1700066C RID: 1644
        // (get) Token: 0x060011A9 RID: 4521 RVA: 0x000611F8 File Offset: 0x0005F3F8
        public override string name
        {
            get { return "help"; }
        }

        // Token: 0x1700066D RID: 1645
        // (get) Token: 0x060011AA RID: 4522 RVA: 0x00061200 File Offset: 0x0005F400
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700066E RID: 1646
        // (get) Token: 0x060011AB RID: 4523 RVA: 0x00061208 File Offset: 0x0005F408
        public override string type
        {
            get { return "information"; }
        }

        // Token: 0x1700066F RID: 1647
        // (get) Token: 0x060011AC RID: 4524 RVA: 0x00061210 File Offset: 0x0005F410
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000670 RID: 1648
        // (get) Token: 0x060011AD RID: 4525 RVA: 0x00061214 File Offset: 0x0005F414
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x060011AF RID: 4527 RVA: 0x00061220 File Offset: 0x0005F420
        public override void Use(Player p, string message)
        {
            try
            {
                message.ToLower();
                switch (message)
                {
                    case "":
                        if (!Server.oldHelp)
                        {
                            Player.SendMessage(p,
                                string.Format("Use &b/help ranks{0} for a list of ranks.", Server.DefaultColor));
                            Player.SendMessage(p,
                                string.Format("Use &b/help build{0} for a list of building commands.",
                                    Server.DefaultColor));
                            Player.SendMessage(p,
                                string.Format("Use &b/help mod{0} for a list of moderation commands.",
                                    Server.DefaultColor));
                            Player.SendMessage(p,
                                string.Format("Use &b/help information{0} for a list of information commands.",
                                    Server.DefaultColor));
                            Player.SendMessage(p,
                                string.Format("Use &b/help other{0} for a list of other commands.",
                                    Server.DefaultColor));
                            Player.SendMessage(p,
                                string.Format("Use &b/help short{0} for a list of shortcuts.", Server.DefaultColor));
                            Player.SendMessage(p,
                                string.Format("Use &b/help all{0} to view the available commands to you.",
                                    Server.DefaultColor));
                            Player.SendMessage(p,
                                string.Format("Use &b/help [command] or /help [block] {0}to view more info.",
                                    Server.DefaultColor));
                            return;
                        }

                        goto case "old";
                    case "ranks":
                        message = "";
                        foreach (var group in Group.groupList)
                            if (@group.Permission != LevelPermission.Nobody)
                                Player.SendMessage(p,
                                    string.Format("{0} - &bBlock limit: {1} - &cPermission: {2}",
                                        @group.color + @group.name, @group.maxBlocks, (int) @group.Permission));
                        return;
                    case "build":
                        message = "";
                        foreach (var command in all.commands)
                            if ((p == null || p.@group.commands.All().Contains(command)) &&
                                command.type.Contains("build"))
                                message = message + ", " + getColor(command.name) + command.name;
                        if (message == "")
                        {
                            Player.SendMessage(p, "No commands of this type are available to you.");
                            return;
                        }

                        Player.SendMessage(p, "Building commands you may use:");
                        Player.SendMessage(p, message.Remove(0, 2) + ".");
                        return;
                    case "mod":
                    case "moderation":
                        message = "";
                        foreach (var command2 in all.commands)
                            if ((p == null || p.@group.commands.All().Contains(command2)) &&
                                command2.type.Contains("mod"))
                                message = message + ", " + getColor(command2.name) + command2.name;
                        if (message == "")
                        {
                            Player.SendMessage(p, "No commands of this type are available to you.");
                            return;
                        }

                        Player.SendMessage(p, "Moderation commands you may use:");
                        Player.SendMessage(p, message.Remove(0, 2) + ".");
                        return;
                    case "information":
                        message = "";
                        foreach (var command3 in all.commands)
                            if ((p == null || p.@group.commands.All().Contains(command3)) &&
                                command3.type.Contains("info"))
                                message = message + ", " + getColor(command3.name) + command3.name;
                        if (message == "")
                        {
                            Player.SendMessage(p, "No commands of this type are available to you.");
                            return;
                        }

                        Player.SendMessage(p, "Information commands you may use:");
                        Player.SendMessage(p, message.Remove(0, 2) + ".");
                        return;
                    case "other":
                        message = "";
                        foreach (var command4 in all.commands)
                            if ((p == null || p.@group.commands.All().Contains(command4)) &&
                                command4.type.Contains("other"))
                                message = message + ", " + getColor(command4.name) + command4.name;
                        if (message == "")
                        {
                            Player.SendMessage(p, "No commands of this type are available to you.");
                            return;
                        }

                        Player.SendMessage(p, "Other commands you may use:");
                        Player.SendMessage(p, message.Remove(0, 2) + ".");
                        return;
                    case "short":
                        message = "";
                        foreach (var command5 in all.commands)
                            if ((p == null || p.@group.commands.All().Contains(command5)) && command5.shortcut != "")
                            {
                                var text = message;
                                message = text + ", &b" + command5.shortcut + " " + Server.DefaultColor + "[" +
                                          command5.name + "]";
                            }

                        Player.SendMessage(p, "Available shortcuts:");
                        Player.SendMessage(p, message.Remove(0, 2) + ".");
                        return;
                    case "old":
                    case "all":
                    {
                        var text2 = "";
                        foreach (var command6 in all.commands)
                            if (p == null || p.@group.commands.All().Contains(command6) && command6.IsWithinScope(p))
                                try
                                {
                                    text2 = text2 + ", " + command6.name;
                                }
                                catch
                                {
                                }

                        Player.SendMessage(p, "Available commands:");
                        Player.SendMessage(p, text2.Remove(0, 2));
                        Player.SendMessage(p, "Type \"/help <command>\" for more help.");
                        Player.SendMessage(p, "Type \"/help shortcuts\" for shortcuts.");
                        return;
                    }
                }

                var cmd = all.Find(message);
                if (cmd != null)
                {
                    cmd.Help(p);
                    var str = Level.PermissionToName(GrpCommands.allowedCommands
                        .Find(grpComm => grpComm.commandName == cmd.name).lowestRank);
                    Player.SendMessage(p, string.Format("Rank needed: {0}", getColor(cmd.name) + str));
                    return;
                }

                var b = Block.Byte(message);
                if (b != byte.MaxValue)
                {
                    Player.SendMessage(p,
                        string.Format("Block \"{0}\" appears as &b{1}", message, Block.Name(Block.Convert(b))));
                    var arg = Level.PermissionToName(Block.BlockList.Find(bs => bs.type == b).lowestRank);
                    Player.SendMessage(p, string.Format("Rank needed: {0}", arg));
                }
                else
                {
                    Player.SendMessage(p, "Could not find command or block specified.");
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
                Player.SendMessage(p, "An error occured");
            }
        }

        // Token: 0x060011B0 RID: 4528 RVA: 0x00061B30 File Offset: 0x0005FD30
        private string getColor(string commName)
        {
            foreach (var rankAllowance in GrpCommands.allowedCommands)
                if (rankAllowance.commandName == commName && Group.findPerm(rankAllowance.lowestRank) != null)
                    return Group.findPerm(rankAllowance.lowestRank).color;
            return "&f";
        }

        // Token: 0x060011B1 RID: 4529 RVA: 0x00061BB0 File Offset: 0x0005FDB0
        public override void Help(Player p)
        {
            Player.SendMessage(p, "...really? Wow. Just...wow.");
        }
    }
}