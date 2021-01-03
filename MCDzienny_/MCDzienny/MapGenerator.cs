using System;

namespace MCDzienny
{
    // Token: 0x02000351 RID: 849
    public class MapGenerator
    {
        // Token: 0x04000C99 RID: 3225
        private static bool Inuse;

        // Token: 0x04000C9A RID: 3226
        private static float[] terrain;

        // Token: 0x04000C9B RID: 3227
        private static float[] overlay;

        // Token: 0x04000C9C RID: 3228
        private static float[] overlay2;

        // Token: 0x04000C9D RID: 3229
        private static float divide;

        // Token: 0x06001844 RID: 6212 RVA: 0x000A3E4C File Offset: 0x000A204C
        public bool GenerateMap(Level lvl, string type)
        {
            var mapThemeType = MapThemeType.Flat;
            switch (type)
            {
                case "flat":
                    mapThemeType = MapThemeType.Flat;
                    break;
                case "ocean":
                    mapThemeType = MapThemeType.Ocean;
                    break;
                case "desert":
                    mapThemeType = MapThemeType.Desert;
                    break;
                case "forest":
                    mapThemeType = MapThemeType.Forest;
                    break;
                case "island":
                    mapThemeType = MapThemeType.Island;
                    break;
                case "pixel":
                    mapThemeType = MapThemeType.Pixel;
                    break;
                case "overlay":
                    mapThemeType = MapThemeType.Overlay;
                    break;
                case "mountains":
                    mapThemeType = MapThemeType.Mountains;
                    break;
                default:
                    return false;
            }

            return GenerateMap(lvl, mapThemeType);
        }

