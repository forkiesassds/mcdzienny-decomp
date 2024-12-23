using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Timer = System.Timers.Timer;

namespace MCDzienny
{
    public sealed class PlayerBot
    {

        public static List<PlayerBot> playerbots = new List<PlayerBot>(64);

        readonly ushort[] basepos = new ushort[3];

        public string AIName = "";

        public Timer botTimer = new Timer(100.0);

        public string color;

        public int countdown;

        int currentjump;

        public int currentPoint;

        ushort[] foundPos = new ushort[3];

        byte[] foundRot = new byte[2];

        public bool hunt;

        public byte id;

        bool jumping;

        public Timer jumpTimer = new Timer(95.0);

        public bool kill;

        public Level level;

        bool movement;

        public int movementSpeed = 24;

        public Timer moveTimer = new Timer(4.0);

        public string name;

        public bool nodUp;

        ushort[] oldpos = new ushort[3];

        byte[] oldrot = new byte[2];

        public ushort[] pos = new ushort[3];

        public byte[] rot = new byte[2];

        public List<Pos> Waypoints = new List<Pos>();

        public PlayerBot(string n, Level l)
        {
            Server.s.Log("adding " + n + " bot");
            name = n;
            color = "&1";
            id = FreeId();
            level = l;
            ushort num = (ushort)((0.5 + level.spawnx) * 32.0);
            ushort num2 = (ushort)((1 + level.spawny) * 32);
            ushort num3 = (ushort)((0.5 + level.spawnz) * 32.0);
            pos = new ushort[3]
            {
                num, num2, num3
            };
            rot = new byte[2]
            {
                level.rotx, level.roty
            };
            GlobalSpawn();
        }

