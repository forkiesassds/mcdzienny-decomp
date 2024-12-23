using System;
using System.Collections.Generic;
using System.IO;

namespace MCDzienny
{
    public class GrpCommands
    {

        public static List<rankAllowance> allowedCommands;

        public static List<string> foundCommands = new List<string>();

        public static LevelPermission defaultRanks(string command)
        {
            Command cmd = Command.all.Find(command);
            return cmd != null ? cmd.defaultRank : LevelPermission.Null;
        }

        public static void fillRanks()
        {
            foundCommands = Command.all.commandNames();
            allowedCommands = new List<rankAllowance>();
            foreach (Command item in Command.all.All())
            {
                rankAllowance rankAllowance = new rankAllowance();
                rankAllowance.commandName = item.name;
                rankAllowance.lowestRank = item.defaultRank;
                allowedCommands.Add(rankAllowance);
            }
            if (File.Exists("properties/command.properties"))
            {
                string[] array = File.ReadAllLines("properties/command.properties");
                if (array[0] == "#Version 2")
                {
                    string[] separator = new string[1]
                    {
                        " : "
                    };
                    string[] array2 = array;
                    foreach (string text in array2)
                    {
                        rankAllowance rankAllowance = new rankAllowance();
                        if (!(text != "") || text[0] == '#')
                        {
                            continue;
                        }
                        string[] array3 = text.Split(separator, StringSplitOptions.None);
                        if (!foundCommands.Contains(array3[0]))
                        {
                            Server.s.Log("Incorrect command name: " + array3[0]);
                            continue;
                        }
                        rankAllowance.commandName = array3[0];
                        string[] array4 = new string[0];
                        if (array3[2] != "")
                        {
                            array4 = array3[2].Split(',');
                        }
                        string[] array5 = new string[0];
                        if (array3[3] != "")
                        {
                            array5 = array3[3].Split(',');
                        }
                        try
                        {
                            rankAllowance.lowestRank = (LevelPermission)int.Parse(array3[1]);
                            string[] array6 = array4;
                            foreach (string s in array6)
                            {
                                rankAllowance.disallow.Add((LevelPermission)int.Parse(s));
                            }
                            string[] array7 = array5;
                            foreach (string s2 in array7)
                            {
                                rankAllowance.allow.Add((LevelPermission)int.Parse(s2));
                            }
                        }
                        catch
                        {
                            Server.s.Log("Hit an error on the command " + text);
                            continue;
                        }
                        int num = 0;
                        foreach (rankAllowance allowedCommand in allowedCommands)
                        {
                            if (array3[0] == allowedCommand.commandName)
                            {
                                allowedCommands[num] = rankAllowance;
                                break;
                            }
                            num++;
                        }
                    }
                }
                else
                {
                    string[] array8 = array;
                    foreach (string text2 in array8)
                    {
                        if (!(text2 != "") || text2[0] == '#')
                        {
                            continue;
                        }
                        rankAllowance rankAllowance = new rankAllowance();
                        string text3 = text2.Split('=')[0].Trim().ToLower();
                        string name = text2.Split('=')[1].Trim().ToLower();
                        if (!foundCommands.Contains(text3))
                        {
                            Server.s.Log("Incorrect command name: " + text3);
                            continue;
                        }
                        if (Level.PermissionFromName(name) == LevelPermission.Null)
                        {
                            Server.s.Log("Incorrect value given for " + text3 + ", using default value.");
                            continue;
                        }
                        rankAllowance.commandName = text3;
                        rankAllowance.lowestRank = Level.PermissionFromName(name);
                        int num2 = 0;
                        foreach (rankAllowance allowedCommand2 in allowedCommands)
                        {
                            if (text3 == allowedCommand2.commandName)
                            {
                                allowedCommands[num2] = rankAllowance;
                                break;
                            }
                            num2++;
                        }
                    }
                }
                Save(allowedCommands);
            }
            else
            {
                Save(allowedCommands);
            }
            foreach (Group group in Group.groupList)
            {
                group.fillCommands();
            }
        }

        public static void Save(List<rankAllowance> givenList)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(File.Create("properties/command.properties")))
                {
                    streamWriter.WriteLine("#Version 2");
                    streamWriter.WriteLine("#   This file contains a reference to every command found in the server software");
                    streamWriter.WriteLine("#   Use this file to specify which ranks get which commands");
                    streamWriter.WriteLine("#   Current ranks: " + Group.concatList(includeColor: false, skipExtra: false, permissions: true));
                    streamWriter.WriteLine("#   Disallow and allow can be left empty, just make sure there's 2 spaces between the colons");
                    streamWriter.WriteLine("#   This works entirely on permission values, not names. Do not enter a rank name. Use it's permission value");
                    streamWriter.WriteLine("#   CommandName : LowestRank : Disallow : Allow");
                    streamWriter.WriteLine("#   gun : 60 : 80,67 : 40,41,55");
                    streamWriter.WriteLine("");
                    foreach (rankAllowance given in givenList)
                    {
                        streamWriter.WriteLine(given.commandName + " : " + (int)given.lowestRank + " : " + getInts(given.disallow) + " : " + getInts(given.allow));
                    }
                }
            }
            catch
            {
                Server.s.Log("SAVE FAILED! command.properties");
            }
        }

        public static string getInts(List<LevelPermission> givenList)
        {
            string text = "";
            bool flag = false;
            foreach (LevelPermission given in givenList)
            {
                flag = true;
                text = text + "," + (int)given;
            }
            if (flag)
            {
                text = text.Remove(0, 1);
            }
            return text;
        }

        public static void AddCommands(out CommandList commands, LevelPermission perm)
        {
            commands = new CommandList();
            foreach (rankAllowance allowedCommand in allowedCommands)
            {
                if (allowedCommand.lowestRank <= perm && !allowedCommand.disallow.Contains(perm) || allowedCommand.allow.Contains(perm))
                {
                    commands.Add(Command.all.Find(allowedCommand.commandName));
                }
            }
        }

        public class rankAllowance
        {

            public List<LevelPermission> allow = new List<LevelPermission>();
            public string commandName;

            public List<LevelPermission> disallow = new List<LevelPermission>();

            public LevelPermission lowestRank;
        }
    }
}