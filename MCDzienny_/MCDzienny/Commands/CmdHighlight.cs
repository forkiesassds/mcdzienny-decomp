using System;
using System.IO;

namespace MCDzienny
{
    // Token: 0x020002FC RID: 764
    public class CmdHighlight : Command
    {
        // Token: 0x1700088F RID: 2191
        // (get) Token: 0x06001599 RID: 5529 RVA: 0x00076B88 File Offset: 0x00074D88
        public override string name
        {
            get { return "highlight"; }
        }

        // Token: 0x17000890 RID: 2192
        // (get) Token: 0x0600159A RID: 5530 RVA: 0x00076B90 File Offset: 0x00074D90
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000891 RID: 2193
        // (get) Token: 0x0600159B RID: 5531 RVA: 0x00076B98 File Offset: 0x00074D98
        public override string type
        {
            get { return "moderation"; }
        }

        // Token: 0x17000892 RID: 2194
        // (get) Token: 0x0600159C RID: 5532 RVA: 0x00076BA0 File Offset: 0x00074DA0
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000893 RID: 2195
        // (get) Token: 0x0600159D RID: 5533 RVA: 0x00076BA4 File Offset: 0x00074DA4
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x17000894 RID: 2196
        // (get) Token: 0x0600159E RID: 5534 RVA: 0x00076BA8 File Offset: 0x00074DA8
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x060015A0 RID: 5536 RVA: 0x00076BB4 File Offset: 0x00074DB4
        public override void Use(Player p, string message)
        {
            var num = 0L;
            var flag = false;
            if (message == "")
            {
                message = p.name + " 300";
            }
            else if (message.Split(' ').Length == 2)
            {
                if (!long.TryParse(message.Split(' ')[1], out num))
                {
                    Player.SendMessage(p, "Invalid seconds.");
                    return;
                }
            }
            else if (long.TryParse(message, out num))
            {
                if (p != null) message = p.name + " " + message;
            }
            else
            {
                num = 300L;
                message += " 300";
            }

            if (num == 0L) num = 5400L;
            var player = Player.Find(message.Split(' ')[0]);
            if (player != null)
            {
                message = player.name + " " + num;
                flag = true;
                HighlightBlocksFromCache(p, num, player);
            }

            try
            {
                if (Directory.Exists("extra/undo/" + message.Split(' ')[0]))
                {
                    var directoryInfo = new DirectoryInfo("extra/undo/" + message.Split(' ')[0]);
                    for (var i = 0; i < directoryInfo.GetFiles("*.undo").Length; i++)
                    {
                        var fileContent = File
                            .ReadAllText(string.Concat("extra/undo/", message.Split(' ')[0], "/", i, ".undo"))
                            .Split(' ');
                        HighlightBlocks(fileContent, num, p);
                    }

                    flag = true;
                }

                if (Directory.Exists("extra/undoPrevious/" + message.Split(' ')[0]))
                {
                    var directoryInfo = new DirectoryInfo("extra/undoPrevious/" + message.Split(' ')[0]);
                    for (var j = 0; j < directoryInfo.GetFiles("*.undo").Length; j++)
                    {
                        var fileContent = File
                            .ReadAllText(string.Concat("extra/undoPrevious/", message.Split(' ')[0], "/", j, ".undo"))
                            .Split(' ');
                        HighlightBlocks(fileContent, num, p);
                    }

                    flag = true;
                }

                if (flag)
                {
                    Player.SendMessage(p,
                        string.Format("Now highlighting &b{0}%s seconds for {1}", num,
                            Server.FindColor(message.Split(' ')[0]) + message.Split(' ')[0]));
                    Player.SendMessage(p, "&cUse /reveal to un-highlight");
                }
                else
                {
                    Player.SendMessage(p, "Could not find player specified.");
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x060015A1 RID: 5537 RVA: 0x00076F20 File Offset: 0x00075120
        private static void HighlightBlocksFromCache(Player p, long seconds, Player who)
        {
            var i = 0;
            for (i = who.UndoBuffer.Count - 1; i >= 0; i--)
                try
                {
                    var undoPos = who.UndoBuffer[i];
                    var level = Level.Find(undoPos.mapName);
                    if (level == p.level)
                    {
                        var tile = level.GetTile(undoPos.x, undoPos.y, undoPos.z);
                        if (!(undoPos.timePlaced.AddSeconds(seconds) >= DateTime.Now)) break;
                        if (tile == undoPos.newtype || Block.Convert(tile) == 8 || Block.Convert(tile) == 10)
                        {
                            if (tile == 0 || Block.Convert(tile) == 8 || Block.Convert(tile) == 10)
                                p.SendBlockchange(undoPos.x, undoPos.y, undoPos.z, 21);
                            else
                                p.SendBlockchange(undoPos.x, undoPos.y, undoPos.z, 25);
                        }
                    }
                }
                catch
                {
                }
        }

        // Token: 0x060015A2 RID: 5538 RVA: 0x00077034 File Offset: 0x00075234
        private static void UnhighlightBlocksFromCache(Player p, long seconds, Player who)
        {
            var i = 0;
            for (i = who.UndoBuffer.Count - 1; i >= 0; i--)
                try
                {
                    var undoPos = who.UndoBuffer[i];
                    var level = Level.Find(undoPos.mapName);
                    if (level == p.level)
                    {
                        var tile = level.GetTile(undoPos.x, undoPos.y, undoPos.z);
                        if (!(undoPos.timePlaced.AddSeconds(seconds) >= DateTime.Now)) break;
                        if (tile == undoPos.newtype || Block.Convert(tile) == 8 || Block.Convert(tile) == 10)
                            p.SendBlockchange(undoPos.x, undoPos.y, undoPos.z,
                                level.GetTile(undoPos.x, undoPos.y, undoPos.z));
                    }
                }
                catch
                {
                }
        }

        // Token: 0x060015A3 RID: 5539 RVA: 0x0007712C File Offset: 0x0007532C
        public void HighlightBlocks(string[] fileContent, long seconds, Player p)
        {
            for (var i = fileContent.Length / 7; i >= 0; i--)
                try
                {
                    if (!(Convert.ToDateTime(fileContent[i * 7 + 4].Replace('&', ' ')).AddSeconds(seconds) >=
                          DateTime.Now)) break;
                    var level = Level.Find(fileContent[i * 7]);
                    if (level != null && level == p.level)
                    {
                        Player.UndoPos undoPos;
                        undoPos.mapName = level.name;
                        undoPos.x = Convert.ToUInt16(fileContent[i * 7 + 1]);
                        undoPos.y = Convert.ToUInt16(fileContent[i * 7 + 2]);
                        undoPos.z = Convert.ToUInt16(fileContent[i * 7 + 3]);
                        undoPos.type = level.GetTile(undoPos.x, undoPos.y, undoPos.z);
                        if (undoPos.type == Convert.ToByte(fileContent[i * 7 + 6]) ||
                            Block.Convert(undoPos.type) == 8 || Block.Convert(undoPos.type) == 10)
                        {
                            if (undoPos.type == 0 || Block.Convert(undoPos.type) == 8 ||
                                Block.Convert(undoPos.type) == 10)
                                p.SendBlockchange(undoPos.x, undoPos.y, undoPos.z, 21);
                            else
                                p.SendBlockchange(undoPos.x, undoPos.y, undoPos.z, 25);
                        }
                    }
                }
                catch
                {
                }
        }

        // Token: 0x060015A4 RID: 5540 RVA: 0x000772C0 File Offset: 0x000754C0
        private static void UnhilightBlocks(string[] fileContent, long seconds, Player p)
        {
            for (var i = fileContent.Length / 7; i >= 0; i--)
                try
                {
                    if (!(Convert.ToDateTime(fileContent[i * 7 + 4].Replace('&', ' ')).AddSeconds(seconds) >=
                          DateTime.Now)) break;
                    var level = Level.Find(fileContent[i * 7]);
                    if (level != null && level == p.level)
                    {
                        Player.UndoPos undoPos;
                        undoPos.mapName = level.name;
                        undoPos.x = Convert.ToUInt16(fileContent[i * 7 + 1]);
                        undoPos.y = Convert.ToUInt16(fileContent[i * 7 + 2]);
                        undoPos.z = Convert.ToUInt16(fileContent[i * 7 + 3]);
                        undoPos.type = level.GetTile(undoPos.x, undoPos.y, undoPos.z);
                        if (undoPos.type == Convert.ToByte(fileContent[i * 7 + 6]) ||
                            Block.Convert(undoPos.type) == 8 || Block.Convert(undoPos.type) == 10)
                            p.SendBlockchange(undoPos.x, undoPos.y, undoPos.z,
                                level.GetTile(undoPos.x, undoPos.y, undoPos.z));
                    }
                }
                catch
                {
                }
        }

        // Token: 0x060015A5 RID: 5541 RVA: 0x00077428 File Offset: 0x00075628
        public override void Help(Player p)
        {
            Player.SendMessage(p,
                "/highlight [player] [seconds] - Highlights blocks modified by [player] in the last [seconds]");
            Player.SendMessage(p, "/highlight [player] 0 - Will highlight 30 minutes");
            Player.SendMessage(p, "&c/highlight cannot be disabled, you must use /reveal to un-highlight");
        }
    }
}