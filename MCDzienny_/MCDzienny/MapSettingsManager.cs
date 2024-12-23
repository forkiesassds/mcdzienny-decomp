using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace MCDzienny
{
    public class MapSettingsManager
    {

        internal List<CommandBlock> commandBlocks;
        string settingsPath;

        public MapSettingsManager(string settingsPath)
        {
            Load(settingsPath);
        }

        public void Save()
        {
            Save(settingsPath);
        }

        public void Save(string newPath)
        {
            //IL_0007: Unknown result type (might be due to invalid IL or missing references)
            //IL_000d: Expected O, but got Unknown
            //IL_026f: Unknown result type (might be due to invalid IL or missing references)
            settingsPath = newPath;
            XmlDocument val = new XmlDocument();
            XmlNode val2 = val.CreateElement("MapSettings");
            val.AppendChild(val2);
            if (commandBlocks != null)
            {
                XmlNode val3 = val.CreateElement("CommandBlocks");
                foreach (CommandBlock commandBlock in commandBlocks)
                {
                    XmlElement val4 = val.CreateElement("CommandBlock");
                    val4.SetAttribute("x", commandBlock.x.ToString());
                    val4.SetAttribute("y", commandBlock.y.ToString());
                    val4.SetAttribute("z", commandBlock.z.ToString());
                    val4.SetAttribute("block", commandBlock.blockType);
                    if (commandBlock.changeAction.IsExplicit)
                    {
                        val4.SetAttribute("onChange", commandBlock.changeAction.Value.ToString());
                    }
                    if (commandBlock.commandElements != null)
                    {
                        foreach (CommandElement commandElement in commandBlock.commandElements)
                        {
                            XmlElement val5 = val.CreateElement("Command");
                            if (commandElement.blockTrigger.IsExplicit)
                            {
                                val5.SetAttribute("trigger", commandElement.blockTrigger.ToString());
                            }
                            if (commandElement.consoleUse.IsExplicit)
                            {
                                val5.SetAttribute("console", commandElement.consoleUse.ToString());
                            }
                            if (commandElement.cooldown.IsExplicit)
                            {
                                val5.SetAttribute("cooldown", commandElement.cooldown.ToString());
                            }
                            val5.InnerText = commandElement.commandString;
                            val4.AppendChild(val5);
                        }
                    }
                    if (commandBlock.actionElements != null)
                    {
                        foreach (ActionElement actionElement in commandBlock.actionElements)
                        {
                            XmlElement val6 = val.CreateElement("Action");
                            if (actionElement.blockTrigger.IsExplicit)
                            {
                                val6.SetAttribute("trigger", actionElement.blockTrigger.ToString());
                            }
                            val6.InnerText = actionElement.actionString;
                            val4.AppendChild(val6);
                        }
                    }
                    if (val4.ChildNodes.Count == 0)
                    {
                        throw new XmlException("No commands or actions are assigned to command block.");
                    }
                    val3.AppendChild(val4);
                }
                val2.AppendChild(val3);
            }
            val.AppendChild(val2);
            val.Save(settingsPath);
        }

        public void Reload()
        {
            Load(settingsPath);
        }

        public void Load(string settingsPath)
        {
            //IL_004b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0051: Expected O, but got Unknown
            //IL_009d: Unknown result type (might be due to invalid IL or missing references)
            //IL_00a4: Expected O, but got Unknown
            //IL_01bf: Unknown result type (might be due to invalid IL or missing references)
            //IL_01c6: Expected O, but got Unknown
            //IL_0389: Unknown result type (might be due to invalid IL or missing references)
            //IL_0390: Expected O, but got Unknown
            this.settingsPath = settingsPath;
            if (!File.Exists(settingsPath))
            {
                using (FileStream stream = File.Create(settingsPath))
                {
                    using (StreamWriter streamWriter = new StreamWriter(stream))
                    {
                        streamWriter.WriteLine("<MapSettings>");
                        streamWriter.Write("</MapSettings>");
                    }
                }
            }
            XmlDocument val = new XmlDocument();
            val.Load(settingsPath);
            XmlElement documentElement = val.DocumentElement;
            XmlNode val2 = documentElement.SelectSingleNode("CommandBlocks");
            if (val2 == null)
            {
                return;
            }
            commandBlocks = new List<CommandBlock>();
            foreach (XmlNode item in val2.SelectNodes("CommandBlock"))
            {
                XmlNode val3 = item;
                CommandBlock commandBlock = new CommandBlock();
                commandBlock.x = int.Parse(val3.Attributes["x"].Value);
                commandBlock.y = int.Parse(val3.Attributes["y"].Value);
                commandBlock.z = int.Parse(val3.Attributes["z"].Value);
                commandBlock.blockType = val3.Attributes["block"].Value;
                if (val3.Attributes["onChange"] != null)
                {
                    commandBlock.changeAction.Value = (ChangeAction)Enum.Parse(typeof(ChangeAction), val3.Attributes["onChange"].Value);
                    commandBlock.changeAction.IsExplicit = true;
                }
                else
                {
                    commandBlock.changeAction.Value = ChangeAction.Restore;
                    commandBlock.changeAction.IsExplicit = false;
                }
                foreach (XmlNode item2 in val3.SelectNodes("Command"))
                {
                    XmlNode val4 = item2;
                    CommandElement commandElement = new CommandElement();
                    if (val4.Attributes["trigger"] != null)
                    {
                        commandElement.blockTrigger.Value = (BlockTrigger)Enum.Parse(typeof(BlockTrigger), val4.Attributes["trigger"].Value);
                        commandElement.blockTrigger.IsExplicit = true;
                    }
                    else
                    {
                        if (Block.Walkthrough(Block.Byte(commandBlock.blockType)))
                        {
                            commandElement.blockTrigger.Value = BlockTrigger.Walk;
                        }
                        else
                        {
                            commandElement.blockTrigger.Value = BlockTrigger.Hit;
                        }
                        commandElement.blockTrigger.IsExplicit = false;
                    }
                    if (val4.Attributes["asConsole"] != null)
                    {
                        commandElement.consoleUse.Value = bool.Parse(val4.Attributes["asConsole"].Value);
                        commandElement.consoleUse.IsExplicit = true;
                    }
                    else
                    {
                        commandElement.consoleUse.Value = false;
                        commandElement.consoleUse.IsExplicit = false;
                    }
                    if (val4.Attributes["cooldown"] != null)
                    {
                        commandElement.cooldown.Value = float.Parse(val4.Attributes["cooldown"].Value);
                        commandElement.cooldown.IsExplicit = true;
                    }
                    else
                    {
                        commandElement.cooldown.Value = 1f;
                        commandElement.cooldown.IsExplicit = false;
                    }
                    commandElement.commandString = val4.InnerText;
                    commandBlock.commandElements.Add(commandElement);
                }
                foreach (XmlNode item3 in val3.SelectNodes("Action"))
                {
                    XmlNode val5 = item3;
                    ActionElement actionElement = new ActionElement();
                    if (val5.Attributes["trigger"] != null)
                    {
                        actionElement.blockTrigger.Value = (BlockTrigger)Enum.Parse(typeof(BlockTrigger), val5.Attributes["trigger"].Value);
                        actionElement.blockTrigger.IsExplicit = true;
                    }
                    else
                    {
                        if (Block.Walkthrough(Block.Byte(commandBlock.blockType)))
                        {
                            actionElement.blockTrigger.Value = BlockTrigger.Walk;
                        }
                        else
                        {
                            actionElement.blockTrigger.Value = BlockTrigger.Hit;
                        }
                        actionElement.blockTrigger.IsExplicit = false;
                    }
                    actionElement.actionString = val5.InnerText;
                    commandBlock.actionElements.Add(actionElement);
                }
                commandBlocks.Add(commandBlock);
            }
        }

        public void DeployBlocks(Level level)
        {
            if (commandBlocks == null)
            {
                return;
            }
            foreach (CommandBlock commandBlock in commandBlocks)
            {
                byte b = Block.Byte(commandBlock.blockType);
                if (b != byte.MaxValue)
                {
                    level.Blockchange((ushort)commandBlock.x, (ushort)commandBlock.y, (ushort)commandBlock.z, b, overRide: true);
                }
            }
        }
    }
}