using System;
using System.Collections.Generic;
using System.Threading;

namespace MCDzienny
{
    // Token: 0x02000158 RID: 344
    public class BlockCatch
    {
        // Token: 0x0200015B RID: 347
        // (Invoke) Token: 0x060009F7 RID: 2551
        public delegate void MultiplePointsDraw<T>(Player p, List<ChangeInfo> changeInfoList, T drawArgs)
            where T : DrawArgs;

        // Token: 0x0200015A RID: 346
        // (Invoke) Token: 0x060009F3 RID: 2547
        public delegate void OnePointDraw<T>(Player p, ChangeInfo changeInfo, T drawArgs);

        // Token: 0x02000159 RID: 345
        // (Invoke) Token: 0x060009EF RID: 2543
        public delegate void TwoPointsDraw<T>(Player p, ChangeInfo changeInfo1, ChangeInfo changeInfo2, T drawArgs);

        // Token: 0x060009E9 RID: 2537 RVA: 0x00034B64 File Offset: 0x00032D64
        public static void CaptureOneBlock<T>(Player p, OnePointDraw<T> onePointDraw, T drawArgs)
        {
            Player.SendMessage(p, "Place the block.");
            var ewh = new EventWaitHandle(false, EventResetMode.ManualReset);
            Player.BlockchangeEventHandler2 value = delegate(Player pl, ushort x, ushort y, ushort z, byte t, byte a)
            {
                p.SendCurrentMapTile(x, y, z);
                var changeInfo = new ChangeInfo(x, y, z, t, a);
                onePointDraw(p, changeInfo, drawArgs);
                ewh.Set();
            };
            p.Blockchange2 += value;
            ewh.WaitOne();
            p.Blockchange2 -= value;
        }

        // Token: 0x060009EA RID: 2538 RVA: 0x00034BDC File Offset: 0x00032DDC
        public static void CaptureTwoBlocks<T>(Player p, TwoPointsDraw<T> twoPointsDraw, T drawArgs) where T : DrawArgs
        {
            Player.SendMessage(p, "Place the first block.");
            p.Blockchange2 += delegate(Player pl, ushort x, ushort y, ushort z, byte t, byte a)
            {
                p.ClearBlockchange2();
                var ci1 = new ChangeInfo(x, y, z, t, a);
                p.SendCurrentMapTile(x, y, z);
                Player.SendMessage(p, "Place the second block.");
                ChangeInfo ci2;
                p.Blockchange2 += delegate(Player pp, ushort xx, ushort yy, ushort zz, byte tt, byte aa)
                {
                    p.ClearBlockchange2();
                    ci2 = new ChangeInfo(xx, yy, zz, tt, aa);
                    p.SendCurrentMapTile(xx, yy, zz);
                    twoPointsDraw(p, ci1, ci2, drawArgs);
                };
            };
        }

        // Token: 0x060009EB RID: 2539 RVA: 0x00034C2C File Offset: 0x00032E2C
        public static void CaptureMultipleBlocks<T>(Player p, int pointsCount, MultiplePointsDraw<T> multiPointsDraw,
            T drawArgs) where T : DrawArgs
        {
            if (pointsCount <= 0) throw new ArgumentException("Value has to be greater than 0.", "pointsCount");
            var changeInfos = new List<ChangeInfo>();
            var points = 0;
            Player.BlockchangeEventHandler2 value = delegate(Player pl, ushort x, ushort y, ushort z, byte t, byte a)
            {
                pl.SendCurrentMapTile(x, y, z);
                changeInfos.Add(new ChangeInfo(x, y, z, t, a));
                points++;
                if (points >= pointsCount)
                {
                    pl.ClearBlockchange2();
                    if (changeInfos.Count == pointsCount) multiPointsDraw(p, changeInfos, drawArgs);
                    return;
                }

                Player.SendMessage(p, "Place the " + GetOrdinalNumber(points + 1) + " block.");
            };
            Player.SendMessage(p, "Place the first block.");
            p.Blockchange2 += value;
        }

        // Token: 0x060009EC RID: 2540 RVA: 0x00034CB0 File Offset: 0x00032EB0
        private static string GetOrdinalNumber(int number)
        {
            if (number < 0 || number > 20)
                throw new ArgumentException("The argument is out of the allowed range.", "number");
            if (number == 0) return "zeroth";
            if (number == 1) return "first";
            if (number == 2) return "second";
            if (number == 3) return "third";
            if (number == 4) return "forth";
            if (number == 5) return "fifth";
            return number + "th";
        }
    }
}