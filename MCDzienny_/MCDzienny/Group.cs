using System;
using System.Collections.Generic;
using System.IO;

namespace MCDzienny
{
    public class Group
    {
        public static List<Group> groupList = new List<Group>();

        public static Group standard;

        public int bigMaps;

        public string color;

        public CommandList commands;

        public string fileName;

        public int maxBlocks;

        public int mediumMaps;

        public string name;

        public LevelPermission Permission;

        public PlayerList playerList;

        public int promotionPrice;

        public int smallMaps;

        public string trueName;

        public Group()
        {
            Permission = LevelPermission.Null;
        }

        public Group(LevelPermission Perm, int maxB, string fullName, char newColor, string file, int pPrice)
        {
            Permission = Perm;
            maxBlocks = maxB;
            trueName = fullName;
            name = trueName.ToLower();
            color = "&" + newColor;
            fileName = file;
            promotionPrice = pPrice;
            if (name != "nobody")
            {
                playerList = PlayerList.Load(fileName, this);
            }
            else
            {
                playerList = new PlayerList();
            }
            if (Perm >= LevelPermission.Guest)
            {
                smallMaps = 5;
            }
        }

        public void fillCommands()
        {
            CommandList commandList = new CommandList();
            GrpCommands.AddCommands(out commandList, Permission);
            commands = commandList;
        }

        public bool CanExecute(Command cmd)
        {
            return commands.Contains(cmd);
        }

