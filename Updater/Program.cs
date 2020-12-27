using System;
using System.Windows.Forms;

namespace Updater
{
    // Token: 0x02000004 RID: 4
    internal static class Program
    {
        // Token: 0x06000008 RID: 8 RVA: 0x000020FD File Offset: 0x000002FD
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UpdaterWindow(args));
        }
    }
}