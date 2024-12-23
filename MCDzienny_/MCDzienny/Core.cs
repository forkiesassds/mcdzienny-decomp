using System;
using System.Collections.Generic;

namespace MCDzienny
{
    public class Core
    {

        public enum ĆĆ
        {
            ł,
            ę
        }

        public enum Ę
        {
            ć,
            ź,
            ń,
            l,
            u,
            q
        }

        public enum ĘĘ
        {
            ć,
            ę
        }

        static readonly List<int> óń = new List<int>();

        readonly float _ćź = 0.5f;

        readonly HashSet<int> bc = new HashSet<int>();

        readonly Vector3[] ć_ = new Vector3[2];

        readonly Ż[] ńó = new Ż[3];

        readonly List<mm> om = new List<mm>();

        readonly HashSet<Ż> ŻŻ = new HashSet<Ż>();

        Vector3 _ą;

        Vector3 _ć = default(Vector3);

        Vector3 _ę;

        Vector3 _ń;

        Vector3 _ó;

        byte _ź = byte.MaxValue;

        Vector3 _ż;

        Vector3 ą_;

        decimal aę;

        decimal ał;

        byte b;

        decimal bł;

        decimal dą;

        decimal eł;

        int eqś;

        decimal fł;

        decimal gę;

        decimal gł;

        decimal hł;

        decimal jł;

        decimal kę;

        decimal lę;

        decimal lł;

        decimal ló;

        bool lV;

        ĄĄ mą;

        public ĆĆ mć;

        public ĘĘ mę;

        ŹŹ mź;

        Vector3 ń_;

        Vector3 ó_;

        decimal oę;

        decimal oń;

        int p;

        decimal pę;

        decimal pś;

        decimal qę;

        decimal qł;

        decimal rę;

        decimal sł;

        decimal tę;

        decimal tł;

        decimal vł;

        decimal wą;

        decimal wł;

        decimal xź;

        decimal yę;

        decimal ył;

        decimal zę;

