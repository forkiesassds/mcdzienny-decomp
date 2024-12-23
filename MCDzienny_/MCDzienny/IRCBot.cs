using System;
using System.Text.RegularExpressions;
using System.Threading;
using Meebey.SmartIrc4net;

namespace MCDzienny
{
    class IRCBot
    {
        static IrcClient irc = new IrcClient();

        static readonly string server = Server.ircServer;

        static readonly string channel = Server.ircChannel;

        static readonly string opchannel = Server.ircOpChannel;

        static readonly string nick = Server.ircNick;

        static Thread ircThread;

        static string[] names;

        public IRCBot()
        {
            ThreadStart start = delegate
            {
                //IL_0038: Unknown result type (might be due to invalid IL or missing references)
                //IL_0042: Expected O, but got Unknown
                //IL_004e: Unknown result type (might be due to invalid IL or missing references)
                //IL_0058: Expected O, but got Unknown
                //IL_0064: Unknown result type (might be due to invalid IL or missing references)
                //IL_006e: Expected O, but got Unknown
                //IL_007a: Unknown result type (might be due to invalid IL or missing references)
                //IL_0084: Expected O, but got Unknown
                //IL_0090: Unknown result type (might be due to invalid IL or missing references)
                //IL_009a: Expected O, but got Unknown
                //IL_00bc: Unknown result type (might be due to invalid IL or missing references)
                //IL_00c6: Expected O, but got Unknown
                //IL_00d2: Unknown result type (might be due to invalid IL or missing references)
                //IL_00dc: Expected O, but got Unknown
                //IL_00e8: Unknown result type (might be due to invalid IL or missing references)
                //IL_00f2: Expected O, but got Unknown
                irc.OnConnecting += OnConnecting;
                irc.OnConnected += OnConnected;
                irc.OnChannelMessage += new IrcEventHandler(OnChanMessage);
                irc.OnJoin += new JoinEventHandler(OnJoin);
                irc.OnPart += new PartEventHandler(OnPart);
                irc.OnQuit += new QuitEventHandler(OnQuit);
                irc.OnNickChange += new NickChangeEventHandler(OnNickChange);
                irc.OnDisconnected += OnDisconnected;
                irc.OnQueryMessage += new IrcEventHandler(OnPrivMsg);
                irc.OnNames += new NamesEventHandler(OnNames);
                irc.OnChannelAction += new ActionEventHandler(OnAction);
                try
                {
                    irc.Connect(server, Server.ircPort);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to connect to IRC server: {0}", ex.Message);
                }
            };
            ircThread = new Thread(start);
            ircThread.IsBackground = true;
            ircThread.Name = "Irc Thread";
            ircThread.Start();
        }

        void OnConnecting(object sender, EventArgs e)
        {
            Server.s.Log("Connecting to IRC");
        }

        void OnConnected(object sender, EventArgs e)
        {
            Server.s.Log("Connected to IRC");
            irc.Login(nick, nick, 0, nick);
            if (Server.ircIdentify && Server.ircPassword != string.Empty)
            {
                Server.s.Log("Identifying with Nickserv");
                irc.SendMessage(0, "nickserv", "IDENTIFY " + Server.ircPassword);
            }
            Server.s.Log("Joining channels");
            irc.RfcJoin(channel);
            irc.RfcJoin(opchannel);
            irc.Listen();
        }

        void OnNames(object sender, NamesEventArgs e)
        {
            names = e.UserList;
        }

        void OnDisconnected(object sender, EventArgs e)
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

        void OnChanMessage(object sender, IrcEventArgs e)
        {
            string text = e.Data.Message;
            string text2 = e.Data.Nick;
            string text3 = "1234567890-=qwertyuiop[]\\asdfghjkl;'zxcvbnm,./!@#$%^*()_+QWERTYUIOPASDFGHJKL:\"ZXCVBNM<>? ";
            string text4 = text;
            for (int i = 0; i < text4.Length; i++)
            {
                char value = text4[i];
                if (text3.IndexOf(value) == -1)
                {
                    text = text.Replace(value.ToString(), "*");
                }
            }
            if (e.Data.Channel == opchannel)
            {
                Server.s.Log("[(Op) IRC] " + e.Data.Nick + ": " + text);
                Player.GlobalMessageOps(Server.IRCColour + "[(Op) IRC] " + text2 + ": &f" + text);
            }
            else
            {
                Server.s.Log("[IRC] " + e.Data.Nick + ": " + text);
                Player.GlobalChat(null, Server.IRCColour + "[IRC] " + text2 + ": &f" + text, showname: false);
            }
        }

