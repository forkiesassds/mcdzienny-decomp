using System;
using System.IO;

namespace MCDzienny
{
    class CmdNewLvl : Command
    {
        public override string name { get { return "newlvl"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            string[] array = message.Split(' ');
            if (array.Length == 5)
            {
                switch (array[4])
                {
                    default:
                        Player.SendMessage(p, "Valid types: island, mountains, forest, ocean, flat, pixel, desert");
                        break;
                    case "flat":
                    case "pixel":
                    case "island":
                    case "mountains":
                    case "ocean":
                    case "forest":
                    case "desert":
                    {
                        string text = array[0].ToLower();
                        if (text == "lava")
                        {
                            Player.SendMessage(p, "You can't name the map 'lava'. Choose any other name.");
                            break;
                        }
                        ushort num = 1;
                        ushort num2 = 1;
                        ushort num3 = 1;
                        try
                        {
                            num = Convert.ToUInt16(array[1]);
                            num2 = Convert.ToUInt16(array[2]);
                            num3 = Convert.ToUInt16(array[3]);
                        }
                        catch
                        {
                            Player.SendMessage(p, "Invalid dimensions.");
                            break;
                        }
                        if (!IsPowerOfTwo(num))
                        {
                            Player.SendMessage(p, string.Format("{0} is not a good dimension! Use a power of 2 next time.", num));
                        }
                        if (!IsPowerOfTwo(num2))
                        {
                            Player.SendMessage(p, string.Format("{0} is not a good dimension! Use a power of 2 next time.", num2));
                        }
                        if (!IsPowerOfTwo(num3))
                        {
                            Player.SendMessage(p, string.Format("{0} is not a good dimension! Use a power of 2 next time.", num3));
                        }
                        if (!Player.ValidName(text))
                        {
                            Player.SendMessage(p, "Invalid name!");
                            break;
                        }
                        if (File.Exists("levels/" + text + ".lvl"))
                        {
                            Player.SendMessage(p, "Level \"" + text + "\" already exists!");
                            break;
                        }
                        try
                        {
                            if (p != null)
                            {
                                if (p.group.Permission < LevelPermission.Admin && num * num2 * num3 > 30000000)
                                {
                                    Player.SendMessage(p, "Cannot create a map with over 30million blocks");
                                    break;
                                }
                            }
                            else if (num * num2 * num3 > 225000000)
                            {
                                Player.SendMessage(p, "You cannot make a map with over 225million blocks");
                                break;
                            }
                        }
                        catch
                        {
                            Player.SendMessage(p, "An error occured");
                        }
                        try
                        {
                            Level level = new Level(text, num, num2, num3, array[4]);
                            level.Save(Override: true);
                        }
                        finally
                        {
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                        }
                        Player.GlobalMessage(string.Format("Level {0} created", text));
                        break;
                    }
                }
            }
            else
            {
                Help(p);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/newlvl - creates a new level.");
            Player.SendMessage(p, "/newlvl mapname 128 64 128 type");
            Player.SendMessage(p, "Valid types: island, mountains, forest, ocean, flat, pixel, desert");
        }

        bool IsPowerOfTwo(int x)
        {
            if (x > 0)
            {
                return (x & x - 1) == 0;
            }
            return false;
        }

        public bool IsDimensionGood(ushort value)
        {
            switch (value)
            {
                case 2:
                case 4:
                case 8:
                case 16:
                case 32:
                case 64:
                case 128:
                case 256:
                case 512:
                case 1024:
                case 2048:
                case 4096:
                case 8192:
                    return true;
                default:
                    return false;
            }
        }
    }
}