using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MCDzienny.Misc;

namespace MCDzienny
{
    public class CmdMyMap : Command
    {

        const int SizeSmall = 262144;

        const int SizeMedium = 1048576;

        const int SizeBig = 8388608;

        public static char[] PathSeparators = new char[2]
        {
            '/', '\\'
        };

        readonly string MyMapDirectory = "maps" + Path.DirectorySeparatorChar + "mymaps";

        public override string name { get { return "mymap"; } }

        public override string shortcut { get { return "mm"; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            DirectoryUtil.CreateIfNotExists(MyMapDirectory);
            Message message2 = new Message(message);
            switch (message2.ReadStringLower())
            {
                case "n":
                case "new":
                    CreateNewMap(p);
                    break;
                case "g":
                case "goto":
                case "v":
                case "visit":
                    VisitMap(p, message2);
                    break;
                case "back":
                case "exit":
                case "leave":
                    GoToMainMap(p);
                    break;
                case "del":
                case "delete":
                case "remove":
                    DeleteMap(p, message2);
                    break;
                case "rename":
                    RenameMap(p, message2);
                    break;
                case "levels":
                case "list":
                case "m":
                case "maps":
                    DisplayMapList(p, message2);
                    break;
                case "inv":
                case "invite":
                    InvitePlayer(p, message2);
                    break;
                case "ss":
                case "setspawn":
                    SetMapSpawn(p);
                    break;
                case "k":
                case "kick":
                    KickPlayer(p, message2);
                    break;
                case "all":
                case "allowed":
                    ShowAllowedPlayers(p);
                    break;
                case "allow":
                    AllowPlayers(p, message2);
                    break;
                case "disallow":
                    DisallowPlayers(p, message2);
                    break;
                case "public":
                    SetMapPublic(p, isPublic: true);
                    break;
                case "private":
                    SetMapPublic(p, isPublic: false);
                    break;
                case "more":
                    HelpMore(p);
                    break;
                case "motd":
                    SetMotd(p, message2);
                    break;
                case "s":
                case "shortcuts":
                    ShowShortcuts(p);
                    break;
                default:
                    Help(p);
                    break;
            }
        }

        void RenameMap(Player p, Message msg)
        {
            if (msg.Count != 3)
            {
                Player.SendMessage(p, "Help:");
                Player.SendMessage(p, "/mm rename [oldmap] [newmap]");
                return;
            }
            string text = msg.ReadStringLower();
            if (!Player.ValidName(text))
            {
                Player.SendMessage(p, "Invalid map name.");
                return;
            }
            string text2 = msg.ReadStringLower();
            if (!Player.ValidName(text2))
            {
                Player.SendMessage(p, "Invalid map name.");
                return;
            }
            if (text2.Length < 3)
            {
                Player.SendMessage(p, "The map name has to consist of at least 3 characters.");
                return;
            }
            List<string> myMapNames = GetMyMapNames(p.name);
            if (!myMapNames.Contains(text))
            {
                Player.SendMessage(p, "You don't have the map named: " + text);
                return;
            }
            if (myMapNames.Contains(text2))
            {
                Player.SendMessage(p, "You can't rename the map to: " + text2);
                Player.SendMessage(p, "Such map already exists!");
                return;
            }
            string myMapDirectoryPath = GetMyMapDirectoryPath(p.name.ToLower());
            string fileName = myMapDirectoryPath + Path.DirectorySeparatorChar + text + ".lvl";
            string fileName2 = myMapDirectoryPath + Path.DirectorySeparatorChar + text + ".properties";
            FileInfo fileInfo = new FileInfo(fileName);
            string destFileName = myMapDirectoryPath + Path.DirectorySeparatorChar + text2 + ".lvl";
            string destFileName2 = myMapDirectoryPath + Path.DirectorySeparatorChar + text2 + ".properties";
            FileInfo fileInfo2 = new FileInfo(fileName2);
            try
            {
                fileInfo.MoveTo(destFileName);
                if (fileInfo2.Exists)
                {
                    fileInfo2.MoveTo(destFileName2);
                }
            }
            catch (Exception ex)
            {
                Player.SendMessage(p, "An error occured.");
                Player.SendMessage(p, ex.Message);
                Server.ErrorLog(ex);
                return;
            }
            Level level = Level.FindExactMM(text, p.name);
            if (level != null)
            {
                level.name = text2;
            }
            Player.SendMessage(p, "The map " + text + " was renamed to " + text2);
        }

        void SetMotd(Player p, Message msg)
        {
            if (!IsOnOwnMap(p))
            {
                Player.SendMessage(p, "You can change this property only if you are on your map.");
                return;
            }
            string text = msg.ReadToEnd();
            if (text == null)
            {
                Player.SendMessage(p, "Write your map MOTD (message of the day):");
                text = p.ReadLine();
            }
            if (text != null)
            {
                Player.SendMessage(p, "Your map new MOTD: " + text);
                p.level.motd = text;
                p.level.changed = true;
            }
        }

        static bool IsOnOwnMap(Player p)
        {
            if (p.level.mapType == MapType.MyMap)
            {
                return IsMapOwnedBy(p.level, p);
            }
            return false;
        }

        void ShowAllowedPlayers(Player p)
        {
            if (p.level.mapType != MapType.MyMap)
            {
                Player.SendMessage(p, "You can check allowed players only if you are on a map of type MyMap.");
                return;
            }
            if (!p.level.IsPublic || p.level.AllowedPlayers.Count != 0)
            {
                if (p.level.AllowedPlayers.Count > 0)
                {
                    Player.SendMessage(p, "Players allowed to build (apart from the map owner):");
                    Player.SendMessage(p, JoinWithComma(p.level.AllowedPlayers.Select(name => Player.RemoveEmailDomain(name)).ToArray()));
                }
                else
                {
                    Player.SendMessage(p, "Only the map owner can build here.");
                }
            }
            if (p.level.IsPublic)
            {
                Player.SendMessage(p, "This map is currently public so anyone can build here.");
            }
        }

        void DisallowPlayers(Player p, Message msg)
        {
            if (!IsOnOwnMap(p))
            {
                Player.SendMessage(p, "You can change this property only if you are on your map.");
                return;
            }
            string text = msg.ReadToEnd();
            if (text == null)
            {
                Player.SendMessage(p, "Write a player name or names to disallow them to build on this map:");
                Player.SendMessage(p, "%7(If you write more than one name separate them with a comma.)");
                text = p.ReadLine();
                if (text == null)
                {
                    return;
                }
            }
            string[] array = (from e in text.Split(new char[1]
                {
                    ','
                }, StringSplitOptions.RemoveEmptyEntries)
                select e.Trim().ToLower()).ToArray();
            var list = new List<string>();
            string[] array2 = array;
            foreach (string player in array2)
            {
                HashSet<string> allowedPlayers = p.level.AllowedPlayers;
                Func<string, bool> predicate = e => e.StartsWith(player);
                string text2 = allowedPlayers.FirstOrDefault(predicate);
                if (text2 != null)
                {
                    p.level.AllowedPlayers.Remove(text2);
                    list.Add(text2);
                    Player player2 = Player.FindExact(text2);
                    if (player2 != null)
                    {
                        Player.SendMessage(p, string.Format("You are no longer allowed to build on map {0}.", p.level.name));
                    }
                }
            }
            if (list.Count == 1)
            {
                Player.SendMessage(p, string.Format("Player {0} is no longer allowed to build on this map.", list[0]));
            }
            else if (list.Count > 1)
            {
                Player.SendMessage(p, string.Format("Players {0} are no longer allowed to build on this map.", JoinWithComma(list.ToArray())));
            }
            else if (array.Length == 1)
            {
                Player.SendMessage(p, string.Format("Player {0} is not on the list of the allowed players.", array[0]));
            }
            else if (array.Length > 1)
            {
                Player.SendMessage(p, string.Format("Players {0} are not on the list of the allowed players.", JoinWithComma(array)));
            }
        }

        static string JoinWithComma(string[] array)
        {
            return string.Join(", ", array);
        }

        void AllowPlayers(Player p, Message msg)
        {
            if (!IsOnOwnMap(p))
            {
                Player.SendMessage(p, "You can change this property only if you are on your map.");
                return;
            }
            string text = msg.ReadToEnd();
            if (text == null)
            {
                Player.SendMessage(p, "Write a player name or names to allow them to build on this map:");
                Player.SendMessage(p, "%7(If you write more than one name separate them with a comma [,].)");
                text = p.ReadLine();
                if (text == null)
                {
                    return;
                }
            }
            string[] array = text.Split(new char[1]
            {
                ','
            }, StringSplitOptions.RemoveEmptyEntries);
            var list = new List<Player>();
            var list2 = new List<string>();
            var list3 = new List<Player>();
            string[] array2 = array;
            foreach (string item in array2)
            {
                Player player = Player.Find(item);
                if (player != null)
                {
                    if (!p.level.AllowedPlayers.Contains(player.name.ToLower()))
                    {
                        p.level.AllowedPlayers.Add(player.name.ToLower());
                        list.Add(player);
                        p.level.changed = true;
                    }
                    else
                    {
                        list3.Add(player);
                    }
                }
                else
                {
                    list2.Add(item);
                }
            }
            if (list.Count == 1)
            {
                Player.GlobalMessageLevel(p.level, string.Format("Player {0} was allowed to build on this map.", list[0].PublicName));
            }
            else if (list.Count > 1)
            {
                Player.GlobalMessageLevel(p.level,
                                          string.Format("Players {0} were allowed to build on this map.", JoinWithComma(list.Select(e => e.PublicName).ToArray())));
            }
            if (list2.Count == 1)
            {
                Player.SendMessage(p, string.Format("Player named {0} couldn't be found.", list2[0]));
            }
            else if (list2.Count > 1)
            {
                Player.SendMessage(p, string.Format("Players named {0} couldn't be found.", JoinWithComma(list2.ToArray())));
            }
            if (list3.Count == 1)
            {
                Player.SendMessage(p, string.Format("Player {0} is already allowed to build here.", list3[0].PublicName));
            }
            else if (list3.Count > 1)
            {
                Player.SendMessage(p, string.Format("Players {0} are already allowed to build here.", JoinWithComma(list3.Select(e => e.PublicName).ToArray())));
            }
        }

        void SetMapPublic(Player p, bool isPublic)
        {
            if (!IsOnOwnMap(p))
            {
                Player.SendMessage(p, "You can change this property only on your map.");
                return;
            }
            if (p.level.IsPublic && isPublic)
            {
                Player.SendMessage(p, "This map is already public.");
                return;
            }
            if (!p.level.IsPublic && !isPublic)
            {
                Player.SendMessage(p, "This map is already private.");
                return;
            }
            p.level.IsPublic = isPublic;
            p.level.changed = true;
            if (isPublic)
            {
                Player.GlobalMessageLevel(p.level, "This map is now public. It means anyone can build here.");
            }
            else
            {
                Player.GlobalMessageLevel(p.level, "This map is now private. It means only allowed persons can build here.");
            }
        }

        void KickPlayer(Player p, Message msg)
        {
            string text = msg.ReadStringLower();
            if (text == null)
            {
                Player.SendMessage(p, "Who to kick? Write the name:");
                text = InputReader.StartReader(p, delegate(string input)
                {
                    input = input.Trim().ToLower();
                    return Player.ValidName(input);
                }, delegate
                {
                    Player.SendMessage(p, "The given name contains a forbidden character.");
                    Player.SendMessage(p, "Who to kick? Write the name again:");
                });
                if (text == null)
                {
                    return;
                }
            }
            else if (!Player.ValidName(text))
            {
                Player.SendMessage(p, "The given name contains a forbidden character.");
                return;
            }
            Player player = Player.Find(text);
            if (player == null)
            {
                Player.SendMessage(p, string.Format("Couldn't find the player named {0}.", text));
                return;
            }
            if (player.group.Permission >= LevelPermission.Operator)
            {
                Player.SendMessage(p, "You can't kick OP+");
                return;
            }
            if (player.level.Owner.ToLower() == p.name.ToLower())
            {
                Player.SendMessage(p, string.Format("You can't kick {0} because he is not on your map.", player.PublicName));
                return;
            }
            player.SendToMap(Server.DefaultLevel);
            Player.SendMessage(player, string.Format("You were kicked by {0} from his map.", p.PublicName));
            Player.SendMessage(p, string.Format("Player {0} was kicked from your map.", player.PublicName));
        }

        void InvitePlayer(Player p, Message msg)
        {
            if (!IsOnOwnMap(p))
            {
                Player.SendMessage(p, "You can only invite if you are on one of your maps.");
                return;
            }
            string text = msg.ReadStringLower();
            if (text == null)
            {
                Player.SendMessage(p, "Who do you want to invite? Write the name:");
                while (true)
                {
                    text = p.ReadLine();
                    if (text == null)
                    {
                        return;
                    }
                    text = text.Trim().ToLower();
                    if (Player.ValidName(text))
                    {
                        break;
                    }
                    Player.SendMessage(p, "The given name contains a forbidden character.");
                    Player.SendMessage(p, "Who do you want to invite? Write the name again:");
                }
            }
            else if (!Player.ValidName(text))
            {
                Player.SendMessage(p, "The given name contains a forbidden character.");
                return;
            }
            Player player = Player.Find(text);
            if (player == null)
            {
                Player.SendMessage(p, string.Format("Couldn't find a player named {0}.", text));
                return;
            }
            player.ExtraData["invitation"] = new object[3]
            {
                p.level.name, p.PublicName, DateTime.Now
            };
            Player.SendMessage(player, string.Format("You were invited by %a{0}%s to visit their map.", p.PublicName));
            Player.SendMessage(player, "Write /accept to accept the invitation.");
            Player.SendMessage(p, string.Format("You sent an invitation to {0}.", player.PublicName));
        }

        void SetMapSpawn(Player p)
        {
            if (!IsOnOwnMap(p))
            {
                Player.SendMessage(p, "You are not allowed to set a map spawn on this map.");
            }
            else
            {
                new CmdSetspawn().Use(p, "");
            }
        }

        void DeleteMap(Player p, Message msg)
        {
            string mapName = msg.ReadStringLower();
            if (mapName == null)
            {
                Player.SendMessage(p, "Write the map name to delete:");
                while (true)
                {
                    mapName = p.ReadLine();
                    if (mapName == null)
                    {
                        return;
                    }
                    mapName = mapName.Trim().ToLower();
                    if (Player.ValidName(mapName))
                    {
                        break;
                    }
                    Player.SendMessage(p, "The given map name contains a forbidden character.");
                    Player.SendMessage(p, "Write the map name to delete:");
                }
            }
            else if (!Player.ValidName(mapName))
            {
                Player.SendMessage(p, "The given map name contains a forbidden character.");
                return;
            }
            string myMapDirectoryPath = GetMyMapDirectoryPath(p.name);
            if (!File.Exists(myMapDirectoryPath + Path.DirectorySeparatorChar + mapName + ".lvl"))
            {
                Player.SendMessage(p, string.Format("You don't have a map named {0}.", mapName));
                return;
            }
            Player.SendMessage(p, string.Format("Delete map {0}? (Yes/No):", mapName));
            string text = null;
            while (true)
            {
                text = p.ReadLine();
                if (text == null)
                {
                    Player.SendMessage(p, "Map deletion was aborted.");
                    return;
                }
                text = text.Trim().ToLower();
                if (text == "no")
                {
                    Player.SendMessage(p, "Map deletion was aborted.");
                    return;
                }
                if (text == "yes")
                {
                    break;
                }
                Player.SendMessage(p, string.Format("Delete map {0}? (Yes/No):", mapName));
                text = p.ReadLine();
            }
            Server.levels.SingleOrDefault(l => l.mapType == MapType.MyMap && l.name == mapName && IsMapOwnedBy(l, p)).Unload();
            File.Delete(myMapDirectoryPath + Path.DirectorySeparatorChar + mapName + ".lvl");
            FileUtil.DeleteIfExists(myMapDirectoryPath + Path.DirectorySeparatorChar + mapName + ".properties");
            var dictionary = new Dictionary<string, object>();
            dictionary.Add("@MapName", mapName.ToLower());
            dictionary.Add("@Owner", p.name.ToLower());
            Dictionary<string, object> parameters = dictionary;
            DBInterface.ExecuteQuery("DELETE FROM MapList WHERE MapName = @MapName AND Owner = @Owner", parameters);
            Player.GlobalMessage(string.Format("Map {0}/{1} was deleted.", p.PublicName, mapName));
        }

        internal static bool IsMapOwnedBy(Level l, Player p)
        {
            return l.Owner.ToLower() == p.name.ToLower();
        }

        void GoToMainMap(Player p)
        {
            if (p.level == Server.mainLevel)
            {
                Player.SendMessage(p, "You are already on the main map.");
            }
            else
            {
                new CmdGoto().Use(p, Server.DefaultLevel.name);
            }
        }

        void VisitMap(Player p, Message msg)
        {
            string mapName = msg.ReadStringLower();
            string owner = p.name.ToLower();
            if (mapName == null)
            {
                Player.SendMessage(p, "Write the map name to visit:");
                Player.SendMessage(p, "%7(If it's someone else map type: [playerName]/[mapName] ,eg notch/coolmap)");
                while (true)
                {
                    mapName = p.ReadLine();
                    if (mapName == null)
                    {
                        return;
                    }
                    mapName = mapName.Trim().ToLower();
                    if (ContainsPathSeparator(mapName) && SplitGivenMapPath(mapName).Length != 2)
                    {
                        Player.SendMessage(p, "Incorrect map path.");
                        Player.SendMessage(p, "If it's someone else map type: [player]/[map] ,eg notch/coolmap");
                        continue;
                    }
                    if (Player.ValidName(mapName))
                    {
                        break;
                    }
                    Player.SendMessage(p, "The given map name contains a forbidden character.");
                    Player.SendMessage(p, "Write the map name again:");
                }
            }
            if (ContainsPathSeparator(mapName))
            {
                string[] array = SplitGivenMapPath(mapName);
                if (array.Length != 2)
                {
                    Player.SendMessage(p, "Incorrect map path.");
                    return;
                }
                mapName = array[1].Trim().ToLower();
                owner = array[0].Trim().ToLower();
            }
            if (!Player.ValidName(mapName))
            {
                Player.SendMessage(p, "Error: The given map name contains a forbidden character.");
                return;
            }
            if (!Player.ValidName(owner))
            {
                Player.SendMessage(p, "Error: The given owner name contains a forbidden character.");
                return;
            }
            if (owner.EndsWith("@"))
            {
                string path = GetMyMapDirectoryPath(owner).Replace(owner, "");
                if (!Directory.Exists(path))
                {
                    Player.SendMessage(p, string.Format("Player named {0} doesn't own any maps.", owner));
                    return;
                }
                string text = Directory.GetDirectories(path).FirstOrDefault(d => d.Contains(owner));
                if (text == null)
                {
                    Player.SendMessage(p, string.Format("Player named {0} doesn't own any maps.", owner));
                    return;
                }
                owner = GetPathTopElement(text);
            }
            Level level = Server.levels.SingleOrDefault(l => l.mapType == MapType.MyMap && l.name == mapName && IsMapOwnedBy(l, p));
            if (level != null)
            {
                p.SendToMap(level);
                return;
            }
            string path2 = GetMyMapDirectoryPath(owner) + Path.DirectorySeparatorChar + mapName + ".lvl";
            if (!File.Exists(path2))
            {
                level = Server.levels.FirstOrDefault(l => l.mapType == MapType.MyMap && l.Owner.Contains(owner.ToLower()) && l.name.Contains(mapName.ToLower()));
                if (level != null)
                {
                    p.SendToMap(level);
                    return;
                }
                if (owner == p.name.ToLower())
                {
                    level = Server.levels.FirstOrDefault(l => l.mapType == MapType.MyMap && l.name == mapName.ToLower());
                    if (level != null)
                    {
                        p.SendToMap(level);
                        return;
                    }
                    level = Server.levels.FirstOrDefault(l => l.mapType == MapType.MyMap && l.name.Contains(mapName.ToLower()));
                    if (level != null)
                    {
                        p.SendToMap(level);
                        return;
                    }
                }
                if (p.name == owner)
                {
                    Player.SendMessage(p, string.Format("You don't have a map named {0}.", mapName));
                }
                else
                {
                    Player.SendMessage(p, string.Format("Player {0} doesn't have a map named {1}.", owner, mapName));
                }
            }
            else
            {
                LoadAndSendToMap(p, mapName, owner);
            }
        }

        static string[] SplitGivenMapPath(string mapName)
        {
            return mapName.Split(PathSeparators, StringSplitOptions.RemoveEmptyEntries);
        }

        static bool ContainsPathSeparator(string mapName)
        {
            char[] pathSeparators = PathSeparators;
            foreach (char value in pathSeparators)
            {
                if (mapName.Contains(value))
                {
                    return true;
                }
            }
            return false;
        }

        internal string GetPathTopElement(string path)
        {
            int num = path.LastIndexOfAny(new char[3]
            {
                Path.DirectorySeparatorChar, '/', '\\'
            });
            if (num < 0)
            {
                return path;
            }
            return path.Remove(0, num + 1);
        }

        internal void LoadAndSendToMap(Player p, string mapName, string owner)
        {
            Level level = Level.Load(GetMyMapDirectoryPath(owner), mapName.ToLower(), owner.ToLower(), MapType.MyMap, isAutoUnloading: true);
            if (level == null)
            {
                Player.SendMessage(p, "%cUnable to load the map {0}.", mapName);
                return;
            }
            Server.AddLevel(level);
            Player.GlobalMessage(string.Format("Map %3{0}%s owned by %3{1}%s was loaded.", mapName, Player.RemoveEmailDomain(owner)));
            p.SendToMap(level);
            Player.GlobalMessage(p.color + "*" + p.PublicName + " went to %b" + p.level.PublicName);
            Server.s.Log(p.PublicName + " went to " + p.level.PublicName);
        }

        void DisplayMapList(Player p, Message msg)
        {
            string text = msg.ReadStringLower();
            if (text == null)
            {
                Player.SendMessage(p, "Your maps:");
                Player.SendMessage(p, JoinWithComma((from e in GetMyMapNames(p.name)
                                                        orderby e
                                                        select e).ToArray()));
            }
            else if (!Player.ValidName(text))
            {
                Player.SendMessage(p, "Error: The given owner name contains a forbidden character.");
            }
            else
            {
                Player.SendMessage(p, string.Format("{0} maps:", text));
                Player.SendMessage(p, JoinWithComma((from e in GetMyMapNames(text)
                                                        orderby e
                                                        select e).ToArray()));
            }
        }

        internal string GetMyMapDirectoryPath(string name)
        {
            string text = name.ToLower();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < 3 && i < text.Length; i++)
            {
                char directorySeparatorChar = Path.DirectorySeparatorChar;
                stringBuilder.Append(directorySeparatorChar.ToString() + text[i]);
            }
            return MyMapDirectory + stringBuilder + Path.DirectorySeparatorChar + name.ToLower();
        }

