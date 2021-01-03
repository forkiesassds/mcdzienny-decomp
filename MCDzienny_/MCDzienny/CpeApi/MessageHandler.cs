using System;
using System.Collections.Generic;
using System.Threading;

namespace MCDzienny.CpeApi
{
    // Token: 0x02000019 RID: 25
    internal class MessageHandler
    {
        // Token: 0x04000051 RID: 81
        private readonly object displayedMessagesLocker = new object();

        // Token: 0x06000093 RID: 147 RVA: 0x00005144 File Offset: 0x00003344
        public MessageHandler(Player player)
        {
            Player = player;
            DisplayedMessages = new Dictionary<V1.MessageType, MessagesQueue>();
        }

        // Token: 0x1700002A RID: 42
        // (get) Token: 0x0600008F RID: 143 RVA: 0x0000511C File Offset: 0x0000331C
        // (set) Token: 0x06000090 RID: 144 RVA: 0x00005124 File Offset: 0x00003324
        private Player Player { get; set; }

        // Token: 0x1700002B RID: 43
        // (get) Token: 0x06000091 RID: 145 RVA: 0x00005130 File Offset: 0x00003330
        // (set) Token: 0x06000092 RID: 146 RVA: 0x00005138 File Offset: 0x00003338
        private Dictionary<V1.MessageType, MessagesQueue> DisplayedMessages { get; set; }

        // Token: 0x06000094 RID: 148 RVA: 0x0000516C File Offset: 0x0000336C
        internal bool SendMessage(V1.MessageType type, V1.MessageOptions options, string message)
        {
            MessagesQueue messagesQueue = null;
            lock (displayedMessagesLocker)
            {
                if (DisplayedMessages.ContainsKey(type))
                {
                    messagesQueue = DisplayedMessages[type];
                }
                else
                {
                    messagesQueue = new MessagesQueue(Player, type);
                    DisplayedMessages[type] = messagesQueue;
                }
            }

            var copy = options.GetCopy();
            return messagesQueue.TryAddMessage(new MessageItem
            {
                Message = message,
                Options = copy
            });
        }

        // Token: 0x0200001A RID: 26
        private class CancellationToken
        {
            // Token: 0x1700002C RID: 44
            // (get) Token: 0x06000095 RID: 149 RVA: 0x00005200 File Offset: 0x00003400
            // (set) Token: 0x06000096 RID: 150 RVA: 0x00005208 File Offset: 0x00003408
            public bool IsCancelled { get; set; }
        }

        // Token: 0x0200001B RID: 27
        private class MessagesQueue
        {
            // Token: 0x04000056 RID: 86
            private readonly object blinkerSync = new object();

            // Token: 0x04000055 RID: 85
            private readonly object itemsLocker = new object();

            // Token: 0x060000A6 RID: 166 RVA: 0x000052A8 File Offset: 0x000034A8
            public MessagesQueue(Player player, V1.MessageType type)
            {
                Player = player;
                Type = (byte) type;
                Items = new List<QueueItem>();
            }

            // Token: 0x1700002D RID: 45
            // (get) Token: 0x06000098 RID: 152 RVA: 0x0000521C File Offset: 0x0000341C
            // (set) Token: 0x06000099 RID: 153 RVA: 0x00005224 File Offset: 0x00003424
            private Player Player { get; set; }

            // Token: 0x1700002E RID: 46
            // (get) Token: 0x0600009A RID: 154 RVA: 0x00005230 File Offset: 0x00003430
            // (set) Token: 0x0600009B RID: 155 RVA: 0x00005238 File Offset: 0x00003438
            private byte Type { get; set; }

            // Token: 0x1700002F RID: 47
            // (get) Token: 0x0600009C RID: 156 RVA: 0x00005244 File Offset: 0x00003444
            // (set) Token: 0x0600009D RID: 157 RVA: 0x0000524C File Offset: 0x0000344C
            private DateTime LastProcessed { get; set; }

            // Token: 0x17000030 RID: 48
            // (get) Token: 0x0600009E RID: 158 RVA: 0x00005258 File Offset: 0x00003458
            // (set) Token: 0x0600009F RID: 159 RVA: 0x00005260 File Offset: 0x00003460
            private CancellationToken LastCancellationToken { get; set; }

            // Token: 0x17000031 RID: 49
            // (get) Token: 0x060000A0 RID: 160 RVA: 0x0000526C File Offset: 0x0000346C
            // (set) Token: 0x060000A1 RID: 161 RVA: 0x00005274 File Offset: 0x00003474
            private CancellationToken LastBlinkerCancellationToken { get; set; }

            // Token: 0x17000032 RID: 50
            // (get) Token: 0x060000A2 RID: 162 RVA: 0x00005280 File Offset: 0x00003480
            // (set) Token: 0x060000A3 RID: 163 RVA: 0x00005288 File Offset: 0x00003488
            private bool IsKept { get; set; }

