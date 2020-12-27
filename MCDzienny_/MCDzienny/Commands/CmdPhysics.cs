using System;

namespace MCDzienny
{
    // Token: 0x02000282 RID: 642
    public class CmdPhysics : Command
    {
        // Token: 0x170006D6 RID: 1750
        // (get) Token: 0x0600126A RID: 4714 RVA: 0x0006573C File Offset: 0x0006393C
        public override string name
        {
            get { return "physics"; }
        }

        // Token: 0x170006D7 RID: 1751
        // (get) Token: 0x0600126B RID: 4715 RVA: 0x00065744 File Offset: 0x00063944
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170006D8 RID: 1752
        // (get) Token: 0x0600126C RID: 4716 RVA: 0x0006574C File Offset: 0x0006394C
        public override string type
        {
            get { return "information"; }
        }

        // Token: 0x170006D9 RID: 1753
        // (get) Token: 0x0600126D RID: 4717 RVA: 0x00065754 File Offset: 0x00063954
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x170006DA RID: 1754
        // (get) Token: 0x0600126E RID: 4718 RVA: 0x00065758 File Offset: 0x00063958
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x06001270 RID: 4720 RVA: 0x00065764 File Offset: 0x00063964
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                foreach (var level in Server.levels)
                    if (level.physics > 0)
                        Player.SendMessage(p,
                            string.Format("&5{0} has physics at &b{1}. &cChecks: {2}; Updates: {3}",
                                level.name + Server.DefaultColor, level.physics + Server.DefaultColor, level.lastCheck,
                                level.lastUpdate));
                return;
            }

            try
            {
                int num;
                Level level2;
                if (message.Split(' ').Length == 1)
                {
                    num = int.Parse(message);
                    if (p != null)
                        level2 = p.level;
                    else
                        level2 = Server.mainLevel;
                }
                else
                {
                    num = Convert.ToInt16(message.Split(' ')[1]);
                    var levelName = message.Split(' ')[0];
                    level2 = Level.Find(levelName);
                }

                if (num >= 0 && num <= 4)
                {
                    level2.setPhysics(num);
                    switch (num)
                    {
                        case 0:
                            level2.ClearPhysics();
                            Player.GlobalMessage(string.Format("Physics are now &cOFF{0} on &b{1}.",
                                Server.DefaultColor, level2.name + Server.DefaultColor));
                            Server.s.Log("Physics are now OFF on " + level2.name + ".");
                            Player.IRCSay(string.Format("Physics are now OFF on {0}.", level2.name));
                            break;
                        case 1:
                            Player.GlobalMessage(string.Format("Physics are now &aNormal{0} on &b{1}.",
                                Server.DefaultColor, level2.name + Server.DefaultColor));
                            Server.s.Log("Physics are now ON on " + level2.name + ".");
                            Player.IRCSay(string.Format("Physics are now ON on {0}.", level2.name));
                            break;
                        case 2:
                            Player.GlobalMessage(string.Format("Physics are now &aAdvanced{0} on &b{1}.",
                                Server.DefaultColor, level2.name + Server.DefaultColor));
                            Server.s.Log("Physics are now ADVANCED on " + level2.name + ".");
                            Player.IRCSay(string.Format("Physics are now ADVANCED on {0}.", level2.name));
                            break;
                        case 3:
                            Player.GlobalMessage(string.Format("Physics are now &aHardcore{0} on &b{1}.",
                                Server.DefaultColor, level2.name + Server.DefaultColor));
                            Server.s.Log("Physics are now HARDCORE on " + level2.name + ".");
                            Player.IRCSay(string.Format("Physics are now HARDCORE on {0}.", level2.name));
                            break;
                        case 4:
                            Player.GlobalMessage(string.Format("Physics are now &aDoor{0} on &b{1}.",
                                Server.DefaultColor, level2.name + Server.DefaultColor));
                            Server.s.Log("Physics are now DOOR on " + level2.name + ".");
                            Player.IRCSay(string.Format("Physics are now DOOR on {0}.", level2.name));
                            break;
                    }

                    level2.changed = true;
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

        // Token: 0x06001271 RID: 4721 RVA: 0x00065B00 File Offset: 0x00063D00
        public override void Help(Player p)
        {
            Player.SendMessage(p,
                "/physics [map] <0/1/2/3/4> - Set the [map]'s physics, 0-Off 1-On 2-Advanced 3-Hardcore 4-Instant");
            Player.SendMessage(p, "If [map] is blank, uses Current level");
        }
    }
}