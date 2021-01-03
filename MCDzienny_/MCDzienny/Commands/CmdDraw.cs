using System;
using System.Collections.Generic;
using MCDzienny.Misc;

namespace MCDzienny
{
    // Token: 0x02000088 RID: 136
    public class CmdDraw : Command
    {
        // Token: 0x17000118 RID: 280
        // (get) Token: 0x06000395 RID: 917 RVA: 0x000131B4 File Offset: 0x000113B4
        public override string name
        {
            get { return "draw"; }
        }

        // Token: 0x17000119 RID: 281
        // (get) Token: 0x06000396 RID: 918 RVA: 0x000131BC File Offset: 0x000113BC
        public override string shortcut
        {
            get { return "d"; }
        }

        // Token: 0x1700011A RID: 282
        // (get) Token: 0x06000397 RID: 919 RVA: 0x000131C4 File Offset: 0x000113C4
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x1700011B RID: 283
        // (get) Token: 0x06000398 RID: 920 RVA: 0x000131CC File Offset: 0x000113CC
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x1700011C RID: 284
        // (get) Token: 0x06000399 RID: 921 RVA: 0x000131D0 File Offset: 0x000113D0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x1700011D RID: 285
        // (get) Token: 0x0600039A RID: 922 RVA: 0x000131D4 File Offset: 0x000113D4
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x0600039B RID: 923 RVA: 0x000131D8 File Offset: 0x000113D8
        public override void Use(Player p, string message)
        {
            var message2 = new Message(message);
            string a;
            if ((a = message2.ReadStringLower()) != null)
            {
                if (a == "cone")
                {
                    DrawCone(p, message2);
                    return;
                }

                if (a == "pillar" || a == "pillars")
                {
                    string a2;
                    if ((a2 = message2.ReadStringLower()) != null && a2 == "2")
                    {
                        DrawPillarType2(p, message);
                        return;
                    }

                    DrawPillarType1(p, message);
                    return;
                }
            }

            Help(p);
        }

        // Token: 0x0600039C RID: 924 RVA: 0x00013258 File Offset: 0x00011458
        public void DrawPillarType1(Player p, string message)
        {
            var array = message.Split(' ');
            byte b;
            byte b2;
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

            if (b == 255 || b2 == 255)
            {
                Player.SendMessage(p, "Incorrect block name given.");
                return;
            }

            if (!Block.canPlace(p, b) || !Block.canPlace(p, b2))
            {
                Player.SendMessage(p, "Cannot place this block type.");
                return;
            }

            BlockCatch.CaptureTwoBlocks(p, DrawPillarType1, new BasicDrawArgs(b, b2, 2));
        }

        // Token: 0x0600039D RID: 925 RVA: 0x00013314 File Offset: 0x00011514
        public void DrawPillarType2(Player p, string message)
        {
            var array = message.Split(' ');
            byte b;
            byte b2;
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

            if (b == 255 || b2 == 255)
            {
                Player.SendMessage(p, "Incorrect block name given.");
                return;
            }

            if (!Block.canPlace(p, b) || !Block.canPlace(p, b2))
            {
                Player.SendMessage(p, "Cannot place this block type.");
                return;
            }

            BlockCatch.CaptureTwoBlocks(p, DrawPillarType1, new BasicDrawArgs(b, b2, 3));
        }

        // Token: 0x0600039E RID: 926 RVA: 0x000133D0 File Offset: 0x000115D0
        private string ReadConeRadius(Player p)
        {
            Player.SendMessage(p, "Write cone radius:");
            return p.ReadLine();
        }

        // Token: 0x0600039F RID: 927 RVA: 0x000133E4 File Offset: 0x000115E4
        private string ReadConeHeight(Player p)
        {
            Player.SendMessage(p, "Write cone height:");
            return p.ReadLine();
        }

        // Token: 0x060003A0 RID: 928 RVA: 0x000133F8 File Offset: 0x000115F8
        public void DrawCone(Player p, Message msg)
        {
            var num = 0;
            var num2 = 0;
            if (!msg.IsNextInt())
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

                            if (num2 > 0) goto IL_D7;
                            Player.SendMessage(p, "Height has to be greater than 0.");
                        }

                        return;
                    }

