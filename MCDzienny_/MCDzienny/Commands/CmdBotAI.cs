using System;
using System.IO;

namespace MCDzienny
{
    // Token: 0x0200024A RID: 586
    public class CmdBotAI : Command
    {
        // Token: 0x17000607 RID: 1543
        // (get) Token: 0x060010F8 RID: 4344 RVA: 0x0005C3D4 File Offset: 0x0005A5D4
        public override string name
        {
            get { return "botai"; }
        }

        // Token: 0x17000608 RID: 1544
        // (get) Token: 0x060010F9 RID: 4345 RVA: 0x0005C3DC File Offset: 0x0005A5DC
        public override string shortcut
        {
            get { return "bai"; }
        }

        // Token: 0x17000609 RID: 1545
        // (get) Token: 0x060010FA RID: 4346 RVA: 0x0005C3E4 File Offset: 0x0005A5E4
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x1700060A RID: 1546
        // (get) Token: 0x060010FB RID: 4347 RVA: 0x0005C3EC File Offset: 0x0005A5EC
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x1700060B RID: 1547
        // (get) Token: 0x060010FC RID: 4348 RVA: 0x0005C3F0 File Offset: 0x0005A5F0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x1700060C RID: 1548
        // (get) Token: 0x060010FD RID: 4349 RVA: 0x0005C3F4 File Offset: 0x0005A5F4
        public override string CustomName
        {
            get { return Lang.Command.BotAIName; }
        }

        // Token: 0x060010FF RID: 4351 RVA: 0x0005C404 File Offset: 0x0005A604
        public override void Use(Player p, string message)
        {
            if (message.Split(' ').Length < 2)
            {
                Help(p);
                return;
            }

            var text = message.Split(' ')[1].ToLower();
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
                        addPoint(p, text);
                    else if (message.Split(' ').Length == 3)
                        addPoint(p, text, message.Split(' ')[2]);
                    else if (message.Split(' ').Length == 4)
                        addPoint(p, text, message.Split(' ')[2], message.Split(' ')[3]);
                    else
                        addPoint(p, text, message.Split(' ')[2], message.Split(' ')[3], message.Split(' ')[4]);
                }
                else if (message.Split(' ')[0] == Lang.Command.BotAIParameter3)
                {
                    if (!Directory.Exists("bots/deleted")) Directory.CreateDirectory("bots/deleted");
                    var num = 0;
                    if (File.Exists("bots/" + text))
                    {
                        try
                        {
                            if (message.Split(' ').Length == 2)
                            {
                                if (num == 0)
                                    File.Move("bots/" + text, "bots/deleted/" + text);
                                else
                                    File.Move("bots/" + text, "bots/deleted/" + text + num);
                            }
                            else
                            {
                                if (message.Split(' ')[2].ToLower() == "last")
                                {
                                    var array = File.ReadAllLines("bots/" + text);
                                    var array2 = new string[array.Length - 1];
                                    for (var i = 0; i < array.Length - 1; i++) array2[i] = array[i];
                                    File.WriteAllLines("bots/" + text, array2);
                                    Player.SendMessage(p, string.Format(Lang.Command.BotAIMessage2, text));
                                    return;
                                }

                                Help(p);
                                return;
                            }
                        }
                        catch (IOException)
                        {
                            num++;
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
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x06001100 RID: 4352 RVA: 0x0005C7A4 File Offset: 0x0005A9A4
        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.BotAIHelp);
            Player.SendMessage(p, Lang.Command.BotAIHelp1);
            Player.SendMessage(p, Lang.Command.BotAIHelp2);
            Player.SendMessage(p, Lang.Command.BotAIHelp3);
            Player.SendMessage(p, Lang.Command.BotAIHelp4);
            Player.SendMessage(p, Lang.Command.BotAIHelp5);
        }

        // Token: 0x06001101 RID: 4353 RVA: 0x0005C7F4 File Offset: 0x0005A9F4
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
                    streamWriter = new StreamWriter("bots/" + foundPath, true);
                }
            }
            catch
            {
                Player.SendMessage(p, Lang.Command.BotAIMessage8);
                return;
            }

            try
            {
                string key;
                switch (key = additional.ToLower())
                {
                    case "":
                    case "walk":
                        streamWriter.WriteLine(string.Concat("walk ", p.pos[0], " ", p.pos[1], " ", p.pos[2], " ",
                            p.rot[0], " ", p.rot[1]));
                        goto IL_4B7;
                    case "teleport":
                    case "tp":
                        streamWriter.WriteLine(string.Concat("teleport ", p.pos[0], " ", p.pos[1], " ", p.pos[2], " ",
                            p.rot[0], " ", p.rot[1]));
                        goto IL_4B7;
                    case "wait":
                        streamWriter.WriteLine("wait " + int.Parse(extra));
                        goto IL_4B7;
                    case "nod":
                        streamWriter.WriteLine(string.Concat("nod ", int.Parse(extra), " ", int.Parse(more)));
                        goto IL_4B7;
                    case "speed":
                        streamWriter.WriteLine("speed " + int.Parse(extra));
                        goto IL_4B7;
                    case "remove":
                        streamWriter.WriteLine("remove");
                        goto IL_4B7;
                    case "reset":
                        streamWriter.WriteLine("reset");
                        goto IL_4B7;
                    case "spin":
                        streamWriter.WriteLine(string.Concat("spin ", int.Parse(extra), " ", int.Parse(more)));
                        goto IL_4B7;
                    case "reverse":
                        for (var i = array.Length - 1; i > 0; i--)
                            if (array[i][0] != '#' && array[i] != "")
                                streamWriter.WriteLine(array[i]);
                        goto IL_4B7;
                    case "linkscript":
                        if (extra != "10")
                        {
                            streamWriter.WriteLine("linkscript " + extra);
                            goto IL_4B7;
                        }

                        Player.SendMessage(p, Lang.Command.BotAIMessage9);
                        goto IL_4B7;
                    case "jump":
                        streamWriter.WriteLine("jump");
                        goto IL_4B7;
                }

                Player.SendMessage(p, string.Format(Lang.Command.BotAIMessage10, additional));
                IL_4B7:
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