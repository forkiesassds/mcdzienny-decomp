using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using MCDzienny.MultiMessages;
using MCDzienny.Settings;

namespace MCDzienny
{
    class Store
    {

        static int firstPageLimit = 7;

        static int listedItemsCount;

        public static Item life = new Item
        {
            id = 0,
            name = "life",
            position = 1,
            price = 10,
            amount = 1,
            description = " - gives you one life,"
        };

        public static Item armor = new Item
        {
            id = 1,
            name = "armor",
            position = 2,
            price = 30,
            amount = 1,
            description = " - gives you full protection from lava for 45seconds,"
        };

        public static Item water = new Item
        {
            id = 2,
            name = "water",
            position = 3,
            price = 15,
            amount = 20,
            description = " - to use write /water and place any block,"
        };

        public static Item sponge = new Item
        {
            id = 3,
            name = "sponge",
            position = 4,
            price = 20,
            amount = 5,
            description = " - removes lava around, dissapears quickly,"
        };

        public static Item hammer = new Item
        {
            id = 4,
            name = "hammer",
            position = 5,
            price = 20,
            amount = 100,
            description = " - acts like cuboid, write /hammer to use it,"
        };

        public static Item door = new Item
        {
            id = 5,
            name = "door",
            position = 6,
            price = 20,
            amount = 6,
            description = " - use /door command to create steel doors,"
        };

        public static Item teleport = new Item
        {
            id = 8,
            name = "teleport",
            position = 7,
            price = 20,
            amount = 1,
            description = " - use /tp [player] to teleport to the player,"
        };

        public static Item color = new Item
        {
            id = 11,
            name = "color",
            position = 8,
            price = 280,
            amount = 1,
            description = " - use /color [color] to set your name color,"
        };

        public static Item title = new Item
        {
            id = 6,
            name = "title",
            position = 9,
            price = 280,
            amount = 1,
            description = " - use /title [your title] to set a new title,"
        };

        public static Item titleColor = new Item
        {
            id = 12,
            name = "titlecolor",
            position = 10,
            price = 200,
            amount = 1,
            description = " - use /titlecolor [color] to set your title color,"
        };

        public static Item promotion = new Item
        {
            id = 7,
            name = "promotion",
            position = 11,
            amount = 1
        };

        public static Item welcomeMessage = new Item
        {
            id = 9,
            name = "welcome",
            position = 12,
            price = 200,
            amount = 1,
            description = " - use /welcome [message] to set your welcome message,"
        };

        public static Item farewellMessage = new Item
        {
            id = 10,
            name = "farewell",
            position = 13,
            price = 180,
            amount = 1,
            description = " - use /farewell [message] to set your farewell message."
        };

        public static List<Item> storeItems = new List<Item>();

        static readonly string storePriceFilePath = "lava\\itemprices.txt";

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

        public static string GetPromotionPriceString(Player p)
        {
            int promotionPrice = GetPromotionPrice(p);
            if (promotionPrice <= 0)
            {
                return Lang.Store.PromotionNotAvailable;
            }
            return promotionPrice.ToString();
        }

        public static int GetPromotionPrice(Player p)
        {
            if (p == null) return 0;
            Group group = Group.NextGroup(p.group);
            return group == null ? 0 : group.promotionPrice;
        }

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

        static int SortItemsByPositionAndAmount(Item x, Item y)
        {
            if (x.amount < 1 && y.amount > 0)
            {
                return 1;
            }
            if (y.amount < 1 && x.amount > 0)
            {
                return -1;
            }
            if (x.amount < 1 && y.amount < 1)
            {
                return 0;
            }
            if (x.position == y.position)
            {
                return 0;
            }
            if (x.position > y.position)
            {
                return 1;
            }
            return -1;
        }

        static void CountListedItems()
        {
            listedItemsCount = 0;
            for (int i = 0; i < storeItems.Count; i++)
            {
                if (storeItems[i].amount > 0)
                {
                    listedItemsCount++;
                }
            }
        }

        public static void RepositionItems()
        {
            storeItems.Sort(SortItemsByPositionAndAmount);
            int i;
            for (i = 1; i <= storeItems.Count; i++)
            {
                if (storeItems[i - 1].amount > 0)
                {
                    storeItems[i - 1].realPosition = i.ToString();
                }
            }
            promotion.realPosition = i.ToString();
        }

