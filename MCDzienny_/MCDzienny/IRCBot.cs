using System;
using System.Text.RegularExpressions;
using System.Threading;
using Meebey.SmartIrc4net;

namespace MCDzienny
{
    // Token: 0x02000308 RID: 776
    internal class IRCBot
    {
        // Token: 0x04000AEC RID: 2796
        private static readonly IrcClient irc = new IrcClient();

        // Token: 0x04000AED RID: 2797
        private static readonly string server = Server.ircServer;

        // Token: 0x04000AEE RID: 2798
        private static readonly string channel = Server.ircChannel;

        // Token: 0x04000AEF RID: 2799
        private static readonly string opchannel = Server.ircOpChannel;

        // Token: 0x04000AF0 RID: 2800
        private static readonly string nick = Server.ircNick;

        // Token: 0x04000AF1 RID: 2801
        private static Thread ircThread;

        // Token: 0x04000AF2 RID: 2802
        private static string[] names;

        // Token: 0x06001666 RID: 5734 RVA: 0x00086BA4 File Offset: 0x00084DA4
        public IRCBot()
        {
            ircThread = new Thread(delegate()
            {
                irc.OnConnecting += OnConnecting;
                irc.OnConnected += OnConnected;
                irc.OnChannelMessage += OnChanMessage;
                irc.OnJoin += OnJoin;
                irc.OnPart += OnPart;
                irc.OnQuit += OnQuit;
                irc.OnNickChange += OnNickChange;
                irc.OnDisconnected += OnDisconnected;
                irc.OnQueryMessage += OnPrivMsg;
                irc.OnNames += OnNames;
                irc.OnChannelAction += OnAction;
                try
                {
                    irc.Connect(server, Server.ircPort);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to connect to IRC server: {0}", ex.Message);
                }
            });
            ircThread.IsBackground = true;
            ircThread.Name = "Irc Thread";
            ircThread.Start();
        }

        // Token: 0x06001667 RID: 5735 RVA: 0x00086BF8 File Offset: 0x00084DF8
        private void OnConnecting(object sender, EventArgs e)
        {
            Server.s.Log("Connecting to IRC");
        }

        // Token: 0x06001668 RID: 5736 RVA: 0x00086C0C File Offset: 0x00084E0C
        private void OnConnected(object sender, EventArgs e)
        {
            Server.s.Log("Connected to IRC");
            irc.Login(nick, nick, 0, nick);
            if (Server.ircIdentify && Server.ircPassword != string.Empty)
            {
                Server.s.Log("Identifying with Nickserv");
                irc.SendMessage(SendType.Message, "nickserv", "IDENTIFY " + Server.ircPassword);
            }

            Server.s.Log("Joining channels");
            irc.RfcJoin(channel);
            irc.RfcJoin(opchannel);
            irc.Listen();
        }

        // Token: 0x06001669 RID: 5737 RVA: 0x00086CC4 File Offset: 0x00084EC4
        private void OnNames(object sender, NamesEventArgs e)
        {
            names = e.UserList;
        }

        // Token: 0x0600166A RID: 5738 RVA: 0x00086CD4 File Offset: 0x00084ED4
        private void OnDisconnected(object sender, EventArgs e)
        {
            try
            {
                irc.Connect(server, 6667);
            }
            catch
            {
                Console.WriteLine("Failed to reconnect to IRC");
            }
        }

        // Token: 0x0600166B RID: 5739 RVA: 0x00086D14 File Offset: 0x00084F14
        private void OnChanMessage(object sender, IrcEventArgs e)
        {
            var text = e.Data.Message;
            var text2 = e.Data.Nick;
            var text3 = "1234567890-=qwertyuiop[]\\asdfghjkl;'zxcvbnm,./!@#$%^*()_+QWERTYUIOPASDFGHJKL:\"ZXCVBNM<>? ";
            foreach (var value in text)
                if (text3.IndexOf(value) == -1)
                    text = text.Replace(value.ToString(), "*");
            if (e.Data.Channel == opchannel)
            {
                Server.s.Log("[(Op) IRC] " + e.Data.Nick + ": " + text);
                Player.GlobalMessageOps(string.Concat(Server.IRCColour, "[(Op) IRC] ", text2, ": &f", text));
                return;
            }

            Server.s.Log("[IRC] " + e.Data.Nick + ": " + text);
            Player.GlobalChat(null, string.Concat(Server.IRCColour, "[IRC] ", text2, ": &f", text), false);
        }

