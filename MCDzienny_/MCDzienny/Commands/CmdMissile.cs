using System;
using System.Collections.Generic;
using System.Threading;

namespace MCDzienny
{
    // Token: 0x0200010D RID: 269
    public class CmdMissile : Command
    {
        // Token: 0x170003BA RID: 954
        // (get) Token: 0x06000833 RID: 2099 RVA: 0x00029294 File Offset: 0x00027494
        public override string name
        {
            get { return "missile"; }
        }

        // Token: 0x170003BB RID: 955
        // (get) Token: 0x06000834 RID: 2100 RVA: 0x0002929C File Offset: 0x0002749C
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170003BC RID: 956
        // (get) Token: 0x06000835 RID: 2101 RVA: 0x000292A4 File Offset: 0x000274A4
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170003BD RID: 957
        // (get) Token: 0x06000836 RID: 2102 RVA: 0x000292AC File Offset: 0x000274AC
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x170003BE RID: 958
        // (get) Token: 0x06000837 RID: 2103 RVA: 0x000292B0 File Offset: 0x000274B0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x170003BF RID: 959
        // (get) Token: 0x06000838 RID: 2104 RVA: 0x000292B4 File Offset: 0x000274B4
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x0600083A RID: 2106 RVA: 0x000292C0 File Offset: 0x000274C0
        public override void Use(Player p, string message)
        {
            if (p.aiming && message == "")
            {
                p.aiming = false;
                Player.SendMessage(p, "Disabled missiles");
                return;
            }

            var pos = default(Pos);
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
            if (p.aiming) return;
            p.aiming = true;
            var thread = new Thread((ThreadStart) delegate
            {
                var list = new List<CatchPos>();
                var item = default(CatchPos);
                while (p.aiming)
                {
                    var list2 = new List<CatchPos>();
                    var num = Math.Sin((128 - p.rot[0]) / 256.0 * 2.0 * Math.PI);
                    var num2 = Math.Cos((128 - p.rot[0]) / 256.0 * 2.0 * Math.PI);
                    var num3 = Math.Cos((p.rot[1] + 64) / 256.0 * 2.0 * Math.PI);
                    try
                    {
                        var num4 = (ushort) (p.pos[0] / 32);
                        num4 = (ushort) Math.Round(num4 + num * 3.0);
                        var num5 = (ushort) (p.pos[1] / 32 + 1);
                        num5 = (ushort) Math.Round(num5 + num3 * 3.0);
                        var num6 = (ushort) (p.pos[2] / 32);
                        num6 = (ushort) Math.Round(num6 + num2 * 3.0);
                        if (num4 > p.level.width || num5 > p.level.height || num6 > p.level.depth)
                            throw new Exception();
                        if (num4 < 0 || num5 < 0 || num6 < 0) throw new Exception();
                        for (var num7 = num4; num7 <= num4 + 1; num7 = (ushort) (num7 + 1))
                        for (var num8 = (ushort) (num5 - 1); num8 <= num5; num8 = (ushort) (num8 + 1))
                        for (var num9 = num6; num9 <= num6 + 1; num9 = (ushort) (num9 + 1))
                            if (p.level.GetTile(num7, num8, num9) == 0)
                            {
                                item.x = num7;
                                item.y = num8;
                                item.z = num9;
                                list2.Add(item);
                            }

                        var list3 = new List<CatchPos>();
                        foreach (var item2 in list)
                            if (!list2.Contains(item2))
                            {
                                p.SendBlockchange(item2.x, item2.y, item2.z, 0);
                                list3.Add(item2);
                            }

                        foreach (var item3 in list3) list.Remove(item3);
                        foreach (var item4 in list2)
                            if (!list.Contains(item4))
                            {
                                list.Add(item4);
                                p.SendBlockchange(item4.x, item4.y, item4.z, 20);
                            }

                        list2.Clear();
                        list3.Clear();
                    }
                    catch
                    {
                    }

                    Thread.Sleep(20);
                }

                foreach (var item5 in list) p.SendBlockchange(item5.x, item5.y, item5.z, 0);
            });
            thread.Start();
        }

