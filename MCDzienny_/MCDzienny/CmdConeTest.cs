using MCDzienny.Misc;

namespace MCDzienny
{
    public class CmdConeTest : Command
    {
        public override string name { get { return "cone"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            Message message2 = new Message(message);
            int num = 0;
            int num2 = 0;
            if (!message2.IsNextInt())
            {
                string text = ReadConeRadius(p);
                while (true)
                {
                    if (text == null)
                    {
                        return;
                    }
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
                        break;
                    }
                    Player.SendMessage(p, "Radius has to be greater than 0.");
                    text = ReadConeRadius(p);
                }
                Player.SendMessage(p, "Cone radius: %a" + num);
                string text2 = ReadConeHeight(p);
                while (true)
                {
                    if (text2 == null)
                    {
                        return;
                    }
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
                        break;
                    }
                    Player.SendMessage(p, "Height has to be greater than 0.");
                    text2 = ReadConeHeight(p);
                }
                Player.SendMessage(p, "Cone height: %a" + num2);
            }
            else
            {
                num = message2.ReadInt();
                if (num <= 0)
                {
                    Player.SendMessage(p, "Incorrect radius value.");
                    return;
                }
                if (message2.IsNextInt())
                {
                    num2 = message2.ReadInt();
                }
                if (num2 <= 0)
                {
                    Player.SendMessage(p, "Incorrect height value.");
                    return;
                }
            }
            byte b = byte.MaxValue;
            string text3 = message2.ReadString();
            if (text3 != null)
            {
                b = Block.Parse(text3);
                if (b == byte.MaxValue)
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

        string ReadConeRadius(Player p)
        {
            Player.SendMessage(p, "Write cone radius:");
            return p.ReadLine();
        }

        string ReadConeHeight(Player p)
        {
            Player.SendMessage(p, "Write cone height:");
            return p.ReadLine();
        }

        void DrawCone(Player p, ChangeInfo ci, ExtendedDrawArgs da)
        {
            int integer = da.Integer;
            int num = da.Integers[0];
            int x = ci.X;
            int y = ci.Y;
            int z = ci.Z;
            byte b = da.Type1 == byte.MaxValue ? ci.Type : da.Type1;
            if (!Block.canPlace(p, b))
            {
                Player.SendMessage(p, "Cannot place this block type.");
                return;
            }
            Core.PrepareCone(p, integer, num, x, y, z, b);
            if (p.BlockChanges.Count > p.group.maxBlocks)
            {
                Player.SendMessage(p, "You tried to change {0} blocks. It's more than your current limit: {1}.", p.BlockChanges.Count, p.group.maxBlocks);
                p.BlockChanges.Abort();
                return;
            }
            int count = p.BlockChanges.Count;
            p.BlockChanges.Commit();
            Player.SendMessage(p, "You've built a cone of radius: {0} and height: {1}, which consists of {2} blocks.", integer + 1, num, count);
            if (p.staticCommands)
            {
                BlockCatch.CaptureOneBlock(p, DrawCone, da);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/cone - draws a cone.");
            Player.SendMessage(p, "For quick drawing:");
            Player.SendMessage(p, "/cone [radius] [height] <block>");
            Player.SendMessage(p, "/cone <block>");
        }
    }
}