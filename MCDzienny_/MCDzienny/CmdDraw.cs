using System;
using System.Collections.Generic;
using MCDzienny.Misc;

namespace MCDzienny
{
    public class CmdDraw : Command
    {

        public override string name { get { return "draw"; } }

        public override string shortcut { get { return "d"; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            Message message2 = new Message(message);
            switch (message2.ReadStringLower())
            {
                case "cone":
                    DrawCone(p, message2);
                    break;
                case "pillar":
                case "pillars":
                {
                    string text;
                    if ((text = message2.ReadStringLower()) != null && text == "2")
                    {
                        DrawPillarType2(p, message);
                    }
                    else
                    {
                        DrawPillarType1(p, message);
                    }
                    break;
                }
                default:
                    Help(p);
                    break;
            }
        }

        public void DrawPillarType1(Player p, string message)
        {
            byte b = byte.MaxValue;
            byte b2 = byte.MaxValue;
            string[] array = message.Split(' ');
            if (message == "")
            {
                b = 7;
                b2 = b;
            }
            else if (array.Length == 1)
            {
                b = Block.Byte(array[0]);
                b2 = b;
            }
            else
            {
                b = Block.Byte(array[0]);
                b2 = Block.Byte(array[1]);
            }
            if (b == byte.MaxValue || b2 == byte.MaxValue)
            {
                Player.SendMessage(p, "Incorrect block name given.");
            }
            else if (!Block.canPlace(p, b) || !Block.canPlace(p, b2))
            {
                Player.SendMessage(p, "Cannot place this block type.");
            }
            else
            {
                BlockCatch.CaptureTwoBlocks(p, DrawPillarType1, new BasicDrawArgs(b, b2, 2));
            }
        }

        public void DrawPillarType2(Player p, string message)
        {
            byte b = byte.MaxValue;
            byte b2 = byte.MaxValue;
            string[] array = message.Split(' ');
            if (message == "")
            {
                b = 7;
                b2 = b;
            }
            else if (array.Length == 1)
            {
                b = Block.Byte(array[0]);
                b2 = b;
            }
            else
            {
                b = Block.Byte(array[0]);
                b2 = Block.Byte(array[1]);
            }
            if (b == byte.MaxValue || b2 == byte.MaxValue)
            {
                Player.SendMessage(p, "Incorrect block name given.");
            }
            else if (!Block.canPlace(p, b) || !Block.canPlace(p, b2))
            {
                Player.SendMessage(p, "Cannot place this block type.");
            }
            else
            {
                BlockCatch.CaptureTwoBlocks(p, DrawPillarType1, new BasicDrawArgs(b, b2, 3));
            }
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

        public void DrawCone(Player p, Message msg)
        {
            int num = 0;
            int num2 = 0;
            if (!msg.IsNextInt())
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
            }
            else
            {
                num = msg.ReadInt();
                if (num <= 0)
                {
                    Player.SendMessage(p, "Incorrect radius value.");
                    return;
                }
                if (msg.IsNextInt())
                {
                    num2 = msg.ReadInt();
                }
                if (num2 <= 0)
                {
                    Player.SendMessage(p, "Incorrect height value.");
                    return;
                }
            }
            byte b = byte.MaxValue;
            string text3 = msg.ReadString();
            if (text3 != null)
            {
                b = Block.Parse(text3);
                if (b == byte.MaxValue)
                {
                    Player.SendMessage(p, "Unknown block type: " + text3);
                    return;
                }
            }
            BlockCatch.CaptureOneBlock(p, DrawCone, new ExtendedDrawArgs(b, num, num2));
        }

        string GetOrdinalNumber(int number)
        {
            switch (number)
            {
                default:
                    throw new ArgumentException("The argument is out of the allowed range.", "number");
                case 0:
                    return "zeroth";
                case 1:
                    return "first";
                case 2:
                    return "second";
                case 3:
                    return "third";
                case 4:
                    return "forth";
                case 5:
                    return "fifth";
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                    return number + "th";
            }
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
                Player.SendMessage(p, "You tried to change " + p.BlockChanges.Count + " blocks. It's more than your current limit: " + p.group.maxBlocks + ".");
                p.BlockChanges.Abort();
            }
            else
            {
                int count = p.BlockChanges.Count;
                p.BlockChanges.Commit();
                Player.SendMessage(p, "You've built a cone of radius: {0} and height: {1}, which consists of {2} blocks.", integer, num, count);
            }
        }

