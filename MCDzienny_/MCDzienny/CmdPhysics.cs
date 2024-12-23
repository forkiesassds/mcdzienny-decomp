using System;

namespace MCDzienny
{
    public class CmdPhysics : Command
    {
        public override string name { get { return "physics"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "information"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                foreach (Level level2 in Server.levels)
                {
                    if (level2.physics > 0)
                    {
                        Player.SendMessage(
                            p,
                            string.Format("&5{0} has physics at &b{1}. &cChecks: {2}; Updates: {3}", level2.name + Server.DefaultColor,
                                          level2.physics + Server.DefaultColor, level2.lastCheck, level2.lastUpdate));
                    }
                }
                return;
            }
            try
            {
                int num = 0;
                Level level = null;
                if (message.Split(' ').Length == 1)
                {
                    num = int.Parse(message);
                    level = p == null ? Server.mainLevel : p.level;
                }
                else
                {
                    num = Convert.ToInt16(message.Split(' ')[1]);
                    string levelName = message.Split(' ')[0];
                    level = Level.Find(levelName);
                }
                if (num >= 0 && num <= 4)
                {
                    level.setPhysics(num);
                    switch (num)
                    {
                        case 0:
                            level.ClearPhysics();
                            Player.GlobalMessage(string.Format("Physics are now &cOFF{0} on &b{1}.", Server.DefaultColor, level.name + Server.DefaultColor));
                            Server.s.Log("Physics are now OFF on " + level.name + ".");
                            Player.IRCSay(string.Format("Physics are now OFF on {0}.", level.name));
                            break;
                        case 1:
                            Player.GlobalMessage(string.Format("Physics are now &aNormal{0} on &b{1}.", Server.DefaultColor, level.name + Server.DefaultColor));
                            Server.s.Log("Physics are now ON on " + level.name + ".");
                            Player.IRCSay(string.Format("Physics are now ON on {0}.", level.name));
                            break;
                        case 2:
                            Player.GlobalMessage(string.Format("Physics are now &aAdvanced{0} on &b{1}.", Server.DefaultColor, level.name + Server.DefaultColor));
                            Server.s.Log("Physics are now ADVANCED on " + level.name + ".");
                            Player.IRCSay(string.Format("Physics are now ADVANCED on {0}.", level.name));
                            break;
                        case 3:
                            Player.GlobalMessage(string.Format("Physics are now &aHardcore{0} on &b{1}.", Server.DefaultColor, level.name + Server.DefaultColor));
                            Server.s.Log("Physics are now HARDCORE on " + level.name + ".");
                            Player.IRCSay(string.Format("Physics are now HARDCORE on {0}.", level.name));
                            break;
                        case 4:
                            Player.GlobalMessage(string.Format("Physics are now &aDoor{0} on &b{1}.", Server.DefaultColor, level.name + Server.DefaultColor));
                            Server.s.Log("Physics are now DOOR on " + level.name + ".");
                            Player.IRCSay(string.Format("Physics are now DOOR on {0}.", level.name));
                            break;
                    }
                    level.changed = true;
                }
                else
                {
                    Player.SendMessage(p, "Not a valid setting");
                }
            }
            catch
            {
                Player.SendMessage(p, "INVALID INPUT");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/physics [map] <0/1/2/3/4> - Set the [map]'s physics, 0-Off 1-On 2-Advanced 3-Hardcore 4-Instant");
            Player.SendMessage(p, "If [map] is blank, uses Current level");
        }
    }
}