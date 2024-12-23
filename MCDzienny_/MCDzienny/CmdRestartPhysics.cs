using System;
using System.Collections.Generic;

namespace MCDzienny
{
    public class CmdRestartPhysics : Command
    {

        public override string name { get { return "restartphysics"; } }

        public override string shortcut { get { return "rp"; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override void Use(Player p, string message)
        {
            CatchPos catchPos = default(CatchPos);
            catchPos.x = 0;
            catchPos.y = 0;
            catchPos.z = 0;
            message = message.ToLower();
            catchPos.extraInfo = "";
            if (message != "")
            {
                int num = 0;
                bool flag = false;
                while (true)
                {
                    string[] array = message.Split(' ');
                    string[] array2;
                    foreach (string text in array)
                    {
                        if (num % 2 == 0)
                        {
                            switch (text)
                            {
                                case "revert":
                                    if (flag)
                                    {
                                        break;
                                    }
                                    array2 = message.Split(' ');
                                    try
                                    {
                                        array2[num + 1] = Block.Byte(message.Split(' ')[num + 1].ToLower()).ToString();
                                        if (array2[num + 1] == "255")
                                        {
                                            throw new OverflowException();
                                        }
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
                        }
                        else
                        {
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

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/restartphysics ([type] [num]) ([type2] [num2]) (...) - Restarts every physics block in an area");
            Player.SendMessage(p, "[type] will set custom physics for selected blocks");
            Player.SendMessage(p, "Possible [types]: drop, explode, dissipate, finite, wait, rainbow, revert");
            Player.SendMessage(p, "/rp revert takes block names");
        }

        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            CatchPos catchPos = (CatchPos)p.blockchangeObject;
            catchPos.x = x;
            catchPos.y = y;
            catchPos.z = z;
            p.blockchangeObject = catchPos;
            p.Blockchange += Blockchange2;
        }

        public void Blockchange2(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            CatchPos catchPos = (CatchPos)p.blockchangeObject;
            var list = new List<CatchPos>();
            CatchPos item = default(CatchPos);
            for (ushort num = Math.Min(catchPos.x, x); num <= Math.Max(catchPos.x, x); num++)
            {
                for (ushort num2 = Math.Min(catchPos.y, y); num2 <= Math.Max(catchPos.y, y); num2++)
                {
                    for (ushort num3 = Math.Min(catchPos.z, z); num3 <= Math.Max(catchPos.z, z); num3++)
                    {
                        if (p.level.GetTile(num, num2, num3) != 0)
                        {
                            item.x = num;
                            item.y = num2;
                            item.z = num3;
                            item.extraInfo = catchPos.extraInfo;
                            list.Add(item);
                        }
                    }
                }
            }
            try
            {
                if (catchPos.extraInfo == "")
                {
                    if (list.Count > Server.rpNormLimit)
                    {
                        Player.SendMessage(p, string.Format("Cannot restart more than {0} blocks.", Server.rpNormLimit));
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
            foreach (CatchPos item2 in list)
            {
                p.level.AddCheck(p.level.PosToInt(item2.x, item2.y, item2.z), item2.extraInfo, overRide: true);
            }
            Player.SendMessage(p, string.Format("Activated {0} blocks.", list.Count));
            if (p.staticCommands)
            {
                p.Blockchange += Blockchange1;
            }
        }

        struct CatchPos
        {
            public ushort x;

            public ushort y;

            public ushort z;

            public string extraInfo;
        }
    }
}