using System.Timers;

namespace MCDzienny
{
    // Token: 0x020003A0 RID: 928
    public class VotingSystem
    {
        // Token: 0x020003A3 RID: 931
        // (Invoke) Token: 0x06001A72 RID: 6770
        public delegate void VoteDelegate(Player player, string message);

        // Token: 0x020003A2 RID: 930
        public enum TypeOfVote
        {
            // Token: 0x04000E9B RID: 3739
            Vote,

            // Token: 0x04000E9C RID: 3740
            VoteKick,

            // Token: 0x04000E9D RID: 3741
            VoteBan,

            // Token: 0x04000E9E RID: 3742
            None
        }

        // Token: 0x020003A1 RID: 929
        public enum VotingChoice
        {
            // Token: 0x04000E97 RID: 3735
            DidntVote,

            // Token: 0x04000E98 RID: 3736
            Yes,

            // Token: 0x04000E99 RID: 3737
            No
        }

        // Token: 0x04000E8D RID: 3725
        public static string message = "";

        // Token: 0x04000E8E RID: 3726
        public static Player pl;

        // Token: 0x04000E8F RID: 3727
        public static TypeOfVote typeOfVote = TypeOfVote.None;

        // Token: 0x04000E90 RID: 3728
        public static bool votingInProgress;

        // Token: 0x04000E91 RID: 3729
        public static int minimumVotes = 5;

        // Token: 0x04000E92 RID: 3730
        public static Timer votingTimer;

        // Token: 0x04000E93 RID: 3731
        public static VoteDelegate voteDelegate;

        // Token: 0x04000E94 RID: 3732
        public static object[] voteDelegateObject;

        // Token: 0x06001A68 RID: 6760 RVA: 0x000B9F98 File Offset: 0x000B8198
        public static void CountVotes(out int votesYes, out int votesNo)
        {
            var vYes = 0;
            var vNo = 0;
            Player.players.ForEach(delegate(Player pl)
            {
                if (pl.votingChoice == VotingChoice.Yes) vYes++;
                if (pl.votingChoice == VotingChoice.No) vNo++;
            });
            votesYes = vYes;
            votesNo = vNo;
        }

        // Token: 0x06001A69 RID: 6761 RVA: 0x000B9FE0 File Offset: 0x000B81E0
        public static byte Decide(int votesYes, int votesNo)
        {
            if (votesYes + votesNo < minimumVotes) return 0;
            if (votesYes - votesNo > 0) return 1;
            return 2;
        }

        // Token: 0x06001A6A RID: 6762 RVA: 0x000B9FF8 File Offset: 0x000B81F8
        public static void SetVote()
        {
            votingInProgress = true;
            minimumVotes = (int) ((float) Player.players.Count / 3.5f + 1f) >= 10
                ? 10
                : (int) (Player.players.Count / 3.5f + 1f);
        }

        // Token: 0x06001A6B RID: 6763 RVA: 0x000BA048 File Offset: 0x000B8248
        public static void EndVote()
        {
            Player.players.ForEach(delegate(Player pl) { pl.votingChoice = VotingChoice.DidntVote; });
        }

        // Token: 0x06001A6C RID: 6764 RVA: 0x000BA074 File Offset: 0x000B8274
        public static void StartVote(Player p, string msg, TypeOfVote tov, int time)
        {
            pl = p;
            message = msg;
            typeOfVote = tov;
            SetVote();
            votingTimer = new Timer(time);
            votingTimer.Elapsed += VoteResultsEvent;
            votingTimer.AutoReset = false;
            votingTimer.Start();
        }

        // Token: 0x06001A6D RID: 6765 RVA: 0x000BA0D0 File Offset: 0x000B82D0
        public static void VoteResultsEvent(object sender, ElapsedEventArgs e)
        {
            votingInProgress = false;
            votingTimer.Elapsed -= VoteResultsEvent;
            var num = 0;
            var num2 = 0;
            CountVotes(out num, out num2);
            if (typeOfVote == TypeOfVote.Vote)
            {
                Player.GlobalMessage(string.Concat("Vote Ended.  Results: Y: %c", num, Server.DefaultColor, " N: %c",
                    num2));
                Server.s.Log(string.Concat("Vote results for ", message, ": ", num, " yes and ", num2, " no votes."));
                if (num - num2 > 0)
                    Player.GlobalMessage("The people said %cYES " + Server.DefaultColor + "!");
                else
                    Player.GlobalMessage("The people said %cNO " + Server.DefaultColor + "!");
            }
            else if (typeOfVote == TypeOfVote.VoteKick)
            {
                switch (Decide(num, num2))
                {
                    case 0:
                        Player.GlobalMessage("Not enough votes were made.");
                        break;
                    case 1:
                        Player.GlobalMessage("People decided, " + pl.PublicName + " has to leave!");
                        pl.Kick("Democracy FTW");
                        break;
                    case 2:
                        Player.GlobalMessage("People decided, " + pl.PublicName + " will stay!");
                        break;
                }
            }
            else if (typeOfVote == TypeOfVote.VoteBan)
            {
                Player.GlobalMessage(string.Concat("Vote Ended.  Results: Y: %c", num, Server.DefaultColor, " N: %c",
                    num2));
                Server.s.Log(string.Concat("VoteBan results for ", pl.PublicName, ": ", num, " yes and ", num2,
                    " no votes."));
                if (num + num2 < minimumVotes)
                {
                    Player.GlobalMessage(string.Concat("Not enough votes were made. ", pl.color, pl.PublicName, " ",
                        Server.DefaultColor, "shall remain!"));
                }
                else if (num - num2 > 0)
                {
                    Player.GlobalMessage(string.Concat("The people decided, ", pl.color, pl.PublicName, " ",
                        Server.DefaultColor, "is gone!"));
                    Command.all.Find("tempban").Use(null, pl.name);
                }
                else
                {
                    Player.GlobalMessage(string.Concat(pl.color, pl.PublicName, " ", Server.DefaultColor,
                        "shall remain!"));
                }
            }

            EndVote();
        }
    }
}