        // Token: 0x06001845 RID: 6213 RVA: 0x000A3F3C File Offset: 0x000A213C
        public bool GenerateMap(Level Lvl, MapThemeType theme)
        {
            Server.s.Log("Attempting map gen");
            if (Inuse)
            {
                Server.s.Log("Generator in use");
                return false;
            }

            var random = new Random();
            try
            {
                Inuse = true;
                terrain = new float[Lvl.width * Lvl.depth];
                overlay = new float[Lvl.width * Lvl.depth];
                if (theme != MapThemeType.Ocean) overlay2 = new float[Lvl.width * Lvl.depth];
                var num = (ushort) (Lvl.height / 2 + 2);
                if (theme == MapThemeType.Ocean) num = (ushort) (Lvl.height * 0.85f);
                GenerateFault(terrain, Lvl, theme, random);
                FilterAverage(Lvl);
                Server.s.Log("Creating overlay");
                GeneratePerlinNoise(overlay, Lvl, "", random);
                if (theme != MapThemeType.Ocean && theme != MapThemeType.Desert)
                {
                    Server.s.Log("Planning trees");
                    GeneratePerlinNoise(overlay2, Lvl, "", random);
                }

                Server.s.Log("Converting height map");
                Server.s.Log("And applying overlays");
                var num2 = 0.2f;
                var num3 = 0.8f;
                var num4 = 0.35f;
                short dist = 3;
                switch (theme)
                {
                    case MapThemeType.Island:
                        num2 = 0.4f;
                        num3 = 0.75f;
                        break;
                    case MapThemeType.Forest:
                        num2 = 0.45f;
                        num3 = 0.8f;
                        num4 = 0.7f;
                        dist = 2;
                        break;
                    case MapThemeType.Mountains:
                        num2 = 0.3f;
                        num3 = 0.9f;
                        dist = 4;
                        break;
                    case MapThemeType.Ocean:
                        num2 = 0.1f;
                        num3 = 0.6f;
                        break;
                    case MapThemeType.Desert:
                        num2 = 0.5f;
                        num3 = 0.85f;
                        num = 0;
                        dist = 24;
                        break;
                }

                for (var i = 0; i < terrain.Length; i++)
                {
                    var x = (ushort) (i % Lvl.width);
                    var num5 = (ushort) (i / Lvl.width);
                    var num6 = theme != MapThemeType.Island
                        ? Evaluate(Lvl, Range(terrain[i], num2, num3))
                        : Evaluate(Lvl,
                            Range(terrain[i], num2 - NegateEdge(x, num5, Lvl), num3 - NegateEdge(x, num5, Lvl)));
                    if (num6 > num)
                    {
                        ushort num7 = 0;
                        while (num6 - num7 >= 0)
                        {
                            if (theme == MapThemeType.Desert)
                                Lvl.skipChange(x, (ushort) (num6 - num7), num5, 12);
                            else if (overlay[i] < 0.72f)
                                switch (theme)
                                {
                                    case MapThemeType.Island:
                                        if (num6 > num + 2)
                                        {
                                            if (num7 == 0)
                                                Lvl.skipChange(x, (ushort) (num6 - num7), num5, 2);
                                            else if (num7 < 3)
                                                Lvl.skipChange(x, (ushort) (num6 - num7), num5, 3);
                                            else
                                                Lvl.skipChange(x, (ushort) (num6 - num7), num5, 1);
                                        }
                                        else
                                        {
                                            Lvl.skipChange(x, (ushort) (num6 - num7), num5, 12);
                                        }

                                        break;
                                    case MapThemeType.Desert:
                                        Lvl.skipChange(x, (ushort) (num6 - num7), num5, 12);
                                        break;
                                    default:
                                        if (num7 == 0)
                                            Lvl.skipChange(x, (ushort) (num6 - num7), num5, 2);
                                        else if (num7 < 3)
                                            Lvl.skipChange(x, (ushort) (num6 - num7), num5, 3);
                                        else
                                            Lvl.skipChange(x, (ushort) (num6 - num7), num5, 1);
                                        break;
                                }
                            else
                                Lvl.skipChange(x, (ushort) (num6 - num7), num5, 1);

                            num7 = (ushort) (num7 + 1);
                        }

                        if (overlay[i] < 0.25f && theme != MapThemeType.Desert)
                            switch (random.Next(12))
                            {
                                case 10:
                                    Lvl.skipChange(x, (ushort) (num6 + 1), num5, 38);
                                    break;
                                case 11:
                                    Lvl.skipChange(x, (ushort) (num6 + 1), num5, 37);
                                    break;
                            }

                        if (theme != MapThemeType.Ocean && overlay[i] < 0.65f && overlay2[i] < num4 &&
                            Lvl.GetTile(x, (ushort) (num6 + 1), num5) == 0 &&
                            (Lvl.GetTile(x, num6, num5) == 2 || theme == MapThemeType.Desert) && random.Next(13) == 0 &&
                            !TreeCheck(Lvl, x, num6, num5, dist))
                        {
                            if (theme == MapThemeType.Desert)
                                AddCactus(Lvl, x, (ushort) (num6 + 1), num5, random);
                            else
                                AddTree(Lvl, x, (ushort) (num6 + 1), num5, random);
                        }

                        continue;
                    }

                    ushort num8 = 0;
                    while (num - num8 >= 0)
                    {
                        if (num - num8 > num6)
                        {
                            Lvl.skipChange(x, (ushort) (num - num8), num5, 8);
                        }
                        else if (num - num8 > num6 - 3)
                        {
                            if (overlay[i] < 0.75f)
                                Lvl.skipChange(x, (ushort) (num - num8), num5, 12);
                            else
                                Lvl.skipChange(x, (ushort) (num - num8), num5, 13);
                        }
                        else
                        {
                            Lvl.skipChange(x, (ushort) (num - num8), num5, 1);
                        }

                        num8 = (ushort) (num8 + 1);
                    }
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
                Server.s.Log("Gen Fail");
                Inuse = false;
                return false;
            }

            terrain = new float[0];
            overlay = new float[0];
            overlay2 = new float[0];
            Inuse = false;
            return true;
        }

        // Token: 0x06001846 RID: 6214 RVA: 0x000A4484 File Offset: 0x000A2684
        private void GenerateFault(float[] array, Level Lvl, MapThemeType theme, Random rand)
        {
            var num = 0.5f;
            var num2 = 0.01f;
            var num3 = -0.0025f;
            switch (theme)
            {
                case MapThemeType.Mountains:
                    num2 = 0.02f;
                    num = 0.6f;
                    break;
                case MapThemeType.Overlay:
                    num2 = 0.02f;
                    num3 = -0.01f;
                    break;
            }

            for (var i = 0; i < array.Length; i++) array[i] = num;
            var num4 = 0f - num2;
            var num5 = num2;
            var num6 = (ushort) (Lvl.width / 2);
            var num7 = (ushort) (Lvl.depth / 2);
            var num8 = Lvl.width + Lvl.depth;
            Server.s.Log("Iterations = " + num8);
            for (ushort num9 = 0; num9 < num8; num9 = (ushort) (num9 + 1))
            {
                var num10 = (float) Math.Sqrt(num6 * num6 + num7 * num7);
                var num11 = (float) (rand.NextDouble() * 360.0);
                var num12 = (float) Math.Cos(num11);
                var num13 = (float) Math.Sin(num11);
                var num14 = (float) rand.NextDouble() * 2f * num10 - num10;
                for (ushort num15 = 0; num15 < Lvl.depth; num15 = (ushort) (num15 + 1))
                for (ushort num16 = 0; num16 < Lvl.width; num16 = (ushort) (num16 + 1))
                {
                    var height = !((num15 - num7) * num12 + (num16 - num6) * num13 + num14 > 0f) ? 0f - num5 : num5;
                    AddTerrainHeight(array, num16, num15, Lvl.width, height);
                }

                num5 += num3;
                if (num5 < num4) num5 = num2;
            }
        }

        // Token: 0x06001847 RID: 6215 RVA: 0x000A4600 File Offset: 0x000A2800
        private void GeneratePerlinNoise(float[] array, Level Lvl, string type, Random rand)
        {
            GenerateNormalized(array, 0.7f, 8, Lvl.width, Lvl.depth, rand.Next(), 64f);
        }

        // Token: 0x06001848 RID: 6216 RVA: 0x000A4628 File Offset: 0x000A2828
        private void GenerateNormalized(float[] array, float persistence, int octaves, int width, int height, int seed,
            float zoom)
        {
            var num = 0f;
            var num2 = 0f;
            for (var i = 0; i < height; i++)
            for (var j = 0; j < width; j++)
            {
                var num3 = 0f;
                var num4 = 1f;
                var num5 = 1f;
                for (var k = 0; k < octaves; k++)
                {
                    num3 += InterpolatedNoise(j * num4 / zoom, i * num4 / zoom, seed) * num5;
                    num4 *= 2f;
                    num5 *= persistence;
                }

                array[i * width + j] = num3;
                num = num3 < num ? num3 : num;
                num2 = num3 > num2 ? num3 : num2;
            }

            for (var l = 0; l < width * height; l++) array[l] = (array[l] - num) / (num2 - num);
        }

        // Token: 0x06001849 RID: 6217 RVA: 0x000A46F8 File Offset: 0x000A28F8
        private float Noise(int x, int y, int seed)
        {
            var num = x + y * 57 + seed;
            num = (num << 13) ^ num;
            return (float) (1.0 - ((num * (num * num * 15731 + 789221) + 1376312589) & int.MaxValue) / 1073741824.0);
        }

        // Token: 0x0600184A RID: 6218 RVA: 0x000A4748 File Offset: 0x000A2948
        private float SmoothNoise(int x, int y, int seed)
        {
            var num = (Noise(x - 1, y - 1, seed) + Noise(x + 1, y - 1, seed) + Noise(x - 1, y + 1, seed) +
                       Noise(x + 1, y + 1, seed)) / 16f;
            var num2 = Noise(x - 1, y, seed) + Noise(x + 1, y, seed) + Noise(x, y - 1, seed) +
                       Noise(x, y + 1, seed) / 8f;
            var num3 = Noise(x, y, seed) / 4f;
            return num + num2 + num3;
        }

        // Token: 0x0600184B RID: 6219 RVA: 0x000A47E0 File Offset: 0x000A29E0
        private float Interpolate(float a, float b, float x)
        {
            var num = x * 3.1415927f;
            var num2 = (float) (1.0 - Math.Cos(num)) * 0.5f;
            return a * (1f - num2) + b * num2;
        }

        // Token: 0x0600184C RID: 6220 RVA: 0x000A481C File Offset: 0x000A2A1C
        private float InterpolatedNoise(float x, float y, int seed)
        {
            var num = (int) x;
            var x2 = x - num;
            var num2 = (int) y;
            var x3 = y - num2;
            var a = SmoothNoise(num, num2, seed);
            var b = SmoothNoise(num + 1, num2, seed);
            var a2 = SmoothNoise(num, num2 + 1, seed);
            var b2 = SmoothNoise(num + 1, num2 + 1, seed);
            var a3 = Interpolate(a, b, x2);
            var b3 = Interpolate(a2, b2, x2);
            return Interpolate(a3, b3, x3);
        }

        // Token: 0x0600184D RID: 6221 RVA: 0x000A4894 File Offset: 0x000A2A94
        private void AddTree(Level Lvl, ushort x, ushort y, ushort z, Random Rand)
        {
            var b = (byte) Rand.Next(5, 8);
            for (ushort num = 0; num < b; num = (ushort) (num + 1)) Lvl.skipChange(x, (ushort) (y + num), z, 17);
            var num2 = (short) (b - Rand.Next(2, 4));
            for (var num3 = (short) -num2; num3 <= num2; num3 = (short) (num3 + 1))
            for (var num4 = (short) -num2; num4 <= num2; num4 = (short) (num4 + 1))
            for (var num5 = (short) -num2; num5 <= num2; num5 = (short) (num5 + 1))
            {
                var num6 = (short) Math.Sqrt(num3 * num3 + num4 * num4 + num5 * num5);
                if (num6 < num2 + 1 && Rand.Next(num6) < 2)
                    try
                    {
                        Lvl.skipChange((ushort) (x + num3), (ushort) (y + num4 + b), (ushort) (z + num5), 18);
                    }
                    catch
                    {
                    }
            }
        }

        // Token: 0x0600184E RID: 6222 RVA: 0x000A4964 File Offset: 0x000A2B64
        private void AddCactus(Level Lvl, ushort x, ushort y, ushort z, Random Rand)
        {
            var b = (byte) Rand.Next(3, 6);
            for (ushort num = 0; num <= b; num = (ushort) (num + 1)) Lvl.skipChange(x, (ushort) (y + num), z, 25);
            var num2 = 0;
            var num3 = 0;
            switch (Rand.Next(1, 3))
            {
                case 1:
                    num2 = -1;
                    break;
                default:
                    num3 = -1;
                    break;
            }

            for (ushort num = b; num <= Rand.Next(b + 2, b + 5); num = (ushort) (num + 1))
                Lvl.skipChange((ushort) (x + num2), (ushort) (y + num), (ushort) (z + num3), 25);
            for (ushort num = b; num <= Rand.Next(b + 2, b + 5); num = (ushort) (num + 1))
                Lvl.skipChange((ushort) (x - num2), (ushort) (y + num), (ushort) (z - num3), 25);
        }

        // Token: 0x0600184F RID: 6223 RVA: 0x000A4A1C File Offset: 0x000A2C1C
        private bool TreeCheck(Level Lvl, ushort x, ushort z, ushort y, short dist)
        {
            for (var num = (short) -dist; num <= dist; num = (short) (num + 1))
            for (var num2 = (short) -dist; num2 <= dist; num2 = (short) (num2 + 1))
            for (var num3 = (short) -dist; num3 <= dist; num3 = (short) (num3 + 1))
            {
                var tile = Lvl.GetTile((ushort) (x + num), (ushort) (z + num3), (ushort) (y + num2));
                if (tile == 17 || tile == 25) return true;
            }

            return false;
        }

        // Token: 0x06001850 RID: 6224 RVA: 0x000A4A80 File Offset: 0x000A2C80
        private void AddTerrainHeight(float[] array, ushort x, ushort y, ushort width, float height)
        {
            var num = x + y * width;
            if (num < 0) return;
            if (num > array.Length) return;
            array[num] += height;
            if (array[num] > 1f) array[num] = 1f;
            if (array[num] < 0f) array[num] = 0f;
        }

        // Token: 0x06001851 RID: 6225 RVA: 0x000A4ADC File Offset: 0x000A2CDC
        private ushort Evaluate(Level lvl, float height)
        {
            var num = (ushort) (height * lvl.height);
            if (num < 0) return 0;
            if (num > lvl.height - 1) return (ushort) (lvl.height - 1);
            return num;
        }

        // Token: 0x06001852 RID: 6226 RVA: 0x000A4B10 File Offset: 0x000A2D10
        private void FilterAverage(Level Lvl)
        {
            Server.s.Log("Applying average filtering");
            var array = new float[terrain.Length];
            for (var i = 0; i < terrain.Length; i++)
            {
                var x = (ushort) (i % Lvl.width);
                var y = (ushort) (i / Lvl.width);
                array[i] = GetAverage9(x, y, Lvl);
            }

            for (var j = 0; j < terrain.Length; j++) terrain[j] = array[j];
        }

        // Token: 0x06001853 RID: 6227 RVA: 0x000A4B90 File Offset: 0x000A2D90
        private float GetAverage5(ushort x, ushort y, Level Lvl)
        {
            divide = 0f;
            var pixel = GetPixel(x, y, Lvl);
            pixel += GetPixel((ushort) (x + 1), y, Lvl);
            pixel += GetPixel((ushort) (x - 1), y, Lvl);
            pixel += GetPixel(x, (ushort) (y + 1), Lvl);
            pixel += GetPixel(x, (ushort) (y - 1), Lvl);
            return pixel / divide;
        }

        // Token: 0x06001854 RID: 6228 RVA: 0x000A4BF4 File Offset: 0x000A2DF4
        private float GetAverage9(ushort x, ushort y, Level Lvl)
        {
            divide = 0f;
            var pixel = GetPixel(x, y, Lvl);
            pixel += GetPixel((ushort) (x + 1), y, Lvl);
            pixel += GetPixel((ushort) (x - 1), y, Lvl);
            pixel += GetPixel(x, (ushort) (y + 1), Lvl);
            pixel += GetPixel(x, (ushort) (y - 1), Lvl);
            pixel += GetPixel((ushort) (x + 1), (ushort) (y + 1), Lvl);
            pixel += GetPixel((ushort) (x - 1), (ushort) (y + 1), Lvl);
            pixel += GetPixel((ushort) (x + 1), (ushort) (y - 1), Lvl);
            pixel += GetPixel((ushort) (x - 1), (ushort) (y - 1), Lvl);
            return pixel / divide;
        }

        // Token: 0x06001855 RID: 6229 RVA: 0x000A4CA0 File Offset: 0x000A2EA0
        private float GetPixel(ushort x, ushort y, Level Lvl)
        {
            if (x < 0) return 0f;
            if (x >= Lvl.width) return 0f;
            if (y < 0) return 0f;
            if (y >= Lvl.depth) return 0f;
            divide += 1f;
            return terrain[x + y * Lvl.width];
        }

        // Token: 0x06001856 RID: 6230 RVA: 0x000A4D00 File Offset: 0x000A2F00
        private float Range(float input, float low, float high)
        {
            if (high <= low) return low;
            return low + input * (high - low);
        }

        // Token: 0x06001857 RID: 6231 RVA: 0x000A4D10 File Offset: 0x000A2F10
        private float NegateEdge(ushort x, ushort y, Level Lvl)
        {
            var num = 0f;
            var num2 = 0f;
            if (x != 0) num = x / (float) Lvl.width * 0.5f;
            if (y != 0) num2 = y / (float) Lvl.depth * 0.5f;
            num = Math.Abs(num - 0.25f);
            num2 = Math.Abs(num2 - 0.25f);
            float num3;
            if (num > num2)
                num3 = num - 0.15f;
            else
                num3 = num2 - 0.15f;
            if (num3 > 0f) return num3;
            return 0f;
        }
    }
}