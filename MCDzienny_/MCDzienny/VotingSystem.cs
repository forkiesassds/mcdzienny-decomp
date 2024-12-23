using System.Timers;

namespace MCDzienny
{
    public class VotingSystem
    {

        public delegate void VoteDelegate(Player player, string message);

        public enum TypeOfVote
        {
            Vote,
            VoteKick,
            VoteBan,
            None
        }

        public enum VotingChoice
        {
            DidntVote,
            Yes,
            No
        }

        public static string message = "";

        public static Player pl;

        public static TypeOfVote typeOfVote = TypeOfVote.None;

        public static bool votingInProgress;

        public static int minimumVotes = 5;

        public static Timer votingTimer;

        public static VoteDelegate voteDelegate;

        public static object[] voteDelegateObject;

        public static void CountVotes(out int votesYes, out int votesNo)
        {
            int vYes = 0;
            int vNo = 0;
            Player.players.ForEach(delegate(Player pl)
            {
                if (pl.votingChoice == VotingChoice.Yes)
                {
                    vYes++;
                }
                if (pl.votingChoice == VotingChoice.No)
                {
                    vNo++;
                }
            });
            votesYes = vYes;
            votesNo = vNo;
        }

        public static byte Decide(int votesYes, int votesNo)
        {
            if (votesYes + votesNo < minimumVotes)
            {
                return 0;
            }
            if (votesYes - votesNo > 0)
            {
                return 1;
            }
            return 2;
        }

        public static void SetVote()
        {
            votingInProgress = true;
            minimumVotes = (int)(Player.players.Count / 3.5f + 1f) >= 10 ? 10 : (int)(Player.players.Count / 3.5f + 1f);
        }

        public static void EndVote()
        {
            Player.players.ForEach(delegate(Player pl) { pl.votingChoice = VotingChoice.DidntVote; });
        }

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

        public static void VoteResultsEvent(object sender, ElapsedEventArgs e)
        {
            votingInProgress = false;
            votingTimer.Elapsed -= VoteResultsEvent;
            int votesYes = 0;
            int votesNo = 0;
            CountVotes(out votesYes, out votesNo);
            if (typeOfVote == TypeOfVote.Vote)
            {
                Player.GlobalMessage("Vote Ended.  Results: Y: %c" + votesYes + Server.DefaultColor + " N: %c" + votesNo);
                Server.s.Log("Vote results for " + message + ": " + votesYes + " yes and " + votesNo + " no votes.");
                if (votesYes - votesNo > 0)
                {
                    Player.GlobalMessage("The people said %cYES " + Server.DefaultColor + "!");
                }
                else
                {
                    Player.GlobalMessage("The people said %cNO " + Server.DefaultColor + "!");
                }
            }
            else if (typeOfVote == TypeOfVote.VoteKick)
            {
                switch (Decide(votesYes, votesNo))
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
                Player.GlobalMessage("Vote Ended.  Results: Y: %c" + votesYes + Server.DefaultColor + " N: %c" + votesNo);
                Server.s.Log("VoteBan results for " + pl.PublicName + ": " + votesYes + " yes and " + votesNo + " no votes.");
                if (votesYes + votesNo < minimumVotes)
                {
                    Player.GlobalMessage("Not enough votes were made. " + pl.color + pl.PublicName + " " + Server.DefaultColor + "shall remain!");
                }
                else if (votesYes - votesNo > 0)
                {
                    Player.GlobalMessage("The people decided, " + pl.color + pl.PublicName + " " + Server.DefaultColor + "is gone!");
                    Command.all.Find("tempban").Use(null, pl.name);
                }
                else
                {
                    Player.GlobalMessage(pl.color + pl.PublicName + " " + Server.DefaultColor + "shall remain!");
                }
            }
            EndVote();
        }
    }
}