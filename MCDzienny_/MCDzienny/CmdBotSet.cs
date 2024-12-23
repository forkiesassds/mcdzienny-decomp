using System;
using System.IO;

namespace MCDzienny
{
    public class CmdBotSet : Command
    {
        public override string name { get { return "botset"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override string CustomName { get { return Lang.Command.BotSetName; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            try
            {
                if (message.Split(' ').Length == 1)
                {
                    PlayerBot playerBot = PlayerBot.Find(message);
                    try
                    {
                        playerBot.Waypoints.Clear();
                    }
                    catch {}
                    playerBot.kill = false;
                    playerBot.hunt = false;
                    playerBot.AIName = "";
                    Player.SendMessage(p, string.Format(Lang.Command.BotSetMessage, playerBot.color + playerBot.name + Server.DefaultColor));
                    return;
                }
                if (message.Split(' ').Length != 2)
                {
                    Help(p);
                    return;
                }
                PlayerBot playerBot2 = PlayerBot.Find(message.Split(' ')[0]);
                if (playerBot2 == null)
                {
                    Player.SendMessage(p, Lang.Command.BotSetMessage1);
                    return;
                }
                string text = message.Split(' ')[1].ToLower();
                if (text == Lang.Command.BotSetParameter)
                {
                    playerBot2.hunt = !playerBot2.hunt;
                    try
                    {
                        playerBot2.Waypoints.Clear();
                    }
                    catch {}
                    playerBot2.AIName = "";
                    if (p != null)
                    {
                        Player.GlobalChatLevel(p, string.Format(Lang.Command.BotSetMessage2, playerBot2.color + playerBot2.name + Server.DefaultColor, playerBot2.hunt),
                                               showname: false);
                    }
                    return;
                }
                if (text == Lang.Command.BotSetParameter1)
                {
                    if (p.group.Permission < LevelPermission.Operator)
                    {
                        Player.SendMessage(p, Lang.Command.BotSetMessage3);
                        return;
                    }
                    playerBot2.kill = !playerBot2.kill;
                    if (p != null)
                    {
                        Player.GlobalChatLevel(p, string.Format(Lang.Command.BotSetMessage4, playerBot2.color + playerBot2.name + Server.DefaultColor, playerBot2.kill),
                                               showname: false);
                    }
                    return;
                }
                if (!File.Exists("bots/" + text))
                {
                    Player.SendMessage(p, Lang.Command.BotSetMessage5);
                    return;
                }
                string[] array = File.ReadAllLines("bots/" + text);
                if (array[0] != "#Version 2")
                {
                    Player.SendMessage(p, Lang.Command.BotSetMessage6);
                    return;
                }
                PlayerBot.Pos item = default(PlayerBot.Pos);
                try
                {
                    playerBot2.Waypoints.Clear();
                    playerBot2.currentPoint = 0;
                    playerBot2.countdown = 0;
                    playerBot2.movementSpeed = 12;
                }
                catch {}
                try
                {
                    string[] array2 = array;
                    foreach (string text2 in array2)
                    {
                        if (text2 != "" && text2[0] != '#')
                        {
                            bool flag = false;
                            item.type = text2.Split(' ')[0];
                            string text3 = text2.Split(' ')[0].ToLower();
                            if (text3 == Lang.Command.BotSetParameter2 || text3 == Lang.Command.BotSetParameter3)
                            {
                                item.x = Convert.ToUInt16(text2.Split(' ')[1]);
                                item.y = Convert.ToUInt16(text2.Split(' ')[2]);
                                item.z = Convert.ToUInt16(text2.Split(' ')[3]);
                                item.rotx = Convert.ToByte(text2.Split(' ')[4]);
                                item.roty = Convert.ToByte(text2.Split(' ')[5]);
                            }
                            else if (text3 == Lang.Command.BotSetParameter4 || text3 == Lang.Command.BotSetParameter5)
                            {
                                item.seconds = Convert.ToInt16(text2.Split(' ')[1]);
                            }
                            else if (text3 == Lang.Command.BotSetParameter6 || text3 == Lang.Command.BotSetParameter7)
                            {
                                item.seconds = Convert.ToInt16(text2.Split(' ')[1]);
                                item.rotspeed = Convert.ToInt16(text2.Split(' ')[2]);
                            }
                            else if (text3 == Lang.Command.BotSetParameter8)
                            {
                                item.newscript = text2.Split(' ')[1];
                            }
                            else if (!(text3 == Lang.Command.BotSetParameter9) && !(text3 == Lang.Command.BotSetParameter10) && !(text3 == Lang.Command.BotSetParameter11))
                            {
                                flag = true;
                            }
                            if (!flag)
                            {
                                playerBot2.Waypoints.Add(item);
                            }
                        }
                    }
                }
                catch
                {
                    Player.SendMessage(p, Lang.Command.BotSetMessage7);
                    return;
                }
                playerBot2.AIName = text;
                if (p != null)
                {
                    Player.GlobalChatLevel(p, string.Format(Lang.Command.BotSetMessage8, playerBot2.color + playerBot2.name + Server.DefaultColor, text), showname: false);
                }
            }
            catch
            {
                Player.SendMessage(p, Lang.Command.BotSetMessage9);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.BotSetHelp);
            Player.SendMessage(p, Lang.Command.BotSetHelp1);
        }
    }
}