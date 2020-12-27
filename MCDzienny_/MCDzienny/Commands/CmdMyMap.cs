using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MCDzienny.Misc;

namespace MCDzienny
{
    // Token: 0x02000066 RID: 102
    public class CmdMyMap : Command
    {
        // Token: 0x0400017F RID: 383
        private const int SizeSmall = 262144;

        // Token: 0x04000180 RID: 384
        private const int SizeMedium = 1048576;

        // Token: 0x04000181 RID: 385
        private const int SizeBig = 8388608;

        // Token: 0x04000183 RID: 387
        public static char[] PathSeparators =
        {
            '/',
            '\\'
        };

        // Token: 0x04000182 RID: 386
        private readonly string MyMapDirectory = "maps" + Path.DirectorySeparatorChar + "mymaps";

        // Token: 0x170000B5 RID: 181
        // (get) Token: 0x06000288 RID: 648 RVA: 0x0000DAB0 File Offset: 0x0000BCB0
        public override string name
        {
            get { return "mymap"; }
        }

        // Token: 0x170000B6 RID: 182
        // (get) Token: 0x06000289 RID: 649 RVA: 0x0000DAB8 File Offset: 0x0000BCB8
        public override string shortcut
        {
            get { return "mm"; }
        }

        // Token: 0x170000B7 RID: 183
        // (get) Token: 0x0600028A RID: 650 RVA: 0x0000DAC0 File Offset: 0x0000BCC0
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170000B8 RID: 184
        // (get) Token: 0x0600028B RID: 651 RVA: 0x0000DAC8 File Offset: 0x0000BCC8
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x170000B9 RID: 185
        // (get) Token: 0x0600028C RID: 652 RVA: 0x0000DACC File Offset: 0x0000BCCC
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x170000BA RID: 186
        // (get) Token: 0x0600028D RID: 653 RVA: 0x0000DAD0 File Offset: 0x0000BCD0
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x0600028E RID: 654 RVA: 0x0000DAD4 File Offset: 0x0000BCD4
        public override void Use(Player p, string message)
        {
            DirectoryUtil.CreateIfNotExists(MyMapDirectory);
            var message2 = new Message(message);
            var text = message2.ReadStringLower();
            string key;
            switch (key = text)
            {
                case "n":
                case "new":
                    CreateNewMap(p);
                    return;
                case "g":
                case "goto":
                case "v":
                case "visit":
                    VisitMap(p, message2);
                    return;
                case "back":
                case "exit":
                case "leave":
                    GoToMainMap(p);
                    return;
                case "del":
                case "delete":
                case "remove":
                    DeleteMap(p, message2);
                    return;
                case "rename":
                    RenameMap(p, message2);
                    return;
                case "levels":
                case "list":
                case "m":
                case "maps":
                    DisplayMapList(p, message2);
                    return;
                case "inv":
                case "invite":
                    InvitePlayer(p, message2);
                    return;
                case "ss":
                case "setspawn":
                    SetMapSpawn(p);
                    return;
                case "k":
                case "kick":
                    KickPlayer(p, message2);
                    return;
                case "all":
                case "allowed":
                    ShowAllowedPlayers(p);
                    return;
                case "allow":
                    AllowPlayers(p, message2);
                    return;
                case "disallow":
                    DisallowPlayers(p, message2);
                    return;
                case "public":
                    SetMapPublic(p, true);
                    return;
                case "private":
                    SetMapPublic(p, false);
                    return;
                case "more":
                    HelpMore(p);
                    return;
                case "motd":
                    SetMotd(p, message2);
                    return;
                case "s":
                case "shortcuts":
                    ShowShortcuts(p);
                    return;
            }

            Help(p);
        }