        List<string> GetMyMapNames(string name)
        {
            string myMapDirectoryPath = GetMyMapDirectoryPath(name);
            DirectoryUtil.CreateIfNotExists(myMapDirectoryPath);
            if (!Directory.Exists(myMapDirectoryPath))
            {
                return new List<string>();
            }
            return (from e in Directory.GetFiles(Environment.CurrentDirectory + Path.DirectorySeparatorChar + myMapDirectoryPath)
                where e.EndsWith(".lvl")
                select GetPathTopElement(e).Replace(".lvl", "")).ToList();
        }

        List<MyMapInfo> GetMyMapInfos(Player p)
        {
            var list = new List<MyMapInfo>();
            string myMapDirectoryPath = GetMyMapDirectoryPath(p.name);
            foreach (string myMapName in GetMyMapNames(p.name))
            {
                string mapName = myMapName.ToLower();
                string[] array = new string[6]
                {
                    Environment.CurrentDirectory, null, null, null, null, null
                };
                char directorySeparatorChar = Path.DirectorySeparatorChar;
                array[1] = directorySeparatorChar.ToString();
                array[2] = myMapDirectoryPath;
                char directorySeparatorChar2 = Path.DirectorySeparatorChar;
                array[3] = directorySeparatorChar2.ToString();
                array[4] = myMapName;
                array[5] = ".lvl";
                list.Add(new MyMapInfo(mapName, Level.ReadLevelInfo(string.Concat(array))));
            }
            return list;
        }

