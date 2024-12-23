using System;
using MCDzienny.Cpe;
using MCDzienny.Levels.Effects;
using MCDzienny.Levels.Info;

namespace MCDzienny.Commands
{
    class CmdTexture : Command
    {
        static readonly string DmwmTextureUrl = "http://s28.postimg.org/6bpm90mwd/terrain.png";

        static readonly string Turtley3Url = "http://s12.postimg.org/ga3trp5yl/terrain.png";

        public override string name { get { return "texture"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            string text = message.ToLower();
            if (!text.StartsWith("http"))
            {
                switch (text)
                {
                    case "default":
                        p.level.Info.Texture = null;
                        ResetTextureForAll(p.level);
                        SaveLevelInfo(p.level);
                        return;
                    case "123dmwm":
                        message = DmwmTextureUrl;
                        break;
                    case "turtley3":
                        message = Turtley3Url;
                        break;
                    default:
                        Player.SendMessage(p, "Unknown texture type: " + message);
                        return;
                }
            }
            string[] array = message.Split(new char[1]
            {
                ' '
            }, StringSplitOptions.RemoveEmptyEntries);
            string text2 = array[0];
            if (!HttpUtil.IsValidUrl(text2))
            {
                Player.SendMessage(p, "Invalid url: " + text2);
                return;
            }
            TextureHandler th = new TextureHandler();
            Texture t = th.Parse(message);
            if (t == null)
            {
                Player.SendMessage(p, "Invalid texture arguments.");
                return;
            }
            Level l = p.level;
            p.level.Info.Texture = message;
            Player.players.ForEachSync(delegate(Player pl)
            {
                if (pl.level == l)
                {
                    th.SendToPlayer(pl, t);
                }
            });
            SaveLevelInfo(p.level);
        }

        static void ResetTextureForAll(Level level)
        {
            short sideLevel = (short)(level.height / 2);
            Player.players.ForEachSync(delegate(Player p)
            {
                if (p.level == level && p.Cpe.EnvMapAppearance == 1)
                {
                    V1.EnvSetMapAppearance(p, "", byte.MaxValue, byte.MaxValue, sideLevel);
                }
            });
        }

        static void SaveLevelInfo(Level level)
        {
            LevelInfoManager levelInfoManager = new LevelInfoManager();
            LevelInfoConverter levelInfoConverter = new LevelInfoConverter();
            LevelInfoRaw info = levelInfoConverter.ToRaw(level.Info);
            levelInfoManager.Save(level, info);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/texture [type] - sets a texture for this map.");
            Player.SendMessage(p, "Available types: default (no texture), 123dmwm, turtley3.");
            Player.SendMessage(p, "/texture [url] - sets the texture url, has to start with 'http'.");
            Player.SendMessage(p, "/texture [url] [sideBlock] [edgeBlock] [sideLevel]");
        }
    }
}