        public PlayerBot(string n, Level l, ushort x, ushort y, ushort z, byte rotx, byte roty)
        {
            PlayerBot playerBot = this;
            name = n;
            color = "&1";
            id = FreeId();
            level = l;
            pos = new ushort[3]
            {
                x, y, z
            };
            rot = new byte[2]
            {
                rotx, roty
            };
            GlobalSpawn();
            PlayerCollection players = Player.players;
            Action<Player> action = delegate(Player p)
            {
                if (p.level == level)
                {
                    Player.SendMessage(p, color + name + Server.DefaultColor + ", the bot, has been added.");
                }
            };
            players.ForEachSync(action);
            botTimer.Elapsed += delegate
            {
                int foundNum = 2400;
                new Random();
                x = (ushort)Math.Round(playerBot.pos[0] / 32m);
                y = (ushort)((playerBot.pos[1] - 33) / 32);
                z = (ushort)Math.Round(playerBot.pos[2] / 32m);
                if (playerBot.kill)
                {
                    Player.players.ForEachSync(delegate(Player p)
                    {
                        if ((ushort)(p.pos[0] / 32) == x && Math.Abs((ushort)(p.pos[1] / 32) - y) < 2 && (ushort)(p.pos[2] / 32) == z)
                        {
                            p.HandleDeath(byte.MaxValue);
                        }
                    });
                }
                if (playerBot.Waypoints.Count < 1)
                {
                    if (playerBot.hunt)
                    {
                        int currentNum;
                        Player.players.ForEachSync(delegate(Player p)
                        {
                            if (p.level == playerBot.level && !p.invincible)
                            {
                                currentNum = Math.Abs(p.pos[0] - playerBot.pos[0]) + Math.Abs(p.pos[1] - playerBot.pos[1]) + Math.Abs(p.pos[2] - playerBot.pos[2]);
                                if (currentNum < foundNum)
                                {
                                    foundNum = currentNum;
                                    playerBot.foundPos = p.pos;
                                    playerBot.foundRot = p.rot;
                                    playerBot.movement = true;
                                    playerBot.rot[1] = (byte)(255 - playerBot.foundRot[1]);
                                    if (playerBot.foundRot[0] < 128)
                                    {
                                        playerBot.rot[0] = (byte)(playerBot.foundRot[0] + 128);
                                    }
                                    else
                                    {
                                        playerBot.rot[0] = (byte)(playerBot.foundRot[0] - 128);
                                    }
                                }
                            }
                        });
                    }
                }
                else
                {
                    bool flag = false;
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
                                if ((ushort)(playerBot.pos[0] / 32) == (ushort)(playerBot.Waypoints[playerBot.currentPoint].x / 32) &&
                                    (ushort)(playerBot.pos[2] / 32) == (ushort)(playerBot.Waypoints[playerBot.currentPoint].z / 32))
                                {
                                    playerBot.rot[0] = playerBot.Waypoints[playerBot.currentPoint].rotx;
                                    playerBot.rot[1] = playerBot.Waypoints[playerBot.currentPoint].roty;
                                    playerBot.currentPoint++;
                                    playerBot.movement = false;
                                    if (playerBot.currentPoint == playerBot.Waypoints.Count)
                                    {
                                        playerBot.currentPoint = 0;
                                    }
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
                                if (playerBot.currentPoint == playerBot.Waypoints.Count)
                                {
                                    playerBot.currentPoint = 0;
                                }
                                return;
                            case "wait":
                                if (playerBot.countdown != 0)
                                {
                                    playerBot.countdown--;
                                    if (playerBot.countdown == 0)
                                    {
                                        playerBot.currentPoint++;
                                        if (playerBot.currentPoint == playerBot.Waypoints.Count)
                                        {
                                            playerBot.currentPoint = 0;
                                        }
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
                                        {
                                            playerBot.nodUp = !playerBot.nodUp;
                                        }
                                        else if (playerBot.rot[1] + (byte)playerBot.Waypoints[playerBot.currentPoint].rotspeed > 255)
                                        {
                                            playerBot.rot[1] = 0;
                                        }
                                        else
                                        {
                                            playerBot.rot[1] += (byte)playerBot.Waypoints[playerBot.currentPoint].rotspeed;
                                        }
                                    }
                                    else if (playerBot.rot[1] > 128 && playerBot.rot[1] < 224)
                                    {
                                        playerBot.nodUp = !playerBot.nodUp;
                                    }
                                    else if (playerBot.rot[1] - (byte)playerBot.Waypoints[playerBot.currentPoint].rotspeed < 0)
                                    {
                                        playerBot.rot[1] = byte.MaxValue;
                                    }
                                    else
                                    {
                                        playerBot.rot[1] -= (byte)playerBot.Waypoints[playerBot.currentPoint].rotspeed;
                                    }
                                    if (playerBot.countdown == 0)
                                    {
                                        playerBot.currentPoint++;
                                        if (playerBot.currentPoint == playerBot.Waypoints.Count)
                                        {
                                            playerBot.currentPoint = 0;
                                        }
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
                                    if (playerBot.rot[0] + (byte)playerBot.Waypoints[playerBot.currentPoint].rotspeed > 255)
                                    {
                                        playerBot.rot[0] = 0;
                                    }
                                    else if (playerBot.rot[0] + (byte)playerBot.Waypoints[playerBot.currentPoint].rotspeed < 0)
                                    {
                                        playerBot.rot[0] = byte.MaxValue;
                                    }
                                    else
                                    {
                                        playerBot.rot[0] += (byte)playerBot.Waypoints[playerBot.currentPoint].rotspeed;
                                    }
                                    if (playerBot.countdown == 0)
                                    {
                                        playerBot.currentPoint++;
                                        if (playerBot.currentPoint == playerBot.Waypoints.Count)
                                        {
                                            playerBot.currentPoint = 0;
                                        }
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
                                playerBot.movementSpeed = (int)Math.Round(0.24m * playerBot.Waypoints[playerBot.currentPoint].seconds);
                                if (playerBot.movementSpeed == 0)
                                {
                                    playerBot.movementSpeed = 1;
                                }
                                playerBot.currentPoint++;
                                if (playerBot.currentPoint == playerBot.Waypoints.Count)
                                {
                                    playerBot.currentPoint = 0;
                                }
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
                                    Command.all.Find("botset").Use(null, playerBot.name + " " + playerBot.Waypoints[playerBot.currentPoint].newscript);
                                }
                                else
                                {
                                    playerBot.currentPoint++;
                                    if (playerBot.currentPoint == playerBot.Waypoints.Count)
                                    {
                                        playerBot.currentPoint = 0;
                                    }
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
                                if (playerBot.currentPoint == playerBot.Waypoints.Count)
                                {
                                    playerBot.currentPoint = 0;
                                }
                                if (!flag)
                                {
                                    flag = true;
                                    continue;
                                }
                                break;
                        }
                        break;
                    }
                    if (playerBot.currentPoint == playerBot.Waypoints.Count)
                    {
                        playerBot.currentPoint = 0;
                    }
                }
                if (!playerBot.movement)
                {
                    if (playerBot.rot[0] < 245)
                    {
                        playerBot.rot[0] += 8;
                    }
                    else
                    {
                        playerBot.rot[0] = 0;
                    }
                    if (playerBot.rot[1] > 32 && playerBot.rot[1] < 64)
                    {
                        playerBot.rot[1] = 224;
                    }
                    else if (playerBot.rot[1] > 250)
                    {
                        playerBot.rot[1] = 0;
                    }
                    else
                    {
                        playerBot.rot[1] += 4;
                    }
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
                    {
                        playerBot.pos[1] = (ushort)(playerBot.pos[1] + 19 - playerBot.pos[1] % 32);
                    }
                    x = (ushort)Math.Round((playerBot.pos[0] - 16) / 32m);
                    y = (ushort)((playerBot.pos[1] - 64) / 32);
                    z = (ushort)Math.Round((playerBot.pos[2] - 16) / 32m);
                    byte type = Block.Convert(playerBot.level.GetTile(x, y, z));
                    if (Block.Walkthrough(type) && !playerBot.jumping)
                    {
                        playerBot.pos[1] = (ushort)(playerBot.pos[1] - 32);
                    }
                    y = (ushort)((playerBot.pos[1] - 64) / 32);
                    int b = playerBot.level.PosToInt((ushort)(x + Math.Sign(playerBot.foundPos[0] - playerBot.pos[0])), y,
                                                     (ushort)(z + Math.Sign(playerBot.foundPos[2] - playerBot.pos[2])));
                    type = Block.Convert(playerBot.level.GetTile(b));
                    byte type2 = Block.Convert(playerBot.level.GetTile(playerBot.level.IntOffset(b, 0, 1, 0)));
                    byte type3 = Block.Convert(playerBot.level.GetTile(playerBot.level.IntOffset(b, 0, 2, 0)));
                    byte type4 = Block.Convert(playerBot.level.GetTile(playerBot.level.IntOffset(b, 0, 3, 0)));
                    if (Block.Walkthrough(type3) && Block.Walkthrough(type4) && !Block.Walkthrough(type2))
                    {
                        playerBot.pos[0] += (ushort)Math.Sign(playerBot.foundPos[0] - playerBot.pos[0]);
                        playerBot.pos[1] += 32;
                        playerBot.pos[2] += (ushort)Math.Sign(playerBot.foundPos[2] - playerBot.pos[2]);
                    }
                    else if (Block.Walkthrough(type2) && Block.Walkthrough(type3))
                    {
                        playerBot.pos[0] += (ushort)Math.Sign(playerBot.foundPos[0] - playerBot.pos[0]);
                        playerBot.pos[2] += (ushort)Math.Sign(playerBot.foundPos[2] - playerBot.pos[2]);
                    }
                    else if (Block.Walkthrough(type) && Block.Walkthrough(type2))
                    {
                        playerBot.pos[0] += (ushort)Math.Sign(playerBot.foundPos[0] - playerBot.pos[0]);
                        playerBot.pos[1] -= 32;
                        playerBot.pos[2] += (ushort)Math.Sign(playerBot.foundPos[2] - playerBot.pos[2]);
                    }
                    x = (ushort)Math.Round((playerBot.pos[0] - 16) / 32m);
                    y = (ushort)((playerBot.pos[1] - 64) / 32);
                    z = (ushort)Math.Round((playerBot.pos[2] - 16) / 32m);
                    type2 = Block.Convert(playerBot.level.GetTile(x, (ushort)(y + 1), z));
                    type3 = Block.Convert(playerBot.level.GetTile(x, (ushort)(y + 2), z));
                    type4 = Block.Convert(playerBot.level.GetTile(x, y, z));
                }
            };
            moveTimer.Start();
        }

        public void SetPos(ushort x, ushort y, ushort z, byte rotx, byte roty)
        {
            pos = new ushort[3]
            {
                x, y, z
            };
            rot = new byte[2]
            {
                rotx, roty
            };
        }

        public void removeBot()
        {
            botTimer.Stop();
            GlobalDie();
            playerbots.Remove(this);
        }

        public void GlobalSpawn()
        {
            Player.players.ForEachSync(delegate(Player p)
            {
                if (p.level == level)
                {
                    p.SendSpawn(id, color + name, pos[0], pos[1], pos[2], rot[0], rot[1]);
                }
            });
        }

        public void GlobalDie()
        {
            Server.s.Log("removing " + name + " bot");
            Player.players.ForEachSync(delegate(Player p)
            {
                if (p.level == level)
                {
                    p.SendDie(id);
                }
            });
            playerbots.Remove(this);
        }

        public void Update() {}

        void UpdatePosition()
        {
            byte b = 0;
            if (oldpos[0] != pos[0] || oldpos[1] != pos[1] || oldpos[2] != pos[2])
            {
                b |= 1;
            }
            if (oldrot[0] != rot[0] || oldrot[1] != rot[1])
            {
                b |= 2;
            }
            if (Math.Abs(pos[0] - basepos[0]) > 32 || Math.Abs(pos[1] - basepos[1]) > 32 || Math.Abs(pos[2] - basepos[2]) > 32)
            {
                b |= 4;
            }
            if (oldpos[0] == pos[0] && oldpos[1] == pos[1] && oldpos[2] == pos[2] && (basepos[0] != pos[0] || basepos[1] != pos[1] || basepos[2] != pos[2]))
            {
                b |= 4;
            }
            byte[] buffer = new byte[0];
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
            else
            {
                switch (b)
                {
                    case 1:
                        msg = 10;
                        buffer = new byte[4];
                        buffer[0] = id;
                        buffer[1] = (byte)(pos[0] - oldpos[0]);
                        buffer[2] = (byte)(pos[1] - oldpos[1]);
                        buffer[3] = (byte)(pos[2] - oldpos[2]);
                        break;
                    case 2:
                        msg = 11;
                        buffer = new byte[3];
                        buffer[0] = id;
                        buffer[1] = rot[0];
                        buffer[2] = rot[1];
                        break;
                    case 3:
                        msg = 9;
                        buffer = new byte[6];
                        buffer[0] = id;
                        buffer[1] = (byte)(pos[0] - oldpos[0]);
                        buffer[2] = (byte)(pos[1] - oldpos[1]);
                        buffer[3] = (byte)(pos[2] - oldpos[2]);
                        buffer[4] = rot[0];
                        buffer[5] = rot[1];
                        break;
                }
            }
            try
            {
                if (b != 0)
                {
                    Player.players.ForEachSync(delegate(Player p)
                    {
                        if (p.level == level && !p.Loading)
                        {
                            p.SendRaw(msg, buffer);
                        }
                    });
                }
            }
            catch {}
            oldpos = pos;
            oldrot = rot;
        }

        static byte FreeId()
        {
            for (byte b = 64; b < 128; b++)
            {
                using (List<PlayerBot>.Enumerator enumerator = playerbots.GetEnumerator())
                {
                    PlayerBot current;
                    do
                    {
                        if (enumerator.MoveNext())
                        {
                            current = enumerator.Current;
                            continue;
                        }
                        return b;
                    } while (current.id != b);
                }
            }
            return byte.MaxValue;
        }

        public static PlayerBot Find(string name)
        {
            PlayerBot playerBot = null;
            bool flag = false;
            foreach (PlayerBot playerbot in playerbots)
            {
                if (playerbot.name.ToLower() == name.ToLower())
                {
                    return playerbot;
                }
                if (playerbot.name.ToLower().IndexOf(name.ToLower()) != -1)
                {
                    if (playerBot == null)
                    {
                        playerBot = playerbot;
                    }
                    else
                    {
                        flag = true;
                    }
                }
            }
            if (flag)
            {
                return null;
            }
            if (playerBot != null)
            {
                return playerBot;
            }
            return null;
        }

        public static bool ValidName(string name)
        {
            string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz01234567890_";
            foreach (char value in name)
            {
                if (text.IndexOf(value) == -1)
                {
                    return false;
                }
            }
            return true;
        }

        public static void GlobalUpdatePosition()
        {
            playerbots.ForEach(delegate(PlayerBot b) { b.UpdatePosition(); });
        }

        public static void GlobalUpdate()
        {
            while (true)
            {
                Thread.Sleep(100);
                playerbots.ForEach(delegate(PlayerBot b) { b.Update(); });
            }
        }

        byte[] HTNO(ushort x)
        {
            byte[] bytes = BitConverter.GetBytes(x);
            Array.Reverse(bytes);
            return bytes;
        }

        ushort NTHO(byte[] x, int offset)
        {
            byte[] array = new byte[2];
            Buffer.BlockCopy(x, offset, array, 0, 2);
            Array.Reverse(array);
            return BitConverter.ToUInt16(array, 0);
        }

        byte[] HTNO(short x)
        {
            byte[] bytes = BitConverter.GetBytes(x);
            Array.Reverse(bytes);
            return bytes;
        }

        public struct Pos
        {
            public string type;

            public string newscript;

            public int seconds;

            public int rotspeed;

            public ushort x;

            public ushort y;

            public ushort z;

            public byte rotx;

            public byte roty;
        }
    }
}