        void DrawPillarType1(Player p, ChangeInfo ci1, ChangeInfo ci2, BasicDrawArgs da)
        {
            bool flag = false;
            if (Math.Abs(ci1.X - ci2.X) > Math.Abs(ci1.Z - ci2.Z))
            {
                flag = true;
            }
            float a = (ci1.Z - ci2.Z) / (float)(ci1.X - ci2.X);
            float b = ci1.Z - a * ci1.X;
            int max = 0;
            int min = 0;
            bool flag2 = false;
            if (flag)
            {
                max = Math.Max(ci1.X, ci2.X);
                min = Math.Min(ci1.X, ci2.X);
                flag2 = min == ci2.X;
            }
            else
            {
                max = Math.Max(ci1.Z, ci2.Z);
                min = Math.Min(ci1.Z, ci2.Z);
                flag2 = min == ci2.Z;
            }
            int num = max - min + 1;
            if (flag)
            {
                Func<int, ushort> func = !flag2 ? i => (ushort)(min + i) : (Func<int, ushort>)(i => (ushort)(max - i));
                for (int j = 0; j < num; j++)
                {
                    if (j % da.Integer != 0)
                    {
                        continue;
                    }
                    ushort num2 = func(j);
                    ushort num3 = (ushort)(j / da.Integer + ci1.Y);
                    ushort z = (ushort)Math.Round(a * num2 + b);
                    p.level.Blockchange(p, num2, num3, z, da.Type1);
                    for (int num4 = j / da.Integer; num4 > 0; num4--)
                    {
                        ushort y2 = (ushort)(num3 - (j / da.Integer - num4 + 1));
                        byte tile = p.level.GetTile(num2, y2, z);
                        if (!Block.IsAir(tile) && !Block.Walkthrough(tile))
                        {
                            break;
                        }
                        p.level.Blockchange(p, num2, y2, z, da.Type2);
                    }
                }
                return;
            }
            Func<int, ushort> func2 = !flag2 ? i => (ushort)(min + i) : (Func<int, ushort>)(i => (ushort)(max - i));
            Func<ushort, float> func3 = !float.IsInfinity(a) ? y => (y - b) / a : (Func<ushort, float>)(y => ci1.X);
            for (int k = 0; k < num; k++)
            {
                if (k % da.Integer != 0)
                {
                    continue;
                }
                ushort num5 = func2(k);
                ushort x = (ushort)Math.Round(func3(num5));
                ushort num6 = (ushort)(k / da.Integer + ci1.Y);
                p.level.Blockchange(p, x, num6, num5, da.Type1);
                for (int num7 = k / da.Integer; num7 > 0; num7--)
                {
                    ushort y3 = (ushort)(num6 - 1 - (k / da.Integer - num7));
                    byte tile2 = p.level.GetTile(x, y3, num5);
                    if (!Block.IsAir(tile2) && !Block.Walkthrough(tile2))
                    {
                        break;
                    }
                    p.level.Blockchange(p, x, y3, num5, da.Type2);
                }
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/draw cone - draws a cone.");
            Player.SendMessage(p, "/draw cone [radius] [height] <block> - for quick drawing.");
            Player.SendMessage(p, "/d - shortcut for /draw");
        }

        delegate void TwoPointsDraw<T>(Player p, ChangeInfo changeInfo1, ChangeInfo changeInfo2, T drawArgs);

        delegate void OnePointDraw<T>(Player p, ChangeInfo changeInfo, T drawArgs);

        delegate void MultiplePointsDraw<T>(Player p, List<ChangeInfo> changeInfoList, T drawArgs);
    }
}