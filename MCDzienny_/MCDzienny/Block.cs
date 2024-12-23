using System;
using System.Collections.Generic;
using System.IO;

namespace MCDzienny
{
    public sealed class Block
    {

        public const byte air = 0;

        public const byte rock = 1;

        public const byte grass = 2;

        public const byte dirt = 3;

        public const byte stone = 4;

        public const byte wood = 5;

        public const byte shrub = 6;

        public const byte blackrock = 7;

        public const byte water = 8;

        public const byte waterstill = 9;

        public const byte lava = 10;

        public const byte lavastill = 11;

        public const byte sand = 12;

        public const byte gravel = 13;

        public const byte goldrock = 14;

        public const byte ironrock = 15;

        public const byte coal = 16;

        public const byte trunk = 17;

        public const byte leaf = 18;

        public const byte sponge = 19;

        public const byte glass = 20;

        public const byte red = 21;

        public const byte orange = 22;

        public const byte yellow = 23;

        public const byte lightgreen = 24;

        public const byte green = 25;

        public const byte aquagreen = 26;

        public const byte cyan = 27;

        public const byte lightblue = 28;

        public const byte blue = 29;

        public const byte purple = 30;

        public const byte lightpurple = 31;

        public const byte pink = 32;

        public const byte darkpink = 33;

        public const byte darkgrey = 34;

        public const byte lightgrey = 35;

        public const byte white = 36;

        public const byte yellowflower = 37;

        public const byte redflower = 38;

        public const byte mushroom = 39;

        public const byte redmushroom = 40;

        public const byte goldsolid = 41;

        public const byte iron = 42;

        public const byte staircasefull = 43;

        public const byte staircasestep = 44;

        public const byte brick = 45;

        public const byte tnt = 46;

        public const byte bookcase = 47;

        public const byte stonevine = 48;

        public const byte obsidian = 49;

        public const byte Zero = byte.MaxValue;

        public const byte op_glass = 100;

        public const byte opsidian = 101;

        public const byte op_lava = 102;

        public const byte op_stone = 103;

        public const byte op_cobblestone = 104;

        public const byte op_air = 105;

        public const byte op_water = 106;

        public const byte wood_float = 110;

        public const byte door = 111;

        public const byte lava_fast = 112;

        public const byte door2 = 113;

        public const byte door3 = 114;

        public const byte door4 = 115;

        public const byte door5 = 116;

        public const byte door6 = 117;

        public const byte door7 = 118;

        public const byte door8 = 119;

        public const byte door9 = 120;

        public const byte door10 = 121;

        public const byte tdoor = 122;

        public const byte tdoor2 = 123;

        public const byte tdoor3 = 124;

        public const byte tdoor4 = 125;

        public const byte tdoor5 = 126;

        public const byte tdoor6 = 127;

        public const byte tdoor7 = 128;

        public const byte tdoor8 = 129;

        public const byte MsgWhite = 130;

        public const byte MsgBlack = 131;

        public const byte MsgAir = 132;

        public const byte MsgWater = 133;

        public const byte MsgLava = 134;

        public const byte tdoor9 = 135;

        public const byte tdoor10 = 136;

        public const byte tdoor11 = 137;

        public const byte tdoor12 = 138;

        public const byte tdoor13 = 139;

        public const byte WaterDown = 140;

        public const byte LavaDown = 141;

        public const byte WaterFaucet = 143;

        public const byte LavaFaucet = 144;

        public const byte finiteWater = 145;

        public const byte finiteLava = 146;

        public const byte finiteFaucet = 147;

        public const byte odoor1 = 148;

        public const byte odoor2 = 149;

        public const byte odoor3 = 150;

        public const byte odoor4 = 151;

        public const byte odoor5 = 152;

        public const byte odoor6 = 153;

        public const byte odoor7 = 154;

        public const byte odoor8 = 155;

        public const byte odoor9 = 156;

        public const byte odoor10 = 157;

        public const byte odoor11 = 158;

        public const byte odoor12 = 159;

        public const byte air_portal = 160;

        public const byte water_portal = 161;

        public const byte lava_portal = 162;

        public const byte air_door = 164;

        public const byte air_switch = 165;

        public const byte water_door = 166;

        public const byte lava_door = 167;

        public const byte odoor1_air = 168;

        public const byte odoor2_air = 169;

        public const byte odoor3_air = 170;

        public const byte odoor4_air = 171;

        public const byte odoor5_air = 172;

        public const byte odoor6_air = 173;

        public const byte odoor7_air = 174;

        public const byte blue_portal = 175;

        public const byte orange_portal = 176;

        public const byte odoor8_air = 177;

        public const byte odoor9_air = 178;

        public const byte odoor10_air = 179;

        public const byte odoor11_air = 180;

        public const byte odoor12_air = 181;

        public const byte smalltnt = 182;

        public const byte bigtnt = 183;

        public const byte tntexplosion = 184;

        public const byte fire = 185;

        public const byte rocketstart = 187;

        public const byte rockethead = 188;

        public const byte firework = 189;

        public const byte deathlava = 190;

        public const byte deathwater = 191;

        public const byte deathair = 192;

        public const byte activedeathwater = 193;

        public const byte activedeathlava = 194;

        public const byte magma = 195;

        public const byte geyser = 196;

        public const byte air_flood = 200;

        public const byte door_air = 201;

        public const byte air_flood_layer = 202;

        public const byte air_flood_down = 203;

        public const byte air_flood_up = 204;

        public const byte door2_air = 205;

        public const byte door3_air = 206;

        public const byte door4_air = 207;

        public const byte door5_air = 208;

        public const byte door6_air = 209;

        public const byte door7_air = 210;

        public const byte door8_air = 211;

        public const byte door9_air = 212;

        public const byte door10_air = 213;

        public const byte door11_air = 214;

        public const byte door12_air = 215;

        public const byte door13_air = 216;

        public const byte door14_air = 217;

        public const byte door_iron = 220;

        public const byte door_dirt = 221;

        public const byte door_grass = 222;

        public const byte door_blue = 223;

        public const byte door_book = 224;

        public const byte door_iron_air = 225;

        public const byte door_dirt_air = 226;

        public const byte door_grass_air = 227;

        public const byte door_blue_air = 228;

        public const byte door_book_air = 229;

        public const byte train = 230;

        public const byte creeper = 231;

        public const byte zombiebody = 232;

        public const byte zombiehead = 233;

        public const byte birdwhite = 235;

        public const byte birdblack = 236;

        public const byte birdwater = 237;

        public const byte birdlava = 238;

        public const byte birdred = 239;

        public const byte birdblue = 240;

        public const byte birdkill = 242;

        public const byte fishgold = 245;

        public const byte fishsponge = 246;

        public const byte fishshark = 247;

        public const byte fishsalmon = 248;

        public const byte fishbetta = 249;

        public const byte fishlavashark = 250;

        public const byte snake = 251;

        public const byte snaketail = 252;

        public const byte universalsponge = 253;

        public const byte meltingglass = 254;

        public const byte cobbleSlab = 50;

        public const byte rope = 51;

        public const byte sandstone = 52;

        public const byte snow = 53;

        public const byte realFire = 54;

        public const byte lightPink = 55;

        public const byte forestGreen = 56;

        public const byte brown = 57;

        public const byte navy = 58;

        public const byte turquoise = 59;

        public const byte ice = 60;

        public const byte ceramicTile = 61;

        public const byte lavaObsidian = 62;

