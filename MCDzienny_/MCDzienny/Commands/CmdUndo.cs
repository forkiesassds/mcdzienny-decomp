using System;
using System.IO;

namespace MCDzienny
{
    // Token: 0x020002A7 RID: 679
    public class CmdUndo : Command
    {
        // Token: 0x1700076B RID: 1899
        // (get) Token: 0x0600137E RID: 4990 RVA: 0x0006AF64 File Offset: 0x00069164
        public override string name
        {
            get { return "undo"; }
        }

        // Token: 0x1700076C RID: 1900
        // (get) Token: 0x0600137F RID: 4991 RVA: 0x0006AF6C File Offset: 0x0006916C
        public override string shortcut
        {
            get { return "u"; }
        }

        // Token: 0x1700076D RID: 1901
        // (get) Token: 0x06001380 RID: 4992 RVA: 0x0006AF74 File Offset: 0x00069174
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x1700076E RID: 1902
        // (get) Token: 0x06001381 RID: 4993 RVA: 0x0006AF7C File Offset: 0x0006917C
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700076F RID: 1903
        // (get) Token: 0x06001382 RID: 4994 RVA: 0x0006AF80 File Offset: 0x00069180
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x06001384 RID: 4996 RVA: 0x0006AF8C File Offset: 0x0006918C
        public override void Use(Player p, string message)
        {
            var num = 30L;
            var i = 0;
            if (p != null) p.RedoBuffer.Clear();
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
                        goto IL_115;
                    }

                    if (p.group.Permission > LevelPermission.Operator) num = 500000L;
                    goto IL_115;
                }

