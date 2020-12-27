using System;
using System.IO;
using MCDzienny.Settings;

namespace MCDzienny
{
    // Token: 0x0200035E RID: 862
    public static class ServerProperties
    {
        // Token: 0x04000CEA RID: 3306
        private static string lastPath;

        // Token: 0x060018B4 RID: 6324 RVA: 0x000A78B8 File Offset: 0x000A5AB8
        public static void Load(string givenPath, bool skipsalt = false)
        {
            lastPath = givenPath;
            if (!skipsalt) Server.salt = "";
            if (File.Exists(givenPath))
            {
                var array = File.ReadAllLines(givenPath);
                var array2 = array;
                foreach (var text in array2)
                {
                    if (!(text != "") || text[0] == '#') continue;
                    var text2 = text.Split('=')[0].Trim();
                    var text3 = text.Substring(text.IndexOf('=') + 1).Trim();
                    var text4 = "";
                    switch (text2.ToLower())
                    {
                        case "server-name":
                            if (ValidString(text3, "![]:.,{}~-+()?_/\\ "))
                                Server.name = text3;
                            else
                                Server.s.Log("server-name invalid! setting to default.");
                            break;
                        case "motd":
                            if (ValidString(text3, "![]&:.,{}~-+()?_/\\= "))
                                Server.motd = text3;
                            else
                                Server.s.Log("motd invalid! setting to default.");
                            break;
                        case "port":
                            try
                            {
                                Server.port = Convert.ToInt32(text3);
                            }
                            catch
                            {
                                Server.s.Log("port invalid! setting to default.");
                            }

                            break;
                        case "verify-names-security":
                            Server.verify = text3.ToLower() == "true" ? true : false;
                            break;
                        case "public":
                            Server.isPublic = text3.ToLower() == "true" ? true : false;
                            break;
                        case "world-chat":
                            Server.worldChat = text3.ToLower() == "true" ? true : false;
                            break;
                        case "guest-goto":
                            Server.guestGoto = text3.ToLower() == "true" ? true : false;
                            break;
                        case "max-players":
                            try
                            {
                                if (Convert.ToByte(text3) > byte.MaxValue)
                                {
                                    text3 = "128";
                                    Server.s.Log("Max players has been lowered to 255.");
                                }
                                else if (Convert.ToByte(text3) < 0)
                                {
                                    text3 = "0";
                                    Server.s.Log("Max players has been set to 0.");
                                }

                                Server.players = Convert.ToByte(text3);
                            }
                            catch
                            {
                                Server.s.Log("max-players invalid! setting to default.");
                            }

                            break;
                        case "max-maps":
                            try
                            {
                                if (Convert.ToByte(text3) > 100)
                                {
                                    text3 = "100";
                                    Server.s.Log("Max maps has been lowered to 100.");
                                }
                                else if (Convert.ToByte(text3) < 1)
                                {
                                    text3 = "1";
                                    Server.s.Log("Max maps has been increased to 1.");
                                }

                                Server.maps = Convert.ToByte(text3);
                            }
                            catch
                            {
                                Server.s.Log("max-maps invalid! setting to default.");
                            }

                            break;
                        case "irc-use":
                            Server.irc = text3.ToLower() == "true" ? true : false;
                            break;
                        case "irc-server":
                            Server.ircServer = text3;
                            break;
                        case "irc-nick":
                            Server.ircNick = text3;
                            break;
                        case "irc-channel":
                            Server.ircChannel = text3;
                            break;
                        case "irc-opchannel":
                            Server.ircOpChannel = text3;
                            break;
                        case "irc-port":
                            try
                            {
                                Server.ircPort = Convert.ToInt32(text3);
                            }
                            catch
                            {
                                Server.s.Log("irc-port invalid! setting to default.");
                            }

                            break;
                        case "irc-identify":
                            try
                            {
                                Server.ircIdentify = Convert.ToBoolean(text3);
                            }
                            catch
                            {
                                Server.s.Log("irc-identify boolean value invalid! Setting to the default of: " +
                                             Server.ircIdentify + ".");
                            }

                            break;
                        case "irc-password":
                            Server.ircPassword = text3;
                            break;
                        case "anti-tunnels":
                            Server.antiTunnel = text3.ToLower() == "true" ? true : false;
                            break;
                        case "max-depth":
                            try
                            {
                                Server.maxDepth = Convert.ToByte(text3);
                            }
                            catch
                            {
                                Server.s.Log("maxDepth invalid! setting to default.");
                            }

                            break;
                        case "rplimit":
                            try
                            {
                                Server.rpLimit = Convert.ToInt16(text3);
                            }
                            catch
                            {
                                Server.s.Log("rpLimit invalid! setting to default.");
                            }

                            break;
                        case "rplimit-norm":
                            try
                            {
                                Server.rpNormLimit = Convert.ToInt16(text3);
                            }
                            catch
                            {
                                Server.s.Log("rpLimit-norm invalid! setting to default.");
                            }

                            break;
                        case "report-back":
                            Server.reportBack = text3.ToLower() == "true" ? true : false;
                            break;
                        case "backup-time":
                            if (Convert.ToInt32(text3) > 1) Server.backupInterval = Convert.ToInt32(text3);
                            break;
                        case "backup-location":
                            try
                            {
                                if (Directory.Exists(text3)) Server.backupLocation = text3;
                            }
                            catch
                            {
                            }

                            break;
                        case "console-only":
                            Server.console = text3.ToLower() == "true" ? true : false;
                            break;
                        case "physicsrestart":
                            Server.physicsRestart = text3.ToLower() == "true" ? true : false;
                            break;
                        case "deathcount":
                            Server.deathcount = text3.ToLower() == "true" ? true : false;
                            break;
                        case "usemysql":
                            Server.useMySQL = text3.ToLower() == "true" ? true : false;
                            break;
                        case "host":
                            Server.MySQLHost = text3;
                            break;
                        case "sqlport":
                            Server.MySQLPort = text3;
                            break;
                        case "username":
                            Server.MySQLUsername = text3;
                            break;
                        case "password":
                            Server.MySQLPassword = text3;
                            break;
                        case "databasename":
                            Server.MySQLDatabaseName = text3;
                            break;
                        case "pooling":
                            try
                            {
                                Server.MySQLPooling = bool.Parse(text3);
                            }
                            catch
                            {
                                Server.s.Log("Invalid " + text2 + ". Using default.");
                            }

                            break;
                        case "defaultcolor":
                            text4 = c.Parse(text3);
                            if (text4 == "")
                            {
                                text4 = c.Name(text3);
                                if (!(text4 != ""))
                                {
                                    Server.s.Log("Could not find " + text3);
                                    return;
                                }

                                text4 = text3;
                            }

                            Server.DefaultColor = text4;
                            break;
                        case "irc-color":
                            text4 = c.Parse(text3);
                            if (text4 == "")
                            {
                                text4 = c.Name(text3);
                                if (!(text4 != ""))
                                {
                                    Server.s.Log("Could not find " + text3);
                                    return;
                                }

                                text4 = text3;
                            }

                            Server.IRCColour = text4;
                            break;
                        case "old-help":
                            try
                            {
                                Server.oldHelp = bool.Parse(text3);
                            }
                            catch
                            {
                                Server.s.Log("Invalid " + text2 + ". Using default.");
                            }

                            break;
                        case "opchat-perm":
                            try
                            {
                                var b = sbyte.Parse(text3);
                                if (b < -50 || b > 120) throw new FormatException();
                                Server.opchatperm = (LevelPermission) b;
                            }
                            catch
                            {
                                Server.s.Log("Invalid " + text2 + ".  Using default.");
                            }

                            break;
                        case "log-heartbeat":
                            try
                            {
                                Server.logbeat = bool.Parse(text3);
                            }
                            catch
                            {
                                Server.s.Log("Invalid " + text2 + ".  Using default.");
                            }

                            break;
                        case "force-cuboid":
                            try
                            {
                                Server.forceCuboid = bool.Parse(text3);
                            }
                            catch
                            {
                                Server.s.Log("Invalid " + text2 + ".  Using default.");
                            }

                            break;
                        case "cheapmessage":
                            try
                            {
                                Server.cheapMessage = bool.Parse(text3);
                            }
                            catch
                            {
                                Server.s.Log("Invalid " + text2 + ". Using default.");
                            }

                            break;
                        case "cheap-message-given":
                            if (text3 != "") Server.cheapMessageGiven = text3;
                            break;
                        case "custom-ban":
                            try
                            {
                                Server.customBan = bool.Parse(text3);
                            }
                            catch
                            {
                                Server.s.Log("Invalid " + text2 + ". Using default.");
                            }

                            break;
                        case "custom-ban-message":
                            if (text3 != "") Server.customBanMessage = text3;
                            break;
                        case "custom-shutdown":
                            try
                            {
                                Server.customShutdown = bool.Parse(text3);
                            }
                            catch
                            {
                                Server.s.Log("Invalid " + text2 + ". Using default.");
                            }

                            break;
                        case "custom-shutdown-message":
                            if (text3 != "") Server.customShutdownMessage = text3;
                            break;
                        case "rank-super":
                            try
                            {
                                Server.rankSuper = bool.Parse(text3);
                            }
                            catch
                            {
                                Server.s.Log("Invalid " + text2 + ". Using default.");
                            }

                            break;
                        case "default-rank":
                            try
                            {
                                Server.defaultRank = text3.ToLower();
                            }
                            catch
                            {
                            }

                            break;
                        case "afk-minutes":
                            try
                            {
                                Server.afkminutes = Convert.ToInt32(text3);
                            }
                            catch
                            {
                                Server.s.Log("irc-port invalid! setting to default.");
                            }

                            break;
                        case "afk-kick":
                            try
                            {
                                Server.afkkick = Convert.ToInt32(text3);
                            }
                            catch
                            {
                                Server.s.Log("irc-port invalid! setting to default.");
                            }

                            break;
                        case "check-updates":
                            try
                            {
                                Server.checkUpdates = bool.Parse(text3);
                            }
                            catch
                            {
                                Server.s.Log("Invalid " + text2 + ". Using default.");
                            }

                            break;
                        case "autoload":
                            try
                            {
                                Server.AutoLoad = bool.Parse(text3);
                            }
                            catch
                            {
                                Server.s.Log("Invalid " + text2 + ". Using default.");
                            }

                            break;
                        case "auto-restart":
                            try
                            {
                                Server.autorestart = bool.Parse(text3);
                            }
                            catch
                            {
                                Server.s.Log("Invalid " + text2 + ". Using default.");
                            }

                            break;
                        case "restarttime":
                            try
                            {
                                Server.restarttime = DateTime.Parse(text3);
                            }
                            catch
                            {
                                Server.s.Log("Invalid " + text2 + ". Using defualt.");
                            }

                            break;
                        case "parse-emotes":
                            try
                            {
                                Server.parseSmiley = bool.Parse(text3);
                            }
                            catch
                            {
                                Server.s.Log("Invalid " + text2 + ". Using default.");
                            }

                            break;
                        case "use-whitelist":
                            Server.useWhitelist = text3.ToLower() == "true" ? true : false;
                            break;
                        case "main-name":
                            if (Player.ValidName(text3))
                                Server.level = text3;
                            else
                                Server.s.Log("Invalid main name");
                            break;
                        case "dollar-before-dollar":
                            try
                            {
                                Server.useDollarSign = bool.Parse(text3);
                            }
                            catch
                            {
                                Server.s.Log("Invalid " + text2 + ". Using default.");
                            }

                            break;
                        case "money-name":
                            if (text3 != "") Server.moneys = text3;
                            break;
                        case "restart-on-error":
                            try
                            {
                                Server.restartOnError = bool.Parse(text3);
                            }
                            catch
                            {
                                Server.s.Log("Invalid " + text2 + ". Using default.");
                            }

                            break;
                        case "repeat-messages":
                            try
                            {
                                Server.repeatMessage = bool.Parse(text3);
                            }
                            catch
                            {
                                Server.s.Log("Invalid " + text2 + ". Using default.");
                            }

                            break;
                        case "host-state":
                            if (text3 != "") Server.ConsoleName = text3;
                            break;
                        case "lava-state":
                            switch (text3.ToLower())
                            {
                                case "calm":
                                    LavaSettings.All.LavaState = LavaState.Calm;
                                    break;
                                case "disturbed":
                                    LavaSettings.All.LavaState = LavaState.Disturbed;
                                    break;
                                case "furious":
                                    LavaSettings.All.LavaState = LavaState.Furious;
                                    break;
                                case "wild":
                                    LavaSettings.All.LavaState = LavaState.Wild;
                                    break;
                                default:
                                    Server.s.Log("Invalid lava-state parameter. Using default.");
                                    break;
                            }

                            break;
                        case "global-time-before":
                            try
                            {
                                LavaSystem.stime = int.Parse(text3);
                            }
                            catch
                            {
                                Server.s.Log("Invalid global-time-before parameter. Using default.");
                            }

                            break;
                        case "global-time-after":
                            try
                            {
                                LavaSystem.stime2 = int.Parse(text3);
                            }
                            catch
                            {
                                Server.s.Log("Invalid global-time-after parameter. Using default.");
                            }

                            break;
                        case "flip-heads":
                            try
                            {
                                Server.flipHead = bool.Parse(text3);
                            }
                            catch
                            {
                                Server.s.Log("Invalid flip-heads parameter. Using default.");
                            }

                            break;
                        case "reappearing-message":
                            if (ValidString(text3, "^*\"|'=%$![]&:;.,{}~-+()?_/\\ "))
                                Server.serverMessage = text3.Replace("^", Environment.NewLine);
                            else
                                Server.s.Log("Invalid reappearing-message text. Reappearing message deactivated.");
                            break;
                        case "reappearing-message-interval":
                            try
                            {
                                Server.serverMessageInterval = int.Parse(text3);
                            }
                            catch
                            {
                                Server.s.Log("Invalid reappearing-message-interval parameter. Using default.");
                            }

                            break;
                        case "use-heaven":
                            try
                            {
                                Server.useHeaven = bool.Parse(text3);
                            }
                            catch
                            {
                                Server.s.Log("Invalid use-heaven parameter. Using default.");
                            }

                            break;
                        case "heaven-map-name":
                            try
                            {
                                Server.heavenMapName = text3;
                            }
                            catch
                            {
                                Server.s.Log("Invalid heaven-map-name parameter. Using default.");
                            }

                            break;
                        case "chance-calm":
                            try
                            {
                                LavaSystem.chanceCalm = int.Parse(text3);
                            }
                            catch
                            {
                                Server.s.Log("Invalid chance-calm parameter. Using default.");
                            }

                            break;
                        case "chance-disturbed":
                            try
                            {
                                LavaSystem.chanceDisturbed = int.Parse(text3);
                            }
                            catch
                            {
                                Server.s.Log("Invalid chance-disturbed parameter. Using default.");
                            }

                            break;
                        case "chance-furious":
                            try
                            {
                                LavaSystem.chanceFurious = int.Parse(text3);
                            }
                            catch
                            {
                                Server.s.Log("Invalid chance-furious parameter. Using default.");
                            }

                            break;
                        case "chance-wild":
                            try
                            {
                                LavaSystem.chanceWild = int.Parse(text3);
                            }
                            catch
                            {
                                Server.s.Log("Invalid chance-wild parameter. Using default.");
                            }

                            break;
                        case "game-mode":
                            switch (text3.ToLower())
                            {
                                case "lava":
                                    Server.mode = Mode.Lava;
                                    break;
                                case "lavafreebuild":
                                    Server.mode = Mode.LavaFreebuild;
                                    break;
                                case "freebuild":
                                    Server.mode = Mode.Freebuild;
                                    break;
                                case "zombie":
                                    Server.mode = Mode.Zombie;
                                    break;
                                default:
                                    Server.s.Log("Invalid lava-state parameter. Using default.");
                                    break;
                            }

                            break;
                        case "server-description":
                            if (ValidString(text3, "![]:.,{}~-+()?_/\\ "))
                                Server.description = text3;
                            else
                                Server.s.Log("Server-description invalid! Setting to default.");
                            break;
                        case "server-flag":
                            if (ValidString(text3, "![]:.,{}~-+()?_/\\ "))
                                Server.Flag = text3;
                            else
                                Server.s.Log("Server-flag invalid! Setting to default.");
                            break;
                        case "auto-flag":
                            try
                            {
                                Server.autoFlag = bool.Parse(text3);
                            }
                            catch
                            {
                                Server.s.Log("Invalid auto-flag parameter. Using default.");
                            }

                            break;
                    }
                }

                Server.s.SettingsUpdate();
                Save(givenPath);
            }
            else
            {
                Save(givenPath);
            }
        }

