using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Timers;
using MCDzienny.Settings;

namespace MCDzienny
{
    public static class Heartbeat
    {
        static string hash;

        public static string serverURL;

        static string staticVars;

        static string mcdziennyMessage;

        static HttpWebRequest request;

        static StreamWriter beatlogger;

        static Random Seed;

        static readonly int minecraftHeartbeatDelay;

        static readonly Timer heartbeatTimer;

        static readonly Timer mcdziennyTimer;

        static readonly Timer classiCube;

        public static char[] reservedChars;

        static bool firstBeat;

        static Heartbeat()
        {
            Seed = new Random();
            minecraftHeartbeatDelay = 25000;
            mcdziennyTimer = new Timer(240000.0);
            classiCube = new Timer(40000.0);
            reservedChars = new char[20]
            {
                ' ', '!', '*', '\'', '(', ')', ';', ':', '@', '&', '=', '+', '$', ',', '/', '?', '%', '#', '[', ']'
            };
            firstBeat = true;
            heartbeatTimer = new Timer(minecraftHeartbeatDelay);
        }

        public static void Init()
        {
            if (Server.logbeat && !File.Exists("heartbeat.log"))
            {
                File.Create("heartbeat.log").Close();
            }
            staticVars = "version=" + (byte)7;
            mcdziennyMessage = staticVars + "&serverversion=" + Server.Version + "&sqlite=" + !Server.useMySQL + "&chkport=" + true;
            Pump(Beat.Minecraft);
            heartbeatTimer.AutoReset = false;
            heartbeatTimer.Elapsed += delegate
            {
                try
                {
                    Pump(Beat.Minecraft);
                }
                catch (Exception ex)
                {
                    Server.ErrorLog(ex);
                }
                heartbeatTimer.Start();
            };
            heartbeatTimer.Start();
            mcdziennyTimer.Elapsed += delegate
            {
                try
                {
                    Pump(Beat.MCDzienny);
                }
                catch (Exception ex2)
                {
                    Server.ErrorLog(ex2);
                }
            };
            mcdziennyTimer.Start();
            classiCube.Elapsed += delegate
            {
                try
                {
                    if (GeneralSettings.All.AllowAndListOnClassiCube)
                    {
                        Pump(Beat.ClassiCube);
                    }
                }
                catch (Exception ex3)
                {
                    Server.ErrorLog(ex3);
                }
            };
            classiCube.Start();
        }