        void CreateNewMap(Player p)
        {
            int num = p.group.smallMaps + p.group.mediumMaps + p.group.bigMaps;
            if (num == 0)
            {
                Player.SendMessage(p, "Your rank is not allowed to create a new map.");
                return;
            }
            List<MyMapInfo> myMapInfos = GetMyMapInfos(p);
            if (myMapInfos.Count >= num)
            {
                if (num == 1)
                {
                    Player.SendMessage(p, "Your only free map slot is used.");
                }
                else
                {
                    Player.SendMessage(p, "All of your " + num + " map slots are used.");
                }
                Player.SendMessage(p, "Remove an old map to be able to create a new one.");
                return;
            }
            Player.SendMessage(p, "------------- Map Creation -------------");
            Player.SendMessage(p, "%7(Write /a to abort.)");
            Player.SendMessage(p, "Write the map name:");
            string mapName = null;
            while (true)
            {
                mapName = p.ReadLine();
                if (mapName == null)
                {
                    return;
                }
                if (!Player.ValidName(mapName))
                {
                    Player.SendMessage(p, "The map name contains disallowed characters.");
                    Player.SendMessage(p, "Write a different name:");
                    continue;
                }
                if (mapName.Length > 20)
                {
                    Player.SendMessage(p, "The map name is too long.");
                    Player.SendMessage(p, "Write a different name:");
                    continue;
                }
                if (myMapInfos.FirstOrDefault(e => e.Name == mapName.ToLower()) == null)
                {
                    break;
                }
                Player.SendMessage(p, "The map named " + mapName + " already exists.");
                Player.SendMessage(p, "Write a different name:");
            }
            Player.SendMessage(p, "Map name: %a" + mapName.ToLower());
            IEnumerable<IGrouping<int, MyMapInfo>> source = myMapInfos.GroupBy(delegate(MyMapInfo m)
            {
                if (m.LevelFileInfo.BlockCount <= 262144)
                {
                    return 262144;
                }
                return m.LevelFileInfo.BlockCount <= 1048576 ? 1048576 : 8388608;
            });
            (from g in source
                where g.Key == 262144
                select from m in g
                    select m).Count();
            int num2 = (from g in source
                where g.Key == 1048576
                from m in g
                select m).Count();
            int num3 = (from g in source
                where g.Key == 8388608
                from m in g
                select m).Count();
            bool flag = p.group.mediumMaps + p.group.bigMaps - (num2 + num3) > 0;
            int num4 = p.group.mediumMaps - num2 < 0 ? -(p.group.mediumMaps - num2) : 0;
            bool flag2 = p.group.bigMaps - num3 - num4 > 0;
            Slot largestFreeSlot = flag2 ? Slot.Big : !flag ? Slot.Small : Slot.Medium;
            SendMessageWriteMapSize(p, largestFreeSlot);
            string text = null;
            int num5;
            int num6;
            int num7;
            while (true)
            {
                text = p.ReadLine();
                if (text == null)
                {
                    return;
                }
                text = text.ToLower();
                if (SplitDimensions(text).Length == 3)
                {
                    string[] array = SplitDimensions(text);
                    try
                    {
                        num5 = int.Parse(array[0]);
                        num6 = int.Parse(array[1]);
                        num7 = int.Parse(array[2]);
                    }
                    catch
                    {
                        Player.SendMessage(p, "The given dimensions were incorrect.");
                        Player.SendMessage(p, "Write the map size (eg 64x64x64):");
                        continue;
                    }
                    if (num5 < 16 || num6 < 16 || num7 < 16)
                    {
                        Player.SendMessage(p, "The dimensions have to be greater or equal to 16.");
                        Player.SendMessage(p, "Write the map size (eg 64x64x64):");
                        continue;
                    }
                    if (!IsPowerOfTwo((uint)num5) || !IsPowerOfTwo((uint)num6) || !IsPowerOfTwo((uint)num7))
                    {
                        Player.SendMessage(p, "The size values have to be a power of 2.");
                        Player.SendMessage(p, "For example: 16, 32, 64, 128, 256...");
                        Player.SendMessage(p, "Write the map size (eg 64x64x64):");
                        continue;
                    }
                    int num8 = num5 * num6 * num7;
                    if (num8 <= 262144)
                    {
                        break;
                    }
                    if (num8 <= 1048576)
                    {
                        if (flag)
                        {
                            break;
                        }
                        Player.SendMessage(p, "Sorry, you don't have any free slot for a medium size map.");
                        Player.SendMessage(p, "Write the map size (eg 64x64x64):");
                    }
                    else if (num8 <= 8388608)
                    {
                        if (flag2)
                        {
                            break;
                        }
                        Player.SendMessage(p, "Sorry, you don't have any free slot for a big size map.");
                        Player.SendMessage(p, "Write the map size (eg 64x64x64):");
                    }
                    else
                    {
                        Player.SendMessage(p, "Sorry, you can't own such a huge map.");
                        Player.SendMessage(p, "Write the map size (eg 64x64x64):");
                    }
                    continue;
                }
                switch (text)
                {
                    case "small":
                        num5 = 64;
                        num6 = 64;
                        num7 = 64;
                        break;
                    case "medium":
                        if (!flag)
                        {
                            Player.SendMessage(p, "Sorry, you don't have any free slot for a medium size map.");
                            SendMessageWriteMapSize(p, largestFreeSlot);
                            continue;
                        }
                        num5 = 128;
                        num6 = 64;
                        num7 = 128;
                        break;
                    case "large":
                    case "big":
                        if (!flag2)
                        {
                            Player.SendMessage(p, "Sorry, you don't have any free slot for a big size map.");
                            SendMessageWriteMapSize(p, largestFreeSlot);
                            continue;
                        }
                        num5 = 256;
                        num6 = 128;
                        num7 = 256;
                        break;
                    default:
                        Player.SendMessage(p, "The given size was incorrect.");
                        SendMessageWriteMapSize(p, largestFreeSlot);
                        continue;
                }
                break;
            }
            Player.SendMessage(p, string.Format("Map size: %a{0}x{1}x{2}", num5, num6, num7));
            Player.SendMessage(p, "Write the map theme (available: flat, pixel, mountains, island, forest, ocean, desert):");
            string text2 = null;
            while (true)
            {
                text2 = p.ReadLine();
                if (text2 == null)
                {
                    break;
                }
                switch (text2.ToLower())
                {
                    case "flat":
                    case "pixel":
                    case "mountains":
                    case "island":
                    case "forest":
                    case "ocean":
                    case "desert":
                    {
                        Player.SendMessage(p, "Theme: %a" + text2);
                        Level level = new Level(mapName, (ushort)num5, (ushort)num6, (ushort)num7, text2);
                        level.directoryPath = GetMyMapDirectoryPath(p.name);
                        level.mapType = MapType.MyMap;
                        level.IsPublic = false;
                        level.Save();
                        LoadAndSendToMap(p, mapName, p.name);
                        return;
                    }
                }
                Player.SendMessage(p, "Incorrect map theme.");
                Player.SendMessage(p, "Write the map theme (available: flat, pixel, mountains, island, forest, ocean, desert):");
            }
        }

