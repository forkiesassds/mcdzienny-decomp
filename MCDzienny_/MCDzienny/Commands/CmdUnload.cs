namespace MCDzienny
{
    // Token: 0x020002A8 RID: 680
    public class CmdUnload : Command
    {
        // Token: 0x17000770 RID: 1904
        // (get) Token: 0x06001388 RID: 5000 RVA: 0x0006BAB8 File Offset: 0x00069CB8
        public override string name
        {
            get { return "unload"; }
        }

        // Token: 0x17000771 RID: 1905
        // (get) Token: 0x06001389 RID: 5001 RVA: 0x0006BAC0 File Offset: 0x00069CC0
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000772 RID: 1906
        // (get) Token: 0x0600138A RID: 5002 RVA: 0x0006BAC8 File Offset: 0x00069CC8
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x17000773 RID: 1907
        // (get) Token: 0x0600138B RID: 5003 RVA: 0x0006BAD0 File Offset: 0x00069CD0
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000774 RID: 1908
        // (get) Token: 0x0600138C RID: 5004 RVA: 0x0006BAD4 File Offset: 0x00069CD4
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x0600138E RID: 5006 RVA: 0x0006BAE0 File Offset: 0x00069CE0
        public override void Use(Player p, string message)
        {
            if (message.ToLower() == "empty")
            {
                var Empty = true;
                using (var enumerator = Server.levels.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        var l = enumerator.Current;
                        Empty = true;
                        Player.players.ForEach(delegate(Player pl)
                        {
                            if (pl.level == l) Empty = false;
                        });
                        if (Empty && l.unload)
                        {
                            l.Unload();
                            return;
                        }
                    }
                }

                Player.SendMessage(p, "No levels were empty.");
                return;
            }

            var level = Level.Find(message);
            if (level != null)
            {
                if (!level.Unload()) Player.SendMessage(p, "You cannot unload the main level.");
                return;
            }

            Player.SendMessage(p, string.Format("There is no level \"{0}\" loaded.", message));
        }

        // Token: 0x0600138F RID: 5007 RVA: 0x0006BBE4 File Offset: 0x00069DE4
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/unload [level] - Unloads a level.");
            Player.SendMessage(p, "/unload empty - Unloads an empty level.");
        }
    }
}