        public static void PrintList(Player p)
        {
            int i = 0;
            int num = 0;
            for (; i < storeItems.Count && i < firstPageLimit; i++)
            {
                if (storeItems[i].amount == 1)
                {
                    Player.SendMessage2(
                        p,
                        "%c" + (i + 1) + ": %d" + storeItems[i].name + " - " + (p.money - storeItems[i].price < 0 ? "%c" : "%a") + storeItems[i].price + " " +
                        Server.moneys + Server.DefaultColor + storeItems[i].description);
                    num++;
                }
                else if (storeItems[i].amount > 1)
                {
                    Player.SendMessage2(
                        p,
                        "%c" + (i + 1) + ": %d" + storeItems[i].name + "(x" + storeItems[i].amount + ") - " + (p.money - storeItems[i].price < 0 ? "%c" : "%a") +
                        storeItems[i].price + " " + Server.moneys + Server.DefaultColor + storeItems[i].description);
                    num++;
                }
            }
            int num2 = 0;
            num2 = GetPromotionPrice(p) <= 0 ? listedItemsCount : listedItemsCount + 1;
            if (num2 > firstPageLimit)
            {
                Player.SendMessage(p, Lang.Store.MoreItemsTip);
                return;
            }
            if (GetPromotionPrice(p) > 0)
            {
                Player.SendMessage2(
                    p,
                    string.Format(Lang.Store.PromotionItem, listedItemsCount + 1, (p.money - GetPromotionPrice(p) < 0 ? "%c" : "%a") + GetPromotionPriceString(p),
                                  Server.moneys + Server.DefaultColor, Group.NextGroup(p.group).color + Group.NextGroup(p.group).trueName + Server.DefaultColor));
            }
            promotion.realPosition = (listedItemsCount + 1).ToString();
        }

        public static void PrintListMore(Player p)
        {
            int num = 0;
            num = GetPromotionPrice(p) <= 0 ? listedItemsCount : listedItemsCount + 1;
            if (num > firstPageLimit)
            {
                for (int i = firstPageLimit; i < storeItems.Count; i++)
                {
                    if (storeItems[i].amount == 1)
                    {
                        Player.SendMessage2(
                            p,
                            "%c" + (i + 1) + ": %d" + storeItems[i].name + " - " + (p.money - storeItems[i].price < 0 ? "%c" : "%a") + storeItems[i].price + " " +
                            Server.moneys + Server.DefaultColor + storeItems[i].description);
                    }
                    else if (storeItems[i].amount > 1)
                    {
                        Player.SendMessage2(
                            p,
                            "%c" + (i + 1) + ": %d" + storeItems[i].name + "(x" + storeItems[i].amount + ") - " + (p.money - storeItems[i].price < 0 ? "%c" : "%a") +
                            storeItems[i].price + " " + Server.moneys + Server.DefaultColor + storeItems[i].description);
                    }
                }
                if (GetPromotionPrice(p) > 0)
                {
                    Player.SendMessage2(
                        p,
                        string.Format(Lang.Store.PromotionItem, listedItemsCount + 1, (p.money - GetPromotionPrice(p) < 0 ? "%c" : "%a") + GetPromotionPriceString(p),
                                      Server.moneys + Server.DefaultColor, Group.NextGroup(p.group).color + Group.NextGroup(p.group).trueName + Server.DefaultColor));
                }
                promotion.realPosition = (listedItemsCount + 1).ToString();
            }
            else
            {
                PrintList(p);
            }
        }

