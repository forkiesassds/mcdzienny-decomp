using System;
using System.Collections.Generic;
using System.Threading;

namespace MCDzienny
{
    public class CmdGun : Command
    {

        public override string name { get { return "gun"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override bool ConsoleAccess { get { return false; } }

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
            if (p.aiming)
            {
                return;
            }
            p.aiming = true;
            Thread thread = new Thread((ThreadStart)delegate
            {
                var buffer = new List<CatchPos>();
                CatchPos item = default(CatchPos);
                while (p.aiming)
                {
                    var tempBuffer = new List<CatchPos>();
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
                                        tempBuffer.Add(item);
                                    }
                                }
                            }
                        }
                        var toRemove = new List<CatchPos>();
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
                    catch {}
                    Thread.Sleep(20);
                }
                buffer.ForEach(delegate(CatchPos cP) { p.SendBlockchange(cP.x, cP.y, cP.z, 0); });
            });
            thread.Start();
        }

        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            byte by = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, by);
            Pos bp = (Pos)p.blockchangeObject;
            double a = Math.Sin((128 - p.rot[0]) / 256.0 * 2.0 * Math.PI);
            double b = Math.Cos((128 - p.rot[0]) / 256.0 * 2.0 * Math.PI);
            double c2 = Math.Cos((p.rot[1] + 64) / 256.0 * 2.0 * Math.PI);
            double bigDiag = Math.Sqrt(Math.Sqrt(p.level.width * p.level.width + p.level.depth * p.level.depth) + p.level.height * p.level.height +
                                       p.level.width * p.level.width);
            var previous = new List<CatchPos>();
            var allBlocks = new List<CatchPos>();
            if (p.modeType != 0)
            {
                type = p.modeType;
            }
            CatchPos pos = default(CatchPos);
            Thread thread = new Thread((ThreadStart)delegate
            {
                ushort num = (ushort)(p.pos[0] / 32);
                ushort num2 = (ushort)(p.pos[1] / 32);
                ushort num3 = (ushort)(p.pos[2] / 32);
                pos.x = (ushort)Math.Round(num + a * 3.0);
                pos.y = (ushort)Math.Round(num2 + c2 * 3.0);
                pos.z = (ushort)Math.Round(num3 + b * 3.0);
                for (double num4 = 4.0; bigDiag > num4; num4 += 1.0)
                {
                    pos.x = (ushort)Math.Round(num + a * num4);
                    pos.y = (ushort)Math.Round(num2 + c2 * num4);
                    pos.z = (ushort)Math.Round(num3 + b * num4);
                    by = p.level.GetTile(pos.x, pos.y, pos.z);
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
                    bool comeOut = false;
                    bool @break = false;
                    Player.players.ForEach(delegate(Player pl)
                    {
                        if (!@break && pl.level == p.level &&
                            ((ushort)(pl.pos[0] / 32) == pos.x || (ushort)(pl.pos[0] / 32 + 1) == pos.x || (ushort)(pl.pos[0] / 32 - 1) == pos.x) &&
                            ((ushort)(pl.pos[1] / 32) == pos.y || (ushort)(pl.pos[1] / 32 + 1) == pos.y || (ushort)(pl.pos[1] / 32 - 1) == pos.y) &&
                            ((ushort)(pl.pos[2] / 32) == pos.z || (ushort)(pl.pos[2] / 32 + 1) == pos.z || (ushort)(pl.pos[2] / 32 - 1) == pos.z))
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
                                        pl.SendMessage(string.Format("You have been shot!  You have &c{0} health remaining.", pl.health + Server.DefaultColor));
                                        comeOut = true;
                                        @break = true;
                                        return;
                                    }
                                }
                                if (p.level.physics == 3 && bp.ending >= 2)
                                {
                                    pl.HandleDeath(4, string.Format(" was blown up by {0}", p.color + p.PublicName), explode: true);
                                }
                                else
                                {
                                    pl.HandleDeath(4, string.Format(" was shot by {0}", p.color + p.PublicName));
                                }
                                comeOut = true;
                            }
                        }
                    });
                    if (comeOut)
                    {
                        break;
                    }
                    if (num4 > 12.0 && bp.ending != 3)
                    {
                        pos = previous[0];
                        p.level.Blockchange(pos.x, pos.y, pos.z, 0);
                        previous.Remove(pos);
                    }
                    if (bp.ending != 3)
                    {
                        Thread.Sleep(20);
                    }
                }
                if (bp.ending == -1)
                {
                    try
                    {
                        p.SendPos(byte.MaxValue, (ushort)(previous[previous.Count - 3].x * 32), (ushort)(previous[previous.Count - 3].y * 32 + 32),
                                  (ushort)(previous[previous.Count - 3].z * 32), p.rot[0], p.rot[1]);
                    }
                    catch {}
                }
                if (bp.ending == 3)
                {
                    Thread.Sleep(400);
                }
                foreach (CatchPos item in previous)
                {
                    p.level.Blockchange(item.x, item.y, item.z, 0);
                    if (bp.ending != 3)
                    {
                        Thread.Sleep(20);
                    }
                }
            });
            thread.Start();
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/gun [at end] - Allows you to fire bullets at people");
            Player.SendMessage(p, "Available [at end] values: &cexplode, destroy, laser, tp");
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