using System;
using MCDzienny.Cpe;

namespace MCDzienny.Levels.Effects
{
    public class TextureHandler
    {
        public Texture Parse(string value)
        {
            if (value == null)
            {
                throw new NullReferenceException("value");
            }
            string[] array = value.Split(new char[1]
            {
                ' '
            }, StringSplitOptions.RemoveEmptyEntries);
            if (array.Length == 0)
            {
                return null;
            }
            string url = array[0];
            string text = "url";
            if (!HttpUtil.IsValidUrl(url))
            {
                throw new FormatException("url");
            }
            try
            {
                sbyte sideBlock = -1;
                if (array.Length > 1)
                {
                    text = "sideBlock";
                    sideBlock = sbyte.Parse(array[1]);
                }
                sbyte edgeBlock = -1;
                if (array.Length > 2)
                {
                    text = "edgeBlock";
                    edgeBlock = sbyte.Parse(array[2]);
                }
                short sideLevel = -1;
                if (array.Length > 3)
                {
                    text = "sideLevel";
                    sideLevel = short.Parse(array[3]);
                }
                return new Texture(url, sideBlock, edgeBlock, sideLevel);
            }
            catch (FormatException ex)
            {
                Server.ErrorLog("Texture: invalid value for " + text);
                Server.ErrorLog(ex);
            }
            catch (OverflowException ex2)
            {
                Server.ErrorLog("Texture: invalid value for " + text);
                Server.ErrorLog(ex2);
            }
            catch (Exception ex3)
            {
                Server.ErrorLog(ex3);
            }
            return null;
        }

        public void SendToPlayer(Player player, Texture texture)
        {
            if (player == null)
            {
                throw new NullReferenceException("player");
            }
            if (texture == null)
            {
                throw new NullReferenceException("texture");
            }
            if (player.Cpe.EnvMapAppearance == 1)
            {
                V1.EnvSetMapAppearance(sideBlock: (byte)(texture.SideBlock != -1 ? (byte)texture.SideBlock : 7),
                                       edgeBlock: (byte)(texture.EdgeBlock != -1 ? (byte)texture.EdgeBlock : 8),
                                       sideLevel: texture.SideLevel != -1 ? texture.SideLevel : (short)(player.level.height / 2), player: player, textureUrl: texture.Url);
            }
        }
    }
}