        public const byte pillar = 63;

        public const byte crate = 64;

        public const byte stoneBrick = 65;

        public const byte blue_gel = 66;

        public const byte red_gel = 67;

        public const byte blue_port = 68;

        public const byte orange_port = 69;

        public const byte meteor = 99;

        public const byte lavaup = 98;

        public const byte treasure = 97;

        public const byte dirtbomb = 96;

        public const byte flagbase = 70;

        public const byte sandstill = 71;

        public const byte door_adminium = 72;

        public const byte door_adminium_air = 73;

        public const byte waterUp = 74;

        public const byte weaktnt = 75;

        public const byte smog = 76;

        public const byte smogbomb = 77;

        public const byte psponge = 78;

        public const byte timedcoal = 79;

        public const byte lavatypea = 80;

        public const byte lavatypeb = 81;

        public const byte lavatypec = 82;

        public const byte lavatyped = 83;

        public static List<Blocks> BlockList = new List<Blocks>();

        public static Dictionary<byte, Blocks> BlocksPermissions = new Dictionary<byte, Blocks>();

        static readonly byte[] conversion = new byte[256];

        public static byte ToMoving(byte block)
        {
            byte b = Convert(block);
            switch (b)
            {
                case 11:
                    return 10;
                case 9:
                    return 8;
                default:
                    return b;
            }
        }

        public static AABB GetBounds(byte block)
        {
            switch (Convert(block))
            {
                case 37:
                case 38:
                case 39:
                case 40:
                    return new AABB(0.3f, 0f, 0.3f, 0.7f, 0.6f, 0.7f);
                case 6:
                    return new AABB(0.1f, 0f, 0.1f, 0.9f, 0.8f, 0.9f);
                case 44:
                case 50:
                    return new AABB(0f, 0f, 0f, 1f, 0.5f, 1f);
                case 53:
                    return new AABB(0f, 0f, 0f, 1f, 0.2f, 1f);
                default:
                    return new AABB(0f, 0f, 0f, 1f, 1f, 1f);
            }
        }

        static bool xIntersects(AABB bounds, Vector3F var1)
        {
            if (var1 != null)
            {
                if (var1.Y >= bounds.y0 && var1.Y <= bounds.y1 && var1.Z >= bounds.z0)
                {
                    return var1.Z <= bounds.z1;
                }
                return false;
            }
            return false;
        }

        static bool yIntersects(AABB bounds, Vector3F var1)
        {
            if (var1 != null)
            {
                if (var1.X >= bounds.x0 && var1.X <= bounds.x1 && var1.Z >= bounds.z0)
                {
                    return var1.Z <= bounds.z1;
                }
                return false;
            }
            return false;
        }

        static bool zIntersects(AABB bounds, Vector3F var1)
        {
            if (var1 != null)
            {
                if (var1.X >= bounds.x0 && var1.X <= bounds.x1 && var1.Y >= bounds.y0)
                {
                    return var1.Y <= bounds.y1;
                }
                return false;
            }
            return false;
        }

        public static MovingObjectPosition clip(byte block, int x, int y, int z, Vector3F a, Vector3F b)
        {
            a = a.add(-x, -y, -z);
            b = b.add(-x, -y, -z);
            AABB bounds = GetBounds(block);
            Vector3F vector3F = a.getXIntersection(b, bounds.x0);
            Vector3F vector3F2 = a.getXIntersection(b, bounds.x1);
            Vector3F vector3F3 = a.getYIntersection(b, bounds.y0);
            Vector3F vector3F4 = a.getYIntersection(b, bounds.y1);
            Vector3F vector3F5 = a.getZIntersection(b, bounds.z0);
            b = a.getZIntersection(b, bounds.z1);
            if (!xIntersects(bounds, vector3F))
            {
                vector3F = null;
            }
            if (!xIntersects(bounds, vector3F2))
            {
                vector3F2 = null;
            }
            if (!yIntersects(bounds, vector3F3))
            {
                vector3F3 = null;
            }
            if (!yIntersects(bounds, vector3F4))
            {
                vector3F4 = null;
            }
            if (!zIntersects(bounds, vector3F5))
            {
                vector3F5 = null;
            }
            if (!zIntersects(bounds, b))
            {
                b = null;
            }
            Vector3F vector3F6 = null;
            if (vector3F != null)
            {
                vector3F6 = vector3F;
            }
            if (vector3F2 != null && (vector3F6 == null || a.distance(vector3F2) < a.distance(vector3F6)))
            {
                vector3F6 = vector3F2;
            }
            if (vector3F3 != null && (vector3F6 == null || a.distance(vector3F3) < a.distance(vector3F6)))
            {
                vector3F6 = vector3F3;
            }
            if (vector3F4 != null && (vector3F6 == null || a.distance(vector3F4) < a.distance(vector3F6)))
            {
                vector3F6 = vector3F4;
            }
            if (vector3F5 != null && (vector3F6 == null || a.distance(vector3F5) < a.distance(vector3F6)))
            {
                vector3F6 = vector3F5;
            }
            if (b != null && (vector3F6 == null || a.distance(b) < a.distance(vector3F6)))
            {
                vector3F6 = b;
            }
            if (vector3F6 == null)
            {
                return null;
            }
            sbyte side = -1;
            if (vector3F6 == vector3F)
            {
                side = 4;
            }
            if (vector3F6 == vector3F2)
            {
                side = 5;
            }
            if (vector3F6 == vector3F3)
            {
                side = 0;
            }
            if (vector3F6 == vector3F4)
            {
                side = 1;
            }
            if (vector3F6 == vector3F5)
            {
                side = 2;
            }
            if (vector3F6 == b)
            {
                side = 3;
            }
            return new MovingObjectPosition(x, y, z, side, vector3F6.add(x, y, z));
        }

        public static bool IsLiquid(byte block)
        {
            switch (Convert(block))
            {
                case 8:
                case 9:
                case 10:
                case 11:
                    return true;
                default:
                    return false;
            }
        }

        public static AABB GetCollisionBox(byte block)
        {
            switch (Convert(block))
            {
                case 8:
                case 9:
                case 10:
                case 11:
                    return null;
                case 44:
                case 50:
                    return new AABB(0f, 0f, 0f, 1f, 0.5f, 1f);
                case 0:
                case 6:
                case 37:
                case 38:
                case 39:
                case 40:
                case 51:
                case 53:
                case 185:
                    return null;
                default:
                    return new AABB(0f, 0f, 0f, 1f, 1f, 1f);
            }
        }

        public static AABB GetCollisionBox(byte block, int x, int y, int z)
        {
            AABB collisionBox = GetCollisionBox(block);
            if (collisionBox != null)
            {
                collisionBox.move(x, y, z);
            }
            return collisionBox;
        }

        public static bool IsAir(byte tile)
        {
            if (tile == 0 || tile == 105)
            {
                return true;
            }
            return false;
        }

