using System;

namespace MCDzienny
{
    public class CmdBlocks : Command
    {
        public override string name { get { return "blocks"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "information"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override string CustomName { get { return Lang.Command.BlocksName; } }

        public override void Use(Player p, string message)
        {
            try
            {
                if (message == "")
                {
                    Player.SendMessage(p, Lang.Command.BlocksMessage);
                    for (byte b = 0; b < 50; b++)
                    {
                        message = message + ", " + Block.Name(b);
                    }
                    Player.SendMessage(p, message.Remove(0, 2));
                    Player.SendMessage(p, string.Format(Lang.Command.BlocksMessage1, Server.DefaultColor));
                    return;
                }
                if (message.ToLower() == Lang.Command.BlocksParameter)
                {
                    Player.SendMessage(p, Lang.Command.BlocksMessage2);
                    for (byte b2 = 50; b2 < byte.MaxValue; b2++)
                    {
                        if (Block.Name(b2).ToLower() != "unknown")
                        {
                            message = message + ", " + Block.Name(b2);
                        }
                    }
                    Player.SendMessage(p, message.Remove(0, 2));
                    Player.SendMessage(p, string.Format(Lang.Command.BlocksMessage3, Server.DefaultColor));
                    return;
                }
                if (message.ToLower().IndexOf(' ') != -1 && message.Split(' ')[0] == Lang.Command.BlocksParameter)
                {
                    int num = 0;
                    try
                    {
                        num = int.Parse(message.Split(' ')[1]);
                    }
                    catch
                    {
                        Player.SendMessage(p, Lang.Command.BlocksMessage4);
                        return;
                    }
                    if (num >= 5 || num < 0)
                    {
                        Player.SendMessage(p, Lang.Command.BlocksMessage5);
                        return;
                    }
                    message = "";
                    Player.SendMessage(p, string.Format(Lang.Command.BlocksMessage6, num * 51, (num + 1) * 51));
                    for (byte b3 = (byte)(num * 51); b3 < (byte)((num + 1) * 51); b3++)
                    {
                        if (Block.Name(b3).ToLower() != "unknown")
                        {
                            message = message + ", " + Block.Name(b3);
                        }
                    }
                    Player.SendMessage(p, message.Remove(0, 2));
                    return;
                }
                string text = ">>>&b";
                if (Block.Byte(message) != byte.MaxValue)
                {
                    byte b4 = Block.Byte(message);
                    if (b4 < 51)
                    {
                        for (byte b5 = 51; b5 < byte.MaxValue; b5++)
                        {
                            if (Block.Convert(b5) == b4)
                            {
                                text = text + Block.Name(b5) + ", ";
                            }
                        }
                        if (text != ">>>&b")
                        {
                            Player.SendMessage(p, string.Format(Lang.Command.BlocksMessage7, message));
                            Player.SendMessage(p, text.Remove(text.Length - 2));
                        }
                        else
                        {
                            Player.SendMessage(p, string.Format(Lang.Command.BlocksMessage8, message));
                        }
                        return;
                    }
                    Player.SendMessage(p, string.Format(Lang.Command.BlocksMessage9, message));
                    Player.SendMessage(p, string.Format(Lang.Command.BlocksMessage10, Block.Name(Block.Convert(b4))));
                    if (Block.LightPass(b4))
                    {
                        Player.SendMessage(p, Lang.Command.BlocksMessage11);
                    }
                    if (Block.Physics(b4))
                    {
                        Player.SendMessage(p, Lang.Command.BlocksMessage12);
                    }
                    else
                    {
                        Player.SendMessage(p, Lang.Command.BlocksMessage13);
                    }
                    if (Block.NeedRestart(b4))
                    {
                        Player.SendMessage(p, Lang.Command.BlocksMessage14);
                    }
                    if (Block.OPBlocks(b4))
                    {
                        Player.SendMessage(p, Lang.Command.BlocksMessage15);
                    }
                    if (Block.AllowBreak(b4))
                    {
                        Player.SendMessage(p, Lang.Command.BlocksMessage16);
                    }
                    if (Block.Walkthrough(b4))
                    {
                        Player.SendMessage(p, Lang.Command.BlocksMessage17);
                    }
                    if (Block.Death(b4))
                    {
                        Player.SendMessage(p, Lang.Command.BlocksMessage18);
                    }
                    if (Block.DoorAirs(b4) != 0)
                    {
                        Player.SendMessage(p, Lang.Command.BlocksMessage19);
                    }
                    if (Block.tDoor(b4))
                    {
                        Player.SendMessage(p, Lang.Command.BlocksMessage20);
                    }
                    if (Block.odoor(b4) != byte.MaxValue)
                    {
                        Player.SendMessage(p, Lang.Command.BlocksMessage21);
                    }
                    if (Block.Mover(b4))
                    {
                        Player.SendMessage(p, Lang.Command.BlocksMessage22);
                    }
                }
                else if (Group.Find(message) != null)
                {
                    LevelPermission permission = Group.Find(message).Permission;
                    foreach (Block.Blocks block in Block.BlockList)
                    {
                        if (Block.canPlace(permission, block.type) && Block.Name(block.type).ToLower() != "unknown")
                        {
                            text = text + Block.Name(block.type) + ", ";
                        }
                    }
                    if (text != ">>>&b")
                    {
                        Player.SendMessage(p, string.Format(Lang.Command.BlocksMessage23, Group.Find(message).color + Group.Find(message).name + Server.DefaultColor));
                        Player.SendMessage(p, text.Remove(text.Length - 2));
                    }
                    else
                    {
                        Player.SendMessage(p, Lang.Command.BlocksMessage24);
                    }
                }
                else if (message.IndexOf(' ') == -1)
                {
                    if (message.ToLower() == Lang.Command.BlocksParameter1)
                    {
                        Player.SendMessage(p, string.Format(Lang.Command.BlocksMessage25, p.level.blocks.Length));
                    }
                    else
                    {
                        Help(p);
                    }
                }
                else if (message.Split(' ')[0].ToLower() == Lang.Command.BlocksParameter1)
                {
                    int num2 = 0;
                    byte b6 = Block.Byte(message.Split(' ')[1]);
                    if (b6 == byte.MaxValue)
                    {
                        Player.SendMessage(p, Lang.Command.BlocksMessage26);
                        return;
                    }
                    for (int i = 0; i < p.level.blocks.Length; i++)
                    {
                        if (b6 == p.level.blocks[i])
                        {
                            num2++;
                        }
                    }
                    switch (num2)
                    {
                        case 0:
                            Player.SendMessage(p, string.Format(Lang.Command.BlocksMessage27, message.Split(' ')[1]));
                            break;
                        case 1:
                            Player.SendMessage(p, string.Format(Lang.Command.BlocksMessage28, message.Split(' ')[1]));
                            break;
                        default:
                            Player.SendMessage(p, string.Format(Lang.Command.BlocksMessage29, num2.ToString(), message.Split(' ')[1]));
                            break;
                    }
                }
                else
                {
                    Player.SendMessage(p, Lang.Command.BlocksMessage30);
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
                Help(p);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.BlocksHelp);
            Player.SendMessage(p, Lang.Command.BlocksHelp1);
            Player.SendMessage(p, Lang.Command.BlocksHelp2);
            Player.SendMessage(p, Lang.Command.BlocksHelp3);
            Player.SendMessage(p, Lang.Command.BlocksHelp4);
            Player.SendMessage(p, ">> " + Group.concatList());
            Player.SendMessage(p, Lang.Command.BlocksHelp5);
        }
    }
}