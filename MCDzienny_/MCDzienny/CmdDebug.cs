using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MCDzienny.Cpe;

namespace MCDzienny
{
    public class CmdDebug : Command
    {
        public static List<WeakReference> refs = new List<WeakReference>();

        public override string name { get { return "debug"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (message == "1")
            {
                if (!p.ExtraData.ContainsKey("supported_extensions"))
                {
                    return;
                }
                var dictionary = (Dictionary<string, int>)p.ExtraData["supported_extensions"];
                {
                    foreach (KeyValuePair<string, int> item in dictionary)
                    {
                        Player.SendMessage(p, "Ext {0} Ver {1}", item.Key, item.Value);
                    }
                    return;
                }
            }
            if (message == "3")
            {
                int num = 256;
                for (int i = 0; i < num; i++)
                {
                    short red = (short)(153.0 / num * i);
                    short green = (short)(204.0 / num * i);
                    short num2 = (short)(255.0 / num * i);
                    V1.EnvSetColor(p, 0, red, green, num2);
                    V1.EnvSetColor(p, 1, num2, num2, num2);
                    V1.EnvSetColor(p, 2, num2, num2, num2);
                    Thread.Sleep(100);
                }
            }
            else if (message.Split()[0] == "2")
            {
                message.Split();
                if (p.Cpe.ExtPlayerList == 1)
                {
                    V1.ExtAddPlayerName(p, 3, "&fTest", "&fTest", "&7Master", 0);
                }
                if (p.Cpe.SelectionCuboid == 1)
                {
                    V1.MakeSelection(p, 0, "test", 20, 20, 20, 40, 40, 40, 255, 0, 0, 64);
                }
            }
            else if (message.Split()[0] == "4")
            {
                message.Split();
                V1.Message(p, V1.Announcement, "Hello " + "abcdefg"[new Random().Next(6)]);
                Thread.Sleep(3000);
                V1.Message(p, V1.Announcement, "");
            }
            else if (message.Split()[0] == "5")
            {
                CpeApi.V1.MessageOptions messageOptions = new CpeApi.V1.MessageOptions();
                messageOptions.MaxDelay = TimeSpan.FromSeconds(30.0);
                messageOptions.MinDisplayTime = TimeSpan.FromSeconds(1.0);
                messageOptions.DisplayTime = TimeSpan.FromSeconds(8.0);
                CpeApi.V1.MessageOptions options = messageOptions;
                CpeApi.V1.MessageOptions messageOptions2 = new CpeApi.V1.MessageOptions();
                messageOptions2.MaxDelay = TimeSpan.FromSeconds(30.0);
                messageOptions2.Priority = CpeApi.V1.MessagePriority.Low;
                messageOptions2.MinDisplayTime = TimeSpan.FromSeconds(8.0);
                messageOptions2.DisplayTime = TimeSpan.FromSeconds(8.0);
                messageOptions2.IsBlinking = true;
                messageOptions2.BlinkPeriod = TimeSpan.FromMilliseconds(600.0);
                messageOptions2.AltMessage = "%0You have been banned!";
                CpeApi.V1.MessageOptions options2 = messageOptions2;
                CpeApi.V1.MessageOptions messageOptions3 = new CpeApi.V1.MessageOptions();
                messageOptions3.MaxDelay = TimeSpan.FromSeconds(3.0);
                messageOptions3.MinDisplayTime = TimeSpan.FromSeconds(3.0);
                messageOptions3.DisplayTime = TimeSpan.FromSeconds(8.0);
                CpeApi.V1.MessageOptions options3 = messageOptions3;
                CpeApi.V1.MessageOptions messageOptions4 = new CpeApi.V1.MessageOptions();
                messageOptions4.MaxDelay = TimeSpan.FromSeconds(3.0);
                messageOptions4.MinDisplayTime = TimeSpan.FromSeconds(1.0);
                messageOptions4.DisplayTime = TimeSpan.FromSeconds(8.0);
                CpeApi.V1.MessageOptions options4 = messageOptions4;
                bool flag = CpeApi.V1.SendMessage(p, CpeApi.V1.MessageType.Announcement, options2, "%fYou have been banned!");
                bool flag2 = CpeApi.V1.SendMessage(p, CpeApi.V1.MessageType.BottomRight2, options3, "%cHello World!");
                bool flag3 = CpeApi.V1.SendMessage(p, CpeApi.V1.MessageType.BottomRight2, options, "%eHello World!");
                bool flag4 = CpeApi.V1.SendMessage(p, CpeApi.V1.MessageType.BottomRight2, options4, "%4Hello World!");
                bool flag5 = CpeApi.V1.SendMessage(p, CpeApi.V1.MessageType.BottomRight2, options, "%fHello World!");
                bool flag6 = CpeApi.V1.SendMessage(p, CpeApi.V1.MessageType.BottomRight2, options, "%3Hello World!");
                Player.SendMessage(p, "Results: " + flag + " " + flag2 + " " + flag3 + " " + flag4 + " " + flag5 + " " + flag6);
            }
            else if (message.Split()[0] == "6")
            {
                int num3 = refs.Where(w => w.Target != null).Count();
                Player.SendMessage(p, "Count: " + num3);
            }
            else
            {
                if (p.ExtraData.ContainsKey("app_name"))
                {
                    Player.SendMessage(p, "Your client: " + p.ExtraData["app_name"]);
                }
                if (p.ExtraData.ContainsKey("extensions_count"))
                {
                    Player.SendMessage(p, "Client supports {0} types of extensions.", (short)p.ExtraData["extensions_count"]);
                }
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "It's used only for debugging.");
        }
    }
}