        public static void SetBlocks()
        {
            FillConversion();
            BlockList = new List<Blocks>();
            Blocks blocks = new Blocks();
            blocks.lowestRank = LevelPermission.Guest;
            for (int i = 0; i < 256; i++)
            {
                blocks = new Blocks();
                blocks.type = (byte)i;
                BlockList.Add(blocks);
            }
            var list = new List<Blocks>();
            foreach (Blocks block in BlockList)
            {
                blocks = new Blocks();
                blocks.type = block.type;
                switch (block.type)
                {
                    case 97:
                    case byte.MaxValue:
                        blocks.lowestRank = LevelPermission.Admin;
                        break;
                    case 7:
                    case 70:
                    case 78:
                    case 100:
                    case 101:
                    case 102:
                    case 103:
                    case 104:
                    case 105:
                    case 106:
                    case 183:
                    case 187:
                    case 188:
                    case 200:
                    case 202:
                    case 203:
                    case 204:
                    case 231:
                    case 232:
                    case 233:
                    case 239:
                    case 240:
                    case 242:
                    case 245:
                    case 246:
                    case 247:
                    case 248:
                    case 249:
                    case 250:
                    case 251:
                    case 252:
                        blocks.lowestRank = LevelPermission.Operator;
                        break;
                    case 8:
                    case 10:
                    case 73:
                    case 74:
                    case 75:
                    case 76:
                    case 77:
                    case 80:
                    case 81:
                    case 82:
                    case 83:
                    case 98:
                    case 99:
                    case 110:
                    case 112:
                    case 130:
                    case 131:
                    case 132:
                    case 133:
                    case 134:
                    case 140:
                    case 141:
                    case 143:
                    case 144:
                    case 145:
                    case 146:
                    case 147:
                    case 160:
                    case 161:
                    case 162:
                    case 168:
                    case 169:
                    case 170:
                    case 171:
                    case 172:
                    case 173:
                    case 174:
                    case 175:
                    case 176:
                    case 177:
                    case 178:
                    case 179:
                    case 180:
                    case 181:
                    case 182:
                    case 184:
                    case 185:
                    case 189:
                    case 190:
                    case 191:
                    case 192:
                    case 193:
                    case 194:
                    case 195:
                    case 196:
                    case 201:
                    case 205:
                    case 206:
                    case 207:
                    case 208:
                    case 209:
                    case 210:
                    case 211:
                    case 212:
                    case 213:
                    case 214:
                    case 215:
                    case 216:
                    case 217:
                    case 225:
                    case 226:
                    case 227:
                    case 228:
                    case 229:
                    case 230:
                    case 235:
                    case 236:
                    case 237:
                    case 238:
                        blocks.lowestRank = LevelPermission.AdvBuilder;
                        break;
                    case 72:
                    case 96:
                    case 111:
                    case 113:
                    case 114:
                    case 115:
                    case 116:
                    case 117:
                    case 118:
                    case 119:
                    case 120:
                    case 121:
                    case 122:
                    case 123:
                    case 124:
                    case 125:
                    case 126:
                    case 127:
                    case 128:
                    case 129:
                    case 135:
                    case 136:
                    case 137:
                    case 138:
                    case 139:
                    case 148:
                    case 149:
                    case 150:
                    case 151:
                    case 152:
                    case 153:
                    case 154:
                    case 155:
                    case 156:
                    case 157:
                    case 158:
                    case 159:
                    case 164:
                    case 165:
                    case 166:
                    case 167:
                    case 220:
                    case 221:
                    case 222:
                    case 223:
                    case 224:
                        blocks.lowestRank = LevelPermission.Builder;
                        break;
                    default:
                        blocks.lowestRank = LevelPermission.Banned;
                        break;
                }
                list.Add(blocks);
            }
            if (File.Exists("properties/block.properties"))
            {
                string[] array = File.ReadAllLines("properties/block.properties");
                if (array[0] == "#Version 2")
                {
                    string[] separator = new string[1]
                    {
                        " : "
                    };
                    string[] array2 = array;
                    foreach (string text in array2)
                    {
                        if (!(text != "") || text[0] == '#')
                        {
                            continue;
                        }
                        string[] array3 = text.Split(separator, StringSplitOptions.None);
                        Blocks blocks2 = new Blocks();
                        if (Byte(array3[0]) == byte.MaxValue)
                        {
                            continue;
                        }
                        blocks2.type = Byte(array3[0]);
                        string[] array4 = new string[0];
                        if (array3[2] != "")
                        {
                            array4 = array3[2].Split(',');
                        }
                        string[] array5 = new string[0];
                        if (array3[3] != "")
                        {
                            array5 = array3[3].Split(',');
                        }
                        try
                        {
                            blocks2.lowestRank = (LevelPermission)int.Parse(array3[1]);
                            string[] array6 = array4;
                            foreach (string s in array6)
                            {
                                blocks2.disallow.Add((LevelPermission)int.Parse(s));
                            }
                            string[] array7 = array5;
                            foreach (string s2 in array7)
                            {
                                blocks2.allow.Add((LevelPermission)int.Parse(s2));
                            }
                        }
                        catch
                        {
                            Server.s.Log("Hit an error on the block " + text);
                            continue;
                        }
                        int num = 0;
                        foreach (Blocks item in list)
                        {
                            if (blocks2.type == item.type)
                            {
                                list[num] = blocks2;
                                break;
                            }
                            num++;
                        }
                    }
                }
                else
                {
                    string[] array8 = array;
                    foreach (string text2 in array8)
                    {
                        if (text2[0] == '#')
                        {
                            continue;
                        }
                        try
                        {
                            Blocks newBlock = new Blocks();
                            newBlock.type = Byte(text2.Split(' ')[0]);
                            newBlock.lowestRank = Level.PermissionFromName(text2.Split(' ')[2]);
                            if (newBlock.lowestRank != LevelPermission.Null)
                            {
                                list[list.FindIndex(sL => sL.type == newBlock.type)] = newBlock;
                                continue;
                            }
                            throw new Exception();
                        }
                        catch
                        {
                            Server.s.Log("Could not find the rank given on " + text2 + ". Using default");
                        }
                    }
                }
            }
            BlockList.Clear();
            BlockList.AddRange(list);
            SaveBlocks(BlockList);
        }