            // Token: 0x17000033 RID: 51
            // (get) Token: 0x060000A4 RID: 164 RVA: 0x00005294 File Offset: 0x00003494
            // (set) Token: 0x060000A5 RID: 165 RVA: 0x0000529C File Offset: 0x0000349C
            private List<QueueItem> Items { get; set; }

            // Token: 0x060000A7 RID: 167 RVA: 0x000052E0 File Offset: 0x000034E0
            public void Next()
            {
                lock (itemsLocker)
                {
                    if (Items.Count == 0)
                    {
                        if (LastBlinkerCancellationToken != null)
                            lock (blinkerSync)
                            {
                                LastBlinkerCancellationToken.IsCancelled = true;
                            }

                        if (!IsKept) Player.SendMessage(Player, Type, "");
                    }
                    else
                    {
                        var current = Items[0];
                        if (current.Message != null)
                        {
                            if (LastBlinkerCancellationToken != null)
                                lock (blinkerSync)
                                {
                                    LastBlinkerCancellationToken.IsCancelled = true;
                                }

                            IsKept = current.Message.Options.DisplayTime <= TimeSpan.Zero;
                            Player.SendMessage(Player, Type, current.Message.Message);
                        }

                        LastProcessed = DateTime.Now;
                        if (current.Message != null && current.Message.Options.IsBlinking)
                        {
                            var blinkerCancellationToken = new CancellationToken();
                            LastBlinkerCancellationToken = blinkerCancellationToken;
                            var switcher = true;
                            Timer blinker = null;
                            blinker = new Timer(delegate
                            {
                                lock (blinkerSync)
                                {
                                    if (blinkerCancellationToken.IsCancelled)
                                    {
                                        blinker.Dispose();
                                    }
                                    else
                                    {
                                        if (switcher)
                                        {
                                            if (current.Message.Options.AltMessage != null)
                                                Player.SendMessage(Player, Type, current.Message.Options.AltMessage);
                                            else
                                                Player.SendMessage(Player, Type, "");
                                        }
                                        else
                                        {
                                            Player.SendMessage(Player, Type, current.Message.Message);
                                        }

                                        switcher = !switcher;
                                    }
                                }
                            }, null, current.Message.Options.BlinkPeriod, current.Message.Options.BlinkPeriod);
                        }

                        var cancellationToken = new CancellationToken();
                        LastCancellationToken = cancellationToken;
                        Timer timer = null;
                        timer = new Timer(delegate
                        {
                            timer.Dispose();
                            if (cancellationToken.IsCancelled) return;
                            lock (itemsLocker)
                            {
                                Items.RemoveAt(0);
                                Next();
                            }
                        }, null, current.TimeSpan, TimeSpan.Zero);
                    }
                }
            }

            // Token: 0x060000A8 RID: 168 RVA: 0x00005534 File Offset: 0x00003734
            public bool TryAddMessage(MessageItem message)
            {
                var options = message.Options;
                bool result;
                lock (itemsLocker)
                {
                    var timeSpan = DateTime.Now.Subtract(LastProcessed);
                    timeSpan = timeSpan < TimeSpan.Zero ? TimeSpan.Zero : timeSpan;
                    if (Items.Count == 0)
                    {
                        AddItem(message);
                        Next();
                        result = true;
                    }
                    else
                    {
                        var priority = (QueuePriority) options.Priority;
                        if (Items.Count == 1)
                        {
                            var queueItem = Items[0];
                            if (queueItem.Message == null && queueItem.QueuePriority <= priority)
                            {
                                LastCancellationToken.IsCancelled = true;
                                Items.RemoveAt(0);
                                AddItem(message);
                                Next();
                                return true;
                            }
                        }

                        var t = timeSpan.Negate();
                        for (var i = 0; i < Items.Count - 1; i++)
                        {
                            if (Items[i].QueuePriority <= priority)
                            {
                                var timeSpan2 = Items[i].TimeSpan;
                                if (i == 0) timeSpan2 = timeSpan2.Subtract(timeSpan);
                                if (timeSpan2 >= options.MinDisplayTime)
                                {
                                    var availableTime = timeSpan2;
                                    InsertItem(i, message, availableTime);
                                    return true;
                                }
                            }

                            t = t.Add(Items[i].TimeSpan);
                            if (t > options.MaxDelay) return false;
                        }

                        var num = Items.Count - 1;
                        if (Items[num].QueuePriority <= priority)
                        {
                            InsertItem(num, message);
                            result = true;
                        }
                        else
                        {
                            t = t.Add(Items[num].TimeSpan);
                            if (t > options.MaxDelay)
                            {
                                result = false;
                            }
                            else
                            {
                                AddItem(message);
                                result = true;
                            }
                        }
                    }
                }

                return result;
            }

            // Token: 0x060000A9 RID: 169 RVA: 0x00005750 File Offset: 0x00003950
            private void InsertItem(int i, MessageItem message)
            {
                InsertItem(i, message, TimeSpan.MaxValue);
            }

