namespace MCDzienny
{
    // Token: 0x020002E1 RID: 737
    public class CmdMap : Command
    {
        // Token: 0x17000821 RID: 2081
        // (get) Token: 0x060014E1 RID: 5345 RVA: 0x000735D0 File Offset: 0x000717D0
        public override string name
        {
            get { return "map"; }
        }

        // Token: 0x17000822 RID: 2082
        // (get) Token: 0x060014E2 RID: 5346 RVA: 0x000735D8 File Offset: 0x000717D8
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000823 RID: 2083
        // (get) Token: 0x060014E3 RID: 5347 RVA: 0x000735E0 File Offset: 0x000717E0
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x17000824 RID: 2084
        // (get) Token: 0x060014E4 RID: 5348 RVA: 0x000735E8 File Offset: 0x000717E8
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000825 RID: 2085
        // (get) Token: 0x060014E5 RID: 5349 RVA: 0x000735EC File Offset: 0x000717EC
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x060014E6 RID: 5350 RVA: 0x000735F0 File Offset: 0x000717F0
        public override void Use(Player p, string message)
        {
            if (message == "" && p == null)
            {
                Help(p);
                return;
            }

            if (message == "") message = p.level.name;
            Level level;
            if (message.IndexOf(' ') == -1)
            {
                level = Level.Find(message);
                if (level != null)
                {
                    Player.SendMessage(p, string.Format("MOTD: &b{0}", level.motd));
                    Player.SendMessage(p, string.Format("Finite mode: {0}", FoundCheck(level.finite)));
                    Player.SendMessage(p, string.Format("Animal AI: {0}", FoundCheck(level.ai)));
                    Player.SendMessage(p, string.Format("Edge water: {0}", FoundCheck(level.edgeWater)));
                    Player.SendMessage(p, string.Format("Grass growing: {0}", FoundCheck(level.GrassGrow)));
                    Player.SendMessage(p, string.Format("Physics speed: &b{0}", level.speedPhysics));
                    Player.SendMessage(p, string.Format("Physics overload: &b{0}", level.overload));
                    Player.SendMessage(p,
                        string.Format("Survival death: {0}(Fall: {1}, Drown: {2})", FoundCheck(level.Death), level.fall,
                            level.drown));
                    Player.SendMessage(p, string.Format("Killer blocks: {0}", FoundCheck(level.Killer)));
                    Player.SendMessage(p, string.Format("Unload: {0}", FoundCheck(level.unload)));
                    Player.SendMessage(p, string.Format("Auto physics: {0}", FoundCheck(level.rp)));
                    Player.SendMessage(p, string.Format("Instant building: {0}", FoundCheck(level.Instant)));
                    Player.SendMessage(p, string.Format("RP chat: {0}", FoundCheck(!level.worldChat)));
                    return;
                }

                if (p != null) level = p.level;
            }
            else
            {
                level = Level.Find(message.Split(' ')[0]);
                if (level == null || message.Split(' ')[0].ToLower() == "ps" || message.Split(' ')[0].ToLower() == "rp")
                    level = p.level;
                else
                    message = message.Substring(message.IndexOf(' ') + 1);
            }

            if (p != null && p.group.Permission < LevelPermission.Operator)
            {
                Player.SendMessage(p, "Setting map options is reserved to OP+");
                return;
            }

            var text = message.IndexOf(' ') != -1 ? message.Split(' ')[0].ToLower() : message.ToLower();
            try
            {
                switch (text)
                {
                    case "theme":
                        level.theme = message.Substring(message.IndexOf(' ') + 1);
                        level.ChatLevel(string.Format("Map theme: &b{0}", level.theme));
                        break;
                    case "finite":
                        level.finite = !level.finite;
                        level.ChatLevel(string.Format("Finite mode: {0}", FoundCheck(level.finite)));
                        break;
                    case "ai":
                        level.ai = !level.ai;
                        level.ChatLevel(string.Format("Animal AI: {0}", FoundCheck(level.ai)));
                        break;
                    case "edge":
                        level.edgeWater = !level.edgeWater;
                        level.ChatLevel(string.Format("Edge water: {0}", FoundCheck(level.edgeWater)));
                        break;
                    case "grass":
                        level.GrassGrow = !level.GrassGrow;
                        level.ChatLevel(string.Format("Growing grass: {0}", FoundCheck(level.GrassGrow)));
                        break;
                    case "ps":
                    case "physicspeed":
                        if (int.Parse(message.Split(' ')[1]) < 10)
                        {
                            Player.SendMessage(p, "Cannot go below 10");
                            return;
                        }

                        level.speedPhysics = int.Parse(message.Split(' ')[1]);
                        level.ChatLevel(string.Format("Physics speed: &b{0}", level.speedPhysics));
                        break;
                    case "overload":
                        if (int.Parse(message.Split(' ')[1]) < 500)
                        {
                            Player.SendMessage(p, "Cannot go below 500 (default is 1500)");
                            return;
                        }

                        if (p.group.Permission < LevelPermission.Admin && int.Parse(message.Split(' ')[1]) > 2500)
                        {
                            Player.SendMessage(p, "Only SuperOPs may set higher than 2500");
                            return;
                        }

                        level.overload = int.Parse(message.Split(' ')[1]);
                        level.ChatLevel(string.Format("Physics overload: &b{0}", level.overload));
                        break;
                    case "motd":
                        if (message.Split(' ').Length == 1)
                            level.motd = "ignore";
                        else
                            level.motd = message.Substring(message.IndexOf(' ') + 1);
                        level.ChatLevel(string.Format("Map MOTD: &b{0}", level.motd));
                        break;
                    case "death":
                        level.Death = !level.Death;
                        level.ChatLevel(string.Format("Survival death: {0}", FoundCheck(level.Death)));
                        break;
                    case "killer":
                        level.Killer = !level.Killer;
                        level.ChatLevel(string.Format("Killer blocks: {0}", FoundCheck(level.Killer)));
                        break;
                    case "fall":
                        level.fall = int.Parse(message.Split(' ')[1]);
                        level.ChatLevel(string.Format("Fall distance: &b{0}", level.fall));
                        break;
                    case "drown":
                        level.drown = int.Parse(message.Split(' ')[1]) * 10;
                        level.ChatLevel(string.Format("Drown time: &b{0}", level.drown / 10));
                        break;
                    case "unload":
                        level.unload = !level.unload;
                        level.ChatLevel(string.Format("Auto unload: {0}", FoundCheck(level.unload)));
                        break;
                    case "rp":
                    case "restartphysics":
                        level.rp = !level.rp;
                        level.ChatLevel(string.Format("Auto physics: {0}", FoundCheck(level.rp)));
                        break;
                    case "instant":
                        if (p.group.Permission < LevelPermission.Admin)
                        {
                            Player.SendMessage(p, "This is reserved for Super+");
                            return;
                        }

                        level.Instant = !level.Instant;
                        level.ChatLevel(string.Format("Instant building: {0}", FoundCheck(level.Instant)));
                        break;
                    case "chat":
                        level.worldChat = !level.worldChat;
                        level.ChatLevel(string.Format("RP chat: {0}", FoundCheck(!level.worldChat)));
                        break;
                    default:
                        Player.SendMessage(p, "Could not find option entered.");
                        return;
                }

                level.changed = true;
                if (p.level != level) Player.SendMessage(p, "/map finished!");
            }
            catch
            {
                Player.SendMessage(p, "INVALID INPUT");
            }
        }

