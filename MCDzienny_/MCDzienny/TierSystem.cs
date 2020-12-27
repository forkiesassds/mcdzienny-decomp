using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using MCDzienny.Settings;

namespace MCDzienny
{
    // Token: 0x02000240 RID: 576
    public class TierSystem
    {
        // Token: 0x040008A2 RID: 2210
        public static int[] tierTreshold =
        {
            500,
            1300,
            2350,
            3820,
            5878,
            8759,
            11928,
            15255,
            18748,
            22415,
            26448,
            30682,
            35128,
            39796,
            44697,
            49844,
            55248,
            60652,
            66056
        };

        // Token: 0x040008A3 RID: 2211
        public static int[] waterBlocsPerTier =
        {
            2,
            5,
            10,
            10,
            10,
            10,
            10,
            10,
            10,
            20,
            20,
            20,
            25,
            25,
            30,
            30,
            30,
            30,
            30,
            50
        };

        // Token: 0x040008A4 RID: 2212
        public static int[] hammerPerTier =
        {
            0,
            0,
            0,
            0,
            0,
            100,
            100,
            100,
            100,
            200,
            200,
            200,
            300,
            300,
            400,
            400,
            400,
            400,
            400,
            600
        };

        // Token: 0x040008A5 RID: 2213
        public static int[] spongeBlocksPerTier =
        {
            0,
            0,
            0,
            1,
            2,
            2,
            2,
            3,
            3,
            3,
            3,
            3,
            4,
            4,
            4,
            4,
            4,
            5,
            5,
            6
        };

        // Token: 0x040008A6 RID: 2214
        public static int[] doorBlocksPerTier =
        {
            0,
            0,
            0,
            0,
            0,
            4,
            4,
            4,
            4,
            6,
            6,
            6,
            6,
            8,
            8,
            8,
            10,
            10,
            10,
            12
        };

        // Token: 0x040008A7 RID: 2215
        private static List<Tier> tiers = new List<Tier>();

        // Token: 0x040008A8 RID: 2216
        private static readonly string filePath = "lava\\levelsystem.txt";

        // Token: 0x060010B6 RID: 4278 RVA: 0x00056A40 File Offset: 0x00054C40
        public static void InitTiers()
        {
            tierTreshold = new[]
            {
                500,
                1300,
                2350,
                3820,
                5878,
                8759,
                11928,
                15255,
                18748,
                22415,
                26448,
                30682,
                35128,
                39796,
                44697,
                49844,
                55248,
                60652,
                66056
            };
            waterBlocsPerTier = new[]
            {
                2,
                5,
                10,
                10,
                10,
                10,
                10,
                10,
                10,
                20,
                20,
                20,
                25,
                25,
                30,
                30,
                30,
                30,
                30,
                50
            };
            hammerPerTier = new[]
            {
                0,
                0,
                0,
                0,
                0,
                100,
                100,
                100,
                100,
                200,
                200,
                200,
                300,
                300,
                400,
                400,
                400,
                400,
                400,
                600
            };
            spongeBlocksPerTier = new[]
            {
                0,
                0,
                0,
                1,
                2,
                2,
                2,
                3,
                3,
                3,
                3,
                3,
                4,
                4,
                4,
                4,
                4,
                5,
                5,
                7
            };
            doorBlocksPerTier = new[]
            {
                0,
                0,
                0,
                0,
                0,
                4,
                4,
                4,
                4,
                6,
                6,
                6,
                6,
                8,
                8,
                8,
                10,
                10,
                10,
                12
            };
        }

        // Token: 0x060010B7 RID: 4279 RVA: 0x00056AC0 File Offset: 0x00054CC0
        public static void InitTierSystem()
        {
            LoadTiersXML();
            ListToArrays();
        }

        // Token: 0x060010B8 RID: 4280 RVA: 0x00056ACC File Offset: 0x00054CCC
        public static void GiveItems(Player p)
        {
            p.waterBlocks = waterBlocsPerTier[p.tier - 1];
            p.hammer = hammerPerTier[p.tier - 1];
            p.spongeBlocks = spongeBlocksPerTier[p.tier - 1];
            p.doorBlocks = doorBlocksPerTier[p.tier - 1];
        }

        // Token: 0x060010B9 RID: 4281 RVA: 0x00056B2C File Offset: 0x00054D2C
        public static void ColorSet(Player who)
        {
            if (LavaSettings.All.AutoNameColoring)
            {
                if (who.tier > 15)
                {
                    who.color = "&3";
                    return;
                }

                if (who.tier > 5)
                {
                    who.color = "&2";
                    return;
                }

                if (who.tier > 2) who.color = "&f";
            }
        }