            // Token: 0x060000AA RID: 170 RVA: 0x00005760 File Offset: 0x00003960
            private void InsertItem(int i, MessageItem message, TimeSpan availableTime)
            {
                Items.RemoveAt(i);
                var queueItem = new QueueItem
                {
                    Message = message,
                    QueuePriority = QueuePriority.Fixed,
                    TimeSpan = message.Options.MinDisplayTime
                };
                Items.Insert(i, queueItem);
                availableTime = availableTime.Subtract(queueItem.TimeSpan);
                var timeSpan = message.Options.DisplayTime.Subtract(message.Options.MinDisplayTime);
                if (timeSpan <= TimeSpan.Zero)
                {
                    if (i == 0)
                    {
                        LastCancellationToken.IsCancelled = true;
                        Next();
                    }

                    return;
                }

                timeSpan = availableTime > timeSpan ? timeSpan : availableTime;
                if (timeSpan > TimeSpan.Zero)
                    Items.Insert(i + 1, new QueueItem
                    {
                        Message = null,
                        QueuePriority = (QueuePriority) message.Options.Priority,
                        TimeSpan = timeSpan
                    });
                if (i == 0)
                {
                    LastCancellationToken.IsCancelled = true;
                    Next();
                }
            }

            // Token: 0x060000AB RID: 171 RVA: 0x00005864 File Offset: 0x00003A64
            private void AddItem(MessageItem message)
            {
                Items.Add(new QueueItem
                {
                    Message = message,
                    TimeSpan = message.Options.MinDisplayTime,
                    QueuePriority = QueuePriority.Fixed
                });
                var timeSpan = message.Options.DisplayTime.Subtract(message.Options.MinDisplayTime);
                if (timeSpan <= TimeSpan.Zero) return;
                Items.Add(new QueueItem
                {
                    Message = null,
                    TimeSpan = timeSpan,
                    QueuePriority = (QueuePriority) message.Options.Priority
                });
            }
        }

        // Token: 0x0200001E RID: 30
        private enum QueuePriority
        {
            // Token: 0x04000067 RID: 103
            Low = -1,

            // Token: 0x04000068 RID: 104
            Normal,

            // Token: 0x04000069 RID: 105
            High,

            // Token: 0x0400006A RID: 106
            Highest,

            // Token: 0x0400006B RID: 107
            Fixed
        }

        // Token: 0x0200001F RID: 31
        private class QueueItem
        {
            // Token: 0x17000034 RID: 52
            // (get) Token: 0x060000B0 RID: 176 RVA: 0x00005AB8 File Offset: 0x00003CB8
            // (set) Token: 0x060000B1 RID: 177 RVA: 0x00005AC0 File Offset: 0x00003CC0
            public MessageItem Message { get; set; }

            // Token: 0x17000035 RID: 53
            // (get) Token: 0x060000B2 RID: 178 RVA: 0x00005ACC File Offset: 0x00003CCC
            // (set) Token: 0x060000B3 RID: 179 RVA: 0x00005AD4 File Offset: 0x00003CD4
            public TimeSpan TimeSpan { get; set; }

            // Token: 0x17000036 RID: 54
            // (get) Token: 0x060000B4 RID: 180 RVA: 0x00005AE0 File Offset: 0x00003CE0
            // (set) Token: 0x060000B5 RID: 181 RVA: 0x00005AE8 File Offset: 0x00003CE8
            public QueuePriority QueuePriority { get; set; }
        }

        // Token: 0x02000020 RID: 32
        private class MessageItem
        {
            // Token: 0x060000BE RID: 190 RVA: 0x00005B50 File Offset: 0x00003D50
            public MessageItem()
            {
                Created = DateTime.Now;
            }

            // Token: 0x17000037 RID: 55
            // (get) Token: 0x060000B7 RID: 183 RVA: 0x00005AFC File Offset: 0x00003CFC
            // (set) Token: 0x060000B8 RID: 184 RVA: 0x00005B04 File Offset: 0x00003D04
            public string Message { get; set; }

            // Token: 0x17000038 RID: 56
            // (get) Token: 0x060000B9 RID: 185 RVA: 0x00005B10 File Offset: 0x00003D10
            // (set) Token: 0x060000BA RID: 186 RVA: 0x00005B18 File Offset: 0x00003D18
            public V1.MessageOptions Options { get; set; }

            // Token: 0x17000039 RID: 57
            // (get) Token: 0x060000BB RID: 187 RVA: 0x00005B24 File Offset: 0x00003D24
            public V1.MessagePriority Priority
            {
                get
                {
                    if (Options == null) return V1.MessagePriority.Normal;
                    return Options.Priority;
                }
            }

            // Token: 0x1700003A RID: 58
            // (get) Token: 0x060000BC RID: 188 RVA: 0x00005B3C File Offset: 0x00003D3C
            // (set) Token: 0x060000BD RID: 189 RVA: 0x00005B44 File Offset: 0x00003D44
            public DateTime Created { get; private set; }
        }
    }
}