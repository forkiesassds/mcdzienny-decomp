using System;
using System.IO;

namespace MCDzienny
{
    public class CmdBotAI : Command
    {
        public override string name { get { return "botai"; } }

        public override string shortcut { get { return "bai"; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override string CustomName { get { return Lang.Command.BotAIName; } }

        public override void Use(Player p, string message)
        {
            if (message.Split(' ').Length < 2)
            {
                Help(p);
                return;
            }
            string text = message.Split(' ')[1].ToLower();
            if (!Player.ValidName(text))
            {
                Player.SendMessage(p, Lang.Command.BotAIMessage);
                return;
            }
            if (text == Lang.Command.BotAIParameter || text == Lang.Command.BotAIParameter1)
            {
                Player.SendMessage(p, Lang.Command.BotAIMessage1);
                return;
            }
            try
            {
                if (message.Split(' ')[0] == Lang.Command.BotAIParameter2)
                {
                    if (message.Split(' ').Length == 2)
                    {
                        addPoint(p, text);
                    }
                    else if (message.Split(' ').Length == 3)
                    {
                        addPoint(p, text, message.Split(' ')[2]);
                    }
                    else if (message.Split(' ').Length == 4)
                    {
                        addPoint(p, text, message.Split(' ')[2], message.Split(' ')[3]);
                    }
                    else
                    {
                        addPoint(p, text, message.Split(' ')[2], message.Split(' ')[3], message.Split(' ')[4]);
                    }
                }
                else if (message.Split(' ')[0] == Lang.Command.BotAIParameter3)
                {
                    if (!Directory.Exists("bots/deleted"))
                    {
                        Directory.CreateDirectory("bots/deleted");
                    }
                    int num = 0;
                    if (File.Exists("bots/" + text))
                    {
                        while (true)
                        {
                            try
                            {
                                if (message.Split(' ').Length == 2)
                                {
                                    if (num == 0)
                                    {
                                        File.Move("bots/" + text, "bots/deleted/" + text);
                                    }
                                    else
                                    {
                                        File.Move("bots/" + text, "bots/deleted/" + text + num);
                                    }
                                    break;
                                }
                                if (message.Split(' ')[2].ToLower() == "last")
                                {
                                    string[] array = File.ReadAllLines("bots/" + text);
                                    string[] array2 = new string[array.Length - 1];
                                    for (int i = 0; i < array.Length - 1; i++)
                                    {
                                        array2[i] = array[i];
                                    }
                                    File.WriteAllLines("bots/" + text, array2);
                                    Player.SendMessage(p, string.Format(Lang.Command.BotAIMessage2, text));
                                }
                                else
                                {
                                    Help(p);
                                }
                                return;
                            }
                            catch (IOException)
                            {
                                num++;
                            }
                        }
                        Player.SendMessage(p, string.Format(Lang.Command.BotAIMessage3, text));
                    }
                    else
                    {
                        Player.SendMessage(p, Lang.Command.BotAIMessage4);
                    }
                }
                else
                {
                    Help(p);
                }
            }
            catch (Exception ex2)
            {
                Server.ErrorLog(ex2);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.BotAIHelp);
            Player.SendMessage(p, Lang.Command.BotAIHelp1);
            Player.SendMessage(p, Lang.Command.BotAIHelp2);
            Player.SendMessage(p, Lang.Command.BotAIHelp3);
            Player.SendMessage(p, Lang.Command.BotAIHelp4);
            Player.SendMessage(p, Lang.Command.BotAIHelp5);
        }

        public void addPoint(Player p, string foundPath, string additional = "", string extra = "10", string more = "2")
        {
            string[] array;
            try
            {
                array = File.ReadAllLines("bots/" + foundPath);
            }
            catch
            {
                array = new string[1];
            }
            StreamWriter streamWriter;
            try
            {
                if (!File.Exists("bots/" + foundPath))
                {
                    Player.SendMessage(p, string.Format(Lang.Command.BotAIMessage5, foundPath));
                    streamWriter = new StreamWriter(File.Create("bots/" + foundPath));
                    streamWriter.WriteLine("#Version 2");
                }
                else if (array[0] != "#Version 2")
                {
                    Player.SendMessage(p, Lang.Command.BotAIMessage6);
                    streamWriter = new StreamWriter(File.Create("bots/" + foundPath));
                    streamWriter.WriteLine("#Version 2");
                }
                else
                {
                    Player.SendMessage(p, string.Format(Lang.Command.BotAIMessage7, foundPath));
                    streamWriter = new StreamWriter("bots/" + foundPath, append: true);
                }
            }
            catch
            {
                Player.SendMessage(p, Lang.Command.BotAIMessage8);
                return;
            }
            try
            {
                switch (additional.ToLower())
                {
                    case "":
                    case "walk":
                        streamWriter.WriteLine("walk " + p.pos[0] + " " + p.pos[1] + " " + p.pos[2] + " " + p.rot[0] + " " + p.rot[1]);
                        break;
                    case "teleport":
                    case "tp":
                        streamWriter.WriteLine("teleport " + p.pos[0] + " " + p.pos[1] + " " + p.pos[2] + " " + p.rot[0] + " " + p.rot[1]);
                        break;
                    case "wait":
                        streamWriter.WriteLine("wait " + int.Parse(extra));
                        break;
                    case "nod":
                        streamWriter.WriteLine("nod " + int.Parse(extra) + " " + int.Parse(more));
                        break;
                    case "speed":
                        streamWriter.WriteLine("speed " + int.Parse(extra));
                        break;
                    case "remove":
                        streamWriter.WriteLine("remove");
                        break;
                    case "reset":
                        streamWriter.WriteLine("reset");
                        break;
                    case "spin":
                        streamWriter.WriteLine("spin " + int.Parse(extra) + " " + int.Parse(more));
                        break;
                    case "reverse":
                    {
                        for (int num = array.Length - 1; num > 0; num--)
                        {
                            if (array[num][0] != '#' && array[num] != "")
                            {
                                streamWriter.WriteLine(array[num]);
                            }
                        }
                        break;
                    }
                    case "linkscript":
                        if (extra != "10")
                        {
                            streamWriter.WriteLine("linkscript " + extra);
                        }
                        else
                        {
                            Player.SendMessage(p, Lang.Command.BotAIMessage9);
                        }
                        break;
                    case "jump":
                        streamWriter.WriteLine("jump");
                        break;
                    default:
                        Player.SendMessage(p, string.Format(Lang.Command.BotAIMessage10, additional));
                        break;
                }
                streamWriter.Dispose();
            }
            catch
            {
                Player.SendMessage(p, Lang.Command.BotAIMessage11);
                streamWriter.Close();
            }
        }
    }
}