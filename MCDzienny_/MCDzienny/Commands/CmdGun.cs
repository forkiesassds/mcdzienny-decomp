using System;
using System.Collections.Generic;
using System.Threading;

namespace MCDzienny
{
    // Token: 0x020000F0 RID: 240
    public class CmdGun : Command
    {
        // Token: 0x17000361 RID: 865
        // (get) Token: 0x06000790 RID: 1936 RVA: 0x000261F8 File Offset: 0x000243F8
        public override string name
        {
            get { return "gun"; }
        }

        // Token: 0x17000362 RID: 866
        // (get) Token: 0x06000791 RID: 1937 RVA: 0x00026200 File Offset: 0x00024400
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000363 RID: 867
        // (get) Token: 0x06000792 RID: 1938 RVA: 0x00026208 File Offset: 0x00024408
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000364 RID: 868
        // (get) Token: 0x06000793 RID: 1939 RVA: 0x00026210 File Offset: 0x00024410
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000365 RID: 869
        // (get) Token: 0x06000794 RID: 1940 RVA: 0x00026214 File Offset: 0x00024414
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x17000366 RID: 870
        // (get) Token: 0x06000795 RID: 1941 RVA: 0x00026218 File Offset: 0x00024418
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06000797 RID: 1943 RVA: 0x00026224 File Offset: 0x00024424
        public override void Use(Player p, string message)
        {
            if (p.hasflag != null)
            {
                Player.SendMessage(p, "You can't use a gun while you have the flag!");
                return;
            }

            if (p.aiming && message == "")
            {
                p.aiming = false;
                p.ClearBlockchange();
                Player.SendMessage(p, "Disabled gun");
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
            else if (message.ToLower() == "laser")
            {
                pos.ending = 3;
            }
            else if (message.ToLower() == "teleport" || message.ToLower() == "tp")
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
            p.SendMessage("Gun mode engaged, fire at will");
            if (p.aiming) return;
            p.aiming = true;
            var toRemove = default(List<CatchPos>);
            var thread = new Thread((ThreadStart) delegate
            {
                var buffer = new List<CatchPos>();
                var tempBuffer = default(List<CatchPos>);
                var item = default(CatchPos);
                while (p.aiming)
                {
                    tempBuffer = new List<CatchPos>();
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
                                tempBuffer.Add(item);
                            }

                        toRemove = new List<CatchPos>();
                        buffer.ForEach(delegate(CatchPos cP)
                        {
                            if (!tempBuffer.Contains(cP))
                            {
                                p.SendBlockchange(cP.x, cP.y, cP.z, 0);
                                toRemove.Add(cP);
                            }
                        });
                        buffer.ForEach(delegate(CatchPos cP) { buffer.Remove(cP); });
                        tempBuffer.ForEach(delegate(CatchPos cP)
                        {
                            if (!buffer.Contains(cP))
                            {
                                buffer.Add(cP);
                                p.SendBlockchange(cP.x, cP.y, cP.z, 20);
                            }
                        });
                        tempBuffer.Clear();
                        toRemove.Clear();
                    }
                    catch
                    {
                    }

                    Thread.Sleep(20);
                }

                buffer.ForEach(delegate(CatchPos cP) { p.SendBlockchange(cP.x, cP.y, cP.z, 0); });
            });
            thread.Start();
        }