        void OnJoin(object sender, JoinEventArgs e)
        {
            Server.s.Log(e.Data.Nick + " has joined channel " + e.Data.Channel);
            if (e.Data.Channel == opchannel)
            {
                Player.GlobalChat(null, Server.IRCColour + e.Data.Nick + Server.DefaultColor + " has joined the operator channel", showname: false);
            }
            else
            {
                Player.GlobalChat(null, Server.IRCColour + e.Data.Nick + Server.DefaultColor + " has joined the channel", showname: false);
            }
            irc.RfcNames(channel);
            irc.RfcNames(opchannel);
        }

        void OnPart(object sender, PartEventArgs e)
        {
            Server.s.Log(e.Data.Nick + " has left channel " + e.Data.Channel);
            if (e.Data.Channel == opchannel)
            {
                Player.GlobalChat(null, Server.IRCColour + e.Data.Nick + Server.DefaultColor + " has left the operator channel", showname: false);
            }
            else
            {
                Player.GlobalChat(null, Server.IRCColour + e.Data.Nick + Server.DefaultColor + " has left the channel", showname: false);
            }
            irc.RfcNames(channel);
            irc.RfcNames(opchannel);
        }

        void OnQuit(object sender, QuitEventArgs e)
        {
            Server.s.Log(e.Data.Nick + " has left IRC");
            Player.GlobalChat(null, Server.IRCColour + e.Data.Nick + Server.DefaultColor + " has left IRC", showname: false);
            irc.RfcNames(channel);
            irc.RfcNames(opchannel);
        }

        void OnPrivMsg(object sender, IrcEventArgs e)
        {
            Server.s.Log("IRC RECEIVING MESSAGE");
            if (!Server.ircControllers.Contains(e.Data.Nick))
            {
                return;
            }
            int num = e.Data.Message.Split(new char[1]
            {
                ' '
            }).Length;
            string text = e.Data.Message.Split(new char[1]
            {
                ' '
            })[0];
            string text2 = num <= 1 ? "" : e.Data.Message.Substring(e.Data.Message.IndexOf(' ')).Trim();
            if (text2 != "" || text == "restart" || text == "update")
            {
                Server.s.Log(text + " : " + text2);
                switch (text)
                {
                    case "kick":
                        if (Player.Find(text2.Split()[0]) != null)
                        {
                            Command.all.Find("kick").Use(null, text2);
                        }
                        else
                        {
                            irc.SendMessage(0, e.Data.Nick, "Player not found.");
                        }
                        break;
                    case "ban":
                        if (Player.Find(text2) != null)
                        {
                            Command.all.Find("ban").Use(null, text2);
                        }
                        else
                        {
                            irc.SendMessage(0, e.Data.Nick, "Player not found.");
                        }
                        break;
                    case "banip":
                        if (Player.Find(text2) != null)
                        {
                            Command.all.Find("banip").Use(null, text2);
                        }
                        else
                        {
                            irc.SendMessage(0, e.Data.Nick, "Player not found.");
                        }
                        break;
                    case "say":
                        irc.SendMessage(0, channel, text2);
                        break;
                    case "setrank":
                        if (Player.Find(text2.Split(' ')[0]) != null)
                        {
                            Command.all.Find("setrank").Use(null, text2);
                        }
                        else
                        {
                            irc.SendMessage(0, e.Data.Nick, "Player not found.");
                        }
                        break;
                    case "mute":
                        if (Player.Find(text2) != null)
                        {
                            Command.all.Find("mute").Use(null, text2);
                        }
                        else
                        {
                            irc.SendMessage(0, e.Data.Nick, "Player not found.");
                        }
                        break;
                    case "joker":
                        if (Player.Find(text2) != null)
                        {
                            Command.all.Find("joker").Use(null, text2);
                        }
                        else
                        {
                            irc.SendMessage(0, e.Data.Nick, "Player not found.");
                        }
                        break;
                    case "physics":
                        if (Level.Find(text2.Split(' ')[0]) != null)
                        {
                            Command.all.Find("physics").Use(null, text2);
                        }
                        else
                        {
                            irc.SendMessage(0, e.Data.Nick, "Map not found.");
                        }
                        break;
                    case "load":
                        if (Level.Find(text2.Split(' ')[0]) != null)
                        {
                            Command.all.Find("load").Use(null, text2);
                        }
                        else
                        {
                            irc.SendMessage(0, e.Data.Nick, "Map not found.");
                        }
                        break;
                    case "unload":
                        if (Level.Find(text2) != null || text2 == "empty")
                        {
                            Command.all.Find("unload").Use(null, text2);
                        }
                        else
                        {
                            irc.SendMessage(0, e.Data.Nick, "Map not found.");
                        }
                        break;
                    case "save":
                        if (Level.Find(text2) != null)
                        {
                            Command.all.Find("save").Use(null, text2);
                        }
                        else
                        {
                            irc.SendMessage(0, e.Data.Nick, "Map not found.");
                        }
                        break;
                    case "map":
                        if (Level.Find(text2.Split(' ')[0]) != null)
                        {
                            Command.all.Find("map").Use(null, text2);
                        }
                        else
                        {
                            irc.SendMessage(0, e.Data.Nick, "Map not found.");
                        }
                        break;
                    case "restart":
                        Player.GlobalMessage("Restart initiated by " + e.Data.Nick);
                        Say("Restart initiated by " + e.Data.Nick);
                        Command.all.Find("restart").Use(null, "");
                        break;
                    case "update":
                        Player.GlobalMessage("Update check initiated by " + e.Data.Nick);
                        Say("Update check initiated by " + e.Data.Nick);
                        Command.all.Find("update").Use(null, "");
                        break;
                    default:
                        irc.SendMessage(0, e.Data.Nick, "Invalid command.");
                        break;
                }
            }
            else
            {
                irc.SendMessage(0, e.Data.Nick, "Invalid command format.");
            }
        }

