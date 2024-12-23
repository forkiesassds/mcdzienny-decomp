namespace MCDzienny
{
    public class CmdFixGrass : Command
    {
        public override string name { get { return "fixgrass"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "moderation"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            int num = 0;
            switch (message.ToLower())
            {
                case "":
                {
                    for (int n = 0; n < p.level.blocks.Length; n++)
                    {
                        try
                        {
                            ushort x4;
                            ushort y4;
                            ushort z4;
                            p.level.IntToPos(n, out x4, out y4, out z4);
                            if (p.level.blocks[n] == 3)
                            {
                                if (Block.LightPass(p.level.blocks[p.level.IntOffset(n, 0, 1, 0)]))
                                {
                                    p.level.Blockchange(p, x4, y4, z4, 2);
                                    num++;
                                }
                            }
                            else if (p.level.blocks[n] == 2 && !Block.LightPass(p.level.blocks[p.level.IntOffset(n, 0, 1, 0)]))
                            {
                                p.level.Blockchange(p, x4, y4, z4, 3);
                                num++;
                            }
                        }
                        catch {}
                    }
                    break;
                }
                case "light":
                {
                    for (int j = 0; j < p.level.blocks.Length; j++)
                    {
                        try
                        {
                            bool flag = false;
                            ushort x2;
                            ushort y2;
                            ushort z2;
                            p.level.IntToPos(j, out x2, out y2, out z2);
                            if (p.level.blocks[j] == 3)
                            {
                                for (int k = 1; k < p.level.height - y2; k++)
                                {
                                    if (!Block.LightPass(p.level.blocks[p.level.IntOffset(j, 0, k, 0)]))
                                    {
                                        flag = true;
                                        break;
                                    }
                                }
                                if (!flag)
                                {
                                    p.level.Blockchange(p, x2, y2, z2, 2);
                                    num++;
                                }
                            }
                            else
                            {
                                if (p.level.blocks[j] != 2)
                                {
                                    continue;
                                }
                                for (int l = 1; l < p.level.height - y2; l++)
                                {
                                    if (Block.LightPass(p.level.blocks[p.level.IntOffset(j, 0, l, 0)]))
                                    {
                                        flag = true;
                                        break;
                                    }
                                }
                                if (!flag)
                                {
                                    p.level.Blockchange(p, x2, y2, z2, 3);
                                    num++;
                                }
                            }
                        }
                        catch {}
                    }
                    break;
                }
                case "grass":
                {
                    for (int m = 0; m < p.level.blocks.Length; m++)
                    {
                        try
                        {
                            ushort x3;
                            ushort y3;
                            ushort z3;
                            p.level.IntToPos(m, out x3, out y3, out z3);
                            if (p.level.blocks[m] == 2 && !Block.LightPass(p.level.blocks[p.level.IntOffset(m, 0, 1, 0)]))
                            {
                                p.level.Blockchange(p, x3, y3, z3, 3);
                                num++;
                            }
                        }
                        catch {}
                    }
                    break;
                }
                case "dirt":
                {
                    for (int i = 0; i < p.level.blocks.Length; i++)
                    {
                        try
                        {
                            ushort x;
                            ushort y;
                            ushort z;
                            p.level.IntToPos(i, out x, out y, out z);
                            if (p.level.blocks[i] == 3 && Block.LightPass(p.level.blocks[p.level.IntOffset(i, 0, 1, 0)]))
                            {
                                p.level.Blockchange(p, x, y, z, 2);
                                num++;
                            }
                        }
                        catch {}
                    }
                    break;
                }
                default:
                    Help(p);
                    return;
            }
            Player.SendMessage(p, string.Format("Fixed {0} blocks.", num));
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/fixgrass <type> - Fixes grass based on type");
            Player.SendMessage(p, "<type> as \"\": Any grass with something on top is made into dirt, dirt with nothing on top is made grass");
            Player.SendMessage(p, "<type> as \"light\": Only dirt/grass in sunlight becomes grass");
            Player.SendMessage(p, "<type> as \"grass\": Only turns grass to dirt when under stuff");
            Player.SendMessage(p, "<type> as \"dirt\": Only turns dirt with nothing on top to grass");
        }
    }
}