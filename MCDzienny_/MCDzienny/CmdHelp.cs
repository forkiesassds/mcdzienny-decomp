using System;

namespace MCDzienny
{
    public class CmdHelp : Command
    {
        public override string name { get { return "help"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "information"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

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
                            Player.SendMessage(p, string.Format("Use &b/help ranks{0} for a list of ranks.", Server.DefaultColor));
                            Player.SendMessage(p, string.Format("Use &b/help build{0} for a list of building commands.", Server.DefaultColor));
                            Player.SendMessage(p, string.Format("Use &b/help mod{0} for a list of moderation commands.", Server.DefaultColor));
                            Player.SendMessage(p, string.Format("Use &b/help information{0} for a list of information commands.", Server.DefaultColor));
                            Player.SendMessage(p, string.Format("Use &b/help other{0} for a list of other commands.", Server.DefaultColor));
                            Player.SendMessage(p, string.Format("Use &b/help short{0} for a list of shortcuts.", Server.DefaultColor));
                            Player.SendMessage(p, string.Format("Use &b/help all{0} to view the available commands to you.", Server.DefaultColor));
                            Player.SendMessage(p, string.Format("Use &b/help [command] or /help [block] {0}to view more info.", Server.DefaultColor));
                            return;
                        }
                        goto case "old";
                    case "ranks":
                        message = "";
                    {
                        foreach (Group group in Group.groupList)
                        {
                            if (group.Permission != LevelPermission.Nobody)
                            {
                                Player.SendMessage(p,
                                                   string.Format("{0} - &bBlock limit: {1} - &cPermission: {2}", group.color + group.name, group.maxBlocks,
                                                                 (int)group.Permission));
                            }
                        }
                        return;
                    }
                    case "build":
                        message = "";
                        foreach (Command command in all.commands)
                        {
                            if ((p == null || p.group.commands.All().Contains(command)) && command.type.Contains("build"))
                            {
                                message = message + ", " + getColor(command.name) + command.name;
                            }
                        }
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
                        foreach (Command command2 in all.commands)
                        {
                            if ((p == null || p.group.commands.All().Contains(command2)) && command2.type.Contains("mod"))
                            {
                                message = message + ", " + getColor(command2.name) + command2.name;
                            }
                        }
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
                        foreach (Command command3 in all.commands)
                        {
                            if ((p == null || p.group.commands.All().Contains(command3)) && command3.type.Contains("info"))
                            {
                                message = message + ", " + getColor(command3.name) + command3.name;
                            }
                        }
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
                        foreach (Command command4 in all.commands)
                        {
                            if ((p == null || p.group.commands.All().Contains(command4)) && command4.type.Contains("other"))
                            {
                                message = message + ", " + getColor(command4.name) + command4.name;
                            }
                        }
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
                        foreach (Command command5 in all.commands)
                        {
                            if ((p == null || p.group.commands.All().Contains(command5)) && command5.shortcut != "")
                            {
                                string text = message;
                                message = text + ", &b" + command5.shortcut + " " + Server.DefaultColor + "[" + command5.name + "]";
                            }
                        }
                        Player.SendMessage(p, "Available shortcuts:");
                        Player.SendMessage(p, message.Remove(0, 2) + ".");
                        return;
                    case "old":
                    case "all":
                    {
                        string text2 = "";
                        foreach (Command command6 in all.commands)
                        {
                            if (p == null || p.group.commands.All().Contains(command6) && command6.IsWithinScope(p))
                            {
                                try
                                {
                                    text2 = text2 + ", " + command6.name;
                                }
                                catch {}
                            }
                        }
                        Player.SendMessage(p, "Available commands:");
                        Player.SendMessage(p, text2.Remove(0, 2));
                        Player.SendMessage(p, "Type \"/help <command>\" for more help.");
                        Player.SendMessage(p, "Type \"/help shortcuts\" for shortcuts.");
                        return;
                    }
                }
                Command cmd = all.Find(message);
                if (cmd != null)
                {
                    cmd.Help(p);
                    string text3 = Level.PermissionToName(GrpCommands.allowedCommands.Find(grpComm => grpComm.commandName == cmd.name).lowestRank);
                    Player.SendMessage(p, string.Format("Rank needed: {0}", getColor(cmd.name) + text3));
                    return;
                }
                byte b = Block.Byte(message);
                if (b != byte.MaxValue)
                {
                    Player.SendMessage(p, string.Format("Block \"{0}\" appears as &b{1}", message, Block.Name(Block.Convert(b))));
                    string arg = Level.PermissionToName(Block.BlockList.Find(bs => bs.type == b).lowestRank);
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

        string getColor(string commName)
        {
            foreach (GrpCommands.rankAllowance allowedCommand in GrpCommands.allowedCommands)
            {
                if (allowedCommand.commandName == commName && Group.findPerm(allowedCommand.lowestRank) != null)
                {
                    return Group.findPerm(allowedCommand.lowestRank).color;
                }
            }
            return "&f";
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "...really? Wow. Just...wow.");
        }
    }
}