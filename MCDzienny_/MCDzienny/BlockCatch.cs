using System;
using System.Collections.Generic;
using System.Threading;

namespace MCDzienny
{
    public class BlockCatch
    {

        public delegate void MultiplePointsDraw<T>(Player p, List<ChangeInfo> changeInfoList, T drawArgs) where T : DrawArgs;

        public delegate void OnePointDraw<T>(Player p, ChangeInfo changeInfo, T drawArgs);

        public delegate void TwoPointsDraw<T>(Player p, ChangeInfo changeInfo1, ChangeInfo changeInfo2, T drawArgs);

        public static void CaptureOneBlock<T>(Player p, OnePointDraw<T> onePointDraw, T drawArgs)
        {
            Player.SendMessage(p, "Place the block.");
            EventWaitHandle ewh = new EventWaitHandle(false, EventResetMode.ManualReset);
            Player.BlockchangeEventHandler2 value = delegate(Player pl, ushort x, ushort y, ushort z, byte t, byte a)
            {
                p.SendCurrentMapTile(x, y, z);
                ChangeInfo changeInfo = new ChangeInfo(x, y, z, t, a);
                onePointDraw(p, changeInfo, drawArgs);
                ewh.Set();
            };
            p.Blockchange2 += value;
            ewh.WaitOne();
            p.Blockchange2 -= value;
        }

        public static void CaptureTwoBlocks<T>(Player p, TwoPointsDraw<T> twoPointsDraw, T drawArgs) where T : DrawArgs
        {
            Player.SendMessage(p, "Place the first block.");
            p.Blockchange2 += delegate(Player pl, ushort x, ushort y, ushort z, byte t, byte a)
            {
                p.ClearBlockchange2();
                ChangeInfo ci1 = new ChangeInfo(x, y, z, t, a);
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

        public static void CaptureMultipleBlocks<T>(Player p, int pointsCount, MultiplePointsDraw<T> multiPointsDraw, T drawArgs) where T : DrawArgs
        {
            if (pointsCount <= 0)
            {
                throw new ArgumentException("Value has to be greater than 0.", "pointsCount");
            }
            var changeInfos = new List<ChangeInfo>();
            int points = 0;
            Player.BlockchangeEventHandler2 value = delegate(Player pl, ushort x, ushort y, ushort z, byte t, byte a)
            {
                pl.SendCurrentMapTile(x, y, z);
                changeInfos.Add(new ChangeInfo(x, y, z, t, a));
                points++;
                if (points >= pointsCount)
                {
                    pl.ClearBlockchange2();
                    if (changeInfos.Count == pointsCount)
                    {
                        multiPointsDraw(p, changeInfos, drawArgs);
                    }
                }
                else
                {
                    Player.SendMessage(p, "Place the " + GetOrdinalNumber(points + 1) + " block.");
                }
            };
            Player.SendMessage(p, "Place the first block.");
            p.Blockchange2 += value;
        }

        static string GetOrdinalNumber(int number)
        {
            switch (number)
            {
                default:
                    throw new ArgumentException("The argument is out of the allowed range.", "number");
                case 0:
                    return "zeroth";
                case 1:
                    return "first";
                case 2:
                    return "second";
                case 3:
                    return "third";
                case 4:
                    return "forth";
                case 5:
                    return "fifth";
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                    return number + "th";
            }
        }
    }
}