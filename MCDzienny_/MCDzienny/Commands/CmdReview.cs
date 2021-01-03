using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using MCDzienny.Misc;
using MCDzienny.Settings;

namespace MCDzienny
{
    // Token: 0x0200007C RID: 124
    public class CmdReview : Command
    {
        // Token: 0x040001C3 RID: 451
        private static Timer checkReviewList;

        // Token: 0x040001C4 RID: 452
        private static readonly ReviewQueue reviewQueue = new ReviewQueue();

        // Token: 0x040001C5 RID: 453
        private static readonly List<string> lastReviewed = new List<string>();

        // Token: 0x0600033B RID: 827 RVA: 0x00011E14 File Offset: 0x00010014
        public CmdReview()
        {
            if (checkReviewList != null) return;
            checkReviewList = new Timer(new TimeSpan(0, 6, 0).TotalMilliseconds);
            checkReviewList.Elapsed += checkReviewList_Elapsed;
            checkReviewList.Start();
        }

        // Token: 0x170000EA RID: 234
        // (get) Token: 0x06000335 RID: 821 RVA: 0x00011DF0 File Offset: 0x0000FFF0
        public override string name
        {
            get { return "review"; }
        }

        // Token: 0x170000EB RID: 235
        // (get) Token: 0x06000336 RID: 822 RVA: 0x00011DF8 File Offset: 0x0000FFF8
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170000EC RID: 236
        // (get) Token: 0x06000337 RID: 823 RVA: 0x00011E00 File Offset: 0x00010000
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170000ED RID: 237
        // (get) Token: 0x06000338 RID: 824 RVA: 0x00011E08 File Offset: 0x00010008
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170000EE RID: 238
        // (get) Token: 0x06000339 RID: 825 RVA: 0x00011E0C File Offset: 0x0001000C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x170000EF RID: 239
        // (get) Token: 0x0600033A RID: 826 RVA: 0x00011E10 File Offset: 0x00010010
        public override CommandScope Scope
        {
            get { return CommandScope.Freebuild | CommandScope.Lava | CommandScope.Home; }
        }

        // Token: 0x0600033C RID: 828 RVA: 0x00011E6C File Offset: 0x0001006C
        private void checkReviewList_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (reviewQueue.RemoveDisconnectedPlayers()) ReportNewQueuePosition();
                if (reviewQueue.QueueLength > 0)
                    Player.GlobalMessageOps(MCColor.DarkTeal + "# There are players waiting for a review.");
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x0600033D RID: 829 RVA: 0x00011ECC File Offset: 0x000100CC
        public override void Use(Player p, string message)
        {
            var message2 = new Message(message);
            string key;
            switch (key = message2.ReadStringLower())
            {
                case "ask":
                case "join":
                case "request":
                    JoinReviewQueue(p);
                    return;
                case "next":
                case "do":
                    ReviewNext(p);
                    return;
                case "leave":
                case "exit":
                    ReviewLeave(p, message);
                    return;
                case "list":
                    ReviewList(p, message);
                    return;
                case "clear":
                    ReviewClear(p, message);
                    return;
                case "last":
                case "checked":
                    ShowLastReviewed(p);
                    return;
            }

            Help(p);
        }

        // Token: 0x0600033E RID: 830 RVA: 0x0001200C File Offset: 0x0001020C
        private void ShowLastReviewed(Player p)
        {
            Player.SendMessage(p, "------- Last Reviewed Players -------");
            Player.SendMessage(p, string.Join(", ", lastReviewed.ToArray()));
        }

        // Token: 0x0600033F RID: 831 RVA: 0x00012034 File Offset: 0x00010234
        public void JoinReviewQueue(Player p)
        {
            if (reviewQueue.RemoveDisconnectedPlayers()) ReportNewQueuePosition();
            if (reviewQueue.Contains(p))
            {
                Player.SendMessage(p,
                    string.Format("You are {0}. in the review queue.", reviewQueue.QuequePosition(p) + 1));
                return;
            }

            reviewQueue.Enqueue(p);
            Player.SendMessage(p, "------------ Review ------------");
            Player.SendMessage(p, "You were added to the review queue.");
            Player.SendMessage(p,
                string.Format("You are currently %c{0}.%s one the review queue.", reviewQueue.QuequePosition(p) + 1));
            Player.SendMessage(p, "Please wait for some Operator to review your work.");
            Player.GlobalMessage(p.color + p.PublicName + " %sjoined the review queue.");
            Player.GlobalMessageOps("# Player " + p.color + p.PublicName + "%s requested a review.");
        }

        // Token: 0x06000340 RID: 832 RVA: 0x0001210C File Offset: 0x0001030C
        public void ReviewNext(Player p)
        {
            if (reviewQueue.QueueLength <= 0)
            {
                Player.SendMessage(p, "There isn't anyone on the queue list.");
                return;
            }

            Player player;
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

            if (p.group.Permission < (LevelPermission) GeneralSettings.All.MinPermissionForReview)
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
            Player.SendMessage(player,
                string.Format(MCColor.Gold + "You are about to get a review from {0}", p.color + p.PublicName));
            Player.SendMessage(player, "========================================================");
            all.Find("tp").Use(p, player.name);
        }

        // Token: 0x06000341 RID: 833 RVA: 0x00012274 File Offset: 0x00010474
        public void ReviewLeave(Player p, string message)
        {
            if (reviewQueue.Contains(p))
            {
                reviewQueue.Remove(p);
                Player.SendMessage(p, "You were removed from the review queue.");
                return;
            }

            Player.SendMessage(p, "You are not listed on the review queue.");
        }

        // Token: 0x06000342 RID: 834 RVA: 0x000122A8 File Offset: 0x000104A8
        public void ReviewList(Player p, string message)
        {
            Player.SendMessage(p, "-------- Review List --------");
            var i = 1;
            var value = (from n in reviewQueue.PlayersOnQueueByName()
                select i++ + "." + n).ToArray();
            Player.SendMessage(p, string.Join(", ", value));
        }

        // Token: 0x06000343 RID: 835 RVA: 0x00012300 File Offset: 0x00010500
        public void ReviewClear(Player p, string message)
        {
            if (p == null || p.group.Permission >= LevelPermission.Admin)
            {
                reviewQueue.Clear();
                Player.SendMessage(p, "Review queue was cleared.");
                return;
            }

            Player.SendMessage(p, "You are not allowed to use /review clear.");
        }

        // Token: 0x06000344 RID: 836 RVA: 0x00012338 File Offset: 0x00010538
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/review ask - places you on the review list,");
            Player.SendMessage(p, "/review leave - removes you from the queue,");
            Player.SendMessage(p, "/review list - shows players waiting for a review.");
            if (p == null || p.group.Permission >= LevelPermission.Operator)
                Player.SendMessage(p, "/review next - (OPs+) lets you review a player.");
            if (p == null || p.group.Permission >= LevelPermission.Admin)
                Player.SendMessage(p, "/review clear - (Admin+) clears the review list.");
        }

        // Token: 0x06000345 RID: 837 RVA: 0x000123A0 File Offset: 0x000105A0
        public void ReportNewQueuePosition()
        {
            if (reviewQueue.QueueLength <= 0) return;
            reviewQueue.PlayersOnQueue().ForEach(delegate(Player pl)
            {
                if (pl != null)
                    Player.SendMessage(pl,
                        string.Format("Currently you are %c{0}.%s on the review list.",
                            reviewQueue.QuequePosition(pl) + 1));
            });
        }
    }
}