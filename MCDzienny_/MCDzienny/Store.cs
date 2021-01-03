using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using MCDzienny.MultiMessages;
using MCDzienny.Settings;

namespace MCDzienny
{
    // Token: 0x02000392 RID: 914
    internal class Store
    {
        // Token: 0x04000E49 RID: 3657
        private static int firstPageLimit = 7;

        // Token: 0x04000E4A RID: 3658
        private static int listedItemsCount;

        // Token: 0x04000E4B RID: 3659
        public static Item life = new Item
        {
            id = 0,
            name = "life",
            position = 1,
            price = 10,
            amount = 1,
            description = " - gives you one life,"
        };

        // Token: 0x04000E4C RID: 3660
        public static Item armor = new Item
        {
            id = 1,
            name = "armor",
            position = 2,
            price = 30,
            amount = 1,
            description = " - gives you full protection from lava for 45seconds,"
        };

        // Token: 0x04000E4D RID: 3661
        public static Item water = new Item
        {
            id = 2,
            name = "water",
            position = 3,
            price = 15,
            amount = 20,
            description = " - to use write /water and place any block,"
        };

        // Token: 0x04000E4E RID: 3662
        public static Item sponge = new Item
        {
            id = 3,
            name = "sponge",
            position = 4,
            price = 20,
            amount = 5,
            description = " - removes lava around, dissapears quickly,"
        };

        // Token: 0x04000E4F RID: 3663
        public static Item hammer = new Item
        {
            id = 4,
            name = "hammer",
            position = 5,
            price = 20,
            amount = 100,
            description = " - acts like cuboid, write /hammer to use it,"
        };

        // Token: 0x04000E50 RID: 3664
        public static Item door = new Item
        {
            id = 5,
            name = "door",
            position = 6,
            price = 20,
            amount = 6,
            description = " - use /door command to create steel doors,"
        };

        // Token: 0x04000E51 RID: 3665
        public static Item teleport = new Item
        {
            id = 8,
            name = "teleport",
            position = 7,
            price = 20,
            amount = 1,
            description = " - use /tp [player] to teleport to the player,"
        };

        // Token: 0x04000E52 RID: 3666
        public static Item color = new Item
        {
            id = 11,
            name = "color",
            position = 8,
            price = 280,
            amount = 1,
            description = " - use /color [color] to set your name color,"
        };

        // Token: 0x04000E53 RID: 3667
        public static Item title = new Item
        {
            id = 6,
            name = "title",
            position = 9,
            price = 280,
            amount = 1,
            description = " - use /title [your title] to set a new title,"
        };

        // Token: 0x04000E54 RID: 3668
        public static Item titleColor = new Item
        {
            id = 12,
            name = "titlecolor",
            position = 10,
            price = 200,
            amount = 1,
            description = " - use /titlecolor [color] to set your title color,"
        };

        // Token: 0x04000E55 RID: 3669
        public static Item promotion = new Item
        {
            id = 7,
            name = "promotion",
            position = 11,
            amount = 1
        };

        // Token: 0x04000E56 RID: 3670
        public static Item welcomeMessage = new Item
        {
            id = 9,
            name = "welcome",
            position = 12,
            price = 200,
            amount = 1,
            description = " - use /welcome [message] to set your welcome message,"
        };

        // Token: 0x04000E57 RID: 3671
        public static Item farewellMessage = new Item
        {
            id = 10,
            name = "farewell",
            position = 13,
            price = 180,
            amount = 1,
            description = " - use /farewell [message] to set your farewell message."
        };

        // Token: 0x04000E58 RID: 3672
        public static List<Item> storeItems = new List<Item>();

        // Token: 0x04000E59 RID: 3673
        private static readonly string storePriceFilePath = "lava\\itemprices.txt";

        // Token: 0x06001A0A RID: 6666 RVA: 0x000B71DC File Offset: 0x000B53DC
        public static void AssignItems()
        {
            life = storeItems.Find(item => item.id == 0);
            armor = storeItems.Find(item => item.id == 1);
            water = storeItems.Find(item => item.id == 2);
            sponge = storeItems.Find(item => item.id == 3);
            hammer = storeItems.Find(item => item.id == 4);
            door = storeItems.Find(item => item.id == 5);
            title = storeItems.Find(item => item.id == 6);
            promotion = storeItems.Find(item => item.id == 7);
        }

