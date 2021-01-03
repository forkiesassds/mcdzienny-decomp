using System;
using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x02000146 RID: 326
    public class Core
    {
        // Token: 0x0200014D RID: 333
        public enum ĆĆ
        {
            // Token: 0x0400044E RID: 1102
            ł,

            // Token: 0x0400044F RID: 1103
            ę
        }

        // Token: 0x02000148 RID: 328
        public enum Ę
        {
            // Token: 0x0400043A RID: 1082
            ć,

            // Token: 0x0400043B RID: 1083
            ź,

            // Token: 0x0400043C RID: 1084
            ń,

            // Token: 0x0400043D RID: 1085
            l,

            // Token: 0x0400043E RID: 1086
            u,

            // Token: 0x0400043F RID: 1087
            q
        }

        // Token: 0x0200014C RID: 332
        public enum ĘĘ
        {
            // Token: 0x0400044B RID: 1099
            ć,

            // Token: 0x0400044C RID: 1100
            ę
        }

        // Token: 0x04000400 RID: 1024
        private static readonly List<int> óń = new List<int>();

        // Token: 0x0400042E RID: 1070
        private Vector3 _ą;

        // Token: 0x04000430 RID: 1072
        private Vector3 _ć = default(Vector3);

        // Token: 0x04000433 RID: 1075
        private readonly float _ćź = 0.5f;

        // Token: 0x04000428 RID: 1064
        private Vector3 _ę;

        // Token: 0x0400042B RID: 1067
        private Vector3 _ń;

        // Token: 0x0400042A RID: 1066
        private Vector3 _ó;

        // Token: 0x04000432 RID: 1074
        private byte _ź = byte.MaxValue;

        // Token: 0x04000429 RID: 1065
        private Vector3 _ż;

        // Token: 0x0400042F RID: 1071
        private Vector3 ą_;

        // Token: 0x04000402 RID: 1026
        private decimal aę;

        // Token: 0x0400040C RID: 1036
        private decimal ał;

        // Token: 0x040003FD RID: 1021
        private byte b;

        // Token: 0x040003FF RID: 1023
        private readonly HashSet<int> bc = new HashSet<int>();

        // Token: 0x04000415 RID: 1045
        private decimal bł;

        // Token: 0x04000431 RID: 1073
        private readonly Vector3[] ć_ = new Vector3[2];

        // Token: 0x04000406 RID: 1030
        private decimal dą;

        // Token: 0x0400040F RID: 1039
        private decimal eł;

        // Token: 0x04000426 RID: 1062
        private int eqś;

        // Token: 0x04000416 RID: 1046
        private decimal fł;

        // Token: 0x0400041C RID: 1052
        private decimal gę;

        // Token: 0x0400040D RID: 1037
        private decimal gł;

        // Token: 0x0400040E RID: 1038
        private decimal hł;

        // Token: 0x04000414 RID: 1044
        private decimal jł;

        // Token: 0x04000417 RID: 1047
        private decimal kę;

        // Token: 0x0400041F RID: 1055
        private decimal lę;

        // Token: 0x0400040A RID: 1034
        private decimal lł;

        // Token: 0x04000407 RID: 1031
        private decimal ló;

        // Token: 0x04000401 RID: 1025
        private bool lV;

        // Token: 0x04000421 RID: 1057
        private ĄĄ mą;

        // Token: 0x04000423 RID: 1059
        public ĆĆ mć;

        // Token: 0x04000422 RID: 1058
        public ĘĘ mę;

        // Token: 0x04000424 RID: 1060
        private ŹŹ mź;

        // Token: 0x0400042C RID: 1068
        private Vector3 ń_;

        // Token: 0x04000425 RID: 1061
        private readonly Ż[] ńó = new Ż[3];

        // Token: 0x0400042D RID: 1069
        private Vector3 ó_;

        // Token: 0x0400041E RID: 1054
        private decimal oę;

        // Token: 0x040003FE RID: 1022
        private readonly List<mm> om = new List<mm>();

        // Token: 0x04000404 RID: 1028
        private decimal oń;

        // Token: 0x040003FC RID: 1020
        private int p;

        // Token: 0x0400041B RID: 1051
        private decimal pę;

        // Token: 0x04000408 RID: 1032
        private decimal pś;

        // Token: 0x04000419 RID: 1049
        private decimal qę;

        // Token: 0x04000411 RID: 1041
        private decimal qł;

        // Token: 0x0400041D RID: 1053
        private decimal rę;

        // Token: 0x04000412 RID: 1042
        private decimal sł;

        // Token: 0x0400041A RID: 1050
        private decimal tę;

        // Token: 0x04000403 RID: 1027
        private decimal tł;

        // Token: 0x0400040B RID: 1035
        private decimal vł;

        // Token: 0x04000409 RID: 1033
        private decimal wą;

        // Token: 0x04000410 RID: 1040
        private decimal wł;

        // Token: 0x04000405 RID: 1029
        private decimal xź;

        // Token: 0x04000420 RID: 1056
        private decimal yę;

        // Token: 0x04000413 RID: 1043
        private decimal ył;

        // Token: 0x04000418 RID: 1048
        private decimal zę;

        // Token: 0x04000427 RID: 1063
        private readonly HashSet<Ż> ŻŻ = new HashSet<Ż>();

        // Token: 0x060009A1 RID: 2465 RVA: 0x0002F310 File Offset: 0x0002D510
        public void BlockchangeF1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            try
            {
                p.ClearBlockchange();
                var it = (IT) p.blockchangeObject;
                if (it.o == 255) it.o = p.bindings[type];
                var tile = p.level.GetTile(x, y, z);
                p.SendBlockchange(x, y, z, tile);
                if (it.o == tile)
                {
                    Player.SendMessage(p, "Cannot fill the same type.");
                }
                else if (!Block.canPlace(p, tile) && !Block.BuildIn(tile))
                {
                    Player.SendMessage(p, "Cannot fill that.");
                }
                else
                {
                    ÓŁk(x, y, z, it.o, tile, p, it.ą);
                    if (p.staticCommands) p.Blockchange += BlockchangeF1;
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x060009A2 RID: 2466 RVA: 0x0002F3EC File Offset: 0x0002D5EC
        public void ÓŁk(int x, int y, int z, byte ef, byte t, Player p, Ę et)
        {
            bc.Clear();
            new HashSet<mm>();
            if (et == Ę.ć)
            {
                fa(x, y, z, t, p.level);
                while (om.Count > 0)
                {
                    var count = om.Count;
                    if (bc.Count > p.group.maxBlocks) break;
                    for (var i = 0; i < count; i++) fa(om[i].ą, om[i].ę, om[i].ć, t, p.level);
                    om.RemoveRange(0, count);
                }
            }
            else if (et == Ę.ź)
            {
                uf(x, y, z, t, p.level);
                while (om.Count > 0)
                {
                    var count = om.Count;
                    if (bc.Count > p.group.maxBlocks) break;
                    for (var j = 0; j < count; j++) uf(om[j].ą, om[j].ę, om[j].ć, t, p.level);
                    om.RemoveRange(0, count);
                }
            }
            else if (et == Ę.ń)
            {
                ff(x, y, z, t, p.level);
                while (om.Count > 0)
                {
                    var count = om.Count;
                    if (bc.Count > p.group.maxBlocks) break;
                    for (var k = 0; k < count; k++) ff(om[k].ą, om[k].ę, om[k].ć, t, p.level);
                    om.RemoveRange(0, count);
                }
            }
            else if (et == Ę.l)
            {
                lf(x, y, z, t, p.level);
                while (om.Count > 0)
                {
                    var count = om.Count;
                    if (bc.Count > p.group.maxBlocks) break;
                    for (var l = 0; l < count; l++) lf(om[l].ą, om[l].ę, om[l].ć, t, p.level);
                    om.RemoveRange(0, count);
                }
            }
            else if (et == Ę.u)
            {
                ll(x, y, z, t, p.level);
                while (om.Count > 0)
                {
                    var count = om.Count;
                    if (bc.Count > p.group.maxBlocks) break;
                    for (var m = 0; m < count; m++) ll(om[m].ą, om[m].ę, om[m].ć, t, p.level);
                    om.RemoveRange(0, count);
                }
            }
            else if (et == Ę.q)
            {
                zl(x, y, z, t, p.level);
                while (om.Count > 0)
                {
                    var count = om.Count;
                    if (bc.Count > p.group.maxBlocks) break;
                    for (var n = 0; n < count; n++) zl(om[n].ą, om[n].ę, om[n].ć, t, p.level);
                    om.RemoveRange(0, count);
                }
            }

            if (bc.Count > p.group.maxBlocks)
            {
                Player.SendMessage(p, "You tried to fill over " + bc.Count + " blocks.");
                Player.SendMessage(p, "But your limit equals to " + p.group.maxBlocks + " blocks.");
                return;
            }

            foreach (var pos in bc)
            {
                ushort x2;
                ushort y2;
                ushort z2;
                p.level.IntToPos(pos, out x2, out y2, out z2);
                p.BlockChanges.Add(x2, y2, z2, ef);
            }

            p.BlockChanges.Commit();
            Player.SendMessage(p, "You filled " + bc.Count + " blocks.");
            om.Clear();
            bc.Clear();
        }

        // Token: 0x060009A3 RID: 2467 RVA: 0x0002F984 File Offset: 0x0002DB84
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

        // Token: 0x060009A4 RID: 2468 RVA: 0x0002FC8C File Offset: 0x0002DE8C
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

        // Token: 0x060009A5 RID: 2469 RVA: 0x0002FF10 File Offset: 0x0002E110
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

        // Token: 0x060009A6 RID: 2470 RVA: 0x00030194 File Offset: 0x0002E394
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

        // Token: 0x060009A7 RID: 2471 RVA: 0x00030398 File Offset: 0x0002E598
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

        // Token: 0x060009A8 RID: 2472 RVA: 0x0003059C File Offset: 0x0002E79C
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

        // Token: 0x060009A9 RID: 2473 RVA: 0x000307A0 File Offset: 0x0002E9A0
        public void Ellipsoid(Player p, double[] ąę, byte ńń)
        {
            double[] array =
            {
                Math.Abs(ąę[0] - ąę[3]) / 2.0,
                Math.Abs(ąę[1] - ąę[4]) / 2.0,
                Math.Abs(ąę[2] - ąę[5]) / 2.0,
                (ąę[0] + ąę[3]) / 2.0,
                (ąę[1] + ąę[4]) / 2.0,
                (ąę[2] + ąę[5]) / 2.0
            };
            var hashSet = new HashSet<Ex>();
            for (var i = -(int) array[0]; i <= (int) array[0]; i++)
            for (var j = -(int) array[1]; j <= (int) array[1]; j++)
                if (-Math.Pow(array[1], 2.0) * Math.Pow(i, 2.0) -
                    Math.Pow(array[0], 2.0) * (Math.Pow(j, 2.0) - Math.Pow(array[1], 2.0)) >= 0.0)
                {
                    hashSet.Add(new Ex
                    {
                        o = (int) Math.Floor(i + array[3]),
                        l = (int) Math.Floor(j + array[4]),
                        r = (int) Math.Floor(
                            Math.Sqrt(-Math.Pow(array[1], 2.0) * Math.Pow(i, 2.0) - Math.Pow(array[0], 2.0) *
                                (Math.Pow(j, 2.0) - Math.Pow(array[1], 2.0))) *
                            Math.Abs(array[2] / (array[0] * array[1])) + array[5])
                    });
                    hashSet.Add(new Ex
                    {
                        o = (int) Math.Floor(i + array[3]),
                        l = (int) Math.Floor(j + array[4]),
                        r = (int) Math.Floor(
                            -Math.Sqrt(-Math.Pow(array[1], 2.0) * Math.Pow(i, 2.0) - Math.Pow(array[0], 2.0) *
                                (Math.Pow(j, 2.0) - Math.Pow(array[1], 2.0))) *
                            Math.Abs(array[2] / (array[0] * array[1])) + array[5])
                    });
                }

            for (var k = -(int) array[2]; k <= (int) array[2]; k++)
            for (var l = -(int) array[1]; l <= (int) array[1]; l++)
                if (-Math.Pow(array[2], 2.0) * Math.Pow(l, 2.0) -
                    Math.Pow(array[1], 2.0) * (Math.Pow(k, 2.0) - Math.Pow(array[2], 2.0)) >= 0.0)
                {
                    hashSet.Add(new Ex
                    {
                        o = (int) Math.Floor(
                            Math.Sqrt(-Math.Pow(array[2], 2.0) * Math.Pow(l, 2.0) - Math.Pow(array[1], 2.0) *
                                (Math.Pow(k, 2.0) - Math.Pow(array[2], 2.0))) *
                            Math.Abs(array[0] / (array[2] * array[1])) + array[3]),
                        l = (int) Math.Floor(l + array[4]),
                        r = (int) Math.Floor(k + array[5])
                    });
                    hashSet.Add(new Ex
                    {
                        o = (int) Math.Floor(
                            -Math.Sqrt(-Math.Pow(array[2], 2.0) * Math.Pow(l, 2.0) - Math.Pow(array[1], 2.0) *
                                (Math.Pow(k, 2.0) - Math.Pow(array[2], 2.0))) *
                            Math.Abs(array[0] / (array[2] * array[1])) + array[3]),
                        l = (int) Math.Floor(l + array[4]),
                        r = (int) Math.Floor(k + array[5])
                    });
                }

            for (var m = -(int) array[2]; m <= (int) array[2]; m++)
            for (var n = -(int) array[0]; n <= (int) array[0]; n++)
                if (-Math.Pow(array[2], 2.0) * Math.Pow(n, 2.0) -
                    Math.Pow(array[0], 2.0) * (Math.Pow(m, 2.0) - Math.Pow(array[2], 2.0)) >= 0.0)
                {
                    hashSet.Add(new Ex
                    {
                        o = (int) Math.Floor(n + array[3]),
                        l = (int) Math.Floor(
                            Math.Sqrt(-Math.Pow(array[2], 2.0) * Math.Pow(n, 2.0) - Math.Pow(array[0], 2.0) *
                                (Math.Pow(m, 2.0) - Math.Pow(array[2], 2.0))) *
                            Math.Abs(array[1] / (array[2] * array[0])) + array[4]),
                        r = (int) Math.Floor(m + array[5])
                    });
                    hashSet.Add(new Ex
                    {
                        o = (int) Math.Floor(n + array[3]),
                        l = (int) Math.Floor(
                            -Math.Sqrt(-Math.Pow(array[2], 2.0) * Math.Pow(n, 2.0) - Math.Pow(array[0], 2.0) *
                                (Math.Pow(m, 2.0) - Math.Pow(array[2], 2.0))) *
                            Math.Abs(array[1] / (array[2] * array[0])) + array[4]),
                        r = (int) Math.Floor(m + array[5])
                    });
                }

            if (p.group.maxBlocks < hashSet.Count)
            {
                Messages.TooManyBlocks(p, hashSet.Count);
                return;
            }

            foreach (var ex in hashSet) p.BlockChanges.Add((ushort) ex.o, (ushort) ex.l, (ushort) ex.r, ńń);
            p.BlockChanges.Commit();
        }

        // Token: 0x060009AA RID: 2474 RVA: 0x00030EFC File Offset: 0x0002F0FC
        public void PillarEraser(Player p, byte b, int o)
        {
            ushort num;
            ushort num2;
            ushort num3;
            p.level.IntToPos(o, out num, out num2, out num3);
            if (łą(b)) return;
            if (Block.OPBlocks(b))
            {
                Player.SendMessage(p, "You are trying to remove OP block");
                return;
            }

            var num4 = 0;
            for (;;)
            {
                var tile = p.level.GetTile(num, (ushort) (num2 + 1 + num4), num3);
                if (b != tile || Ąó(p, num, (ushort) (num2 + 1 + num4), num3)) break;
                óń.Add(p.level.PosToInt(num, (ushort) (num2 + 1 + num4), num3));
                num4++;
            }

            num4 = 0;
            for (;;)
            {
                var tile2 = p.level.GetTile(num, (ushort) (num2 - 1 - num4), num3);
                if (b != tile2 || Ąó(p, num, (ushort) (num2 - 1 - num4), num3)) break;
                óń.Add(p.level.PosToInt(num, (ushort) (num2 - 1 - num4), num3));
                num4++;
            }

            óń.ForEach(delegate(int ę)
            {
                ushort x;
                ushort y;
                ushort z;
                p.level.IntToPos(ę, out x, out y, out z);
                p.BlockChanges.Add(x, y, z, 0);
            });
            p.BlockChanges.Add(num, num2, num3, 0);
            p.BlockChanges.Commit();
            Player.SendMessage(p, "Pillar was removed.");
        }

        // Token: 0x060009AB RID: 2475 RVA: 0x00031068 File Offset: 0x0002F268
        private bool łą(byte b)
        {
            return b == 0 || b == 105;
        }

        // Token: 0x060009AC RID: 2476 RVA: 0x00031078 File Offset: 0x0002F278
        private bool Ąó(Player p, ushort ą, ushort ę, ushort ł)
        {
            var tile = p.level.GetTile(ą + 1, ę, ł);
            if (łą(tile)) return false;
            tile = p.level.GetTile(ą - 1, ę, ł);
            return !łą(tile) && !łą(p.level.GetTile(ą, ę, ł + 1)) && !łą(p.level.GetTile(ą, ę, ł - 1)) &&
                   !łą(p.level.GetTile(ą + 1, ę, ł + 1)) && !łą(p.level.GetTile(ą - 1, ę, ł - 1)) &&
                   !łą(p.level.GetTile(ą + 1, ę, ł - 1)) && !łą(p.level.GetTile(ą - 1, ę, ł + 1));
        }

        // Token: 0x060009AD RID: 2477 RVA: 0x00031178 File Offset: 0x0002F378
        public void ĄŻ(Player p)
        {
            foreach (var ż in ŻŻ)
            {
                var tile = p.level.GetTile(ż.ęę, ż.ź, ż.ć);
                p.SendBlockchange((ushort) ż.ęę, (ushort) ż.ź, (ushort) ż.ć, tile);
            }

            lV = false;
        }

        // Token: 0x060009AE RID: 2478 RVA: 0x00031208 File Offset: 0x0002F408
        public void ŻĄ(Player p)
        {
            ŻŻ.Clear();
            if (mć == ĆĆ.ł)
            {
                if (mą == ĄĄ.ę)
                {
                    for (var i = 0; i < (int) p.level.depth; i++)
                        ŻŻ.Add(new Ż
                        {
                            ęę = (int) oń,
                            ć = i,
                            ź = (int) aę
                        });
                }
                else if (mą == ĄĄ.ć)
                {
                    for (var j = 0; j < (int) p.level.width; j++)
                    {
                        var ęę = j;
                        var ć = (int) oń;
                        var ź = (int) aę;
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
                    for (var k = 0; k < (int) p.level.width; k++)
                    {
                        var ęę2 = k;
                        var ć2 = (int) (k * tł + oń);
                        var ź2 = (int) aę;
                        ŻŻ.Add(new Ż
                        {
                            ęę = ęę2,
                            ć = ć2,
                            ź = ź2
                        });
                    }

                    for (var l = 0; l < (int) p.level.depth; l++)
                    {
                        var ęę3 = (int) ((l - oń) / tł);
                        var ć3 = l;
                        var ź3 = (int) aę;
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
                for (var m = 0; m < (int) p.level.depth; m++)
                    ŻŻ.Add(new Ż
                    {
                        ęę = (int) gł,
                        ć = m,
                        ź = ńó[0].ź
                    });
                for (var n = 0; n < (int) p.level.width; n++)
                    ŻŻ.Add(new Ż
                    {
                        ęę = n,
                        ć = (int) eł,
                        ź = ńó[0].ź
                    });
            }
            else if (mź == ŹŹ.ć)
            {
                for (var num = 0; num < (int) p.level.depth; num++)
                    ŻŻ.Add(new Ż
                    {
                        ęę = (int) eł,
                        ć = num,
                        ź = ńó[0].ź
                    });
                for (var num2 = 0; num2 < (int) p.level.width; num2++)
                    ŻŻ.Add(new Ż
                    {
                        ęę = num2,
                        ć = (int) gł,
                        ź = ńó[0].ź
                    });
            }
            else
            {
                for (var num3 = 0; num3 < (int) p.level.width; num3++)
                {
                    var ęę4 = num3;
                    var ć4 = (int) (num3 * ał + gł);
                    var ź4 = ńó[0].ź;
                    ŻŻ.Add(new Ż
                    {
                        ęę = ęę4,
                        ć = ć4,
                        ź = ź4
                    });
                }

                for (var num4 = 0; num4 < (int) p.level.depth; num4++)
                {
                    var ęę5 = (int) ((num4 - gł) / ał);
                    var ć5 = num4;
                    var ź5 = ńó[0].ź;
                    ŻŻ.Add(new Ż
                    {
                        ęę = ęę5,
                        ć = ć5,
                        ź = ź5
                    });
                }

                for (var num5 = 0; num5 < (int) p.level.width; num5++)
                {
                    var ęę6 = num5;
                    var ć6 = (int) (num5 * hł + eł);
                    var ź6 = ńó[0].ź;
                    ŻŻ.Add(new Ż
                    {
                        ęę = ęę6,
                        ć = ć6,
                        ź = ź6
                    });
                }

                for (var num6 = 0; num6 < (int) p.level.depth; num6++)
                {
                    var ęę7 = (int) ((num6 - eł) / hł);
                    var ć7 = num6;
                    var ź7 = ńó[0].ź;
                    ŻŻ.Add(new Ż
                    {
                        ęę = ęę7,
                        ć = ć7,
                        ź = ź7
                    });
                }
            }

            foreach (var ż in ŻŻ) p.SendBlockchange((ushort) ż.ęę, (ushort) ż.ź, (ushort) ż.ć, 14);
            lV = true;
        }

        // Token: 0x060009AF RID: 2479 RVA: 0x00031804 File Offset: 0x0002FA04
        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type, byte ń)
        {
            if (lV) p.core.ĄŻ(p);
            p.ClearBlockchange2();
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            var cp = (CP) p.blockchangeObject;
            cp.x = x;
            cp.y = y;
            cp.z = z;
            p.blockchangeObject = cp;
            p.Blockchange2 += Blockchange2;
        }

        // Token: 0x060009B0 RID: 2480 RVA: 0x0003188C File Offset: 0x0002FA8C
        public void Blockchange2(Player p, ushort x, ushort y, ushort z, byte type, byte ń)
        {
            p.ClearBlockchange2();
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            var cp = (CP) p.blockchangeObject;
            mą = ĄĄ.ą;
            mć = ĆĆ.ł;
            if (cp.x - x == 0)
            {
                mą = ĄĄ.ę;
                oń = x;
            }
            else
            {
                oń = (z * cp.x - cp.z * x) / (cp.x - x);
                tł = (cp.z - oń) / cp.x;
                if (tł == 0m) mą = ĄĄ.ć;
            }

            aę = cp.y;
            p.core.ŻĄ(p);
            p.Blockchange2 += Blockchange3;
        }

        // Token: 0x060009B1 RID: 2481 RVA: 0x00031998 File Offset: 0x0002FB98
        public void Blockchange3(Player p, ushort x, ushort y, ushort z, byte type, byte ń)
        {
            p.ClearBlockchange2();
            if (ń == 0) type = 0;
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
            if (ló >= 0m && pś >= 0m && wą >= 0m) p.ManualChangeCheck((ushort) ló, (ushort) pś, (ushort) wą, ń, type);
            p.Blockchange2 += Blockchange3;
        }

        // Token: 0x060009B2 RID: 2482 RVA: 0x00031D48 File Offset: 0x0002FF48
        public void Blockchange4(Player p, ushort x, ushort y, ushort z, byte type, byte ń)
        {
            if (lV) p.core.ĄŻ(p);
            p.ClearBlockchange2();
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            if (eqś < 2)
            {
                ńó[eqś] = new Ż();
                ńó[eqś].ęę = x;
                ńó[eqś].ź = y;
                ńó[eqś].ć = z;
                eqś++;
                p.Blockchange2 += Blockchange4;
                return;
            }

            ńó[eqś] = new Ż();
            ńó[eqś].ęę = x;
            ńó[eqś].ź = y;
            ńó[eqś].ć = z;
            eqś = 0;
            ÓW();
            p.core.ŻĄ(p);
            p.Blockchange2 += Blockchange5;
        }

        // Token: 0x060009B3 RID: 2483 RVA: 0x00031E78 File Offset: 0x00030078
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
                if (ał == 0m) mź = ŹŹ.ć;
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

        // Token: 0x060009B4 RID: 2484 RVA: 0x00032034 File Offset: 0x00030234
        public void Blockchange5(Player p, ushort x, ushort y, ushort z, byte type, byte ń)
        {
            p.ClearBlockchange2();
            if (ń == 0) type = 0;
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
            if (jł >= 0m && zę >= 0m && pę >= 0m) p.ManualChangeCheck((ushort) jł, (ushort) kę, (ushort) tę, ń, type);
            if (jł >= 0m && zę >= 0m && pę >= 0m) p.ManualChangeCheck((ushort) bł, (ushort) zę, (ushort) pę, ń, type);
            if (jł >= 0m && zę >= 0m && pę >= 0m) p.ManualChangeCheck((ushort) fł, (ushort) qę, (ushort) gę, ń, type);
            p.Blockchange2 += Blockchange5;
        }

        // Token: 0x060009B5 RID: 2485 RVA: 0x00032950 File Offset: 0x00030B50
        public void DrawTriangle(Player p, List<BlockPoints> blockPoints)
        {
            _ź = blockPoints[2].blockType;
            _ę = blockPoints[0].position;
            _ż = blockPoints[1].position;
            _ó = blockPoints[2].position;
            ć_[0] = new Vector3(Math.Min(Math.Min(_ę.X, _ż.X), _ó.X), Math.Min(Math.Min(_ę.Y, _ż.Y), _ó.Y),
                Math.Min(Math.Min(_ę.Z, _ż.Z), _ó.Z));
            ć_[1] = new Vector3(Math.Max(Math.Max(_ę.X, _ż.X), _ó.X), Math.Max(Math.Max(_ę.Y, _ż.Y), _ó.Y),
                Math.Max(Math.Max(_ę.Z, _ż.Z), _ó.Z));
            _ć = ć_[0];
            _ą = (_ż - _ę).Cross(_ó - _ę);
            ą_ = _ą.Normalize();
            _ń = _ą.Cross(_ę - _ż).Normalize();
            ń_ = _ą.Cross(_ż - _ó).Normalize();
            ó_ = _ą.Cross(_ó - _ę).Normalize();
            var num = óń_(p);
            if (num > p.group.maxBlocks)
            {
                Player.SendMessage(p,
                    string.Concat("You tried to place ", num, " but you can only build ", p.group.maxBlocks,
                        " blocks."));
                p.BlockChanges.Abort();
                return;
            }

            p.BlockChanges.Commit();
            Player.SendMessage(p, "You drew a triangle that consists of " + num + " blocks.");
        }

        // Token: 0x060009B6 RID: 2486 RVA: 0x00032C40 File Offset: 0x00030E40
        public void DrawQuad(Player p, List<BlockPoints> ąęć_)
        {
            _ź = ąęć_[3].blockType;
            _ę = ąęć_[0].position;
            _ż = ąęć_[1].position;
            _ó = ąęć_[2].position;
            ć_[0] = new Vector3(Math.Min(Math.Min(_ę.X, _ż.X), _ó.X), Math.Min(Math.Min(_ę.Y, _ż.Y), _ó.Y),
                Math.Min(Math.Min(_ę.Z, _ż.Z), _ó.Z));
            ć_[1] = new Vector3(Math.Max(Math.Max(_ę.X, _ż.X), _ó.X), Math.Max(Math.Max(_ę.Y, _ż.Y), _ó.Y),
                Math.Max(Math.Max(_ę.Z, _ż.Z), _ó.Z));
            _ć = ć_[0];
            _ą = (_ż - _ę).Cross(_ó - _ę);
            ą_ = _ą.Normalize();
            _ń = _ą.Cross(_ę - _ż).Normalize();
            ń_ = _ą.Cross(_ż - _ó).Normalize();
            ó_ = _ą.Cross(_ó - _ę).Normalize();
            var num = óń_(p);
            _ę = ąęć_[2].position;
            _ż = ąęć_[3].position;
            _ó = ąęć_[0].position;
            ć_[0] = new Vector3(Math.Min(Math.Min(_ę.X, _ż.X), _ó.X), Math.Min(Math.Min(_ę.Y, _ż.Y), _ó.Y),
                Math.Min(Math.Min(_ę.Z, _ż.Z), _ó.Z));
            ć_[1] = new Vector3(Math.Max(Math.Max(_ę.X, _ż.X), _ó.X), Math.Max(Math.Max(_ę.Y, _ż.Y), _ó.Y),
                Math.Max(Math.Max(_ę.Z, _ż.Z), _ó.Z));
            _ć = ć_[0];
            _ą = (_ż - _ę).Cross(_ó - _ę);
            ą_ = _ą.Normalize();
            _ń = _ą.Cross(_ę - _ż).Normalize();
            ń_ = _ą.Cross(_ż - _ó).Normalize();
            ó_ = _ą.Cross(_ó - _ę).Normalize();
            num += óń_(p);
            if (num > p.group.maxBlocks)
            {
                Player.SendMessage(p,
                    string.Concat("You tried to place ", num, " but you can only build ", p.group.maxBlocks,
                        " blocks."));
                p.BlockChanges.Abort();
                return;
            }

            p.BlockChanges.Commit();
            Player.SendMessage(p, "You drew a quad that consists of " + num + " blocks.");
        }

        // Token: 0x060009B7 RID: 2487 RVA: 0x00033178 File Offset: 0x00031378
        private int óń_(Player p)
        {
            var num = 0;
            while (_ć.X <= ć_[1].X)
            {
                while (_ć.Y <= ć_[1].Y)
                {
                    while (_ć.Z <= ć_[1].Z)
                    {
                        if (źć_())
                        {
                            p.BlockChanges.Add((ushort) _ć.X, (ushort) _ć.Z, (ushort) _ć.Y, _ź);
                            num++;
                        }

                        _ć.Z = _ć.Z + 1f;
                    }

                    _ć.Z = ć_[0].Z;
                    _ć.Y = _ć.Y + 1f;
                }

                _ć.Y = ć_[0].Y;
                _ć.X = _ć.X + 1f;
            }

            return num;
        }

        // Token: 0x060009B8 RID: 2488 RVA: 0x000332A8 File Offset: 0x000314A8
        private bool źć_()
        {
            return Math.Abs(ą_.Dot(_ć - _ę)) <= 1f && (_ć - _ę).Dot(_ń) <= _ćź && (_ć - _ż).Dot(ń_) <= _ćź &&
                   (_ć - _ó).Dot(ó_) <= _ćź && (_źć(1, 0, 0) || _źć(0, 1, 0) || _źć(0, 0, 1));
        }

        // Token: 0x060009B9 RID: 2489 RVA: 0x00033378 File Offset: 0x00031578
        private bool _źć(int x, int y, int z)
        {
            var vec = new Vector3(x, y, z);
            var num = (int) _ą.Dot(_ę - _ć);
            var num2 = (int) _ą.Dot(vec);
            if (num2 == 0) return num == 0;
            var num3 = num / (double) num2;
            return num3 > -(double) _ćź && num3 <= _ćź;
        }

        // Token: 0x060009BA RID: 2490 RVA: 0x000333E8 File Offset: 0x000315E8
        public static int PrepareCone(Player s, int ś, int ń, int ó, int ę, int śś, byte ź)
        {
            ś--;
            double num = ś * ś;
            double num2 = ń * ń;
            for (var i = ó - ś; i <= ó + ś; i++)
            for (var j = śś - ś; j <= śś + ś; j++)
            {
                var num3 = (int) Math.Round(Math.Sqrt(Math.Pow(i - ó, 2.0) + Math.Pow(j - śś, 2.0)) * (ń / (double) ś));
                if (ń - num3 - 1 >= 0)
                {
                    var num4 = ń - num3 - 1;
                    s.BlockChanges.Add(i, num4 + ę, j, ź);
                }
            }

            for (var k = ó - ś; k <= ó + ś; k++)
            for (var l = ę; l < ę + ń; l++)
            {
                var num5 = (int) Math.Round(Math.Sqrt(num * Math.Pow(l - ę, 2.0) - num2 * Math.Pow(k - ó, 2.0)) /
                                            Math.Abs(ń));
                if (num5 >= 0)
                {
                    var y = ń - l + 2 * ę - 1;
                    s.BlockChanges.Add(k, y, -num5 + śś, ź);
                    s.BlockChanges.Add(k, y, num5 + śś, ź);
                }
            }

            for (var m = śś - ś; m <= śś + ś; m++)
            for (var n = ę; n < ę + ń; n++)
            {
                var num6 = (int) Math.Round(Math.Sqrt(num * Math.Pow(n - ę, 2.0) - num2 * Math.Pow(m - śś, 2.0)) /
                                            Math.Abs(ń));
                if (num6 >= 0)
                {
                    var y2 = ń - n + 2 * ę - 1;
                    s.BlockChanges.Add(-num6 + ó, y2, m, ź);
                    s.BlockChanges.Add(num6 + ó, y2, m, ź);
                }
            }

            return ś;
        }

        // Token: 0x02000147 RID: 327
        public struct IT
        {
            // Token: 0x04000434 RID: 1076
            public ushort x;

            // Token: 0x04000435 RID: 1077
            public ushort y;

            // Token: 0x04000436 RID: 1078
            public ushort z;

            // Token: 0x04000437 RID: 1079
            public byte o;

            // Token: 0x04000438 RID: 1080
            public Ę ą;
        }

        // Token: 0x02000149 RID: 329
        private class mm
        {
            // Token: 0x04000440 RID: 1088
            public int ą;

            // Token: 0x04000442 RID: 1090
            public int ć;

            // Token: 0x04000441 RID: 1089
            public int ę;
        }

        // Token: 0x0200014A RID: 330
        private class Ex
        {
            // Token: 0x04000444 RID: 1092
            public int l;

            // Token: 0x04000443 RID: 1091
            public int o;

            // Token: 0x04000445 RID: 1093
            public int r;
        }

        // Token: 0x0200014B RID: 331
        private enum ĄĄ
        {
            // Token: 0x04000447 RID: 1095
            ą,

            // Token: 0x04000448 RID: 1096
            ę,

            // Token: 0x04000449 RID: 1097
            ć
        }

        // Token: 0x0200014E RID: 334
        private enum ŹŹ
        {
            // Token: 0x04000451 RID: 1105
            ł,

            // Token: 0x04000452 RID: 1106
            ę,

            // Token: 0x04000453 RID: 1107
            ć
        }

        // Token: 0x0200014F RID: 335
        private class Ż
        {
            // Token: 0x04000456 RID: 1110
            public int ć;

            // Token: 0x04000454 RID: 1108
            public int ęę;

            // Token: 0x04000455 RID: 1109
            public int ź;
        }

        // Token: 0x02000150 RID: 336
        public struct CP
        {
            // Token: 0x04000457 RID: 1111
            public ushort x;

            // Token: 0x04000458 RID: 1112
            public ushort y;

            // Token: 0x04000459 RID: 1113
            public ushort z;

            // Token: 0x0400045A RID: 1114
            public byte type;
        }
    }
}