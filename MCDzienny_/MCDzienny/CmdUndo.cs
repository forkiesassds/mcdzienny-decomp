using System;
using System.IO;

namespace MCDzienny
{
    public class CmdUndo : Command
    {
        public override string name { get { return "undo"; } }

        public override string shortcut { get { return "u"; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override void Use(Player p, string message)
        {
            long num = 30L;
            int num2 = 0;
            p.RedoBuffer.Clear();
            if (message == "")
            {
                if (p == null)
                {
                    Server.s.Log("Console can't undo its actions.");
                    return;
                }
                message = p.name + " 30";
            }
            if (message.Split(' ').Length == 2)
            {
                if (message.Split(' ')[1].ToLower() == "all")
                {
                    if (p == null)
                    {
                        num = 500000L;
                    }
                    else if (p.group.Permission > LevelPermission.Operator)
                    {
                        num = 500000L;
                    }
                }
                else
                {
                    try
                    {
                        num = long.Parse(message.Split(' ')[1]);
                    }
                    catch
                    {
                        Player.SendMessage(p, "Invalid seconds.");
                        return;
                    }
                }
            }
            else
            {
                try
                {
                    num = int.Parse(message);
                    if (p != null)
                    {
                        message = p.name + " " + message;
                    }
                }
                catch
                {
                    num = 30L;
                    message += " 30";
                }
            }
            if (num == 0)
            {
                num = 5400L;
            }
            Player player = Player.Find(message.Split(' ')[0]);
            if (player != null)
            {
                if (p != null)
                {
                    if (player.group.Permission > p.group.Permission && player != p)
                    {
                        Player.SendMessage(p, "Cannot undo a user of higher or equal rank");
                        return;
                    }
                    if (player != p && p.group.Permission < LevelPermission.Operator)
                    {
                        Player.SendMessage(p, "Only an OP+ may undo other people's actions");
                        return;
                    }
                    if (p.group.Permission < LevelPermission.Builder && num > 120)
                    {
                        Player.SendMessage(p, "Guests may only undo 2 minutes.");
                        return;
                    }
                    if (p.group.Permission < LevelPermission.AdvBuilder && num > 300)
                    {
                        Player.SendMessage(p, "Builders may only undo 300 seconds.");
                        return;
                    }
                    if (p.group.Permission < LevelPermission.Operator && num > 1200)
                    {
                        Player.SendMessage(p, "AdvBuilders may only undo 600 seconds.");
                        return;
                    }
                    if (p.group.Permission == LevelPermission.Operator && num > 5400)
                    {
                        Player.SendMessage(p, "Operators may only undo 5400 seconds.");
                        return;
                    }
                }
                for (num2 = player.UndoBuffer.Count - 1; num2 >= 0; num2--)
                {
                    try
                    {
                        Player.UndoPos item = player.UndoBuffer[num2];
                        Level level = Level.FindExact(item.mapName);
                        byte tile = level.GetTile(item.x, item.y, item.z);
                        if (!(item.timePlaced.AddSeconds(num) >= DateTime.Now))
                        {
                            break;
                        }
                        if (tile == item.newtype || Block.Convert(tile) == 8 || Block.Convert(tile) == 10)
                        {
                            if (item.type != 97)
                            {
                                level.Blockchange(item.x, item.y, item.z, item.type, overRide: true);
                                item.newtype = item.type;
                                item.type = tile;
                                p.RedoBuffer.Add(item);
                            }
                            player.UndoBuffer.RemoveAt(num2);
                        }
                    }
                    catch {}
                }
                UndoOffline(p, message, num);
                if (p != player)
                {
                    Player.GlobalChat(p,
                                      string.Format("{0}'s actions for the past &b{1} seconds were undone.", player.color + player.PublicName + Server.DefaultColor, num), showname: false);
                }
                else
                {
                    Player.SendMessage(p, string.Format("Undid your actions for the past &b{0} seconds.", num + Server.DefaultColor));
                }
                return;
            }
            if (message.Split(' ')[0].ToLower() == "physics")
            {
                if (p.group.Permission < LevelPermission.AdvBuilder)
                {
                    Player.SendMessage(p, "Reserved for Adv+");
                    return;
                }
                if (p.group.Permission < LevelPermission.Operator && num > 1200)
                {
                    Player.SendMessage(p, "AdvBuilders may only undo 1200 seconds.");
                    return;
                }
                if (p.group.Permission == LevelPermission.Operator && num > 5400)
                {
                    Player.SendMessage(p, "Operators may only undo 5400 seconds.");
                    return;
                }
                ushort x;
                ushort y;
                ushort z;
                if (p.level.UndoBuffer.Count != Server.physUndo)
                {
                    for (num2 = p.level.currentUndo; num2 >= 0; num2--)
                    {
                        try
                        {
                            Level.UndoPos undoPos = p.level.UndoBuffer[num2];
                            byte tile = p.level.GetTile(undoPos.location);
                            if (undoPos.timePerformed.AddSeconds(num) >= DateTime.Now)
                            {
                                if ((tile == undoPos.newType || Block.Convert(tile) == 8 || Block.Convert(tile) == 10) && undoPos.oldType != 97)
                                {
                                    p.level.IntToPos(undoPos.location, out x, out y, out z);
                                    p.level.Blockchange(p, x, y, z, undoPos.oldType, addaction: true);
                                }
                                continue;
                            }
                        }
                        catch
                        {
                            continue;
                        }
                        break;
                    }
                }
                else
                {
                    for (num2 = p.level.currentUndo; num2 != p.level.currentUndo + 1; num2--)
                    {
                        try
                        {
                            if (num2 < 0)
                            {
                                num2 = p.level.UndoBuffer.Count - 1;
                            }
                            Level.UndoPos undoPos = p.level.UndoBuffer[num2];
                            byte tile = p.level.GetTile(undoPos.location);
                            if (undoPos.timePerformed.AddSeconds(num) >= DateTime.Now)
                            {
                                if ((tile == undoPos.newType || Block.Convert(tile) == 8 || Block.Convert(tile) == 10) && undoPos.oldType != 97)
                                {
                                    p.level.IntToPos(undoPos.location, out x, out y, out z);
                                    p.level.Blockchange(p, x, y, z, undoPos.oldType, addaction: true);
                                }
                                continue;
                            }
                        }
                        catch
                        {
                            continue;
                        }
                        break;
                    }
                }
                Player.GlobalMessage(string.Format("Physics were undone &b{0} seconds", num + Server.DefaultColor));
                return;
            }
            if (p != null)
            {
                if (p.group.Permission < LevelPermission.Operator)
                {
                    Player.SendMessage(p, "Reserved for OP+");
                    return;
                }
                if (num > 5400 && p.group.Permission == LevelPermission.Operator)
                {
                    Player.SendMessage(p, "Only SuperOPs may undo more than 90 minutes.");
                    return;
                }
            }
            bool flag = false;
            try
            {
                p.RedoBuffer.Clear();
                if (UndoOffline(p, message, num))
                {
                    Player.GlobalChat(
                        p,
                        string.Format("{0}'s actions for the past &b{1} seconds were undone.",
                                      Server.FindColor(message.Split(' ')[0]) + message.Split(' ')[0] + Server.DefaultColor, num + Server.DefaultColor),
                        showname: false);
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

        bool UndoOffline(Player p, string message, long seconds)
        {
            bool result = false;
            string text = "extra/undo/" + message.Split(' ')[0].ToLower();
            if (Directory.Exists(text))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(text);
                for (int num = directoryInfo.GetFiles("*.undo").Length - 1; num >= 0; num--)
                {
                    Server.s.Log(num.ToString());
                    string[] fileContent = File.ReadAllText(text + "/" + num + ".undo").Split(new char[1]
                    {
                        ' '
                    }, StringSplitOptions.RemoveEmptyEntries);
                    try
                    {
                        if (!undoBlah(fileContent, seconds, p))
                        {
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Server.ErrorLog(ex);
                        File.Delete(text + "/" + num + ".undo");
                    }
                }
                result = true;
            }
            string text2 = "extra/undoPrevious/" + message.Split(' ')[0].ToLower();
            if (Directory.Exists(text2))
            {
                DirectoryInfo directoryInfo2 = new DirectoryInfo(text2);
                for (int num2 = directoryInfo2.GetFiles("*.undo").Length - 1; num2 >= 0; num2--)
                {
                    string[] fileContent2 = File.ReadAllText(text2 + "/" + num2 + ".undo").Split(new char[1]
                    {
                        ' '
                    }, StringSplitOptions.RemoveEmptyEntries);
                    try
                    {
                        if (!undoBlah(fileContent2, seconds, p))
                        {
                            break;
                        }
                    }
                    catch (Exception ex2)
                    {
                        Server.ErrorLog(ex2);
                        File.Delete(text2 + "/" + num2 + ".undo");
                    }
                }
                result = true;
            }
            return result;
        }

        public bool undoBlah(string[] fileContent, long seconds, Player p)
        {
            if (fileContent.Length % 7 != 0)
            {
                throw new ArgumentException("Length has to be a multiplication of 7", "fileContent");
            }
            int num = (fileContent.Length - 1) / 7;
            Player.UndoPos item = default(Player.UndoPos);
            while (num >= 0)
            {
                if (Convert.ToDateTime(fileContent[num * 7 + 4].Replace('&', ' ')).AddSeconds(seconds) >= DateTime.Now)
                {
                    Level level = Level.FindExact(fileContent[num * 7]);
                    if (level != null)
                    {
                        item.mapName = level.name;
                        item.x = Convert.ToUInt16(fileContent[num * 7 + 1]);
                        item.y = Convert.ToUInt16(fileContent[num * 7 + 2]);
                        item.z = Convert.ToUInt16(fileContent[num * 7 + 3]);
                        item.type = level.GetTile(item.x, item.y, item.z);
                        if ((item.type == Convert.ToByte(fileContent[num * 7 + 6]) || Block.Convert(item.type) == 8 || Block.Convert(item.type) == 10 || item.type == 2) &&
                            item.type != 97)
                        {
                            item.newtype = Convert.ToByte(fileContent[num * 7 + 5]);
                            item.timePlaced = DateTime.Now;
                            level.Blockchange(item.x, item.y, item.z, item.newtype, overRide: true);
                            p.RedoBuffer.Add(item);
                        }
                    }
                    num--;
                    continue;
                }
                return false;
            }
            return true;
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/undo [player] [seconds] - Undoes the blockchanges made by [player] in the previous [seconds].");
            Player.SendMessage(p, "/undo [player] all - &cWill undo 138 hours for [player] <SuperOP+>");
            Player.SendMessage(p, "/undo [player] 0 - &cWill undo 30 minutes <Operator+>");
            Player.SendMessage(p, "/undo physics [seconds] - Undoes the physics for the current map");
        }
    }
}