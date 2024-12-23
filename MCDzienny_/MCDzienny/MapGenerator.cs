using System;

namespace MCDzienny
{
    public class MapGenerator
    {
        static bool Inuse;

        static float[] terrain;

        static float[] overlay;

        static float[] overlay2;

        static float divide;

        public bool GenerateMap(Level lvl, string type)
        {
            MapThemeType mapThemeType = MapThemeType.Flat;
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

        public bool GenerateMap(Level Lvl, MapThemeType theme)
        {
            Server.s.Log("Attempting map gen");
            if (Inuse)
            {
                Server.s.Log("Generator in use");
                return false;
            }
            Random random = new Random();
            try
            {
                Inuse = true;
                terrain = new float[Lvl.width * Lvl.depth];
                overlay = new float[Lvl.width * Lvl.depth];
                if (theme != MapThemeType.Ocean)
                {
                    overlay2 = new float[Lvl.width * Lvl.depth];
                }
                ushort num = (ushort)(Lvl.height / 2 + 2);
                if (theme == MapThemeType.Ocean)
                {
                    num = (ushort)(Lvl.height * 0.85f);
                }
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
                float num2 = 0.2f;
                float num3 = 0.8f;
                float num4 = 0.35f;
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
                for (int i = 0; i < terrain.Length; i++)
                {
                    ushort x = (ushort)(i % Lvl.width);
                    ushort num5 = (ushort)(i / Lvl.width);
                    ushort num6 = theme != MapThemeType.Island ? Evaluate(Lvl, Range(terrain[i], num2, num3))
                        : Evaluate(Lvl, Range(terrain[i], num2 - NegateEdge(x, num5, Lvl), num3 - NegateEdge(x, num5, Lvl)));
                    if (num6 > num)
                    {
                        ushort num7 = 0;
                        while (num6 - num7 >= 0)
                        {
                            if (theme == MapThemeType.Desert)
                            {
                                Lvl.skipChange(x, (ushort)(num6 - num7), num5, 12);
                            }
                            else if (overlay[i] < 0.72f)
                            {
                                switch (theme)
                                {
                                    case MapThemeType.Island:
                                        if (num6 > num + 2)
                                        {
                                            if (num7 == 0)
                                            {
                                                Lvl.skipChange(x, (ushort)(num6 - num7), num5, 2);
                                            }
                                            else if (num7 < 3)
                                            {
                                                Lvl.skipChange(x, (ushort)(num6 - num7), num5, 3);
                                            }
                                            else
                                            {
                                                Lvl.skipChange(x, (ushort)(num6 - num7), num5, 1);
                                            }
                                        }
                                        else
                                        {
                                            Lvl.skipChange(x, (ushort)(num6 - num7), num5, 12);
                                        }
                                        break;
                                    case MapThemeType.Desert:
                                        Lvl.skipChange(x, (ushort)(num6 - num7), num5, 12);
                                        break;
                                    default:
                                        if (num7 == 0)
                                        {
                                            Lvl.skipChange(x, (ushort)(num6 - num7), num5, 2);
                                        }
                                        else if (num7 < 3)
                                        {
                                            Lvl.skipChange(x, (ushort)(num6 - num7), num5, 3);
                                        }
                                        else
                                        {
                                            Lvl.skipChange(x, (ushort)(num6 - num7), num5, 1);
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                Lvl.skipChange(x, (ushort)(num6 - num7), num5, 1);
                            }
                            num7++;
                        }
                        if (overlay[i] < 0.25f && theme != MapThemeType.Desert)
                        {
                            switch (random.Next(12))
                            {
                                case 10:
                                    Lvl.skipChange(x, (ushort)(num6 + 1), num5, 38);
                                    break;
                                case 11:
                                    Lvl.skipChange(x, (ushort)(num6 + 1), num5, 37);
                                    break;
                            }
                        }
                        if (theme != MapThemeType.Ocean && overlay[i] < 0.65f && overlay2[i] < num4 && Lvl.GetTile(x, (ushort)(num6 + 1), num5) == 0 &&
                            (Lvl.GetTile(x, num6, num5) == 2 || theme == MapThemeType.Desert) && random.Next(13) == 0 && !TreeCheck(Lvl, x, num6, num5, dist))
                        {
                            if (theme == MapThemeType.Desert)
                            {
                                AddCactus(Lvl, x, (ushort)(num6 + 1), num5, random);
                            }
                            else
                            {
                                AddTree(Lvl, x, (ushort)(num6 + 1), num5, random);
                            }
                        }
                        continue;
                    }
                    ushort num8 = 0;
                    while (num - num8 >= 0)
                    {
                        if (num - num8 > num6)
                        {
                            Lvl.skipChange(x, (ushort)(num - num8), num5, 8);
                        }
                        else if (num - num8 > num6 - 3)
                        {
                            if (overlay[i] < 0.75f)
                            {
                                Lvl.skipChange(x, (ushort)(num - num8), num5, 12);
                            }
                            else
                            {
                                Lvl.skipChange(x, (ushort)(num - num8), num5, 13);
                            }
                        }
                        else
                        {
                            Lvl.skipChange(x, (ushort)(num - num8), num5, 1);
                        }
                        num8++;
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

        void GenerateFault(float[] array, Level Lvl, MapThemeType theme, Random rand)
        {
            float num = 0.5f;
            float num2 = 0.01f;
            float num3 = -0.0025f;
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
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = num;
            }
            float num4 = 0f - num2;
            float num5 = num2;
            ushort num6 = (ushort)(Lvl.width / 2);
            ushort num7 = (ushort)(Lvl.depth / 2);
            int num8 = Lvl.width + Lvl.depth;
            Server.s.Log("Iterations = " + num8);
            for (ushort num9 = 0; num9 < num8; num9++)
            {
                float num10 = (float)Math.Sqrt(num6 * num6 + num7 * num7);
                float num11 = (float)(rand.NextDouble() * 360.0);
                float num12 = (float)Math.Cos(num11);
                float num13 = (float)Math.Sin(num11);
                float num14 = (float)rand.NextDouble() * 2f * num10 - num10;
                for (ushort num15 = 0; num15 < Lvl.depth; num15++)
                {
                    for (ushort num16 = 0; num16 < Lvl.width; num16++)
                    {
                        float height = !((num15 - num7) * num12 + (num16 - num6) * num13 + num14 > 0f) ? 0f - num5 : num5;
                        AddTerrainHeight(array, num16, num15, Lvl.width, height);
                    }
                }
                num5 += num3;
                if (num5 < num4)
                {
                    num5 = num2;
                }
            }
        }

        void GeneratePerlinNoise(float[] array, Level Lvl, string type, Random rand)
        {
            GenerateNormalized(array, 0.7f, 8, Lvl.width, Lvl.depth, rand.Next(), 64f);
        }

        void GenerateNormalized(float[] array, float persistence, int octaves, int width, int height, int seed, float zoom)
        {
            float num = 0f;
            float num2 = 0f;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    float num3 = 0f;
                    float num4 = 1f;
                    float num5 = 1f;
                    for (int k = 0; k < octaves; k++)
                    {
                        num3 += InterpolatedNoise(j * num4 / zoom, i * num4 / zoom, seed) * num5;
                        num4 *= 2f;
                        num5 *= persistence;
                    }
                    array[i * width + j] = num3;
                    num = num3 < num ? num3 : num;
                    num2 = num3 > num2 ? num3 : num2;
                }
            }
            for (int l = 0; l < width * height; l++)
            {
                array[l] = (array[l] - num) / (num2 - num);
            }
        }

        float Noise(int x, int y, int seed)
        {
            int num = x + y * 57 + seed;
            num = num << 13 ^ num;
            return (float)(1.0 - (num * (num * num * 15731 + 789221) + 1376312589 & 0x7FFFFFFF) / 1073741824.0);
        }

        float SmoothNoise(int x, int y, int seed)
        {
            float num = (Noise(x - 1, y - 1, seed) + Noise(x + 1, y - 1, seed) + Noise(x - 1, y + 1, seed) + Noise(x + 1, y + 1, seed)) / 16f;
            float num2 = Noise(x - 1, y, seed) + Noise(x + 1, y, seed) + Noise(x, y - 1, seed) + Noise(x, y + 1, seed) / 8f;
            float num3 = Noise(x, y, seed) / 4f;
            return num + num2 + num3;
        }

        float Interpolate(float a, float b, float x)
        {
            float num = x * (float)Math.PI;
            float num2 = (float)(1.0 - Math.Cos(num)) * 0.5f;
            return a * (1f - num2) + b * num2;
        }

        float InterpolatedNoise(float x, float y, int seed)
        {
            int num = (int)x;
            float x2 = x - num;
            int num2 = (int)y;
            float x3 = y - num2;
            float a = SmoothNoise(num, num2, seed);
            float b = SmoothNoise(num + 1, num2, seed);
            float a2 = SmoothNoise(num, num2 + 1, seed);
            float b2 = SmoothNoise(num + 1, num2 + 1, seed);
            float a3 = Interpolate(a, b, x2);
            float b3 = Interpolate(a2, b2, x2);
            return Interpolate(a3, b3, x3);
        }

        void AddTree(Level Lvl, ushort x, ushort y, ushort z, Random Rand)
        {
            byte b = (byte)Rand.Next(5, 8);
            for (ushort num = 0; num < b; num++)
            {
                Lvl.skipChange(x, (ushort)(y + num), z, 17);
            }
            short num2 = (short)(b - Rand.Next(2, 4));
            for (short num3 = (short)-num2; num3 <= num2; num3++)
            {
                for (short num4 = (short)-num2; num4 <= num2; num4++)
                {
                    for (short num5 = (short)-num2; num5 <= num2; num5++)
                    {
                        short num6 = (short)Math.Sqrt(num3 * num3 + num4 * num4 + num5 * num5);
                        if (num6 < num2 + 1 && Rand.Next(num6) < 2)
                        {
                            try
                            {
                                Lvl.skipChange((ushort)(x + num3), (ushort)(y + num4 + b), (ushort)(z + num5), 18);
                            }
                            catch {}
                        }
                    }
                }
            }
        }

        void AddCactus(Level Lvl, ushort x, ushort y, ushort z, Random Rand)
        {
            byte b = (byte)Rand.Next(3, 6);
            for (ushort num = 0; num <= b; num++)
            {
                Lvl.skipChange(x, (ushort)(y + num), z, 25);
            }
            int num2 = 0;
            int num3 = 0;
            switch (Rand.Next(1, 3))
            {
                case 1:
                    num2 = -1;
                    break;
                default:
                    num3 = -1;
                    break;
            }
            for (ushort num = b; num <= Rand.Next(b + 2, b + 5); num++)
            {
                Lvl.skipChange((ushort)(x + num2), (ushort)(y + num), (ushort)(z + num3), 25);
            }
            for (ushort num = b; num <= Rand.Next(b + 2, b + 5); num++)
            {
                Lvl.skipChange((ushort)(x - num2), (ushort)(y + num), (ushort)(z - num3), 25);
            }
        }

        bool TreeCheck(Level Lvl, ushort x, ushort z, ushort y, short dist)
        {
            for (short num = (short)-dist; num <= dist; num++)
            {
                for (short num2 = (short)-dist; num2 <= dist; num2++)
                {
                    for (short num3 = (short)-dist; num3 <= dist; num3++)
                    {
                        byte tile = Lvl.GetTile((ushort)(x + num), (ushort)(z + num3), (ushort)(y + num2));
                        if (tile == 17 || tile == 25)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        void AddTerrainHeight(float[] array, ushort x, ushort y, ushort width, float height)
        {
            int num = x + y * width;
            if (num >= 0 && num <= array.Length)
            {
                array[num] += height;
                if (array[num] > 1f)
                {
                    array[num] = 1f;
                }
                if (array[num] < 0f)
                {
                    array[num] = 0f;
                }
            }
        }

        ushort Evaluate(Level lvl, float height)
        {
            ushort num = (ushort)(height * lvl.height);
            if (num < 0)
            {
                return 0;
            }
            if (num > lvl.height - 1)
            {
                return (ushort)(lvl.height - 1);
            }
            return num;
        }

        void FilterAverage(Level Lvl)
        {
            Server.s.Log("Applying average filtering");
            float[] array = new float[terrain.Length];
            for (int i = 0; i < terrain.Length; i++)
            {
                ushort x = (ushort)(i % Lvl.width);
                ushort y = (ushort)(i / Lvl.width);
                array[i] = GetAverage9(x, y, Lvl);
            }
            for (int j = 0; j < terrain.Length; j++)
            {
                terrain[j] = array[j];
            }
        }

        float GetAverage5(ushort x, ushort y, Level Lvl)
        {
            divide = 0f;
            float pixel = GetPixel(x, y, Lvl);
            pixel += GetPixel((ushort)(x + 1), y, Lvl);
            pixel += GetPixel((ushort)(x - 1), y, Lvl);
            pixel += GetPixel(x, (ushort)(y + 1), Lvl);
            pixel += GetPixel(x, (ushort)(y - 1), Lvl);
            return pixel / divide;
        }

        float GetAverage9(ushort x, ushort y, Level Lvl)
        {
            divide = 0f;
            float pixel = GetPixel(x, y, Lvl);
            pixel += GetPixel((ushort)(x + 1), y, Lvl);
            pixel += GetPixel((ushort)(x - 1), y, Lvl);
            pixel += GetPixel(x, (ushort)(y + 1), Lvl);
            pixel += GetPixel(x, (ushort)(y - 1), Lvl);
            pixel += GetPixel((ushort)(x + 1), (ushort)(y + 1), Lvl);
            pixel += GetPixel((ushort)(x - 1), (ushort)(y + 1), Lvl);
            pixel += GetPixel((ushort)(x + 1), (ushort)(y - 1), Lvl);
            pixel += GetPixel((ushort)(x - 1), (ushort)(y - 1), Lvl);
            return pixel / divide;
        }

        float GetPixel(ushort x, ushort y, Level Lvl)
        {
            if (x < 0)
            {
                return 0f;
            }
            if (x >= Lvl.width)
            {
                return 0f;
            }
            if (y < 0)
            {
                return 0f;
            }
            if (y >= Lvl.depth)
            {
                return 0f;
            }
            divide += 1f;
            return terrain[x + y * Lvl.width];
        }

        float Range(float input, float low, float high)
        {
            if (high <= low)
            {
                return low;
            }
            return low + input * (high - low);
        }

        float NegateEdge(ushort x, ushort y, Level Lvl)
        {
            float num = 0f;
            float num2 = 0f;
            if (x != 0)
            {
                num = x / (float)Lvl.width * 0.5f;
            }
            if (y != 0)
            {
                num2 = y / (float)Lvl.depth * 0.5f;
            }
            num = Math.Abs(num - 0.25f);
            num2 = Math.Abs(num2 - 0.25f);
            float num3 = !(num > num2) ? num2 - 0.15f : num - 0.15f;
            if (num3 > 0f)
            {
                return num3;
            }
            return 0f;
        }
    }
}