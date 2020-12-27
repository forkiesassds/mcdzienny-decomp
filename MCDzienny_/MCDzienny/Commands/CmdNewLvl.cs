using System;
using System.IO;

namespace MCDzienny
{
    // Token: 0x0200027B RID: 635
    internal class CmdNewLvl : Command
    {
        // Token: 0x170006BB RID: 1723
        // (get) Token: 0x0600123B RID: 4667 RVA: 0x00064CD8 File Offset: 0x00062ED8
        public override string name
        {
            get { return "newlvl"; }
        }

        // Token: 0x170006BC RID: 1724
        // (get) Token: 0x0600123C RID: 4668 RVA: 0x00064CE0 File Offset: 0x00062EE0
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170006BD RID: 1725
        // (get) Token: 0x0600123D RID: 4669 RVA: 0x00064CE8 File Offset: 0x00062EE8
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x170006BE RID: 1726
        // (get) Token: 0x0600123E RID: 4670 RVA: 0x00064CF0 File Offset: 0x00062EF0
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170006BF RID: 1727
        // (get) Token: 0x0600123F RID: 4671 RVA: 0x00064CF4 File Offset: 0x00062EF4
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x06001240 RID: 4672 RVA: 0x00064CF8 File Offset: 0x00062EF8
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            var array = message.Split(' ');
            if (array.Length == 5)
            {
                string key;
                switch (key = array[4])
                {
                    case "flat":
                    case "pixel":
                    case "island":
                    case "mountains":
                    case "ocean":
                    case "forest":
                    case "desert":
                    {
                        var text = array[0].ToLower();
                        if (text == "lava")
                        {
                            Player.SendMessage(p, "You can't name the map 'lava'. Choose any other name.");
                            return;
                        }

                        ushort num2 = 1;
                        ushort num3 = 1;
                        ushort num4 = 1;
                        try
                        {
                            num2 = Convert.ToUInt16(array[1]);
                            num3 = Convert.ToUInt16(array[2]);
                            num4 = Convert.ToUInt16(array[3]);
                        }
                        catch
                        {
                            Player.SendMessage(p, "Invalid dimensions.");
                            return;
                        }

                        if (!IsPowerOfTwo(num2))
                            Player.SendMessage(p,
                                string.Format("{0} is not a good dimension! Use a power of 2 next time.", num2));
                        if (!IsPowerOfTwo(num3))
                            Player.SendMessage(p,
                                string.Format("{0} is not a good dimension! Use a power of 2 next time.", num3));
                        if (!IsPowerOfTwo(num4))
                            Player.SendMessage(p,
                                string.Format("{0} is not a good dimension! Use a power of 2 next time.", num4));
                        if (!Player.ValidName(text))
                        {
                            Player.SendMessage(p, "Invalid name!");
                        }
                        else
                        {
                            if (File.Exists("levels/" + text + ".lvl"))
                            {
                                Player.SendMessage(p, "Level \"" + text + "\" already exists!");
                                return;
                            }

                            try
                            {
                                if (p != null)
                                {
                                    if (p.group.Permission < LevelPermission.Admin && num2 * num3 * num4 > 30000000)
                                    {
                                        Player.SendMessage(p, "Cannot create a map with over 30million blocks");
                                        return;
                                    }
                                }
                                else if (num2 * num3 * num4 > 225000000)
                                {
                                    Player.SendMessage(p, "You cannot make a map with over 225million blocks");
                                    return;
                                }
                            }
                            catch
                            {
                                Player.SendMessage(p, "An error occured");
                            }

                            try
                            {
                                var level = new Level(text, num2, num3, num4, array[4]);
                                level.Save(true);
                            }
                            finally
                            {
                                GC.Collect();
                                GC.WaitForPendingFinalizers();
                            }

                            Player.GlobalMessage(string.Format("Level {0} created", text));
                            return;
                        }

                        return;
                    }
                }

                Player.SendMessage(p, "Valid types: island, mountains, forest, ocean, flat, pixel, desert");
                return;
            }

            Help(p);
        }

        // Token: 0x06001241 RID: 4673 RVA: 0x00064FA0 File Offset: 0x000631A0
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/newlvl - creates a new level.");
            Player.SendMessage(p, "/newlvl mapname 128 64 128 type");
            Player.SendMessage(p, "Valid types: island, mountains, forest, ocean, flat, pixel, desert");
        }

        // Token: 0x06001242 RID: 4674 RVA: 0x00064FC4 File Offset: 0x000631C4
        private bool IsPowerOfTwo(int x)
        {
            return x > 0 && (x & (x - 1)) == 0;
        }

        // Token: 0x06001243 RID: 4675 RVA: 0x00064FD4 File Offset: 0x000631D4
        public bool IsDimensionGood(ushort value)
        {
            if (value <= 128)
            {
                if (value <= 16)
                    switch (value)
                    {
                        case 2:
                        case 4:
                            break;
                        case 3:
                            return false;
                        default:
                            if (value != 8 && value != 16) return false;
                            break;
                    }
                else if (value != 32 && value != 64 && value != 128) return false;
            }
            else if (value <= 1024)
            {
                if (value != 256 && value != 512 && value != 1024) return false;
            }
            else if (value != 2048 && value != 4096 && value != 8192)
            {
                return false;
            }

            return true;
        }
    }
}