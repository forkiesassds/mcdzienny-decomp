using System;

namespace MCDzienny.CpeApi
{
    // Token: 0x02000021 RID: 33
    public class V1
    {
        // Token: 0x02000022 RID: 34
        public enum MessagePriority
        {
            // Token: 0x04000074 RID: 116
            Low = -1,

            // Token: 0x04000075 RID: 117
            Normal,

            // Token: 0x04000076 RID: 118
            High,

            // Token: 0x04000077 RID: 119
            Highest
        }

        // Token: 0x02000023 RID: 35
        public enum MessageType
        {
            // Token: 0x04000079 RID: 121
            Chat,

            // Token: 0x0400007A RID: 122
            Status1,

            // Token: 0x0400007B RID: 123
            Status2,

            // Token: 0x0400007C RID: 124
            Status3,

            // Token: 0x0400007D RID: 125
            BottomRight1 = 11,

            // Token: 0x0400007E RID: 126
            BottomRight2,

            // Token: 0x0400007F RID: 127
            BottomRight3,

            // Token: 0x04000080 RID: 128
            TopLeft = 21,

            // Token: 0x04000081 RID: 129
            Announcement = 100
        }

        // Token: 0x04000072 RID: 114
        public static readonly string MessageHandler = "MessageHandler";

        // Token: 0x060000BF RID: 191 RVA: 0x00005B64 File Offset: 0x00003D64
        public static bool SendMessageTopLeft(Player player, MessageOptions options, string message)
        {
            return SendMessage(player, MessageType.TopLeft, options, message);
        }

        // Token: 0x060000C0 RID: 192 RVA: 0x00005B70 File Offset: 0x00003D70
        public static bool SendMessageCenter(Player player, MessageOptions options, string message)
        {
            return SendMessage(player, MessageType.Announcement, options, message);
        }

        // Token: 0x060000C1 RID: 193 RVA: 0x00005B7C File Offset: 0x00003D7C
        public static bool SendMessageBottomRight(Player player, int line, MessageOptions options, string message)
        {
            if (line < 1 || line > 3)
                throw new ArgumentOutOfRangeException("line", "Value has to be within 1..3 range.");
            var type = MessageType.Chat;
            switch (line)
            {
                case 1:
                    type = MessageType.BottomRight1;
                    break;
                case 2:
                    type = MessageType.BottomRight2;
                    break;
                case 3:
                    type = MessageType.BottomRight3;
                    break;
            }

            return SendMessage(player, type, options, message);
        }

        // Token: 0x060000C2 RID: 194 RVA: 0x00005BD4 File Offset: 0x00003DD4
        public static bool SendMessageTopRight(Player player, int line, MessageOptions options, string message)
        {
            if (line < 1 || line > 3)
                throw new ArgumentOutOfRangeException("line", "Value has to be within 1..3 range.");
            var type = MessageType.Chat;
            switch (line)
            {
                case 1:
                    type = MessageType.Status1;
                    break;
                case 2:
                    type = MessageType.Status2;
                    break;
                case 3:
                    type = MessageType.Status3;
                    break;
            }

            return SendMessage(player, type, options, message);
        }

        // Token: 0x060000C3 RID: 195 RVA: 0x00005C28 File Offset: 0x00003E28
        public static bool SendMessage(Player player, MessageType type, MessageOptions options, string message)
        {
            if (!Enum.IsDefined(typeof(MessageType), type))
                throw new ArgumentException("type", "Represents undefined MessageType.");
            if (type == MessageType.Chat)
                throw new ArgumentException("type", "Argument must not be equal to MessageType.Chat.");
            if (message == null) throw new ArgumentNullException("message");
            MessageHandler messageHandler;
            if (player.ExtraData.ContainsKey(MessageHandler))
            {
                messageHandler = (MessageHandler) player.ExtraData[MessageHandler];
            }
            else
            {
                messageHandler = new MessageHandler(player);
                player.ExtraData[MessageHandler] = messageHandler;
            }

            if (options == null) options = MessageOptions.Default;
            if (options.MaxDelay <= TimeSpan.Zero) options.MaxDelay = TimeSpan.FromMilliseconds(300.0);
            if (options.MinDisplayTime > TimeSpan.FromSeconds(30.0))
            {
                Server.s.Log("Warning: MinDisplayTime can't be higher than 30 seconds.");
                options.MinDisplayTime = TimeSpan.FromSeconds(30.0);
            }

            if (options.DisplayTime > TimeSpan.Zero && options.DisplayTime < options.MinDisplayTime)
                options.DisplayTime = options.MinDisplayTime;
            if (options.BlinkPeriod < TimeSpan.FromMilliseconds(500.0))
                options.BlinkPeriod = TimeSpan.FromMilliseconds(500.0);
            if (message.Length > 61) message = message.Substring(0, 61);
            return messageHandler.SendMessage(type, options, message);
        }

