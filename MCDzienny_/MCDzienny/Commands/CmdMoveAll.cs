using System;
using System.Threading;

namespace MCDzienny
{
    // Token: 0x020000CD RID: 205
    internal class CmdMoveAll : Command
    {
        // Token: 0x170002F9 RID: 761
        // (get) Token: 0x060006C7 RID: 1735 RVA: 0x00022F44 File Offset: 0x00021144
        public override string name
        {
            get { return "moveall"; }
        }

        // Token: 0x170002FA RID: 762
        // (get) Token: 0x060006C8 RID: 1736 RVA: 0x00022F4C File Offset: 0x0002114C
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170002FB RID: 763
        // (get) Token: 0x060006C9 RID: 1737 RVA: 0x00022F54 File Offset: 0x00021154
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x170002FC RID: 764
        // (get) Token: 0x060006CA RID: 1738 RVA: 0x00022F5C File Offset: 0x0002115C
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170002FD RID: 765
        // (get) Token: 0x060006CB RID: 1739 RVA: 0x00022F60 File Offset: 0x00021160
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x060006CC RID: 1740 RVA: 0x00022F64 File Offset: 0x00021164
        public override void Use(Player p, string message)
        {
            if (message == string.Empty || message.Split(' ').Length > 2)
            {
                Help(p);
                return;
            }

            if (message.Split(' ').Length == 2)
            {
                var map = message.ToLower().Split(' ');
                if (Level.Find(map[0]) == null)
                {
                    Player.SendMessage(p, "The first map couldn't be found.");
                    return;
                }

                if (Level.Find(map[1]) == null)
                {
                    Player.SendMessage(p, "The second map couldn't be found.");
                    return;
                }

                Player.players.ForEachSync(delegate(Player pl)
                {
                    ThreadPool.QueueUserWorkItem(delegate
                    {
                        try
                        {
                            if (pl.level.name == map[0]) all.Find("goto").Use(pl, map[1]);
                        }
                        catch (Exception ex)
                        {
                            Server.ErrorLog(ex);
                        }
                    });
                });
            }
            else
            {
                if (Level.Find(message.ToLower()) == null)
                {
                    Player.SendMessage(p, "The map couldn't be found.");
                    return;
                }

                Player.players.ForEachSync(delegate(Player pl)
                {
                    ThreadPool.QueueUserWorkItem(delegate
                    {
                        try
                        {
                            all.Find("goto").Use(pl, message.ToLower());
                        }
                        catch (Exception ex)
                        {
                            Server.ErrorLog(ex);
                        }
                    });
                });
            }
        }

        // Token: 0x060006CD RID: 1741 RVA: 0x000230AC File Offset: 0x000212AC
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/moveall [map] - moves all players on the server to the pointed map.");
            Player.SendMessage(p, "/moveall [map1] [map2] - moves all players that are on the map1 to the map2.");
        }
    }
}