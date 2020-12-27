using System;
using MCDzienny.Cpe;
using MCDzienny.Levels.Effects;
using MCDzienny.Levels.Info;

namespace MCDzienny.Commands
{
    // Token: 0x02000014 RID: 20
    internal class CmdTexture : Command
    {
        // Token: 0x04000047 RID: 71
        private static readonly string DmwmTextureUrl = "http://s28.postimg.org/6bpm90mwd/terrain.png";

        // Token: 0x04000048 RID: 72
        private static readonly string Turtley3Url = "http://s12.postimg.org/ga3trp5yl/terrain.png";

        // Token: 0x1700001E RID: 30
        // (get) Token: 0x06000074 RID: 116 RVA: 0x00004D24 File Offset: 0x00002F24
        public override string name
        {
            get { return "texture"; }
        }

        // Token: 0x1700001F RID: 31
        // (get) Token: 0x06000075 RID: 117 RVA: 0x00004D2C File Offset: 0x00002F2C
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000020 RID: 32
        // (get) Token: 0x06000076 RID: 118 RVA: 0x00004D34 File Offset: 0x00002F34
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x17000021 RID: 33
        // (get) Token: 0x06000077 RID: 119 RVA: 0x00004D3C File Offset: 0x00002F3C
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000022 RID: 34
        // (get) Token: 0x06000078 RID: 120 RVA: 0x00004D40 File Offset: 0x00002F40
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x17000023 RID: 35
        // (get) Token: 0x06000079 RID: 121 RVA: 0x00004D44 File Offset: 0x00002F44
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x0600007A RID: 122 RVA: 0x00004D48 File Offset: 0x00002F48
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            var text = message.ToLower();
            if (!text.StartsWith("http"))
            {
                string a;
                if ((a = text) != null)
                {
                    if (a == "default")
                    {
                        p.level.Info.Texture = null;
                        ResetTextureForAll(p.level);
                        SaveLevelInfo(p.level);
                        return;
                    }

                    if (a == "123dmwm")
                    {
                        message = DmwmTextureUrl;
                        goto IL_AD;
                    }

                    if (a == "turtley3")
                    {
                        message = Turtley3Url;
                        goto IL_AD;
                    }
                }

                Player.SendMessage(p, "Unknown texture type: " + message);
                return;
            }

            IL_AD:
            var array = message.Split(new[]
            {
                ' '
            }, StringSplitOptions.RemoveEmptyEntries);
            var text2 = array[0];
            if (!HttpUtil.IsValidUrl(text2))
            {
                Player.SendMessage(p, "Invalid url: " + text2);
                return;
            }

            var th = new TextureHandler();
            var t = th.Parse(message);
            if (t == null)
            {
                Player.SendMessage(p, "Invalid texture arguments.");
                return;
            }

            var l = p.level;
            p.level.Info.Texture = message;
            Player.players.ForEachSync(delegate(Player pl)
            {
                if (pl.level == l) th.SendToPlayer(pl, t);
            });
            SaveLevelInfo(p.level);
        }

        // Token: 0x0600007B RID: 123 RVA: 0x00004EA8 File Offset: 0x000030A8
        private static void ResetTextureForAll(Level level)
        {
            var sideLevel = (short) (level.height / 2);
            Player.players.ForEachSync(delegate(Player p)
            {
                if (p.level == level && p.Cpe.EnvMapAppearance == 1)
                    V1.EnvSetMapAppearance(p, "", byte.MaxValue, byte.MaxValue, sideLevel);
            });
        }

        // Token: 0x0600007C RID: 124 RVA: 0x00004EEC File Offset: 0x000030EC
        private static void SaveLevelInfo(Level level)
        {
            var levelInfoManager = new LevelInfoManager();
            var levelInfoConverter = new LevelInfoConverter();
            var info = levelInfoConverter.ToRaw(level.Info);
            levelInfoManager.Save(level, info);
        }

        // Token: 0x0600007D RID: 125 RVA: 0x00004F1C File Offset: 0x0000311C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/texture [type] - sets a texture for this map.");
            Player.SendMessage(p, "Available types: default (no texture), 123dmwm, turtley3.");
            Player.SendMessage(p, "/texture [url] - sets the texture url, has to start with 'http'.");
            Player.SendMessage(p, "/texture [url] [sideBlock] [edgeBlock] [sideLevel]");
        }
    }
}