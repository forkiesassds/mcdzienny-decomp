using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using MCDzienny.Gui;

namespace MCDzienny
{
    class CmdTest : Command
    {

        public static object lockObject = new object();

        static readonly string[] colors = new string[4]
        {
            "&2", "&5", "&7", "&a"
        };

        readonly List<BlockPoints> blockPoints = new List<BlockPoints>();

        readonly List<Player> players = new List<Player>();

        int count;
        ASCIIEncoding enc = new ASCIIEncoding();

        byte lastID;

        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

        public override string name { get { return "test"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "information"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Nobody; } }

        public string GetRandomString(Random rand, string pattern)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < 25; i++)
            {
                stringBuilder.Append(colors[rand.Next(0, colors.Length)]);
                stringBuilder.Append(pattern[rand.Next(0, pattern.Length)]);
            }
            return stringBuilder.ToString();
        }

        public void NarrowThroat(string message)
        {
            Window.thisWindow.UpdateChat(message);
        }

        public override void Use(Player p, string message)
        {
            byte value = 33;
            count += 2;
            count %= 12;
            message = "[{\"health\":{\"value\":" + count + ",\"display\":true,\"max\":10}},\r\n{\"experimental\":{\"flags\":\"portal-blocks-enable\"}}]";
            short num = (short)message.Length;
            ByteStream byteStream = new ByteStream(num + 3);
            byteStream.WriteByte(value);
            byteStream.WriteInt16(num);
            byteStream.WriteString(message);
            p.SendRaw(byteStream.ToArray());
        }

        void Player_Joined(object sender, PlayerEventArgs e)
        {
            throw new NotImplementedException();
        }

        byte GetFreeId()
        {
            int i;
            for (i = lastID + 1; i < 128; i++)
            {
                bool flag = false;
                if (!players.Exists(p => p.id == i ? true : false))
                {
                    lastID = (byte)i;
                    return (byte)i;
                }
            }
            int j;
            for (j = 0; j <= lastID; j++)
            {
                bool flag2 = false;
                if (!players.Exists(p => p.id == j ? true : false))
                {
                    lastID = (byte)j;
                    return (byte)j;
                }
            }
            return 1;
        }

        void p_Blockchange(Player p, ushort x, ushort y, ushort z, byte type)
        {
            blockPoints.Add(new BlockPoints(new Vector3(x, y, z), type));
            p.Blockchange -= p_Blockchange;
            p.Blockchange += p_Blockchange2;
        }

        void p_Blockchange2(Player p, ushort x, ushort y, ushort z, byte type)
        {
            blockPoints.Add(new BlockPoints(new Vector3(x, y, z), type));
            p.Blockchange -= p_Blockchange2;
            Draw(p);
        }

        public void Draw(Player p)
        {
            new BoundingBox(blockPoints[0].position, blockPoints[1].position).BoxOutline().ForEach(delegate(Vector3 v3)
            {
                p.BlockChanges.Add((ushort)v3.X, (ushort)v3.Y, (ushort)v3.Z, 14);
            });
            p.BlockChanges.Commit();
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/test - for debugging. Do not use!");
        }
    }
}