        // Token: 0x0600083B RID: 2107 RVA: 0x00029414 File Offset: 0x00027614
        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            if (!p.staticCommands)
            {
                p.ClearBlockchange();
                p.aiming = false;
            }

            var by = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, by);
            var bp = (Pos) p.blockchangeObject;
            var previous = new List<CatchPos>();
            var allBlocks = new List<CatchPos>();
            if (p.modeType != 0) type = p.modeType;
            var pos = default(CatchPos);
            var thread = new Thread((ThreadStart) delegate
            {
                var x2 = (ushort) (p.pos[0] / 32);
                var y2 = (ushort) (p.pos[1] / 32);
                var z2 = (ushort) (p.pos[2] / 32);
                pos.x = x2;
                pos.y = y2;
                pos.z = z2;
                var num = 0;
                var catchPos = default(CatchPos);
                while (true)
                {
                    x2 = (ushort) (p.pos[0] / 32);
                    y2 = (ushort) (p.pos[1] / 32);
                    z2 = (ushort) (p.pos[2] / 32);
                    num++;
                    var num2 = Math.Sin((128 - p.rot[0]) / 256.0 * 2.0 * Math.PI);
                    var num3 = Math.Cos((128 - p.rot[0]) / 256.0 * 2.0 * Math.PI);
                    var num4 = Math.Cos((p.rot[1] + 64) / 256.0 * 2.0 * Math.PI);
                    var num5 = 1;
                    while (true)
                    {
                        catchPos.x = (ushort) Math.Round(x2 + num2 * num5);
                        catchPos.y = (ushort) Math.Round(y2 + num4 * num5);
                        catchPos.z = (ushort) Math.Round(z2 + num3 * num5);
                        by = p.level.GetTile(catchPos.x, catchPos.y, catchPos.z);
                        if (by == byte.MaxValue) break;
                        if (by != 0 && !allBlocks.Contains(catchPos))
                        {
                            if (p.level.physics < 2 || bp.ending <= 0) break;
                            if (bp.ending == 1)
                            {
                                if (!Block.LavaKill(by) && !Block.NeedRestart(by) && by != 20) break;
                            }
                            else if (p.level.physics != 3 || by != 20)
                            {
                                break;
                            }
                        }

                        var flag = false;
                        foreach (var player in Player.players)
                            if (player.level == p.level && player != p &&
                                ((ushort) (player.pos[0] / 32) == catchPos.x ||
                                 (ushort) (player.pos[0] / 32 + 1) == catchPos.x ||
                                 (ushort) (player.pos[0] / 32 - 1) == catchPos.x) &&
                                ((ushort) (player.pos[1] / 32) == catchPos.y ||
                                 (ushort) (player.pos[1] / 32 + 1) == catchPos.y ||
                                 (ushort) (player.pos[1] / 32 - 1) == catchPos.y) &&
                                ((ushort) (player.pos[2] / 32) == catchPos.z ||
                                 (ushort) (player.pos[2] / 32 + 1) == catchPos.z ||
                                 (ushort) (player.pos[2] / 32 - 1) == catchPos.z))
                            {
                                catchPos.x = (ushort) (player.pos[0] / 32);
                                catchPos.y = (ushort) (player.pos[1] / 32);
                                catchPos.z = (ushort) (player.pos[2] / 32);
                                flag = true;
                                break;
                            }

                        if (flag) break;
                        num5++;
                    }

                    catchPos.x = (ushort) Math.Round(x2 + num2 * (num5 - 1));
                    catchPos.y = (ushort) Math.Round(y2 + num4 * (num5 - 1));
                    catchPos.z = (ushort) Math.Round(z2 + num3 * (num5 - 1));
                    findNext(catchPos, ref pos);
                    by = p.level.GetTile(pos.x, pos.y, pos.z);
                    if (num > 3)
                    {
                        if (by != 0 && !allBlocks.Contains(pos))
                        {
                            if (p.level.physics < 2 || bp.ending <= 0) break;
                            if (bp.ending == 1)
                            {
                                if (!Block.LavaKill(by) && !Block.NeedRestart(by) && by != 20) break;
                            }
                            else
                            {
                                if (p.level.physics != 3) break;
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
                        var flag2 = false;
                        foreach (var player2 in Player.players)
                            if (player2.level == p.level && player2 != p &&
                                ((ushort) (player2.pos[0] / 32) == pos.x ||
                                 (ushort) (player2.pos[0] / 32 + 1) == pos.x ||
                                 (ushort) (player2.pos[0] / 32 - 1) == pos.x) &&
                                ((ushort) (player2.pos[1] / 32) == pos.y ||
                                 (ushort) (player2.pos[1] / 32 + 1) == pos.y ||
                                 (ushort) (player2.pos[1] / 32 - 1) == pos.y) &&
                                ((ushort) (player2.pos[2] / 32) == pos.z ||
                                 (ushort) (player2.pos[2] / 32 + 1) == pos.z ||
                                 (ushort) (player2.pos[2] / 32 - 1) == pos.z))
                            {
                                if (p.level.physics == 3 && bp.ending >= 2)
                                    player2.HandleDeath(4,
                                        string.Format(" was blown up by {0}", p.color + p.PublicName), true);
                                else
                                    player2.HandleDeath(4,
                                        string.Format(" was hit a missile from {0}", p.color + p.PublicName));
                                flag2 = true;
                            }

                        if (flag2) break;
                        if (pos.x == catchPos.x && pos.y == catchPos.y && pos.z == catchPos.z)
                        {
                            if (p.level.physics == 3 && bp.ending >= 2)
                                p.level.MakeExplosion(catchPos.x, catchPos.y, catchPos.z, 2);
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

                foreach (var item in previous)
                {
                    p.level.Blockchange(item.x, item.y, item.z, 0);
                    Thread.Sleep(100);
                }
            });
            thread.Start();
        }

        // Token: 0x0600083C RID: 2108 RVA: 0x000294F4 File Offset: 0x000276F4
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/missile [at end] - Allows you to fire missiles at people");
            Player.SendMessage(p, "Available [at end] values: &cexplode, destroy");
            Player.SendMessage(p, "Differs from /gun in that the missile is guided");
        }

        // Token: 0x0600083D RID: 2109 RVA: 0x00029518 File Offset: 0x00027718
        public void findNext(CatchPos lookedAt, ref CatchPos pos)
        {
            int[] array =
            {
                pos.x,
                pos.y,
                pos.z
            };
            var num = lookedAt.x - pos.x;
            var num2 = lookedAt.y - pos.y;
            var num3 = lookedAt.z - pos.z;
            var num4 = num < 0 ? -1 : 1;
            var num5 = Math.Abs(num);
            var num6 = num2 < 0 ? -1 : 1;
            var num7 = Math.Abs(num2);
            var num8 = num3 < 0 ? -1 : 1;
            var num9 = Math.Abs(num3);
            var num10 = num5 << 1;
            var num11 = num7 << 1;
            var num12 = num9 << 1;
            int num13;
            int num14;
            if (num5 >= num7 && num5 >= num9)
            {
                num13 = num11 - num5;
                num14 = num12 - num5;
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
                pos.x = (ushort) array[0];
                pos.y = (ushort) array[1];
                pos.z = (ushort) array[2];
                return;
            }

            if (num7 >= num5 && num7 >= num9)
            {
                num13 = num10 - num7;
                num14 = num12 - num7;
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
                pos.x = (ushort) array[0];
                pos.y = (ushort) array[1];
                pos.z = (ushort) array[2];
                return;
            }

            num13 = num11 - num9;
            num14 = num10 - num9;
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
            pos.x = (ushort) array[0];
            pos.y = (ushort) array[1];
            pos.z = (ushort) array[2];
        }

        // Token: 0x0200010E RID: 270
        public struct CatchPos
        {
            // Token: 0x040003C1 RID: 961
            public ushort x;

            // Token: 0x040003C2 RID: 962
            public ushort y;

            // Token: 0x040003C3 RID: 963
            public ushort z;
        }

        // Token: 0x0200010F RID: 271
        public struct Pos
        {
            // Token: 0x040003C4 RID: 964
            public ushort x;

            // Token: 0x040003C5 RID: 965
            public ushort y;

            // Token: 0x040003C6 RID: 966
            public ushort z;

            // Token: 0x040003C7 RID: 967
            public int ending;
        }
    }
}