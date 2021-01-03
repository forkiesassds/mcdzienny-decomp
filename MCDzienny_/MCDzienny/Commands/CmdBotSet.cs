using System;
using System.IO;

namespace MCDzienny
{
    // Token: 0x0200024B RID: 587
    public class CmdBotSet : Command
    {
        // Token: 0x1700060D RID: 1549
        // (get) Token: 0x06001102 RID: 4354 RVA: 0x0005CD20 File Offset: 0x0005AF20
        public override string name
        {
            get { return "botset"; }
        }

        // Token: 0x1700060E RID: 1550
        // (get) Token: 0x06001103 RID: 4355 RVA: 0x0005CD28 File Offset: 0x0005AF28
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700060F RID: 1551
        // (get) Token: 0x06001104 RID: 4356 RVA: 0x0005CD30 File Offset: 0x0005AF30
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000610 RID: 1552
        // (get) Token: 0x06001105 RID: 4357 RVA: 0x0005CD38 File Offset: 0x0005AF38
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000611 RID: 1553
        // (get) Token: 0x06001106 RID: 4358 RVA: 0x0005CD3C File Offset: 0x0005AF3C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x17000612 RID: 1554
        // (get) Token: 0x06001107 RID: 4359 RVA: 0x0005CD40 File Offset: 0x0005AF40
        public override string CustomName
        {
            get { return Lang.Command.BotSetName; }
        }

        // Token: 0x06001109 RID: 4361 RVA: 0x0005CD50 File Offset: 0x0005AF50
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
                    var playerBot = PlayerBot.Find(message);
                    try
                    {
                        playerBot.Waypoints.Clear();
                    }
                    catch
                    {
                    }

                    playerBot.kill = false;
                    playerBot.hunt = false;
                    playerBot.AIName = "";
                    Player.SendMessage(p,
                        string.Format(Lang.Command.BotSetMessage,
                            playerBot.color + playerBot.name + Server.DefaultColor));
                }
                else if (message.Split(' ').Length != 2)
                {
                    Help(p);
                }
                else
                {
                    var playerBot2 = PlayerBot.Find(message.Split(' ')[0]);
                    if (playerBot2 == null)
                    {
                        Player.SendMessage(p, Lang.Command.BotSetMessage1);
                    }
                    else
                    {
                        var text = message.Split(' ')[1].ToLower();
                        if (text == Lang.Command.BotSetParameter)
                        {
                            playerBot2.hunt = !playerBot2.hunt;
                            try
                            {
                                playerBot2.Waypoints.Clear();
                            }
                            catch
                            {
                            }

                            playerBot2.AIName = "";
                            if (p != null)
                                Player.GlobalChatLevel(p,
                                    string.Format(Lang.Command.BotSetMessage2,
                                        playerBot2.color + playerBot2.name + Server.DefaultColor, playerBot2.hunt),
                                    false);
                        }
                        else if (text == Lang.Command.BotSetParameter1)
                        {
                            if (p.group.Permission < LevelPermission.Operator)
                            {
                                Player.SendMessage(p, Lang.Command.BotSetMessage3);
                            }
                            else
                            {
                                playerBot2.kill = !playerBot2.kill;
                                if (p != null)
                                    Player.GlobalChatLevel(p,
                                        string.Format(Lang.Command.BotSetMessage4,
                                            playerBot2.color + playerBot2.name + Server.DefaultColor, playerBot2.kill),
                                        false);
                            }
                        }
                        else if (!File.Exists("bots/" + text))
                        {
                            Player.SendMessage(p, Lang.Command.BotSetMessage5);
                        }
                        else
                        {
                            var array = File.ReadAllLines("bots/" + text);
                            if (array[0] != "#Version 2")
                            {
                                Player.SendMessage(p, Lang.Command.BotSetMessage6);
                            }
                            else
                            {
                                var item = default(PlayerBot.Pos);
                                try
                                {
                                    playerBot2.Waypoints.Clear();
                                    playerBot2.currentPoint = 0;
                                    playerBot2.countdown = 0;
                                    playerBot2.movementSpeed = 12;
                                }
                                catch
                                {
                                }

                                try
                                {
                                    foreach (var text2 in array)
                                        if (text2 != "" && text2[0] != '#')
                                        {
                                            var flag = false;
                                            item.type = text2.Split(' ')[0];
                                            var a = text2.Split(' ')[0].ToLower();
                                            if (a == Lang.Command.BotSetParameter2 ||
                                                a == Lang.Command.BotSetParameter3)
                                            {
                                                item.x = Convert.ToUInt16(text2.Split(' ')[1]);
                                                item.y = Convert.ToUInt16(text2.Split(' ')[2]);
                                                item.z = Convert.ToUInt16(text2.Split(' ')[3]);
                                                item.rotx = Convert.ToByte(text2.Split(' ')[4]);
                                                item.roty = Convert.ToByte(text2.Split(' ')[5]);
                                            }
                                            else if (a == Lang.Command.BotSetParameter4 ||
                                                     a == Lang.Command.BotSetParameter5)
                                            {
                                                item.seconds = Convert.ToInt16(text2.Split(' ')[1]);
                                            }
                                            else if (a == Lang.Command.BotSetParameter6 ||
                                                     a == Lang.Command.BotSetParameter7)
                                            {
                                                item.seconds = Convert.ToInt16(text2.Split(' ')[1]);
                                                item.rotspeed = Convert.ToInt16(text2.Split(' ')[2]);
                                            }
                                            else if (a == Lang.Command.BotSetParameter8)
                                            {
                                                item.newscript = text2.Split(' ')[1];
                                            }
                                            else if (!(a == Lang.Command.BotSetParameter9) &&
                                                     !(a == Lang.Command.BotSetParameter10) &&
                                                     !(a == Lang.Command.BotSetParameter11))
                                            {
                                                flag = true;
                                            }

                                            if (!flag) playerBot2.Waypoints.Add(item);
                                        }
                                }
                                catch
                                {
                                    Player.SendMessage(p, Lang.Command.BotSetMessage7);
                                    return;
                                }

                                playerBot2.AIName = text;
                                if (p != null)
                                    Player.GlobalChatLevel(p,
                                        string.Format(Lang.Command.BotSetMessage8,
                                            playerBot2.color + playerBot2.name + Server.DefaultColor, text), false);
                            }
                        }
                    }
                }
            }
            catch
            {
                Player.SendMessage(p, Lang.Command.BotSetMessage9);
            }
        }

        // Token: 0x0600110A RID: 4362 RVA: 0x0005D314 File Offset: 0x0005B514
        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.BotSetHelp);
            Player.SendMessage(p, Lang.Command.BotSetHelp1);
        }
    }
}