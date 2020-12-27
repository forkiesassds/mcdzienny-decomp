using System;
using System.IO;

namespace MCDzienny
{
    // Token: 0x020000B9 RID: 185
    public class CmdCmdCreate : Command
    {
        // Token: 0x170002B8 RID: 696
        // (get) Token: 0x06000649 RID: 1609 RVA: 0x00021394 File Offset: 0x0001F594
        public override string name
        {
            get { return "cmdcreate"; }
        }

        // Token: 0x170002B9 RID: 697
        // (get) Token: 0x0600064A RID: 1610 RVA: 0x0002139C File Offset: 0x0001F59C
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170002BA RID: 698
        // (get) Token: 0x0600064B RID: 1611 RVA: 0x000213A4 File Offset: 0x0001F5A4
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170002BB RID: 699
        // (get) Token: 0x0600064C RID: 1612 RVA: 0x000213AC File Offset: 0x0001F5AC
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170002BC RID: 700
        // (get) Token: 0x0600064D RID: 1613 RVA: 0x000213B0 File Offset: 0x0001F5B0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x0600064F RID: 1615 RVA: 0x000213BC File Offset: 0x0001F5BC
        public override void Use(Player p, string message)
        {
            if (message == "" || message.IndexOf(' ') != -1)
            {
                Help(p);
                return;
            }

            if (File.Exists("extra/commands/source/Cmd" + message + ".cs"))
            {
                Player.SendMessage(p, string.Format("File Cmd{0}.cs already exists.  Choose another name.", message));
                return;
            }

            try
            {
                Scripting.CreateNew(message);
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
                Player.SendMessage(p, "An error occurred creating the class file.");
                return;
            }

            Player.SendMessage(p, "Successfully created a new command class.");
        }

        // Token: 0x06000650 RID: 1616 RVA: 0x0002144C File Offset: 0x0001F64C
        public override void Help(Player p)
        {
            Player.SendMessage(p,
                "/cmdcreate <message> - Creates a dummy command class named Cmd<Message> from which you can make a new command.");
        }
    }
}