        // Token: 0x060018B5 RID: 6325 RVA: 0x000A8F54 File Offset: 0x000A7154
        public static bool ValidString(string str, string allowed)
        {
            var text = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz01234567890" + allowed;
            foreach (var value in str)
                if (text.IndexOf(value) == -1)
                    return false;
            return true;
        }

        // Token: 0x060018B6 RID: 6326 RVA: 0x000A8FA0 File Offset: 0x000A71A0
        public static void Save()
        {
            Save(lastPath);
        }

        // Token: 0x060018B7 RID: 6327 RVA: 0x000A8FAC File Offset: 0x000A71AC
        private static void Save(string givenPath)
        {
            try
            {
                var streamWriter = new StreamWriter(File.Create(givenPath));
                if (givenPath.IndexOf("server") != -1)
                {
                    streamWriter.WriteLine(
                        "# Edit the settings below to modify how your server operates. This is an explanation of what each setting does.");
                    streamWriter.WriteLine("#   server-name\t=\tThe name which displays on minecraft.net");
                    streamWriter.WriteLine("#   motd\t=\tThe message which displays when a player connects");
                    streamWriter.WriteLine("#   port\t=\tThe port to operate from");
                    streamWriter.WriteLine(
                        "#   console-only\t=\tRun without a GUI (useful for Linux servers with mono)");
                    streamWriter.WriteLine(
                        "#   verify-names-security\t=\tPerform user verification, keep it set to true");
                    streamWriter.WriteLine("#   public\t=\tSet to true to appear in the public server list");
                    streamWriter.WriteLine("#   max-players\t=\tThe maximum number of connections");
                    streamWriter.WriteLine("#   max-maps\t=\tThe maximum number of maps loaded at once");
                    streamWriter.WriteLine("#   world-chat\t=\tSet to true to enable world chat");
                    streamWriter.WriteLine("#   guest-goto\t=\tSet to true to give guests goto and levels commands");
                    streamWriter.WriteLine("#   irc\t=\tSet to true to enable the IRC bot");
                    streamWriter.WriteLine("#   irc-nick\t=\tThe name of the IRC bot");
                    streamWriter.WriteLine("#   irc-server\t=\tThe server to connect to");
                    streamWriter.WriteLine("#   irc-channel\t=\tThe channel to join");
                    streamWriter.WriteLine("#   irc-opchannel\t=\tThe channel to join (posts OpChat)");
                    streamWriter.WriteLine("#   irc-port\t=\tThe port to use to connect");
                    streamWriter.WriteLine(
                        "#   irc-identify\t=(true/false)\tDo you want the IRC bot to Identify itself with nickserv. Note: You will need to register it's name with nickserv manually.");
                    streamWriter.WriteLine(
                        "#   irc-password\t=\tThe password you want to use if you're identifying with nickserv");
                    streamWriter.WriteLine("#   anti-tunnels\t=\tStops people digging below max-depth");
                    streamWriter.WriteLine("#   max-depth\t=\tThe maximum allowed depth to dig down");
                    streamWriter.WriteLine("#   backup-time\t=\tThe number of seconds between automatic backups");
                    streamWriter.WriteLine(
                        "#   overload\t=\tThe higher this is, the longer the physics is allowed to lag. Default 1500");
                    streamWriter.WriteLine(
                        "#   use-whitelist\t=\tSwitch to allow use of a whitelist to override IP bans for certain players.  Default false.");
                    streamWriter.WriteLine(
                        "#   force-cuboid\t=\tRun cuboid until the limit is hit, instead of canceling the whole operation.  Default false.");
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("#   Host\t=\tThe host name for the database (usually 127.0.0.1)");
                    streamWriter.WriteLine(
                        "#   SQLPort\t=\tPort number to be used for MySQL.  Unless you manually changed the port, leave this alone.  Default 3306.");
                    streamWriter.WriteLine(
                        "#   Username\t=\tThe username you used to create the database (usually root)");
                    streamWriter.WriteLine("#   Password\t=\tThe password set while making the database");
                    streamWriter.WriteLine("#   DatabaseName\t=\tThe name of the database stored (Default = MCZall)");
                    streamWriter.WriteLine();
                    streamWriter.WriteLine(
                        "#   defaultColor\t=\tThe color code of the default messages (Default = &e)");
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("#   Super-limit\t=\tThe limit for building commands for SuperOPs");
                    streamWriter.WriteLine("#   Op-limit\t=\tThe limit for building commands for Operators");
                    streamWriter.WriteLine("#   Adv-limit\t=\tThe limit for building commands for AdvBuilders");
                    streamWriter.WriteLine("#   Builder-limit\t=\tThe limit for building commands for Builders");
                    streamWriter.WriteLine();
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("# Server options");
                    streamWriter.WriteLine("server-name = " + Server.name);
                    streamWriter.WriteLine("motd = " + Server.motd);
                    streamWriter.WriteLine("port = " + Server.port);
                    streamWriter.WriteLine("verify-names-security = " + Server.verify.ToString().ToLower());
                    streamWriter.WriteLine("public = " + Server.isPublic.ToString().ToLower());
                    streamWriter.WriteLine("max-players = " + Server.players);
                    streamWriter.WriteLine("max-maps = " + Server.maps);
                    streamWriter.WriteLine("world-chat = " + Server.worldChat.ToString().ToLower());
                    streamWriter.WriteLine("check-updates = " + Server.checkUpdates.ToString().ToLower());
                    streamWriter.WriteLine("autoload = " + Server.AutoLoad.ToString().ToLower());
                    streamWriter.WriteLine("auto-restart = " + Server.autorestart.ToString().ToLower());
                    streamWriter.WriteLine("restarttime = " + Server.restarttime.ToShortTimeString());
                    streamWriter.WriteLine("restart-on-error = " + Server.restartOnError);
                    streamWriter.WriteLine("main-name = " + Server.level);
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("# irc bot options");
                    streamWriter.WriteLine("irc-use = " + Server.irc.ToString().ToLower());
                    streamWriter.WriteLine("irc-nick = " + Server.ircNick);
                    streamWriter.WriteLine("irc-server = " + Server.ircServer);
                    streamWriter.WriteLine("irc-channel = " + Server.ircChannel);
                    streamWriter.WriteLine("irc-opchannel = " + Server.ircOpChannel);
                    streamWriter.WriteLine("irc-port = " + Server.ircPort);
                    streamWriter.WriteLine("irc-identify = " + Server.ircIdentify);
                    streamWriter.WriteLine("irc-password = " + Server.ircPassword);
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("# other options");
                    streamWriter.WriteLine("anti-tunnels = " + Server.antiTunnel.ToString().ToLower());
                    streamWriter.WriteLine("max-depth = " + Server.maxDepth.ToString().ToLower());
                    streamWriter.WriteLine("rplimit = " + Server.rpLimit.ToString().ToLower());
                    streamWriter.WriteLine("rplimit-norm = " + Server.rpNormLimit.ToString().ToLower());
                    streamWriter.WriteLine("physicsrestart = " + Server.physicsRestart.ToString().ToLower());
                    streamWriter.WriteLine("old-help = " + Server.oldHelp.ToString().ToLower());
                    streamWriter.WriteLine("deathcount = " + Server.deathcount.ToString().ToLower());
                    streamWriter.WriteLine("afk-minutes = " + Server.afkminutes);
                    streamWriter.WriteLine("afk-kick = " + Server.afkkick);
                    streamWriter.WriteLine("parse-emotes = " + Server.parseSmiley.ToString().ToLower());
                    streamWriter.WriteLine("dollar-before-dollar = " + Server.useDollarSign.ToString().ToLower());
                    streamWriter.WriteLine("use-whitelist = " + Server.useWhitelist.ToString().ToLower());
                    streamWriter.WriteLine("money-name = " + Server.moneys);
                    streamWriter.WriteLine("opchat-perm = " + ((sbyte) Server.opchatperm));
                    streamWriter.WriteLine("log-heartbeat = " + Server.logbeat);
                    streamWriter.WriteLine("force-cuboid = " + Server.forceCuboid);
                    streamWriter.WriteLine("repeat-messages = " + Server.repeatMessage);
                    streamWriter.WriteLine("host-state = " + Server.ConsoleName);
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("# backup options");
                    streamWriter.WriteLine("backup-time = " + Server.backupInterval);
                    streamWriter.WriteLine("backup-location = " + Server.backupLocation);
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("#Error logging");
                    streamWriter.WriteLine("report-back = " + Server.reportBack.ToString().ToLower());
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("#MySQL information");
                    streamWriter.WriteLine("UseMySQL = " + Server.useMySQL);
                    streamWriter.WriteLine("Host = " + Server.MySQLHost);
                    streamWriter.WriteLine("SQLPort = " + Server.MySQLPort);
                    streamWriter.WriteLine("Username = " + Server.MySQLUsername);
                    streamWriter.WriteLine("Password = " + Server.MySQLPassword);
                    streamWriter.WriteLine("DatabaseName = " + Server.MySQLDatabaseName);
                    streamWriter.WriteLine("Pooling = " + Server.MySQLPooling);
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("#Colors");
                    streamWriter.WriteLine("defaultColor = " + Server.DefaultColor);
                    streamWriter.WriteLine("irc-color = " + Server.IRCColour);
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("#Running on mono?");
                    streamWriter.WriteLine("mono = " + Server.mono);
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("#Custom Messages");
                    streamWriter.WriteLine("custom-ban = " + Server.customBan.ToString().ToLower());
                    streamWriter.WriteLine("custom-ban-message = " + Server.customBanMessage);
                    streamWriter.WriteLine("custom-shutdown = " + Server.customShutdown.ToString().ToLower());
                    streamWriter.WriteLine("custom-shutdown-message = " + Server.customShutdownMessage);
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("cheapmessage = " + Server.cheapMessage.ToString().ToLower());
                    streamWriter.WriteLine("cheap-message-given = " + Server.cheapMessageGiven);
                    streamWriter.WriteLine("rank-super = " + Server.rankSuper.ToString().ToLower());
                    try
                    {
                        streamWriter.WriteLine("default-rank = " + Server.defaultRank);
                    }
                    catch
                    {
                        streamWriter.WriteLine("default-rank = guest");
                    }

                    streamWriter.WriteLine();
                    streamWriter.WriteLine("#Lava Settings");
                    streamWriter.WriteLine(
                        "lava-state = " + Enum.GetName(typeof(LavaState), LavaSettings.All.LavaState));
                    streamWriter.WriteLine("global-time-before = " + LavaSystem.stime);
                    streamWriter.WriteLine("global-time-after = " + LavaSystem.stime2);
                    streamWriter.WriteLine("reappearing-message = " +
                                           Server.serverMessage.Replace(Environment.NewLine, "^"));
                    streamWriter.WriteLine("reappearing-message-interval = " + Server.serverMessageInterval);
                    streamWriter.WriteLine("use-heaven = " + Server.useHeaven);
                    streamWriter.WriteLine("heaven-map-name = " + Server.heavenMapName);
                    streamWriter.WriteLine("chance-calm = " + LavaSystem.chanceCalm);
                    streamWriter.WriteLine("chance-disturbed = " + LavaSystem.chanceDisturbed);
                    streamWriter.WriteLine("chance-furious = " + LavaSystem.chanceFurious);
                    streamWriter.WriteLine("chance-wild = " + LavaSystem.chanceWild);
                    streamWriter.WriteLine("game-mode = " + Enum.GetName(typeof(Mode), Server.mode));
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("#WOM Settings");
                    streamWriter.WriteLine("server-description = " + Server.description);
                    streamWriter.WriteLine("server-flag = " + Server.Flag);
                    streamWriter.WriteLine("auto-flag = " + Server.autoFlag);
                }

                streamWriter.Flush();
                streamWriter.Close();
                streamWriter.Dispose();
            }
            catch
            {
                Server.s.Log("SAVE FAILED! " + givenPath);
            }
        }
    }
}