        // Token: 0x06001A0B RID: 6667 RVA: 0x000B734C File Offset: 0x000B554C
        public static string GetPromotionPriceString(Player p)
        {
            var promotionPrice = GetPromotionPrice(p);
            if (promotionPrice <= 0) return Lang.Store.PromotionNotAvailable;
            return promotionPrice.ToString();
        }

        // Token: 0x06001A0C RID: 6668 RVA: 0x000B7374 File Offset: 0x000B5574
        public static int GetPromotionPrice(Player p)
        {
            if (p == null) return 0;
            var group = Group.NextGroup(p.group);
            if (group == null) return 0;
            return group.promotionPrice;
        }

        // Token: 0x06001A0D RID: 6669 RVA: 0x000B73A0 File Offset: 0x000B55A0
        public static void InitStorePrices()
        {
            storeItems.Add(life);
            storeItems.Add(armor);
            storeItems.Add(water);
            storeItems.Add(sponge);
            storeItems.Add(hammer);
            storeItems.Add(door);
            storeItems.Add(color);
            storeItems.Add(title);
            storeItems.Add(titleColor);
            storeItems.Add(teleport);
            storeItems.Add(welcomeMessage);
            storeItems.Add(farewellMessage);
        }

        // Token: 0x06001A0E RID: 6670 RVA: 0x000B7464 File Offset: 0x000B5664
        private static int SortItemsByPositionAndAmount(Item x, Item y)
        {
            if (x.amount < 1 && y.amount > 0) return 1;
            if (y.amount < 1 && x.amount > 0) return -1;
            if (x.amount < 1 && y.amount < 1) return 0;
            if (x.position == y.position) return 0;
            if (x.position > y.position) return 1;
            return -1;
        }

        // Token: 0x06001A0F RID: 6671 RVA: 0x000B74D0 File Offset: 0x000B56D0
        private static void CountListedItems()
        {
            listedItemsCount = 0;
            for (var i = 0; i < storeItems.Count; i++)
                if (storeItems[i].amount > 0)
                    listedItemsCount++;
        }

        // Token: 0x06001A10 RID: 6672 RVA: 0x000B7518 File Offset: 0x000B5718
        public static void RepositionItems()
        {
            storeItems.Sort(SortItemsByPositionAndAmount);
            int i;
            for (i = 1; i <= storeItems.Count; i++)
                if (storeItems[i - 1].amount > 0)
                    storeItems[i - 1].realPosition = i.ToString();
            promotion.realPosition = i.ToString();
        }

        // Token: 0x06001A11 RID: 6673 RVA: 0x000B7590 File Offset: 0x000B5790
        public static void PrintList(Player p)
        {
            var i = 0;
            var num = 0;
            for (; i < storeItems.Count && i < firstPageLimit; i++)
                if (storeItems[i].amount == 1)
                {
                    Player.SendMessage2(p,
                        "%c" + (i + 1) + ": %d" + storeItems[i].name + " - " +
                        (p.money - storeItems[i].price < 0 ? "%c" : "%a") + storeItems[i].price + " " + Server.moneys +
                        Server.DefaultColor + storeItems[i].description);
                    num++;
                }
                else if (storeItems[i].amount > 1)
                {
                    Player.SendMessage2(p,
                        "%c" + (i + 1) + ": %d" + storeItems[i].name + "(x" + storeItems[i].amount + ") - " +
                        (p.money - storeItems[i].price < 0 ? "%c" : "%a") + storeItems[i].price + " " + Server.moneys +
                        Server.DefaultColor + storeItems[i].description);
                    num++;
                }

            var num2 = 0;
            num2 = GetPromotionPrice(p) <= 0 ? listedItemsCount : listedItemsCount + 1;
            if (num2 > firstPageLimit)
            {
                Player.SendMessage(p, Lang.Store.MoreItemsTip);
                return;
            }

            if (GetPromotionPrice(p) > 0)
                Player.SendMessage2(p,
                    string.Format(Lang.Store.PromotionItem, listedItemsCount + 1,
                        (p.money - GetPromotionPrice(p) < 0 ? "%c" : "%a") + GetPromotionPriceString(p),
                        Server.moneys + Server.DefaultColor,
                        Group.NextGroup(p.@group).color + Group.NextGroup(p.@group).trueName + Server.DefaultColor));
            promotion.realPosition = (listedItemsCount + 1).ToString();
        }

