using System;
using System.Collections.Generic;
using System.Threading;

namespace MCDzienny
{
    public class CmdMissile : Command
    {

        public override string name { get { return "missile"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (p.aiming && message == "")
            {
                p.aiming = false;
                Player.SendMessage(p, "Disabled missiles");
                return;
            }
            Pos pos = default(Pos);
            pos.ending = 0;
            if (message.ToLower() == "destroy")
            {
                pos.ending = 1;
            }
            else if (message.ToLower() == "explode")
            {
                pos.ending = 2;
            }
            else if (message.ToLower() == "teleport")
            {
                pos.ending = -1;
            }
            else if (message != "")
            {
                Help(p);
                return;
            }
            pos.x = 0;
            pos.y = 0;
            pos.z = 0;
            p.blockchangeObject = pos;
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
            Player.SendMessage(p, "Missile mode engaged, fire and guide!");
            if (p.aiming)
            {
                return;
            }
            p.aiming = true;
            Thread thread = new Thread((ThreadStart)delegate
            {
                var list = new List<CatchPos>();
                CatchPos item = default(CatchPos);
                while (p.aiming)
                {
                    var list2 = new List<CatchPos>();
                    double num = Math.Sin((128 - p.rot[0]) / 256.0 * 2.0 * Math.PI);
                    double num2 = Math.Cos((128 - p.rot[0]) / 256.0 * 2.0 * Math.PI);
                    double num3 = Math.Cos((p.rot[1] + 64) / 256.0 * 2.0 * Math.PI);
                    try
                    {
                        ushort num4 = (ushort)(p.pos[0] / 32);
                        num4 = (ushort)Math.Round(num4 + num * 3.0);
                        ushort num5 = (ushort)(p.pos[1] / 32 + 1);
                        num5 = (ushort)Math.Round(num5 + num3 * 3.0);
                        ushort num6 = (ushort)(p.pos[2] / 32);
                        num6 = (ushort)Math.Round(num6 + num2 * 3.0);
                        if (num4 > p.level.width || num5 > p.level.height || num6 > p.level.depth)
                        {
                            throw new Exception();
                        }
                        if (num4 < 0 || num5 < 0 || num6 < 0)
                        {
                            throw new Exception();
                        }
                        for (ushort num7 = num4; num7 <= num4 + 1; num7++)
                        {
                            for (ushort num8 = (ushort)(num5 - 1); num8 <= num5; num8++)
                            {
                                for (ushort num9 = num6; num9 <= num6 + 1; num9++)
                                {
                                    if (p.level.GetTile(num7, num8, num9) == 0)
                                    {
                                        item.x = num7;
                                        item.y = num8;
                                        item.z = num9;
                                        list2.Add(item);
                                    }
                                }
                            }
                        }
                        var list3 = new List<CatchPos>();
                        foreach (CatchPos item2 in list)
                        {
                            if (!list2.Contains(item2))
                            {
                                p.SendBlockchange(item2.x, item2.y, item2.z, 0);
                                list3.Add(item2);
                            }
                        }
                        foreach (CatchPos item3 in list3)
                        {
                            list.Remove(item3);
                        }
                        foreach (CatchPos item4 in list2)
                        {
                            if (!list.Contains(item4))
                            {
                                list.Add(item4);
                                p.SendBlockchange(item4.x, item4.y, item4.z, 20);
                            }
                        }
                        list2.Clear();
                        list3.Clear();
                    }
                    catch {}
                    Thread.Sleep(20);
                }
                foreach (CatchPos item5 in list)
                {
                    p.SendBlockchange(item5.x, item5.y, item5.z, 0);
                }
            });
            thread.Start();
        }

        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            if (!p.staticCommands)
            {
                p.ClearBlockchange();
                p.aiming = false;
            }
            byte by = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, by);
            Pos bp = (Pos)p.blockchangeObject;
            var previous = new List<CatchPos>();
            var allBlocks = new List<CatchPos>();
            if (p.modeType != 0)
            {
                type = p.modeType;
            }
            CatchPos pos = default(CatchPos);
            Thread thread = new Thread((ThreadStart)delegate
            {
                ushort x2 = (ushort)(p.pos[0] / 32);
                ushort y2 = (ushort)(p.pos[1] / 32);
                ushort z2 = (ushort)(p.pos[2] / 32);
                pos.x = x2;
                pos.y = y2;
                pos.z = z2;
                int num = 0;
                CatchPos catchPos = default(CatchPos);
                while (true)
                {
                    x2 = (ushort)(p.pos[0] / 32);
                    y2 = (ushort)(p.pos[1] / 32);
                    z2 = (ushort)(p.pos[2] / 32);
                    num++;
                    double num2 = Math.Sin((128 - p.rot[0]) / 256.0 * 2.0 * Math.PI);
                    double num3 = Math.Cos((128 - p.rot[0]) / 256.0 * 2.0 * Math.PI);
                    double num4 = Math.Cos((p.rot[1] + 64) / 256.0 * 2.0 * Math.PI);
                    int num5 = 1;
                    while (true)
                    {
                        catchPos.x = (ushort)Math.Round(x2 + num2 * num5);
                        catchPos.y = (ushort)Math.Round(y2 + num4 * num5);
                        catchPos.z = (ushort)Math.Round(z2 + num3 * num5);
                        by = p.level.GetTile(catchPos.x, catchPos.y, catchPos.z);
                        if (by == byte.MaxValue)
                        {
                            break;
                        }
                        if (by != 0 && !allBlocks.Contains(catchPos))
                        {
                            if (p.level.physics < 2 || bp.ending <= 0)
                            {
                                break;
                            }
                            if (bp.ending == 1)
                            {
                                if (!Block.LavaKill(by) && !Block.NeedRestart(by) && by != 20)
                                {
                                    break;
                                }
                            }
                            else if (p.level.physics != 3 || by != 20)
                            {
                                break;
                            }
                        }
                        bool flag = false;
                        foreach (Player player in Player.players)
                        {
                            if (player.level == p.level && player != p &&
                                ((ushort)(player.pos[0] / 32) == catchPos.x || (ushort)(player.pos[0] / 32 + 1) == catchPos.x ||
                                    (ushort)(player.pos[0] / 32 - 1) == catchPos.x) &&
                                ((ushort)(player.pos[1] / 32) == catchPos.y || (ushort)(player.pos[1] / 32 + 1) == catchPos.y ||
                                    (ushort)(player.pos[1] / 32 - 1) == catchPos.y) && ((ushort)(player.pos[2] / 32) == catchPos.z ||
                                    (ushort)(player.pos[2] / 32 + 1) == catchPos.z || (ushort)(player.pos[2] / 32 - 1) == catchPos.z))
                            {
                                catchPos.x = (ushort)(player.pos[0] / 32);
                                catchPos.y = (ushort)(player.pos[1] / 32);
                                catchPos.z = (ushort)(player.pos[2] / 32);
                                flag = true;
                                break;
                            }
                        }
                        if (flag)
                        {
                            break;
                        }
                        num5++;
                    }
                    catchPos.x = (ushort)Math.Round(x2 + num2 * (num5 - 1));
                    catchPos.y = (ushort)Math.Round(y2 + num4 * (num5 - 1));
                    catchPos.z = (ushort)Math.Round(z2 + num3 * (num5 - 1));
                    findNext(catchPos, ref pos);
                    by = p.level.GetTile(pos.x, pos.y, pos.z);
                    if (num > 3)
                    {
                        if (by != 0 && !allBlocks.Contains(pos))
                        {
                            if (p.level.physics < 2 || bp.ending <= 0)
                            {
                                break;
                            }
                            if (bp.ending == 1)
                            {
                                if (!Block.LavaKill(by) && !Block.NeedRestart(by) && by != 20)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                if (p.level.physics != 3)
                                {
                                    break;
                                }
                                if (by != 20)
                                {
                                    p.level.MakeExplosion(pos.x, pos.y, pos.z, 1);
                                    break;
                                }
                            }
                        }
                        p.level.Blockchange(pos.x, pos.y, pos.z, type);
                        previous.Add(pos);
                        allBlocks.Add(pos);
                        bool flag2 = false;
                        foreach (Player player2 in Player.players)
                        {
                            if (player2.level == p.level && player2 != p &&
                                ((ushort)(player2.pos[0] / 32) == pos.x || (ushort)(player2.pos[0] / 32 + 1) == pos.x || (ushort)(player2.pos[0] / 32 - 1) == pos.x) &&
                                ((ushort)(player2.pos[1] / 32) == pos.y || (ushort)(player2.pos[1] / 32 + 1) == pos.y || (ushort)(player2.pos[1] / 32 - 1) == pos.y) &&
                                ((ushort)(player2.pos[2] / 32) == pos.z || (ushort)(player2.pos[2] / 32 + 1) == pos.z || (ushort)(player2.pos[2] / 32 - 1) == pos.z))
                            {
                                if (p.level.physics == 3 && bp.ending >= 2)
                                {
                                    player2.HandleDeath(4, string.Format(" was blown up by {0}", p.color + p.PublicName), explode: true);
                                }
                                else
                                {
                                    player2.HandleDeath(4, string.Format(" was hit a missile from {0}", p.color + p.PublicName));
                                }
                                flag2 = true;
                            }
                        }
                        if (flag2)
                        {
                            break;
                        }
                        if (pos.x == catchPos.x && pos.y == catchPos.y && pos.z == catchPos.z)
                        {
                            if (p.level.physics == 3 && bp.ending >= 2)
                            {
                                p.level.MakeExplosion(catchPos.x, catchPos.y, catchPos.z, 2);
                            }
                            break;
                        }
                        if (previous.Count > 12)
                        {
                            p.level.Blockchange(previous[0].x, previous[0].y, previous[0].z, 0);
                            previous.Remove(previous[0]);
                        }
                        Thread.Sleep(100);
                    }
                }
                foreach (CatchPos item in previous)
                {
                    p.level.Blockchange(item.x, item.y, item.z, 0);
                    Thread.Sleep(100);
                }
            });
            thread.Start();
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/missile [at end] - Allows you to fire missiles at people");
            Player.SendMessage(p, "Available [at end] values: &cexplode, destroy");
            Player.SendMessage(p, "Differs from /gun in that the missile is guided");
        }