        // Token: 0x060010BA RID: 4282 RVA: 0x00056B84 File Offset: 0x00054D84
        public static void ArraysToList()
        {
            var list = new List<Tier>();
            var item = new Tier
            {
                tier = 1,
                waterBlocks = waterBlocsPerTier[0],
                hammer = hammerPerTier[0],
                spongeBlocks = spongeBlocksPerTier[0],
                experience = 0,
                doorBlocks = doorBlocksPerTier[0]
            };
            list.Add(item);
            for (var i = 0; i < tierTreshold.Length; i++)
                list.Add(new Tier
                {
                    tier = i + 2,
                    experience = tierTreshold[i],
                    doorBlocks = doorBlocksPerTier[i + 1],
                    hammer = hammerPerTier[i + 1],
                    spongeBlocks = spongeBlocksPerTier[i + 1],
                    waterBlocks = waterBlocsPerTier[i + 1]
                });
            tiers = list;
        }

        // Token: 0x060010BB RID: 4283 RVA: 0x00056C68 File Offset: 0x00054E68
        public static void ListToArrays()
        {
            var tier = tiers[tiers.Count - 1].tier;
            tierTreshold = new int[tier - 1];
            waterBlocsPerTier = new int[tier];
            spongeBlocksPerTier = new int[tier];
            hammerPerTier = new int[tier];
            doorBlocksPerTier = new int[tier];
            for (var i = 0; i < tier; i++)
            {
                var tier2 = tiers[i].tier;
                if (tier2 != 1)
                {
                    tierTreshold[tier2 - 2] = tiers[i].experience;
                    waterBlocsPerTier[tier2 - 1] = tiers[i].waterBlocks;
                    spongeBlocksPerTier[tier2 - 1] = tiers[i].spongeBlocks;
                    hammerPerTier[tier2 - 1] = tiers[i].hammer;
                    doorBlocksPerTier[tier2 - 1] = tiers[i].doorBlocks;
                }
                else
                {
                    waterBlocsPerTier[tier2 - 1] = tiers[i].waterBlocks;
                    spongeBlocksPerTier[tier2 - 1] = tiers[i].spongeBlocks;
                    hammerPerTier[tier2 - 1] = tiers[i].hammer;
                    doorBlocksPerTier[tier2 - 1] = tiers[i].doorBlocks;
                }
            }

            var num = 0;
            for (var j = 0; j < tierTreshold.Length; j++)
            {
                if (tierTreshold[j] < num)
                {
                    Server.s.Log("Error: List of levels is corrupted at level: " + (j + 2) + ". Using default levels.");
                    InitTiers();
                    return;
                }

                num = tierTreshold[j];
            }
        }

        // Token: 0x060010BC RID: 4284 RVA: 0x00056E2C File Offset: 0x0005502C
        public static void TierSet(Player who)
        {
            who.tier = TierCheck(who.totalScore);
        }

        // Token: 0x060010BD RID: 4285 RVA: 0x00056E40 File Offset: 0x00055040
        public static int TierCheck(int exp)
        {
            for (var i = tierTreshold.Length - 1; i >= 0; i--)
                if (exp > tierTreshold[i])
                    return i + 2;
            return 1;
        }

        // Token: 0x060010BE RID: 4286 RVA: 0x00056E70 File Offset: 0x00055070
        private static int SortByTier(Tier x, Tier y)
        {
            if (x.tier > y.tier) return 1;
            if (x.tier < y.tier) return -1;
            return 0;
        }

        // Token: 0x060010BF RID: 4287 RVA: 0x00056E94 File Offset: 0x00055094
        public static void SortTiers()
        {
            tiers.RemoveAll(t => t.tier <= 0);
            tiers.RemoveAll(t => t.experience < 0);
            tiers.Sort(SortByTier);
        }

        // Token: 0x060010C0 RID: 4288 RVA: 0x00056F08 File Offset: 0x00055108
        public static void ReloadLevels()
        {
            tiers.Clear();
            LoadTiersXML();
        }

