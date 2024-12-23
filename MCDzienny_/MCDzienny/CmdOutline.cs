using System;
using System.Collections.Generic;

namespace MCDzienny
{
    public class CmdOutline : Command
    {

        public override string name { get { return "outline"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            int num = message.Split(' ').Length;
            if (num != 2)
            {
                Help(p);
                return;
            }
            int num2 = message.IndexOf(' ');
            string arg = message.Substring(0, num2).ToLower();
            string arg2 = message.Substring(num2 + 1).ToLower();
            byte b = Block.Byte(arg);
            if (b == byte.MaxValue)
            {
                Player.SendMessage(p, string.Format("There is no block \"{0}\".", arg));
                return;
            }
            byte b2 = Block.Byte(arg2);
            if (b2 == byte.MaxValue)
            {
                Player.SendMessage(p, string.Format("There is no block \"{0}\".", arg2));
                return;
            }
            if (!Block.canPlace(p, b2))
            {
                Player.SendMessage(p, "Cannot place that block type.");
                return;
            }
            CatchPos catchPos = default(CatchPos);
            catchPos.type2 = b2;
            catchPos.type = b;
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
            Player.SendMessage(p, "/outline [type] [type2] - Outlines [type] with [type2]");
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
            CatchPos cpos = (CatchPos)p.blockchangeObject;
            if (cpos.type != byte.MaxValue)
            {
                type = cpos.type;
            }
            var list = new List<Pos>();
            bool flag = false;
            Pos item = default(Pos);
            for (ushort num = Math.Min(cpos.x, x); num <= Math.Max(cpos.x, x); num++)
            {
                for (ushort num2 = Math.Min(cpos.y, y); num2 <= Math.Max(cpos.y, y); num2++)
                {
                    for (ushort num3 = Math.Min(cpos.z, z); num3 <= Math.Max(cpos.z, z); num3++)
                    {
                        flag = false;
                        if (p.level.GetTile((ushort)(num - 1), num2, num3) == cpos.type)
                        {
                            flag = true;
                        }
                        else if (p.level.GetTile((ushort)(num + 1), num2, num3) == cpos.type)
                        {
                            flag = true;
                        }
                        else if (p.level.GetTile(num, (ushort)(num2 - 1), num3) == cpos.type)
                        {
                            flag = true;
                        }
                        else if (p.level.GetTile(num, (ushort)(num2 + 1), num3) == cpos.type)
                        {
                            flag = true;
                        }
                        else if (p.level.GetTile(num, num2, (ushort)(num3 - 1)) == cpos.type)
                        {
                            flag = true;
                        }
                        else if (p.level.GetTile(num, num2, (ushort)(num3 + 1)) == cpos.type)
                        {
                            flag = true;
                        }
                        if (flag && p.level.GetTile(num, num2, num3) != cpos.type)
                        {
                            item.x = num;
                            item.y = num2;
                            item.z = num3;
                            list.Add(item);
                        }
                    }
                }
            }
            if (list.Count > p.group.maxBlocks)
            {
                Player.SendMessage(p, string.Format("You tried to outline more than {0} blocks.", list.Count));
                Player.SendMessage(p, string.Format("You cannot outline more than {0}", p.group.maxBlocks + ".)"));
                return;
            }
            list.ForEach(delegate(Pos pos1) { p.level.Blockchange(p, pos1.x, pos1.y, pos1.z, cpos.type2); });
            Player.SendMessage(p, string.Format("You outlined {0} blocks.", list.Count));
            if (p.staticCommands)
            {
                p.Blockchange += Blockchange1;
            }
        }

        void BufferAdd(List<Pos> list, ushort x, ushort y, ushort z)
        {
            Pos item = default(Pos);
            item.x = x;
            item.y = y;
            item.z = z;
            list.Add(item);
        }

        struct Pos
        {
            public ushort x;

            public ushort y;

            public ushort z;
        }

        struct CatchPos
        {
            public byte type;

            public byte type2;

            public ushort x;

            public ushort y;

            public ushort z;
        }
    }
}