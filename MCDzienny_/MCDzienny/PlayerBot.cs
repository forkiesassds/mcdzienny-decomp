using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Timer = System.Timers.Timer;

namespace MCDzienny
{
    // Token: 0x02000357 RID: 855
    public sealed class PlayerBot
    {
        // Token: 0x04000CB8 RID: 3256
        public static List<PlayerBot> playerbots = new List<PlayerBot>(64);

        // Token: 0x04000CBB RID: 3259
        public string AIName = "";

        // Token: 0x04000CC6 RID: 3270
        private ushort[] basepos;

        // Token: 0x04000CCF RID: 3279
        public Timer botTimer;

        // Token: 0x04000CBE RID: 3262
        public string color;

        // Token: 0x04000CC1 RID: 3265
        public int countdown;

        // Token: 0x04000CCE RID: 3278
        private int currentjump;

        // Token: 0x04000CC0 RID: 3264
        public int currentPoint;

        // Token: 0x04000CC9 RID: 3273
        private ushort[] foundPos;

        // Token: 0x04000CCA RID: 3274
        private byte[] foundRot;

        // Token: 0x04000CB9 RID: 3257
        public bool hunt;

        // Token: 0x04000CBD RID: 3261
        public byte id;

        // Token: 0x04000CCD RID: 3277
        private bool jumping;

        // Token: 0x04000CD1 RID: 3281
        public Timer jumpTimer;

        // Token: 0x04000CBA RID: 3258
        public bool kill;

        // Token: 0x04000CBF RID: 3263
        public Level level;

        // Token: 0x04000CCB RID: 3275
        private bool movement;

        // Token: 0x04000CCC RID: 3276
        public int movementSpeed;

        // Token: 0x04000CD0 RID: 3280
        public Timer moveTimer;

        // Token: 0x04000CBC RID: 3260
        public string name;

        // Token: 0x04000CC2 RID: 3266
        public bool nodUp;

        // Token: 0x04000CC5 RID: 3269
        private ushort[] oldpos;

        // Token: 0x04000CC8 RID: 3272
        private byte[] oldrot;

        // Token: 0x04000CC4 RID: 3268
        public ushort[] pos;

        // Token: 0x04000CC7 RID: 3271
        public byte[] rot;

        // Token: 0x04000CC3 RID: 3267
        public List<Pos> Waypoints = new List<Pos>();

        // Token: 0x0600188A RID: 6282 RVA: 0x000A5704 File Offset: 0x000A3904
        public PlayerBot(string n, Level l)
        {
            Server.s.Log("adding " + n + " bot");
            name = n;
            color = "&1";
            id = FreeId();
            level = l;
            var num = (ushort) ((0.5 + level.spawnx) * 32.0);
            var num2 = (ushort) ((1 + level.spawny) * 32);
            var num3 = (ushort) ((0.5 + level.spawnz) * 32.0);
            pos = new ushort[3]
            {
                num,
                num2,
                num3
            };
            rot = new byte[2]
            {
                level.rotx,
                level.roty
            };
            GlobalSpawn();
        }