                try
                {
                    num = long.Parse(message.Split(' ')[1]);
                    goto IL_115;
                }
                catch
                {
                    Player.SendMessage(p, "Invalid seconds.");
                    return;
                }
            }

            try
            {
                num = int.Parse(message);
                if (p != null) message = p.name + " " + message;
            }
            catch
            {
                num = 30L;
                message += " 30";
            }

            IL_115:
            if (num == 0L) num = 5400L;
            var player = Player.Find(message.Split(' ')[0]);
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

                    if (p.group.Permission < LevelPermission.Builder && num > 120L)
                    {
                        Player.SendMessage(p, "Guests may only undo 2 minutes.");
                        return;
                    }

                    if (p.group.Permission < LevelPermission.AdvBuilder && num > 300L)
                    {
                        Player.SendMessage(p, "Builders may only undo 300 seconds.");
                        return;
                    }

                    if (p.group.Permission < LevelPermission.Operator && num > 1200L)
                    {
                        Player.SendMessage(p, "AdvBuilders may only undo 600 seconds.");
                        return;
                    }

                    if (p.group.Permission == LevelPermission.Operator && num > 5400L)
                    {
                        Player.SendMessage(p, "Operators may only undo 5400 seconds.");
                        return;
                    }
                }

                for (i = player.UndoBuffer.Count - 1; i >= 0; i--)
                    try
                    {
                        var item = player.UndoBuffer[i];
                        var level = Level.FindExact(item.mapName);
                        var tile = level.GetTile(item.x, item.y, item.z);
                        if (!(item.timePlaced.AddSeconds(num) >= DateTime.Now)) break;
                        if (tile == item.newtype || Block.Convert(tile) == 8 || Block.Convert(tile) == 10)
                        {
                            if (item.type != 97)
                            {
                                level.Blockchange(item.x, item.y, item.z, item.type, true);
                                item.newtype = item.type;
                                item.type = tile;
                                if (p != null) p.RedoBuffer.Add(item);
                            }

                            player.UndoBuffer.RemoveAt(i);
                        }
                    }
                    catch
                    {
                    }

                UndoOffline(p, message, num);
                if (p != player)
                {
                    Player.GlobalChat(p,
                        string.Format("{0}'s actions for the past &b{1} seconds were undone.",
                            player.color + player.PublicName + Server.DefaultColor, num), false);
                    return;
                }

                Player.SendMessage(p,
                    string.Format("Undid your actions for the past &b{0} seconds.", num + Server.DefaultColor));
            }
            else
            {
                if (!(message.Split(' ')[0].ToLower() == "physics"))
                {
                    if (p != null)
                    {
                        if (p.group.Permission < LevelPermission.Operator)
                        {
                            Player.SendMessage(p, "Reserved for OP+");
                            return;
                        }

                        if (num > 5400L && p.group.Permission == LevelPermission.Operator)
                        {
                            Player.SendMessage(p, "Only SuperOPs may undo more than 90 minutes.");
                            return;
                        }
                    }

                    try
                    {
                        if (p != null) p.RedoBuffer.Clear();
                        var flag = UndoOffline(p, message, num);
                        if (flag)
                            Player.GlobalChat(p,
                                string.Format("{0}'s actions for the past &b{1} seconds were undone.",
                                    Server.FindColor(message.Split(' ')[0]) + message.Split(' ')[0] +
                                    Server.DefaultColor, num + Server.DefaultColor), false);
                        else
                            Player.SendMessage(p, "Could not find player specified.");
                    }
                    catch (Exception ex)
                    {
                        Server.ErrorLog(ex);
                    }

                    return;
                }

                if (p.group.Permission < LevelPermission.AdvBuilder)
                {
                    Player.SendMessage(p, "Reserved for Adv+");
                    return;
                }

                if (p.group.Permission < LevelPermission.Operator && num > 1200L)
                {
                    Player.SendMessage(p, "AdvBuilders may only undo 1200 seconds.");
                    return;
                }

                if (p.group.Permission == LevelPermission.Operator && num > 5400L)
                {
                    Player.SendMessage(p, "Operators may only undo 5400 seconds.");
                    return;
                }

                if (p.level.UndoBuffer.Count != Server.physUndo)
                    for (i = p.level.currentUndo; i >= 0; i--)
                        try
                        {
                            var undoPos = p.level.UndoBuffer[i];
                            var tile = p.level.GetTile(undoPos.location);
                            if (!(undoPos.timePerformed.AddSeconds(num) >= DateTime.Now)) break;
                            if ((tile == undoPos.newType || Block.Convert(tile) == 8 || Block.Convert(tile) == 10) &&
                                undoPos.oldType != 97)
                            {
                                ushort x;
                                ushort y;
                                ushort z;
                                p.level.IntToPos(undoPos.location, out x, out y, out z);
                                p.level.Blockchange(p, x, y, z, undoPos.oldType, true);
                            }
                        }
                        catch
                        {
                        }
                else
                    for (i = p.level.currentUndo; i != p.level.currentUndo + 1; i--)
                        try
                        {
                            if (i < 0) i = p.level.UndoBuffer.Count - 1;
                            var undoPos = p.level.UndoBuffer[i];
                            var tile = p.level.GetTile(undoPos.location);
                            if (!(undoPos.timePerformed.AddSeconds(num) >= DateTime.Now)) break;
                            if ((tile == undoPos.newType || Block.Convert(tile) == 8 || Block.Convert(tile) == 10) &&
                                undoPos.oldType != 97)
                            {
                                ushort x;
                                ushort y;
                                ushort z;
                                p.level.IntToPos(undoPos.location, out x, out y, out z);
                                p.level.Blockchange(p, x, y, z, undoPos.oldType, true);
                            }
                        }
                        catch
                        {
                        }

                Player.GlobalMessage(string.Format("Physics were undone &b{0} seconds", num + Server.DefaultColor));
            }
        }

        // Token: 0x06001385 RID: 4997 RVA: 0x0006B6C0 File Offset: 0x000698C0
        private bool UndoOffline(Player p, string message, long seconds)
        {
            var result = false;
            var text = "extra/undo/" + message.Split(' ')[0].ToLower();
            if (Directory.Exists(text))
            {
                var directoryInfo = new DirectoryInfo(text);
                for (var i = directoryInfo.GetFiles("*.undo").Length - 1; i >= 0; i--)
                {
                    Server.s.Log(i.ToString());
                    var fileContent = File.ReadAllText(string.Concat(text, "/", i, ".undo")).Split(new[]
                    {
                        ' '
                    }, StringSplitOptions.RemoveEmptyEntries);
                    try
                    {
                        if (!undoBlah(fileContent, seconds, p)) break;
                    }
                    catch (Exception ex)
                    {
                        Server.ErrorLog(ex);
                        File.Delete(string.Concat(text, "/", i, ".undo"));
                    }
                }

                result = true;
            }

            var text2 = "extra/undoPrevious/" + message.Split(' ')[0].ToLower();
            if (Directory.Exists(text2))
            {
                var directoryInfo2 = new DirectoryInfo(text2);
                for (var j = directoryInfo2.GetFiles("*.undo").Length - 1; j >= 0; j--)
                {
                    var fileContent2 = File.ReadAllText(string.Concat(text2, "/", j, ".undo")).Split(new[]
                    {
                        ' '
                    }, StringSplitOptions.RemoveEmptyEntries);
                    try
                    {
                        if (!undoBlah(fileContent2, seconds, p)) break;
                    }
                    catch (Exception ex2)
                    {
                        Server.ErrorLog(ex2);
                        File.Delete(string.Concat(text2, "/", j, ".undo"));
                    }
                }

                result = true;
            }

            return result;
        }

        // Token: 0x06001386 RID: 4998 RVA: 0x0006B900 File Offset: 0x00069B00
        public bool undoBlah(string[] fileContent, long seconds, Player p)
        {
            if (fileContent.Length % 7 != 0)
                throw new ArgumentException("Length has to be a multiplication of 7", "fileContent");
            for (var i = (fileContent.Length - 1) / 7; i >= 0; i--)
            {
                if (!(Convert.ToDateTime(fileContent[i * 7 + 4].Replace('&', ' ')).AddSeconds(seconds) >=
                      DateTime.Now)) return false;
                var level = Level.FindExact(fileContent[i * 7]);
                if (level != null)
                {
                    Player.UndoPos item;
                    item.mapName = level.name;
                    item.x = Convert.ToUInt16(fileContent[i * 7 + 1]);
                    item.y = Convert.ToUInt16(fileContent[i * 7 + 2]);
                    item.z = Convert.ToUInt16(fileContent[i * 7 + 3]);
                    item.type = level.GetTile(item.x, item.y, item.z);
                    if ((item.type == Convert.ToByte(fileContent[i * 7 + 6]) || Block.Convert(item.type) == 8 ||
                         Block.Convert(item.type) == 10 || item.type == 2) && item.type != 97)
                    {
                        item.newtype = Convert.ToByte(fileContent[i * 7 + 5]);
                        item.timePlaced = DateTime.Now;
                        level.Blockchange(item.x, item.y, item.z, item.newtype, true);
                        if (p != null) p.RedoBuffer.Add(item);
                    }
                }
            }

            return true;
        }

        // Token: 0x06001387 RID: 4999 RVA: 0x0006BA88 File Offset: 0x00069C88
        public override void Help(Player p)
        {
            Player.SendMessage(p,
                "/undo [player] [seconds] - Undoes the blockchanges made by [player] in the previous [seconds].");
            Player.SendMessage(p, "/undo [player] all - &cWill undo 138 hours for [player] <SuperOP+>");
            Player.SendMessage(p, "/undo [player] 0 - &cWill undo 30 minutes <Operator+>");
            Player.SendMessage(p, "/undo physics [seconds] - Undoes the physics for the current map");
        }
    }
}