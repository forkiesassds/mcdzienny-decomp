namespace MCDzienny
{
    // Token: 0x020002F4 RID: 756
    public class CmdFixGrass : Command
    {
        // Token: 0x17000873 RID: 2163
        // (get) Token: 0x06001569 RID: 5481 RVA: 0x00075D04 File Offset: 0x00073F04
        public override string name
        {
            get { return "fixgrass"; }
        }

        // Token: 0x17000874 RID: 2164
        // (get) Token: 0x0600156A RID: 5482 RVA: 0x00075D0C File Offset: 0x00073F0C
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000875 RID: 2165
        // (get) Token: 0x0600156B RID: 5483 RVA: 0x00075D14 File Offset: 0x00073F14
        public override string type
        {
            get { return "moderation"; }
        }

        // Token: 0x17000876 RID: 2166
        // (get) Token: 0x0600156C RID: 5484 RVA: 0x00075D1C File Offset: 0x00073F1C
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000877 RID: 2167
        // (get) Token: 0x0600156D RID: 5485 RVA: 0x00075D20 File Offset: 0x00073F20
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x17000878 RID: 2168
        // (get) Token: 0x0600156E RID: 5486 RVA: 0x00075D24 File Offset: 0x00073F24
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06001570 RID: 5488 RVA: 0x00075D30 File Offset: 0x00073F30
        public override void Use(Player p, string message)
        {
            var num = 0;
            string a;
            if ((a = message.ToLower()) != null)
            {
                if (!(a == ""))
                {
                    if (!(a == "light"))
                    {
                        if (!(a == "grass"))
                        {
                            if (!(a == "dirt")) goto IL_350;
                            for (var i = 0; i < p.level.blocks.Length; i++)
                                try
                                {
                                    ushort x;
                                    ushort y;
                                    ushort z;
                                    p.level.IntToPos(i, out x, out y, out z);
                                    if (p.level.blocks[i] == 3 &&
                                        Block.LightPass(p.level.blocks[p.level.IntOffset(i, 0, 1, 0)]))
                                    {
                                        p.level.Blockchange(p, x, y, z, 2);
                                        num++;
                                    }
                                }
                                catch
                                {
                                }
                        }
                        else
                        {
                            for (var j = 0; j < p.level.blocks.Length; j++)
                                try
                                {
                                    ushort x2;
                                    ushort y2;
                                    ushort z2;
                                    p.level.IntToPos(j, out x2, out y2, out z2);
                                    if (p.level.blocks[j] == 2 &&
                                        !Block.LightPass(p.level.blocks[p.level.IntOffset(j, 0, 1, 0)]))
                                    {
                                        p.level.Blockchange(p, x2, y2, z2, 3);
                                        num++;
                                    }
                                }
                                catch
                                {
                                }
                        }
                    }
                    else
                    {
                        for (var k = 0; k < p.level.blocks.Length; k++)
                            try
                            {
                                var flag = false;
                                ushort x3;
                                ushort num2;
                                ushort z3;
                                p.level.IntToPos(k, out x3, out num2, out z3);
                                if (p.level.blocks[k] == 3)
                                {
                                    for (var l = 1; l < p.level.height - num2; l++)
                                        if (!Block.LightPass(p.level.blocks[p.level.IntOffset(k, 0, l, 0)]))
                                        {
                                            flag = true;
                                            break;
                                        }

                                    if (!flag)
                                    {
                                        p.level.Blockchange(p, x3, num2, z3, 2);
                                        num++;
                                    }
                                }
                                else if (p.level.blocks[k] == 2)
                                {
                                    for (var m = 1; m < p.level.height - num2; m++)
                                        if (Block.LightPass(p.level.blocks[p.level.IntOffset(k, 0, m, 0)]))
                                        {
                                            flag = true;
                                            break;
                                        }

                                    if (!flag)
                                    {
                                        p.level.Blockchange(p, x3, num2, z3, 3);
                                        num++;
                                    }
                                }
                            }
                            catch
                            {
                            }
                    }
                }
                else
                {
                    for (var n = 0; n < p.level.blocks.Length; n++)
                        try
                        {
                            ushort x4;
                            ushort y3;
                            ushort z4;
                            p.level.IntToPos(n, out x4, out y3, out z4);
                            if (p.level.blocks[n] == 3)
                            {
                                if (Block.LightPass(p.level.blocks[p.level.IntOffset(n, 0, 1, 0)]))
                                {
                                    p.level.Blockchange(p, x4, y3, z4, 2);
                                    num++;
                                }
                            }
                            else if (p.level.blocks[n] == 2 &&
                                     !Block.LightPass(p.level.blocks[p.level.IntOffset(n, 0, 1, 0)]))
                            {
                                p.level.Blockchange(p, x4, y3, z4, 3);
                                num++;
                            }
                        }
                        catch
                        {
                        }
                }

                Player.SendMessage(p, string.Format("Fixed {0} blocks.", num));
                return;
            }

            IL_350:
            Help(p);
        }

        // Token: 0x06001571 RID: 5489 RVA: 0x00076110 File Offset: 0x00074310
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/fixgrass <type> - Fixes grass based on type");
            Player.SendMessage(p,
                "<type> as \"\": Any grass with something on top is made into dirt, dirt with nothing on top is made grass");
            Player.SendMessage(p, "<type> as \"light\": Only dirt/grass in sunlight becomes grass");
            Player.SendMessage(p, "<type> as \"grass\": Only turns grass to dirt when under stuff");
            Player.SendMessage(p, "<type> as \"dirt\": Only turns dirt with nothing on top to grass");
        }
    }
}