        static string[] SplitDimensions(string mapSize)
        {
            return mapSize.Split(new char[2]
            {
                'x', ' '
            }, StringSplitOptions.RemoveEmptyEntries);
        }

        static void SendMessageWriteMapSize(Player p, Slot largestFreeSlot)
        {
            switch (largestFreeSlot)
            {
                case Slot.Big:
                    Player.SendMessage(p, "Write the map size (small/medium/big):");
                    break;
                case Slot.Medium:
                    Player.SendMessage(p, "Write the map size (small/medium):");
                    break;
                case Slot.Small:
                    Player.SendMessage(p, "Write the map size (eg small or 64x64x64 etc):");
                    break;
                default:
                    Player.SendMessage(p, "There's no free slot!");
                    break;
            }
        }

        bool IsPowerOfTwo(uint x)
        {
            if (x != 0)
            {
                return (x & x - 1) == 0;
            }
            return false;
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "%a/mymap [argument]%s where arguments are:");
            Player.SendMessage(p, "%anew%s - creates a new map,");
            Player.SendMessage(p, "%avisit%s - makes you visit a mymap map,");
            Player.SendMessage(p, "%amaps%s - displays a list of your maps,");
            Player.SendMessage(p, "%aback%s - sends you to the main map,");
            Player.SendMessage(p, "%adelete%s - lets you delete your mymap map,");
            Player.SendMessage(p, "%ainvite%s - you can invite another player,");
            Player.SendMessage(p, "%amore%s - shows you more arguments.");
        }