        public static void InitAll()
        {
            groupList = new List<Group>();
            if (File.Exists("properties/ranks.properties"))
            {
                string[] array = File.ReadAllLines("properties/ranks.properties");
                Group group = new Group();
                int num = 0;
                int num2 = 0;
                bool flag = false;
                string[] array2 = array;
                foreach (string text in array2)
                {
                    try
                    {
                        if (!(text != "") || text[0] == '#')
                        {
                            continue;
                        }
                        if (text.Split('=').Length != 2)
                        {
                            Server.s.Log("In ranks.properties, the line " + text + " is wrongly formatted");
                            continue;
                        }
                        string text2 = text.Split('=')[0].Trim();
                        string value = text.Split('=')[1].Trim();
                        if (group.name == "" && text2.ToLower() != "rankname")
                        {
                            Server.s.Log("Hitting an error at " + text + " of ranks.properties");
                            continue;
                        }
                        switch (text2.ToLower())
                        {
                            case "rankname":
                                num = 0;
                                num2 = 0;
                                group = new Group();
                                if (value.ToLower() == "developers" || value.ToLower() == "devs")
                                {
                                    Server.s.Log("You are not a developer. Stop pretending you are.");
                                }
                                else if (groupList.Find(grp => grp.name == value.ToLower()) == null)
                                {
                                    group.trueName = value;
                                }
                                else
                                {
                                    Server.s.Log("Cannot add the rank " + value + " twice");
                                }
                                break;
                            case "permission":
                            {
                                int foundPermission;
                                try
                                {
                                    foundPermission = int.Parse(value);
                                }
                                catch
                                {
                                    Server.s.Log("Invalid permission on " + text);
                                    break;
                                }
                                if (group.Permission != LevelPermission.Null)
                                {
                                    Server.s.Log("Setting permission again on " + text);
                                    num--;
                                }
                                bool flag2 = true;
                                if (groupList.Find(grp => grp.Permission == (LevelPermission)foundPermission) != null)
                                {
                                    flag2 = false;
                                }
                                if (foundPermission > 119 || foundPermission < -50)
                                {
                                    Server.s.Log("Permission must be between -50 and 119 for ranks");
                                }
                                else if (flag2)
                                {
                                    num++;
                                    group.Permission = (LevelPermission)foundPermission;
                                }
                                else
                                {
                                    Server.s.Log("Cannot have 2 ranks set at permission level " + value);
                                }
                                break;
                            }
                            case "limit":
                            {
                                int num3;
                                try
                                {
                                    num3 = int.Parse(value);
                                }
                                catch
                                {
                                    Server.s.Log("Invalid limit on " + text);
                                    break;
                                }
                                num++;
                                group.maxBlocks = num3;
                                break;
                            }
                            case "color":
                            {
                                char c2;
                                try
                                {
                                    c2 = char.Parse(value);
                                }
                                catch
                                {
                                    Server.s.Log("Incorrect color on " + text);
                                    break;
                                }
                                if (c2 >= '0' && c2 <= '9' || c2 >= 'a' && c2 <= 'f')
                                {
                                    num++;
                                    group.color = c2.ToString();
                                }
                                else
                                {
                                    Server.s.Log("Invalid color code at " + text);
                                }
                                break;
                            }
                            case "promotionprice":
                                try
                                {
                                    num++;
                                    group.promotionPrice = int.Parse(value);
                                }
                                catch
                                {
                                    Server.s.Log("Invalid promotion price at " + text);
                                }
                                break;
                            case "smallmaps":
                                try
                                {
                                    group.smallMaps = int.Parse(value);
                                    flag = true;
                                    num2++;
                                }
                                catch
                                {
                                    Server.s.Log("Invalid small maps count at " + text);
                                }
                                break;
                            case "mediummaps":
                                try
                                {
                                    group.mediumMaps = int.Parse(value);
                                    num2++;
                                }
                                catch
                                {
                                    Server.s.Log("Invalid medium maps count at " + text);
                                }
                                break;
                            case "bigmaps":
                                try
                                {
                                    group.bigMaps = int.Parse(value);
                                    num2++;
                                }
                                catch
                                {
                                    Server.s.Log("Invalid big maps count at " + text);
                                }
                                break;
                            case "filename":
                                if (value.Contains("\\") || value.Contains("/"))
                                {
                                    Server.s.Log("Invalid filename on " + text);
                                    break;
                                }
                                num++;
                                group.fileName = value;
                                break;
                        }
                        if (num >= 5)
                        {
                            Group group2 = new Group(group.Permission, group.maxBlocks, group.trueName, group.color[0], group.fileName, group.promotionPrice);
                            if (num2 >= 3)
                            {
                                group2.smallMaps = flag ? group.smallMaps : 5;
                                group2.mediumMaps = group.mediumMaps;
                                group2.bigMaps = group.bigMaps;
                            }
                            else
                            {
                                group2.smallMaps = 5;
                            }
                            groupList.Add(group2);
                        }
                    }
                    catch (Exception ex)
                    {
                        Server.ErrorLog(ex);
                    }
                }
            }
            if (groupList.Find(grp => grp.Permission == LevelPermission.Banned) == null)
            {
                groupList.Add(new Group(LevelPermission.Banned, 1, "Banned", '8', "banned.txt", 0));
            }
            if (groupList.Find(grp => grp.Permission == LevelPermission.Guest) == null)
            {
                groupList.Add(new Group(LevelPermission.Guest, 1, "Guest", '7', "guest.txt", 0));
            }
            if (groupList.Find(grp => grp.Permission == LevelPermission.Builder) == null)
            {
                groupList.Add(new Group(LevelPermission.Builder, 400, "Builder", '2', "builders.txt", 330));
            }
            if (groupList.Find(grp => grp.Permission == LevelPermission.AdvBuilder) == null)
            {
                groupList.Add(new Group(LevelPermission.AdvBuilder, 1200, "AdvBuilder", '3', "advbuilders.txt", 540));
            }
            if (groupList.Find(grp => grp.Permission == LevelPermission.Operator) == null)
            {
                groupList.Add(new Group(LevelPermission.Operator, 2500, "Operator", 'c', "operators.txt", 0));
            }
            if (groupList.Find(grp => grp.Permission == LevelPermission.Admin) == null)
            {
                groupList.Add(new Group(LevelPermission.Admin, 65536, "SuperOP", 'e', "uberOps.txt", 0));
            }
            groupList.Add(new Group(LevelPermission.Nobody, 65536, "Nobody", '0', "nobody.txt", 0));
            bool flag3 = true;
            while (flag3)
            {
                flag3 = false;
                for (int j = 0; j < groupList.Count - 1; j++)
                {
                    if (groupList[j].Permission > groupList[j + 1].Permission)
                    {
                        flag3 = true;
                        Group value2 = groupList[j];
                        groupList[j] = groupList[j + 1];
                        groupList[j + 1] = value2;
                    }
                }
            }
            if (Find(Server.defaultRank) != null)
            {
                standard = Find(Server.defaultRank);
            }
            else
            {
                standard = findPerm(LevelPermission.Guest);
            }
            Player.players.ForEach(delegate(Player pl) { pl.group = groupList.Find(g => g.name == pl.group.name); });
            saveGroups(groupList);
        }

