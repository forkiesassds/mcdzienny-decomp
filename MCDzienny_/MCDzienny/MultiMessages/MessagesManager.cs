using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace MCDzienny.MultiMessages
{
    public sealed class MessagesManager
    {
        static MessagesCollection messages;

        static readonly object messageAddLock = new object();

        static string cultureCode;

        static readonly string fileName = "messages";

        static readonly string DefaultContent =
            "// You can use only the following characters: \r\n// ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789![]:.,{}~-+()?_/\\%&=\r\n// For color text use %0..%9 and %a..%f or you can use &0..&9 and &a..&f\r\n// If you want to use a double quotation mark (\") you have to write (\\\") instead\r\n// You can create as many alternative messages as you want.\r\n// Each alternative message add inline by delimiting it with a coma (,) and\r\n// writing it within a double-quotation mark.\r\n// Remember no special characters apart from the mentioned before are allowed\r\n// as they may lead to a client crash!\r\n// In order to set this file to its default state, delete it and restart the server.\r\n\r\nSurvivorsCongratulations = \"%5Survivors, Congratulations!!!\"\r\nWinnersList = \"%6Winners list:\"\r\nTopWinners = \"%dTop winners:\"\r\nScoreResult = \"%c{0} (Score: {1})\"\r\nScoreResultBest = \"%c {0} This round best! (Score: {1})\"\r\nExperienceGain = \"%a+%e {0} experience points!\"\r\n\r\nLavaStateCalm = \"%bLava stays %aCALM%b in this round.\"\r\nLavaStateDisturbed = \"%bLava is %9DISTURBED%b in this round.\"\r\nLavaStateFurious = \"%bLava becomes %cFURIOUS%b in this round.\"\r\nLavaStateWild = \"%bLava gets %cWILD%b in this round!!!\"\r\n\r\nLavaIsComing = \"%5(right)Look out! Lava is coming!!!\"\r\nMinuteToLavaFlood = \"%d{0} minute left to lava flood!\"\r\nMinutesToLavaFlood = \"%d{0} minutes left to lava flood!\"\r\nMinuteToLavaFloodEnd = \"%d{0} minute left to the end of lava flood!\"\r\nMinutesToLavaFloodEnd = \"%d{0} minutes left to the end of lava flood!\"\r\n\r\nGlobalMessagePlayerJoined = \"{0} has joined the game.\"\r\nGlobalMessagePlayerDisconnected = \"{0} disconnected.\"\r\n\r\nIsGhost = \"{0} is a ghost now.\"\r\nHelpMessageGhost = \"* You are a ghost now. You should buy a life, write: /buy life *\"\r\nWarningBlockChangeGhost = \"You can't modify blocks because you are a ghost. You should buy a new life.\"\r\nSentToHeaven = \"%bYou were sent to heaven!\"\r\nHelpMessageInHeaven = \"%aIf you want to come back to lava arena buy a life.\"\r\n\r\nWaterIsComing = \"%5(right)Look out! Water is coming!!!\"\r\nMinuteToWaterFlood =  \"%d{0} minute left to water flood!\"\r\nMinutesToWaterFlood = \"%d{0} minutes left to water flood\"\r\nMinuteToWaterFloodEnd = \"%d{0} minute left to the end of water flood!\"\r\nMinutesToWaterFloodEnd = \"%d{0} minutes left to the end of water flood\"\r\n\r\nDeathActiveColdWater = \"{0} stepped in &dcold water and froze.\"\r\nDeathMagma = \"{0} was hit by &cflowing magma and melted.\"\r\nDeathZombie = \"{0} died due to lack of &5brain.\"\r\nDeathCreeper = \"{0} was killed &cb-SSSSSSSSSSSSSS\"\r\nDeathActiveHotLava = \"{0} stood in &cmagma and melted.\", \"{0} was %cevaporated\", \"{0} was %ceaten by lava\"\r\nDeathTntExplosion = \"{0} &cblew into pieces.\"\r\nDeathAir = \"{0} walked into &cnerve gas and suffocated.\"\r\nDeathGeyser = \"{0} was hit by &cboiling water and melted.\"\r\nDeathBird = \"{0} was hit by a &cphoenix and burnt.\"\r\nDeathTrain = \"{0} was hit by a &ctrain.\"\r\nDeathShark = \"{0} was eaten by a &cshark.\"\r\nDeathFire = \"{0} burnt to a &ccrisp.\"\r\nDeathRocket = \"{0} was &cin a fiery explosion.\"\r\nDeathFall = \"{0} hit the floor &chard.\"\r\nDeathDrawn = \"{0} &cdrowned.\"\r\nDeathTermination = \"{0} was &cterminated\"\r\nDeathLavaShark = \"{0} was eaten by a ... LAVA SHARK?!\"\r\n\r\nInfectionVirusWillBeReleased = \"%cGet ready!\"\r\nInfectionWasInfected = \"%c{0} was infected!!\", \"%c{0} was zombified!\", \"%c{0} is no longer a human!\", \"%c{0} got into hands of a zombie\"\r\nInfectionRunHumans = \"%b Run humans, run!\"\r\nInfectionWasBitten = \"%c{0} was bitten by {1}\", \"%c{0} got into hands of {1}\", \"%c{0} was caught by {1}\", \"%c{0} was zombified by {1}\"\r\nInfectionKillingSpree = \"%a{0} is on {1}x killing spree!!!\"\r\nInfectionEnded = \"The infection has ended!\"\r\nInfectionWinners = \"The winners are\"\r\nInfectionReward = \"You were rewarded {0} {1}!\"\r\nInfectionTimeLeft = \"There is {0}min {1}s left to the end of the infection.\"\r\nInfectionHumansLeft = \"%7Humans left: %a{0}\"\r\nInfectionEncourageHumans = \"%aYou are doing great keep it up. Keep running!\", \"%aStay away from zombies and you will be fine :)\", \"%aDon't let zombies get you!\"\r\nInfectionEncourageZombies = \"%cGet those tasty humans! Have some feast!\", \"%cHunt them all!\", \"%cNever give up!\"\r\nInfectionRoundStartsIn = \"Round starts in %c{0}\"\r\nInfectionRoundEndsIn = \"Round ends in %c{0}\"\r\n\r\nTreasureFound = \"Congratulations you've found a treasure!\"\r\nTreasureGain = \"You gained {0} {1}\"\r\n\r\nRegistrationRequired = \"%cYou have to register on the forum before you can get a higher rank!\"";

        public static void SetCulture(string culture)
        {
            if (messages == null)
            {
                messages = new MessagesCollection();
            }
            else
            {
                messages.Clear();
            }
            Load(culture);
        }

        public static string GetString(string key)
        {
            if (messages == null)
            {
                Load("");
                VerifyAndFix(messages);
            }
            return messages[key];
        }

        static void SetToDefault(string fullPath)
        {
            using (StreamWriter streamWriter = new StreamWriter(fullPath))
            {
                streamWriter.Write(DefaultContent);
            }
        }

        public static void Reload()
        {
            Load(cultureCode);
        }

        static void Load(string culture = null)
        {
            try
            {
                string text = "messages/";
                string text2 = ".txt";
                string text3 = fileName + text2;
                cultureCode = culture;
                messages = new MessagesCollection();
                if (!Directory.Exists(text))
                {
                    Directory.CreateDirectory(text);
                }
                if (culture == null || culture.ToLower() == "default" || culture.ToLower() == "" || culture.ToLower() == "en")
                {
                    if (!File.Exists(text + text3))
                    {
                        File.Create(text + text3).Close();
                        SetToDefault(text + text3);
                    }
                }
                else
                {
                    text3 = string.Format("{0}.{1}{2}", fileName, culture, text2);
                    if (!File.Exists(text + text3))
                    {
                        File.Create(text + text3).Close();
                        SetToDefault(text + text3);
                    }
                }
                messages = ReadMessagesFromStream(new FileStream(text + text3, FileMode.Open));
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        static MessagesCollection ReadMessagesFromStream(Stream stream)
        {
            MessagesCollection messagesCollection = new MessagesCollection();
            using (stream)
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    string text;
                    while ((text = streamReader.ReadLine()) != null && !text.StartsWith("#"))
                    {
                        text = text.Trim();
                        if (!(text == "") && !text.StartsWith("//") && !text.StartsWith("#") && text.IndexOf("=") != -1)
                        {
                            text = text.Trim(',');
                            string key = text.Substring(0, text.IndexOf("=")).Trim();
                            string input = text.Substring(text.IndexOf("=") + 1, text.Length - 1 - text.IndexOf("="));
                            Regex regex = new Regex("\"[^\"\\\\]*(?:\\\\.[^\"\\\\]*)*\"");
                            MatchCollection matchCollection = regex.Matches(input, 0);
                            string[] array = new string[matchCollection.Count];
                            for (int i = 0; i < matchCollection.Count; i++)
                            {
                                array[i] = matchCollection[i].Value.Remove(matchCollection[i].Value.Length - 1).Remove(0, 1).Replace("\\\"", "\"");
                            }
                            lock (messageAddLock)
                            {
                                messagesCollection.Add(key, array);
                            }
                        }
                    }
                }
            }
            return messagesCollection;
        }

        static void AddKeysToFile(MessagesCollection dmc)
        {
            using (StreamWriter streamWriter = File.AppendText("messages/" + fileName + ".txt"))
            {
                streamWriter.WriteLine();
                foreach (KeyValuePair<string, string[]> item in dmc)
                {
                    streamWriter.WriteLine(item.Key + " = " + StringArrayToMessageLine(item.Value));
                }
            }
        }

        static string StringArrayToMessageLine(string[] array)
        {
            string text = "";
            foreach (string text2 in array)
            {
                text = "\"" + text2 + "\" ,";
            }
            if (text.Length >= 2)
            {
                text = text.Remove(text.Length - 2, 2);
            }
            return text;
        }

        static void VerifyAndFix(MessagesCollection mc)
        {
            MessagesCollection messagesCollection = ReadMessagesFromStream(new MemoryStream(Encoding.Default.GetBytes(DefaultContent)));
            MessagesCollection messagesCollection2 = new MessagesCollection();
            foreach (KeyValuePair<string, string[]> item in messagesCollection)
            {
                if (!mc.ContainsKey(item.Key))
                {
                    mc.Add(item.Key, item.Value);
                    messagesCollection2.Add(item.Key, item.Value);
                }
            }
            if (messagesCollection2.Count > 0)
            {
                AddKeysToFile(messagesCollection2);
            }
        }
    }
}