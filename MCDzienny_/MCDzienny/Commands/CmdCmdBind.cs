using System;

namespace MCDzienny
{
    // Token: 0x020002E8 RID: 744
    public class CmdCmdBind : Command
    {
        // Token: 0x1700083B RID: 2107
        // (get) Token: 0x0600150D RID: 5389 RVA: 0x00074A50 File Offset: 0x00072C50
        public override string name
        {
            get { return "cmdbind"; }
        }

        // Token: 0x1700083C RID: 2108
        // (get) Token: 0x0600150E RID: 5390 RVA: 0x00074A58 File Offset: 0x00072C58
        public override string shortcut
        {
            get { return "cb"; }
        }

        // Token: 0x1700083D RID: 2109
        // (get) Token: 0x0600150F RID: 5391 RVA: 0x00074A60 File Offset: 0x00072C60
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x1700083E RID: 2110
        // (get) Token: 0x06001510 RID: 5392 RVA: 0x00074A68 File Offset: 0x00072C68
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700083F RID: 2111
        // (get) Token: 0x06001511 RID: 5393 RVA: 0x00074A6C File Offset: 0x00072C6C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Builder; }
        }

        // Token: 0x17000840 RID: 2112
        // (get) Token: 0x06001512 RID: 5394 RVA: 0x00074A70 File Offset: 0x00072C70
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06001514 RID: 5396 RVA: 0x00074A7C File Offset: 0x00072C7C
        public override void Use(Player p, string message)
        {
            var text = "";
            if (message.IndexOf(' ') == -1)
            {
                var flag = false;
                for (var i = 0; i < 10; i++)
                    if (p.cmdBind[i] != null)
                    {
                        Player.SendMessage(p,
                            string.Format("&c/{0} bound to &b{1} {2}", i, p.cmdBind[i], p.messageBind[i]));
                        flag = true;
                    }

                if (!flag) Player.SendMessage(p, "You have no commands binded");
                return;
            }

            if (message.Split(' ').Length == 1)
                try
                {
                    int num = Convert.ToInt16(message);
                    if (p.cmdBind[num] == null)
                    {
                        Player.SendMessage(p, "No command stored here yet.");
                        return;
                    }

                    var text2 = "/" + p.cmdBind[num] + " " + p.messageBind[num];
                    Player.SendMessage(p, string.Format("Stored command: &b{0}", text2));
                    return;
                }
                catch
                {
                    Help(p);
                    return;
                }

            if (message.Split(' ').Length > 1)
                try
                {
                    int num = Convert.ToInt16(message.Split(' ')[message.Split(' ').Length - 1]);
                    var text2 = message.Split(' ')[0];
                    if (message.Split(' ').Length > 2)
                    {
                        text = message.Substring(message.IndexOf(' ') + 1);
                        text = text.Remove(text.LastIndexOf(' '));
                    }

                    p.cmdBind[num] = text2;
                    p.messageBind[num] = text;
                    Player.SendMessage(p, string.Format("Binded &b/{0} {1} to &c/{2}", text2, text, num));
                }
                catch
                {
                    Help(p);
                }
        }

        // Token: 0x06001515 RID: 5397 RVA: 0x00074C70 File Offset: 0x00072E70
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/cmdbind [command] [num] - Binds [command] to [num]");
            Player.SendMessage(p, "[num] must be between 0 and 9");
            Player.SendMessage(p, "Use with \"/[num]\" &b(example: /2)");
            Player.SendMessage(p, "Use /cmdbind [num] to see stored commands.");
        }
    }
}