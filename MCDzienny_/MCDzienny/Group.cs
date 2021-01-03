using System;
using System.Collections.Generic;
using System.IO;

namespace MCDzienny
{
    // Token: 0x02000347 RID: 839
    public class Group
    {
        // Token: 0x04000C64 RID: 3172
        public static List<Group> groupList = new List<Group>();

        // Token: 0x04000C65 RID: 3173
        public static Group standard;

        // Token: 0x04000C6C RID: 3180
        public int bigMaps;

        // Token: 0x04000C68 RID: 3176
        public string color;

        // Token: 0x04000C6F RID: 3183
        public CommandList commands;

        // Token: 0x04000C70 RID: 3184
        public string fileName;

        // Token: 0x04000C6E RID: 3182
        public int maxBlocks;

        // Token: 0x04000C6B RID: 3179
        public int mediumMaps;

        // Token: 0x04000C66 RID: 3174
        public string name;

        // Token: 0x04000C6D RID: 3181
        public LevelPermission Permission;

        // Token: 0x04000C71 RID: 3185
        public PlayerList playerList;

        // Token: 0x04000C69 RID: 3177
        public int promotionPrice;

        // Token: 0x04000C6A RID: 3178
        public int smallMaps;

        // Token: 0x04000C67 RID: 3175
        public string trueName;

        // Token: 0x06001813 RID: 6163 RVA: 0x000A1AA4 File Offset: 0x0009FCA4
        public Group()
        {
            Permission = LevelPermission.Null;
        }

        // Token: 0x06001814 RID: 6164 RVA: 0x000A1AB8 File Offset: 0x0009FCB8
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
                playerList = PlayerList.Load(fileName, this);
            else
                playerList = new PlayerList();
            if (Perm >= LevelPermission.Guest) smallMaps = 5;
        }

        // Token: 0x06001815 RID: 6165 RVA: 0x000A1B54 File Offset: 0x0009FD54
        public void fillCommands()
        {
            var commandList = new CommandList();
            GrpCommands.AddCommands(out commandList, Permission);
            commands = commandList;
        }

        // Token: 0x06001816 RID: 6166 RVA: 0x000A1B7C File Offset: 0x0009FD7C
        public bool CanExecute(Command cmd)
        {
            return commands.Contains(cmd);
        }