        // Token: 0x0600028F RID: 655 RVA: 0x0000DE00 File Offset: 0x0000C000
        private void RenameMap(Player p, Message msg)
        {
            if (msg.Count != 3)
            {
                Player.SendMessage(p, "Help:");
                Player.SendMessage(p, "/mm rename [oldmap] [newmap]");
                return;
            }

            var text = msg.ReadStringLower();
            if (!Player.ValidName(text))
            {
                Player.SendMessage(p, "Invalid map name.");
                return;
            }

            var text2 = msg.ReadStringLower();
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

            var myMapNames = GetMyMapNames(p.name);
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

            var myMapDirectoryPath = GetMyMapDirectoryPath(p.name.ToLower());
            var fileName = string.Concat(myMapDirectoryPath, Path.DirectorySeparatorChar, text, ".lvl");
            var fileName2 = string.Concat(myMapDirectoryPath, Path.DirectorySeparatorChar, text, ".properties");
            var fileInfo = new FileInfo(fileName);
            var destFileName = string.Concat(myMapDirectoryPath, Path.DirectorySeparatorChar, text2, ".lvl");
            var destFileName2 = string.Concat(myMapDirectoryPath, Path.DirectorySeparatorChar, text2, ".properties");
            var fileInfo2 = new FileInfo(fileName2);
            try
            {
                fileInfo.MoveTo(destFileName);
                if (fileInfo2.Exists) fileInfo2.MoveTo(destFileName2);
            }
            catch (Exception ex)
            {
                Player.SendMessage(p, "An error occured.");
                Player.SendMessage(p, ex.Message);
                Server.ErrorLog(ex);
                return;
            }

            var level = Level.FindExactMM(text, p.name);
            if (level != null) level.name = text2;
            Player.SendMessage(p, "The map " + text + " was renamed to " + text2);
        }

        // Token: 0x06000290 RID: 656 RVA: 0x0000E034 File Offset: 0x0000C234
        private void SetMotd(Player p, Message msg)
        {
            if (!IsOnOwnMap(p))
            {
                Player.SendMessage(p, "You can change this property only if you are on your map.");
                return;
            }

            var text = msg.ReadToEnd();
            if (text == null)
            {
                Player.SendMessage(p, "Write your map MOTD (message of the day):");
                text = p.ReadLine();
            }

            if (text == null) return;
            Player.SendMessage(p, "Your map new MOTD: " + text);
            p.level.motd = text;
            p.level.changed = true;
        }

        // Token: 0x06000291 RID: 657 RVA: 0x0000E0A0 File Offset: 0x0000C2A0
        private static bool IsOnOwnMap(Player p)
        {
            return p.level.mapType == MapType.MyMap && IsMapOwnedBy(p.level, p);
        }

