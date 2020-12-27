using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using MCDzienny.Gui;

namespace MCDzienny
{
    // Token: 0x02000138 RID: 312
    internal class CmdTest : Command
    {
        // Token: 0x040003E9 RID: 1001
        public static object lockObject = new object();

        // Token: 0x040003EC RID: 1004
        private static readonly string[] colors =
        {
            "&2",
            "&5",
            "&7",
            "&a"
        };

        // Token: 0x040003EE RID: 1006
        private readonly List<BlockPoints> blockPoints = new List<BlockPoints>();

        // Token: 0x040003ED RID: 1005
        private int count;

        // Token: 0x040003E7 RID: 999
        private ASCIIEncoding enc = new ASCIIEncoding();

        // Token: 0x040003EB RID: 1003
        private byte lastID;

        // Token: 0x040003E8 RID: 1000
        private MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

        // Token: 0x040003EA RID: 1002
        private readonly List<Player> players = new List<Player>();

        // Token: 0x17000460 RID: 1120
        // (get) Token: 0x06000959 RID: 2393 RVA: 0x0002E308 File Offset: 0x0002C508
        public override string name
        {
            get { return "test"; }
        }

        // Token: 0x17000461 RID: 1121
        // (get) Token: 0x0600095A RID: 2394 RVA: 0x0002E310 File Offset: 0x0002C510
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000462 RID: 1122
        // (get) Token: 0x0600095B RID: 2395 RVA: 0x0002E318 File Offset: 0x0002C518
        public override string type
        {
            get { return "information"; }
        }

        // Token: 0x17000463 RID: 1123
        // (get) Token: 0x0600095C RID: 2396 RVA: 0x0002E320 File Offset: 0x0002C520
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000464 RID: 1124
        // (get) Token: 0x0600095D RID: 2397 RVA: 0x0002E324 File Offset: 0x0002C524
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Nobody; }
        }

        // Token: 0x0600095F RID: 2399 RVA: 0x0002E35C File Offset: 0x0002C55C
        public string GetRandomString(Random rand, string pattern)
        {
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < 25; i++)
            {
                stringBuilder.Append(colors[rand.Next(0, colors.Length)]);
                stringBuilder.Append(pattern[rand.Next(0, pattern.Length)]);
            }

            return stringBuilder.ToString();
        }

        // Token: 0x06000960 RID: 2400 RVA: 0x0002E3B8 File Offset: 0x0002C5B8
        public void NarrowThroat(string message)
        {
            Window.thisWindow.UpdateChat(message);
        }

        // Token: 0x06000961 RID: 2401 RVA: 0x0002E3C8 File Offset: 0x0002C5C8
        public override void Use(Player p, string message)
        {
            byte value = 33;
            count += 2;
            count %= 12;
            message = "[{\"health\":{\"value\":" + count +
                      ",\"display\":true,\"max\":10}},\r\n{\"experimental\":{\"flags\":\"portal-blocks-enable\"}}]";
            var num = (short) message.Length;
            var byteStream = new ByteStream(num + 3);
            byteStream.WriteByte(value);
            byteStream.WriteInt16(num);
            byteStream.WriteString(message);
            p.SendRaw(byteStream.ToArray());
        }

        // Token: 0x06000962 RID: 2402 RVA: 0x0002E444 File Offset: 0x0002C644
        private void Player_Joined(object sender, PlayerEventArgs e)
        {
            throw new NotImplementedException();
        }

        // Token: 0x06000963 RID: 2403 RVA: 0x0002E44C File Offset: 0x0002C64C
        private byte GetFreeId()
        {
            int j;
            for (j = lastID + 1; j < 128; j++)
                if (!players.Exists(p => (int) p.id == j))
                {
                    lastID = (byte) j;
                    return (byte) j;
                }

            int i;
            for (i = 0; i <= (int) lastID; i++)
                if (!players.Exists(p => (int) p.id == i))
                {
                    lastID = (byte) i;
                    return (byte) i;
                }

            return 1;
        }

        // Token: 0x06000964 RID: 2404 RVA: 0x0002E534 File Offset: 0x0002C734
        private void p_Blockchange(Player p, ushort x, ushort y, ushort z, byte type)
        {
            blockPoints.Add(new BlockPoints(new Vector3(x, y, z), type));
            p.Blockchange -= p_Blockchange;
            p.Blockchange += p_Blockchange2;
        }

        // Token: 0x06000965 RID: 2405 RVA: 0x0002E584 File Offset: 0x0002C784
        private void p_Blockchange2(Player p, ushort x, ushort y, ushort z, byte type)
        {
            blockPoints.Add(new BlockPoints(new Vector3(x, y, z), type));
            p.Blockchange -= p_Blockchange2;
            Draw(p);
        }

        // Token: 0x06000966 RID: 2406 RVA: 0x0002E5C0 File Offset: 0x0002C7C0
        public void Draw(Player p)
        {
            var boundingBox = new BoundingBox(blockPoints[0].position, blockPoints[1].position);
            boundingBox.BoxOutline().ForEach(delegate(Vector3 v3)
            {
                p.BlockChanges.Add((ushort) v3.X, (ushort) v3.Y, (ushort) v3.Z, 14);
            });
            p.BlockChanges.Commit();
        }

        // Token: 0x06000967 RID: 2407 RVA: 0x0002E62C File Offset: 0x0002C82C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/test - for debugging. Do not use!");
        }
    }
}