        // Token: 0x06001817 RID: 6167 RVA: 0x000A1B8C File Offset: 0x0009FD8C
        public static void InitAll()
        {
            groupList = new List<Group>();
            if (File.Exists("properties/ranks.properties"))
            {
                var array = File.ReadAllLines("properties/ranks.properties");
                var group = new Group();
                var num = 0;
                var num2 = 0;
                var flag = false;
                var array2 = array;
                string value;
                int foundPermission;
                foreach (var text in array2)
                    try
                    {
                        if (!(text != "") || text[0] == '#') continue;
                        if (text.Split('=').Length != 2)
                        {
                            Server.s.Log("In ranks.properties, the line " + text + " is wrongly formatted");
                            continue;
                        }

                        var text2 = text.Split('=')[0].Trim();
                        value = text.Split('=')[1].Trim();
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
                                    Server.s.Log("You are not a developer. Stop pretending you are.");
                                else if (groupList.Find(grp => grp.name == value.ToLower()) == null)
                                    group.trueName = value;
                                else
                                    Server.s.Log("Cannot add the rank " + value + " twice");
                                break;
                            case "permission":
                            {
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

                                var flag2 = true;
                                if (groupList.Find(grp => grp.Permission == (LevelPermission) foundPermission) != null)
                                    flag2 = false;
                                if (foundPermission > 119 || foundPermission < -50)
                                {
                                    Server.s.Log("Permission must be between -50 and 119 for ranks");
                                }
                                else if (flag2)
                                {
                                    num++;
                                    group.Permission = (LevelPermission) foundPermission;
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
                                char c;
                                try
                                {
                                    c = char.Parse(value);
                                }
                                catch
                                {
                                    Server.s.Log("Incorrect color on " + text);
                                    break;
                                }

                                if (c >= '0' && c <= '9' || c >= 'a' && c <= 'f')
                                {
                                    num++;
                                    group.color = c.ToString();
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
                            var group2 = new Group(group.Permission, group.maxBlocks, group.trueName, group.color[0],
                                group.fileName, group.promotionPrice);
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

            if (groupList.Find(grp => grp.Permission == LevelPermission.Banned) == null)
                groupList.Add(new Group(LevelPermission.Banned, 1, "Banned", '8', "banned.txt", 0));
            if (groupList.Find(grp => grp.Permission == LevelPermission.Guest) == null)
                groupList.Add(new Group(LevelPermission.Guest, 1, "Guest", '7', "guest.txt", 0));
            if (groupList.Find(grp => grp.Permission == LevelPermission.Builder) == null)
                groupList.Add(new Group(LevelPermission.Builder, 400, "Builder", '2', "builders.txt", 330));
            if (groupList.Find(grp => grp.Permission == LevelPermission.AdvBuilder) == null)
                groupList.Add(new Group(LevelPermission.AdvBuilder, 1200, "AdvBuilder", '3', "advbuilders.txt", 540));
            if (groupList.Find(grp => grp.Permission == LevelPermission.Operator) == null)
                groupList.Add(new Group(LevelPermission.Operator, 2500, "Operator", 'c', "operators.txt", 0));
            if (groupList.Find(grp => grp.Permission == LevelPermission.Admin) == null)
                groupList.Add(new Group(LevelPermission.Admin, 65536, "SuperOP", 'e', "uberOps.txt", 0));
            groupList.Add(new Group(LevelPermission.Nobody, 65536, "Nobody", '0', "nobody.txt", 0));
            var flag3 = true;
            while (flag3)
            {
                flag3 = false;
                for (var j = 0; j < groupList.Count - 1; j++)
                    if (groupList[j].Permission > groupList[j + 1].Permission)
                    {
                        flag3 = true;
                        var value2 = groupList[j];
                        groupList[j] = groupList[j + 1];
                        groupList[j + 1] = value2;
                    }
            }

            if (Find(Server.defaultRank) != null)
                standard = Find(Server.defaultRank);
            else
                standard = findPerm(LevelPermission.Guest);
            Player.players.ForEach(delegate(Player pl) { pl.group = groupList.Find(g => g.name == pl.group.name); });
            saveGroups(groupList);
        }

        // Token: 0x06001818 RID: 6168 RVA: 0x000A254C File Offset: 0x000A074C
        public static void saveGroups(List<Group> givenList)
        {
            using (var streamWriter = new StreamWriter(File.Create("properties/ranks.properties")))
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
                streamWriter.WriteLine(
                    "#\t\tThe higher the number, the more commands do (such as undo allowing more seconds)");
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
                foreach (var group in givenList)
                    if (group.name != "nobody")
                    {
                        streamWriter.WriteLine("RankName = " + group.trueName);
                        streamWriter.WriteLine("Permission = " + (int) group.Permission);
                        streamWriter.WriteLine("Limit = " + group.maxBlocks);
                        streamWriter.WriteLine("Color = " + group.color[1]);
                        streamWriter.WriteLine("PromotionPrice = " + group.promotionPrice);
                        streamWriter.WriteLine("SmallMaps = " + group.smallMaps);
                        streamWriter.WriteLine("MediumMaps = " + group.mediumMaps);
                        streamWriter.WriteLine("BigMaps = " + group.bigMaps);
                        streamWriter.WriteLine("FileName = " + group.fileName);
                        streamWriter.WriteLine();
                    }
            }
        }

        // Token: 0x06001819 RID: 6169 RVA: 0x000A27D8 File Offset: 0x000A09D8
        public static bool Exists(string name)
        {
            name = name.ToLower();
            foreach (var group in groupList)
                if (group.name == name.ToLower())
                    return true;
            return false;
        }

        // Token: 0x0600181A RID: 6170 RVA: 0x000A2848 File Offset: 0x000A0A48
        public static Group Find(string name)
        {
            name = name.ToLower();
            foreach (var group in groupList)
                if (group.name == name.ToLower())
                    return group;
            return null;
        }

        // Token: 0x0600181B RID: 6171 RVA: 0x000A28B8 File Offset: 0x000A0AB8
        public static Group findPerm(LevelPermission Perm)
        {
            foreach (var group in groupList)
                if (group.Permission == Perm)
                    return group;
            return null;
        }

        // Token: 0x0600181C RID: 6172 RVA: 0x000A2914 File Offset: 0x000A0B14
        public static string findPlayer(string playerName)
        {
            foreach (var group in groupList)
                if (group.playerList.Contains(playerName))
                    return group.name;
            return standard.name;
        }

        // Token: 0x0600181D RID: 6173 RVA: 0x000A2984 File Offset: 0x000A0B84
        public static Group findPlayerGroup(string playerName)
        {
            foreach (var group in groupList)
                if (group.playerList.Contains(playerName))
                    return group;
            return standard;
        }

        // Token: 0x0600181E RID: 6174 RVA: 0x000A29E8 File Offset: 0x000A0BE8
        public static string concatList(bool includeColor = true, bool skipExtra = false, bool permissions = false)
        {
            var text = "";
            foreach (var group in groupList)
                if (!skipExtra || group.Permission > LevelPermission.Guest && group.Permission < LevelPermission.Nobody)
                {
                    if (includeColor)
                    {
                        var text2 = text;
                        text = string.Concat(text2, ", ", group.color, group.name, Server.DefaultColor);
                    }
                    else if (permissions)
                    {
                        var str = text;
                        var str2 = ", ";
                        var permission = (int) group.Permission;
                        text = str + str2 + permission;
                    }
                    else
                    {
                        text = text + ", " + group.name;
                    }
                }

            if (includeColor) text = text.Remove(text.Length - 2);
            return text.Remove(0, 2);
        }

        // Token: 0x0600181F RID: 6175 RVA: 0x000A2AE4 File Offset: 0x000A0CE4
        public static Group NextGroup(Group grp)
        {
            for (var i = 0; i < groupList.Count; i++)
                if (groupList[i].Permission == grp.Permission && groupList.Count > i + 1)
                    return groupList[i + 1];
            return null;
        }
    }
}