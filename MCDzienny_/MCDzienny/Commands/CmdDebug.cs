using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MCDzienny.Cpe;

namespace MCDzienny
{
    // Token: 0x02000010 RID: 16
    public class CmdDebug : Command
    {
        // Token: 0x04000042 RID: 66
        public static List<WeakReference> refs = new List<WeakReference>();

        // Token: 0x1700000C RID: 12
        // (get) Token: 0x06000055 RID: 85 RVA: 0x000045C8 File Offset: 0x000027C8
        public override string name
        {
            get { return "debug"; }
        }

        // Token: 0x1700000D RID: 13
        // (get) Token: 0x06000056 RID: 86 RVA: 0x000045D0 File Offset: 0x000027D0
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700000E RID: 14
        // (get) Token: 0x06000057 RID: 87 RVA: 0x000045D8 File Offset: 0x000027D8
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x1700000F RID: 15
        // (get) Token: 0x06000058 RID: 88 RVA: 0x000045E0 File Offset: 0x000027E0
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000010 RID: 16
        // (get) Token: 0x06000059 RID: 89 RVA: 0x000045E4 File Offset: 0x000027E4
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x17000011 RID: 17
        // (get) Token: 0x0600005A RID: 90 RVA: 0x000045E8 File Offset: 0x000027E8
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x0600005B RID: 91 RVA: 0x000045EC File Offset: 0x000027EC
        public override void Use(Player p, string message)
        {
            if (message == "1")
            {
                if (p.ExtraData.ContainsKey("supported_extensions"))
                {
                    var dictionary = (Dictionary<string, int>) p.ExtraData["supported_extensions"];
                    foreach (var keyValuePair in dictionary)
                        Player.SendMessage(p, "Ext {0} Ver {1}", keyValuePair.Key, keyValuePair.Value);
                }

                return;
            }

            if (message == "3")
            {
                var num = 256;
                for (var i = 0; i < num; i++)
                {
                    var red = (short) (153.0 / num * i);
                    var green = (short) (204.0 / num * i);
                    var num2 = (short) (255.0 / num * i);
                    V1.EnvSetColor(p, 0, red, green, num2);
                    V1.EnvSetColor(p, 1, num2, num2, num2);
                    V1.EnvSetColor(p, 2, num2, num2, num2);
                    Thread.Sleep(100);
                }

                return;
            }

            if (message.Split()[0] == "2")
            {
                message.Split();
                if (p.Cpe.ExtPlayerList == 1) V1.ExtAddPlayerName(p, 3, "&fTest", "&fTest", "&7Master", 0);
                if (p.Cpe.SelectionCuboid == 1) V1.MakeSelection(p, 0, "test", 20, 20, 20, 40, 40, 40, 255, 0, 0, 64);
                return;
            }

            if (message.Split()[0] == "4")
            {
                message.Split();
                V1.Message(p, V1.Announcement, "Hello " + "abcdefg"[new Random().Next(6)]);
                Thread.Sleep(3000);
                V1.Message(p, V1.Announcement, "");
                return;
            }

            if (message.Split()[0] == "5")
            {
                var options = new CpeApi.V1.MessageOptions
                {
                    MaxDelay = TimeSpan.FromSeconds(30.0),
                    MinDisplayTime = TimeSpan.FromSeconds(1.0),
                    DisplayTime = TimeSpan.FromSeconds(8.0)
                };
                var options2 = new CpeApi.V1.MessageOptions
                {
                    MaxDelay = TimeSpan.FromSeconds(30.0),
                    Priority = CpeApi.V1.MessagePriority.Low,
                    MinDisplayTime = TimeSpan.FromSeconds(8.0),
                    DisplayTime = TimeSpan.FromSeconds(8.0),
                    IsBlinking = true,
                    BlinkPeriod = TimeSpan.FromMilliseconds(600.0),
                    AltMessage = "%0You have been banned!"
                };
                var options3 = new CpeApi.V1.MessageOptions
                {
                    MaxDelay = TimeSpan.FromSeconds(3.0),
                    MinDisplayTime = TimeSpan.FromSeconds(3.0),
                    DisplayTime = TimeSpan.FromSeconds(8.0)
                };
                var options4 = new CpeApi.V1.MessageOptions
                {
                    MaxDelay = TimeSpan.FromSeconds(3.0),
                    MinDisplayTime = TimeSpan.FromSeconds(1.0),
                    DisplayTime = TimeSpan.FromSeconds(8.0)
                };
                var flag = CpeApi.V1.SendMessage(p, CpeApi.V1.MessageType.Announcement, options2,
                    "%fYou have been banned!");
                var flag2 = CpeApi.V1.SendMessage(p, CpeApi.V1.MessageType.BottomRight2, options3, "%cHello World!");
                var flag3 = CpeApi.V1.SendMessage(p, CpeApi.V1.MessageType.BottomRight2, options, "%eHello World!");
                var flag4 = CpeApi.V1.SendMessage(p, CpeApi.V1.MessageType.BottomRight2, options4, "%4Hello World!");
                var flag5 = CpeApi.V1.SendMessage(p, CpeApi.V1.MessageType.BottomRight2, options, "%fHello World!");
                var flag6 = CpeApi.V1.SendMessage(p, CpeApi.V1.MessageType.BottomRight2, options, "%3Hello World!");
                Player.SendMessage(p,
                    string.Concat("Results: ", flag, " ", flag2, " ", flag3, " ", flag4, " ", flag5, " ", flag6));
                return;
            }

            if (message.Split()[0] == "6")
            {
                var num3 = (from w in refs
                    where w.Target != null
                    select w).Count();
                Player.SendMessage(p, "Count: " + num3);
                return;
            }

            if (p.ExtraData.ContainsKey("app_name")) Player.SendMessage(p, "Your client: " + p.ExtraData["app_name"]);
            if (p.ExtraData.ContainsKey("extensions_count"))
                Player.SendMessage(p, "Client supports {0} types of extensions.",
                    (short) p.ExtraData["extensions_count"]);
        }

        // Token: 0x0600005C RID: 92 RVA: 0x00004B54 File Offset: 0x00002D54
        public override void Help(Player p)
        {
            Player.SendMessage(p, "It's used only for debugging.");
        }
    }
}