                    Player.SendMessage(p, "Radius has to be greater than 0.");
                }

                return;
            }

            num = msg.ReadInt();
            if (num <= 0)
            {
                Player.SendMessage(p, "Incorrect radius value.");
                return;
            }

            if (msg.IsNextInt()) num2 = msg.ReadInt();
            if (num2 <= 0)
            {
                Player.SendMessage(p, "Incorrect height value.");
                return;
            }

            IL_D7:
            var b = byte.MaxValue;
            var text3 = msg.ReadString();
            if (text3 != null)
            {
                b = Block.Parse(text3);
                if (b == 255)
                {
                    Player.SendMessage(p, "Unknown block type: " + text3);
                    return;
                }
            }

            BlockCatch.CaptureOneBlock(p, DrawCone, new ExtendedDrawArgs(b, num, num2));
        }

        // Token: 0x060003A1 RID: 929 RVA: 0x0001355C File Offset: 0x0001175C
        private string GetOrdinalNumber(int number)
        {
            if (number < 0 || number > 20)
                throw new ArgumentException("The argument is out of the allowed range.", "number");
            if (number == 0) return "zeroth";
            if (number == 1) return "first";
            if (number == 2) return "second";
            if (number == 3) return "third";
            if (number == 4) return "forth";
            if (number == 5) return "fifth";
            return number + "th";
        }

        // Token: 0x060003A2 RID: 930 RVA: 0x000135D0 File Offset: 0x000117D0
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
                Player.SendMessage(p,
                    string.Concat("You tried to change ", p.BlockChanges.Count,
                        " blocks. It's more than your current limit: ", p.group.maxBlocks, "."));
                p.BlockChanges.Abort();
                return;
            }

            var count = p.BlockChanges.Count;
            p.BlockChanges.Commit();
            Player.SendMessage(p, "You've built a cone of radius: {0} and height: {1}, which consists of {2} blocks.",
                integer, num, count);
        }

        // Token: 0x060003A3 RID: 931 RVA: 0x00013710 File Offset: 0x00011910
        private void DrawPillarType1(Player p, ChangeInfo ci1, ChangeInfo ci2, BasicDrawArgs da)
        {
            var flag = false;
            if (Math.Abs(ci1.X - ci2.X) > Math.Abs(ci1.Z - ci2.Z)) flag = true;
            var a = (ci1.Z - ci2.Z) / (float) (ci1.X - ci2.X);
            var b = ci1.Z - a * ci1.X;
            var max = 0;
            var min = 0;
            bool flag2;
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

            var num = max - min + 1;
            if (flag)
            {
                Func<int, ushort> func;
                if (flag2)
                    func = i => (ushort) (max - i);
                else
                    func = i => (ushort) (min + i);
                for (var m = 0; m < num; m++)
                    if (m % da.Integer == 0)
                    {
                        var num2 = func(m);
                        var num3 = (ushort) (m / da.Integer + ci1.Y);
                        var z = (ushort) Math.Round(a * num2 + b);
                        p.level.Blockchange(p, num2, num3, z, da.Type1);
                        for (var j = m / da.Integer; j > 0; j--)
                        {
                            var y3 = (ushort) (num3 - (m / da.Integer - j + 1));
                            var tile = p.level.GetTile(num2, y3, z);
                            if (!Block.IsAir(tile) && !Block.Walkthrough(tile)) break;
                            p.level.Blockchange(p, num2, y3, z, da.Type2);
                        }
                    }

                return;
            }

            Func<int, ushort> func2;
            if (flag2)
                func2 = i => (ushort) (max - i);
            else
                func2 = i => (ushort) (min + i);
            Func<ushort, float> func3;
            if (float.IsInfinity(a))
                func3 = y => (float) ci1.X;
            else
                func3 = y => ((float) y - b) / a;
            for (var k = 0; k < num; k++)
                if (k % da.Integer == 0)
                {
                    var num4 = func2(k);
                    var x = (ushort) Math.Round(func3(num4));
                    var num5 = (ushort) (k / da.Integer + ci1.Y);
                    p.level.Blockchange(p, x, num5, num4, da.Type1);
                    for (var l = k / da.Integer; l > 0; l--)
                    {
                        var y2 = (ushort) (num5 - 1 - (k / da.Integer - l));
                        var tile2 = p.level.GetTile(x, y2, num4);
                        if (!Block.IsAir(tile2) && !Block.Walkthrough(tile2)) break;
                        p.level.Blockchange(p, x, y2, num4, da.Type2);
                    }
                }
        }

        // Token: 0x060003A4 RID: 932 RVA: 0x00013B04 File Offset: 0x00011D04
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/draw cone - draws a cone.");
            Player.SendMessage(p, "/draw cone [radius] [height] <block> - for quick drawing.");
            Player.SendMessage(p, "/d - shortcut for /draw");
        }

        // Token: 0x02000089 RID: 137
        // (Invoke) Token: 0x060003A7 RID: 935
        private delegate void TwoPointsDraw<T>(Player p, ChangeInfo changeInfo1, ChangeInfo changeInfo2, T drawArgs);

        // Token: 0x0200008A RID: 138
        // (Invoke) Token: 0x060003AB RID: 939
        private delegate void OnePointDraw<T>(Player p, ChangeInfo changeInfo, T drawArgs);

        // Token: 0x0200008B RID: 139
        // (Invoke) Token: 0x060003AF RID: 943
        private delegate void MultiplePointsDraw<T>(Player p, List<ChangeInfo> changeInfoList, T drawArgs);
    }
}