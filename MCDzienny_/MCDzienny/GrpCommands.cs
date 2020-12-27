using System;
using System.Collections.Generic;
using System.IO;

namespace MCDzienny
{
    // Token: 0x020001D9 RID: 473
    public class GrpCommands
    {
        // Token: 0x040006DB RID: 1755
        public static List<rankAllowance> allowedCommands;

        // Token: 0x040006DC RID: 1756
        public static List<string> foundCommands = new List<string>();

        // Token: 0x06000D3C RID: 3388 RVA: 0x0004BAE0 File Offset: 0x00049CE0
        public static LevelPermission defaultRanks(string command)
        {
            var command2 = Command.all.Find(command);
            if (command2 != null) return command2.defaultRank;
            return LevelPermission.Null;
        }

        // Token: 0x06000D3D RID: 3389 RVA: 0x0004BB08 File Offset: 0x00049D08
        public static void fillRanks()
        {
            foundCommands = Command.all.commandNames();
            allowedCommands = new List<rankAllowance>();
            foreach (var command in Command.all.All())
            {
                var rankAllowance = new rankAllowance();
                rankAllowance.commandName = command.name;
                rankAllowance.lowestRank = command.defaultRank;
                allowedCommands.Add(rankAllowance);
            }

            if (File.Exists("properties/command.properties"))
            {
                var array = File.ReadAllLines("properties/command.properties");
                if (array[0] == "#Version 2")
                {
                    string[] separator =
                    {
                        " : "
                    };
                    foreach (var text in array)
                    {
                        var rankAllowance = new rankAllowance();
                        if (text != "" && text[0] != '#')
                        {
                            var array3 = text.Split(separator, StringSplitOptions.None);
                            if (!foundCommands.Contains(array3[0]))
                            {
                                Server.s.Log("Incorrect command name: " + array3[0]);
                            }
                            else
                            {
                                rankAllowance.commandName = array3[0];
                                var array4 = new string[0];
                                if (array3[2] != "") array4 = array3[2].Split(',');
                                var array5 = new string[0];
                                if (array3[3] != "") array5 = array3[3].Split(',');
                                try
                                {
                                    rankAllowance.lowestRank = (LevelPermission) int.Parse(array3[1]);
                                    foreach (var s in array4)
                                        rankAllowance.disallow.Add((LevelPermission) int.Parse(s));
                                    foreach (var s2 in array5) rankAllowance.allow.Add((LevelPermission) int.Parse(s2));
                                }
                                catch
                                {
                                    Server.s.Log("Hit an error on the command " + text);
                                    goto IL_27C;
                                }

                                var num = 0;
                                foreach (var rankAllowance2 in allowedCommands)
                                {
                                    if (array3[0] == rankAllowance2.commandName)
                                    {
                                        allowedCommands[num] = rankAllowance;
                                        break;
                                    }

                                    num++;
                                }
                            }
                        }

                        IL_27C: ;
                    }
                }
                else
                {
                    foreach (var text2 in array)
                        if (text2 != "" && text2[0] != '#')
                        {
                            var rankAllowance = new rankAllowance();
                            var text3 = text2.Split('=')[0].Trim().ToLower();
                            var name = text2.Split('=')[1].Trim().ToLower();
                            if (!foundCommands.Contains(text3))
                            {
                                Server.s.Log("Incorrect command name: " + text3);
                            }
                            else if (Level.PermissionFromName(name) == LevelPermission.Null)
                            {
                                Server.s.Log("Incorrect value given for " + text3 + ", using default value.");
                            }
                            else
                            {
                                rankAllowance.commandName = text3;
                                rankAllowance.lowestRank = Level.PermissionFromName(name);
                                var num2 = 0;
                                foreach (var rankAllowance3 in allowedCommands)
                                {
                                    if (text3 == rankAllowance3.commandName)
                                    {
                                        allowedCommands[num2] = rankAllowance;
                                        break;
                                    }

                                    num2++;
                                }
                            }
                        }
                }

                Save(allowedCommands);
            }
            else
            {
                Save(allowedCommands);
            }

            foreach (var group in Group.groupList) group.fillCommands();
        }

        // Token: 0x06000D3E RID: 3390 RVA: 0x0004BF8C File Offset: 0x0004A18C
        public static void Save(List<rankAllowance> givenList)
        {
            try
            {
                using (var streamWriter = new StreamWriter(File.Create("properties/command.properties")))
                {
                    streamWriter.WriteLine("#Version 2");
                    streamWriter.WriteLine(
                        "#   This file contains a reference to every command found in the server software");
                    streamWriter.WriteLine("#   Use this file to specify which ranks get which commands");
                    streamWriter.WriteLine("#   Current ranks: " + Group.concatList(false, false, true));
                    streamWriter.WriteLine(
                        "#   Disallow and allow can be left empty, just make sure there's 2 spaces between the colons");
                    streamWriter.WriteLine(
                        "#   This works entirely on permission values, not names. Do not enter a rank name. Use it's permission value");
                    streamWriter.WriteLine("#   CommandName : LowestRank : Disallow : Allow");
                    streamWriter.WriteLine("#   gun : 60 : 80,67 : 40,41,55");
                    streamWriter.WriteLine("");
                    foreach (var rankAllowance in givenList)
                        streamWriter.WriteLine(string.Concat(rankAllowance.commandName, " : ",
                            (int) rankAllowance.lowestRank, " : ", getInts(rankAllowance.disallow), " : ",
                            getInts(rankAllowance.allow)));
                }
            }
            catch
            {
                Server.s.Log("SAVE FAILED! command.properties");
            }
        }

        // Token: 0x06000D3F RID: 3391 RVA: 0x0004C110 File Offset: 0x0004A310
        public static string getInts(List<LevelPermission> givenList)
        {
            var text = "";
            var flag = false;
            foreach (var levelPermission in givenList)
            {
                flag = true;
                text = text + "," + (int) levelPermission;
            }

            if (flag) text = text.Remove(0, 1);
            return text;
        }

        // Token: 0x06000D40 RID: 3392 RVA: 0x0004C180 File Offset: 0x0004A380
        public static void AddCommands(out CommandList commands, LevelPermission perm)
        {
            commands = new CommandList();
            foreach (var rankAllowance in allowedCommands)
                if (rankAllowance.lowestRank <= perm && !rankAllowance.disallow.Contains(perm) ||
                    rankAllowance.allow.Contains(perm))
                    commands.Add(Command.all.Find(rankAllowance.commandName));
        }

        // Token: 0x020001DA RID: 474
        public class rankAllowance
        {
            // Token: 0x040006E0 RID: 1760
            public List<LevelPermission> allow = new List<LevelPermission>();

            // Token: 0x040006DD RID: 1757
            public string commandName;

            // Token: 0x040006DF RID: 1759
            public List<LevelPermission> disallow = new List<LevelPermission>();

            // Token: 0x040006DE RID: 1758
            public LevelPermission lowestRank;
        }
    }
}