        public static void SaveBlocks(List<Blocks> givenList)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(File.Create("properties/block.properties")))
                {
                    streamWriter.WriteLine("#Version 2");
                    streamWriter.WriteLine("#   This file dictates what levels may use what blocks");
                    streamWriter.WriteLine("#   If someone has royally screwed up the ranks, just delete this file and let the server restart");
                    streamWriter.WriteLine("#   Allowed ranks: " + Group.concatList(includeColor: false, skipExtra: false, permissions: true));
                    streamWriter.WriteLine("#   Disallow and allow can be left empty, just make sure there's 2 spaces between the colons");
                    streamWriter.WriteLine("#   This works entirely on permission values, not names. Do not enter a rank name. Use it's permission value");
                    streamWriter.WriteLine("#   BlockName : LowestRank : Disallow : Allow");
                    streamWriter.WriteLine("#   lava : 60 : 80,67 : 40,41,55");
                    streamWriter.WriteLine("");
                    foreach (Blocks given in givenList)
                    {
                        if (Name(given.type).ToLower() != "unknown")
                        {
                            string value = Name(given.type) + " : " + (int)given.lowestRank + " : " + GrpCommands.getInts(given.disallow) + " : " +
                                GrpCommands.getInts(given.allow);
                            streamWriter.WriteLine(value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        public static bool canPlace(Player p, byte b)
        {
            return canPlace(p.group.Permission, b);
        }

        public static bool canPlace(LevelPermission givenPerm, byte givenBlock)
        {
            foreach (Blocks block in BlockList)
            {
                if (givenBlock == block.type)
                {
                    if (block.lowestRank <= givenPerm && !block.disallow.Contains(givenPerm) || block.allow.Contains(givenPerm))
                    {
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }

        public static byte ConvertFromBlockmension(byte block)
        {
            switch (block)
            {
                case 66:
                    return 29;
                case 67:
                    return 21;
                case 68:
                    return 8;
                case 69:
                    return 10;
                default:
                    return block;
            }
        }

        public static byte ConvertExtended(byte block)
        {
            switch (block)
            {
                case 51:
                    return 39;
                case 60:
                    return 20;
                case 54:
                    return 10;
                case 58:
                    return 29;
                case 59:
                    return 27;
                case 56:
                    return 25;
                case 63:
                    return 36;
                case 61:
                    return 42;
                case 50:
                    return 44;
                case 52:
                    return 12;
                case 53:
                    return 0;
                case 55:
                    return 32;
                case 57:
                    return 3;
                case 62:
                    return 49;
                case 64:
                    return 5;
                case 65:
                    return 4;
                case 66:
                    return 29;
                case 67:
                    return 21;
                case 68:
                    return 8;
                case 69:
                    return 10;
                default:
                    return block;
            }
        }

        public static bool Walkthrough(byte type)
        {
            type = Convert(type);
            switch (type)
            {
                case 0:
                case 6:
                case 8:
                case 9:
                case 10:
                case 11:
                case 37:
                case 38:
                case 39:
                case 40:
                case 51:
                case 53:
                case 54:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsDoor(byte type)
        {
            switch (type)
            {
                case 72:
                case 73:
                case 111:
                case 113:
                case 114:
                case 115:
                case 116:
                case 117:
                case 118:
                case 119:
                case 120:
                case 121:
                case 122:
                case 123:
                case 124:
                case 125:
                case 126:
                case 127:
                case 128:
                case 129:
                case 135:
                case 136:
                case 137:
                case 138:
                case 139:
                case 148:
                case 149:
                case 150:
                case 151:
                case 152:
                case 153:
                case 154:
                case 155:
                case 156:
                case 157:
                case 158:
                case 159:
                case 164:
                case 165:
                case 166:
                case 167:
                case 168:
                case 169:
                case 170:
                case 171:
                case 172:
                case 173:
                case 174:
                case 177:
                case 178:
                case 179:
                case 180:
                case 181:
                case 201:
                case 205:
                case 206:
                case 207:
                case 208:
                case 209:
                case 210:
                case 211:
                case 212:
                case 213:
                case 214:
                case 215:
                case 216:
                case 217:
                case 220:
                case 221:
                case 222:
                case 223:
                case 224:
                case 225:
                case 226:
                case 227:
                case 228:
                case 229:
                    return true;
                default:
                    return false;
            }
        }

        public static bool Standable(byte type)
        {
            if (type == byte.MaxValue)
            {
                return true;
            }
            type = Convert(type);
            switch (type)
            {
                case 0:
                case 6:
                case 8:
                case 9:
                case 10:
                case 11:
                case 37:
                case 38:
                case 39:
                case 40:
                case 51:
                case 53:
                case 185:
                    return true;
                default:
                    return false;
            }
        }

        public static bool AnyBuild(byte type)
        {
            switch (type)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                case 36:
                case 37:
                case 38:
                case 39:
                case 40:
                case 41:
                case 42:
                case 43:
                case 44:
                case 45:
                case 46:
                case 47:
                case 48:
                case 49:
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                case 58:
                case 59:
                case 60:
                case 61:
                case 62:
                case 63:
                case 64:
                case 65:
                case 66:
                case 71:
                    return true;
                default:
                    return false;
            }
        }

        public static bool AllowBreak(byte type)
        {
            switch (type)
            {
                case 72:
                case 76:
                case 97:
                case 111:
                case 113:
                case 114:
                case 115:
                case 116:
                case 117:
                case 118:
                case 119:
                case 120:
                case 121:
                case 130:
                case 131:
                case 175:
                case 176:
                case 182:
                case 183:
                case 187:
                case 189:
                case 220:
                case 221:
                case 222:
                case 223:
                case 224:
                case 231:
                case 232:
                case 233:
                    return true;
                default:
                    return false;
            }
        }

        public static bool Placable(byte type)
        {
            switch (type)
            {
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                    return false;
                default:
                    if (type > 49)
                    {
                        return false;
                    }
                    return true;
            }
        }

        public static bool RightClick(byte type, bool countAir = false)
        {
            if (countAir && type == 0)
            {
                return true;
            }
            switch (type)
            {
                case 8:
                case 9:
                case 10:
                case 11:
                    return true;
                default:
                    return false;
            }
        }

        public static bool OPBlocks(byte type)
        {
            switch (type)
            {
                case 7:
                case 100:
                case 101:
                case 102:
                case 103:
                case 104:
                case 105:
                case 106:
                case 187:
                case byte.MaxValue:
                    return true;
                default:
                    return false;
            }
        }

        public static bool Death(byte type)
        {
            switch (type)
            {
                case 74:
                case 80:
                case 81:
                case 82:
                case 83:
                case 98:
                case 184:
                case 185:
                case 188:
                case 190:
                case 191:
                case 192:
                case 193:
                case 194:
                case 195:
                case 196:
                case 230:
                case 231:
                case 232:
                case 242:
                case 247:
                case 250:
                case 251:
                    return true;
                default:
                    return false;
            }
        }

        public static bool BuildIn(byte type)
        {
            if (type == 106 || portal(type) || mb(type))
            {
                return false;
            }
            switch (Convert(type))
            {
                case 8:
                case 9:
                case 10:
                case 11:
                    return true;
                default:
                    return false;
            }
        }

        public static bool Mover(byte type)
        {
            switch (type)
            {
                case 70:
                case 132:
                case 133:
                case 134:
                case 160:
                case 161:
                case 162:
                case 165:
                case 166:
                case 167:
                    return true;
                default:
                    return false;
            }
        }

        public static bool LavaKill(byte type)
        {
            switch (type)
            {
                case 5:
                case 6:
                case 17:
                case 18:
                case 19:
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                case 36:
                case 37:
                case 38:
                case 39:
                case 40:
                case 47:
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                case 58:
                case 59:
                case 60:
                case 61:
                case 62:
                case 63:
                case 64:
                case 65:
                case 66:
                case 67:
                case 68:
                case 69:
                    return true;
                default:
                    return false;
            }
        }

        public static bool WaterKill(byte type)
        {
            switch (type)
            {
                case 0:
                case 6:
                case 18:
                case 37:
                case 38:
                case 39:
                case 40:
                case 53:
                case 54:
                    return true;
                default:
                    return false;
            }
        }

        public static bool LightPass(byte type)
        {
            switch (Convert(type))
            {
                case 0:
                case 6:
                case 18:
                case 20:
                case 37:
                case 38:
                case 39:
                case 40:
                case 51:
                case 60:
                    return true;
                default:
                    return false;
            }
        }

        public static bool NeedRestart(byte type)
        {
            switch (type)
            {
                case 76:
                case 184:
                case 185:
                case 188:
                case 189:
                case 230:
                case 231:
                case 232:
                case 233:
                case 235:
                case 236:
                case 237:
                case 238:
                case 239:
                case 240:
                case 242:
                case 245:
                case 246:
                case 247:
                case 248:
                case 249:
                case 250:
                case 251:
                case 252:
                    return true;
                default:
                    return false;
            }
        }

        public static bool portal(byte type)
        {
            switch (type)
            {
                case 160:
                case 161:
                case 162:
                case 175:
                case 176:
                    return true;
                default:
                    return false;
            }
        }

        public static bool mb(byte type)
        {
            switch (type)
            {
                case 130:
                case 131:
                case 132:
                case 133:
                case 134:
                    return true;
                default:
                    return false;
            }
        }

        public static bool Physics(byte type)
        {
            switch (type)
            {
                case 1:
                case 4:
                case 7:
                case 9:
                case 11:
                case 14:
                case 15:
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                case 36:
                case 41:
                case 42:
                case 43:
                case 45:
                case 46:
                case 48:
                case 49:
                case 70:
                case 72:
                case 100:
                case 101:
                case 102:
                case 103:
                case 104:
                case 105:
                case 106:
                case 111:
                case 113:
                case 114:
                case 115:
                case 116:
                case 117:
                case 118:
                case 119:
                case 120:
                case 121:
                case 122:
                case 123:
                case 124:
                case 125:
                case 126:
                case 127:
                case 128:
                case 129:
                case 130:
                case 131:
                case 132:
                case 133:
                case 134:
                case 135:
                case 136:
                case 137:
                case 138:
                case 139:
                case 160:
                case 161:
                case 162:
                case 164:
                case 165:
                case 166:
                case 167:
                case 175:
                case 176:
                case 190:
                case 191:
                case 192:
                case 220:
                case 221:
                case 222:
                case 223:
                case 224:
                    return false;
                default:
                    return true;
            }
        }

        public static string Name(byte type)
        {
            switch (type)
            {
                case 0:
                    return "air";
                case 1:
                    return "stone";
                case 2:
                    return "grass";
                case 3:
                    return "dirt";
                case 4:
                    return "cobblestone";
                case 5:
                    return "wood";
                case 6:
                    return "plant";
                case 7:
                    return "adminium";
                case 8:
                    return "active_water";
                case 9:
                    return "water";
                case 10:
                    return "active_lava";
                case 11:
                    return "lava";
                case 12:
                    return "sand";
                case 13:
                    return "gravel";
                case 14:
                    return "gold_ore";
                case 15:
                    return "iron_ore";
                case 16:
                    return "coal";
                case 17:
                    return "tree";
                case 18:
                    return "leaves";
                case 19:
                    return "sponge";
                case 20:
                    return "glass";
                case 21:
                    return "red";
                case 22:
                    return "orange";
                case 23:
                    return "yellow";
                case 24:
                    return "greenyellow";
                case 25:
                    return "green";
                case 26:
                    return "springgreen";
                case 27:
                    return "cyan";
                case 28:
                    return "blue";
                case 29:
                    return "blueviolet";
                case 30:
                    return "indigo";
                case 31:
                    return "purple";
                case 32:
                    return "magenta";
                case 33:
                    return "pink";
                case 34:
                    return "black";
                case 35:
                    return "gray";
                case 36:
                    return "white";
                case 37:
                    return "yellow_flower";
                case 38:
                    return "red_flower";
                case 39:
                    return "brown_shroom";
                case 40:
                    return "red_shroom";
                case 41:
                    return "gold";
                case 42:
                    return "iron";
                case 43:
                    return "double_stair";
                case 44:
                    return "stair";
                case 45:
                    return "brick";
                case 46:
                    return "tnt";
                case 47:
                    return "bookcase";
                case 48:
                    return "mossy_cobblestone";
                case 49:
                    return "obsidian";
                case 78:
                    return "psponge";
                case 70:
                    return "flagbase";
                case 100:
                    return "op_glass";
                case 101:
                    return "opsidian";
                case 102:
                    return "op_lava";
                case 103:
                    return "op_stone";
                case 104:
                    return "op_cobblestone";
                case 105:
                    return "op_air";
                case 106:
                    return "op_water";
                case 50:
                    return "cobble_slab";
                case 51:
                    return "rope";
                case 52:
                    return "sandstone";
                case 53:
                    return "snow";
                case 54:
                    return "fire";
                case 55:
                    return "light_pink";
                case 56:
                    return "forest_green";
                case 57:
                    return "brown";
                case 58:
                    return "navy";
                case 59:
                    return "turquoise";
                case 60:
                    return "ice";
                case 61:
                    return "ceramic_tile";
                case 62:
                    return "lava_obsidian";
                case 66:
                    return "blue_gel";
                case 67:
                    return "red_gel";
                case 68:
                    return "portal_blue";
                case 69:
                    return "portal_orange";
                case 63:
                    return "pillar";
                case 64:
                    return "crate";
                case 65:
                    return "stone_brick";
                case 110:
                    return "wood_float";
                case 111:
                    return "door_tree";
                case 112:
                    return "lava_fast";
                case 113:
                    return "door_obsidian";
                case 72:
                    return "door_adminium";
                case 114:
                    return "door_glass";
                case 115:
                    return "door_stone";
                case 116:
                    return "door_leaves";
                case 117:
                    return "door_sand";
                case 118:
                    return "door_wood";
                case 119:
                    return "door_green";
                case 120:
                    return "door_tnt";
                case 121:
                    return "door_stair";
                case 220:
                    return "door_iron";
                case 222:
                    return "door_grass";
                case 221:
                    return "door_dirt";
                case 223:
                    return "door_blue";
                case 224:
                    return "door_book";
                case 122:
                    return "tdoor_tree";
                case 123:
                    return "tdoor_obsidian";
                case 124:
                    return "tdoor_glass";
                case 125:
                    return "tdoor_stone";
                case 126:
                    return "tdoor_leaves";
                case 127:
                    return "tdoor_sand";
                case 128:
                    return "tdoor_wood";
                case 129:
                    return "tdoor_green";
                case 135:
                    return "tdoor_tnt";
                case 136:
                    return "tdoor_stair";
                case 137:
                    return "tdoor_air";
                case 138:
                    return "tdoor_water";
                case 139:
                    return "tdoor_lava";
                case 148:
                    return "odoor_tree";
                case 149:
                    return "odoor_obsidian";
                case 150:
                    return "odoor_glass";
                case 151:
                    return "odoor_stone";
                case 152:
                    return "odoor_leaves";
                case 153:
                    return "odoor_sand";
                case 154:
                    return "odoor_wood";
                case 155:
                    return "odoor_green";
                case 156:
                    return "odoor_tnt";
                case 157:
                    return "odoor_stair";
                case 158:
                    return "odoor_lava";
                case 159:
                    return "odoor_water";
                case 168:
                    return "odoor_tree_air";
                case 169:
                    return "odoor_obsidian_air";
                case 170:
                    return "odoor_glass_air";
                case 171:
                    return "odoor_stone_air";
                case 172:
                    return "odoor_leaves_air";
                case 173:
                    return "odoor_sand_air";
                case 174:
                    return "odoor_wood_air";
                case 177:
                    return "odoor_red";
                case 178:
                    return "odoor_tnt_air";
                case 179:
                    return "odoor_stair_air";
                case 180:
                    return "odoor_lava_air";
                case 181:
                    return "odoor_water_air";
                case 130:
                    return "white_message";
                case 131:
                    return "black_message";
                case 132:
                    return "air_message";
                case 133:
                    return "water_message";
                case 134:
                    return "lava_message";
                case 140:
                    return "waterfall";
                case 141:
                    return "lavafall";
                case 143:
                    return "water_faucet";
                case 144:
                    return "lava_faucet";
                case 145:
                    return "finite_water";
                case 146:
                    return "finite_lava";
                case 147:
                    return "finite_faucet";
                case 160:
                    return "air_portal";
                case 161:
                    return "water_portal";
                case 162:
                    return "lava_portal";
                case 164:
                    return "air_door";
                case 165:
                    return "air_switch";
                case 166:
                    return "door_water";
                case 167:
                    return "door_lava";
                case 175:
                    return "blue_portal";
                case 176:
                    return "orange_portal";
                case 182:
                    return "small_tnt";
                case 183:
                    return "big_tnt";
                case 184:
                    return "tnt_explosion";
                case 185:
                    return "firelava";
                case 187:
                    return "rocketstart";
                case 188:
                    return "rockethead";
                case 189:
                    return "firework";
                case 77:
                    return "smog_bomb";
                case 71:
                    return "still_sand";
                case 190:
                    return "hot_lava";
                case 191:
                    return "cold_water";
                case 98:
                    return "lavaup";
                case 74:
                    return "waterup";
                case 192:
                    return "nerve_gas";
                case 193:
                    return "active_cold_water";
                case 194:
                    return "active_hot_lava";
                case 80:
                    return "lava_type_a";
                case 81:
                    return "lava_type_b";
                case 82:
                    return "lava_type_c";
                case 83:
                    return "lava_type_d";
                case 99:
                    return "meteor";
                case 97:
                    return "treasure";
                case 96:
                    return "dirt_bomb";
                case 75:
                    return "weak_tnt";
                case 195:
                    return "magma";
                case 196:
                    return "geyser";
                case 73:
                    return "door_adminium_air";
                case 200:
                    return "air_flood";
                case 201:
                    return "door_air";
                case 202:
                    return "air_flood_layer";
                case 203:
                    return "air_flood_down";
                case 204:
                    return "air_flood_up";
                case 205:
                    return "door2_air";
                case 206:
                    return "door3_air";
                case 207:
                    return "door4_air";
                case 208:
                    return "door5_air";
                case 209:
                    return "door6_air";
                case 210:
                    return "door7_air";
                case 211:
                    return "door8_air";
                case 212:
                    return "door9_air";
                case 213:
                    return "door10_air";
                case 214:
                    return "door11_air";
                case 215:
                    return "door12_air";
                case 216:
                    return "door13_air";
                case 217:
                    return "door14_air";
                case 225:
                    return "door_iron_air";
                case 226:
                    return "door_dirt_air";
                case 227:
                    return "door_grass_air";
                case 228:
                    return "door_blue_air";
                case 229:
                    return "door_book_air";
                case 230:
                    return "train";
                case 251:
                    return "snake";
                case 252:
                    return "snake_tail";
                case 231:
                    return "creeper";
                case 232:
                    return "zombie";
                case 233:
                    return "zombie_head";
                case 240:
                    return "blue_bird";
                case 239:
                    return "red_robin";
                case 235:
                    return "dove";
                case 236:
                    return "pidgeon";
                case 237:
                    return "duck";
                case 238:
                    return "phoenix";
                case 242:
                    return "killer_phoenix";
                case 249:
                    return "betta_fish";
                case 245:
                    return "goldfish";
                case 248:
                    return "salmon";
                case 247:
                    return "shark";
                case 246:
                    return "sea_sponge";
                case 250:
                    return "lava_shark";
                case 254:
                    return "melting_glass";
                default:
                    return "unknown";
            }
        }

        public static byte Parse(string type)
        {
            return Byte(type);
        }

        public static byte Byte(string type)
        {
            switch (type.ToLower())
            {
                case "air":
                    return 0;
                case "rock":
                case "stone":
                    return 1;
                case "grass":
                    return 2;
                case "ground":
                case "dirt":
                    return 3;
                case "cobble_stone":
                case "cobblestone":
                    return 4;
                case "wood":
                    return 5;
                case "sapling":
                case "plant":
                    return 6;
                case "bedrock":
                case "solid":
                case "admintite":
                case "blackrock":
                case "adminium":
                    return 7;
                case "activewater":
                case "active_water":
                    return 8;
                case "water":
                    return 9;
                case "activelava":
                case "active_lava":
                    return 10;
                case "lava":
                    return 11;
                case "sand":
                    return 12;
                case "gravel":
                    return 13;
                case "gold_ore":
                    return 14;
                case "iron_ore":
                    return 15;
                case "coal":
                    return 16;
                case "trunk":
                case "tree":
                    return 17;
                case "leaves":
                    return 18;
                case "sponge":
                    return 19;
                case "glass":
                    return 20;
                case "red":
                    return 21;
                case "orange":
                    return 22;
                case "yellow":
                    return 23;
                case "greenyellow":
                    return 24;
                case "green":
                    return 25;
                case "springgreen":
                    return 26;
                case "cyan":
                    return 27;
                case "blue":
                    return 28;
                case "blueviolet":
                    return 29;
                case "indigo":
                    return 30;
                case "purple":
                    return 31;
                case "magenta":
                    return 32;
                case "pink":
                    return 33;
                case "black":
                    return 34;
                case "gray":
                    return 35;
                case "white":
                    return 36;
                case "yellow_flower":
                    return 37;
                case "red_flower":
                    return 38;
                case "brown_shroom":
                    return 39;
                case "red_shroom":
                    return 40;
                case "gold":
                    return 41;
                case "iron":
                    return 42;
                case "double_stair":
                    return 43;
                case "stair":
                    return 44;
                case "brick":
                    return 45;
                case "tnt":
                    return 46;
                case "bookcase":
                    return 47;
                case "mossy_cobblestone":
                    return 48;
                case "obsidian":
                    return 49;
                case "cobble_slab":
                    return 50;
                case "rope":
                    return 51;
                case "sandstone":
                    return 52;
                case "snow":
                    return 53;
                case "fire":
                    return 54;
                case "light_pink":
                    return 55;
                case "forest_green":
                    return 56;
                case "brown":
                    return 57;
                case "navy":
                    return 58;
                case "turquoise":
                    return 59;
                case "ice":
                    return 60;
                case "blue_gel":
                    return 66;
                case "red_gel":
                    return 67;
                case "portal_blue":
                    return 68;
                case "portal_orange":
                    return 69;
                case "ceramic_tile":
                    return 61;
                case "lava_obsidian":
                    return 62;
                case "pillar":
                    return 63;
                case "crate":
                    return 64;
                case "stone_brick":
                    return 65;
                case "psponge":
                    return 78;
                case "op_glass":
                    return 100;
                case "opsidian":
                    return 101;
                case "op_lava":
                    return 102;
                case "op_stone":
                    return 103;
                case "op_cobblestone":
                    return 104;
                case "op_air":
                    return 105;
                case "op_water":
                    return 106;
                case "wood_float":
                    return 110;
                case "lava_fast":
                    return 112;
                case "door_tree":
                    return 111;
                case "door":
                    return 220;
                case "door_obsidian":
                case "door2":
                    return 113;
                case "door_adminium":
                case "door_bedrock":
                case "door_solid":
                    return 72;
                case "door_glass":
                case "door3":
                    return 114;
                case "door_stone":
                case "door4":
                    return 115;
                case "door_leaves":
                case "door5":
                    return 116;
                case "door_sand":
                case "door6":
                    return 117;
                case "door_wood":
                case "door7":
                    return 118;
                case "door_green":
                case "door8":
                    return 119;
                case "door_tnt":
                case "door9":
                    return 120;
                case "door_stair":
                case "door10":
                    return 121;
                case "door11":
                case "door_iron":
                    return 220;
                case "door12":
                case "door_dirt":
                    return 221;
                case "door13":
                case "door_grass":
                    return 222;
                case "door14":
                case "door_blue":
                    return 223;
                case "door15":
                case "door_book":
                    return 224;
                case "tdoor_tree":
                case "tdoor":
                    return 122;
                case "tdoor_obsidian":
                case "tdoor2":
                    return 123;
                case "tdoor_glass":
                case "tdoor3":
                    return 124;
                case "tdoor_stone":
                case "tdoor4":
                    return 125;
                case "tdoor_leaves":
                case "tdoor5":
                    return 126;
                case "tdoor_sand":
                case "tdoor6":
                    return 127;
                case "tdoor_wood":
                case "tdoor7":
                    return 128;
                case "tdoor_green":
                case "tdoor8":
                    return 129;
                case "tdoor_tnt":
                case "tdoor9":
                    return 135;
                case "tdoor_stair":
                case "tdoor10":
                    return 136;
                case "tair_switch":
                case "tdoor11":
                    return 137;
                case "tdoor_water":
                case "tdoor12":
                    return 138;
                case "tdoor_lava":
                case "tdoor13":
                    return 139;
                case "odoor_tree":
                case "odoor":
                    return 148;
                case "odoor_obsidian":
                case "odoor2":
                    return 149;
                case "odoor_glass":
                case "odoor3":
                    return 150;
                case "odoor_stone":
                case "odoor4":
                    return 151;
                case "odoor_leaves":
                case "odoor5":
                    return 152;
                case "odoor_sand":
                case "odoor6":
                    return 153;
                case "odoor_wood":
                case "odoor7":
                    return 154;
                case "odoor_green":
                case "odoor8":
                    return 155;
                case "odoor_tnt":
                case "odoor9":
                    return 156;
                case "odoor_stair":
                case "odoor10":
                    return 157;
                case "odoor_lava":
                case "odoor11":
                    return 158;
                case "odoor_water":
                case "odoor12":
                    return 159;
                case "odoor_red":
                    return 177;
                case "white_message":
                    return 130;
                case "black_message":
                    return 131;
                case "air_message":
                    return 132;
                case "water_message":
                    return 133;
                case "lava_message":
                    return 134;
                case "waterfall":
                    return 140;
                case "lavafall":
                    return 141;
                case "water_faucet":
                    return 143;
                case "lava_faucet":
                    return 144;
                case "finite_water":
                    return 145;
                case "finite_lava":
                    return 146;
                case "finite_faucet":
                    return 147;
                case "air_portal":
                    return 160;
                case "water_portal":
                    return 161;
                case "lava_portal":
                    return 162;
                case "air_door":
                    return 164;
                case "air_switch":
                    return 165;
                case "door_water":
                case "water_door":
                    return 166;
                case "door_lava":
                case "lava_door":
                    return 167;
                case "blue_portal":
                    return 175;
                case "orange_portal":
                    return 176;
                case "small_tnt":
                    return 182;
                case "big_tnt":
                    return 183;
                case "tnt_explosion":
                    return 184;
                case "lava_fire":
                    return 185;
                case "rocketstart":
                    return 187;
                case "rockethead":
                    return 188;
                case "firework":
                    return 189;
                case "sb":
                case "smog_bomb":
                    return 77;
                case "hot_lava":
                    return 190;
                case "cold_water":
                    return 191;
                case "nerve_gas":
                    return 192;
                case "acw":
                case "active_cold_water":
                    return 193;
                case "lta":
                case "lava_type_a":
                    return 80;
                case "ltb":
                case "lava_type_b":
                    return 81;
                case "ltc":
                case "lava_type_c":
                    return 82;
                case "ltd":
                case "lava_type_d":
                    return 83;
                case "ahl":
                case "active_hot_lava":
                    return 194;
                case "lavaup":
                    return 98;
                case "waterup":
                    return 74;
                case "meteor":
                    return 99;
                case "ztnt":
                case "weak_tnt":
                    return 75;
                case "db":
                case "dirt_bomb":
                    return 96;
                case "treasure":
                    return 97;
                case "magma":
                    return 195;
                case "geyser":
                    return 196;
                case "air_flood":
                    return 200;
                case "air_flood_layer":
                    return 202;
                case "air_flood_down":
                    return 203;
                case "air_flood_up":
                    return 204;
                case "door_adminium_air":
                    return 73;
                case "door_air":
                    return 201;
                case "door2_air":
                    return 205;
                case "door3_air":
                    return 206;
                case "door4_air":
                    return 207;
                case "door5_air":
                    return 208;
                case "door6_air":
                    return 209;
                case "door7_air":
                    return 210;
                case "door8_air":
                    return 211;
                case "door9_air":
                    return 212;
                case "door10_air":
                    return 213;
                case "door11_air":
                    return 214;
                case "door12_air":
                    return 215;
                case "door13_air":
                    return 216;
                case "door14_air":
                    return 217;
                case "door_iron_air":
                    return 225;
                case "door_dirt_air":
                    return 226;
                case "door_grass_air":
                    return 227;
                case "door_blue_air":
                    return 228;
                case "door_book_air":
                    return 229;
                case "train":
                    return 230;
                case "still_sand":
                    return 71;
                case "snake":
                    return 251;
                case "snake_tail":
                    return 252;
                case "creeper":
                    return 231;
                case "zombie":
                    return 232;
                case "zombie_head":
                    return 233;
                case "blue_bird":
                    return 240;
                case "red_robin":
                    return 239;
                case "dove":
                    return 235;
                case "pidgeon":
                    return 236;
                case "duck":
                    return 237;
                case "phoenix":
                    return 238;
                case "killer_phoenix":
                    return 242;
                case "betta_fish":
                    return 249;
                case "goldfish":
                    return 245;
                case "salmon":
                    return 248;
                case "shark":
                    return 247;
                case "sea_sponge":
                    return 246;
                case "lava_shark":
                    return 250;
                default:
                    return byte.MaxValue;
            }
        }

        static void FillConversion()
        {
            for (int i = 0; i < 70; i++)
            {
                conversion[i] = (byte)i;
            }
            for (int j = 70; j < 256; j++)
            {
                conversion[j] = 22;
            }
            AddConversion(79, 16);
            AddConversion(78, 19);
            AddConversion(254, 20);
            AddConversion(70, 39);
            AddConversion(100, 20);
            AddConversion(101, 49);
            AddConversion(102, 11);
            AddConversion(103, 1);
            AddConversion(104, 4);
            AddConversion(105, 0);
            AddConversion(106, 9);
            AddConversion(110, 5);
            AddConversion(112, 10);
            AddConversion(72, 7);
            AddConversion(111, 17);
            AddConversion(113, 49);
            AddConversion(114, 20);
            AddConversion(115, 1);
            AddConversion(116, 18);
            AddConversion(117, 12);
            AddConversion(118, 5);
            AddConversion(119, 25);
            AddConversion(120, 46);
            AddConversion(121, 44);
            AddConversion(220, 42);
            AddConversion(221, 3);
            AddConversion(222, 2);
            AddConversion(223, 29);
            AddConversion(224, 47);
            AddConversion(122, 17);
            AddConversion(123, 49);
            AddConversion(124, 20);
            AddConversion(125, 1);
            AddConversion(126, 18);
            AddConversion(127, 12);
            AddConversion(128, 5);
            AddConversion(129, 25);
            AddConversion(135, 46);
            AddConversion(136, 44);
            AddConversion(137, 0);
            AddConversion(138, 9);
            AddConversion(139, 11);
            AddConversion(148, 17);
            AddConversion(149, 49);
            AddConversion(150, 20);
            AddConversion(151, 1);
            AddConversion(152, 18);
            AddConversion(153, 12);
            AddConversion(154, 5);
            AddConversion(155, 25);
            AddConversion(156, 46);
            AddConversion(157, 44);
            AddConversion(158, 11);
            AddConversion(159, 9);
            AddConversion(130, 36);
            AddConversion(131, 34);
            AddConversion(132, 0);
            AddConversion(133, 9);
            AddConversion(134, 11);
            AddConversion(140, 8);
            AddConversion(141, 10);
            AddConversion(143, 27);
            AddConversion(144, 22);
            AddConversion(140, 8);
            AddConversion(141, 10);
            AddConversion(143, 27);
            AddConversion(144, 22);
            AddConversion(145, 8);
            AddConversion(146, 10);
            AddConversion(147, 28);
            AddConversion(160, 0);
            AddConversion(161, 9);
            AddConversion(162, 11);
            AddConversion(164, 0);
            AddConversion(165, 0);
            AddConversion(166, 9);
            AddConversion(167, 11);
            AddConversion(175, 28);
            AddConversion(176, 22);
            AddConversion(182, 46);
            AddConversion(183, 46);
            AddConversion(184, 10);
            AddConversion(185, 10);
            AddConversion(187, 20);
            AddConversion(188, 41);
            AddConversion(189, 42);
            AddConversion(191, 9);
            AddConversion(190, 11);
            AddConversion(192, 0);
            AddConversion(193, 8);
            AddConversion(80, 10);
            AddConversion(81, 10);
            AddConversion(82, 10);
            AddConversion(83, 10);
            AddConversion(194, 10);
            AddConversion(99, 7);
            AddConversion(195, 10);
            AddConversion(196, 8);
            AddConversion(97, 14);
            AddConversion(98, 10);
            AddConversion(74, 8);
            AddConversion(96, 46);
            AddConversion(75, 46);
            AddConversion(77, 46);
            AddConversion(76, 36);
            AddConversion(71, 12);
            AddConversion(200, 0);
            AddConversion(201, 0);
            AddConversion(202, 0);
            AddConversion(203, 0);
            AddConversion(204, 0);
            AddConversion(205, 0);
            AddConversion(73, 0);
            AddConversion(206, 0);
            AddConversion(207, 0);
            AddConversion(208, 0);
            AddConversion(209, 0);
            AddConversion(210, 0);
            AddConversion(213, 0);
            AddConversion(214, 0);
            AddConversion(215, 0);
            AddConversion(216, 0);
            AddConversion(217, 0);
            AddConversion(225, 0);
            AddConversion(226, 0);
            AddConversion(227, 0);
            AddConversion(228, 0);
            AddConversion(229, 0);
            AddConversion(212, 10);
            AddConversion(211, 21);
            AddConversion(168, 0);
            AddConversion(169, 0);
            AddConversion(170, 0);
            AddConversion(171, 0);
            AddConversion(172, 0);
            AddConversion(173, 0);
            AddConversion(174, 0);
            AddConversion(179, 0);
            AddConversion(180, 0);
            AddConversion(181, 0);
            AddConversion(177, 21);
            AddConversion(178, 11);
            AddConversion(230, 27);
            AddConversion(251, 34);
            AddConversion(252, 16);
            AddConversion(231, 46);
            AddConversion(232, 48);
            AddConversion(233, 24);
            AddConversion(235, 36);
            AddConversion(236, 34);
            AddConversion(238, 10);
            AddConversion(239, 21);
            AddConversion(237, 8);
            AddConversion(240, 29);
            AddConversion(242, 10);
            AddConversion(249, 29);
            AddConversion(245, 41);
            AddConversion(248, 21);
            AddConversion(247, 35);
            AddConversion(246, 19);
            AddConversion(250, 49);
        }

        static void AddConversion(byte blockA, byte blockB)
        {
            conversion[blockA] = blockB;
        }

        public static byte Convert(byte b)
        {
            return conversion[b];
        }

        public static byte SaveConvert(byte b)
        {
            switch (b)
            {
                case 200:
                case 202:
                case 203:
                case 204:
                    return 0;
                case 201:
                    return 111;
                case 205:
                    return 113;
                case 206:
                    return 114;
                case 207:
                    return 115;
                case 208:
                    return 116;
                case 209:
                    return 117;
                case 210:
                    return 118;
                case 211:
                    return 119;
                case 212:
                    return 120;
                case 213:
                    return 121;
                case 214:
                    return 165;
                case 215:
                    return 166;
                case 216:
                    return 167;
                case 217:
                    return 164;
                case 73:
                    return 72;
                case 225:
                    return 220;
                case 226:
                    return 221;
                case 227:
                    return 222;
                case 228:
                    return 223;
                case 229:
                    return 224;
                case 168:
                case 169:
                case 170:
                case 171:
                case 172:
                case 173:
                case 174:
                case 177:
                case 178:
                case 179:
                case 180:
                case 181:
                    return odoor(b);
                default:
                    return b;
            }
        }

        public static byte DoorAirs(byte b)
        {
            switch (b)
            {
                case 72:
                    return 73;
                case 111:
                    return 201;
                case 113:
                    return 205;
                case 114:
                    return 206;
                case 115:
                    return 207;
                case 116:
                    return 208;
                case 117:
                    return 209;
                case 118:
                    return 210;
                case 119:
                    return 211;
                case 120:
                    return 212;
                case 121:
                    return 213;
                case 165:
                    return 214;
                case 166:
                    return 215;
                case 167:
                    return 216;
                case 164:
                    return 217;
                case 220:
                    return 225;
                case 221:
                    return 226;
                case 222:
                    return 227;
                case 223:
                    return 228;
                case 224:
                    return 229;
                default:
                    return 0;
            }
        }

        public static bool tDoor(byte b)
        {
            switch (b)
            {
                case 122:
                case 123:
                case 124:
                case 125:
                case 126:
                case 127:
                case 128:
                case 129:
                case 135:
                case 136:
                case 137:
                case 138:
                case 139:
                    return true;
                default:
                    return false;
            }
        }

        public static byte odoor(byte b)
        {
            switch (b)
            {
                case 148:
                    return 168;
                case 149:
                    return 169;
                case 150:
                    return 170;
                case 151:
                    return 171;
                case 152:
                    return 172;
                case 153:
                    return 173;
                case 154:
                    return 174;
                case 155:
                    return 177;
                case 156:
                    return 178;
                case 157:
                    return 179;
                case 158:
                    return 180;
                case 159:
                    return 181;
                case 168:
                    return 148;
                case 169:
                    return 149;
                case 170:
                    return 150;
                case 171:
                    return 151;
                case 172:
                    return 152;
                case 173:
                    return 153;
                case 174:
                    return 154;
                case 177:
                    return 155;
                case 178:
                    return 156;
                case 179:
                    return 157;
                case 180:
                    return 158;
                case 181:
                    return 159;
                default:
                    return byte.MaxValue;
            }
        }

        internal static bool IsWater(byte block)
        {
            if (block != 8)
            {
                return block == 9;
            }
            return true;
        }

        public class Blocks
        {

            public List<LevelPermission> allow = new List<LevelPermission>();

            public List<LevelPermission> disallow = new List<LevelPermission>();

            public LevelPermission lowestRank;
            public byte type;
        }
    }
}