        // Token: 0x06001A12 RID: 6674 RVA: 0x000B7888 File Offset: 0x000B5A88
        public static void PrintListMore(Player p)
        {
            var num = 0;
            num = GetPromotionPrice(p) <= 0 ? listedItemsCount : listedItemsCount + 1;
            if (num > firstPageLimit)
            {
                for (var i = firstPageLimit; i < storeItems.Count; i++)
                    if (storeItems[i].amount == 1)
                        Player.SendMessage2(p,
                            "%c" + (i + 1) + ": %d" + storeItems[i].name + " - " +
                            (p.money - storeItems[i].price < 0 ? "%c" : "%a") + storeItems[i].price + " " +
                            Server.moneys + Server.DefaultColor + storeItems[i].description);
                    else if (storeItems[i].amount > 1)
                        Player.SendMessage2(p,
                            "%c" + (i + 1) + ": %d" + storeItems[i].name + "(x" + storeItems[i].amount + ") - " +
                            (p.money - storeItems[i].price < 0 ? "%c" : "%a") + storeItems[i].price + " " +
                            Server.moneys + Server.DefaultColor + storeItems[i].description);
                if (GetPromotionPrice(p) > 0)
                    Player.SendMessage2(p,
                        string.Format(Lang.Store.PromotionItem, listedItemsCount + 1,
                            (p.money - GetPromotionPrice(p) < 0 ? "%c" : "%a") + GetPromotionPriceString(p),
                            Server.moneys + Server.DefaultColor,
                            Group.NextGroup(p.@group).color + Group.NextGroup(p.@group).trueName +
                            Server.DefaultColor));
                promotion.realPosition = (listedItemsCount + 1).ToString();
            }
            else
            {
                PrintList(p);
            }
        }

        // Token: 0x06001A13 RID: 6675 RVA: 0x000B7B60 File Offset: 0x000B5D60
        public static void LoadPricesXML()
        {
            if (File.Exists(storePriceFilePath))
            {
                try
                {
                    var fileStream = new FileStream(storePriceFilePath, FileMode.Open, FileAccess.Read,
                        FileShare.ReadWrite);
                    var xmlDocument = new XmlDocument();
                    xmlDocument.Load(fileStream);
                    var firstChild = xmlDocument.FirstChild;
                    if (firstChild.Attributes != null)
                        foreach (XmlAttribute attribute in firstChild.Attributes)
                            if (attribute.Name.ToLower() == "first-page-limit")
                                try
                                {
                                    firstPageLimit = int.Parse(attribute.Value);
                                }
                                catch
                                {
                                    Server.s.Log(
                                        "Incorrect 'firstPageLimit' value in 'itemprices.txt'. Using default.");
                                }

                    var elementsByTagName = xmlDocument.GetElementsByTagName("Item");
                    for (var i = 0; i < elementsByTagName.Count; i++)
                    {
                        var xmlattrc = elementsByTagName[i].Attributes;
                        var item2 = new Item();
                        int ii;
                        for (ii = 0; ii < xmlattrc.Count; ii++)
                            if (xmlattrc[ii].Name == "id")
                                item2 = storeItems.Find(item => item.id.ToString() == xmlattrc[ii].Value);
                        if (item2 == null) continue;
                        for (var j = 0; j < xmlattrc.Count; j++)
                            switch (xmlattrc[j].Name.ToLower())
                            {
                                case "name":
                                    item2.name = xmlattrc[j].Value;
                                    break;
                                case "description":
                                    item2.description = xmlattrc[j].Value;
                                    break;
                                case "position":
                                    try
                                    {
                                        item2.position = int.Parse(xmlattrc[j].Value);
                                    }
                                    catch
                                    {
                                        Server.s.Log("Wrong value of position in store price file(id=" + item2.id +
                                                     "\", using default value.");
                                    }

                                    break;
                                case "amount":
                                    try
                                    {
                                        item2.amount = int.Parse(xmlattrc[j].Value);
                                    }
                                    catch
                                    {
                                        Server.s.Log("Wrong value of amount in store price file(id=" + item2.id +
                                                     "\", using default value.");
                                    }

                                    break;
                                case "price":
                                    try
                                    {
                                        item2.price = ushort.Parse(xmlattrc[j].Value);
                                    }
                                    catch
                                    {
                                        Server.s.Log("Wrong value of price in store price file(id=" + item2.id +
                                                     "\", using default value.");
                                    }

                                    break;
                            }
                    }

                    fileStream.Close();
                    fileStream.Dispose();
                }
                catch (Exception ex)
                {
                    Server.ErrorLog(ex);
                }

                SavePricesXML();
                RepositionItems();
                CountListedItems();
            }
            else
            {
                SavePricesXML();
                RepositionItems();
                CountListedItems();
            }
        }

