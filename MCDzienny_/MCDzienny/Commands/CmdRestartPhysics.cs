using System;
using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x020002C1 RID: 705
    public class CmdRestartPhysics : Command
    {
        // Token: 0x170007AB RID: 1963
        // (get) Token: 0x0600140B RID: 5131 RVA: 0x0006F284 File Offset: 0x0006D484
        public override string name
        {
            get { return "restartphysics"; }
        }

        // Token: 0x170007AC RID: 1964
        // (get) Token: 0x0600140C RID: 5132 RVA: 0x0006F28C File Offset: 0x0006D48C
        public override string shortcut
        {
            get { return "rp"; }
        }

        // Token: 0x170007AD RID: 1965
        // (get) Token: 0x0600140D RID: 5133 RVA: 0x0006F294 File Offset: 0x0006D494
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x170007AE RID: 1966
        // (get) Token: 0x0600140E RID: 5134 RVA: 0x0006F29C File Offset: 0x0006D49C
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x170007AF RID: 1967
        // (get) Token: 0x0600140F RID: 5135 RVA: 0x0006F2A0 File Offset: 0x0006D4A0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x06001411 RID: 5137 RVA: 0x0006F2AC File Offset: 0x0006D4AC
        public override void Use(Player p, string message)
        {
            var catchPos = default(CatchPos);
            catchPos.x = 0;
            catchPos.y = 0;
            catchPos.z = 0;
            message = message.ToLower();
            catchPos.extraInfo = "";
            if (message != "")
            {
                var num = 0;
                var flag = false;
                while (true)
                {
                    var array = message.Split(' ');
                    string[] array2;
                    foreach (var text in array)
                    {
                        if (num % 2 == 0)
                            switch (text)
                            {
                                case "revert":
                                    if (flag) break;
                                    array2 = message.Split(' ');
                                    try
                                    {
                                        array2[num + 1] = Block.Byte(message.Split(' ')[num + 1].ToLower()).ToString();
                                        if (array2[num + 1] == "255") throw new OverflowException();
                                    }
                                    catch
                                    {
                                        Player.SendMessage(p, "Invalid block type.");
                                        return;
                                    }

                                    goto IL_01a3;
                                default:
                                    Player.SendMessage(p, string.Format("{0} is not supported.", text));
                                    return;
                                case "drop":
                                case "explode":
                                case "dissipate":
                                case "finite":
                                case "wait":
                                case "rainbow":
                                    break;
                            }
                        else
                            try
                            {
                                if (int.Parse(text) < 1)
                                {
                                    Player.SendMessage(p, "Values must be above 0");
                                    return;
                                }
                            }
                            catch
                            {
                                Player.SendMessage(p, "/rp [text] [num] [text] [num]");
                                return;
                            }

                        num++;
                    }

                    break;
                    IL_01a3:
                    message = string.Join(" ", array2);
                    flag = true;
                    num = 0;
                }

                if (num % 2 == 1)
                {
                    Player.SendMessage(p, "Number of parameters must be even");
                    Help(p);
                    return;
                }

                catchPos.extraInfo = message;
            }

            p.blockchangeObject = catchPos;
            Player.SendMessage(p, "Place two blocks to determine the edges.");
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
        }

        // Token: 0x06001412 RID: 5138 RVA: 0x0006F530 File Offset: 0x0006D730
        public override void Help(Player p)
        {
            Player.SendMessage(p,
                "/restartphysics ([type] [num]) ([type2] [num2]) (...) - Restarts every physics block in an area");
            Player.SendMessage(p, "[type] will set custom physics for selected blocks");
            Player.SendMessage(p, "Possible [types]: drop, explode, dissipate, finite, wait, rainbow, revert");
            Player.SendMessage(p, "/rp revert takes block names");
        }

        // Token: 0x06001413 RID: 5139 RVA: 0x0006F560 File Offset: 0x0006D760
        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            var catchPos = (CatchPos) p.blockchangeObject;
            catchPos.x = x;
            catchPos.y = y;
            catchPos.z = z;
            p.blockchangeObject = catchPos;
            p.Blockchange += Blockchange2;
        }

        // Token: 0x06001414 RID: 5140 RVA: 0x0006F5D4 File Offset: 0x0006D7D4
        public void Blockchange2(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            var catchPos = (CatchPos) p.blockchangeObject;
            var list = new List<CatchPos>();
            var item = default(CatchPos);
            for (var num = Math.Min(catchPos.x, x); num <= Math.Max(catchPos.x, x); num += 1)
            for (var num2 = Math.Min(catchPos.y, y); num2 <= Math.Max(catchPos.y, y); num2 += 1)
            for (var num3 = Math.Min(catchPos.z, z); num3 <= Math.Max(catchPos.z, z); num3 += 1)
                if (p.level.GetTile(num, num2, num3) != 0)
                {
                    item.x = num;
                    item.y = num2;
                    item.z = num3;
                    item.extraInfo = catchPos.extraInfo;
                    list.Add(item);
                }

            try
            {
                if (catchPos.extraInfo == "")
                {
                    if (list.Count > Server.rpNormLimit)
                    {
                        Player.SendMessage(p,
                            string.Format("Cannot restart more than {0} blocks.", Server.rpNormLimit));
                        Player.SendMessage(p, string.Format("Tried to restart {0} blocks.", list.Count));
                        return;
                    }
                }
                else if (list.Count > Server.rpLimit)
                {
                    Player.SendMessage(p, string.Format("Tried to add physics to {0} blocks.", list.Count));
                    Player.SendMessage(p, string.Format("Cannot add physics to more than {0} blocks.", Server.rpLimit));
                    return;
                }
            }
            catch
            {
                return;
            }

            foreach (var catchPos2 in list)
                p.level.AddCheck(p.level.PosToInt(catchPos2.x, catchPos2.y, catchPos2.z), catchPos2.extraInfo, true);
            Player.SendMessage(p, string.Format("Activated {0} blocks.", list.Count));
            if (p.staticCommands) p.Blockchange += Blockchange1;
        }

        // Token: 0x020002C2 RID: 706
        private struct CatchPos
        {
            // Token: 0x04000993 RID: 2451
            public ushort x;

            // Token: 0x04000994 RID: 2452
            public ushort y;

            // Token: 0x04000995 RID: 2453
            public ushort z;

            // Token: 0x04000996 RID: 2454
            public string extraInfo;
        }
    }
}