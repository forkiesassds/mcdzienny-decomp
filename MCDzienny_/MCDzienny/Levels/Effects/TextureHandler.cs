using System;
using MCDzienny.Cpe;

namespace MCDzienny.Levels.Effects
{
    // Token: 0x02000036 RID: 54
    public class TextureHandler
    {
        // Token: 0x06000137 RID: 311 RVA: 0x000082DC File Offset: 0x000064DC
        public Texture Parse(string value)
        {
            if (value == null) throw new NullReferenceException("value");
            var array = value.Split(new[]
            {
                ' '
            }, StringSplitOptions.RemoveEmptyEntries);
            if (array.Length == 0) return null;
            var url = array[0];
            var str = "url";
            if (!HttpUtil.IsValidUrl(url)) throw new FormatException("url");
            try
            {
                sbyte sideBlock = -1;
                if (array.Length > 1)
                {
                    str = "sideBlock";
                    sideBlock = sbyte.Parse(array[1]);
                }

                sbyte edgeBlock = -1;
                if (array.Length > 2)
                {
                    str = "edgeBlock";
                    edgeBlock = sbyte.Parse(array[2]);
                }

                short sideLevel = -1;
                if (array.Length > 3)
                {
                    str = "sideLevel";
                    sideLevel = short.Parse(array[3]);
                }

                return new Texture(url, sideBlock, edgeBlock, sideLevel);
            }
            catch (FormatException ex)
            {
                Server.ErrorLog("Texture: invalid value for " + str);
                Server.ErrorLog(ex);
            }
            catch (OverflowException ex2)
            {
                Server.ErrorLog("Texture: invalid value for " + str);
                Server.ErrorLog(ex2);
            }
            catch (Exception ex3)
            {
                Server.ErrorLog(ex3);
            }

            return null;
        }

        // Token: 0x06000138 RID: 312 RVA: 0x000083F8 File Offset: 0x000065F8
        public void SendToPlayer(Player player, Texture texture)
        {
            if (player == null) throw new NullReferenceException("player");
            if (texture == null) throw new NullReferenceException("texture");
            if (player.Cpe.EnvMapAppearance == 1)
            {
                byte sideBlock;
                if (texture.SideBlock == -1)
                    sideBlock = 7;
                else
                    sideBlock = (byte) texture.SideBlock;
                byte edgeBlock;
                if (texture.EdgeBlock == -1)
                    edgeBlock = 8;
                else
                    edgeBlock = (byte) texture.EdgeBlock;
                short sideLevel;
                if (texture.SideLevel == -1)
                    sideLevel = (short) (player.level.height / 2);
                else
                    sideLevel = texture.SideLevel;
                V1.EnvSetMapAppearance(player, texture.Url, sideBlock, edgeBlock, sideLevel);
            }
        }
    }
}