        // Token: 0x06001A14 RID: 6676 RVA: 0x000B7F78 File Offset: 0x000B6178
        public static void SavePricesXML()
        {
            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", "yes"));
                xmlDocument.AppendChild(xmlDocument.CreateWhitespace("\r\n"));
                xmlDocument.AppendChild(
                    xmlDocument.CreateComment("If you set amount to 0 then the item will not appear in the shop."));
                xmlDocument.AppendChild(xmlDocument.CreateWhitespace("\r\n"));
                var xmlElement = xmlDocument.CreateElement("Store");
                var xmlAttribute = xmlDocument.CreateAttribute("first-page-limit");
                xmlAttribute.Value = firstPageLimit.ToString();
                xmlElement.SetAttributeNode(xmlAttribute);
                for (var i = 0; i < storeItems.Count; i++)
                {
                    var xmlElement2 = xmlDocument.CreateElement("Item");
                    var xmlAttribute2 = xmlDocument.CreateAttribute("id");
                    var xmlAttribute3 = xmlDocument.CreateAttribute("name");
                    var xmlAttribute4 = xmlDocument.CreateAttribute("position");
                    var xmlAttribute5 = xmlDocument.CreateAttribute("price");
                    var xmlAttribute6 = xmlDocument.CreateAttribute("amount");
                    var xmlAttribute7 = xmlDocument.CreateAttribute("description");
                    xmlAttribute2.Value = storeItems[i].id.ToString();
                    xmlAttribute3.Value = storeItems[i].name;
                    xmlAttribute4.Value = storeItems[i].position.ToString();
                    xmlAttribute5.Value = storeItems[i].price.ToString();
                    xmlAttribute6.Value = storeItems[i].amount.ToString();
                    xmlAttribute7.Value = storeItems[i].description;
                    xmlElement2.SetAttributeNode(xmlAttribute4);
                    xmlElement2.SetAttributeNode(xmlAttribute2);
                    xmlElement2.SetAttributeNode(xmlAttribute3);
                    xmlElement2.SetAttributeNode(xmlAttribute5);
                    xmlElement2.SetAttributeNode(xmlAttribute6);
                    xmlElement2.SetAttributeNode(xmlAttribute7);
                    xmlElement.AppendChild(xmlElement2);
                }

                var fileStream = new FileStream(storePriceFilePath, FileMode.Create, FileAccess.Write,
                    FileShare.ReadWrite);
                xmlDocument.AppendChild(xmlElement);
                xmlDocument.Save(fileStream);
                fileStream.Close();
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x06001A15 RID: 6677 RVA: 0x000B81B8 File Offset: 0x000B63B8
        public static void BuyLife(Player p)
        {
            if (p.money >= life.price)
            {
                p.money -= life.price;
                if (p.lives == 0) p.invincible = false;
                p.lives += (byte) life.amount;
                p.flipHead = false;
                Player.SendMessage(p, Lang.Store.BoughtLife);
                if (p.inHeaven)
                {
                    p.inHeaven = false;
                    Command.all.Find("goto").Use(p, Server.LavaLevel.name);
                }
            }
            else
            {
                Player.SendMessage(p, string.Format(Lang.Store.NotEnoughMoney, Server.moneys));
            }
        }

        // Token: 0x06001A16 RID: 6678 RVA: 0x000B8270 File Offset: 0x000B6470
        public static void BuyArmor(Player p)
        {
            if (p.money >= armor.price)
            {
                p.money -= armor.price;
                p.UseArmor();
                Player.SendMessage(p, Lang.Store.BoughtArmor);
                return;
            }

            Player.SendMessage(p, string.Format(Lang.Store.NotEnoughMoney, Server.moneys));
        }

        // Token: 0x06001A17 RID: 6679 RVA: 0x000B82D0 File Offset: 0x000B64D0
        public static void BuySponge(Player p)
        {
            if (p.money >= sponge.price)
            {
                p.money -= sponge.price;
                p.spongeBlocks += sponge.amount;
                Player.SendMessage(p, Lang.Store.BoughtSponges);
                return;
            }

            Player.SendMessage(p, string.Format(Lang.Store.NotEnoughMoney, Server.moneys));
        }

        // Token: 0x06001A18 RID: 6680 RVA: 0x000B8340 File Offset: 0x000B6540
        public static void BuyWater(Player p)
        {
            if (p.money >= water.price)
            {
                p.money -= water.price;
                p.waterBlocks += water.amount;
                Player.SendMessage(p, Lang.Store.BoughtWater);
                return;
            }

            Player.SendMessage(p, string.Format(Lang.Store.NotEnoughMoney, Server.moneys));
        }

        // Token: 0x06001A19 RID: 6681 RVA: 0x000B83B0 File Offset: 0x000B65B0
        public static void BuyHammer(Player p)
        {
            if (p.money >= hammer.price)
            {
                p.money -= hammer.price;
                Player.SendMessage(p, Lang.Store.BoughtHammer);
                p.hammer += hammer.amount;
                return;
            }

            Player.SendMessage(p, string.Format(Lang.Store.NotEnoughMoney, Server.moneys));
        }

        // Token: 0x06001A1A RID: 6682 RVA: 0x000B8420 File Offset: 0x000B6620
        public static void BuyDoor(Player p)
        {
            if (p.money >= door.price)
            {
                p.money -= door.price;
                p.doorBlocks += door.amount;
                Player.SendMessage(p, Lang.Store.BoughtDoors);
                return;
            }

            Player.SendMessage(p, string.Format(Lang.Store.NotEnoughMoney, Server.moneys));
        }

        // Token: 0x06001A1B RID: 6683 RVA: 0x000B8490 File Offset: 0x000B6690
        public static void BuyTitle(Player p)
        {
            if (p.boughtTitle)
            {
                Player.SendMessage(p, Lang.Store.BoughtTitleTip);
                return;
            }

            if (p.money >= title.price)
            {
                p.money -= title.price;
                Player.SendMessage(p, Lang.Store.BoughtTitle);
                p.boughtTitle = true;
                return;
            }

            Player.SendMessage(p, string.Format(Lang.Store.NotEnoughMoney, Server.moneys));
        }

        // Token: 0x06001A1C RID: 6684 RVA: 0x000B8504 File Offset: 0x000B6704
        public static void BuyWelcome(Player p)
        {
            if (p.boughtWelcome)
            {
                Player.SendMessage(p, Lang.Store.BoughtWelcomeTip);
                return;
            }

            if (p.money >= welcomeMessage.price)
            {
                p.money -= welcomeMessage.price;
                Player.SendMessage(p, Lang.Store.BoughtWelcome);
                p.boughtWelcome = true;
                return;
            }

            Player.SendMessage(p, string.Format(Lang.Store.NotEnoughMoney, Server.moneys));
        }

        // Token: 0x06001A1D RID: 6685 RVA: 0x000B8578 File Offset: 0x000B6778
        public static void BuyFarewell(Player p)
        {
            if (p.boughtFarewell)
            {
                Player.SendMessage(p, Lang.Store.BoughtFarewellTip);
                return;
            }

            if (p.money >= farewellMessage.price)
            {
                p.money -= farewellMessage.price;
                Player.SendMessage(p, Lang.Store.BoughtFarewell);
                p.boughtFarewell = true;
                return;
            }

            Player.SendMessage(p, string.Format(Lang.Store.NotEnoughMoney, Server.moneys));
        }

        // Token: 0x06001A1E RID: 6686 RVA: 0x000B85EC File Offset: 0x000B67EC
        public static void BuyTeleport(Player p)
        {
            if (p.hasTeleport)
            {
                Player.SendMessage(p, Lang.Store.BoughtTeleportTip);
                return;
            }

            if (p.money >= teleport.price)
            {
                p.money -= teleport.price;
                Player.SendMessage(p, Lang.Store.BoughtTeleport);
                p.hasTeleport = true;
                return;
            }

            Player.SendMessage(p, string.Format(Lang.Store.NotEnoughMoney, Server.moneys));
        }

        // Token: 0x06001A1F RID: 6687 RVA: 0x000B8660 File Offset: 0x000B6860
        public static void BuyPromotion(Player p)
        {
            var promotionPrice = GetPromotionPrice(p);
            if (promotionPrice == 0)
            {
                Player.SendMessage(p, Lang.Store.BoughtPromotionWarning);
                return;
            }

            if (LavaSettings.All.RequireRegistrationForPromotion && !p.FlagsCollection["registered"])
            {
                var flag = false;
                Player.OnPlayerRegisteredCheck(p, ref flag);
                if (!flag)
                {
                    var text = MessagesManager.GetString("RegistrationRequired");
                    text = text == "" ? "%cYou have to register on the forum before you can get a higher rank!" : text;
                    Player.SendMessage(p, text);
                    return;
                }

                p.FlagsCollection["registered"] = true;
            }

            if (p.money >= promotionPrice)
            {
                if (LavaSystem.RankUp(p))
                {
                    p.money -= promotionPrice;
                }
            }
            else
            {
                Player.SendMessage(p, string.Format(Lang.Store.NotEnoughMoney, Server.moneys));
            }
        }

        // Token: 0x06001A20 RID: 6688 RVA: 0x000B8720 File Offset: 0x000B6920
        public static void BuyColor(Player p)
        {
            if (p.boughtColor)
            {
                Player.SendMessage(p, Lang.Store.BoughtColorTip);
                return;
            }

            if (p.money >= color.price)
            {
                p.money -= color.price;
                Player.SendMessage(p, Lang.Store.BoughtColor);
                p.boughtColor = true;
                return;
            }

            Player.SendMessage(p, string.Format(Lang.Store.NotEnoughMoney, Server.moneys));
        }

        // Token: 0x06001A21 RID: 6689 RVA: 0x000B8794 File Offset: 0x000B6994
        public static void BuyTitleColor(Player p)
        {
            if (p.boughtTColor)
            {
                Player.SendMessage(p, Lang.Store.BoughtTitleColorTip);
                return;
            }

            if (p.money >= titleColor.price)
            {
                p.money -= titleColor.price;
                Player.SendMessage(p, Lang.Store.BoughtTitleColor);
                p.boughtTColor = true;
                return;
            }

            Player.SendMessage(p, string.Format(Lang.Store.NotEnoughMoney, Server.moneys));
        }

        // Token: 0x02000393 RID: 915
        public class Item
        {
            // Token: 0x04000E64 RID: 3684
            public int amount;

            // Token: 0x04000E67 RID: 3687
            public string description;

            // Token: 0x04000E62 RID: 3682
            public int id;

            // Token: 0x04000E66 RID: 3686
            public string name;

            // Token: 0x04000E63 RID: 3683
            public int position;

            // Token: 0x04000E65 RID: 3685
            public ushort price;

            // Token: 0x04000E68 RID: 3688
            public string realPosition;
        }
    }
}