        // Token: 0x0600166C RID: 5740 RVA: 0x00086E58 File Offset: 0x00085058
        private void OnJoin(object sender, JoinEventArgs e)
        {
            Server.s.Log(e.Data.Nick + " has joined channel " + e.Data.Channel);
            if (e.Data.Channel == opchannel)
                Player.GlobalChat(null,
                    Server.IRCColour + e.Data.Nick + Server.DefaultColor + " has joined the operator channel", false);
            else
                Player.GlobalChat(null,
                    Server.IRCColour + e.Data.Nick + Server.DefaultColor + " has joined the channel", false);
            irc.RfcNames(channel);
            irc.RfcNames(opchannel);
        }

        // Token: 0x0600166D RID: 5741 RVA: 0x00086F14 File Offset: 0x00085114
        private void OnPart(object sender, PartEventArgs e)
        {
            Server.s.Log(e.Data.Nick + " has left channel " + e.Data.Channel);
            if (e.Data.Channel == opchannel)
                Player.GlobalChat(null,
                    Server.IRCColour + e.Data.Nick + Server.DefaultColor + " has left the operator channel", false);
            else
                Player.GlobalChat(null, Server.IRCColour + e.Data.Nick + Server.DefaultColor + " has left the channel",
                    false);
            irc.RfcNames(channel);
            irc.RfcNames(opchannel);
        }

        // Token: 0x0600166E RID: 5742 RVA: 0x00086FD0 File Offset: 0x000851D0
        private void OnQuit(object sender, QuitEventArgs e)
        {
            Server.s.Log(e.Data.Nick + " has left IRC");
            Player.GlobalChat(null, Server.IRCColour + e.Data.Nick + Server.DefaultColor + " has left IRC", false);
            irc.RfcNames(channel);
            irc.RfcNames(opchannel);
        }

        // Token: 0x0600166F RID: 5743 RVA: 0x00087044 File Offset: 0x00085244
        private void OnPrivMsg(object sender, IrcEventArgs e)
        {
            Server.s.Log("IRC RECEIVING MESSAGE");
            if (Server.ircControllers.Contains(e.Data.Nick))
            {
                var num = e.Data.Message.Split(' ').Length;
                var text = e.Data.Message.Split(' ')[0];
                string text2;
                if (num > 1)
                    text2 = e.Data.Message.Substring(e.Data.Message.IndexOf(' ')).Trim();
                else
                    text2 = "";
                if (text2 != "" || text == "restart" || text == "update")
                {
                    Server.s.Log(text + " : " + text2);
                    string key;
                    switch (key = text)
                    {
                        case "kick":
                            if (Player.Find(text2.Split()[0]) != null)
                            {
                                Command.all.Find("kick").Use(null, text2);
                                return;
                            }

                            irc.SendMessage(SendType.Message, e.Data.Nick, "Player not found.");
                            return;
                        case "ban":
                            if (Player.Find(text2) != null)
                            {
                                Command.all.Find("ban").Use(null, text2);
                                return;
                            }

                            irc.SendMessage(SendType.Message, e.Data.Nick, "Player not found.");
                            return;
                        case "banip":
                            if (Player.Find(text2) != null)
                            {
                                Command.all.Find("banip").Use(null, text2);
                                return;
                            }

                            irc.SendMessage(SendType.Message, e.Data.Nick, "Player not found.");
                            return;
                        case "say":
                            irc.SendMessage(SendType.Message, channel, text2);
                            return;
                        case "setrank":
                            if (Player.Find(text2.Split(' ')[0]) != null)
                            {
                                Command.all.Find("setrank").Use(null, text2);
                                return;
                            }

                            irc.SendMessage(SendType.Message, e.Data.Nick, "Player not found.");
                            return;
                        case "mute":
                            if (Player.Find(text2) != null)
                            {
                                Command.all.Find("mute").Use(null, text2);
                                return;
                            }

                            irc.SendMessage(SendType.Message, e.Data.Nick, "Player not found.");
                            return;
                        case "joker":
                            if (Player.Find(text2) != null)
                            {
                                Command.all.Find("joker").Use(null, text2);
                                return;
                            }

                            irc.SendMessage(SendType.Message, e.Data.Nick, "Player not found.");
                            return;
                        case "physics":
                            if (Level.Find(text2.Split(' ')[0]) != null)
                            {
                                Command.all.Find("physics").Use(null, text2);
                                return;
                            }

                            irc.SendMessage(SendType.Message, e.Data.Nick, "Map not found.");
                            return;
                        case "load":
                            if (Level.Find(text2.Split(' ')[0]) != null)
                            {
                                Command.all.Find("load").Use(null, text2);
                                return;
                            }

                            irc.SendMessage(SendType.Message, e.Data.Nick, "Map not found.");
                            return;
                        case "unload":
                            if (Level.Find(text2) != null || text2 == "empty")
                            {
                                Command.all.Find("unload").Use(null, text2);
                                return;
                            }

                            irc.SendMessage(SendType.Message, e.Data.Nick, "Map not found.");
                            return;
                        case "save":
                            if (Level.Find(text2) != null)
                            {
                                Command.all.Find("save").Use(null, text2);
                                return;
                            }

                            irc.SendMessage(SendType.Message, e.Data.Nick, "Map not found.");
                            return;
                        case "map":
                            if (Level.Find(text2.Split(' ')[0]) != null)
                            {
                                Command.all.Find("map").Use(null, text2);
                                return;
                            }

                            irc.SendMessage(SendType.Message, e.Data.Nick, "Map not found.");
                            return;
                        case "restart":
                            Player.GlobalMessage("Restart initiated by " + e.Data.Nick);
                            Say("Restart initiated by " + e.Data.Nick);
                            Command.all.Find("restart").Use(null, "");
                            return;
                        case "update":
                            Player.GlobalMessage("Update check initiated by " + e.Data.Nick);
                            Say("Update check initiated by " + e.Data.Nick);
                            Command.all.Find("update").Use(null, "");
                            return;
                    }

                    irc.SendMessage(SendType.Message, e.Data.Nick, "Invalid command.");
                    return;
                }

                irc.SendMessage(SendType.Message, e.Data.Nick, "Invalid command format.");
            }
        }

