using System.Collections.Generic;
using System.Net;
using MCDzienny.Misc;

namespace MCDzienny.Commands
{
    public class CmdRemote : Command
    {

        readonly List<CaseAction> caseActionList;
        readonly SyntaxCaseSolver caseSolver;

        public CmdRemote()
        {
            caseActionList = new List<CaseAction>();
            caseActionList.Add(new CaseAction(Add, "add"));
            caseActionList.Add(new CaseAction(List, "list"));
            caseActionList.Add(new CaseAction(Remove, "remove"));
            caseActionList.Add(new CaseAction(Ban, "ban"));
            caseActionList.Add(new CaseAction(UnBan, "unban"));
            caseSolver = new SyntaxCaseSolver(caseActionList, Help);
        }

        public override string name { get { return "remote"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Nobody; } }

        public override bool HighSecurity { get { return true; } }

        public void UnBan(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            IPAddress ip;
            if (!IPAddress.TryParse(message, out ip))
            {
                Player.SendMessage(p, message + " is not a valid IP address.");
                return;
            }
            if (!Server.bannedRemoteAccounts.BannnedIPs.Contains(message))
            {
                Player.SendMessage(p, message + " is not banned.");
                return;
            }
            Server.bannedRemoteAccounts.BannnedIPs.Remove(message);
            Server.bannedRemoteAccounts.Save();
            Player.SendMessage(p, message + " was unbanned.");
        }

        public void Ban(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            IPAddress ip;
            if (!IPAddress.TryParse(message, out ip))
            {
                Player.SendMessage(p, message + " is not a valid IP address.");
                return;
            }
            if (Server.bannedRemoteAccounts.BannnedIPs.Contains(message))
            {
                Player.SendMessage(p, message + " is already banned.");
                return;
            }
            Server.bannedRemoteAccounts.BannnedIPs.Add(message);
            Server.bannedRemoteAccounts.Save();
            Player.SendMessage(p, message + " was banned!");
        }

        public void Remove(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            Server.remoteAccounts.Accounts.Remove(message);
            Server.remoteAccounts.Save();
            Player.SendMessage(p, message + " remote account was removed.");
        }

        public void Add(Player p, string message)
        {
            if (message.Split(' ').Length < 2)
            {
                Help(p);
                return;
            }
            string text = message.Split(' ')[0];
            string plainPassword = message.Split(' ')[1];
            if (Server.remoteAccounts.Accounts.ContainsKey(text))
            {
                Player.SendMessage(p, "Account named " + text + " already exists.");
                return;
            }
            Server.remoteAccounts.Accounts.Add(text, plainPassword);
            Server.remoteAccounts.Save();
            Player.SendMessage(p, "Remote account for " + text + " was created.");
        }

        public void List(Player p, string message)
        {
            Player.SendMessage(p, "Remote accounts:");
            foreach (string key in Server.remoteAccounts.Accounts.Keys)
            {
                Player.SendMessage(p, key);
            }
        }

        public override void Use(Player p, string message)
        {
            caseSolver.Process(p, message);
        }

        public void Help(Player p, string message)
        {
            Help(p);
        }

        public override void Help(Player p)
        {
            Player.SendMessage2(p, "/remote add [name] [password] - adds a new remote account with a given name and password.");
            Player.SendMessage2(p, "/remote remove [name] - removes a remote account.");
            Player.SendMessage2(p, "/remote list - list of accounts.");
            Player.SendMessage2(p, "/remote ban [ip] - bans ip from accessing remote authentication service.");
        }
    }
}