        // Token: 0x060010C1 RID: 4289 RVA: 0x00056F1C File Offset: 0x0005511C
        public static void LoadTiersXML()
        {
            if (File.Exists(filePath))
            {
                try
                {
                    var xmlDocument = new XmlDocument();
                    using (var inStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        xmlDocument.Load(inStream);
                    }

                    var elementsByTagName = xmlDocument.GetElementsByTagName("Level");
                    for (var i = 0; i < elementsByTagName.Count; i++)
                    {
                        var attributes = elementsByTagName[i].Attributes;
                        var tier = new Tier();
                        for (var j = 0; j < attributes.Count; j++)
                            switch (attributes[j].Name.ToLower())
                            {
                                case "level":
                                    try
                                    {
                                        tier.tier = int.Parse(attributes[j].Value);
                                        if (!int.TryParse(attributes[j].Value, out tier.tier)) tier.tier = -1;
                                    }
                                    catch
                                    {
                                        Server.s.Log("Wrong value of tier in tier's list.");
                                    }

                                    break;
                                case "water":
                                    try
                                    {
                                        tier.waterBlocks = int.Parse(attributes[j].Value);
                                    }
                                    catch
                                    {
                                        Server.s.Log("Wrong value of water in tier's list(tier=" + tier.tier +
                                                     "\", using default value.");
                                    }

                                    break;
                                case "hammer":
                                    try
                                    {
                                        tier.hammer = int.Parse(attributes[j].Value);
                                    }
                                    catch
                                    {
                                        Server.s.Log("Wrong value of hammer in tier's list(tier=" + tier.tier +
                                                     "\", using default value.");
                                    }

                                    break;
                                case "door":
                                    try
                                    {
                                        tier.doorBlocks = int.Parse(attributes[j].Value);
                                    }
                                    catch
                                    {
                                        Server.s.Log("Wrong value of door in tier's list(tier=" + tier.tier +
                                                     "\", using default value.");
                                    }

                                    break;
                                case "sponge":
                                    try
                                    {
                                        tier.spongeBlocks = int.Parse(attributes[j].Value);
                                    }
                                    catch
                                    {
                                        Server.s.Log("Wrong value of sponge in tier's list(tier=" + tier.tier +
                                                     "\", using default value.");
                                    }

                                    break;
                                case "experience":
                                    try
                                    {
                                        tier.experience = int.Parse(attributes[j].Value);
                                    }
                                    catch
                                    {
                                        Server.s.Log("Wrong value of experience in tier's list(tier=" + tier.tier +
                                                     "\", using default value.");
                                    }

                                    break;
                            }

                        tiers.Add(tier);
                    }
                }
                catch (Exception ex)
                {
                    Server.ErrorLog(ex);
                }

                SortTiers();
                SaveTiersXML();
            }
            else
            {
                ArraysToList();
                SaveTiersXML();
            }
        }

        // Token: 0x060010C2 RID: 4290 RVA: 0x000572EC File Offset: 0x000554EC
        public static void SaveTiersXML()
        {
            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", "yes"));
                xmlDocument.AppendChild(xmlDocument.CreateWhitespace("\r\n"));
                xmlDocument.AppendChild(xmlDocument.CreateComment(
                    "You can add as many tiers as you wish, but minimum number is 10. Also tier 1 always requires 0 experience."));
                xmlDocument.AppendChild(xmlDocument.CreateWhitespace("\r\n"));
                var xmlElement = xmlDocument.CreateElement("Levels");
                for (var i = 0; i < tiers.Count; i++)
                {
                    var xmlElement2 = xmlDocument.CreateElement("Level");
                    var xmlAttribute = xmlDocument.CreateAttribute("level");
                    var xmlAttribute2 = xmlDocument.CreateAttribute("experience");
                    var xmlAttribute3 = xmlDocument.CreateAttribute("water");
                    var xmlAttribute4 = xmlDocument.CreateAttribute("sponge");
                    var xmlAttribute5 = xmlDocument.CreateAttribute("door");
                    var xmlAttribute6 = xmlDocument.CreateAttribute("hammer");
                    xmlAttribute.Value = tiers[i].tier.ToString();
                    xmlAttribute2.Value = tiers[i].experience.ToString();
                    xmlAttribute3.Value = tiers[i].waterBlocks.ToString();
                    xmlAttribute6.Value = tiers[i].hammer.ToString();
                    xmlAttribute4.Value = tiers[i].spongeBlocks.ToString();
                    xmlAttribute5.Value = tiers[i].doorBlocks.ToString();
                    xmlElement2.SetAttributeNode(xmlAttribute);
                    xmlElement2.SetAttributeNode(xmlAttribute2);
                    xmlElement2.SetAttributeNode(xmlAttribute3);
                    xmlElement2.SetAttributeNode(xmlAttribute6);
                    xmlElement2.SetAttributeNode(xmlAttribute5);
                    xmlElement2.SetAttributeNode(xmlAttribute4);
                    xmlElement.AppendChild(xmlElement2);
                }

                var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                xmlDocument.AppendChild(xmlElement);
                xmlDocument.Save(fileStream);
                fileStream.Close();
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x02000241 RID: 577
        private class Tier
        {
            // Token: 0x040008AE RID: 2222
            public int doorBlocks;

            // Token: 0x040008B0 RID: 2224
            public int experience;

            // Token: 0x040008AF RID: 2223
            public int hammer;

            // Token: 0x040008AD RID: 2221
            public int spongeBlocks;

            // Token: 0x040008AB RID: 2219
            public int tier;

            // Token: 0x040008AC RID: 2220
            public int waterBlocks;
        }
    }
}