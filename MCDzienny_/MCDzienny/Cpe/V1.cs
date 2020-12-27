using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace MCDzienny.Cpe
{
    // Token: 0x02000026 RID: 38
    public static class V1
    {
        // Token: 0x04000095 RID: 149
        public static readonly byte Chat = 0;

        // Token: 0x04000096 RID: 150
        public static readonly byte Status1 = 1;

        // Token: 0x04000097 RID: 151
        public static readonly byte Status2 = 2;

        // Token: 0x04000098 RID: 152
        public static readonly byte Status3 = 3;

        // Token: 0x04000099 RID: 153
        public static readonly byte BottomRight1 = 11;

        // Token: 0x0400009A RID: 154
        public static readonly byte BottomRight2 = 12;

        // Token: 0x0400009B RID: 155
        public static readonly byte BottomRight3 = 13;

        // Token: 0x0400009C RID: 156
        public static readonly byte TopLeft = 21;

        // Token: 0x0400009D RID: 157
        public static readonly byte Announcement = 100;

        // Token: 0x060000D8 RID: 216 RVA: 0x00005F28 File Offset: 0x00004128
        public static void EnvSetColor(Player player, byte target, short red, short green, short blue)
        {
            if (player == null) throw new ArgumentNullException("player");
            var list = new List<byte>(8);
            list.Add(25);
            list.Add(target);
            list.AddRange(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(red)));
            list.AddRange(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(green)));
            list.AddRange(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(blue)));
            player.SendRaw(list.ToArray());
        }

        // Token: 0x060000D9 RID: 217 RVA: 0x00005F9C File Offset: 0x0000419C
        public static void SetClickDistance(Player player, short distance)
        {
            if (player == null) throw new ArgumentNullException("player");
            using (var memoryStream = new MemoryStream(3))
            {
                memoryStream.WriteByte(18);
                var bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(distance));
                memoryStream.Write(bytes, 0, 2);
                player.SendRaw(memoryStream.ToArray());
            }
        }

        // Token: 0x060000DA RID: 218 RVA: 0x00006004 File Offset: 0x00004204
        public static void HoldThis(Player player, byte block, byte preventChanges)
        {
            if (player == null) throw new ArgumentNullException("player");
            player.SendRaw(new byte[]
            {
                20,
                block,
                preventChanges
            });
        }

        // Token: 0x060000DB RID: 219 RVA: 0x0000603C File Offset: 0x0000423C
        public static void SetTextHotKey(Player player, string label, string action, int keyCode, byte keyMods)
        {
            if (player == null) throw new ArgumentNullException("player");
            if (label == null) throw new ArgumentNullException("label");
            if (label.Length > 64) throw new ArgumentException("String length can't be greater than 64.", "label");
            if (action == null) throw new ArgumentNullException("action");
            if (action.Length > 64) throw new ArgumentException("String length can't be greater than 64.", "action");
            using (var memoryStream = new MemoryStream(134))
            {
                memoryStream.WriteByte(21);
                var bytes = Encoding.ASCII.GetBytes(label.PadRight(64));
                memoryStream.Write(bytes, 0, 64);
                var bytes2 = Encoding.ASCII.GetBytes(action.PadRight(64));
                memoryStream.Write(bytes2, 0, 64);
                var bytes3 = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(keyCode));
                memoryStream.Write(bytes3, 0, 4);
                memoryStream.WriteByte(keyMods);
                player.SendRaw(memoryStream.ToArray());
            }
        }

        // Token: 0x060000DC RID: 220 RVA: 0x0000613C File Offset: 0x0000433C
        public static void ExtAddPlayerName(Player player, short itemId, string playerName, string listName,
            string groupName, byte groupRank)
        {
            if (player == null) throw new ArgumentNullException("player");
            if (playerName == null) throw new ArgumentNullException("playerName");
            if (playerName.Length > 64)
                throw new ArgumentException("String length can't be greater than 64.", "playerName");
            if (listName == null) throw new ArgumentNullException("listName");
            if (listName.Length > 64)
                throw new ArgumentException("String length can't be greater than 64.", "listName");
            if (groupName == null) throw new ArgumentNullException("groupName");
            if (groupName.Length > 64)
                throw new ArgumentException("String length can't be greater than 64.", "groupName");
            using (var byteStream = new ByteStream(196))
            {
                byteStream.WriteByte(22);
                byteStream.WriteInt16(itemId);
                byteStream.WriteString(playerName.PadRight(64));
                byteStream.WriteString(listName.PadRight(64));
                byteStream.WriteString(groupName.PadRight(64));
                byteStream.WriteByte(groupRank);
                player.SendRaw(byteStream.ToArray());
            }
        }

        // Token: 0x060000DD RID: 221 RVA: 0x00006248 File Offset: 0x00004448
        public static void ExtAddEntity(Player player, byte entityId, string inGameName, string skinName)
        {
            if (player == null) throw new ArgumentNullException("player");
            if (inGameName == null) throw new ArgumentNullException("inGameName");
            if (inGameName.Length > 64)
                throw new ArgumentException("String length can't be greater than 64.", "inGameName");
            if (skinName == null) throw new ArgumentNullException("skinName");
            if (skinName.Length > 64)
                throw new ArgumentException("String length can't be greater than 64.", "skinName");
            using (var byteStream = new ByteStream(130))
            {
                byteStream.WriteByte(23);
                byteStream.WriteByte(entityId);
                byteStream.WriteString(inGameName.PadRight(64));
                byteStream.WriteString(skinName.PadRight(64));
                player.SendRaw(byteStream.ToArray());
            }
        }

        // Token: 0x060000DE RID: 222 RVA: 0x00006314 File Offset: 0x00004514
        public static void ExtRemovePlayerName(Player player, short itemId)
        {
            if (player == null) throw new ArgumentNullException("player");
            using (var byteStream = new ByteStream(3))
            {
                byteStream.WriteByte(24);
                byteStream.WriteInt16(itemId);
                player.SendRaw(byteStream.ToArray());
            }
        }

        // Token: 0x060000DF RID: 223 RVA: 0x00006370 File Offset: 0x00004570
        public static void SetBlockPermission(Player player, byte blockType, byte allowPlacement, byte allowDeletion)
        {
            if (player == null) throw new ArgumentNullException("player");
            player.SendRaw(new byte[]
            {
                28,
                blockType,
                allowPlacement,
                allowDeletion
            });
        }

        // Token: 0x060000E0 RID: 224 RVA: 0x000063AC File Offset: 0x000045AC
        public static void JsonData(Player player, string jsonData)
        {
            if (player == null) throw new ArgumentNullException("player");
            if (jsonData.Length > 32767)
                throw new ArgumentException("jsonData can't be longer than " + short.MaxValue + ".");
            using (var byteStream = new ByteStream(jsonData.Length + 3))
            {
                byteStream.WriteByte(33);
                byteStream.WriteInt16((short) jsonData.Length);
                byteStream.WriteString(jsonData);
                player.SendRaw(byteStream.ToArray());
            }
        }

        // Token: 0x060000E1 RID: 225 RVA: 0x00006448 File Offset: 0x00004648
        public static void MakeSelection(Player player, byte selectionId, string label, short x1, short y1, short z1,
            short x2, short y2, short z2, short red, short green, short blue, short opacity)
        {
            if (player == null) throw new ArgumentNullException("player");
            if (label == null) throw new ArgumentNullException("label");
            if (label.Length > 64) throw new ArgumentException("String length can't be greater than 64.", "label");
            using (var byteStream = new ByteStream(86))
            {
                byteStream.WriteByte(26);
                byteStream.WriteByte(selectionId);
                byteStream.WriteString(label.PadRight(64));
                byteStream.WriteInt16(x1);
                byteStream.WriteInt16(y1);
                byteStream.WriteInt16(z1);
                byteStream.WriteInt16(x2);
                byteStream.WriteInt16(y2);
                byteStream.WriteInt16(z2);
                byteStream.WriteInt16(red);
                byteStream.WriteInt16(green);
                byteStream.WriteInt16(blue);
                byteStream.WriteInt16(opacity);
                player.SendRaw(byteStream.ToArray());
            }
        }

        // Token: 0x060000E2 RID: 226 RVA: 0x00006528 File Offset: 0x00004728
        public static void RemoveSelection(Player player, byte selectionId)
        {
            if (player == null) throw new ArgumentNullException("player");
            player.SendRaw(new byte[]
            {
                27,
                selectionId
            });
        }

        // Token: 0x060000E3 RID: 227 RVA: 0x0000655C File Offset: 0x0000475C
        public static void ChangeModel(Player player, byte entityId, string modelName)
        {
            if (player == null) throw new ArgumentNullException("player");
            if (modelName == null) throw new ArgumentNullException("modelName");
            if (modelName.Length > 64)
                throw new ArgumentException("String length can't be greater than 64.", "modelName");
            using (var byteStream = new ByteStream(66))
            {
                byteStream.WriteByte(29);
                byteStream.WriteByte(entityId);
                byteStream.WriteString(modelName.PadRight(64));
                player.SendRaw(byteStream.ToArray());
            }
        }

        // Token: 0x060000E4 RID: 228 RVA: 0x000065EC File Offset: 0x000047EC
        public static void EnvSetMapAppearance(Player player, string textureUrl, byte sideBlock, byte edgeBlock,
            short sideLevel)
        {
            if (player == null) throw new ArgumentNullException("player");
            if (textureUrl == null) throw new ArgumentNullException("textureUrl");
            if (textureUrl.Length > 64)
                throw new ArgumentException("String length can't be greater than 64.", "textureUrl");
            using (var byteStream = new ByteStream(69))
            {
                byteStream.WriteByte(30);
                byteStream.WriteString(textureUrl.PadRight(64));
                byteStream.WriteByte(sideBlock);
                byteStream.WriteByte(edgeBlock);
                byteStream.WriteInt16(sideLevel);
                player.SendRaw(byteStream.ToArray());
            }
        }

        // Token: 0x060000E5 RID: 229 RVA: 0x0000668C File Offset: 0x0000488C
        public static void EnvSetWeatherType(Player player, byte weatherType)
        {
            if (player == null) throw new ArgumentNullException("player");
            player.SendRaw(new byte[]
            {
                31,
                weatherType
            });
        }

        // Token: 0x060000E6 RID: 230 RVA: 0x000066C0 File Offset: 0x000048C0
        public static void HackControl(Player player, byte flying, byte noClip, byte speeding, byte spawnControl,
            byte thirdPersonView, byte jumpHeight)
        {
            if (player == null) throw new ArgumentNullException("player");
            using (var byteStream = new ByteStream(8))
            {
                byteStream.WriteByte(32);
                byteStream.WriteByte(flying);
                byteStream.WriteByte(noClip);
                byteStream.WriteByte(speeding);
                byteStream.WriteByte(spawnControl);
                byteStream.WriteByte(thirdPersonView);
                byteStream.WriteInt16(jumpHeight);
                player.SendRaw(byteStream.ToArray());
            }
        }

        // Token: 0x060000E7 RID: 231 RVA: 0x00006740 File Offset: 0x00004940
        public static void Message(Player player, byte type, string message)
        {
            if (type != Chat && message.Length > 61) message = message.Substring(0, 61);
            Player.SendMessage(player, type, message);
        }
    }
}