        public static void LoadPricesXML()
        {
            //IL_001d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0023: Expected O, but got Unknown
            //IL_004f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0055: Expected O, but got Unknown
            if (File.Exists(storePriceFilePath))
            {
                try
                {
                    FileStream fileStream = new FileStream(storePriceFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    XmlDocument val = new XmlDocument();
                    val.Load(fileStream);
                    XmlNode firstChild = val.FirstChild;
                    if (firstChild.Attributes != null)
                    {
                        foreach (XmlAttribute item3 in firstChild.Attributes)
                        {
                            XmlAttribute val2 = item3;
                            if (val2.Name.ToLower() == "first-page-limit")
                            {
                                try
                                {
                                    firstPageLimit = int.Parse(val2.Value);
                                }
                                catch
                                {
                                    Server.s.Log("Incorrect 'firstPageLimit' value in 'itemprices.txt'. Using default.");
                                }
                            }
                        }
                    }
                    XmlNodeList elementsByTagName = val.GetElementsByTagName("Item");
                    for (int i = 0; i < elementsByTagName.Count; i++)
                    {
                        XmlAttributeCollection xmlattrc = elementsByTagName[i].Attributes;
                        Item item2 = new Item();
                        int ii;
                        for (ii = 0; ii < xmlattrc.Count; ii++)
                        {
                            if (xmlattrc[ii].Name == "id")
                            {
                                item2 = storeItems.Find(item => item.id.ToString() == xmlattrc[ii].Value);
                            }
                        }
                        if (item2 == null)
                        {
                            continue;
                        }
                        for (int j = 0; j < xmlattrc.Count; j++)
                        {
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
                                        Server.s.Log("Wrong value of position in store price file(id=" + item2.id + "\", using default value.");
                                    }
                                    break;
                                case "amount":
                                    try
                                    {
                                        item2.amount = int.Parse(xmlattrc[j].Value);
                                    }
                                    catch
                                    {
                                        Server.s.Log("Wrong value of amount in store price file(id=" + item2.id + "\", using default value.");
                                    }
                                    break;
                                case "price":
                                    try
                                    {
                                        item2.price = ushort.Parse(xmlattrc[j].Value);
                                    }
                                    catch
                                    {
                                        Server.s.Log("Wrong value of price in store price file(id=" + item2.id + "\", using default value.");
                                    }
                                    break;
                            }
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

        public static void SavePricesXML()
        {
            //IL_0000: Unknown result type (might be due to invalid IL or missing references)
            //IL_0006: Expected O, but got Unknown
            try
            {
                XmlDocument val = new XmlDocument();
                val.AppendChild(val.CreateXmlDeclaration("1.0", "UTF-8", "yes"));
                val.AppendChild(val.CreateWhitespace("\r\n"));
                val.AppendChild(val.CreateComment("If you set amount to 0 then the item will not appear in the shop."));
                val.AppendChild(val.CreateWhitespace("\r\n"));
                XmlElement val2 = val.CreateElement("Store");
                XmlAttribute val3 = val.CreateAttribute("first-page-limit");
                val3.Value = firstPageLimit.ToString();
                val2.SetAttributeNode(val3);
                for (int i = 0; i < storeItems.Count; i++)
                {
                    XmlElement val4 = val.CreateElement("Item");
                    XmlAttribute val5 = val.CreateAttribute("id");
                    XmlAttribute val6 = val.CreateAttribute("name");
                    XmlAttribute val7 = val.CreateAttribute("position");
                    XmlAttribute val8 = val.CreateAttribute("price");
                    XmlAttribute val9 = val.CreateAttribute("amount");
                    XmlAttribute val10 = val.CreateAttribute("description");
                    val5.Value = storeItems[i].id.ToString();
                    val6.Value = storeItems[i].name;
                    val7.Value = storeItems[i].position.ToString();
                    val8.Value = storeItems[i].price.ToString();
                    val9.Value = storeItems[i].amount.ToString();
                    val10.Value = storeItems[i].description;
                    val4.SetAttributeNode(val7);
                    val4.SetAttributeNode(val5);
                    val4.SetAttributeNode(val6);
                    val4.SetAttributeNode(val8);
                    val4.SetAttributeNode(val9);
                    val4.SetAttributeNode(val10);
                    val2.AppendChild(val4);
                }
                FileStream fileStream = new FileStream(storePriceFilePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                val.AppendChild(val2);
                val.Save(fileStream);
                fileStream.Close();
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        public static void BuyLife(Player p)
        {
            if (p.money >= life.price)
            {
                p.money -= life.price;
                if (p.lives == 0)
                {
                    p.invincible = false;
                }
                p.lives += (byte)life.amount;
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

        public static void BuyArmor(Player p)
        {
            if (p.money >= armor.price)
            {
                p.money -= armor.price;
                p.UseArmor();
                Player.SendMessage(p, Lang.Store.BoughtArmor);
            }
            else
            {
                Player.SendMessage(p, string.Format(Lang.Store.NotEnoughMoney, Server.moneys));
            }
        }

        public static void BuySponge(Player p)
        {
            if (p.money >= sponge.price)
            {
                p.money -= sponge.price;
                p.spongeBlocks += sponge.amount;
                Player.SendMessage(p, Lang.Store.BoughtSponges);
            }
            else
            {
                Player.SendMessage(p, string.Format(Lang.Store.NotEnoughMoney, Server.moneys));
            }
        }

        public static void BuyWater(Player p)
        {
            if (p.money >= water.price)
            {
                p.money -= water.price;
                p.waterBlocks += water.amount;
                Player.SendMessage(p, Lang.Store.BoughtWater);
            }
            else
            {
                Player.SendMessage(p, string.Format(Lang.Store.NotEnoughMoney, Server.moneys));
            }
        }

        public static void BuyHammer(Player p)
        {
            if (p.money >= hammer.price)
            {
                p.money -= hammer.price;
                Player.SendMessage(p, Lang.Store.BoughtHammer);
                p.hammer += hammer.amount;
            }
            else
            {
                Player.SendMessage(p, string.Format(Lang.Store.NotEnoughMoney, Server.moneys));
            }
        }

        public static void BuyDoor(Player p)
        {
            if (p.money >= door.price)
            {
                p.money -= door.price;
                p.doorBlocks += door.amount;
                Player.SendMessage(p, Lang.Store.BoughtDoors);
            }
            else
            {
                Player.SendMessage(p, string.Format(Lang.Store.NotEnoughMoney, Server.moneys));
            }
        }

        public static void BuyTitle(Player p)
        {
            if (p.boughtTitle)
            {
                Player.SendMessage(p, Lang.Store.BoughtTitleTip);
            }
            else if (p.money >= title.price)
            {
                p.money -= title.price;
                Player.SendMessage(p, Lang.Store.BoughtTitle);
                p.boughtTitle = true;
            }
            else
            {
                Player.SendMessage(p, string.Format(Lang.Store.NotEnoughMoney, Server.moneys));
            }
        }

        public static void BuyWelcome(Player p)
        {
            if (p.boughtWelcome)
            {
                Player.SendMessage(p, Lang.Store.BoughtWelcomeTip);
            }
            else if (p.money >= welcomeMessage.price)
            {
                p.money -= welcomeMessage.price;
                Player.SendMessage(p, Lang.Store.BoughtWelcome);
                p.boughtWelcome = true;
            }
            else
            {
                Player.SendMessage(p, string.Format(Lang.Store.NotEnoughMoney, Server.moneys));
            }
        }

        public static void BuyFarewell(Player p)
        {
            if (p.boughtFarewell)
            {
                Player.SendMessage(p, Lang.Store.BoughtFarewellTip);
            }
            else if (p.money >= farewellMessage.price)
            {
                p.money -= farewellMessage.price;
                Player.SendMessage(p, Lang.Store.BoughtFarewell);
                p.boughtFarewell = true;
            }
            else
            {
                Player.SendMessage(p, string.Format(Lang.Store.NotEnoughMoney, Server.moneys));
            }
        }

        public static void BuyTeleport(Player p)
        {
            if (p.hasTeleport)
            {
                Player.SendMessage(p, Lang.Store.BoughtTeleportTip);
            }
            else if (p.money >= teleport.price)
            {
                p.money -= teleport.price;
                Player.SendMessage(p, Lang.Store.BoughtTeleport);
                p.hasTeleport = true;
            }
            else
            {
                Player.SendMessage(p, string.Format(Lang.Store.NotEnoughMoney, Server.moneys));
            }
        }

        public static void BuyPromotion(Player p)
        {
            int promotionPrice = GetPromotionPrice(p);
            if (promotionPrice == 0)
            {
                Player.SendMessage(p, Lang.Store.BoughtPromotionWarning);
                return;
            }
            if (LavaSettings.All.RequireRegistrationForPromotion && !p.FlagsCollection["registered"])
            {
                bool isRegistered = false;
                Player.OnPlayerRegisteredCheck(p, ref isRegistered);
                if (!isRegistered)
                {
                    string @string = MessagesManager.GetString("RegistrationRequired");
                    @string = @string == "" ? "%cYou have to register on the forum before you can get a higher rank!" : @string;
                    Player.SendMessage(p, @string);
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

        public static void BuyColor(Player p)
        {
            if (p.boughtColor)
            {
                Player.SendMessage(p, Lang.Store.BoughtColorTip);
            }
            else if (p.money >= color.price)
            {
                p.money -= color.price;
                Player.SendMessage(p, Lang.Store.BoughtColor);
                p.boughtColor = true;
            }
            else
            {
                Player.SendMessage(p, string.Format(Lang.Store.NotEnoughMoney, Server.moneys));
            }
        }

        public static void BuyTitleColor(Player p)
        {
            if (p.boughtTColor)
            {
                Player.SendMessage(p, Lang.Store.BoughtTitleColorTip);
            }
            else if (p.money >= titleColor.price)
            {
                p.money -= titleColor.price;
                Player.SendMessage(p, Lang.Store.BoughtTitleColor);
                p.boughtTColor = true;
            }
            else
            {
                Player.SendMessage(p, string.Format(Lang.Store.NotEnoughMoney, Server.moneys));
            }
        }

        public class Item
        {

            public int amount;

            public string description;
            public int id;

            public string name;

            public int position;

            public ushort price;

            public string realPosition;
        }
    }
}