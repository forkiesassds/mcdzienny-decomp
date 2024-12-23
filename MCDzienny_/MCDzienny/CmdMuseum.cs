using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;

namespace MCDzienny
{
    public class CmdMuseum : Command
    {
        public override string name { get { return "museum"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override void Use(Player p, string message)
        {
            string path;
            if (message.Split(' ').Length == 1)
            {
                path = "levels/" + message + ".lvl";
            }
            else
            {
                if (message.Split(' ').Length != 2)
                {
                    Help(p);
                    return;
                }
                try
                {
                    path = Server.backupLocation + "/" + message.Split(' ')[0] + "/" + int.Parse(message.Split(' ')[1]) + "/" + message.Split(' ')[0] + ".lvl";
                }
                catch
                {
                    Help(p);
                    return;
                }
            }
            if (File.Exists(path))
            {
                FileStream fileStream = File.OpenRead(path);
                try
                {
                    GZipStream gZipStream = new GZipStream(fileStream, CompressionMode.Decompress);
                    byte[] array = new byte[2];
                    gZipStream.Read(array, 0, array.Length);
                    ushort num = BitConverter.ToUInt16(array, 0);
                    Level level;
                    if (num == 1874)
                    {
                        byte[] array2 = new byte[16];
                        gZipStream.Read(array2, 0, array2.Length);
                        ushort x = BitConverter.ToUInt16(array2, 0);
                        ushort z = BitConverter.ToUInt16(array2, 2);
                        ushort y = BitConverter.ToUInt16(array2, 4);
                        level = new Level(name, x, y, z, "empty");
                        level.spawnx = BitConverter.ToUInt16(array2, 6);
                        level.spawnz = BitConverter.ToUInt16(array2, 8);
                        level.spawny = BitConverter.ToUInt16(array2, 10);
                        level.rotx = array2[12];
                        level.roty = array2[13];
                    }
                    else
                    {
                        byte[] array3 = new byte[12];
                        gZipStream.Read(array3, 0, array3.Length);
                        ushort x2 = num;
                        ushort z2 = BitConverter.ToUInt16(array3, 0);
                        ushort y2 = BitConverter.ToUInt16(array3, 2);
                        level = new Level(name, x2, y2, z2, "grass");
                        level.spawnx = BitConverter.ToUInt16(array3, 4);
                        level.spawnz = BitConverter.ToUInt16(array3, 6);
                        level.spawny = BitConverter.ToUInt16(array3, 8);
                        level.rotx = array3[10];
                        level.roty = array3[11];
                    }
                    level.setPhysics(0);
                    byte[] array4 = new byte[level.width * level.depth * level.height];
                    gZipStream.Read(array4, 0, array4.Length);
                    level.blocks = array4;
                    gZipStream.Close();
                    level.backDup = true;
                    level.permissionbuild = LevelPermission.Admin;
                    level.jailx = (ushort)(level.spawnx * 32);
                    level.jaily = (ushort)(level.spawny * 32);
                    level.jailz = (ushort)(level.spawnz * 32);
                    level.jailrotx = level.rotx;
                    level.jailroty = level.roty;
                    p.Loading = true;
                    foreach (Player player in Player.players)
                    {
                        if (p.level == player.level && p != player)
                        {
                            p.SendDie(player.id);
                        }
                    }
                    foreach (PlayerBot playerbot in PlayerBot.playerbots)
                    {
                        if (p.level == playerbot.level)
                        {
                            p.SendDie(playerbot.id);
                        }
                    }
                    Player.GlobalDie(p, self: true);
                    p.level = level;
                    p.SendMotd();
                    p.SendRaw(2);
                    byte[] array5 = new byte[level.blocks.Length + 4];
                    BitConverter.GetBytes(IPAddress.HostToNetworkOrder(level.blocks.Length)).CopyTo(array5, 0);
                    for (int i = 0; i < level.blocks.Length; i++)
                    {
                        array5[4 + i] = Block.Convert(level.blocks[i]);
                    }
                    array5 = Player.GZip(array5);
                    int num2 = (int)Math.Ceiling(array5.Length / 1024.0);
                    int num3 = 1;
                    while (array5.Length > 0)
                    {
                        short num4 = (short)Math.Min(array5.Length, 1024);
                        byte[] array6 = new byte[1027];
                        Player.HTNO(num4).CopyTo(array6, 0);
                        Buffer.BlockCopy(array5, 0, array6, 2, num4);
                        byte[] array7 = new byte[array5.Length - num4];
                        Buffer.BlockCopy(array5, num4, array7, 0, array5.Length - num4);
                        array5 = array7;
                        array6[1026] = (byte)(num3 * 100 / num2);
                        p.SendRaw(3, array6);
                        Thread.Sleep(10);
                        num3++;
                    }
                    array5 = new byte[6];
                    Player.HTNO((short)level.width).CopyTo(array5, 0);
                    Player.HTNO((short)level.height).CopyTo(array5, 2);
                    Player.HTNO((short)level.depth).CopyTo(array5, 4);
                    p.SendRaw(4, array5);
                    ushort x3 = (ushort)((0.5 + level.spawnx) * 32.0);
                    ushort y3 = (ushort)((1 + level.spawny) * 32);
                    ushort z3 = (ushort)((0.5 + level.spawnz) * 32.0);
                    p.aiming = false;
                    Player.GlobalSpawn(p, x3, y3, z3, level.rotx, level.roty, self: true);
                    p.ClearBlockchange();
                    p.Loading = false;
                    if (message.IndexOf(' ') == -1)
                    {
                        level.name = string.Format("&cMuseum {0}({1})", Server.DefaultColor, message.Split(' ')[0]);
                    }
                    else
                    {
                        level.name = string.Format("&cMuseum {0}({1} {2})", Server.DefaultColor, message.Split(' ')[0], message.Split(' ')[1]);
                    }
                    if (!p.hidden)
                    {
                        Player.GlobalChat(null, string.Format("{0} went to the {1}", p.color + p.prefix + p.PublicName + Server.DefaultColor, level.name), showname: false);
                    }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    return;
                }
                catch (Exception ex)
                {
                    Player.SendMessage(p, "Error loading level.");
                    Server.ErrorLog(ex);
                    return;
                }
                finally
                {
                    fileStream.Close();
                }
            }
            Player.SendMessage(p, "Level or backup could not be found.");
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/museum <map> <restore> - Allows you to access a restore of the map entered.");
            Player.SendMessage(p, "Works on offline maps");
        }
    }
}