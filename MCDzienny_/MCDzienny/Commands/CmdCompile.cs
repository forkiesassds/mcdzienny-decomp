using System;

namespace MCDzienny
{
    // Token: 0x020000BC RID: 188
    public class CmdCompile : Command
    {
        // Token: 0x170002C7 RID: 711
        // (get) Token: 0x06000661 RID: 1633 RVA: 0x00021578 File Offset: 0x0001F778
        public override string name
        {
            get { return "compile"; }
        }

        // Token: 0x170002C8 RID: 712
        // (get) Token: 0x06000662 RID: 1634 RVA: 0x00021580 File Offset: 0x0001F780
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170002C9 RID: 713
        // (get) Token: 0x06000663 RID: 1635 RVA: 0x00021588 File Offset: 0x0001F788
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170002CA RID: 714
        // (get) Token: 0x06000664 RID: 1636 RVA: 0x00021590 File Offset: 0x0001F790
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170002CB RID: 715
        // (get) Token: 0x06000665 RID: 1637 RVA: 0x00021594 File Offset: 0x0001F794
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x06000667 RID: 1639 RVA: 0x000215A0 File Offset: 0x0001F7A0
        public override void Use(Player p, string message)
        {
            if (message == "" || message.IndexOf(' ') != -1)
            {
                Help(p);
                return;
            }

            var flag = false;
            try
            {
                flag = Scripting.Compile(message);
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
                Player.SendMessage(p, "An exception was thrown during compilation.");
                return;
            }

            if (flag)
            {
                Player.SendMessage(p, "Compiled successfully.");
                return;
            }

            Player.SendMessage(p, "Compilation error.  Please check logs/compiler/errors.txt for more information.");
        }

        // Token: 0x06000668 RID: 1640 RVA: 0x00021618 File Offset: 0x0001F818
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/compile <class name> - Compiles a command class file into a DLL.");
        }
    }
}