        // Token: 0x0600188B RID: 6283 RVA: 0x000A58C8 File Offset: 0x000A3AC8
        public PlayerBot(string n, Level l, ushort x, ushort y, ushort z, byte rotx, byte roty)
        {
            var playerBot = this;
            name = n;
            color = "&1";
            id = FreeId();
            level = l;
            pos = new ushort[3]
            {
                x,
                y,
                z
            };
            rot = new byte[2]
            {
                rotx,
                roty
            };
            GlobalSpawn();
            var players = Player.players;
            Action<Player> action = delegate(Player p)
            {
                if (p.level == level)
                    Player.SendMessage(p, color + name + Server.DefaultColor + ", the bot, has been added.");
            };
            players.ForEachSync(action);
            botTimer.Elapsed += delegate
            {
                var foundNum = 2400;
                new Random();
                x = (ushort) Math.Round(playerBot.pos[0] / 32m);
                y = (ushort) ((playerBot.pos[1] - 33) / 32);
                z = (ushort) Math.Round(playerBot.pos[2] / 32m);
                if (playerBot.kill)
                    Player.players.ForEachSync(delegate(Player p)
                    {
                        if ((ushort) (p.pos[0] / 32) == x && Math.Abs((ushort) (p.pos[1] / 32) - y) < 2 &&
                            (ushort) (p.pos[2] / 32) == z) p.HandleDeath(byte.MaxValue);
                    });
                if (playerBot.Waypoints.Count < 1)
                {
                    if (playerBot.hunt)
                    {
                        var currentNum = default(int);
                        Player.players.ForEachSync(delegate(Player p)
                        {
                            if (p.level == playerBot.level && !p.invincible)
                            {
                                currentNum = Math.Abs(p.pos[0] - playerBot.pos[0]) +
                                             Math.Abs(p.pos[1] - playerBot.pos[1]) +
                                             Math.Abs(p.pos[2] - playerBot.pos[2]);
                                if (currentNum < foundNum)
                                {
                                    foundNum = currentNum;
                                    playerBot.foundPos = p.pos;
                                    playerBot.foundRot = p.rot;
                                    playerBot.movement = true;
                                    playerBot.rot[1] = (byte) (255 - playerBot.foundRot[1]);
                                    if (playerBot.foundRot[0] < 128)
                                        playerBot.rot[0] = (byte) (playerBot.foundRot[0] + 128);
                                    else
                                        playerBot.rot[0] = (byte) (playerBot.foundRot[0] - 128);
                                }
                            }
                        });
                    }
                }
                else
                {
                    var flag = false;
                    playerBot.movement = false;
                    while (true)
                    {
                        switch (playerBot.Waypoints[playerBot.currentPoint].type)
                        {
                            case "walk":
                                playerBot.foundPos[0] = playerBot.Waypoints[playerBot.currentPoint].x;
                                playerBot.foundPos[1] = playerBot.Waypoints[playerBot.currentPoint].y;
                                playerBot.foundPos[2] = playerBot.Waypoints[playerBot.currentPoint].z;
                                playerBot.movement = true;
                                if ((ushort) (playerBot.pos[0] / 32) ==
                                    (ushort) (playerBot.Waypoints[playerBot.currentPoint].x / 32) &&
                                    (ushort) (playerBot.pos[2] / 32) ==
                                    (ushort) (playerBot.Waypoints[playerBot.currentPoint].z / 32))
                                {
                                    playerBot.rot[0] = playerBot.Waypoints[playerBot.currentPoint].rotx;
                                    playerBot.rot[1] = playerBot.Waypoints[playerBot.currentPoint].roty;
                                    playerBot.currentPoint++;
                                    playerBot.movement = false;
                                    if (playerBot.currentPoint == playerBot.Waypoints.Count) playerBot.currentPoint = 0;
                                    if (!flag)
                                    {
                                        flag = true;
                                        continue;
                                    }
                                }

                                break;
                            case "teleport":
                                playerBot.pos[0] = playerBot.Waypoints[playerBot.currentPoint].x;
                                playerBot.pos[1] = playerBot.Waypoints[playerBot.currentPoint].y;
                                playerBot.pos[2] = playerBot.Waypoints[playerBot.currentPoint].z;
                                playerBot.rot[0] = playerBot.Waypoints[playerBot.currentPoint].rotx;
                                playerBot.rot[1] = playerBot.Waypoints[playerBot.currentPoint].roty;
                                playerBot.currentPoint++;
                                if (playerBot.currentPoint == playerBot.Waypoints.Count) playerBot.currentPoint = 0;
                                return;
                            case "wait":
                                if (playerBot.countdown != 0)
                                {
                                    playerBot.countdown--;
                                    if (playerBot.countdown == 0)
                                    {
                                        playerBot.currentPoint++;
                                        if (playerBot.currentPoint == playerBot.Waypoints.Count)
                                            playerBot.currentPoint = 0;
                                        if (!flag)
                                        {
                                            flag = true;
                                            continue;
                                        }
                                    }
                                }
                                else
                                {
                                    playerBot.countdown = playerBot.Waypoints[playerBot.currentPoint].seconds;
                                }

                                return;
                            case "nod":
                                if (playerBot.countdown != 0)
                                {
                                    playerBot.countdown--;
                                    if (playerBot.nodUp)
                                    {
                                        if (playerBot.rot[1] > 32 && playerBot.rot[1] < 128)
                                            playerBot.nodUp = !playerBot.nodUp;
                                        else if (playerBot.rot[1] +
                                            (byte) playerBot.Waypoints[playerBot.currentPoint].rotspeed > 255)
                                            playerBot.rot[1] = 0;
                                        else
                                            playerBot.rot[1] +=
                                                (byte) playerBot.Waypoints[playerBot.currentPoint].rotspeed;
                                    }
                                    else if (playerBot.rot[1] > 128 && playerBot.rot[1] < 224)
                                    {
                                        playerBot.nodUp = !playerBot.nodUp;
                                    }
                                    else if (playerBot.rot[1] -
                                        (byte) playerBot.Waypoints[playerBot.currentPoint].rotspeed < 0)
                                    {
                                        playerBot.rot[1] = byte.MaxValue;
                                    }
                                    else
                                    {
                                        playerBot.rot[1] -= (byte) playerBot.Waypoints[playerBot.currentPoint].rotspeed;
                                    }

                                    if (playerBot.countdown == 0)
                                    {
                                        playerBot.currentPoint++;
                                        if (playerBot.currentPoint == playerBot.Waypoints.Count)
                                            playerBot.currentPoint = 0;
                                        if (!flag)
                                        {
                                            flag = true;
                                            continue;
                                        }
                                    }
                                }
                                else
                                {
                                    playerBot.countdown = playerBot.Waypoints[playerBot.currentPoint].seconds;
                                }

                                return;
                            case "spin":
                                if (playerBot.countdown != 0)
                                {
                                    playerBot.countdown--;
                                    if (playerBot.rot[0] + (byte) playerBot.Waypoints[playerBot.currentPoint].rotspeed >
                                        255)
                                        playerBot.rot[0] = 0;
                                    else if (playerBot.rot[0] +
                                        (byte) playerBot.Waypoints[playerBot.currentPoint].rotspeed < 0)
                                        playerBot.rot[0] = byte.MaxValue;
                                    else
                                        playerBot.rot[0] += (byte) playerBot.Waypoints[playerBot.currentPoint].rotspeed;
                                    if (playerBot.countdown == 0)
                                    {
                                        playerBot.currentPoint++;
                                        if (playerBot.currentPoint == playerBot.Waypoints.Count)
                                            playerBot.currentPoint = 0;
                                        if (!flag)
                                        {
                                            flag = true;
                                            continue;
                                        }
                                    }
                                }
                                else
                                {
                                    playerBot.countdown = playerBot.Waypoints[playerBot.currentPoint].seconds;
                                }

                                return;
                            case "speed":
                                playerBot.movementSpeed =
                                    (int) Math.Round(0.24m * playerBot.Waypoints[playerBot.currentPoint].seconds);
                                if (playerBot.movementSpeed == 0) playerBot.movementSpeed = 1;
                                playerBot.currentPoint++;
                                if (playerBot.currentPoint == playerBot.Waypoints.Count) playerBot.currentPoint = 0;
                                if (!flag)
                                {
                                    flag = true;
                                    continue;
                                }

                                return;
                            case "reset":
                                playerBot.currentPoint = 0;
                                return;
                            case "remove":
                                playerBot.removeBot();
                                return;
                            case "linkscript":
                                if (File.Exists("bots/" + playerBot.Waypoints[playerBot.currentPoint].newscript))
                                {
                                    Command.all.Find("botset").Use(null,
                                        playerBot.name + " " + playerBot.Waypoints[playerBot.currentPoint].newscript);
                                }
                                else
                                {
                                    playerBot.currentPoint++;
                                    if (playerBot.currentPoint == playerBot.Waypoints.Count) playerBot.currentPoint = 0;
                                    if (!flag)
                                    {
                                        flag = true;
                                        continue;
                                    }
                                }

                                return;
                            case "jump":
                                playerBot.jumpTimer.Elapsed += delegate
                                {
                                    playerBot.currentjump++;
                                    switch (playerBot.currentjump)
                                    {
                                        case 1:
                                        case 2:
                                            playerBot.pos[1] += 24;
                                            break;
                                        case 4:
                                            playerBot.pos[1] -= 24;
                                            break;
                                        case 5:
                                            playerBot.pos[1] -= 24;
                                            playerBot.jumping = false;
                                            playerBot.currentjump = 0;
                                            playerBot.jumpTimer.Stop();
                                            break;
                                        case 3:
                                            break;
                                    }
                                };
                                playerBot.jumpTimer.Start();
                                playerBot.currentPoint++;
                                if (playerBot.currentPoint == playerBot.Waypoints.Count) playerBot.currentPoint = 0;
                                if (!flag)
                                {
                                    flag = true;
                                    continue;
                                }

                                break;
                        }

                        break;
                    }

                    if (playerBot.currentPoint == playerBot.Waypoints.Count) playerBot.currentPoint = 0;
                }

                if (!playerBot.movement)
                {
                    if (playerBot.rot[0] < 245)
                        playerBot.rot[0] += 8;
                    else
                        playerBot.rot[0] = 0;
                    if (playerBot.rot[1] > 32 && playerBot.rot[1] < 64)
                        playerBot.rot[1] = 224;
                    else if (playerBot.rot[1] > 250)
                        playerBot.rot[1] = 0;
                    else
                        playerBot.rot[1] += 4;
                }
            };
            botTimer.Start();
            moveTimer.Elapsed += delegate
            {
                playerBot.moveTimer.Interval = Server.updateTimer.Interval / playerBot.movementSpeed;
                if (playerBot.movement)
                {
                    new Random();
                    if ((playerBot.pos[1] - 19) % 32 != 0 && !playerBot.jumping)
                        playerBot.pos[1] = (ushort) (playerBot.pos[1] + 19 - playerBot.pos[1] % 32);
                    x = (ushort) Math.Round((playerBot.pos[0] - 16) / 32m);
                    y = (ushort) ((playerBot.pos[1] - 64) / 32);
                    z = (ushort) Math.Round((playerBot.pos[2] - 16) / 32m);
                    var type = Block.Convert(playerBot.level.GetTile(x, y, z));
                    if (Block.Walkthrough(type) && !playerBot.jumping)
                        playerBot.pos[1] = (ushort) (playerBot.pos[1] - 32);
                    y = (ushort) ((playerBot.pos[1] - 64) / 32);
                    var b = playerBot.level.PosToInt((ushort) (x + Math.Sign(playerBot.foundPos[0] - playerBot.pos[0])),
                        y, (ushort) (z + Math.Sign(playerBot.foundPos[2] - playerBot.pos[2])));
                    type = Block.Convert(playerBot.level.GetTile(b));
                    var type2 = Block.Convert(playerBot.level.GetTile(playerBot.level.IntOffset(b, 0, 1, 0)));
                    var type3 = Block.Convert(playerBot.level.GetTile(playerBot.level.IntOffset(b, 0, 2, 0)));
                    var type4 = Block.Convert(playerBot.level.GetTile(playerBot.level.IntOffset(b, 0, 3, 0)));
                    if (Block.Walkthrough(type3) && Block.Walkthrough(type4) && !Block.Walkthrough(type2))
                    {
                        playerBot.pos[0] += (ushort) Math.Sign(playerBot.foundPos[0] - playerBot.pos[0]);
                        playerBot.pos[1] += 32;
                        playerBot.pos[2] += (ushort) Math.Sign(playerBot.foundPos[2] - playerBot.pos[2]);
                    }
                    else if (Block.Walkthrough(type2) && Block.Walkthrough(type3))
                    {
                        playerBot.pos[0] += (ushort) Math.Sign(playerBot.foundPos[0] - playerBot.pos[0]);
                        playerBot.pos[2] += (ushort) Math.Sign(playerBot.foundPos[2] - playerBot.pos[2]);
                    }
                    else if (Block.Walkthrough(type) && Block.Walkthrough(type2))
                    {
                        playerBot.pos[0] += (ushort) Math.Sign(playerBot.foundPos[0] - playerBot.pos[0]);
                        playerBot.pos[1] -= 32;
                        playerBot.pos[2] += (ushort) Math.Sign(playerBot.foundPos[2] - playerBot.pos[2]);
                    }

                    x = (ushort) Math.Round((playerBot.pos[0] - 16) / 32m);
                    y = (ushort) ((playerBot.pos[1] - 64) / 32);
                    z = (ushort) Math.Round((playerBot.pos[2] - 16) / 32m);
                    type2 = Block.Convert(playerBot.level.GetTile(x, (ushort) (y + 1), z));
                    type3 = Block.Convert(playerBot.level.GetTile(x, (ushort) (y + 2), z));
                    type4 = Block.Convert(playerBot.level.GetTile(x, y, z));
                }
            };
            moveTimer.Start();
        }

