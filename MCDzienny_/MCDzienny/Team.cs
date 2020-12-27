using System;
using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x02000344 RID: 836
    public class Team
    {
        // Token: 0x04000C4C RID: 3148
        public char color;

        // Token: 0x04000C4E RID: 3150
        public ushort[] flagBase;

        // Token: 0x04000C53 RID: 3155
        public bool flagishome;

        // Token: 0x04000C4F RID: 3151
        public ushort[] flagLocation;

        // Token: 0x04000C55 RID: 3157
        public bool flagmoved;

        // Token: 0x04000C5A RID: 3162
        public int ftcount;

        // Token: 0x04000C57 RID: 3159
        public Player holdingFlag;

        // Token: 0x04000C52 RID: 3154
        public Level mapOn;

        // Token: 0x04000C51 RID: 3153
        public List<Player> players;

        // Token: 0x04000C4D RID: 3149
        public int points;

        // Token: 0x04000C50 RID: 3152
        public List<Spawn> spawns;

        // Token: 0x04000C54 RID: 3156
        public bool spawnset;

        // Token: 0x04000C56 RID: 3158
        public string teamstring;

        // Token: 0x04000C58 RID: 3160
        public CatchPos tempFlagblock;

        // Token: 0x04000C59 RID: 3161
        public CatchPos tfb;

        // Token: 0x06001812 RID: 6162 RVA: 0x000A1A54 File Offset: 0x0009FC54
        public Team()
        {
            var array = new ushort[3];
            flagBase = array;
            var array2 = new ushort[3];
            flagLocation = array2;
            spawns = new List<Spawn>();
            players = new List<Player>();
            teamstring = "";
            //base..ctor();
        }

        // Token: 0x0600180C RID: 6156 RVA: 0x000A12CC File Offset: 0x0009F4CC
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

                if (p.team != null) p.team.RemoveMember(p);
                p.team = this;
                Player.GlobalDie(p, false);
                p.CTFtempcolor = p.color;
                p.CTFtempprefix = p.prefix;
                p.color = "&" + color;
                p.carryingFlag = false;
                p.hasflag = null;
                p.prefix = p.color + "[" + c.Name("&" + color).ToUpper() + "]";
                players.Add(p);
                mapOn.ChatLevel(string.Concat(p.color, p.prefix, p.name, Server.DefaultColor, " has joined the ",
                    teamstring, "."));
                Player.GlobalSpawn(p, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1], false);
                if (mapOn.ctfgame.gameOn) SpawnPlayer(p);
            }
        }

        // Token: 0x0600180D RID: 6157 RVA: 0x000A1460 File Offset: 0x0009F660
        public void RemoveMember(Player p)
        {
            if (p.team == this)
            {
                if (p.carryingFlag) mapOn.ctfgame.DropFlag(p, p.hasflag);
                p.team = null;
                Player.GlobalDie(p, false);
                p.color = p.CTFtempcolor;
                p.prefix = p.CTFtempprefix;
                p.carryingFlag = false;
                p.hasflag = null;
                players.Remove(p);
                mapOn.ChatLevel(string.Concat(p.color, p.prefix, p.name, Server.DefaultColor, " has left the ",
                    teamstring, "."));
                Player.GlobalSpawn(p, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1], false);
            }
        }

        // Token: 0x0600180E RID: 6158 RVA: 0x000A1564 File Offset: 0x0009F764
        public void SpawnPlayer(Player p)
        {
            p.spawning = true;
            if (spawns.Count != 0)
            {
                var random = new Random();
                var index = random.Next(0, spawns.Count);
                var x = spawns[index].x;
                var y = spawns[index].y;
                var z = spawns[index].z;
                var x2 = (ushort) ((0.5 + x) * 32.0);
                var y2 = (ushort) ((1 + y) * 32);
                var z2 = (ushort) ((0.5 + z) * 32.0);
                var rotx = spawns[index].rotx;
                p.SendSpawn(byte.MaxValue, p.name, p.ModelName, x2, y2, z2, (byte) rotx, 0);
                p.health = 100;
            }
            else
            {
                var x3 = (ushort) ((0.5 + mapOn.spawnx) * 32.0);
                var y3 = (ushort) ((1 + mapOn.spawny) * 32);
                var z3 = (ushort) ((0.5 + mapOn.spawnz) * 32.0);
                ushort rotx2 = mapOn.rotx;
                ushort roty = mapOn.roty;
                p.SendSpawn(byte.MaxValue, p.PublicName, p.ModelName, x3, y3, z3, (byte) rotx2, (byte) roty);
            }

            p.spawning = false;
        }

        // Token: 0x0600180F RID: 6159 RVA: 0x000A16F4 File Offset: 0x0009F8F4
        public void AddSpawn(ushort x, ushort y, ushort z, ushort rotx, ushort roty)
        {
            var item = default(Spawn);
            item.x = x;
            item.y = y;
            item.z = z;
            item.rotx = rotx;
            item.roty = roty;
            spawns.Add(item);
        }

        // Token: 0x06001810 RID: 6160 RVA: 0x000A1740 File Offset: 0x0009F940
        public void Drawflag()
        {
            var x = flagLocation[0];
            var num = flagLocation[1];
            var z = flagLocation[2];
            if (mapOn.GetTile(x, (ushort) (num - 1), z) == 0) flagLocation[1] = (ushort) (flagLocation[1] - 1);
            mapOn.Blockchange(tfb.x, tfb.y, tfb.z, tfb.type);
            mapOn.Blockchange(tfb.x, (ushort) (tfb.y + 1), tfb.z, 0);
            mapOn.Blockchange(tfb.x, (ushort) (tfb.y + 2), tfb.z, 0);
            if (holdingFlag == null)
            {
                tfb.type = mapOn.GetTile(x, num, z);
                if (mapOn.GetTile(x, num, z) != 70) mapOn.Blockchange(x, num, z, 70);
                if (mapOn.GetTile(x, (ushort) (num + 1), z) != 39) mapOn.Blockchange(x, (ushort) (num + 1), z, 39);
                if (mapOn.GetTile(x, (ushort) (num + 2), z) != GetColorBlock(color))
                    mapOn.Blockchange(x, (ushort) (num + 2), z, GetColorBlock(color));
                tfb.x = x;
                tfb.y = num;
                tfb.z = z;
            }
            else
            {
                x = (ushort) (holdingFlag.pos[0] / 32);
                num = (ushort) (holdingFlag.pos[1] / 32 + 3);
                z = (ushort) (holdingFlag.pos[2] / 32);
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

        // Token: 0x06001811 RID: 6161 RVA: 0x000A1A18 File Offset: 0x0009FC18
        public static byte GetColorBlock(char color)
        {
            if (color == '2') return 25;
            if (color == '5') return 30;
            if (color == '8') return 34;
            if (color == '9') return 29;
            if (color == 'c') return 21;
            if (color == 'e') return 23;
            if (color == 'f') return 36;
            return 0;
        }

        // Token: 0x02000345 RID: 837
        public struct CatchPos
        {
            // Token: 0x04000C5B RID: 3163
            public ushort x;

            // Token: 0x04000C5C RID: 3164
            public ushort y;

            // Token: 0x04000C5D RID: 3165
            public ushort z;

            // Token: 0x04000C5E RID: 3166
            public byte type;
        }

        // Token: 0x02000346 RID: 838
        public struct Spawn
        {
            // Token: 0x04000C5F RID: 3167
            public ushort x;

            // Token: 0x04000C60 RID: 3168
            public ushort y;

            // Token: 0x04000C61 RID: 3169
            public ushort z;

            // Token: 0x04000C62 RID: 3170
            public ushort rotx;

            // Token: 0x04000C63 RID: 3171
            public ushort roty;
        }
    }
}