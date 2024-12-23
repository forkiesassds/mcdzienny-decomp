using System;

namespace MCDzienny.CpeApi
{
    public class V1
    {
        public enum MessagePriority
        {
            Low = -1,
            Normal,
            High,
            Highest
        }

        public enum MessageType
        {
            Chat = 0,
            Status1 = 1,
            Status2 = 2,
            Status3 = 3,
            BottomRight1 = 11,
            BottomRight2 = 12,
            BottomRight3 = 13,
            TopLeft = 21,
            Announcement = 100
        }

        public static readonly string MessageHandler = "MessageHandler";

        public static bool SendMessageTopLeft(Player player, MessageOptions options, string message)
        {
            return SendMessage(player, MessageType.TopLeft, options, message);
        }

        public static bool SendMessageCenter(Player player, MessageOptions options, string message)
        {
            return SendMessage(player, MessageType.Announcement, options, message);
        }

        public static bool SendMessageBottomRight(Player player, int line, MessageOptions options, string message)
        {
            if (line < 1 || line > 3)
            {
                throw new ArgumentOutOfRangeException("line", "Value has to be within 1..3 range.");
            }
            MessageType type = MessageType.Chat;
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

        public static bool SendMessageTopRight(Player player, int line, MessageOptions options, string message)
        {
            if (line < 1 || line > 3)
            {
                throw new ArgumentOutOfRangeException("line", "Value has to be within 1..3 range.");
            }
            MessageType type = MessageType.Chat;
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

        public static bool SendMessage(Player player, MessageType type, MessageOptions options, string message)
        {
            if (!Enum.IsDefined(typeof(MessageType), type))
            {
                throw new ArgumentException("type", "Represents undefined MessageType.");
            }
            if (type == MessageType.Chat)
            {
                throw new ArgumentException("type", "Argument must not be equal to MessageType.Chat.");
            }
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }
            MessageHandler messageHandler = null;
            if (player.ExtraData.ContainsKey(MessageHandler))
            {
                messageHandler = (MessageHandler)player.ExtraData[MessageHandler];
            }
            else
            {
                messageHandler = new MessageHandler(player);
                player.ExtraData[MessageHandler] = messageHandler;
            }
            if (options == null)
            {
                options = MessageOptions.Default;
            }
            if (options.MaxDelay <= TimeSpan.Zero)
            {
                options.MaxDelay = TimeSpan.FromMilliseconds(300.0);
            }
            if (options.MinDisplayTime > TimeSpan.FromSeconds(30.0))
            {
                Server.s.Log("Warning: MinDisplayTime can't be higher than 30 seconds.");
                options.MinDisplayTime = TimeSpan.FromSeconds(30.0);
            }
            if (options.DisplayTime > TimeSpan.Zero && options.DisplayTime < options.MinDisplayTime)
            {
                options.DisplayTime = options.MinDisplayTime;
            }
            if (options.BlinkPeriod < TimeSpan.FromMilliseconds(500.0))
            {
                options.BlinkPeriod = TimeSpan.FromMilliseconds(500.0);
            }
            if (message.Length > 61)
            {
                message = message.Substring(0, 61);
            }
            return messageHandler.SendMessage(type, options, message);
        }

        public class MessageOptions
        {
            public TimeSpan MaxDelay { get; set; }

            public TimeSpan DisplayTime { get; set; }

            public TimeSpan MinDisplayTime { get; set; }

            public MessagePriority Priority { get; set; }

            public bool IsBlinking { get; set; }

            public string AltMessage { get; set; }

            public TimeSpan BlinkPeriod { get; set; }

            public static MessageOptions Default
            {
                get
                {
                    MessageOptions messageOptions = new MessageOptions();
                    messageOptions.MaxDelay = TimeSpan.Zero;
                    messageOptions.DisplayTime = TimeSpan.Zero;
                    messageOptions.MinDisplayTime = TimeSpan.FromSeconds(1.0);
                    messageOptions.Priority = MessagePriority.Normal;
                    messageOptions.IsBlinking = false;
                    messageOptions.AltMessage = null;
                    messageOptions.BlinkPeriod = TimeSpan.Zero;
                    return messageOptions;
                }
            }

            public MessageOptions GetCopy()
            {
                MessageOptions messageOptions = new MessageOptions();
                messageOptions.MaxDelay = MaxDelay;
                messageOptions.DisplayTime = DisplayTime;
                messageOptions.MinDisplayTime = MinDisplayTime;
                messageOptions.Priority = Priority;
                messageOptions.IsBlinking = IsBlinking;
                messageOptions.AltMessage = AltMessage;
                messageOptions.BlinkPeriod = BlinkPeriod;
                return messageOptions;
            }
        }
    }
}