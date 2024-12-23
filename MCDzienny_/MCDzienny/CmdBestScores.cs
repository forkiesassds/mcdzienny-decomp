using System.Data;

namespace MCDzienny
{
    public class CmdBestScores : Command
    {
        public override string name { get { return "bestscores"; } }

        public override string shortcut { get { return "bs"; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override string CustomName { get { return Lang.Command.BestScoresName; } }

        public override CommandScope Scope { get { return CommandScope.Lava; } }

        public override void Use(Player p, string message)
        {
            Player.SendMessage(p, Lang.Command.BestScoresMessage);
            using (DataTable dataTable = DBInterface.fillData("SELECT * FROM `Players` ORDER BY bestScore DESC LIMIT 10"))
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Player.SendMessage(
                        p,
                        string.Format(Lang.Command.BestScoresMessage1, i + 1, dataTable.Rows[i]["Name"], dataTable.Rows[i]["bestScore"]));
                }
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.BestScoresHelp);
        }
    }
}