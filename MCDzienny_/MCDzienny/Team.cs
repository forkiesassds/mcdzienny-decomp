using System;
using System.Collections.Generic;

namespace MCDzienny
{
    public class Team
    {

        public char color;

        public ushort[] flagBase = new ushort[3];

        public bool flagishome;

        public ushort[] flagLocation = new ushort[3];

        public bool flagmoved;

        public int ftcount;

        public Player holdingFlag;

        public Level mapOn;

        public List<Player> players = new List<Player>();

        public int points;

        public List<Spawn> spawns = new List<Spawn>();

        public bool spawnset;

        public string teamstring = "";

        public CatchPos tempFlagblock;

        public CatchPos tfb;

        public void AddMember(Player p)
        {
            if (p.team != this)
            {
                if (p.carryingFlag)
                {
                    p.spawning = true;
                    mapOn.ctfgame.DropFlag(p, p.hasflag);
                    p.spawning = false;
                }
                if (p.team != null)
                {
                    p.team.RemoveMember(p);
                }
                p.team = this;
                Player.GlobalDie(p, self: false);
                p.CTFtempcolor = p.color;
                p.CTFtempprefix = p.prefix;
                p.color = "&" + color;
                p.carryingFlag = false;
                p.hasflag = null;
                p.prefix = p.color + "[" + c.Name("&" + color).ToUpper() + "]";
                players.Add(p);
                mapOn.ChatLevel(p.color + p.prefix + p.name + Server.DefaultColor + " has joined the " + teamstring + ".");
                Player.GlobalSpawn(p, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1], self: false);
                if (mapOn.ctfgame.gameOn)
                {
                    SpawnPlayer(p);
                }
            }
        }

        public void RemoveMember(Player p)
        {
            if (p.team == this)
            {
                if (p.carryingFlag)
                {
                    mapOn.ctfgame.DropFlag(p, p.hasflag);
                }
                p.team = null;
                Player.GlobalDie(p, self: false);
                p.color = p.CTFtempcolor;
                p.prefix = p.CTFtempprefix;
                p.carryingFlag = false;
                p.hasflag = null;
                players.Remove(p);
                mapOn.ChatLevel(p.color + p.prefix + p.name + Server.DefaultColor + " has left the " + teamstring + ".");
                Player.GlobalSpawn(p, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1], self: false);
            }
        }

        public void SpawnPlayer(Player p)
        {
            p.spawning = true;
            if (spawns.Count != 0)
            {
                Random random = new Random();
                int index = random.Next(0, spawns.Count);
                ushort x = spawns[index].x;
                ushort y = spawns[index].y;
                ushort z = spawns[index].z;
                ushort x2 = (ushort)((0.5 + x) * 32.0);
                ushort y2 = (ushort)((1 + y) * 32);
                ushort z2 = (ushort)((0.5 + z) * 32.0);
                ushort rotx = spawns[index].rotx;
                p.SendSpawn(byte.MaxValue, p.name, p.ModelName, x2, y2, z2, (byte)rotx, 0);
                p.health = 100;
            }
            else
            {
                ushort x3 = (ushort)((0.5 + mapOn.spawnx) * 32.0);
                ushort y3 = (ushort)((1 + mapOn.spawny) * 32);
                ushort z3 = (ushort)((0.5 + mapOn.spawnz) * 32.0);
                ushort rotx2 = mapOn.rotx;
                ushort roty = mapOn.roty;
                p.SendSpawn(byte.MaxValue, p.PublicName, p.ModelName, x3, y3, z3, (byte)rotx2, (byte)roty);
            }
            p.spawning = false;
        }

        public void AddSpawn(ushort x, ushort y, ushort z, ushort rotx, ushort roty)
        {
            Spawn item = default(Spawn);
            item.x = x;
            item.y = y;
            item.z = z;
            item.rotx = rotx;
            item.roty = roty;
            spawns.Add(item);
        }

        public void Drawflag()
        {
            ushort x = flagLocation[0];
            ushort num = flagLocation[1];
            ushort z = flagLocation[2];
            if (mapOn.GetTile(x, (ushort)(num - 1), z) == 0)
            {
                flagLocation[1] = (ushort)(flagLocation[1] - 1);
            }
            mapOn.Blockchange(tfb.x, tfb.y, tfb.z, tfb.type);
            mapOn.Blockchange(tfb.x, (ushort)(tfb.y + 1), tfb.z, 0);
            mapOn.Blockchange(tfb.x, (ushort)(tfb.y + 2), tfb.z, 0);
            if (holdingFlag == null)
            {
                tfb.type = mapOn.GetTile(x, num, z);
                if (mapOn.GetTile(x, num, z) != 70)
                {
                    mapOn.Blockchange(x, num, z, 70);
                }
                if (mapOn.GetTile(x, (ushort)(num + 1), z) != 39)
                {
                    mapOn.Blockchange(x, (ushort)(num + 1), z, 39);
                }
                if (mapOn.GetTile(x, (ushort)(num + 2), z) != GetColorBlock(color))
                {
                    mapOn.Blockchange(x, (ushort)(num + 2), z, GetColorBlock(color));
                }
                tfb.x = x;
                tfb.y = num;
                tfb.z = z;
            }
            else
            {
                x = (ushort)(holdingFlag.pos[0] / 32);
                num = (ushort)(holdingFlag.pos[1] / 32 + 3);
                z = (ushort)(holdingFlag.pos[2] / 32);
                if (tempFlagblock.x != x || tempFlagblock.y != num || tempFlagblock.z != z)
                {
                    mapOn.Blockchange(tempFlagblock.x, tempFlagblock.y, tempFlagblock.z, tempFlagblock.type);
                    tempFlagblock.type = mapOn.GetTile(x, num, z);
                    mapOn.Blockchange(x, num, z, GetColorBlock(color));
                    tempFlagblock.x = x;
                    tempFlagblock.y = num;
                    tempFlagblock.z = z;
                }
            }
        }

        public static byte GetColorBlock(char color)
        {
            switch (color)
            {
                case '2':
                    return 25;
                case '5':
                    return 30;
                case '8':
                    return 34;
                case '9':
                    return 29;
                case 'c':
                    return 21;
                case 'e':
                    return 23;
                case 'f':
                    return 36;
                default:
                    return 0;
            }
        }

        public struct CatchPos
        {
            public ushort x;

            public ushort y;

            public ushort z;

            public byte type;
        }

        public struct Spawn
        {
            public ushort x;

            public ushort y;

            public ushort z;

            public ushort rotx;

            public ushort roty;
        }
    }
}