        // Token: 0x060014E7 RID: 5351 RVA: 0x00073EB0 File Offset: 0x000720B0
        public string FoundCheck(bool check)
        {
            if (check) return "&aON";
            return "&cOFF";
        }

        // Token: 0x060014E8 RID: 5352 RVA: 0x00073EC0 File Offset: 0x000720C0
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/map [level] [toggle] - Sets [toggle] on [map]");
            Player.SendMessage(p,
                "Possible toggles: theme, finite, ai, edge, ps, overload, motd, death, fall, drown, unload, rp, instant, killer, chat");
            Player.SendMessage(p, "Edge will cause edge water to flow.");
            Player.SendMessage(p, "Grass will make grass not grow without physics");
            Player.SendMessage(p, "Finite will cause all liquids to be finite.");
            Player.SendMessage(p, "AI will make animals hunt or flee.");
            Player.SendMessage(p, "PS will set the map's physics speed.");
            Player.SendMessage(p, "Overload will change how easy/hard it is to kill physics.");
            Player.SendMessage(p, "MOTD will set a custom motd for the map. (leave blank to reset)");
            Player.SendMessage(p, "Death will allow survival-style dying (falling, drowning)");
            Player.SendMessage(p, "Fall/drown set the distance/time before dying from each.");
            Player.SendMessage(p, "Killer turns killer blocks on and off.");
            Player.SendMessage(p, "Unload sets whether the map unloads when no one's there.");
            Player.SendMessage(p, "RP sets whether the physics auto-start for the map");
            Player.SendMessage(p, "Instant mode works by not updating everyone's screens");
            Player.SendMessage(p, "Chat sets the map to recieve no messages from other maps");
        }
    }
}