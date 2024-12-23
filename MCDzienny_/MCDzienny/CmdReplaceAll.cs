using System.Collections.Generic;

namespace MCDzienny
{
    class CmdReplaceAll : Command
    {

        public override string name { get { return "replaceall"; } }

        public override string shortcut { get { return "ra"; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override void Use(Player p, string message)
        {
            if (message.IndexOf(' ') == -1 || message.Split(' ').Length > 3)
            {
                Help(p);
                return;
            }
            Level level = null;
            byte b = Block.Byte(message.Split(' ')[0]);
            byte b2 = Block.Byte(message.Split(' ')[1]);
            if (p == null)
            {
                if (message.Split(' ').Length != 3)
                {
                    Player.SendMessage(p, "You didn't set the map.");
                    Help(p);
                    return;
                }
                level = !(message.Split(' ')[2] == "lava") ? Level.Find(message.Split(' ')[2]) : LavaSystem.currentlvl;
                if (level == null)
                {
                    Player.SendMessage(p, "Map wasn't found.");
                    return;
                }
            }
            if (b == byte.MaxValue || b2 == byte.MaxValue)
            {
                Player.SendMessage(p, "Could not find specified blocks.");
                return;
            }
            int num = 0;
            var list = new List<Pos>();
            ushort x;
            ushort y;
            ushort z;
            Pos item = default(Pos);
            if (p != null)
            {
                byte[] blocks = p.level.blocks;
                foreach (byte b3 in blocks)
                {
                    if (b3 == b)
                    {
                        p.level.IntToPos(num, out x, out y, out z);
                        item.x = x;
                        item.y = y;
                        item.z = z;
                        list.Add(item);
                    }
                    num++;
                }
                if (list.Count > p.group.maxBlocks * 2)
                {
                    Player.SendMessage(p, string.Format("Cannot replace more than {0} blocks.", p.group.maxBlocks * 2));
                    return;
                }
                Player.SendMessage(p, string.Format("{0} blocks out of {1} are {2}", list.Count, num, Block.Name(b)));
                foreach (Pos item2 in list)
                {
                    p.level.Blockchange(p, item2.x, item2.y, item2.z, b2);
                }
            }
            else
            {
                byte[] blocks2 = level.blocks;
                foreach (byte b4 in blocks2)
                {
                    if (b4 == b)
                    {
                        level.IntToPos(num, out x, out y, out z);
                        item.x = x;
                        item.y = y;
                        item.z = z;
                        list.Add(item);
                    }
                    num++;
                }
                foreach (Pos item3 in list)
                {
                    level.Blockchange(item3.x, item3.y, item3.z, b2);
                }
            }
            Player.SendMessage(p, "&4/replaceall finished!");
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/replaceall [block1] [block2] - Replaces all of [block1] with [block2] in a map");
            Player.SendMessage(p, "/replaceall [block1] [block2] [map] - Replaces all of [block1] with [block2] in a [map]");
        }

        public struct Pos
        {
            public ushort x;

            public ushort y;

            public ushort z;
        }
    }
}