        // Token: 0x02000024 RID: 36
        public class MessageOptions
        {
            // Token: 0x1700003B RID: 59
            // (get) Token: 0x060000C6 RID: 198 RVA: 0x00005DC4 File Offset: 0x00003FC4
            // (set) Token: 0x060000C7 RID: 199 RVA: 0x00005DCC File Offset: 0x00003FCC
            public TimeSpan MaxDelay { get; set; }

            // Token: 0x1700003C RID: 60
            // (get) Token: 0x060000C8 RID: 200 RVA: 0x00005DD8 File Offset: 0x00003FD8
            // (set) Token: 0x060000C9 RID: 201 RVA: 0x00005DE0 File Offset: 0x00003FE0
            public TimeSpan DisplayTime { get; set; }

            // Token: 0x1700003D RID: 61
            // (get) Token: 0x060000CA RID: 202 RVA: 0x00005DEC File Offset: 0x00003FEC
            // (set) Token: 0x060000CB RID: 203 RVA: 0x00005DF4 File Offset: 0x00003FF4
            public TimeSpan MinDisplayTime { get; set; }

            // Token: 0x1700003E RID: 62
            // (get) Token: 0x060000CC RID: 204 RVA: 0x00005E00 File Offset: 0x00004000
            // (set) Token: 0x060000CD RID: 205 RVA: 0x00005E08 File Offset: 0x00004008
            public MessagePriority Priority { get; set; }

            // Token: 0x1700003F RID: 63
            // (get) Token: 0x060000CE RID: 206 RVA: 0x00005E14 File Offset: 0x00004014
            // (set) Token: 0x060000CF RID: 207 RVA: 0x00005E1C File Offset: 0x0000401C
            public bool IsBlinking { get; set; }

            // Token: 0x17000040 RID: 64
            // (get) Token: 0x060000D0 RID: 208 RVA: 0x00005E28 File Offset: 0x00004028
            // (set) Token: 0x060000D1 RID: 209 RVA: 0x00005E30 File Offset: 0x00004030
            public string AltMessage { get; set; }

            // Token: 0x17000041 RID: 65
            // (get) Token: 0x060000D2 RID: 210 RVA: 0x00005E3C File Offset: 0x0000403C
            // (set) Token: 0x060000D3 RID: 211 RVA: 0x00005E44 File Offset: 0x00004044
            public TimeSpan BlinkPeriod { get; set; }

            // Token: 0x17000042 RID: 66
            // (get) Token: 0x060000D5 RID: 213 RVA: 0x00005EB8 File Offset: 0x000040B8
            public static MessageOptions Default
            {
                get
                {
                    return new MessageOptions
                    {
                        MaxDelay = TimeSpan.Zero,
                        DisplayTime = TimeSpan.Zero,
                        MinDisplayTime = TimeSpan.FromSeconds(1.0),
                        Priority = MessagePriority.Normal,
                        IsBlinking = false,
                        AltMessage = null,
                        BlinkPeriod = TimeSpan.Zero
                    };
                }
            }

            // Token: 0x060000D4 RID: 212 RVA: 0x00005E50 File Offset: 0x00004050
            public MessageOptions GetCopy()
            {
                return new MessageOptions
                {
                    MaxDelay = MaxDelay,
                    DisplayTime = DisplayTime,
                    MinDisplayTime = MinDisplayTime,
                    Priority = Priority,
                    IsBlinking = IsBlinking,
                    AltMessage = AltMessage,
                    BlinkPeriod = BlinkPeriod
                };
            }
        }
    }
}