        // Token: 0x06000292 RID: 658 RVA: 0x0000E0C0 File Offset: 0x0000C2C0
        private void ShowAllowedPlayers(Player p)
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
                    Player.SendMessage(p, JoinWithComma((from name in p.level.AllowedPlayers
                        select Player.RemoveEmailDomain(name)).ToArray()));
                }
                else
                {
                    Player.SendMessage(p, "Only the map owner can build here.");
                }
            }

            if (p.level.IsPublic) Player.SendMessage(p, "This map is currently public so anyone can build here.");
        }

        // Token: 0x06000293 RID: 659 RVA: 0x0000E188 File Offset: 0x0000C388
        private void DisallowPlayers(Player p, Message msg)
        {
            if (!IsOnOwnMap(p))
            {
                Player.SendMessage(p, "You can change this property only if you are on your map.");
                return;
            }

            var text = msg.ReadToEnd();
            if (text == null)
            {
                Player.SendMessage(p, "Write a player name or names to disallow them to build on this map:");
                Player.SendMessage(p, "%7(If you write more than one name separate them with a comma.)");
                text = p.ReadLine();
                if (text == null) return;
            }

            var array = (from e in text.Split(new[]
                {
                    ','
                }, StringSplitOptions.RemoveEmptyEntries)
                select e.Trim().ToLower()).ToArray();
            var list = new List<string>();
            var array2 = array;
            for (var i = 0; i < array2.Length; i++)
            {
                var player = array2[i];
                var text2 = p.level.AllowedPlayers.FirstOrDefault(e => e.StartsWith(player));
                if (text2 != null)
                {
                    p.level.AllowedPlayers.Remove(text2);
                    list.Add(text2);
                    var player2 = Player.FindExact(text2);
                    if (player2 != null)
                        Player.SendMessage(p,
                            string.Format("You are no longer allowed to build on map {0}.", p.level.name));
                }
            }

            if (list.Count == 1)
            {
                Player.SendMessage(p, string.Format("Player {0} is no longer allowed to build on this map.", list[0]));
                return;
            }

            if (list.Count > 1)
            {
                Player.SendMessage(p,
                    string.Format("Players {0} are no longer allowed to build on this map.",
                        JoinWithComma(list.ToArray())));
                return;
            }

            if (array.Length == 1)
            {
                Player.SendMessage(p, string.Format("Player {0} is not on the list of the allowed players.", array[0]));
                return;
            }

            if (array.Length > 1)
                Player.SendMessage(p,
                    string.Format("Players {0} are not on the list of the allowed players.", JoinWithComma(array)));
        }

        // Token: 0x06000294 RID: 660 RVA: 0x0000E330 File Offset: 0x0000C530
        private static string JoinWithComma(string[] array)
        {
            return string.Join(", ", array);
        }

        // Token: 0x06000295 RID: 661 RVA: 0x0000E340 File Offset: 0x0000C540
        private void AllowPlayers(Player p, Message msg)
        {
            if (!IsOnOwnMap(p))
            {
                Player.SendMessage(p, "You can change this property only if you are on your map.");
                return;
            }

            var text = msg.ReadToEnd();
            if (text == null)
            {
                Player.SendMessage(p, "Write a player name or names to allow them to build on this map:");
                Player.SendMessage(p, "%7(If you write more than one name separate them with a comma [,].)");
                text = p.ReadLine();
                if (text == null) return;
            }

            var array = text.Split(new[]
            {
                ','
            }, StringSplitOptions.RemoveEmptyEntries);
            var list = new List<Player>();
            var list2 = new List<string>();
            var list3 = new List<Player>();
            foreach (var text2 in array)
            {
                var player = Player.Find(text2);
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
                    list2.Add(text2);
                }
            }

            if (list.Count == 1)
                Player.GlobalMessageLevel(p.level,
                    string.Format("Player {0} was allowed to build on this map.", list[0].PublicName));
            else if (list.Count > 1)
                Player.GlobalMessageLevel(p.level, string.Format("Players {0} were allowed to build on this map.",
                    JoinWithComma((from e in list
                        select e.PublicName).ToArray())));
            if (list2.Count == 1)
                Player.SendMessage(p, string.Format("Player named {0} couldn't be found.", list2[0]));
            else if (list2.Count > 1)
                Player.SendMessage(p,
                    string.Format("Players named {0} couldn't be found.", JoinWithComma(list2.ToArray())));
            if (list3.Count == 1)
            {
                Player.SendMessage(p,
                    string.Format("Player {0} is already allowed to build here.", list3[0].PublicName));
                return;
            }

            if (list3.Count > 1)
                Player.SendMessage(p, string.Format("Players {0} are already allowed to build here.", JoinWithComma(
                    (from e in list3
                        select e.PublicName).ToArray())));
        }

        // Token: 0x06000296 RID: 662 RVA: 0x0000E578 File Offset: 0x0000C778
        private void SetMapPublic(Player p, bool isPublic)
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
                return;
            }

            Player.GlobalMessageLevel(p.level,
                "This map is now private. It means only allowed persons can build here.");
        }

        // Token: 0x06000297 RID: 663 RVA: 0x0000E610 File Offset: 0x0000C810
        private void KickPlayer(Player p, Message msg)
        {
            var text = msg.ReadStringLower();
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
                if (text == null) return;
            }
            else if (!Player.ValidName(text))
            {
                Player.SendMessage(p, "The given name contains a forbidden character.");
                return;
            }

            var player = Player.Find(text);
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
                Player.SendMessage(p,
                    string.Format("You can't kick {0} because he is not on your map.", player.PublicName));
                return;
            }

            player.SendToMap(Server.DefaultLevel);
            Player.SendMessage(player, string.Format("You were kicked by {0} from his map.", p.PublicName));
            Player.SendMessage(p, string.Format("Player {0} was kicked from your map.", player.PublicName));
        }

        // Token: 0x06000298 RID: 664 RVA: 0x0000E764 File Offset: 0x0000C964
        private void InvitePlayer(Player p, Message msg)
        {
            if (!IsOnOwnMap(p))
            {
                Player.SendMessage(p, "You can only invite if you are on one of your maps.");
                return;
            }

            var text = msg.ReadStringLower();
            if (text == null)
            {
                Player.SendMessage(p, "Who do you want to invite? Write the name:");
                for (;;)
                {
                    text = p.ReadLine();
                    if (text == null) break;
                    text = text.Trim().ToLower();
                    if (Player.ValidName(text)) goto IL_74;
                    Player.SendMessage(p, "The given name contains a forbidden character.");
                    Player.SendMessage(p, "Who do you want to invite? Write the name again:");
                }

                return;
            }

            if (!Player.ValidName(text))
            {
                Player.SendMessage(p, "The given name contains a forbidden character.");
                return;
            }

            IL_74:
            var player = Player.Find(text);
            if (player == null)
            {
                Player.SendMessage(p, string.Format("Couldn't find a player named {0}.", text));
                return;
            }

            player.ExtraData["invitation"] = new object[]
            {
                p.level.name,
                p.PublicName,
                DateTime.Now
            };
            Player.SendMessage(player, string.Format("You were invited by %a{0}%s to visit their map.", p.PublicName));
            Player.SendMessage(player, "Write /accept to accept the invitation.");
            Player.SendMessage(p, string.Format("You sent an invitation to {0}.", player.PublicName));
        }

        // Token: 0x06000299 RID: 665 RVA: 0x0000E880 File Offset: 0x0000CA80
        private void SetMapSpawn(Player p)
        {
            if (!IsOnOwnMap(p))
            {
                Player.SendMessage(p, "You are not allowed to set a map spawn on this map.");
                return;
            }

            new CmdSetspawn().Use(p, "");
        }

        // Token: 0x0600029A RID: 666 RVA: 0x0000E8A8 File Offset: 0x0000CAA8
        private void DeleteMap(Player p, Message msg)
        {
            var mapName = msg.ReadStringLower();
            if (mapName == null)
            {
                Player.SendMessage(p, "Write the map name to delete:");
                for (;;)
                {
                    mapName = p.ReadLine();
                    if (mapName == null) break;
                    mapName = mapName.Trim().ToLower();
                    if (Player.ValidName(mapName)) goto IL_BD;
                    Player.SendMessage(p, "The given map name contains a forbidden character.");
                    Player.SendMessage(p, "Write the map name to delete:");
                }

                return;
            }

            if (!Player.ValidName(mapName))
            {
                Player.SendMessage(p, "The given map name contains a forbidden character.");
                return;
            }

            IL_BD:
            var myMapDirectoryPath = GetMyMapDirectoryPath(p.name);
            if (!File.Exists(string.Concat(myMapDirectoryPath, Path.DirectorySeparatorChar, mapName, ".lvl")))
            {
                Player.SendMessage(p, string.Format("You don't have a map named {0}.", mapName));
                return;
            }

            Player.SendMessage(p, string.Format("Delete map {0}? (Yes/No):", mapName));
            for (;;)
            {
                var text = p.ReadLine();
                if (text == null) break;
                text = text.Trim().ToLower();
                if (text == "no") goto Block_7;
                if (text == "yes") goto IL_1D3;
                Player.SendMessage(p, string.Format("Delete map {0}? (Yes/No):", mapName));
                text = p.ReadLine();
            }

            Player.SendMessage(p, "Map deletion was aborted.");
            return;
            Block_7:
            Player.SendMessage(p, "Map deletion was aborted.");
            return;
            IL_1D3:
            var level = Server.levels.SingleOrDefault(l =>
                l.mapType == MapType.MyMap && l.name == mapName && IsMapOwnedBy(l, p));
            if (level != null) level.Unload();
            File.Delete(string.Concat(myMapDirectoryPath, Path.DirectorySeparatorChar, mapName, ".lvl"));
            FileUtil.DeleteIfExists(string.Concat(myMapDirectoryPath, Path.DirectorySeparatorChar, mapName,
                ".properties"));
            var parameters = new Dictionary<string, object>
            {
                {
                    "@MapName",
                    mapName.ToLower()
                },
                {
                    "@Owner",
                    p.name.ToLower()
                }
            };
            DBInterface.ExecuteQuery("DELETE FROM MapList WHERE MapName = @MapName AND Owner = @Owner", parameters);
            Player.GlobalMessage(string.Format("Map {0}/{1} was deleted.", p.PublicName, mapName));
        }

        // Token: 0x0600029B RID: 667 RVA: 0x0000EB90 File Offset: 0x0000CD90
        internal static bool IsMapOwnedBy(Level l, Player p)
        {
            return l.Owner.ToLower() == p.name.ToLower();
        }

        // Token: 0x0600029C RID: 668 RVA: 0x0000EBB0 File Offset: 0x0000CDB0
        private void GoToMainMap(Player p)
        {
            if (p.level == Server.mainLevel)
            {
                Player.SendMessage(p, "You are already on the main map.");
                return;
            }

            new CmdGoto().Use(p, Server.DefaultLevel.name);
        }

        // Token: 0x0600029D RID: 669 RVA: 0x0000EBE0 File Offset: 0x0000CDE0
        private void VisitMap(Player p, Message msg)
        {
            var mapName = msg.ReadStringLower();
            var owner = p.name.ToLower();
            if (mapName == null)
            {
                Player.SendMessage(p, "Write the map name to visit:");
                Player.SendMessage(p, "%7(If it's someone else map type: [playerName]/[mapName] ,eg notch/coolmap)");
                for (;;)
                {
                    mapName = p.ReadLine();
                    if (mapName == null) break;
                    mapName = mapName.Trim().ToLower();
                    if (ContainsPathSeparator(mapName) && SplitGivenMapPath(mapName).Length != 2)
                    {
                        Player.SendMessage(p, "Incorrect map path.");
                        Player.SendMessage(p, "If it's someone else map type: [player]/[map] ,eg notch/coolmap");
                    }
                    else
                    {
                        if (Player.ValidName(mapName)) goto IL_11B;
                        Player.SendMessage(p, "The given map name contains a forbidden character.");
                        Player.SendMessage(p, "Write the map name again:");
                    }
                }

                return;
            }

            IL_11B:
            if (ContainsPathSeparator(mapName))
            {
                var array = SplitGivenMapPath(mapName);
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
                var path = GetMyMapDirectoryPath(owner).Replace(owner, "");
                if (!Directory.Exists(path))
                {
                    Player.SendMessage(p, string.Format("Player named {0} doesn't own any maps.", owner));
                    return;
                }

                var text = Directory.GetDirectories(path).FirstOrDefault(d => d.Contains(owner));
                if (text == null)
                {
                    Player.SendMessage(p, string.Format("Player named {0} doesn't own any maps.", owner));
                    return;
                }

                owner = GetPathTopElement(text);
            }

            var level = Server.levels.SingleOrDefault(l =>
                l.mapType == MapType.MyMap && l.name == mapName && IsMapOwnedBy(l, p));
            if (level != null)
            {
                p.SendToMap(level);
                return;
            }

            var path2 = string.Concat(GetMyMapDirectoryPath(owner), Path.DirectorySeparatorChar, mapName, ".lvl");
            if (File.Exists(path2))
            {
                LoadAndSendToMap(p, mapName, owner);
                return;
            }

            level = Server.levels.FirstOrDefault(l =>
                l.mapType == MapType.MyMap && l.Owner.Contains(owner.ToLower()) && l.name.Contains(mapName.ToLower()));
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

                level = Server.levels.FirstOrDefault(l =>
                    l.mapType == MapType.MyMap && l.name.Contains(mapName.ToLower()));
                if (level != null)
                {
                    p.SendToMap(level);
                    return;
                }
            }

            if (p.name == owner)
            {
                Player.SendMessage(p, string.Format("You don't have a map named {0}.", mapName));
                return;
            }

            Player.SendMessage(p, string.Format("Player {0} doesn't have a map named {1}.", owner, mapName));
        }

        // Token: 0x0600029E RID: 670 RVA: 0x0000EFF4 File Offset: 0x0000D1F4
        private static string[] SplitGivenMapPath(string mapName)
        {
            return mapName.Split(PathSeparators, StringSplitOptions.RemoveEmptyEntries);
        }

        // Token: 0x0600029F RID: 671 RVA: 0x0000F004 File Offset: 0x0000D204
        private static bool ContainsPathSeparator(string mapName)
        {
            foreach (var value in PathSeparators)
                if (mapName.Contains(value))
                    return true;
            return false;
        }

        // Token: 0x060002A0 RID: 672 RVA: 0x0000F03C File Offset: 0x0000D23C
        internal string GetPathTopElement(string path)
        {
            var num = path.LastIndexOfAny(new[]
            {
                Path.DirectorySeparatorChar,
                '/',
                '\\'
            });
            if (num < 0) return path;
            return path.Remove(0, num + 1);
        }

        // Token: 0x060002A1 RID: 673 RVA: 0x0000F07C File Offset: 0x0000D27C
        internal void LoadAndSendToMap(Player p, string mapName, string owner)
        {
            var level = Level.Load(GetMyMapDirectoryPath(owner), mapName.ToLower(), owner.ToLower(), MapType.MyMap,
                true);
            if (level == null)
            {
                Player.SendMessage(p, "%cUnable to load the map {0}.", mapName);
                return;
            }

            Server.AddLevel(level);
            Player.GlobalMessage(string.Format("Map %3{0}%s owned by %3{1}%s was loaded.", mapName,
                Player.RemoveEmailDomain(owner)));
            p.SendToMap(level);
            Player.GlobalMessage(string.Concat(p.color, "*", p.PublicName, " went to %b", p.level.PublicName));
            Server.s.Log(p.PublicName + " went to " + p.level.PublicName);
        }

        // Token: 0x060002A2 RID: 674 RVA: 0x0000F14C File Offset: 0x0000D34C
        private void DisplayMapList(Player p, Message msg)
        {
            var text = msg.ReadStringLower();
            if (text == null)
            {
                Player.SendMessage(p, "Your maps:");
                Player.SendMessage(p, JoinWithComma((from e in GetMyMapNames(p.name)
                    orderby e
                    select e).ToArray()));
                return;
            }

            if (!Player.ValidName(text))
            {
                Player.SendMessage(p, "Error: The given owner name contains a forbidden character.");
                return;
            }

            Player.SendMessage(p, string.Format("{0} maps:", text));
            Player.SendMessage(p, JoinWithComma((from e in GetMyMapNames(text)
                orderby e
                select e).ToArray()));
        }

        // Token: 0x060002A3 RID: 675 RVA: 0x0000F20C File Offset: 0x0000D40C
        internal string GetMyMapDirectoryPath(string name)
        {
            var text = name.ToLower();
            var stringBuilder = new StringBuilder();
            var num = 0;
            while (num < 3 && num < text.Length)
            {
                stringBuilder.Append(Path.DirectorySeparatorChar + text[num].ToString());
                num++;
            }

            return string.Concat(MyMapDirectory, stringBuilder.ToString(), Path.DirectorySeparatorChar, name.ToLower());
        }

        // Token: 0x060002A4 RID: 676 RVA: 0x0000F2A0 File Offset: 0x0000D4A0
        private List<string> GetMyMapNames(string name)
        {
            var myMapDirectoryPath = GetMyMapDirectoryPath(name);
            DirectoryUtil.CreateIfNotExists(myMapDirectoryPath);
            if (!Directory.Exists(myMapDirectoryPath)) return new List<string>();
            return (from e in Directory.GetFiles(Environment.CurrentDirectory + Path.DirectorySeparatorChar +
                                                 myMapDirectoryPath)
                where e.EndsWith(".lvl")
                select GetPathTopElement(e).Replace(".lvl", "")).ToList();
        }

        // Token: 0x060002A5 RID: 677 RVA: 0x0000F31C File Offset: 0x0000D51C
        private List<MyMapInfo> GetMyMapInfos(Player p)
        {
            var list = new List<MyMapInfo>();
            var myMapDirectoryPath = GetMyMapDirectoryPath(p.name);
            foreach (var text in GetMyMapNames(p.name))
                list.Add(new MyMapInfo(text.ToLower(),
                    Level.ReadLevelInfo(string.Concat(Environment.CurrentDirectory,
                        Path.DirectorySeparatorChar.ToString(), myMapDirectoryPath,
                        Path.DirectorySeparatorChar.ToString(), text, ".lvl"))));
            return list;
        }

        // Token: 0x060002A6 RID: 678 RVA: 0x0000F3E8 File Offset: 0x0000D5E8
        private void CreateNewMap(Player p)
        {
            var num = p.group.smallMaps + p.group.mediumMaps + p.group.bigMaps;
            if (num == 0)
            {
                Player.SendMessage(p, "Your rank is not allowed to create a new map.");
                return;
            }

            var myMapInfos = GetMyMapInfos(p);
            if (myMapInfos.Count >= num)
            {
                if (num == 1)
                    Player.SendMessage(p, "Your only free map slot is used.");
                else
                    Player.SendMessage(p, "All of your " + num + " map slots are used.");
                Player.SendMessage(p, "Remove an old map to be able to create a new one.");
                return;
            }

            Player.SendMessage(p, "------------- Map Creation -------------");
            Player.SendMessage(p, "%7(Write /a to abort.)");
            Player.SendMessage(p, "Write the map name:");
            string mapName = null;
            for (;;)
            {
                mapName = p.ReadLine();
                if (mapName == null) break;
                if (!Player.ValidName(mapName))
                {
                    Player.SendMessage(p, "The map name contains disallowed characters.");
                    Player.SendMessage(p, "Write a different name:");
                }
                else if (mapName.Length > 20)
                {
                    Player.SendMessage(p, "The map name is too long.");
                    Player.SendMessage(p, "Write a different name:");
                }
                else
                {
                    if (myMapInfos.FirstOrDefault(e => e.Name == mapName.ToLower()) == null) goto IL_15D;
                    Player.SendMessage(p, "The map named " + mapName + " already exists.");
                    Player.SendMessage(p, "Write a different name:");
                }
            }

            return;
            IL_15D:
            Player.SendMessage(p, "Map name: %a" + mapName.ToLower());
            var source = myMapInfos.GroupBy(delegate(MyMapInfo m)
            {
                if (m.LevelFileInfo.BlockCount <= 262144) return 262144;
                if (m.LevelFileInfo.BlockCount <= 1048576) return 1048576;
                return 8388608;
            });
            (from g in source
                where g.Key == 262144
                select
                    from m in g
                    select m).Count();
            var num2 = (from g in source
                where g.Key == 1048576
                from m in g
                select m).Count();
            var num3 = (from g in source
                where g.Key == 8388608
                from m in g
                select m).Count();
            var flag = p.group.mediumMaps + p.group.bigMaps - (num2 + num3) > 0;
            var num4 = p.group.mediumMaps - num2 < 0 ? -(p.group.mediumMaps - num2) : 0;
            var flag2 = p.group.bigMaps - num3 - num4 > 0;
            var largestFreeSlot = flag2 ? Slot.Big : flag ? Slot.Medium : Slot.Small;
            SendMessageWriteMapSize(p, largestFreeSlot);
            int num5;
            int num6;
            int num7;
            for (;;)
            {
                var text = p.ReadLine();
                if (text == null) break;
                text = text.ToLower();
                if (SplitDimensions(text).Length == 3)
                {
                    var array = SplitDimensions(text);
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
                    }
                    else if (!IsPowerOfTwo((uint) num5) || !IsPowerOfTwo((uint) num6) || !IsPowerOfTwo((uint) num7))
                    {
                        Player.SendMessage(p, "The size values have to be a power of 2.");
                        Player.SendMessage(p, "For example: 16, 32, 64, 128, 256...");
                        Player.SendMessage(p, "Write the map size (eg 64x64x64):");
                    }
                    else
                    {
                        var num8 = num5 * num6 * num7;
                        if (num8 <= 262144) goto IL_55E;
                        if (num8 <= 1048576)
                        {
                            if (flag) goto IL_55E;
                            Player.SendMessage(p, "Sorry, you don't have any free slot for a medium size map.");
                            Player.SendMessage(p, "Write the map size (eg 64x64x64):");
                        }
                        else if (num8 <= 8388608)
                        {
                            if (flag2) goto IL_55E;
                            Player.SendMessage(p, "Sorry, you don't have any free slot for a big size map.");
                            Player.SendMessage(p, "Write the map size (eg 64x64x64):");
                        }
                        else
                        {
                            Player.SendMessage(p, "Sorry, you can't own such a huge map.");
                            Player.SendMessage(p, "Write the map size (eg 64x64x64):");
                        }
                    }
                }
                else
                {
                    string a;
                    if ((a = text) != null)
                    {
                        if (a == "small") goto IL_4D5;
                        if (!(a == "medium"))
                        {
                            if (a == "large" || a == "big")
                            {
                                if (!flag2)
                                {
                                    Player.SendMessage(p, "Sorry, you don't have any free slot for a big size map.");
                                    SendMessageWriteMapSize(p, largestFreeSlot);
                                    continue;
                                }

                                goto IL_52F;
                            }
                        }
                        else
                        {
                            if (!flag)
                            {
                                Player.SendMessage(p, "Sorry, you don't have any free slot for a medium size map.");
                                SendMessageWriteMapSize(p, largestFreeSlot);
                                continue;
                            }

                            goto IL_4FF;
                        }
                    }

                    Player.SendMessage(p, "The given size was incorrect.");
                    SendMessageWriteMapSize(p, largestFreeSlot);
                }
            }

            return;
            IL_4D5:
            num5 = 64;
            num6 = 64;
            num7 = 64;
            goto IL_55E;
            IL_4FF:
            num5 = 128;
            num6 = 64;
            num7 = 128;
            goto IL_55E;
            IL_52F:
            num5 = 256;
            num6 = 128;
            num7 = 256;
            IL_55E:
            Player.SendMessage(p, string.Format("Map size: %a{0}x{1}x{2}", num5, num6, num7));
            Player.SendMessage(p,
                "Write the map theme (available: flat, pixel, mountains, island, forest, ocean, desert):");
            string text2;
            for (;;)
            {
                text2 = p.ReadLine();
                if (text2 == null) break;
                string key;
                switch (key = text2.ToLower())
                {
                    case "flat":
                    case "pixel":
                    case "mountains":
                    case "island":
                    case "forest":
                    case "ocean":
                    case "desert":
                        goto IL_667;
                }

                Player.SendMessage(p, "Incorrect map theme.");
                Player.SendMessage(p,
                    "Write the map theme (available: flat, pixel, mountains, island, forest, ocean, desert):");
            }

            return;
            IL_667:
            Player.SendMessage(p, "Theme: %a" + text2);
            new Level(mapName, (ushort) num5, (ushort) num6, (ushort) num7, text2)
            {
                directoryPath = GetMyMapDirectoryPath(p.name),
                mapType = MapType.MyMap,
                IsPublic = false
            }.Save();
            LoadAndSendToMap(p, mapName, p.name);
        }

        // Token: 0x060002A7 RID: 679 RVA: 0x0000FAD8 File Offset: 0x0000DCD8
        private static string[] SplitDimensions(string mapSize)
        {
            return mapSize.Split(new[]
            {
                'x',
                ' '
            }, StringSplitOptions.RemoveEmptyEntries);
        }

        // Token: 0x060002A8 RID: 680 RVA: 0x0000FB00 File Offset: 0x0000DD00
        private static void SendMessageWriteMapSize(Player p, Slot largestFreeSlot)
        {
            switch (largestFreeSlot)
            {
                case Slot.Small:
                    Player.SendMessage(p, "Write the map size (eg small or 64x64x64 etc):");
                    return;
                case Slot.Medium:
                    Player.SendMessage(p, "Write the map size (small/medium):");
                    return;
                case Slot.Big:
                    Player.SendMessage(p, "Write the map size (small/medium/big):");
                    return;
                default:
                    Player.SendMessage(p, "There's no free slot!");
                    return;
            }
        }

        // Token: 0x060002A9 RID: 681 RVA: 0x0000FB54 File Offset: 0x0000DD54
        private bool IsPowerOfTwo(uint x)
        {
            return x != 0U && (x & (x - 1U)) == 0U;
        }

        // Token: 0x060002AA RID: 682 RVA: 0x0000FB64 File Offset: 0x0000DD64
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

        // Token: 0x060002AB RID: 683 RVA: 0x0000FBCC File Offset: 0x0000DDCC
        private void HelpMore(Player p)
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

        // Token: 0x060002AC RID: 684 RVA: 0x0000FC3C File Offset: 0x0000DE3C
        private void ShowShortcuts(Player p)
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

        // Token: 0x02000067 RID: 103
        public static class InputReader
        {
            // Token: 0x060002C2 RID: 706 RVA: 0x0000FE0C File Offset: 0x0000E00C
            public static string StartReader(Player player, Func<string, bool> validator, Action whenInvalid)
            {
                for (;;)
                {
                    var text = player.ReadLine();
                    if (text == null) break;
                    if (validator(text)) return text;
                    whenInvalid();
                }

                return null;
            }
        }

        // Token: 0x02000068 RID: 104
        internal class MyMapInfo
        {
            // Token: 0x04000196 RID: 406
            public LevelFileInfo LevelFileInfo;

            // Token: 0x04000197 RID: 407
            public string Name;

            // Token: 0x060002C3 RID: 707 RVA: 0x0000FE3C File Offset: 0x0000E03C
            public MyMapInfo(string mapName, LevelFileInfo info)
            {
                Name = mapName;
                LevelFileInfo = info;
            }
        }

        // Token: 0x02000069 RID: 105
        private enum Slot
        {
            // Token: 0x04000199 RID: 409
            None,

            // Token: 0x0400019A RID: 410
            Small,

            // Token: 0x0400019B RID: 411
            Medium,

            // Token: 0x0400019C RID: 412
            Big
        }
    }
}