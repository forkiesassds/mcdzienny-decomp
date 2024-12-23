using System;
using System.IO;

namespace MCDzienny
{
    public class CmdHighlight : Command
    {
        public override string name { get { return "highlight"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "moderation"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            long result = 0L;
            bool flag = false;
            if (message == "")
            {
                message = p.name + " 300";
            }
            else if (message.Split(' ').Length == 2)
            {
                if (!long.TryParse(message.Split(' ')[1], out result))
                {
                    Player.SendMessage(p, "Invalid seconds.");
                    return;
                }
            }
            else if (long.TryParse(message, out result))
            {
                if (p != null)
                {
                    message = p.name + " " + message;
                }
            }
            else
            {
                result = 300L;
                message += " 300";
            }
            if (result == 0)
            {
                result = 5400L;
            }
            Player player = Player.Find(message.Split(' ')[0]);
            if (player != null)
            {
                message = player.name + " " + result;
                flag = true;
                HighlightBlocksFromCache(p, result, player);
            }
            try
            {
                if (Directory.Exists("extra/undo/" + message.Split(' ')[0]))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo("extra/undo/" + message.Split(' ')[0]);
                    for (int i = 0; i < directoryInfo.GetFiles("*.undo").Length; i++)
                    {
                        string[] fileContent = File.ReadAllText("extra/undo/" + message.Split(' ')[0] + "/" + i + ".undo").Split(' ');
                        HighlightBlocks(fileContent, result, p);
                    }
                    flag = true;
                }
                if (Directory.Exists("extra/undoPrevious/" + message.Split(' ')[0]))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo("extra/undoPrevious/" + message.Split(' ')[0]);
                    for (int j = 0; j < directoryInfo.GetFiles("*.undo").Length; j++)
                    {
                        string[] fileContent = File.ReadAllText("extra/undoPrevious/" + message.Split(' ')[0] + "/" + j + ".undo").Split(' ');
                        HighlightBlocks(fileContent, result, p);
                    }
                    flag = true;
                }
                if (flag)
                {
                    Player.SendMessage(p,
                                       string.Format("Now highlighting &b{0}%s seconds for {1}", result, Server.FindColor(message.Split(' ')[0]) + message.Split(' ')[0]));
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

        static void HighlightBlocksFromCache(Player p, long seconds, Player who)
        {
            int num = 0;
            for (num = who.UndoBuffer.Count - 1; num >= 0; num--)
            {
                try
                {
                    Player.UndoPos undoPos = who.UndoBuffer[num];
                    Level level = Level.Find(undoPos.mapName);
                    if (level == p.level)
                    {
                        byte tile = level.GetTile(undoPos.x, undoPos.y, undoPos.z);
                        if (!(undoPos.timePlaced.AddSeconds(seconds) >= DateTime.Now))
                        {
                            break;
                        }
                        if (tile == undoPos.newtype || Block.Convert(tile) == 8 || Block.Convert(tile) == 10)
                        {
                            if (tile == 0 || Block.Convert(tile) == 8 || Block.Convert(tile) == 10)
                            {
                                p.SendBlockchange(undoPos.x, undoPos.y, undoPos.z, 21);
                            }
                            else
                            {
                                p.SendBlockchange(undoPos.x, undoPos.y, undoPos.z, 25);
                            }
                        }
                    }
                }
                catch {}
            }
        }

        static void UnhighlightBlocksFromCache(Player p, long seconds, Player who)
        {
            int num = 0;
            for (num = who.UndoBuffer.Count - 1; num >= 0; num--)
            {
                try
                {
                    Player.UndoPos undoPos = who.UndoBuffer[num];
                    Level level = Level.Find(undoPos.mapName);
                    if (level == p.level)
                    {
                        byte tile = level.GetTile(undoPos.x, undoPos.y, undoPos.z);
                        if (!(undoPos.timePlaced.AddSeconds(seconds) >= DateTime.Now))
                        {
                            break;
                        }
                        if (tile == undoPos.newtype || Block.Convert(tile) == 8 || Block.Convert(tile) == 10)
                        {
                            p.SendBlockchange(undoPos.x, undoPos.y, undoPos.z, level.GetTile(undoPos.x, undoPos.y, undoPos.z));
                        }
                    }
                }
                catch {}
            }
        }

        public void HighlightBlocks(string[] fileContent, long seconds, Player p)
        {
            Player.UndoPos undoPos = default(Player.UndoPos);
            for (int num = fileContent.Length / 7; num >= 0; num--)
            {
                try
                {
                    if (!(Convert.ToDateTime(fileContent[num * 7 + 4].Replace('&', ' ')).AddSeconds(seconds) >= DateTime.Now))
                    {
                        break;
                    }
                    Level level = Level.Find(fileContent[num * 7]);
                    if (level != null && level == p.level)
                    {
                        undoPos.mapName = level.name;
                        undoPos.x = Convert.ToUInt16(fileContent[num * 7 + 1]);
                        undoPos.y = Convert.ToUInt16(fileContent[num * 7 + 2]);
                        undoPos.z = Convert.ToUInt16(fileContent[num * 7 + 3]);
                        undoPos.type = level.GetTile(undoPos.x, undoPos.y, undoPos.z);
                        if (undoPos.type == Convert.ToByte(fileContent[num * 7 + 6]) || Block.Convert(undoPos.type) == 8 || Block.Convert(undoPos.type) == 10)
                        {
                            if (undoPos.type == 0 || Block.Convert(undoPos.type) == 8 || Block.Convert(undoPos.type) == 10)
                            {
                                p.SendBlockchange(undoPos.x, undoPos.y, undoPos.z, 21);
                            }
                            else
                            {
                                p.SendBlockchange(undoPos.x, undoPos.y, undoPos.z, 25);
                            }
                        }
                    }
                }
                catch {}
            }
        }

        static void UnhilightBlocks(string[] fileContent, long seconds, Player p)
        {
            Player.UndoPos undoPos = default(Player.UndoPos);
            for (int num = fileContent.Length / 7; num >= 0; num--)
            {
                try
                {
                    if (!(Convert.ToDateTime(fileContent[num * 7 + 4].Replace('&', ' ')).AddSeconds(seconds) >= DateTime.Now))
                    {
                        break;
                    }
                    Level level = Level.Find(fileContent[num * 7]);
                    if (level != null && level == p.level)
                    {
                        undoPos.mapName = level.name;
                        undoPos.x = Convert.ToUInt16(fileContent[num * 7 + 1]);
                        undoPos.y = Convert.ToUInt16(fileContent[num * 7 + 2]);
                        undoPos.z = Convert.ToUInt16(fileContent[num * 7 + 3]);
                        undoPos.type = level.GetTile(undoPos.x, undoPos.y, undoPos.z);
                        if (undoPos.type == Convert.ToByte(fileContent[num * 7 + 6]) || Block.Convert(undoPos.type) == 8 || Block.Convert(undoPos.type) == 10)
                        {
                            p.SendBlockchange(undoPos.x, undoPos.y, undoPos.z, level.GetTile(undoPos.x, undoPos.y, undoPos.z));
                        }
                    }
                }
                catch {}
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/highlight [player] [seconds] - Highlights blocks modified by [player] in the last [seconds]");
            Player.SendMessage(p, "/highlight [player] 0 - Will highlight 30 minutes");
            Player.SendMessage(p, "&c/highlight cannot be disabled, you must use /reveal to un-highlight");
        }
    }
}