using MCDzienny.Misc;

namespace MCDzienny
{
    // Token: 0x0200005C RID: 92
    public class CmdConeTest : Command
    {
        // Token: 0x17000083 RID: 131
        // (get) Token: 0x06000229 RID: 553 RVA: 0x0000C7C0 File Offset: 0x0000A9C0
        public override string name
        {
            get { return "cone"; }
        }

        // Token: 0x17000084 RID: 132
        // (get) Token: 0x0600022A RID: 554 RVA: 0x0000C7C8 File Offset: 0x0000A9C8
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000085 RID: 133
        // (get) Token: 0x0600022B RID: 555 RVA: 0x0000C7D0 File Offset: 0x0000A9D0
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x17000086 RID: 134
        // (get) Token: 0x0600022C RID: 556 RVA: 0x0000C7D8 File Offset: 0x0000A9D8
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000087 RID: 135
        // (get) Token: 0x0600022D RID: 557 RVA: 0x0000C7DC File Offset: 0x0000A9DC
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Builder; }
        }

        // Token: 0x17000088 RID: 136
        // (get) Token: 0x0600022E RID: 558 RVA: 0x0000C7E0 File Offset: 0x0000A9E0
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x0600022F RID: 559 RVA: 0x0000C7E4 File Offset: 0x0000A9E4
        public override void Use(Player p, string message)
        {
            var message2 = new Message(message);
            var num = 0;
            var num2 = 0;
            if (!message2.IsNextInt())
            {
                for (var text = ReadConeRadius(p); text != null; text = ReadConeRadius(p))
                {
                    try
                    {
                        num = int.Parse(text.Trim());
                    }
                    catch
                    {
                        Player.SendMessage(p, "Given value is not a number.");
                        text = ReadConeRadius(p);
                        continue;
                    }

                    if (num > 0)
                    {
                        Player.SendMessage(p, "Cone radius: %a" + num);
                        for (var text2 = ReadConeHeight(p); text2 != null; text2 = ReadConeHeight(p))
                        {
                            try
                            {
                                num2 = int.Parse(text2.Trim());
                            }
                            catch
                            {
                                Player.SendMessage(p, "Given value is not a number.");
                                text2 = ReadConeHeight(p);
                                continue;
                            }

                            if (num2 > 0)
                            {
                                Player.SendMessage(p, "Cone height: %a" + num2);
                                goto IL_113;
                            }

                            Player.SendMessage(p, "Height has to be greater than 0.");
                        }

                        return;
                    }

                    Player.SendMessage(p, "Radius has to be greater than 0.");
                }

                return;
            }

            num = message2.ReadInt();
            if (num <= 0)
            {
                Player.SendMessage(p, "Incorrect radius value.");
                return;
            }

            if (message2.IsNextInt()) num2 = message2.ReadInt();
            if (num2 <= 0)
            {
                Player.SendMessage(p, "Incorrect height value.");
                return;
            }

            IL_113:
            var b = byte.MaxValue;
            var text3 = message2.ReadString();
            if (text3 != null)
            {
                b = Block.Parse(text3);
                if (b == 255)
                {
                    Player.SendMessage(p, "Unknown block type: " + text3);
                    return;
                }

                if (!Block.canPlace(p, b))
                {
                    Player.SendMessage(p, "Cannot place this block type.");
                    return;
                }
            }

            BlockCatch.CaptureOneBlock(p, DrawCone, new ExtendedDrawArgs(b, num, num2));
        }

        // Token: 0x06000230 RID: 560 RVA: 0x0000C998 File Offset: 0x0000AB98
        private string ReadConeRadius(Player p)
        {
            Player.SendMessage(p, "Write cone radius:");
            return p.ReadLine();
        }

        // Token: 0x06000231 RID: 561 RVA: 0x0000C9AC File Offset: 0x0000ABAC
        private string ReadConeHeight(Player p)
        {
            Player.SendMessage(p, "Write cone height:");
            return p.ReadLine();
        }

        // Token: 0x06000232 RID: 562 RVA: 0x0000C9C0 File Offset: 0x0000ABC0
        private void DrawCone(Player p, ChangeInfo ci, ExtendedDrawArgs da)
        {
            var integer = da.Integer;
            var num = da.Integers[0];
            int x = ci.X;
            int y = ci.Y;
            int z = ci.Z;
            var b = da.Type1 == byte.MaxValue ? ci.Type : da.Type1;
            if (!Block.canPlace(p, b))
            {
                Player.SendMessage(p, "Cannot place this block type.");
                return;
            }

            Core.PrepareCone(p, integer, num, x, y, z, b);
            if (p.BlockChanges.Count > p.group.maxBlocks)
            {
                Player.SendMessage(p, "You tried to change {0} blocks. It's more than your current limit: {1}.",
                    p.BlockChanges.Count, p.group.maxBlocks);
                p.BlockChanges.Abort();
                return;
            }

            var count = p.BlockChanges.Count;
            p.BlockChanges.Commit();
            Player.SendMessage(p, "You've built a cone of radius: {0} and height: {1}, which consists of {2} blocks.",
                integer + 1, num, count);
            if (p.staticCommands) BlockCatch.CaptureOneBlock(p, DrawCone, da);
        }

        // Token: 0x06000233 RID: 563 RVA: 0x0000CB00 File Offset: 0x0000AD00
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/cone - draws a cone.");
            Player.SendMessage(p, "For quick drawing:");
            Player.SendMessage(p, "/cone [radius] [height] <block>");
            Player.SendMessage(p, "/cone <block>");
        }
    }
}