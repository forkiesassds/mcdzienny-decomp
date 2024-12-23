using System;
using System.Collections.Generic;
using System.Threading;

namespace MCDzienny.CpeApi
{
    class MessageHandler
    {

        readonly object displayedMessagesLocker = new object();

        public MessageHandler(Player player)
        {
            Player = player;
            DisplayedMessages = new Dictionary<V1.MessageType, MessagesQueue>();
        }

        Player Player { get; set; }

        Dictionary<V1.MessageType, MessagesQueue> DisplayedMessages { get; set; }

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
            V1.MessageOptions copy = options.GetCopy();
            return messagesQueue.TryAddMessage(new MessageItem
            {
                Message = message,
                Options = copy
            });
        }

        class CancellationToken
        {
            public bool IsCancelled { get; set; }
        }

        class MessagesQueue
        {

            readonly object blinkerSync = new object();
            readonly object itemsLocker = new object();

            public MessagesQueue(Player player, V1.MessageType type)
            {
                Player = player;
                Type = (byte)type;
                Items = new List<QueueItem>();
            }

            Player Player { get; set; }

            byte Type { get; set; }

            DateTime LastProcessed { get; set; }

            CancellationToken LastCancellationToken { get; set; }

            CancellationToken LastBlinkerCancellationToken { get; set; }

            bool IsKept { get; set; }

            List<QueueItem> Items { get; set; }

            public void Next()
            {
                lock (itemsLocker)
                {
                    if (Items.Count == 0)
                    {
                        if (LastBlinkerCancellationToken != null)
                        {
                            lock (blinkerSync)
                            {
                                LastBlinkerCancellationToken.IsCancelled = true;
                            }
                        }
                        if (!IsKept)
                        {
                            Player.SendMessage(Player, Type, "");
                        }
                        return;
                    }
                    QueueItem current = Items[0];
                    if (current.Message != null)
                    {
                        if (LastBlinkerCancellationToken != null)
                        {
                            lock (blinkerSync)
                            {
                                LastBlinkerCancellationToken.IsCancelled = true;
                            }
                        }
                        IsKept = current.Message.Options.DisplayTime <= TimeSpan.Zero;
                        Player.SendMessage(Player, Type, current.Message.Message);
                    }
                    LastProcessed = DateTime.Now;
                    if (current.Message != null && current.Message.Options.IsBlinking)
                    {
                        CancellationToken blinkerCancellationToken = new CancellationToken();
                        LastBlinkerCancellationToken = blinkerCancellationToken;
                        bool switcher = true;
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
                                        {
                                            Player.SendMessage(Player, Type, current.Message.Options.AltMessage);
                                        }
                                        else
                                        {
                                            Player.SendMessage(Player, Type, "");
                                        }
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
                    CancellationToken cancellationToken = new CancellationToken();
                    LastCancellationToken = cancellationToken;
                    Timer timer = null;
                    timer = new Timer(delegate
                    {
                        timer.Dispose();
                        if (cancellationToken.IsCancelled)
                        {
                            return;
                        }
                        lock (itemsLocker)
                        {
                            Items.RemoveAt(0);
                            Next();
                        }
                    }, null, current.TimeSpan, TimeSpan.Zero);
                }
            }

            public bool TryAddMessage(MessageItem message)
            {
                V1.MessageOptions options = message.Options;
                lock (itemsLocker)
                {
                    TimeSpan timeSpan = DateTime.Now.Subtract(LastProcessed);
                    timeSpan = timeSpan < TimeSpan.Zero ? TimeSpan.Zero : timeSpan;
                    if (Items.Count == 0)
                    {
                        AddItem(message);
                        Next();
                        return true;
                    }
                    QueuePriority priority = (QueuePriority)options.Priority;
                    if (Items.Count == 1)
                    {
                        QueueItem queueItem = Items[0];
                        if (queueItem.Message == null && queueItem.QueuePriority <= priority)
                        {
                            LastCancellationToken.IsCancelled = true;
                            Items.RemoveAt(0);
                            AddItem(message);
                            Next();
                            return true;
                        }
                    }
                    TimeSpan timeSpan2 = timeSpan.Negate();
                    for (int i = 0; i < Items.Count - 1; i++)
                    {
                        if (Items[i].QueuePriority <= priority)
                        {
                            TimeSpan timeSpan3 = Items[i].TimeSpan;
                            if (i == 0)
                            {
                                timeSpan3 = timeSpan3.Subtract(timeSpan);
                            }
                            if (timeSpan3 >= options.MinDisplayTime)
                            {
                                TimeSpan availableTime = timeSpan3;
                                InsertItem(i, message, availableTime);
                                return true;
                            }
                        }
                        timeSpan2 = timeSpan2.Add(Items[i].TimeSpan);
                        if (timeSpan2 > options.MaxDelay)
                        {
                            return false;
                        }
                    }
                    int num = Items.Count - 1;
                    if (Items[num].QueuePriority <= priority)
                    {
                        InsertItem(num, message);
                        return true;
                    }
                    timeSpan2 = timeSpan2.Add(Items[num].TimeSpan);
                    if (timeSpan2 > options.MaxDelay)
                    {
                        return false;
                    }
                    AddItem(message);
                    return true;
                }
            }

            void InsertItem(int i, MessageItem message)
            {
                InsertItem(i, message, TimeSpan.MaxValue);
            }

            void InsertItem(int i, MessageItem message, TimeSpan availableTime)
            {
                Items.RemoveAt(i);
                QueueItem queueItem = new QueueItem();
                queueItem.Message = message;
                queueItem.QueuePriority = QueuePriority.Fixed;
                queueItem.TimeSpan = message.Options.MinDisplayTime;
                QueueItem queueItem2 = queueItem;
                Items.Insert(i, queueItem2);
                availableTime = availableTime.Subtract(queueItem2.TimeSpan);
                TimeSpan timeSpan = message.Options.DisplayTime.Subtract(message.Options.MinDisplayTime);
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
                {
                    Items.Insert(i + 1, new QueueItem
                    {
                        Message = null,
                        QueuePriority = (QueuePriority)message.Options.Priority,
                        TimeSpan = timeSpan
                    });
                }
                if (i == 0)
                {
                    LastCancellationToken.IsCancelled = true;
                    Next();
                }
            }

            void AddItem(MessageItem message)
            {
                Items.Add(new QueueItem
                {
                    Message = message,
                    TimeSpan = message.Options.MinDisplayTime,
                    QueuePriority = QueuePriority.Fixed
                });
                TimeSpan timeSpan = message.Options.DisplayTime.Subtract(message.Options.MinDisplayTime);
                if (!(timeSpan <= TimeSpan.Zero))
                {
                    Items.Add(new QueueItem
                    {
                        Message = null,
                        TimeSpan = timeSpan,
                        QueuePriority = (QueuePriority)message.Options.Priority
                    });
                }
            }
        }

        enum QueuePriority
        {
            Low = -1,
            Normal,
            High,
            Highest,
            Fixed
        }

        class QueueItem
        {
            public MessageItem Message { get; set; }

            public TimeSpan TimeSpan { get; set; }

            public QueuePriority QueuePriority { get; set; }
        }

        class MessageItem
        {

            public MessageItem()
            {
                Created = DateTime.Now;
            }
            public string Message { get; set; }

            public V1.MessageOptions Options { get; set; }

            public V1.MessagePriority Priority
            {
                get
                {
                    if (Options == null)
                    {
                        return V1.MessagePriority.Normal;
                    }
                    return Options.Priority;
                }
            }

            public DateTime Created { get; private set; }
        }
    }
}