        void HelpMore(Player p)
        {
            Player.SendMessage(p, "Mymap arguments page 2:");
            Player.SendMessage(p, "%akick%s - kicks a player from your map,");
            Player.SendMessage(p, "%aallow%s - allows a player to build here,");
            Player.SendMessage(p, "%adisallow%s - removes a player from the allowed list,");
            Player.SendMessage(p, "%aallowed%s - displays a list of allowed to build,");
            Player.SendMessage(p, "%asetspawn%s - sets a map spawn,");
            Player.SendMessage(p, "%apublic/private%s - if public anyone can build,");
            Player.SendMessage(p, "%amotd%s - you can set motd for your map,");
            Player.SendMessage(p, "%ashortcuts%s - shortcuts for the above.");
        }

        void ShowShortcuts(Player p)
        {
            Player.SendMessage(p, "Available shortcuts:");
            Player.SendMessage(p, "%a/mymap%s - /mm,");
            Player.SendMessage(p, "%avisit%s - g goto v,");
            Player.SendMessage(p, "%amaps%s - levels list m,");
            Player.SendMessage(p, "%aback%s - exit leave,");
            Player.SendMessage(p, "%ainvite%s - inv,");
            Player.SendMessage(p, "%akick%s - k,");
            Player.SendMessage(p, "%aallowed%s - all,");
            Player.SendMessage(p, "Exemplary use: /mm inv Notch");
        }

        public static class InputReader
        {
            public static string StartReader(Player player, Func<string, bool> validator, Action whenInvalid)
            {
                string text;
                while (true)
                {
                    text = player.ReadLine();
                    if (text == null)
                    {
                        return null;
                    }
                    if (validator(text))
                    {
                        break;
                    }
                    whenInvalid();
                }
                return text;
            }
        }

        internal class MyMapInfo
        {
            public LevelFileInfo LevelFileInfo;

            public string Name;

            public MyMapInfo(string mapName, LevelFileInfo info)
            {
                Name = mapName;
                LevelFileInfo = info;
            }
        }

        enum Slot
        {
            None,
            Small,
            Medium,
            Big
        }
    }
}