        // Token: 0x06001670 RID: 5744 RVA: 0x00087644 File Offset: 0x00085844
        private void OnNickChange(object sender, NickChangeEventArgs e)
        {
            if (e.NewNickname.Split('|').Length == 2)
            {
                var text = e.NewNickname.Split('|')[1];
                string a;
                if (text != null && text != "" && (a = text) != null)
                {
                    if (!(a == "AFK"))
                    {
                        if (a == "Away")
                        {
                            Player.GlobalMessage(string.Concat("[IRC] ", Server.IRCColour, e.OldNickname,
                                Server.DefaultColor, " is Away"));
                            Server.afkset.Add(e.OldNickname);
                        }
                    }
                    else
                    {
                        Player.GlobalMessage(string.Concat("[IRC] ", Server.IRCColour, e.OldNickname,
                            Server.DefaultColor, " is AFK"));
                        Server.afkset.Add(e.OldNickname);
                    }
                }
            }
            else if (Server.afkset.Contains(e.NewNickname))
            {
                Player.GlobalMessage(string.Concat("[IRC] ", Server.IRCColour, e.NewNickname, Server.DefaultColor,
                    " is no longer away"));
                Server.afkset.Remove(e.NewNickname);
            }
            else
            {
                Player.GlobalMessage(string.Concat("[IRC] ", Server.IRCColour, e.OldNickname, Server.DefaultColor,
                    " is now known as ", e.NewNickname));
            }

            irc.RfcNames(channel);
            irc.RfcNames(opchannel);
        }

        // Token: 0x06001671 RID: 5745 RVA: 0x0008784C File Offset: 0x00085A4C
        private void OnAction(object sender, ActionEventArgs e)
        {
            Player.GlobalMessage("* " + e.Data.Nick + " " + e.ActionMessage);
        }

        // Token: 0x06001672 RID: 5746 RVA: 0x00087874 File Offset: 0x00085A74
        public static void Say(string msg)
        {
            Say(msg, false);
        }

        // Token: 0x06001673 RID: 5747 RVA: 0x00087880 File Offset: 0x00085A80
        public static void Say(string msg, bool opchat)
        {
            var regex = new Regex("%[0-9a-f]|&[0-9a-f]");
            var message = regex.Replace(msg, "");
            if (irc != null && irc.IsConnected && Server.irc)
            {
                if (!opchat)
                {
                    irc.SendMessage(SendType.Message, channel, message);
                    return;
                }

                irc.SendMessage(SendType.Message, opchannel, message);
            }
        }

        // Token: 0x06001674 RID: 5748 RVA: 0x000878E8 File Offset: 0x00085AE8
        public static bool IsConnected()
        {
            return irc.IsConnected;
        }

        // Token: 0x06001675 RID: 5749 RVA: 0x000878FC File Offset: 0x00085AFC
        public static void Reset()
        {
            if (irc.IsConnected) irc.Disconnect();
            ircThread = new Thread(delegate()
            {
                try
                {
                    irc.Connect(server, Server.ircPort);
                }
                catch (Exception ex)
                {
                    Server.s.Log("Error Connecting to IRC");
                    Server.s.Log(ex.ToString());
                }
            });
            ircThread.Start();
        }

        // Token: 0x06001676 RID: 5750 RVA: 0x00087950 File Offset: 0x00085B50
        public static string[] GetConnectedUsers()
        {
            return names;
        }

        // Token: 0x06001677 RID: 5751 RVA: 0x00087958 File Offset: 0x00085B58
        public static void ShutDown()
        {
            irc.Disconnect();
            ircThread.Abort();
        }
    }
}