        public void findNext(CatchPos lookedAt, ref CatchPos pos)
        {
            int[] array = new int[3]
            {
                pos.x, pos.y, pos.z
            };
            int num = lookedAt.x - pos.x;
            int num2 = lookedAt.y - pos.y;
            int num3 = lookedAt.z - pos.z;
            int num4 = num >= 0 ? 1 : -1;
            int num5 = Math.Abs(num);
            int num6 = num2 >= 0 ? 1 : -1;
            int num7 = Math.Abs(num2);
            int num8 = num3 >= 0 ? 1 : -1;
            int num9 = Math.Abs(num3);
            int num10 = num5 << 1;
            int num11 = num7 << 1;
            int num12 = num9 << 1;
            if (num5 >= num7 && num5 >= num9)
            {
                int num13 = num11 - num5;
                int num14 = num12 - num5;
                array[0] += num4;
                if (num13 > 0)
                {
                    array[1] += num6;
                    num13 -= num10;
                }
                if (num14 > 0)
                {
                    array[2] += num8;
                    num14 -= num10;
                }
                num13 += num11;
                num14 += num12;
                pos.x = (ushort)array[0];
                pos.y = (ushort)array[1];
                pos.z = (ushort)array[2];
            }
            else if (num7 >= num5 && num7 >= num9)
            {
                int num13 = num10 - num7;
                int num14 = num12 - num7;
                array[1] += num6;
                if (num13 > 0)
                {
                    array[0] += num4;
                    num13 -= num11;
                }
                if (num14 > 0)
                {
                    array[2] += num8;
                    num14 -= num11;
                }
                num13 += num10;
                num14 += num12;
                pos.x = (ushort)array[0];
                pos.y = (ushort)array[1];
                pos.z = (ushort)array[2];
            }
            else
            {
                int num13 = num11 - num9;
                int num14 = num10 - num9;
                array[2] += num8;
                if (num13 > 0)
                {
                    array[1] += num6;
                    num13 -= num12;
                }
                if (num14 > 0)
                {
                    array[0] += num4;
                    num14 -= num12;
                }
                num13 += num11;
                num14 += num10;
                pos.x = (ushort)array[0];
                pos.y = (ushort)array[1];
                pos.z = (ushort)array[2];
            }
        }

        public struct CatchPos
        {
            public ushort x;

            public ushort y;

            public ushort z;
        }

        public struct Pos
        {
            public ushort x;

            public ushort y;

            public ushort z;

            public int ending;
        }
    }
}