        public void BlockchangeF1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            try
            {
                p.ClearBlockchange();
                IT iT = (IT)p.blockchangeObject;
                if (iT.o == byte.MaxValue)
                {
                    iT.o = p.bindings[type];
                }
                byte tile = p.level.GetTile(x, y, z);
                p.SendBlockchange(x, y, z, tile);
                if (iT.o == tile)
                {
                    Player.SendMessage(p, "Cannot fill the same type.");
                    return;
                }
                if (!Block.canPlace(p, tile) && !Block.BuildIn(tile))
                {
                    Player.SendMessage(p, "Cannot fill that.");
                    return;
                }
                ÓŁk(x, y, z, iT.o, tile, p, iT.ą);
                if (p.staticCommands)
                {
                    p.Blockchange += BlockchangeF1;
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        public void ÓŁk(int x, int y, int z, byte ef, byte t, Player p, Ę et)
        {
            bc.Clear();
            new HashSet<mm>();
            int num = 0;
            switch (et)
            {
                case Ę.ć:
                    fa(x, y, z, t, p.level);
                    while (om.Count > 0)
                    {
                        num = om.Count;
                        if (bc.Count > p.group.maxBlocks)
                        {
                            break;
                        }
                        for (int l = 0; l < num; l++)
                        {
                            fa(om[l].ą, om[l].ę, om[l].ć, t, p.level);
                        }
                        om.RemoveRange(0, num);
                    }
                    break;
                case Ę.ź:
                    uf(x, y, z, t, p.level);
                    while (om.Count > 0)
                    {
                        num = om.Count;
                        if (bc.Count > p.group.maxBlocks)
                        {
                            break;
                        }
                        for (int n = 0; n < num; n++)
                        {
                            uf(om[n].ą, om[n].ę, om[n].ć, t, p.level);
                        }
                        om.RemoveRange(0, num);
                    }
                    break;
                case Ę.ń:
                    ff(x, y, z, t, p.level);
                    while (om.Count > 0)
                    {
                        num = om.Count;
                        if (bc.Count > p.group.maxBlocks)
                        {
                            break;
                        }
                        for (int j = 0; j < num; j++)
                        {
                            ff(om[j].ą, om[j].ę, om[j].ć, t, p.level);
                        }
                        om.RemoveRange(0, num);
                    }
                    break;
                case Ę.l:
                    lf(x, y, z, t, p.level);
                    while (om.Count > 0)
                    {
                        num = om.Count;
                        if (bc.Count > p.group.maxBlocks)
                        {
                            break;
                        }
                        for (int m = 0; m < num; m++)
                        {
                            lf(om[m].ą, om[m].ę, om[m].ć, t, p.level);
                        }
                        om.RemoveRange(0, num);
                    }
                    break;
                case Ę.u:
                    ll(x, y, z, t, p.level);
                    while (om.Count > 0)
                    {
                        num = om.Count;
                        if (bc.Count > p.group.maxBlocks)
                        {
                            break;
                        }
                        for (int k = 0; k < num; k++)
                        {
                            ll(om[k].ą, om[k].ę, om[k].ć, t, p.level);
                        }
                        om.RemoveRange(0, num);
                    }
                    break;
                case Ę.q:
                    zl(x, y, z, t, p.level);
                    while (om.Count > 0)
                    {
                        num = om.Count;
                        if (bc.Count > p.group.maxBlocks)
                        {
                            break;
                        }
                        for (int i = 0; i < num; i++)
                        {
                            zl(om[i].ą, om[i].ę, om[i].ć, t, p.level);
                        }
                        om.RemoveRange(0, num);
                    }
                    break;
            }
            if (bc.Count > p.group.maxBlocks)
            {
                Player.SendMessage(p, "You tried to fill over " + bc.Count + " blocks.");
                Player.SendMessage(p, "But your limit equals to " + p.group.maxBlocks + " blocks.");
                return;
            }
            foreach (int item in bc)
            {
                ushort x2;
                ushort y2;
                ushort z2;
                p.level.IntToPos(item, out x2, out y2, out z2);
                p.BlockChanges.Add(x2, y2, z2, ef);
            }
            p.BlockChanges.Commit();
            Player.SendMessage(p, "You filled " + bc.Count + " blocks.");
            om.Clear();
            bc.Clear();
        }

        public void fa(int yu, int ą, int ę, byte tt, Level level)
        {
            p = level.PosToInt(yu + 1, ą, ę);
            b = level.GetTile(p);
            if (b == tt && !bc.Contains(p))
            {
                bc.Add(p);
                om.Add(new mm
                {
                    ą = yu + 1,
                    ę = ą,
                    ć = ę
                });
            }
            p = level.PosToInt(yu - 1, ą, ę);
            b = level.GetTile(p);
            if (b == tt && !bc.Contains(p))
            {
                bc.Add(p);
                om.Add(new mm
                {
                    ą = yu - 1,
                    ę = ą,
                    ć = ę
                });
            }
            p = level.PosToInt(yu, ą + 1, ę);
            b = level.GetTile(p);
            if (b == tt && !bc.Contains(p))
            {
                bc.Add(p);
                om.Add(new mm
                {
                    ą = yu,
                    ę = ą + 1,
                    ć = ę
                });
            }
            p = level.PosToInt(yu, ą - 1, ę);
            b = level.GetTile(p);
            if (b == tt && !bc.Contains(p))
            {
                bc.Add(p);
                om.Add(new mm
                {
                    ą = yu,
                    ę = ą - 1,
                    ć = ę
                });
            }
            p = level.PosToInt(yu, ą, ę + 1);
            b = level.GetTile(p);
            if (b == tt && !bc.Contains(p))
            {
                bc.Add(p);
                om.Add(new mm
                {
                    ą = yu,
                    ę = ą,
                    ć = ę + 1
                });
            }
            p = level.PosToInt(yu, ą, ę - 1);
            b = level.GetTile(p);
            if (b == tt && !bc.Contains(p))
            {
                bc.Add(p);
                om.Add(new mm
                {
                    ą = yu,
                    ę = ą,
                    ć = ę - 1
                });
            }
        }

        public void uf(int ć, int ś, int ń, byte wt, Level level)
        {
            p = level.PosToInt(ć + 1, ś, ń);
            b = level.GetTile(p);
            if (b == wt && !bc.Contains(p))
            {
                bc.Add(p);
                om.Add(new mm
                {
                    ą = ć + 1,
                    ę = ś,
                    ć = ń
                });
            }
            p = level.PosToInt(ć - 1, ś, ń);
            b = level.GetTile(p);
            if (b == wt && !bc.Contains(p))
            {
                bc.Add(p);
                om.Add(new mm
                {
                    ą = ć - 1,
                    ę = ś,
                    ć = ń
                });
            }
            p = level.PosToInt(ć, ś + 1, ń);
            b = level.GetTile(p);
            if (b == wt && !bc.Contains(p))
            {
                bc.Add(p);
                om.Add(new mm
                {
                    ą = ć,
                    ę = ś + 1,
                    ć = ń
                });
            }
            p = level.PosToInt(ć, ś, ń + 1);
            b = level.GetTile(p);
            if (b == wt && !bc.Contains(p))
            {
                bc.Add(p);
                om.Add(new mm
                {
                    ą = ć,
                    ę = ś,
                    ć = ń + 1
                });
            }
            p = level.PosToInt(ć, ś, ń - 1);
            b = level.GetTile(p);
            if (b == wt && !bc.Contains(p))
            {
                bc.Add(p);
                om.Add(new mm
                {
                    ą = ć,
                    ę = ś,
                    ć = ń - 1
                });
            }
        }

        public void ff(int ł, int ó, int ą, byte tw, Level level)
        {
            p = level.PosToInt(ł + 1, ó, ą);
            b = level.GetTile(p);
            if (b == tw && !bc.Contains(p))
            {
                bc.Add(p);
                om.Add(new mm
                {
                    ą = ł + 1,
                    ę = ó,
                    ć = ą
                });
            }
            p = level.PosToInt(ł - 1, ó, ą);
            b = level.GetTile(p);
            if (b == tw && !bc.Contains(p))
            {
                bc.Add(p);
                om.Add(new mm
                {
                    ą = ł - 1,
                    ę = ó,
                    ć = ą
                });
            }
            p = level.PosToInt(ł, ó - 1, ą);
            b = level.GetTile(p);
            if (b == tw && !bc.Contains(p))
            {
                bc.Add(p);
                om.Add(new mm
                {
                    ą = ł,
                    ę = ó - 1,
                    ć = ą
                });
            }
            p = level.PosToInt(ł, ó, ą + 1);
            b = level.GetTile(p);
            if (b == tw && !bc.Contains(p))
            {
                bc.Add(p);
                om.Add(new mm
                {
                    ą = ł,
                    ę = ó,
                    ć = ą + 1
                });
            }
            p = level.PosToInt(ł, ó, ą - 1);
            b = level.GetTile(p);
            if (b == tw && !bc.Contains(p))
            {
                bc.Add(p);
                om.Add(new mm
                {
                    ą = ł,
                    ę = ó,
                    ć = ą - 1
                });
            }
        }

        public void lf(int ą, int ę, int ć, byte ąę, Level level)
        {
            p = level.PosToInt(ą + 1, ę, ć);
            b = level.GetTile(p);
            if (b == ąę && !bc.Contains(p))
            {
                bc.Add(p);
                om.Add(new mm
                {
                    ą = ą + 1,
                    ę = ę,
                    ć = ć
                });
            }
            p = level.PosToInt(ą - 1, ę, ć);
            b = level.GetTile(p);
            if (b == ąę && !bc.Contains(p))
            {
                bc.Add(p);
                om.Add(new mm
                {
                    ą = ą - 1,
                    ę = ę,
                    ć = ć
                });
            }
            p = level.PosToInt(ą, ę, ć + 1);
            b = level.GetTile(p);
            if (b == ąę && !bc.Contains(p))
            {
                bc.Add(p);
                om.Add(new mm
                {
                    ą = ą,
                    ę = ę,
                    ć = ć + 1
                });
            }
            p = level.PosToInt(ą, ę, ć - 1);
            b = level.GetTile(p);
            if (b == ąę && !bc.Contains(p))
            {
                bc.Add(p);
                om.Add(new mm
                {
                    ą = ą,
                    ę = ę,
                    ć = ć - 1
                });
            }
        }

        public void ll(int ł, int ń, int ą, byte qś, Level level)
        {
            p = level.PosToInt(ł, ń + 1, ą);
            b = level.GetTile(p);
            if (b == qś && !bc.Contains(p))
            {
                bc.Add(p);
                om.Add(new mm
                {
                    ą = ł,
                    ę = ń + 1,
                    ć = ą
                });
            }
            p = level.PosToInt(ł, ń - 1, ą);
            b = level.GetTile(p);
            if (b == qś && !bc.Contains(p))
            {
                bc.Add(p);
                om.Add(new mm
                {
                    ą = ł,
                    ę = ń - 1,
                    ć = ą
                });
            }
            p = level.PosToInt(ł, ń, ą + 1);
            b = level.GetTile(p);
            if (b == qś && !bc.Contains(p))
            {
                bc.Add(p);
                om.Add(new mm
                {
                    ą = ł,
                    ę = ń,
                    ć = ą + 1
                });
            }
            p = level.PosToInt(ł, ń, ą - 1);
            b = level.GetTile(p);
            if (b == qś && !bc.Contains(p))
            {
                bc.Add(p);
                om.Add(new mm
                {
                    ą = ł,
                    ę = ń,
                    ć = ą - 1
                });
            }
        }

        public void zl(int ś, int ź, int ł, byte źź, Level level)
        {
            p = level.PosToInt(ś + 1, ź, ł);
            b = level.GetTile(p);
            if (b == źź && !bc.Contains(p))
            {
                bc.Add(p);
                om.Add(new mm
                {
                    ą = ś + 1,
                    ę = ź,
                    ć = ł
                });
            }
            p = level.PosToInt(ś - 1, ź, ł);
            b = level.GetTile(p);
            if (b == źź && !bc.Contains(p))
            {
                bc.Add(p);
                om.Add(new mm
                {
                    ą = ś - 1,
                    ę = ź,
                    ć = ł
                });
            }
            p = level.PosToInt(ś, ź + 1, ł);
            b = level.GetTile(p);
            if (b == źź && !bc.Contains(p))
            {
                bc.Add(p);
                om.Add(new mm
                {
                    ą = ś,
                    ę = ź + 1,
                    ć = ł
                });
            }
            p = level.PosToInt(ś, ź - 1, ł);
            b = level.GetTile(p);
            if (b == źź && !bc.Contains(p))
            {
                bc.Add(p);
                om.Add(new mm
                {
                    ą = ś,
                    ę = ź - 1,
                    ć = ł
                });
            }
        }

        public void Ellipsoid(Player p, double[] ąę, byte ńń)
        {
            double[] array = new double[6]
            {
                Math.Abs(ąę[0] - ąę[3]) / 2.0, Math.Abs(ąę[1] - ąę[4]) / 2.0, Math.Abs(ąę[2] - ąę[5]) / 2.0, (ąę[0] + ąę[3]) / 2.0, (ąę[1] + ąę[4]) / 2.0,
                (ąę[2] + ąę[5]) / 2.0
            };
            var hashSet = new HashSet<Ex>();
            for (int i = (int)(0.0 - array[0]); i <= (int)array[0]; i++)
            {
                for (int j = (int)(0.0 - array[1]); j <= (int)array[1]; j++)
                {
                    if (!((0.0 - Math.Pow(array[1], 2.0)) * Math.Pow(i, 2.0) - Math.Pow(array[0], 2.0) * (Math.Pow(j, 2.0) - Math.Pow(array[1], 2.0)) < 0.0))
                    {
                        hashSet.Add(new Ex
                        {
                            o = (int)Math.Floor(i + array[3]),
                            l = (int)Math.Floor(j + array[4]),
                            r = (int)Math.Floor(
                                Math.Sqrt((0.0 - Math.Pow(array[1], 2.0)) * Math.Pow(i, 2.0) - Math.Pow(array[0], 2.0) * (Math.Pow(j, 2.0) - Math.Pow(array[1], 2.0))) *
                                Math.Abs(array[2] / (array[0] * array[1])) + array[5])
                        });
                        hashSet.Add(new Ex
                        {
                            o = (int)Math.Floor(i + array[3]),
                            l = (int)Math.Floor(j + array[4]),
                            r = (int)Math.Floor(
                                (0.0 - Math.Sqrt((0.0 - Math.Pow(array[1], 2.0)) * Math.Pow(i, 2.0) -
                                                 Math.Pow(array[0], 2.0) * (Math.Pow(j, 2.0) - Math.Pow(array[1], 2.0)))) * Math.Abs(array[2] / (array[0] * array[1])) +
                                array[5])
                        });
                    }
                }
            }
            for (int k = (int)(0.0 - array[2]); k <= (int)array[2]; k++)
            {
                for (int l = (int)(0.0 - array[1]); l <= (int)array[1]; l++)
                {
                    if (!((0.0 - Math.Pow(array[2], 2.0)) * Math.Pow(l, 2.0) - Math.Pow(array[1], 2.0) * (Math.Pow(k, 2.0) - Math.Pow(array[2], 2.0)) < 0.0))
                    {
                        hashSet.Add(new Ex
                        {
                            o = (int)Math.Floor(
                                Math.Sqrt((0.0 - Math.Pow(array[2], 2.0)) * Math.Pow(l, 2.0) - Math.Pow(array[1], 2.0) * (Math.Pow(k, 2.0) - Math.Pow(array[2], 2.0))) *
                                Math.Abs(array[0] / (array[2] * array[1])) + array[3]),
                            l = (int)Math.Floor(l + array[4]),
                            r = (int)Math.Floor(k + array[5])
                        });
                        hashSet.Add(new Ex
                        {
                            o = (int)Math.Floor(
                                (0.0 - Math.Sqrt((0.0 - Math.Pow(array[2], 2.0)) * Math.Pow(l, 2.0) -
                                                 Math.Pow(array[1], 2.0) * (Math.Pow(k, 2.0) - Math.Pow(array[2], 2.0)))) * Math.Abs(array[0] / (array[2] * array[1])) +
                                array[3]),
                            l = (int)Math.Floor(l + array[4]),
                            r = (int)Math.Floor(k + array[5])
                        });
                    }
                }
            }
            for (int m = (int)(0.0 - array[2]); m <= (int)array[2]; m++)
            {
                for (int n = (int)(0.0 - array[0]); n <= (int)array[0]; n++)
                {
                    if (!((0.0 - Math.Pow(array[2], 2.0)) * Math.Pow(n, 2.0) - Math.Pow(array[0], 2.0) * (Math.Pow(m, 2.0) - Math.Pow(array[2], 2.0)) < 0.0))
                    {
                        hashSet.Add(new Ex
                        {
                            o = (int)Math.Floor(n + array[3]),
                            l = (int)Math.Floor(
                                Math.Sqrt((0.0 - Math.Pow(array[2], 2.0)) * Math.Pow(n, 2.0) - Math.Pow(array[0], 2.0) * (Math.Pow(m, 2.0) - Math.Pow(array[2], 2.0))) *
                                Math.Abs(array[1] / (array[2] * array[0])) + array[4]),
                            r = (int)Math.Floor(m + array[5])
                        });
                        hashSet.Add(new Ex
                        {
                            o = (int)Math.Floor(n + array[3]),
                            l = (int)Math.Floor(
                                (0.0 - Math.Sqrt((0.0 - Math.Pow(array[2], 2.0)) * Math.Pow(n, 2.0) -
                                                 Math.Pow(array[0], 2.0) * (Math.Pow(m, 2.0) - Math.Pow(array[2], 2.0)))) * Math.Abs(array[1] / (array[2] * array[0])) +
                                array[4]),
                            r = (int)Math.Floor(m + array[5])
                        });
                    }
                }
            }
            if (p.group.maxBlocks < hashSet.Count)
            {
                Messages.TooManyBlocks(p, hashSet.Count);
                return;
            }
            foreach (Ex item in hashSet)
            {
                p.BlockChanges.Add((ushort)item.o, (ushort)item.l, (ushort)item.r, ńń);
            }
            p.BlockChanges.Commit();
        }

        public void PillarEraser(Player p, byte b, int o)
        {
            ushort x;
            ushort y;
            ushort z;
            p.level.IntToPos(o, out x, out y, out z);
            if (łą(b))
            {
                return;
            }
            if (Block.OPBlocks(b))
            {
                Player.SendMessage(p, "You are trying to remove OP block");
                return;
            }
            int num = 0;
            while (true)
            {
                byte tile = p.level.GetTile(x, (ushort)(y + 1 + num), z);
                if (b != tile || Ąó(p, x, (ushort)(y + 1 + num), z))
                {
                    break;
                }
                óń.Add(p.level.PosToInt(x, (ushort)(y + 1 + num), z));
                num++;
            }
            num = 0;
            while (true)
            {
                byte tile2 = p.level.GetTile(x, (ushort)(y - 1 - num), z);
                if (b != tile2 || Ąó(p, x, (ushort)(y - 1 - num), z))
                {
                    break;
                }
                óń.Add(p.level.PosToInt(x, (ushort)(y - 1 - num), z));
                num++;
            }
            óń.ForEach(delegate(int ę)
            {
                ushort x2;
                ushort y2;
                ushort z2;
                p.level.IntToPos(ę, out x2, out y2, out z2);
                p.BlockChanges.Add(x2, y2, z2, 0);
            });
            p.BlockChanges.Add(x, y, z, 0);
            p.BlockChanges.Commit();
            Player.SendMessage(p, "Pillar was removed.");
        }

        bool łą(byte b)
        {
            if (b == 0 || b == 105)
            {
                return true;
            }
            return false;
        }

        bool Ąó(Player p, ushort ą, ushort ę, ushort ł)
        {
            byte tile = p.level.GetTile((ushort)(ą + 1), ę, ł);
            if (łą(tile))
            {
                return false;
            }
            tile = p.level.GetTile((ushort)(ą - 1), ę, ł);
            if (łą(tile))
            {
                return false;
            }
            if (łą(p.level.GetTile(ą, ę, (ushort)(ł + 1))))
            {
                return false;
            }
            if (łą(p.level.GetTile(ą, ę, (ushort)(ł - 1))))
            {
                return false;
            }
            if (łą(p.level.GetTile((ushort)(ą + 1), ę, (ushort)(ł + 1))))
            {
                return false;
            }
            if (łą(p.level.GetTile((ushort)(ą - 1), ę, (ushort)(ł - 1))))
            {
                return false;
            }
            if (łą(p.level.GetTile((ushort)(ą + 1), ę, (ushort)(ł - 1))))
            {
                return false;
            }
            if (łą(p.level.GetTile((ushort)(ą - 1), ę, (ushort)(ł + 1))))
            {
                return false;
            }
            return true;
        }

        public void ĄŻ(Player p)
        {
            foreach (Ż item in ŻŻ)
            {
                byte tile = p.level.GetTile(item.ęę, item.ź, item.ć);
                p.SendBlockchange((ushort)item.ęę, (ushort)item.ź, (ushort)item.ć, tile);
            }
            lV = false;
        }

        public void ŻĄ(Player p)
        {
            ŻŻ.Clear();
            if (mć == ĆĆ.ł)
            {
                if (mą == ĄĄ.ę)
                {
                    for (int i = 0; i < p.level.depth; i++)
                    {
                        ŻŻ.Add(new Ż
                        {
                            ęę = (int)oń,
                            ć = i,
                            ź = (int)aę
                        });
                    }
                }
                else if (mą == ĄĄ.ć)
                {
                    for (int j = 0; j < p.level.width; j++)
                    {
                        int ęę = j;
                        int ć = (int)oń;
                        int ź = (int)aę;
                        ŻŻ.Add(new Ż
                        {
                            ęę = ęę,
                            ć = ć,
                            ź = ź
                        });
                    }
                }
                else
                {
                    for (int k = 0; k < p.level.width; k++)
                    {
                        int ęę2 = k;
                        int ć2 = (int)(k * tł + oń);
                        int ź2 = (int)aę;
                        ŻŻ.Add(new Ż
                        {
                            ęę = ęę2,
                            ć = ć2,
                            ź = ź2
                        });
                    }
                    for (int l = 0; l < p.level.depth; l++)
                    {
                        int ęę3 = (int)((l - oń) / tł);
                        int ć3 = l;
                        int ź3 = (int)aę;
                        ŻŻ.Add(new Ż
                        {
                            ęę = ęę3,
                            ć = ć3,
                            ź = ź3
                        });
                    }
                }
            }
            else if (mź == ŹŹ.ę)
            {
                for (int m = 0; m < p.level.depth; m++)
                {
                    ŻŻ.Add(new Ż
                    {
                        ęę = (int)gł,
                        ć = m,
                        ź = ńó[0].ź
                    });
                }
                for (int n = 0; n < p.level.width; n++)
                {
                    ŻŻ.Add(new Ż
                    {
                        ęę = n,
                        ć = (int)eł,
                        ź = ńó[0].ź
                    });
                }
            }
            else if (mź == ŹŹ.ć)
            {
                for (int num = 0; num < p.level.depth; num++)
                {
                    ŻŻ.Add(new Ż
                    {
                        ęę = (int)eł,
                        ć = num,
                        ź = ńó[0].ź
                    });
                }
                for (int num2 = 0; num2 < p.level.width; num2++)
                {
                    ŻŻ.Add(new Ż
                    {
                        ęę = num2,
                        ć = (int)gł,
                        ź = ńó[0].ź
                    });
                }
            }
            else
            {
                for (int num3 = 0; num3 < p.level.width; num3++)
                {
                    int ęę4 = num3;
                    int ć4 = (int)(num3 * ał + gł);
                    int ź4 = ńó[0].ź;
                    ŻŻ.Add(new Ż
                    {
                        ęę = ęę4,
                        ć = ć4,
                        ź = ź4
                    });
                }
                for (int num4 = 0; num4 < p.level.depth; num4++)
                {
                    int ęę5 = (int)((num4 - gł) / ał);
                    int ć5 = num4;
                    int ź5 = ńó[0].ź;
                    ŻŻ.Add(new Ż
                    {
                        ęę = ęę5,
                        ć = ć5,
                        ź = ź5
                    });
                }
                for (int num5 = 0; num5 < p.level.width; num5++)
                {
                    int ęę6 = num5;
                    int ć6 = (int)(num5 * hł + eł);
                    int ź6 = ńó[0].ź;
                    ŻŻ.Add(new Ż
                    {
                        ęę = ęę6,
                        ć = ć6,
                        ź = ź6
                    });
                }
                for (int num6 = 0; num6 < p.level.depth; num6++)
                {
                    int ęę7 = (int)((num6 - eł) / hł);
                    int ć7 = num6;
                    int ź7 = ńó[0].ź;
                    ŻŻ.Add(new Ż
                    {
                        ęę = ęę7,
                        ć = ć7,
                        ź = ź7
                    });
                }
            }
            foreach (Ż item in ŻŻ)
            {
                p.SendBlockchange((ushort)item.ęę, (ushort)item.ź, (ushort)item.ć, 14);
            }
            lV = true;
        }

        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type, byte ń)
        {
            if (lV)
            {
                p.core.ĄŻ(p);
            }
            p.ClearBlockchange2();
            byte tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            CP cP = (CP)p.blockchangeObject;
            cP.x = x;
            cP.y = y;
            cP.z = z;
            p.blockchangeObject = cP;
            p.Blockchange2 += Blockchange2;
        }

        public void Blockchange2(Player p, ushort x, ushort y, ushort z, byte type, byte ń)
        {
            p.ClearBlockchange2();
            byte tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            CP cP = (CP)p.blockchangeObject;
            mą = ĄĄ.ą;
            mć = ĆĆ.ł;
            if (cP.x - x == 0)
            {
                mą = ĄĄ.ę;
                oń = x;
            }
            else
            {
                oń = (z * cP.x - cP.z * x) / (cP.x - x);
                tł = (cP.z - oń) / cP.x;
                if (tł == 0m)
                {
                    mą = ĄĄ.ć;
                }
            }
            aę = cP.y;
            p.core.ŻĄ(p);
            p.Blockchange2 += Blockchange3;
        }

        public void Blockchange3(Player p, ushort x, ushort y, ushort z, byte type, byte ń)
        {
            p.ClearBlockchange2();
            if (ń == 0)
            {
                type = 0;
            }
            if (mę == ĘĘ.ć)
            {
                if (mą == ĄĄ.ą)
                {
                    lł = -1m / tł;
                    vł = z - lł * x;
                    xź = (oń - vł) / (lł - tł);
                    dą = lł * xź + vł;
                    ló = 2m * xź - x;
                    wą = 2m * dą - z;
                    pś = y;
                }
                else if (mą == ĄĄ.ę)
                {
                    ló = 2m * oń - x;
                    wą = z;
                    pś = y;
                }
                else if (mą == ĄĄ.ć)
                {
                    ló = x;
                    wą = 2m * oń - z;
                    pś = y;
                }
            }
            else if (mą == ĄĄ.ą)
            {
                lł = -1m / tł;
                vł = z - lł * x;
                xź = (oń - vł) / (lł - tł);
                dą = lł * xź + vł;
                ló = 2m * xź - x - 1m;
                wą = 2m * dą - z - 1m;
                pś = y;
            }
            else if (mą == ĄĄ.ę)
            {
                ló = 2m * oń - x - 1m;
                wą = z;
                pś = y;
            }
            else if (mą == ĄĄ.ć)
            {
                ló = x;
                wą = 2m * oń - z - 1m;
                pś = y;
            }
            p.ManualChangeCheck(x, y, z, ń, type);
            if (ló >= 0m && pś >= 0m && wą >= 0m)
            {
                p.ManualChangeCheck((ushort)ló, (ushort)pś, (ushort)wą, ń, type);
            }
            p.Blockchange2 += Blockchange3;
        }

        public void Blockchange4(Player p, ushort x, ushort y, ushort z, byte type, byte ń)
        {
            if (lV)
            {
                p.core.ĄŻ(p);
            }
            p.ClearBlockchange2();
            byte tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            if (eqś < 2)
            {
                ńó[eqś] = new Ż();
                ńó[eqś].ęę = x;
                ńó[eqś].ź = y;
                ńó[eqś].ć = z;
                eqś++;
                p.Blockchange2 += Blockchange4;
            }
            else
            {
                ńó[eqś] = new Ż();
                ńó[eqś].ęę = x;
                ńó[eqś].ź = y;
                ńó[eqś].ć = z;
                eqś = 0;
                ÓW();
                p.core.ŻĄ(p);
                p.Blockchange2 += Blockchange5;
            }
        }

        public void ÓW()
        {
            mź = ŹŹ.ł;
            mć = ĆĆ.ę;
            if (ńó[0].ęę - ńó[1].ęę == 0)
            {
                mź = ŹŹ.ę;
                gł = ńó[1].ęę;
            }
            else
            {
                gł = (ńó[1].ć * ńó[0].ęę - ńó[0].ć * ńó[1].ęę) / (ńó[0].ęę - ńó[1].ęę);
                ał = (ńó[0].ć - gł) / ńó[0].ęę;
                if (ał == 0m)
                {
                    mź = ŹŹ.ć;
                }
            }
            aę = ńó[0].ź;
            if (mź == ŹŹ.ę)
            {
                eł = ńó[2].ć;
                return;
            }
            if (mź == ŹŹ.ć)
            {
                eł = ńó[2].ęę;
                return;
            }
            hł = -1m / ał;
            eł = ńó[2].ć - hł * ńó[2].ęę;
        }

        public void Blockchange5(Player p, ushort x, ushort y, ushort z, byte type, byte ń)
        {
            p.ClearBlockchange2();
            if (ń == 0)
            {
                type = 0;
            }
            if (mę == ĘĘ.ć)
            {
                if (mź == ŹŹ.ł)
                {
                    wł = -1m / ał;
                    qł = z - wł * x;
                    rę = (gł - qł) / (wł - ał);
                    lę = wł * rę + qł;
                    jł = 2m * rę - x;
                    tę = 2m * lę - z;
                    kę = y;
                    sł = -1m / hł;
                    ył = z - sł * x;
                    oę = (eł - ył) / (sł - hł);
                    yę = sł * oę + ył;
                    bł = 2m * oę - x;
                    pę = 2m * yę - z;
                    zę = y;
                    fł = jł + bł - x;
                    gę = tę + pę - z;
                    qę = y;
                }
                else if (mź == ŹŹ.ę)
                {
                    jł = 2m * gł - x;
                    tę = z;
                    kę = y;
                    bł = x;
                    pę = 2m * eł - z;
                    zę = y;
                    fł = 2m * gł - x;
                    gę = 2m * eł - z;
                    qę = y;
                }
                else if (mź == ŹŹ.ć)
                {
                    jł = 2m * eł - x;
                    tę = z;
                    kę = y;
                    bł = x;
                    pę = 2m * gł - z;
                    zę = y;
                    fł = 2m * eł - x;
                    gę = 2m * gł - z;
                    qę = y;
                }
            }
            else if (mź == ŹŹ.ł)
            {
                wł = hł;
                qł = z - wł * x;
                rę = (gł - qł) / (wł - ał);
                lę = wł * rę + qł;
                jł = 2m * rę - x - 1m;
                tę = 2m * lę - z - 1m;
                kę = y;
                sł = ał;
                ył = z - sł * x;
                oę = (eł - ył) / (sł - hł);
                yę = sł * oę + ył;
                bł = 2m * oę - x - 1m;
                pę = 2m * yę - z - 1m;
                zę = y;
                fł = jł + bł - x;
                gę = tę + pę - z;
                qę = y;
            }
            else if (mź == ŹŹ.ę)
            {
                jł = 2m * gł - x - 1m;
                tę = z;
                kę = y;
                bł = x;
                pę = 2m * eł - z - 1m;
                zę = y;
                fł = 2m * gł - x - 1m;
                gę = 2m * eł - z - 1m;
                qę = y;
            }
            else if (mź == ŹŹ.ć)
            {
                jł = 2m * eł - x - 1m;
                tę = z;
                kę = y;
                bł = x;
                pę = 2m * gł - z - 1m;
                zę = y;
                fł = 2m * eł - x - 1m;
                gę = 2m * gł - z - 1m;
                qę = y;
            }
            p.ManualChangeCheck(x, y, z, ń, type);
            if (jł >= 0m && zę >= 0m && pę >= 0m)
            {
                p.ManualChangeCheck((ushort)jł, (ushort)kę, (ushort)tę, ń, type);
            }
            if (jł >= 0m && zę >= 0m && pę >= 0m)
            {
                p.ManualChangeCheck((ushort)bł, (ushort)zę, (ushort)pę, ń, type);
            }
            if (jł >= 0m && zę >= 0m && pę >= 0m)
            {
                p.ManualChangeCheck((ushort)fł, (ushort)qę, (ushort)gę, ń, type);
            }
            p.Blockchange2 += Blockchange5;
        }

        public void DrawTriangle(Player p, List<BlockPoints> blockPoints)
        {
            _ź = blockPoints[2].blockType;
            _ę = blockPoints[0].position;
            _ż = blockPoints[1].position;
            _ó = blockPoints[2].position;
            ć_[0] = new Vector3(Math.Min(Math.Min(_ę.X, _ż.X), _ó.X), Math.Min(Math.Min(_ę.Y, _ż.Y), _ó.Y), Math.Min(Math.Min(_ę.Z, _ż.Z), _ó.Z));
            ć_[1] = new Vector3(Math.Max(Math.Max(_ę.X, _ż.X), _ó.X), Math.Max(Math.Max(_ę.Y, _ż.Y), _ó.Y), Math.Max(Math.Max(_ę.Z, _ż.Z), _ó.Z));
            _ć = ć_[0];
            _ą = (_ż - _ę).Cross(_ó - _ę);
            ą_ = _ą.Normalize();
            _ń = _ą.Cross(_ę - _ż).Normalize();
            ń_ = _ą.Cross(_ż - _ó).Normalize();
            ó_ = _ą.Cross(_ó - _ę).Normalize();
            int num = óń_(p);
            if (num > p.group.maxBlocks)
            {
                Player.SendMessage(p, "You tried to place " + num + " but you can only build " + p.group.maxBlocks + " blocks.");
                p.BlockChanges.Abort();
            }
            else
            {
                p.BlockChanges.Commit();
                Player.SendMessage(p, "You drew a triangle that consists of " + num + " blocks.");
            }
        }

        public void DrawQuad(Player p, List<BlockPoints> ąęć_)
        {
            _ź = ąęć_[3].blockType;
            _ę = ąęć_[0].position;
            _ż = ąęć_[1].position;
            _ó = ąęć_[2].position;
            ć_[0] = new Vector3(Math.Min(Math.Min(_ę.X, _ż.X), _ó.X), Math.Min(Math.Min(_ę.Y, _ż.Y), _ó.Y), Math.Min(Math.Min(_ę.Z, _ż.Z), _ó.Z));
            ć_[1] = new Vector3(Math.Max(Math.Max(_ę.X, _ż.X), _ó.X), Math.Max(Math.Max(_ę.Y, _ż.Y), _ó.Y), Math.Max(Math.Max(_ę.Z, _ż.Z), _ó.Z));
            _ć = ć_[0];
            _ą = (_ż - _ę).Cross(_ó - _ę);
            ą_ = _ą.Normalize();
            _ń = _ą.Cross(_ę - _ż).Normalize();
            ń_ = _ą.Cross(_ż - _ó).Normalize();
            ó_ = _ą.Cross(_ó - _ę).Normalize();
            int num = óń_(p);
            _ę = ąęć_[2].position;
            _ż = ąęć_[3].position;
            _ó = ąęć_[0].position;
            ć_[0] = new Vector3(Math.Min(Math.Min(_ę.X, _ż.X), _ó.X), Math.Min(Math.Min(_ę.Y, _ż.Y), _ó.Y), Math.Min(Math.Min(_ę.Z, _ż.Z), _ó.Z));
            ć_[1] = new Vector3(Math.Max(Math.Max(_ę.X, _ż.X), _ó.X), Math.Max(Math.Max(_ę.Y, _ż.Y), _ó.Y), Math.Max(Math.Max(_ę.Z, _ż.Z), _ó.Z));
            _ć = ć_[0];
            _ą = (_ż - _ę).Cross(_ó - _ę);
            ą_ = _ą.Normalize();
            _ń = _ą.Cross(_ę - _ż).Normalize();
            ń_ = _ą.Cross(_ż - _ó).Normalize();
            ó_ = _ą.Cross(_ó - _ę).Normalize();
            num += óń_(p);
            if (num > p.group.maxBlocks)
            {
                Player.SendMessage(p, "You tried to place " + num + " but you can only build " + p.group.maxBlocks + " blocks.");
                p.BlockChanges.Abort();
            }
            else
            {
                p.BlockChanges.Commit();
                Player.SendMessage(p, "You drew a quad that consists of " + num + " blocks.");
            }
        }

        int óń_(Player p)
        {
            int num = 0;
            while (_ć.X <= ć_[1].X)
            {
                while (_ć.Y <= ć_[1].Y)
                {
                    while (_ć.Z <= ć_[1].Z)
                    {
                        if (źć_())
                        {
                            p.BlockChanges.Add((ushort)_ć.X, (ushort)_ć.Z, (ushort)_ć.Y, _ź);
                            num++;
                        }
                        _ć.Z += 1f;
                    }
                    _ć.Z = ć_[0].Z;
                    _ć.Y += 1f;
                }
                _ć.Y = ć_[0].Y;
                _ć.X += 1f;
            }
            return num;
        }

        bool źć_()
        {
            if (Math.Abs(ą_.Dot(_ć - _ę)) > 1f)
            {
                return false;
            }
            if ((_ć - _ę).Dot(_ń) > _ćź || (_ć - _ż).Dot(ń_) > _ćź || (_ć - _ó).Dot(ó_) > _ćź)
            {
                return false;
            }
            if (!_źć(1, 0, 0) && !_źć(0, 1, 0))
            {
                return _źć(0, 0, 1);
            }
            return true;
        }

        bool _źć(int x, int y, int z)
        {
            Vector3 vec = new Vector3(x, y, z);
            int num = (int)_ą.Dot(_ę - _ć);
            int num2 = (int)_ą.Dot(vec);
            if (num2 == 0)
            {
                return num == 0;
            }
            double num3 = num / (double)num2;
            if (num3 > 0f - _ćź)
            {
                return num3 <= _ćź;
            }
            return false;
        }

        public static int PrepareCone(Player s, int ś, int ń, int ó, int ę, int śś, byte ź)
        {
            ś--;
            double num = ś * ś;
            double num2 = ń * ń;
            for (int i = ó - ś; i <= ó + ś; i++)
            {
                for (int j = śś - ś; j <= śś + ś; j++)
                {
                    int num3 = (int)Math.Round(Math.Sqrt(Math.Pow(i - ó, 2.0) + Math.Pow(j - śś, 2.0)) * (ń / (double)ś));
                    if (ń - num3 - 1 >= 0)
                    {
                        int num4 = ń - num3 - 1;
                        s.BlockChanges.Add(i, num4 + ę, j, ź);
                    }
                }
            }
            for (int k = ó - ś; k <= ó + ś; k++)
            {
                for (int l = ę; l < ę + ń; l++)
                {
                    int num5 = (int)Math.Round(Math.Sqrt(num * Math.Pow(l - ę, 2.0) - num2 * Math.Pow(k - ó, 2.0)) / Math.Abs(ń));
                    if (num5 >= 0)
                    {
                        int y = ń - l + 2 * ę - 1;
                        s.BlockChanges.Add(k, y, -num5 + śś, ź);
                        s.BlockChanges.Add(k, y, num5 + śś, ź);
                    }
                }
            }
            for (int m = śś - ś; m <= śś + ś; m++)
            {
                for (int n = ę; n < ę + ń; n++)
                {
                    int num6 = (int)Math.Round(Math.Sqrt(num * Math.Pow(n - ę, 2.0) - num2 * Math.Pow(m - śś, 2.0)) / Math.Abs(ń));
                    if (num6 >= 0)
                    {
                        int y2 = ń - n + 2 * ę - 1;
                        s.BlockChanges.Add(-num6 + ó, y2, m, ź);
                        s.BlockChanges.Add(num6 + ó, y2, m, ź);
                    }
                }
            }
            return ś;
        }

        public struct IT
        {
            public ushort x;

            public ushort y;

            public ushort z;

            public byte o;

            public Ę ą;
        }

        class mm
        {
            public int ą;

            public int ć;

            public int ę;
        }

        class Ex
        {

            public int l;
            public int o;

            public int r;
        }

        enum ĄĄ
        {
            ą,
            ę,
            ć
        }

        enum ŹŹ
        {
            ł,
            ę,
            ć
        }

        class Ż
        {

            public int ć;
            public int ęę;

            public int ź;
        }

        public struct CP
        {
            public ushort x;

            public ushort y;

            public ushort z;

            public byte type;
        }
    }
}