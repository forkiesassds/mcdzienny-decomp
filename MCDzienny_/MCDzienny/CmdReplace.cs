using System;
using System.Collections.Generic;

namespace MCDzienny
{
    public class CmdReplace : Command
    {

        public override string name { get { return "replace"; } }

        public override string shortcut { get { return "r"; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (p.level.mapType == MapType.Lava && p.group.Permission < LevelPermission.Admin)
            {
                p.SendMessage("Only admin is allowed to use this command on lava map");
            }
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
            if (!Block.canPlace(p, b) && !Block.BuildIn(b))
            {
                Player.SendMessage(p, "Cannot replace that.");
                return;
            }
            if (!Block.canPlace(p, b2))
            {
                Player.SendMessage(p, "Cannot place that.");
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
            Player.SendMessage(p, "/replace [type] [type2] - replace type with type2 inside a selected cuboid");
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
            for (ushort num = Math.Min(cpos.x, x); num <= Math.Max(cpos.x, x); num++)
            {
                for (ushort num2 = Math.Min(cpos.y, y); num2 <= Math.Max(cpos.y, y); num2++)
                {
                    for (ushort num3 = Math.Min(cpos.z, z); num3 <= Math.Max(cpos.z, z); num3++)
                    {
                        if (p.level.GetTile(num, num2, num3) == type)
                        {
                            BufferAdd(list, num, num2, num3);
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
            list.ForEach(delegate(Pos pos) { p.level.Blockchange(p, pos.x, pos.y, pos.z, cpos.type2); });
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