        // Token: 0x0600188C RID: 6284 RVA: 0x000A5AAC File Offset: 0x000A3CAC
        public void SetPos(ushort x, ushort y, ushort z, byte rotx, byte roty)
        {
            pos = new[]
            {
                x,
                y,
                z
            };
            rot = new[]
            {
                rotx,
                roty
            };
        }

        // Token: 0x0600188D RID: 6285 RVA: 0x000A5AEC File Offset: 0x000A3CEC
        public void removeBot()
        {
            botTimer.Stop();
            GlobalDie();
            playerbots.Remove(this);
        }

        // Token: 0x0600188E RID: 6286 RVA: 0x000A5B0C File Offset: 0x000A3D0C
        public void GlobalSpawn()
        {
            Player.players.ForEachSync(delegate(Player p)
            {
                if (p.level != level) return;
                p.SendSpawn(id, color + name, pos[0], pos[1], pos[2], rot[0], rot[1]);
            });
        }

        // Token: 0x0600188F RID: 6287 RVA: 0x000A5B24 File Offset: 0x000A3D24
        public void GlobalDie()
        {
            Server.s.Log("removing " + name + " bot");
            Player.players.ForEachSync(delegate(Player p)
            {
                if (p.level != level) return;
                p.SendDie(id);
            });
            playerbots.Remove(this);
        }