        public static void saveGroups(List<Group> givenList)
        {
            using (StreamWriter streamWriter = new StreamWriter(File.Create("properties/ranks.properties")))
            {
                streamWriter.WriteLine("#RankName = string");
                streamWriter.WriteLine("#     The name of the rank, use capitalization.");
                streamWriter.WriteLine("#");
                streamWriter.WriteLine("#Permission = num");
                streamWriter.WriteLine("#     The \"permission\" of the rank. It's a number.");
                streamWriter.WriteLine("#\t\tThere are pre-defined permissions already set. (for the old ranks)");
                streamWriter.WriteLine("#\t\tBanned = -20, Guest = 0, Builder = 30, AdvBuilder = 50, Operator = 80");
                streamWriter.WriteLine("#\t\tSuperOP = 100, Nobody = 120");
                streamWriter.WriteLine("#\t\tMust be greater than -50 and less than 120");
                streamWriter.WriteLine("#\t\tThe higher the number, the more commands do (such as undo allowing more seconds)");
                streamWriter.WriteLine("#Limit = num");
                streamWriter.WriteLine("#     The command limit for the rank (can be changed in-game with /limit)");
                streamWriter.WriteLine("#\t\tMust be greater than 0 and less than 10000000");
                streamWriter.WriteLine("#Color = char");
                streamWriter.WriteLine("#     A single letter or number denoting the color of the rank");
                streamWriter.WriteLine("#\t    Possibilities:");
                streamWriter.WriteLine("#\t\t    0, 1, 2, 3, 4, 5, 6, 7, 8, 9, a, b, c, d, e, f");
                streamWriter.WriteLine("#FileName = string.txt");
                streamWriter.WriteLine("#     The file which players of this rank will be stored in");
                streamWriter.WriteLine("#\t\tIt doesn't need to be a .txt file, but you may as well");
                streamWriter.WriteLine("#\t\tGenerally a good idea to just use the same file name as the rank name");
                streamWriter.WriteLine();
                streamWriter.WriteLine();
                foreach (Group given in givenList)
                {
                    if (given.name != "nobody")
                    {
                        streamWriter.WriteLine("RankName = " + given.trueName);
                        streamWriter.WriteLine("Permission = " + (int)given.Permission);
                        streamWriter.WriteLine("Limit = " + given.maxBlocks);
                        streamWriter.WriteLine("Color = " + given.color[1]);
                        streamWriter.WriteLine("PromotionPrice = " + given.promotionPrice);
                        streamWriter.WriteLine("SmallMaps = " + given.smallMaps);
                        streamWriter.WriteLine("MediumMaps = " + given.mediumMaps);
                        streamWriter.WriteLine("BigMaps = " + given.bigMaps);
                        streamWriter.WriteLine("FileName = " + given.fileName);
                        streamWriter.WriteLine();
                    }
                }
            }
        }

        public static bool Exists(string name)
        {
            name = name.ToLower();
            foreach (Group group in groupList)
            {
                if (group.name == name.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        public static Group Find(string name)
        {
            name = name.ToLower();
            foreach (Group group in groupList)
            {
                if (group.name == name.ToLower())
                {
                    return group;
                }
            }
            return null;
        }

        public static Group findPerm(LevelPermission Perm)
        {
            foreach (Group group in groupList)
            {
                if (group.Permission == Perm)
                {
                    return group;
                }
            }
            return null;
        }

        public static string findPlayer(string playerName)
        {
            foreach (Group group in groupList)
            {
                if (group.playerList.Contains(playerName))
                {
                    return group.name;
                }
            }
            return standard.name;
        }

        public static Group findPlayerGroup(string playerName)
        {
            foreach (Group group in groupList)
            {
                if (group.playerList.Contains(playerName))
                {
                    return group;
                }
            }
            return standard;
        }

        public static string concatList(bool includeColor = true, bool skipExtra = false, bool permissions = false)
        {
            string text = "";
            foreach (Group group in groupList)
            {
                if (!skipExtra || group.Permission > LevelPermission.Guest && group.Permission < LevelPermission.Nobody)
                {
                    if (includeColor)
                    {
                        string text2 = text;
                        text = text2 + ", " + group.color + group.name + Server.DefaultColor;
                    }
                    else if (permissions)
                    {
                        string text3 = text;
                        int permission = (int)group.Permission;
                        text = text3 + ", " + permission;
                    }
                    else
                    {
                        text = text + ", " + group.name;
                    }
                }
            }
            if (includeColor)
            {
                text = text.Remove(text.Length - 2);
            }
            return text.Remove(0, 2);
        }

        public static Group NextGroup(Group grp)
        {
            for (int i = 0; i < groupList.Count; i++)
            {
                if (groupList[i].Permission == grp.Permission && groupList.Count > i + 1)
                {
                    return groupList[i + 1];
                }
            }
            return null;
        }
    }
}