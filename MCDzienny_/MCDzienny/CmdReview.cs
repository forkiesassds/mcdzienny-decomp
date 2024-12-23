using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using MCDzienny.Misc;
using MCDzienny.Settings;

namespace MCDzienny
{
    public class CmdReview : Command
    {
        static Timer checkReviewList;

        static readonly ReviewQueue reviewQueue = new ReviewQueue();

        static readonly List<string> lastReviewed = new List<string>();

        public CmdReview()
        {
            if (checkReviewList == null)
            {
                checkReviewList = new Timer(new TimeSpan(0, 6, 0).TotalMilliseconds);
                checkReviewList.Elapsed += checkReviewList_Elapsed;
                checkReviewList.Start();
            }
        }

        public override string name { get { return "review"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override CommandScope Scope { get { return CommandScope.Freebuild | CommandScope.Lava | CommandScope.Home; } }

        void checkReviewList_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (reviewQueue.RemoveDisconnectedPlayers())
                {
                    ReportNewQueuePosition();
                }
                if (reviewQueue.QueueLength > 0)
                {
                    Player.GlobalMessageOps(string.Concat(MCColor.DarkTeal, "# There are players waiting for a review."));
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        public override void Use(Player p, string message)
        {
            Message message2 = new Message(message);
            switch (message2.ReadStringLower())
            {
                case "ask":
                case "join":
                case "request":
                    JoinReviewQueue(p);
                    break;
                case "next":
                case "do":
                    ReviewNext(p);
                    break;
                case "leave":
                case "exit":
                    ReviewLeave(p, message);
                    break;
                case "list":
                    ReviewList(p, message);
                    break;
                case "clear":
                    ReviewClear(p, message);
                    break;
                case "last":
                case "checked":
                    ShowLastReviewed(p);
                    break;
                default:
                    Help(p);
                    break;
            }
        }

        void ShowLastReviewed(Player p)
        {
            Player.SendMessage(p, "------- Last Reviewed Players -------");
            Player.SendMessage(p, string.Join(", ", lastReviewed.ToArray()));
        }

        public void JoinReviewQueue(Player p)
        {
            if (reviewQueue.RemoveDisconnectedPlayers())
            {
                ReportNewQueuePosition();
            }
            if (reviewQueue.Contains(p))
            {
                Player.SendMessage(p, string.Format("You are {0}. in the review queue.", reviewQueue.QuequePosition(p) + 1));
                return;
            }
            reviewQueue.Enqueue(p);
            Player.SendMessage(p, "------------ Review ------------");
            Player.SendMessage(p, "You were added to the review queue.");
            Player.SendMessage(p, string.Format("You are currently %c{0}.%s one the review queue.", reviewQueue.QuequePosition(p) + 1));
            Player.SendMessage(p, "Please wait for some Operator to review your work.");
            Player.GlobalMessage(p.color + p.PublicName + " %sjoined the review queue.");
            Player.GlobalMessageOps("# Player " + p.color + p.PublicName + "%s requested a review.");
        }

        public void ReviewNext(Player p)
        {
            if (reviewQueue.QueueLength > 0)
            {
                Player player = null;
                do
                {
                    player = reviewQueue.Peek();
                    if (player == null)
                    {
                        reviewQueue.Dequeue();
                        ReportNewQueuePosition();
                    }
                } while (player == null && reviewQueue.QueueLength > 0);
                if (player == null)
                {
                    Player.SendMessage(p, "There isn't anyone on the review list.");
                    return;
                }
                if ((int)p.group.Permission < GeneralSettings.All.MinPermissionForReview)
                {
                    Player.SendMessage(p, "Your rank isn't high enough to let you review other people's work.");
                    return;
                }
                reviewQueue.Dequeue();
                ReportNewQueuePosition();
                Player.SendMessage(p, "-------------- Review --------------");
                Player.SendMessage(p, string.Format("You are reviewing: {0}", player.color + player.PublicName));
                Player.SendMessage(p, string.Format("Map: {0}", player.level.name));
                Player.SendMessage(p, "* You will be teleported to them shortly.");
                Player.SendMessage(player, "========================================================");
                Player.SendMessage(player, string.Format(string.Concat(MCColor.Gold, "You are about to get a review from {0}"), p.color + p.PublicName));
                Player.SendMessage(player, "========================================================");
                all.Find("tp").Use(p, player.name);
            }
            else
            {
                Player.SendMessage(p, "There isn't anyone on the queue list.");
            }
        }

        public void ReviewLeave(Player p, string message)
        {
            if (reviewQueue.Contains(p))
            {
                reviewQueue.Remove(p);
                Player.SendMessage(p, "You were removed from the review queue.");
            }
            else
            {
                Player.SendMessage(p, "You are not listed on the review queue.");
            }
        }

        public void ReviewList(Player p, string message)
        {
            Player.SendMessage(p, "-------- Review List --------");
            int i = 1;
            string[] value = (from n in reviewQueue.PlayersOnQueueByName()
                select i++ + "." + n).ToArray();
            Player.SendMessage(p, string.Join(", ", value));
        }

        public void ReviewClear(Player p, string message)
        {
            if (p == null || p.group.Permission >= LevelPermission.Admin)
            {
                reviewQueue.Clear();
                Player.SendMessage(p, "Review queue was cleared.");
            }
            else
            {
                Player.SendMessage(p, "You are not allowed to use /review clear.");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/review ask - places you on the review list,");
            Player.SendMessage(p, "/review leave - removes you from the queue,");
            Player.SendMessage(p, "/review list - shows players waiting for a review.");
            if (p == null || p.group.Permission >= LevelPermission.Operator)
            {
                Player.SendMessage(p, "/review next - (OPs+) lets you review a player.");
            }
            if (p == null || p.group.Permission >= LevelPermission.Admin)
            {
                Player.SendMessage(p, "/review clear - (Admin+) clears the review list.");
            }
        }

        public void ReportNewQueuePosition()
        {
            if (reviewQueue.QueueLength <= 0)
            {
                return;
            }
            reviewQueue.PlayersOnQueue().ForEach(delegate(Player pl)
            {
                if (pl != null)
                {
                    Player.SendMessage(pl, string.Format("Currently you are %c{0}.%s on the review list.", reviewQueue.QuequePosition(pl) + 1));
                }
            });
        }
    }
}