        public static bool Pump(Beat type)
        {
            string text = "";
            text += staticVars;
            string uriString = "https://minecraft.net/heartbeat.jsp";
            int num = 0;
            try
            {
                int num2 = 0;
                num++;
                switch (type)
                {
                    case Beat.Minecraft:
                    {
                        if (Server.logbeat)
                        {
                            beatlogger = new StreamWriter("heartbeat.log", append: true);
                        }
                        StringBuilder stringBuilder2 = new StringBuilder();
                        stringBuilder2.AppendFormat("version={0}&port={1}&max={2}&name={3}&salt={4}&users={5}&public={6}", (byte)7, Server.port, Server.players,
                                                    UrlEncode(Server.name), Server.Salt, Player.number - num2, Server.isPublic);
                        text = stringBuilder2.ToString();
                        break;
                    }
                    case Beat.ClassiCube:
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        stringBuilder.AppendFormat("public={0}&max={1}&users={2}&port={3}&version={4}&salt={5}&name={6}&software=MCDzienny", Server.isPublic,
                                                   Server.players, Player.number - num2, Server.port, (byte)7, Server.SaltClassiCube, UrlEncode(Server.name));
                        text = stringBuilder.ToString();
                        uriString = "http://www.classicube.net/server/heartbeat";
                        break;
                    }
                    case Beat.MCDzienny:
                        if (hash == null)
                        {
                            return false;
                        }
                        text = mcdziennyMessage + "&pcount=" + Player.players.Count + "&hash=" + hash + "&uvisits=" + Server.UniqueVisits + "&mode=" + (int)Server.mode +
                            "&port=" + Server.port + "&max=" + Server.players + "&name=" + UrlEncode(Server.name) + "&public=" + Server.isPublic + "&uptime=" +
                            (int)DateTime.Now.Subtract(Server.TimeOnline).TotalMinutes + "&rport=" + (RemoteSettings.All.ShowInBrowser ? RemoteSettings.All.Port : -1);
                        uriString = "http://mcdzienny.cba.pl/heartbeat.php";
                        break;
                    case Beat.WOM:
                    {
                        object obj = text;
                        text = string.Concat(obj, "&port=", Server.port, "&max=", Server.players, "&name=", UrlEncode(Server.name), "&public=", Server.isPublic, "&salt=",
                                             Server.Salt, "&users=", Player.number, "&alt=", UrlEncode(Server.name), "&desc=", UrlEncode(Server.description), "&flags=",
                                             UrlEncode(Server.Flag));
                        uriString = "http://direct.worldofminecraft.com/hb.php";
                        break;
                    }
                }
                request = (HttpWebRequest)WebRequest.Create(new Uri(uriString));
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
                byte[] bytes = Encoding.ASCII.GetBytes(text);
                request.ContentLength = bytes.Length;
                request.Timeout = 10000;
                try
                {
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(bytes, 0, bytes.Length);
                        if (type == Beat.Minecraft && Server.logbeat)
                        {
                            beatlogger.WriteLine("Request sent at " + DateTime.Now);
                        }
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.Timeout)
                    {
                        if (type == Beat.Minecraft && Server.logbeat)
                        {
                            beatlogger.WriteLine("Timeout detected at " + DateTime.Now);
                        }
                    }
                    else if (type == Beat.Minecraft && Server.logbeat)
                    {
                        beatlogger.WriteLine("Non-timeout exception detected: " + ex.Message);
                        beatlogger.Write("Stack Trace: " + ex.StackTrace);
                    }
                }
                if (type == Beat.Minecraft)
                {
                    try
                    {
                        using (WebResponse webResponse = request.GetResponse())
                        {
                            using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream()))
                            {
                                string text2 = streamReader.ReadToEnd().Trim();
                                if (hash == null)
                                {
                                    if (type == Beat.Minecraft && Server.logbeat)
                                    {
                                        beatlogger.WriteLine("Response received at " + DateTime.Now);
                                        beatlogger.WriteLine("Received: " + text2);
                                    }
                                    hash = text2.Substring(text2.LastIndexOf('=') + 1);
                                    Server.ServerHash = hash;
                                    serverURL = text2;
                                    Server.s.UpdateUrl(serverURL);
                                    File.WriteAllText("text/externalurl.txt", text2);
                                    Server.s.Log("URL found: " + text2);
                                    Pump(Beat.MCDzienny);
                                }
                                else if (type == Beat.Minecraft && Server.logbeat)
                                {
                                    beatlogger.WriteLine("Response received at " + DateTime.Now);
                                }
                            }
                        }
                    }
                    catch (Exception) {}
                }
                if (type == Beat.ClassiCube)
                {
                    try
                    {
                        using (WebResponse webResponse2 = request.GetResponse())
                        {
                            using (StreamReader streamReader2 = new StreamReader(webResponse2.GetResponseStream()))
                            {
                                if (firstBeat)
                                {
                                    firstBeat = false;
                                    string text3 = streamReader2.ReadToEnd().Trim();
                                    Server.s.Log("ClassiCube URL found: " + text3);
                                }
                            }
                        }
                    }
                    catch (Exception) {}
                }
            }
            catch (WebException ex4)
            {
                if (ex4.Status == WebExceptionStatus.Timeout && type == Beat.Minecraft && Server.logbeat)
                {
                    beatlogger.WriteLine("Timeout detected at " + DateTime.Now);
                }
                if (type == Beat.Minecraft && hash == null)
                {
                    Server.s.Log("Warninig: Couldn't get response from minecraft.net website. Retrying...");
                }
            }
            catch
            {
                if (type == Beat.Minecraft && Server.logbeat)
                {
                    beatlogger.WriteLine("Heartbeat failure #" + num + " at " + DateTime.Now);
                }
                if (type == Beat.Minecraft && Server.logbeat)
                {
                    beatlogger.WriteLine("Failed three times.  Stopping.");
                    beatlogger.Close();
                }
                return false;
            }
            finally
            {
                request.Abort();
            }
            if (beatlogger != null)
            {
                beatlogger.Close();
            }
            return true;
        }

        public static string UrlEncode(string input)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] >= '0' && input[i] <= '9' || input[i] >= 'a' && input[i] <= 'z' || input[i] >= 'A' && input[i] <= 'Z' || input[i] == '-' || input[i] == '_' ||
                    input[i] == '.' || input[i] == '~')
                {
                    stringBuilder.Append(input[i]);
                }
                else if (Array.IndexOf(reservedChars, input[i]) != -1)
                {
                    stringBuilder.Append('%').Append(((int)input[i]).ToString("X"));
                }
            }
            return stringBuilder.ToString();
        }
    }
}