        // Token: 0x06001890 RID: 6288 RVA: 0x000A5B74 File Offset: 0x000A3D74
        public void Update()
        {
        }

        // Token: 0x06001891 RID: 6289 RVA: 0x000A5B78 File Offset: 0x000A3D78
        private void UpdatePosition()
        {
            byte b = 0;
            if (oldpos[0] != pos[0] || oldpos[1] != pos[1] || oldpos[2] != pos[2]) b |= 1;
            if (oldrot[0] != rot[0] || oldrot[1] != rot[1]) b |= 2;
            if (Math.Abs(pos[0] - basepos[0]) > 32 || Math.Abs(pos[1] - basepos[1]) > 32 ||
                Math.Abs(pos[2] - basepos[2]) > 32) b |= 4;
            if (oldpos[0] == pos[0] && oldpos[1] == pos[1] && oldpos[2] == pos[2] &&
                (basepos[0] != pos[0] || basepos[1] != pos[1] || basepos[2] != pos[2])) b |= 4;
            var buffer = new byte[0];
            byte msg = 0;
            if ((b & 4) != 0)
            {
                msg = 8;
                buffer = new byte[9];
                buffer[0] = id;
                HTNO(pos[0]).CopyTo(buffer, 1);
                HTNO(pos[1]).CopyTo(buffer, 3);
                HTNO(pos[2]).CopyTo(buffer, 5);
                buffer[7] = rot[0];
                buffer[8] = rot[1];
            }
            else if (b == 1)
            {
                msg = 10;
                buffer = new byte[4];
                buffer[0] = id;
                buffer[1] = (byte) (pos[0] - oldpos[0]);
                buffer[2] = (byte) (pos[1] - oldpos[1]);
                buffer[3] = (byte) (pos[2] - oldpos[2]);
            }
            else if (b == 2)
            {
                msg = 11;
                buffer = new byte[3];
                buffer[0] = id;
                buffer[1] = rot[0];
                buffer[2] = rot[1];
            }
            else if (b == 3)
            {
                msg = 9;
                buffer = new byte[6];
                buffer[0] = id;
                buffer[1] = (byte) (pos[0] - oldpos[0]);
                buffer[2] = (byte) (pos[1] - oldpos[1]);
                buffer[3] = (byte) (pos[2] - oldpos[2]);
                buffer[4] = rot[0];
                buffer[5] = rot[1];
            }

            try
            {
                if (b != 0)
                    Player.players.ForEachSync(delegate(Player p)
                    {
                        if (p.level == level && !p.Loading) p.SendRaw(msg, buffer);
                    });
            }
            catch
            {
            }

            oldpos = pos;
            oldrot = rot;
        }

