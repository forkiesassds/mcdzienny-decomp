using System;
using System.Collections.Generic;

namespace MCDzienny
{
    public class CmdRainbow : Command
    {

        public override string name { get { return "rainbow"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            CatchPos catchPos = default(CatchPos);
            catchPos.x = 0;
            catchPos.y = 0;
            catchPos.z = 0;
            p.blockchangeObject = catchPos;
            Player.SendMessage(p, "Place two blocks to determine the edges.");
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/rainbow - Taste the rainbow");
        }

        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            CatchPos catchPos = (CatchPos)p.blockchangeObject;
            catchPos.x = x;
            catchPos.y = y;
            catchPos.z = z;
            p.blockchangeObject = catchPos;
            p.Blockchange += Blockchange2;
        }

        public void Blockchange2(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            CatchPos catchPos = (CatchPos)p.blockchangeObject;
            var list = new List<Pos>();
            byte b = 33;
            int num = Math.Abs(catchPos.x - x);
            int num2 = Math.Abs(catchPos.y - y);
            int num3 = Math.Abs(catchPos.z - z);
            if (num >= num2 && num >= num3)
            {
                for (ushort num4 = Math.Min(catchPos.x, x); num4 <= Math.Max(catchPos.x, x); num4++)
                {
                    b++;
                    if (b > 33)
                    {
                        b = 21;
                    }
                    for (ushort num5 = Math.Min(catchPos.y, y); num5 <= Math.Max(catchPos.y, y); num5++)
                    {
                        for (ushort num6 = Math.Min(catchPos.z, z); num6 <= Math.Max(catchPos.z, z); num6++)
                        {
                            if (p.level.GetTile(num4, num5, num6) != 0)
                            {
                                BufferAdd(list, num4, num5, num6, b);
                            }
                        }
                    }
                }
            }
            else if (num2 > num && num2 > num3)
            {
                for (ushort num7 = Math.Min(catchPos.y, y); num7 <= Math.Max(catchPos.y, y); num7++)
                {
                    b++;
                    if (b > 33)
                    {
                        b = 21;
                    }
                    for (ushort num8 = Math.Min(catchPos.x, x); num8 <= Math.Max(catchPos.x, x); num8++)
                    {
                        for (ushort num9 = Math.Min(catchPos.z, z); num9 <= Math.Max(catchPos.z, z); num9++)
                        {
                            if (p.level.GetTile(num8, num7, num9) != 0)
                            {
                                BufferAdd(list, num8, num7, num9, b);
                            }
                        }
                    }
                }
            }
            else if (num3 > num2 && num3 > num)
            {
                for (ushort num10 = Math.Min(catchPos.z, z); num10 <= Math.Max(catchPos.z, z); num10++)
                {
                    b++;
                    if (b > 33)
                    {
                        b = 21;
                    }
                    for (ushort num11 = Math.Min(catchPos.y, y); num11 <= Math.Max(catchPos.y, y); num11++)
                    {
                        for (ushort num12 = Math.Min(catchPos.x, x); num12 <= Math.Max(catchPos.x, x); num12++)
                        {
                            if (p.level.GetTile(num12, num11, num10) != 0)
                            {
                                BufferAdd(list, num12, num11, num10, b);
                            }
                        }
                    }
                }
            }
            if (list.Count > p.group.maxBlocks)
            {
                Player.SendMessage(p, string.Format("You tried to replace {0} blocks.", list.Count));
                Player.SendMessage(p, string.Format("You cannot replace more than {0}.", p.group.maxBlocks));
                return;
            }
            Player.SendMessage(p, string.Format("{0} blocks.", list.Count.ToString()));
            list.ForEach(delegate(Pos pos) { p.level.Blockchange(p, pos.x, pos.y, pos.z, pos.newType); });
            if (p.staticCommands)
            {
                p.Blockchange += Blockchange1;
            }
        }

        void BufferAdd(List<Pos> list, ushort x, ushort y, ushort z, byte newType)
        {
            Pos item = default(Pos);
            item.x = x;
            item.y = y;
            item.z = z;
            item.newType = newType;
            list.Add(item);
        }

        struct Pos
        {
            public ushort x;

            public ushort y;

            public ushort z;

            public byte newType;
        }

        struct CatchPos
        {
            public ushort x;

            public ushort y;

            public ushort z;
        }
    }
}