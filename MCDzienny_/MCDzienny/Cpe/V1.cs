using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace MCDzienny.Cpe
{
    public static class V1
    {
        public static readonly byte Chat = 0;

        public static readonly byte Status1 = 1;

        public static readonly byte Status2 = 2;

        public static readonly byte Status3 = 3;

        public static readonly byte BottomRight1 = 11;

        public static readonly byte BottomRight2 = 12;

        public static readonly byte BottomRight3 = 13;

        public static readonly byte TopLeft = 21;

        public static readonly byte Announcement = 100;

        public static void EnvSetColor(Player player, byte target, short red, short green, short blue)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }
            var list = new List<byte>(8);
            list.Add(25);
            list.Add(target);
            list.AddRange(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(red)));
            list.AddRange(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(green)));
            list.AddRange(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(blue)));
            player.SendRaw(list.ToArray());
        }

        public static void SetClickDistance(Player player, short distance)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }
            using (MemoryStream memoryStream = new MemoryStream(3))
            {
                memoryStream.WriteByte(18);
                byte[] bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(distance));
                memoryStream.Write(bytes, 0, 2);
                player.SendRaw(memoryStream.ToArray());
            }
        }

        public static void HoldThis(Player player, byte block, byte preventChanges)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }
            player.SendRaw(new byte[3]
            {
                20, block, preventChanges
            });
        }

        public static void SetTextHotKey(Player player, string label, string action, int keyCode, byte keyMods)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }
            if (label == null)
            {
                throw new ArgumentNullException("label");
            }
            if (label.Length > 64)
            {
                throw new ArgumentException("String length can't be greater than 64.", "label");
            }
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            if (action.Length > 64)
            {
                throw new ArgumentException("String length can't be greater than 64.", "action");
            }
            using (MemoryStream memoryStream = new MemoryStream(134))
            {
                memoryStream.WriteByte(21);
                byte[] bytes = Encoding.ASCII.GetBytes(label.PadRight(64));
                memoryStream.Write(bytes, 0, 64);
                byte[] bytes2 = Encoding.ASCII.GetBytes(action.PadRight(64));
                memoryStream.Write(bytes2, 0, 64);
                byte[] bytes3 = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(keyCode));
                memoryStream.Write(bytes3, 0, 4);
                memoryStream.WriteByte(keyMods);
                player.SendRaw(memoryStream.ToArray());
            }
        }

        public static void ExtAddPlayerName(Player player, short itemId, string playerName, string listName, string groupName, byte groupRank)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }
            if (playerName == null)
            {
                throw new ArgumentNullException("playerName");
            }
            if (playerName.Length > 64)
            {
                throw new ArgumentException("String length can't be greater than 64.", "playerName");
            }
            if (listName == null)
            {
                throw new ArgumentNullException("listName");
            }
            if (listName.Length > 64)
            {
                throw new ArgumentException("String length can't be greater than 64.", "listName");
            }
            if (groupName == null)
            {
                throw new ArgumentNullException("groupName");
            }
            if (groupName.Length > 64)
            {
                throw new ArgumentException("String length can't be greater than 64.", "groupName");
            }
            using (ByteStream byteStream = new ByteStream(196))
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

        public static void ExtAddEntity(Player player, byte entityId, string inGameName, string skinName)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }
            if (inGameName == null)
            {
                throw new ArgumentNullException("inGameName");
            }
            if (inGameName.Length > 64)
            {
                throw new ArgumentException("String length can't be greater than 64.", "inGameName");
            }
            if (skinName == null)
            {
                throw new ArgumentNullException("skinName");
            }
            if (skinName.Length > 64)
            {
                throw new ArgumentException("String length can't be greater than 64.", "skinName");
            }
            using (ByteStream byteStream = new ByteStream(130))
            {
                byteStream.WriteByte(23);
                byteStream.WriteByte(entityId);
                byteStream.WriteString(inGameName.PadRight(64));
                byteStream.WriteString(skinName.PadRight(64));
                player.SendRaw(byteStream.ToArray());
            }
        }

        public static void ExtRemovePlayerName(Player player, short itemId)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }
            using (ByteStream byteStream = new ByteStream(3))
            {
                byteStream.WriteByte(24);
                byteStream.WriteInt16(itemId);
                player.SendRaw(byteStream.ToArray());
            }
        }

        public static void SetBlockPermission(Player player, byte blockType, byte allowPlacement, byte allowDeletion)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }
            player.SendRaw(new byte[4]
            {
                28, blockType, allowPlacement, allowDeletion
            });
        }

        public static void JsonData(Player player, string jsonData)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }
            if (jsonData.Length > 32767)
            {
                throw new ArgumentException("jsonData can't be longer than " + short.MaxValue + ".");
            }
            using (ByteStream byteStream = new ByteStream(jsonData.Length + 3))
            {
                byteStream.WriteByte(33);
                byteStream.WriteInt16((short)jsonData.Length);
                byteStream.WriteString(jsonData);
                player.SendRaw(byteStream.ToArray());
            }
        }

        public static void MakeSelection(Player player, byte selectionId, string label, short x1, short y1, short z1, short x2, short y2, short z2, short red, short green,
                                         short blue, short opacity)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }
            if (label == null)
            {
                throw new ArgumentNullException("label");
            }
            if (label.Length > 64)
            {
                throw new ArgumentException("String length can't be greater than 64.", "label");
            }
            using (ByteStream byteStream = new ByteStream(86))
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

        public static void RemoveSelection(Player player, byte selectionId)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }
            player.SendRaw(new byte[2]
            {
                27, selectionId
            });
        }

        public static void ChangeModel(Player player, byte entityId, string modelName)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }
            if (modelName == null)
            {
                throw new ArgumentNullException("modelName");
            }
            if (modelName.Length > 64)
            {
                throw new ArgumentException("String length can't be greater than 64.", "modelName");
            }
            using (ByteStream byteStream = new ByteStream(66))
            {
                byteStream.WriteByte(29);
                byteStream.WriteByte(entityId);
                byteStream.WriteString(modelName.PadRight(64));
                player.SendRaw(byteStream.ToArray());
            }
        }

        public static void EnvSetMapAppearance(Player player, string textureUrl, byte sideBlock, byte edgeBlock, short sideLevel)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }
            if (textureUrl == null)
            {
                throw new ArgumentNullException("textureUrl");
            }
            if (textureUrl.Length > 64)
            {
                throw new ArgumentException("String length can't be greater than 64.", "textureUrl");
            }
            using (ByteStream byteStream = new ByteStream(69))
            {
                byteStream.WriteByte(30);
                byteStream.WriteString(textureUrl.PadRight(64));
                byteStream.WriteByte(sideBlock);
                byteStream.WriteByte(edgeBlock);
                byteStream.WriteInt16(sideLevel);
                player.SendRaw(byteStream.ToArray());
            }
        }

        public static void EnvSetWeatherType(Player player, byte weatherType)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }
            player.SendRaw(new byte[2]
            {
                31, weatherType
            });
        }

        public static void HackControl(Player player, byte flying, byte noClip, byte speeding, byte spawnControl, byte thirdPersonView, byte jumpHeight)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }
            using (ByteStream byteStream = new ByteStream(8))
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

        public static void Message(Player player, byte type, string message)
        {
            if (type != Chat && message.Length > 61)
            {
                message = message.Substring(0, 61);
            }
            Player.SendMessage(player, type, message);
        }
    }
}