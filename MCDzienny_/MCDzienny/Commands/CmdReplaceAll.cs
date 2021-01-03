using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x020002E2 RID: 738
    internal class CmdReplaceAll : Command
    {
        // Token: 0x17000826 RID: 2086
        // (get) Token: 0x060014EA RID: 5354 RVA: 0x00073F88 File Offset: 0x00072188
        public override string name
        {
            get { return "replaceall"; }
        }

        // Token: 0x17000827 RID: 2087
        // (get) Token: 0x060014EB RID: 5355 RVA: 0x00073F90 File Offset: 0x00072190
        public override string shortcut
        {
            get { return "ra"; }
        }

        // Token: 0x17000828 RID: 2088
        // (get) Token: 0x060014EC RID: 5356 RVA: 0x00073F98 File Offset: 0x00072198
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x17000829 RID: 2089
        // (get) Token: 0x060014ED RID: 5357 RVA: 0x00073FA0 File Offset: 0x000721A0
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x1700082A RID: 2090
        // (get) Token: 0x060014EE RID: 5358 RVA: 0x00073FA4 File Offset: 0x000721A4
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x060014F0 RID: 5360 RVA: 0x00073FB0 File Offset: 0x000721B0
        public override void Use(Player p, string message)
        {
            if (message.IndexOf(' ') == -1 || message.Split(' ').Length > 3)
            {
                Help(p);
                return;
            }

            Level level = null;
            var b = Block.Byte(message.Split(' ')[0]);
            var b2 = Block.Byte(message.Split(' ')[1]);
            if (p == null)
            {
                if (message.Split(' ').Length != 3)
                {
                    Player.SendMessage(p, "You didn't set the map.");
                    Help(p);
                    return;
                }

                if (message.Split(' ')[2] == "lava")
                    level = LavaSystem.currentlvl;
                else
                    level = Level.Find(message.Split(' ')[2]);
                if (level == null)
                {
                    Player.SendMessage(p, "Map wasn't found.");
                    return;
                }
            }

            if (b == 255 || b2 == 255)
            {
                Player.SendMessage(p, "Could not find specified blocks.");
                return;
            }

            var num = 0;
            var list = new List<Pos>();
            Pos item;
            if (p != null)
            {
                foreach (var b3 in p.level.blocks)
                {
                    if (b3 == b)
                    {
                        ushort x;
                        ushort y;
                        ushort z;
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
                using (var enumerator = list.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        var pos = enumerator.Current;
                        p.level.Blockchange(p, pos.x, pos.y, pos.z, b2);
                    }

                    goto IL_2EE;
                }
            }

            foreach (var b4 in level.blocks)
            {
                if (b4 == b)
                {
                    ushort x;
                    ushort y;
                    ushort z;
                    level.IntToPos(num, out x, out y, out z);
                    item.x = x;
                    item.y = y;
                    item.z = z;
                    list.Add(item);
                }

                num++;
            }

            foreach (var pos2 in list) level.Blockchange(pos2.x, pos2.y, pos2.z, b2);
            IL_2EE:
            Player.SendMessage(p, "&4/replaceall finished!");
        }

        // Token: 0x060014F1 RID: 5361 RVA: 0x000742D4 File Offset: 0x000724D4
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/replaceall [block1] [block2] - Replaces all of [block1] with [block2] in a map");
            Player.SendMessage(p,
                "/replaceall [block1] [block2] [map] - Replaces all of [block1] with [block2] in a [map]");
        }

        // Token: 0x020002E3 RID: 739
        public struct Pos
        {
            // Token: 0x040009B7 RID: 2487
            public ushort x;

            // Token: 0x040009B8 RID: 2488
            public ushort y;

            // Token: 0x040009B9 RID: 2489
            public ushort z;
        }
    }
}