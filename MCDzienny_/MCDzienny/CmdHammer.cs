using System;
using System.Collections.Generic;

namespace MCDzienny
{
    public class CmdHammer : Command
    {

        public override string name { get { return "hammer"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override bool ConsoleAccess { get { return false; } }

        public override CommandScope Scope { get { return CommandScope.Lava; } }

        public override void Use(Player p, string message)
        {
            if (p.hammer <= 0)
            {
                Player.SendMessage(p, "You run out of hammer. Check the /store.");
                return;
            }
            CatchPos catchPos = default(CatchPos);
            catchPos.type = byte.MaxValue;
            catchPos.x = 0;
            catchPos.y = 0;
            catchPos.z = 0;
            p.blockchangeObject = catchPos;
            Player.SendMessage(p, "Place two blocks to determine the edges.");
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
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
            if (catchPos.type != byte.MaxValue)
            {
                type = catchPos.type;
            }
            else
            {
                type = p.bindings[type];
            }
            if (!Block.canPlace(p, type))
            {
                Player.SendMessage(p, "Cannot place that.");
                return;
            }
            var list = new List<Pos>();
            list.Capacity = Math.Abs(catchPos.x - x) * Math.Abs(catchPos.y - y) * Math.Abs(catchPos.z - z);
            for (ushort num = Math.Min(catchPos.x, x); num <= Math.Max(catchPos.x, x); num++)
            {
                for (ushort num2 = Math.Min(catchPos.y, y); num2 <= Math.Max(catchPos.y, y); num2++)
                {
                    for (ushort num3 = Math.Min(catchPos.z, z); num3 <= Math.Max(catchPos.z, z); num3++)
                    {
                        if (p.level.GetTile(num, num2, num3) != type)
                        {
                            BufferAdd(list, num, num2, num3);
                        }
                    }
                }
            }
            if (list.Count > p.hammer)
            {
                Player.SendMessage(p, string.Format("You tried to hammer {0} blocks.", list.Count));
                Player.SendMessage(p, string.Format("But your hammer may endure no more than {0}.", p.hammer));
                return;
            }
            p.hammer -= list.Count;
            Player.SendMessage(p, list.Count + " blocks.");
            list.ForEach(delegate(Pos pos) { p.level.Blockchange(p, pos.x, pos.y, pos.z, type); });
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/hammer - Allows you to build faster.");
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

            public ushort x;

            public ushort y;

            public ushort z;
        }
    }
}