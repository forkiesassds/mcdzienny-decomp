using System;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace MCDzienny
{
    public static class ConvertDat
    {
        public static Level Load(Stream lvlStream, string fileName)
        {
            byte[] array = new byte[8];
            Level level = new Level(fileName, 0, 0, 0, "empty");
            try
            {
                lvlStream.Seek(-4L, SeekOrigin.End);
                lvlStream.Read(array, 0, 4);
                lvlStream.Seek(0L, SeekOrigin.Begin);
                int num = BitConverter.ToInt32(array, 0);
                byte[] array2 = new byte[num];
                using (GZipStream gZipStream = new GZipStream(lvlStream, CompressionMode.Decompress, leaveOpen: true))
                {
                    gZipStream.Read(array2, 0, num);
                }
                for (int i = 0; i < num - 1; i++)
                {
                    if (array2[i] != 172 || array2[i + 1] != 237)
                    {
                        continue;
                    }
                    int num2 = i + 6;
                    Array.Copy(array2, num2, array, 0, 2);
                    num2 += IPAddress.HostToNetworkOrder(BitConverter.ToInt16(array, 0));
                    num2 += 13;
                    int num3 = 0;
                    for (num3 = num2; num3 < array2.Length - 1; num3++)
                    {
                        if (array2[num3] == 120 && array2[num3 + 1] == 112)
                        {
                            num3 += 2;
                            break;
                        }
                    }
                    int num4 = 0;
                    while (num2 < num3)
                    {
                        if (array2[num2] == 90)
                        {
                            num4++;
                        }
                        else if (array2[num2] == 73 || array2[num2] == 70)
                        {
                            num4 += 4;
                        }
                        else if (array2[num2] == 74)
                        {
                            num4 += 8;
                        }
                        num2++;
                        Array.Copy(array2, num2, array, 0, 2);
                        short num5 = IPAddress.HostToNetworkOrder(BitConverter.ToInt16(array, 0));
                        num2 += 2;
                        Array.Copy(array2, num3 + num4 - 4, array, 0, 4);
                        if (MemCmp(array2, num2, "width"))
                        {
                            level.width = (ushort)IPAddress.HostToNetworkOrder(BitConverter.ToInt32(array, 0));
                        }
                        else if (MemCmp(array2, num2, "depth"))
                        {
                            level.height = (ushort)IPAddress.HostToNetworkOrder(BitConverter.ToInt32(array, 0));
                        }
                        else if (MemCmp(array2, num2, "height"))
                        {
                            level.depth = (ushort)IPAddress.HostToNetworkOrder(BitConverter.ToInt32(array, 0));
                        }
                        num2 += num5;
                    }
                    level.spawnx = (ushort)(level.width / 1.3);
                    level.spawny = (ushort)(level.height / 1.3);
                    level.spawnz = (ushort)(level.depth / 1.3);
                    bool flag = false;
                    num4 = Array.IndexOf(array2, (byte)0, num3);
                    while (num4 != -1 && num4 < array2.Length - 2)
                    {
                        if (array2[num4] == 0 && array2[num4 + 1] == 120 && array2[num4 + 2] == 112)
                        {
                            flag = true;
                            num2 = num4 + 7;
                        }
                        num4 = Array.IndexOf(array2, (byte)0, num4 + 1);
                    }
                    if (flag)
                    {
                        level.CopyBlocks(array2, num2);
                        level.Save(Override: true);
                        break;
                    }
                    throw new Exception("Could not locate block array.");
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

        static bool MemCmp(byte[] data, int offset, string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (offset + i >= data.Length || data[offset + i] != value[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}