        void OnNickChange(object sender, NickChangeEventArgs e)
        {
            if (e.NewNickname.Split(new char[1]
                {
                    '|'
                }).Length == 2)
            {
                string text = e.NewNickname.Split(new char[1]
                {
                    '|'
                })[1];
                if (text != null && text != "")
                {
                    switch (text)
                    {
                        case "AFK":
                            Player.GlobalMessage("[IRC] " + Server.IRCColour + e.OldNickname + Server.DefaultColor + " is AFK");
                            Server.afkset.Add(e.OldNickname);
                            break;
                        case "Away":
                            Player.GlobalMessage("[IRC] " + Server.IRCColour + e.OldNickname + Server.DefaultColor + " is Away");
                            Server.afkset.Add(e.OldNickname);
                            break;
                    }
                }
            }
            else if (Server.afkset.Contains(e.NewNickname))
            {
                Player.GlobalMessage("[IRC] " + Server.IRCColour + e.NewNickname + Server.DefaultColor + " is no longer away");
                Server.afkset.Remove(e.NewNickname);
            }
            else
            {
                Player.GlobalMessage("[IRC] " + Server.IRCColour + e.OldNickname + Server.DefaultColor + " is now known as " + e.NewNickname);
            }
            irc.RfcNames(channel);
            irc.RfcNames(opchannel);
        }

        void OnAction(object sender, ActionEventArgs e)
        {
            Player.GlobalMessage("* " + e.Data.Nick + " " + e.ActionMessage);
        }

        public static void Say(string msg)
        {
            Say(msg, opchat: false);
        }

        public static void Say(string msg, bool opchat)
        {
            Regex regex = new Regex("%[0-9a-f]|&[0-9a-f]");
            string text = regex.Replace(msg, "");
            if (irc != null && irc.IsConnected && Server.irc)
            {
                if (!opchat)
                {
                    irc.SendMessage(0, channel, text);
                }
                else
                {
                    irc.SendMessage(0, opchannel, text);
                }
            }
        }

        public static bool IsConnected()
        {
            if (irc.IsConnected)
            {
                return true;
            }
            return false;
        }

        public static void Reset()
        {
            if (irc.IsConnected)
            {
                irc.Disconnect();
            }
            ircThread = new Thread((ThreadStart)delegate
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

        public static string[] GetConnectedUsers()
        {
            return names;
        }

        public static void ShutDown()
        {
            irc.Disconnect();
            ircThread.Abort();
        }
    }
}