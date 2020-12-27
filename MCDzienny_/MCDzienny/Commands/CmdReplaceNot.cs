using System;
using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x020002B9 RID: 697
    public class CmdReplaceNot : Command
    {
        // Token: 0x17000795 RID: 1941
        // (get) Token: 0x060013E2 RID: 5090 RVA: 0x0006E194 File Offset: 0x0006C394
        public override string name
        {
            get { return "replacenot"; }
        }

        // Token: 0x17000796 RID: 1942
        // (get) Token: 0x060013E3 RID: 5091 RVA: 0x0006E19C File Offset: 0x0006C39C
        public override string shortcut
        {
            get { return "rn"; }
        }

        // Token: 0x17000797 RID: 1943
        // (get) Token: 0x060013E4 RID: 5092 RVA: 0x0006E1A4 File Offset: 0x0006C3A4
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x17000798 RID: 1944
        // (get) Token: 0x060013E5 RID: 5093 RVA: 0x0006E1AC File Offset: 0x0006C3AC
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000799 RID: 1945
        // (get) Token: 0x060013E6 RID: 5094 RVA: 0x0006E1B0 File Offset: 0x0006C3B0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x1700079A RID: 1946
        // (get) Token: 0x060013E7 RID: 5095 RVA: 0x0006E1B4 File Offset: 0x0006C3B4
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x060013E9 RID: 5097 RVA: 0x0006E1C0 File Offset: 0x0006C3C0
        public override void Use(Player p, string message)
        {
            if (p.level.mapType == MapType.Lava && p.group.Permission < LevelPermission.Admin)
                p.SendMessage("Only admin is allowed to use this command on lava map");
            var num = message.Split(' ').Length;
            var catchPos = default(CatchPos);
            if (num < 2)
            {
                Help(p);
                return;
            }

            var b = Block.Byte(message.Split(' ')[0]);
            if (b == 255)
            {
                Player.SendMessage(p,
                    string.Format("{0} does not exist, please spell it correctly.", message.Split(' ')[0]));
                return;
            }

            catchPos.type = b;
            if (Block.Byte(message.Split(' ')[1]) == 255)
            {
                Player.SendMessage(p,
                    string.Format("{0} does not exist, please spell it correctly.", message.Split(' ')[1]));
                return;
            }

            catchPos.type2 = Block.Byte(message.Split(' ')[1]);
            if (!Block.canPlace(p, catchPos.type2))
            {
                Player.SendMessage(p, "Cannot place this block type!");
                return;
            }

            catchPos.x = 0;
            catchPos.y = 0;
            catchPos.z = 0;
            p.blockchangeObject = catchPos;
            Player.SendMessage(p, "Place two blocks to determine the edges.");
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
        }

        // Token: 0x060013EA RID: 5098 RVA: 0x0006E34C File Offset: 0x0006C54C
        public override void Help(Player p)
        {
            Player.SendMessage(p,
                "/rn [type] [type2] - replace everything but the type with type2 inside a selected cuboid");
        }

        // Token: 0x060013EB RID: 5099 RVA: 0x0006E35C File Offset: 0x0006C55C
        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            var catchPos = (CatchPos) p.blockchangeObject;
            catchPos.x = x;
            catchPos.y = y;
            catchPos.z = z;
            p.blockchangeObject = catchPos;
            p.Blockchange += Blockchange2;
        }

        // Token: 0x060013EC RID: 5100 RVA: 0x0006E3D0 File Offset: 0x0006C5D0
        public void Blockchange2(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            var cpos = (CatchPos) p.blockchangeObject;
            var list = new List<Pos>();
            for (var num = Math.Min(cpos.x, x); num <= Math.Max(cpos.x, x); num += 1)
            for (var num2 = Math.Min(cpos.y, y); num2 <= Math.Max(cpos.y, y); num2 += 1)
            for (var num3 = Math.Min(cpos.z, z); num3 <= Math.Max(cpos.z, z); num3 += 1)
                if (p.level.GetTile(num, num2, num3) != cpos.type)
                    BufferAdd(list, num, num2, num3);
            if (list.Count > p.group.maxBlocks)
            {
                Player.SendMessage(p, string.Format("You tried to replace {0} blocks.", list.Count));
                Player.SendMessage(p, string.Format("You cannot replace more than {0}.", p.group.maxBlocks));
                return;
            }

            Player.SendMessage(p, string.Format("{0} blocks.", list.Count.ToString()));
            list.ForEach(delegate(Pos pos) { p.level.Blockchange(p, pos.x, pos.y, pos.z, cpos.type2); });
            if (p.staticCommands) p.Blockchange += Blockchange1;
        }

        // Token: 0x060013ED RID: 5101 RVA: 0x0006E5CC File Offset: 0x0006C7CC
        private void BufferAdd(List<Pos> list, ushort x, ushort y, ushort z)
        {
            Pos item;
            item.x = x;
            item.y = y;
            item.z = z;
            list.Add(item);
        }

        // Token: 0x020002BA RID: 698
        private struct Pos
        {
            // Token: 0x04000983 RID: 2435
            public ushort x;

            // Token: 0x04000984 RID: 2436
            public ushort y;

            // Token: 0x04000985 RID: 2437
            public ushort z;
        }

        // Token: 0x020002BB RID: 699
        private struct CatchPos
        {
            // Token: 0x04000986 RID: 2438
            public byte type;

            // Token: 0x04000987 RID: 2439
            public byte type2;

            // Token: 0x04000988 RID: 2440
            public ushort x;

            // Token: 0x04000989 RID: 2441
            public ushort y;

            // Token: 0x0400098A RID: 2442
            public ushort z;
        }
    }
}