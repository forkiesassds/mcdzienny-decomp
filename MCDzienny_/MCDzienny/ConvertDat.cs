using System;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace MCDzienny
{
    // Token: 0x02000342 RID: 834
    public static class ConvertDat
    {
        // Token: 0x06001807 RID: 6151 RVA: 0x000A0CC0 File Offset: 0x0009EEC0
        public static Level Load(Stream lvlStream, string fileName)
        {
            var array = new byte[8];
            var level = new Level(fileName, 0, 0, 0, "empty");
            try
            {
                lvlStream.Seek(-4L, SeekOrigin.End);
                lvlStream.Read(array, 0, 4);
                lvlStream.Seek(0L, SeekOrigin.Begin);
                var num = BitConverter.ToInt32(array, 0);
                var array2 = new byte[num];
                using (var gzipStream = new GZipStream(lvlStream, CompressionMode.Decompress, true))
                {
                    gzipStream.Read(array2, 0, num);
                }

                var i = 0;
                while (i < num - 1)
                    if (array2[i] == 172 && array2[i + 1] == 237)
                    {
                        var j = i + 6;
                        Array.Copy(array2, j, array, 0, 2);
                        j += IPAddress.HostToNetworkOrder(BitConverter.ToInt16(array, 0));
                        j += 13;
                        int k;
                        for (k = j; k < array2.Length - 1; k++)
                            if (array2[k] == 120 && array2[k + 1] == 112)
                            {
                                k += 2;
                                break;
                            }

                        var num2 = 0;
                        while (j < k)
                        {
                            if (array2[j] == 90)
                                num2++;
                            else if (array2[j] == 73 || array2[j] == 70)
                                num2 += 4;
                            else if (array2[j] == 74) num2 += 8;
                            j++;
                            Array.Copy(array2, j, array, 0, 2);
                            var num3 = IPAddress.HostToNetworkOrder(BitConverter.ToInt16(array, 0));
                            j += 2;
                            Array.Copy(array2, k + num2 - 4, array, 0, 4);
                            if (MemCmp(array2, j, "width"))
                                level.width = (ushort) IPAddress.HostToNetworkOrder(BitConverter.ToInt32(array, 0));
                            else if (MemCmp(array2, j, "depth"))
                                level.height = (ushort) IPAddress.HostToNetworkOrder(BitConverter.ToInt32(array, 0));
                            else if (MemCmp(array2, j, "height"))
                                level.depth = (ushort) IPAddress.HostToNetworkOrder(BitConverter.ToInt32(array, 0));
                            j += num3;
                        }

                        level.spawnx = (ushort) (level.width / 1.3);
                        level.spawny = (ushort) (level.height / 1.3);
                        level.spawnz = (ushort) (level.depth / 1.3);
                        var flag = false;
                        num2 = Array.IndexOf<byte>(array2, 0, k);
                        while (num2 != -1 && num2 < array2.Length - 2)
                        {
                            if (array2[num2] == 0 && array2[num2 + 1] == 120 && array2[num2 + 2] == 112)
                            {
                                flag = true;
                                j = num2 + 7;
                            }

                            num2 = Array.IndexOf<byte>(array2, 0, num2 + 1);
                        }

                        if (flag)
                        {
                            level.CopyBlocks(array2, j);
                            level.Save(true);
                            break;
                        }

                        throw new Exception("Could not locate block array.");
                    }
                    else
                    {
                        i++;
                    }
            }
            catch (Exception ex)
            {
                Server.s.Log("Conversion failed");
                Server.ErrorLog(ex);
                return null;
            }

            return level;
        }

        // Token: 0x06001808 RID: 6152 RVA: 0x000A0FC4 File Offset: 0x0009F1C4
        private static bool MemCmp(byte[] data, int offset, string value)
        {
            for (var i = 0; i < value.Length; i++)
                if (offset + i >= data.Length || (char) data[offset + i] != value[i])
                    return false;
            return true;
        }
    }
}