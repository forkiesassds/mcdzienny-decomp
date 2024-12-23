using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using SuperSocket.SocketBase;
using SuperWebSocket;
using SuperWebSocket.Protocol;

namespace MCDzienny.RemoteAccess
{
    public class RemoteClient
    {

        static readonly ASCIIEncoding asci = new ASCIIEncoding();

        static DateTime lastAttempt = DateTime.MinValue;

        internal static List<RemoteClient> remoteClients = new List<RemoteClient>();

        readonly RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();

        readonly MD5CryptoServiceProvider md5crypto = new MD5CryptoServiceProvider();

        readonly SendManager sendManager;

        internal string clientType = "Unknown";

        internal byte[] generatedSalt;

        internal string ip;

        internal string login;

        internal byte[] partialBuffer = new byte[0];

        internal byte[] receiveBuffer = new byte[255];

        internal bool verified;

        internal WebSocketSession ws;

        public RemoteClient(WebSocketSession ws)
        {
            try
            {
                this.ws = ws;
                ip = ((AppSession<WebSocketSession, IWebSocketFragment>)(object)ws).RemoteEndPoint.Address.ToString();
                IsConnected = true;
                Console.WriteLine(string.Format("{0} attempts to get a remote access.",
                                                ((AppSession<WebSocketSession, IWebSocketFragment>)(object)ws).RemoteEndPoint.Address));
                sendManager = new SendManager(this, ws);
                if (Server.bannedRemoteAccounts.BannnedIPs.Contains(ip))
                {
                    Disconnect("Banned");
                }
                else if (lastAttempt.AddSeconds(5.0) > DateTime.Now)
                {
                    Disconnect("Wait 5 seconds and try again");
                }
                else
                {
                    lastAttempt = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                Disconnect();
                if (ex is SocketException)
                {
                    if (((SocketException)ex).ErrorCode != 10054)
                    {
                        Console.WriteLine(ex);
                    }
                }
                else
                {
                    Console.WriteLine(ex);
                }
            }
        }

        public bool IsConnected { get; private set; }

        public void HandleIncomingData(byte[] data)
        {
            HandleReceive.NewMessage(this, data);
        }

        public void SendSalt()
        {
            generatedSalt = new byte[16];
            crypto.GetBytes(generatedSalt);
            sendManager.SendSalt(generatedSalt);
        }

        public void Verify(string name, byte[] passCode)
        {
            if (!Server.remoteAccounts.Accounts.ContainsKey(name))
            {
                Server.s.Log("Unknown login.");
                Disconnect("Incorrect login or/and password.");
                return;
            }
            string text = Server.remoteAccounts.Accounts[name];
            if (BitConverter.ToString(md5crypto.ComputeHash(asci.GetBytes(BitConverter.ToString(generatedSalt) + text))).Replace("-", "").ToLower()
                    .TrimStart('0') != asci.GetString(passCode))
            {
                Server.s.Log("Incorrect password.");
                Disconnect("Incorrect login or/and password.");
                return;
            }
            login = name;
            verified = true;
            sendManager.SendSuccess();
            SendPlayers();
            SendMaps();
            remoteClients.Add(this);
            Server.s.Log(ip + " " + login + " - remote access granted!");
        }

        public void SendLog(string log)
        {
            if (log.Length > 250)
            {
                Server.s.Log("Remote message couldn't be send because it was too long.");
            }
            else
            {
                sendManager.SendLog(log);
            }
        }

        public void SendCommandLog(string log)
        {
            if (log.Length > 250)
            {
                Server.s.Log("Remote message couldn't be send because it was too long.");
            }
            else
            {
                sendManager.SendCommandLog(log);
            }
        }

        public void SendMaps()
        {
            StringBuilder stringBuilder = new StringBuilder();
            var maps = new Dictionary<string, List<string>>();
            Server.levels.ForEach(delegate(Level l)
            {
                if (!maps.ContainsKey(l.mapType.ToString()))
                {
                    maps.Add(l.mapType.ToString(), new List<string>
                    {
                        l.name
                    });
                }
                else
                {
                    maps[l.mapType.ToString()].Add(l.name);
                }
            });
            string[] names = Enum.GetNames(typeof(MapType));
            foreach (string text in names)
            {
                if (!maps.ContainsKey(text))
                {
                    continue;
                }
                stringBuilder.Append(text);
                stringBuilder.Append(":");
                foreach (string item in maps[text])
                {
                    stringBuilder.Append(item);
                    stringBuilder.Append(",");
                }
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
                stringBuilder.Append(";");
            }
            if (stringBuilder.Length != 0)
            {
                if (stringBuilder.Length > 1000)
                {
                    Server.s.Log("Remote message mapsList couldn't be send because it was too long.");
                }
                else
                {
                    sendManager.SendMaps(stringBuilder.ToString());
                }
            }
        }

        public void SendPlayers()
        {
            StringBuilder stringBuilder = new StringBuilder();
            var groupPlayer = new Dictionary<string, List<string>>();
            Player.players.ForEach(delegate(Player p)
            {
                if (!groupPlayer.ContainsKey(p.group.trueName))
                {
                    groupPlayer.Add(p.group.trueName, new List<string>
                    {
                        p.name
                    });
                }
                else
                {
                    groupPlayer[p.group.trueName].Add(p.name);
                }
            });
            for (int num = Group.groupList.Count - 1; num >= 0; num--)
            {
                if (groupPlayer.ContainsKey(Group.groupList[num].trueName))
                {
                    stringBuilder.Append(Group.groupList[num].trueName);
                    stringBuilder.Append(":");
                    foreach (string item in groupPlayer[Group.groupList[num].trueName])
                    {
                        stringBuilder.Append(item);
                        stringBuilder.Append(",");
                    }
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
                    stringBuilder.Append(";");
                }
            }
            if (stringBuilder.Length != 0)
            {
                if (stringBuilder.Length > 1000)
                {
                    Server.s.Log("Remote message playerList couldn't be send because it was too long.");
                }
                else
                {
                    sendManager.SendPlayers(stringBuilder.ToString());
                }
            }
        }

        public void SendLag(int lag)
        {
            sendManager.SendLag(lag);
        }

        public void SendTimes(int time1, int time2)
        {
            sendManager.SendTimes(time1, time2);
        }

        public void SendUptime(TimeSpan uptime)
        {
            sendManager.SendUptime(uptime);
        }

        public void SendInfoBundle()
        {
            sendManager.SendInfoBundle("maxPlayers=" + Server.players + "&maxMaps=" + Server.maps + "&serverName=" + Server.name);
        }

        public void HandleChat(string chat)
        {
            if (!ServerProperties.ValidString(chat, "%![]:.,{}~-+()?_/\\^*#@$~`\"'|=;<>& "))
            {
                Server.s.Log("Warning: Invalid character was detected in a remote message.");
            }
            else if (chat.Length > 1 && chat[0] == '#')
            {
                chat = chat.Remove(0, 1).Trim();
                Player.GlobalMessageOps("To Ops &f-" + Server.DefaultColor + Server.ConsoleRealName.Substring(0, Server.ConsoleRealName.Length - 1) + "&f- " + chat);
                Server.s.Log("(OPs): Console: " + chat);
                Player.IRCSay("Console: " + chat, opchat: true);
            }
            else if (chat.Length > 1 && chat[0] == '/')
            {
                string text = "";
                string text2 = "";
                chat = chat.Remove(0, 1).Trim();
                if (chat.Length != 0)
                {
                    if (chat.IndexOf(' ') != -1)
                    {
                        text = chat.Split(' ')[0];
                        text2 = chat.Substring(chat.IndexOf(' ') + 1);
                    }
                    else
                    {
                        if (!(chat != ""))
                        {
                            Server.s.Log("Warning: Empty command was sent by a remote client.");
                            return;
                        }
                        text = chat;
                    }
                    try
                    {
                        Command command = Command.all.Find(text);
                        if (command != null)
                        {
                            if (!command.ConsoleAccess)
                            {
                                Server.s.CommandUsed(string.Format("You can't use {0} command from console.", text));
                                return;
                            }
                            command.Use(null, text2);
                            if (command.HighSecurity)
                            {
                                Server.s.CommandUsed("REMOTE CONSOLE: USED /" + text + " ***");
                            }
                            else
                            {
                                Server.s.CommandUsed("REMOTE CONSOLE: USED /" + text + " " + text2);
                            }
                        }
                        if (command == null)
                        {
                            Server.s.CommandUsed("REMOTE CONSOLE: Command  '/" + text + "'  does not exist.");
                        }
                        return;
                    }
                    catch (Exception ex)
                    {
                        Server.ErrorLog(ex);
                        Server.s.CommandUsed("REMOTE CONSOLE: Failed command.");
                        return;
                    }
                }
                Server.s.Log("Warning: Empty command was sent by a remote client.");
            }
            else
            {
                Player.GlobalMessage(Server.ConsoleRealName + chat);
                Player.IRCSay(Server.ConsoleRealNameIRC + chat);
                Server.s.Log("<R.CONSOLE> " + chat);
            }
        }

        public void AddPlayer(Player player)
        {
            sendManager.AddPlayer(player.name + ":" + player.group.name);
        }

        public void RemovePlayer(Player player)
        {
            sendManager.RemovePlayer(player.name);
        }

        public void AddMap(Level level)
        {
            sendManager.AddMap(level.name + ":" + level.mapType);
        }

        public void RemoveMap(Level level)
        {
            sendManager.RemoveMap(level.name);
        }

        public void Disconnect(string reason)
        {
            if (!IsConnected)
            {
                return;
            }
            verified = false;
            remoteClients.Remove(this);
            if (reason.Length > 64)
            {
                reason = reason.Substring(0, 64);
            }
            try
            {
                sendManager.SendDisconnect(reason);
                Server.s.Log(ip + " disconnected from remote console." + (reason.Length > 0 ? " Reason: " + reason : ""));
                IsConnected = false;
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        public void Disconnect()
        {
            if (!IsConnected)
            {
                return;
            }
            try
            {
                verified = false;
                remoteClients.Remove(this);
                IsConnected = false;
                ((AppSession<WebSocketSession, IWebSocketFragment>)(object)ws).Close();
            }
            catch (Exception value)
            {
                Console.WriteLine(value);
            }
        }
    }
}