        // Token: 0x06001892 RID: 6290 RVA: 0x000A5F14 File Offset: 0x000A4114
        private static byte FreeId()
        {
            for (byte b = 64; b < 128; b += 1)
            {
                foreach (var playerBot in playerbots)
                    if (playerBot.id == b)
                        goto IL_40;
                return b;
                IL_40: ;
            }

            return byte.MaxValue;
        }

        // Token: 0x06001893 RID: 6291 RVA: 0x000A5F84 File Offset: 0x000A4184
        public static PlayerBot Find(string name)
        {
            PlayerBot playerBot = null;
            var flag = false;
            foreach (var playerBot2 in playerbots)
            {
                if (playerBot2.name.ToLower() == name.ToLower()) return playerBot2;
                if (playerBot2.name.ToLower().IndexOf(name.ToLower()) != -1)
                {
                    if (playerBot == null)
                        playerBot = playerBot2;
                    else
                        flag = true;
                }
            }

            if (flag) return null;
            if (playerBot != null) return playerBot;
            return null;
        }

        // Token: 0x06001894 RID: 6292 RVA: 0x000A6020 File Offset: 0x000A4220
        public static bool ValidName(string name)
        {
            var text = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz01234567890_";
            foreach (var value in name)
                if (text.IndexOf(value) == -1)
                    return false;
            return true;
        }