        // Token: 0x06000798 RID: 1944 RVA: 0x000263D4 File Offset: 0x000245D4
        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            var by = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, by);
            var bp = (Pos) p.blockchangeObject;
            var a = Math.Sin((128 - p.rot[0]) / 256.0 * 2.0 * Math.PI);
            var b = Math.Cos((128 - p.rot[0]) / 256.0 * 2.0 * Math.PI);
            var c = Math.Cos((p.rot[1] + 64) / 256.0 * 2.0 * Math.PI);
            var bigDiag = Math.Sqrt(Math.Sqrt(p.level.width * p.level.width + p.level.depth * p.level.depth) +
                                    p.level.height * p.level.height + p.level.width * p.level.width);
            var previous = new List<CatchPos>();
            var allBlocks = new List<CatchPos>();
            if (p.modeType != 0) type = p.modeType;
            var pos = default(CatchPos);
            var thread = new Thread((ThreadStart) delegate
            {
                var num = (ushort) (p.pos[0] / 32);
                var num2 = (ushort) (p.pos[1] / 32);
                var num3 = (ushort) (p.pos[2] / 32);
                pos.x = (ushort) Math.Round(num + a * 3.0);
                pos.y = (ushort) Math.Round(num2 + c * 3.0);
                pos.z = (ushort) Math.Round(num3 + b * 3.0);
                var comeOut = default(bool);
                var @break = default(bool);
                for (var num4 = 4.0; bigDiag > num4; num4 += 1.0)
                {
                    pos.x = (ushort) Math.Round(num + a * num4);
                    pos.y = (ushort) Math.Round(num2 + c * num4);
                    pos.z = (ushort) Math.Round(num3 + b * num4);
                    by = p.level.GetTile(pos.x, pos.y, pos.z);
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
                    comeOut = false;
                    @break = false;
                    Player.players.ForEach(delegate(Player pl)
                    {
                        if (!@break && pl.level == p.level &&
                            ((ushort) (pl.pos[0] / 32) == pos.x || (ushort) (pl.pos[0] / 32 + 1) == pos.x ||
                             (ushort) (pl.pos[0] / 32 - 1) == pos.x) &&
                            ((ushort) (pl.pos[1] / 32) == pos.y || (ushort) (pl.pos[1] / 32 + 1) == pos.y ||
                             (ushort) (pl.pos[1] / 32 - 1) == pos.y) && ((ushort) (pl.pos[2] / 32) == pos.z ||
                                                                         (ushort) (pl.pos[2] / 32 + 1) == pos.z ||
                                                                         (ushort) (pl.pos[2] / 32 - 1) == pos.z))
                        {
                            if (p.level.ctfmode && !p.level.ctfgame.friendlyfire && p.team == pl.team)
                            {
                                comeOut = true;
                                @break = true;
                            }
                            else
                            {
                                if (p.level.ctfmode)
                                {
                                    pl.health -= 25;
                                    if (pl.health > 0)
                                    {
                                        pl.SendMessage(string.Format(
                                            "You have been shot!  You have &c{0} health remaining.",
                                            pl.health + Server.DefaultColor));
                                        comeOut = true;
                                        @break = true;
                                        return;
                                    }
                                }

                                if (p.level.physics == 3 && bp.ending >= 2)
                                    pl.HandleDeath(4, string.Format(" was blown up by {0}", p.color + p.PublicName),
                                        true);
                                else
                                    pl.HandleDeath(4, string.Format(" was shot by {0}", p.color + p.PublicName));
                                comeOut = true;
                            }
                        }
                    });
                    if (comeOut) break;
                    if (num4 > 12.0 && bp.ending != 3)
                    {
                        pos = previous[0];
                        p.level.Blockchange(pos.x, pos.y, pos.z, 0);
                        previous.Remove(pos);
                    }

                    if (bp.ending != 3) Thread.Sleep(20);
                }

                if (bp.ending == -1)
                    try
                    {
                        p.SendPos(byte.MaxValue, (ushort) (previous[previous.Count - 3].x * 32),
                            (ushort) (previous[previous.Count - 3].y * 32 + 32),
                            (ushort) (previous[previous.Count - 3].z * 32), p.rot[0], p.rot[1]);
                    }
                    catch
                    {
                    }

                if (bp.ending == 3) Thread.Sleep(400);
                foreach (var item in previous)
                {
                    p.level.Blockchange(item.x, item.y, item.z, 0);
                    if (bp.ending != 3) Thread.Sleep(20);
                }
            });
            thread.Start();
        }

        // Token: 0x06000799 RID: 1945 RVA: 0x000265D8 File Offset: 0x000247D8
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/gun [at end] - Allows you to fire bullets at people");
            Player.SendMessage(p, "Available [at end] values: &cexplode, destroy, laser, tp");
        }

        // Token: 0x020000F1 RID: 241
        public struct CatchPos
        {
            // Token: 0x0400039B RID: 923
            public ushort x;

            // Token: 0x0400039C RID: 924
            public ushort y;

            // Token: 0x0400039D RID: 925
            public ushort z;
        }

        // Token: 0x020000F2 RID: 242
        public struct Pos
        {
            // Token: 0x0400039E RID: 926
            public ushort x;

            // Token: 0x0400039F RID: 927
            public ushort y;

            // Token: 0x040003A0 RID: 928
            public ushort z;

            // Token: 0x040003A1 RID: 929
            public int ending;
        }
    }
}