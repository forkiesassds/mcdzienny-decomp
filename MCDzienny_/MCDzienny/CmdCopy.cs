using System;
using System.Collections.Generic;

namespace MCDzienny
{
    public class CmdCopy : Command
    {

        public int allowoffset;

        public override string name { get { return "copy"; } }

        public override string shortcut { get { return "c"; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            CatchPos catchPos = default(CatchPos);
            catchPos.ignoreTypes = new List<byte>();
            catchPos.type = 0;
            p.copyoffset[0] = 0;
            p.copyoffset[1] = 0;
            p.copyoffset[2] = 0;
            allowoffset = message.IndexOf('@');
            if (allowoffset != -1)
            {
                message = message.Replace("@ ", "");
            }
            if (message.ToLower() == "cut")
            {
                catchPos.type = 1;
                message = "";
            }
            else if (message.ToLower() == "air")
            {
                catchPos.type = 2;
                message = "";
            }
            else if (message == "@")
            {
                message = "";
            }
            else if (message.IndexOf(' ') != -1)
            {
                if (!(message.Split(' ')[0] == "ignore"))
                {
                    Help(p);
                    return;
                }
                string[] array = message.Substring(message.IndexOf(' ') + 1).Split(' ');
                foreach (string arg in array)
                {
                    if (Block.Byte(arg) != byte.MaxValue)
                    {
                        catchPos.ignoreTypes.Add(Block.Byte(arg));
                        Player.SendMessage(p, string.Format("Ignoring &b{0}", arg));
                    }
                }
                message = "";
            }
            catchPos.x = 0;
            catchPos.y = 0;
            catchPos.z = 0;
            p.blockchangeObject = catchPos;
            if (message != "")
            {
                Help(p);
                return;
            }
            Player.SendMessage(p, "Place two blocks to determine the edges.");
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/copy - Copies the blocks in an area.");
            Player.SendMessage(p, "/copy cut - Copies the blocks in an area, then removes them.");
            Player.SendMessage(p, "/copy air - Copies the blocks in an area, including air.");
            Player.SendMessage(p, "/copy ignore <block1> <block2>.. - Ignores <blocks> when copying");
            Player.SendMessage(p, "/copy @ - @ toggle for all the above, gives you a third click after copying that determines where to paste from");
        }

        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            CatchPos catchPos = (CatchPos)p.blockchangeObject;
            p.copystart[0] = x;
            p.copystart[1] = y;
            p.copystart[2] = z;
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
            p.CopyBuffer.Clear();
            int num = 0;
            if (catchPos.type == 2)
            {
                p.copyAir = true;
            }
            else
            {
                p.copyAir = false;
            }
            for (ushort num2 = Math.Min(catchPos.x, x); num2 <= Math.Max(catchPos.x, x); num2++)
            {
                for (ushort num3 = Math.Min(catchPos.y, y); num3 <= Math.Max(catchPos.y, y); num3++)
                {
                    for (ushort num4 = Math.Min(catchPos.z, z); num4 <= Math.Max(catchPos.z, z); num4++)
                    {
                        tile = p.level.GetTile(num2, num3, num4);
                        if (Block.canPlace(p, tile))
                        {
                            if (tile == 0 && catchPos.type != 2 || catchPos.ignoreTypes.Contains(tile))
                            {
                                num++;
                            }
                            if (catchPos.ignoreTypes.Contains(tile))
                            {
                                BufferAdd(p, (ushort)(num2 - catchPos.x), (ushort)(num3 - catchPos.y), (ushort)(num4 - catchPos.z), 0);
                            }
                            else
                            {
                                BufferAdd(p, (ushort)(num2 - catchPos.x), (ushort)(num3 - catchPos.y), (ushort)(num4 - catchPos.z), tile);
                            }
                        }
                        else
                        {
                            BufferAdd(p, (ushort)(num2 - catchPos.x), (ushort)(num3 - catchPos.y), (ushort)(num4 - catchPos.z), 0);
                        }
                    }
                }
            }
            if (p.CopyBuffer.Count - num > p.group.maxBlocks)
            {
                Player.SendMessage(p, string.Format("You tried to copy {0} blocks.", p.CopyBuffer.Count));
                Player.SendMessage(p, string.Format("You cannot copy more than {0}.", p.group.maxBlocks));
                p.CopyBuffer.Clear();
                return;
            }
            if (catchPos.type == 1)
            {
                for (ushort num5 = Math.Min(catchPos.x, x); num5 <= Math.Max(catchPos.x, x); num5++)
                {
                    for (ushort num6 = Math.Min(catchPos.y, y); num6 <= Math.Max(catchPos.y, y); num6++)
                    {
                        for (ushort num7 = Math.Min(catchPos.z, z); num7 <= Math.Max(catchPos.z, z); num7++)
                        {
                            tile = p.level.GetTile(num5, num6, num7);
                            if (tile != 0 && Block.canPlace(p, tile))
                            {
                                p.level.Blockchange(p, num5, num6, num7, 0);
                            }
                        }
                    }
                }
            }
            Player.SendMessage(p, string.Format("{0} blocks copied.", p.CopyBuffer.Count - num));
            if (allowoffset != -1)
            {
                Player.SendMessage(p, "Place a block to determine where to paste from");
                p.Blockchange += Blockchange3;
            }
        }

        public void Blockchange3(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            p.copyoffset[0] = p.copystart[0] - x;
            p.copyoffset[1] = p.copystart[1] - y;
            p.copyoffset[2] = p.copystart[2] - z;
        }

        void BufferAdd(Player p, ushort x, ushort y, ushort z, byte type)
        {
            Player.CopyPos item = default(Player.CopyPos);
            item.x = x;
            item.y = y;
            item.z = z;
            item.type = type;
            p.CopyBuffer.Add(item);
        }

        struct CatchPos
        {
            public ushort x;

            public ushort y;

            public ushort z;

            public int type;

            public List<byte> ignoreTypes;
        }
    }
}