        // Token: 0x06001895 RID: 6293 RVA: 0x000A6064 File Offset: 0x000A4264
        public static void GlobalUpdatePosition()
        {
            playerbots.ForEach(delegate(PlayerBot b) { b.UpdatePosition(); });
        }

        // Token: 0x06001896 RID: 6294 RVA: 0x000A6090 File Offset: 0x000A4290
        public static void GlobalUpdate()
        {
            for (;;)
            {
                Thread.Sleep(100);
                playerbots.ForEach(delegate(PlayerBot b) { b.Update(); });
            }
        }

        // Token: 0x06001897 RID: 6295 RVA: 0x000A60C4 File Offset: 0x000A42C4
        private byte[] HTNO(ushort x)
        {
            var bytes = BitConverter.GetBytes(x);
            Array.Reverse(bytes);
            return bytes;
        }

        // Token: 0x06001898 RID: 6296 RVA: 0x000A60E0 File Offset: 0x000A42E0
        private ushort NTHO(byte[] x, int offset)
        {
            var array = new byte[2];
            Buffer.BlockCopy(x, offset, array, 0, 2);
            Array.Reverse(array);
            return BitConverter.ToUInt16(array, 0);
        }

        // Token: 0x06001899 RID: 6297 RVA: 0x000A610C File Offset: 0x000A430C
        private byte[] HTNO(short x)
        {
            var bytes = BitConverter.GetBytes(x);
            Array.Reverse(bytes);
            return bytes;
        }

        // Token: 0x02000358 RID: 856
        public struct Pos
        {
            // Token: 0x04000CD4 RID: 3284
            public string type;

            // Token: 0x04000CD5 RID: 3285
            public string newscript;

            // Token: 0x04000CD6 RID: 3286
            public int seconds;

            // Token: 0x04000CD7 RID: 3287
            public int rotspeed;

            // Token: 0x04000CD8 RID: 3288
            public ushort x;

            // Token: 0x04000CD9 RID: 3289
            public ushort y;

            // Token: 0x04000CDA RID: 3290
            public ushort z;

            // Token: 0x04000CDB RID: 3291
            public byte rotx;

            // Token: 0x04000CDC RID: 3292
            public byte roty;
        }
    }
}