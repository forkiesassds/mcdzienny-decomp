using System;

namespace MCDzienny
{
    // Token: 0x020002EE RID: 750
    public class CmdBlocks : Command
    {
        // Token: 0x1700085C RID: 2140
        // (get) Token: 0x06001540 RID: 5440 RVA: 0x000750C0 File Offset: 0x000732C0
        public override string name
        {
            get { return "blocks"; }
        }

        // Token: 0x1700085D RID: 2141
        // (get) Token: 0x06001541 RID: 5441 RVA: 0x000750C8 File Offset: 0x000732C8
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700085E RID: 2142
        // (get) Token: 0x06001542 RID: 5442 RVA: 0x000750D0 File Offset: 0x000732D0
        public override string type
        {
            get { return "information"; }
        }

        // Token: 0x1700085F RID: 2143
        // (get) Token: 0x06001543 RID: 5443 RVA: 0x000750D8 File Offset: 0x000732D8
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000860 RID: 2144
        // (get) Token: 0x06001544 RID: 5444 RVA: 0x000750DC File Offset: 0x000732DC
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x17000861 RID: 2145
        // (get) Token: 0x06001545 RID: 5445 RVA: 0x000750E0 File Offset: 0x000732E0
        public override string CustomName
        {
            get { return Lang.Command.BlocksName; }
        }

        // Token: 0x06001547 RID: 5447 RVA: 0x000750F0 File Offset: 0x000732F0
        public override void Use(Player p, string message)
        {
            try
            {
                if (message == "")
                {
                    Player.SendMessage(p, Lang.Command.BlocksMessage);
                    for (byte b = 0; b < 50; b += 1) message = message + ", " + Block.Name(b);
                    Player.SendMessage(p, message.Remove(0, 2));
                    Player.SendMessage(p, string.Format(Lang.Command.BlocksMessage1, Server.DefaultColor));
                }
                else if (message.ToLower() == Lang.Command.BlocksParameter)
                {
                    Player.SendMessage(p, Lang.Command.BlocksMessage2);
                    for (byte b2 = 50; b2 < 255; b2 += 1)
                        if (Block.Name(b2).ToLower() != "unknown")
                            message = message + ", " + Block.Name(b2);
                    Player.SendMessage(p, message.Remove(0, 2));
                    Player.SendMessage(p, string.Format(Lang.Command.BlocksMessage3, Server.DefaultColor));
                }
                else if (message.ToLower().IndexOf(' ') != -1 && message.Split(' ')[0] == Lang.Command.BlocksParameter)
                {
                    var num = 0;
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
                    }
                    else
                    {
                        message = "";
                        Player.SendMessage(p, string.Format(Lang.Command.BlocksMessage6, num * 51, (num + 1) * 51));
                        for (var b3 = (byte) (num * 51); b3 < (byte) ((num + 1) * 51); b3 += 1)
                            if (Block.Name(b3).ToLower() != "unknown")
                                message = message + ", " + Block.Name(b3);
                        Player.SendMessage(p, message.Remove(0, 2));
                    }
                }
                else
                {
                    var text = ">>>&b";
                    if (Block.Byte(message) != 255)
                    {
                        var b4 = Block.Byte(message);
                        if (b4 < 51)
                        {
                            for (byte b5 = 51; b5 < 255; b5 += 1)
                                if (Block.Convert(b5) == b4)
                                    text = text + Block.Name(b5) + ", ";
                            if (text != ">>>&b")
                            {
                                Player.SendMessage(p, string.Format(Lang.Command.BlocksMessage7, message));
                                Player.SendMessage(p, text.Remove(text.Length - 2));
                            }
                            else
                            {
                                Player.SendMessage(p, string.Format(Lang.Command.BlocksMessage8, message));
                            }
                        }
                        else
                        {
                            Player.SendMessage(p, string.Format(Lang.Command.BlocksMessage9, message));
                            Player.SendMessage(p,
                                string.Format(Lang.Command.BlocksMessage10, Block.Name(Block.Convert(b4))));
                            if (Block.LightPass(b4)) Player.SendMessage(p, Lang.Command.BlocksMessage11);
                            if (Block.Physics(b4))
                                Player.SendMessage(p, Lang.Command.BlocksMessage12);
                            else
                                Player.SendMessage(p, Lang.Command.BlocksMessage13);
                            if (Block.NeedRestart(b4)) Player.SendMessage(p, Lang.Command.BlocksMessage14);
                            if (Block.OPBlocks(b4)) Player.SendMessage(p, Lang.Command.BlocksMessage15);
                            if (Block.AllowBreak(b4)) Player.SendMessage(p, Lang.Command.BlocksMessage16);
                            if (Block.Walkthrough(b4)) Player.SendMessage(p, Lang.Command.BlocksMessage17);
                            if (Block.Death(b4)) Player.SendMessage(p, Lang.Command.BlocksMessage18);
                            if (Block.DoorAirs(b4) != 0) Player.SendMessage(p, Lang.Command.BlocksMessage19);
                            if (Block.tDoor(b4)) Player.SendMessage(p, Lang.Command.BlocksMessage20);
                            if (Block.odoor(b4) != 255) Player.SendMessage(p, Lang.Command.BlocksMessage21);
                            if (Block.Mover(b4)) Player.SendMessage(p, Lang.Command.BlocksMessage22);
                        }
                    }
                    else if (Group.Find(message) != null)
                    {
                        var permission = Group.Find(message).Permission;
                        foreach (var blocks in Block.BlockList)
                            if (Block.canPlace(permission, blocks.type) &&
                                Block.Name(blocks.type).ToLower() != "unknown")
                                text = text + Block.Name(blocks.type) + ", ";
                        if (text != ">>>&b")
                        {
                            Player.SendMessage(p,
                                string.Format(Lang.Command.BlocksMessage23,
                                    Group.Find(message).color + Group.Find(message).name + Server.DefaultColor));
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
                            Player.SendMessage(p, string.Format(Lang.Command.BlocksMessage25, p.level.blocks.Length));
                        else
                            Help(p);
                    }
                    else if (message.Split(' ')[0].ToLower() == Lang.Command.BlocksParameter1)
                    {
                        var num2 = 0;
                        var b6 = Block.Byte(message.Split(' ')[1]);
                        if (b6 == 255)
                        {
                            Player.SendMessage(p, Lang.Command.BlocksMessage26);
                        }
                        else
                        {
                            for (var i = 0; i < p.level.blocks.Length; i++)
                                if (b6 == p.level.blocks[i])
                                    num2++;
                            if (num2 == 0)
                                Player.SendMessage(p,
                                    string.Format(Lang.Command.BlocksMessage27, message.Split(' ')[1]));
                            else if (num2 == 1)
                                Player.SendMessage(p,
                                    string.Format(Lang.Command.BlocksMessage28, message.Split(' ')[1]));
                            else
                                Player.SendMessage(p,
                                    string.Format(Lang.Command.BlocksMessage29, num2.ToString(),
                                        message.Split(' ')[1]));
                        }
                    }
                    else
                    {
                        Player.SendMessage(p, Lang.Command.BlocksMessage30);
                    }
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
                Help(p);
            }
        }

        // Token: 0x06001548 RID: 5448 RVA: 0x00075790 File Offset: 0x00073990
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