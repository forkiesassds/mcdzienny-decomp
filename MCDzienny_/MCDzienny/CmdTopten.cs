using System.Data;

namespace MCDzienny
{
    public class CmdTopten : Command
    {
        public override string name { get { return "topten"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override CommandScope Scope { get { return CommandScope.Lava; } }

        public override void Use(Player p, string message)
        {
            Player.SendMessage(p, "The elite of the server:");
            using (DataTable dataTable = DBInterface.fillData("SELECT * FROM `Players` ORDER BY totalScore DESC LIMIT 10"))
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Player.SendMessage(
                        p,
                        string.Format("%c{0}. {1} - level: {2}", i + 1, dataTable.Rows[i]["Name"],
                                      TierSystem.TierCheck(int.Parse(dataTable.Rows[i]["totalScore"].ToString()))));
                }
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/topten - Displays names of ten best players.");
        }
    }
}