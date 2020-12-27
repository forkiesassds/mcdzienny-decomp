using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using MCDzienny;
using MCDzienny.Gui;
using MCDzienny.Updaters;
using Timer = System.Timers.Timer;

namespace MCDzienny_.Gui
{
    // Token: 0x0200034B RID: 843
    public static class Program
    {
        // Token: 0x04000C7D RID: 3197
        public static bool usingConsole;

        // Token: 0x04000C7E RID: 3198
        public static bool CurrentUpdate = false;

        // Token: 0x04000C7F RID: 3199
        public static Timer updateTimer = new Timer(7200000.0);

        // Token: 0x0600182E RID: 6190 RVA: 0x000A2C2C File Offset: 0x000A0E2C
        public static void GlobalExHandler(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception) e.ExceptionObject;
            Server.ErrorLog(ex);
            if (!Server.restartOnError)
            {
                restartMe();
                return;
            }

            restartMe(false);
        }

        // Token: 0x0600182F RID: 6191 RVA: 0x000A2C60 File Offset: 0x000A0E60
        public static void ThreadExHandler(object sender, ThreadExceptionEventArgs e)
        {
            var exception = e.Exception;
            Server.ErrorLog(exception);
            if (!Server.restartOnError)
            {
                restartMe();
                return;
            }

            restartMe(false);
        }

        // Token: 0x06001830 RID: 6192 RVA: 0x000A2C90 File Offset: 0x000A0E90
        public static void Main(string[] args)
        {
            if (Process.GetProcessesByName("MCDziennyLava").Length != 1)
                foreach (var process in Process.GetProcessesByName("MCDziennyLava"))
                    if (process.MainModule.BaseAddress == Process.GetCurrentProcess().MainModule.BaseAddress &&
                        process.Id != Process.GetCurrentProcess().Id)
                        process.Kill();
            try
            {
                new SupplementaryUpdate().DownloadMissingFiles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Environment.Exit(0);
            }

            PidgeonLogger.Init();
            AppDomain.CurrentDomain.UnhandledException += GlobalExHandler;
            Application.ThreadException += ThreadExHandler;
            if (Server.CLI)
            {
                Server.CLI = true;
                var server = new Server();
                server.OnLog += Console.WriteLine;
                server.OnCommand += Console.WriteLine;
                server.OnSystem += Console.WriteLine;
                server.Start();
                Console.Title = Server.name + " MCDzienny Version: " + Server.Version;
                usingConsole = true;
                handleComm(Console.ReadLine());
                return;
            }

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                var startState = FormWindowState.Normal;
                foreach (var text in args)
                    if (text.ToLower().Contains("totray"))
                        startState = FormWindowState.Minimized;
                Application.Run(new Window(startState));
            }
            catch (Exception ex2)
            {
                Server.ErrorLog(ex2);
            }
        }

        // Token: 0x06001831 RID: 6193 RVA: 0x000A2E44 File Offset: 0x000A1044
        public static void handleComm(string s)
        {
            var text = "";
            string text2;
            if (s.IndexOf(' ') != -1)
            {
                text2 = s.Split(' ')[0];
                text = s.Substring(s.IndexOf(' ') + 1);
            }
            else
            {
                if (!(s != "")) goto IL_F5;
                text2 = s;
            }

            if (s.Length > 1 && s[0] == '/')
                try
                {
                    var command = Command.all.Find(text2.Substring(1));
                    if (command != null)
                    {
                        if (!command.ConsoleAccess)
                        {
                            Console.WriteLine("You can't use this command from console.");
                            handleComm(Console.ReadLine());
                            return;
                        }

                        command.Use(null, text);
                        Console.WriteLine("CONSOLE: USED " + text2 + " " + text);
                        handleComm(Console.ReadLine());
                        return;
                    }

                    Console.WriteLine("Typed command wasn't found.");
                }
                catch (Exception ex)
                {
                    Server.ErrorLog(ex);
                    Console.WriteLine("CONSOLE: Failed command.");
                    handleComm(Console.ReadLine());
                    return;
                }

            IL_F5:
            handleComm("/say " + Group.findPerm(LevelPermission.Admin).color + "Console: &f" + s);
            handleComm(Console.ReadLine());
        }

        // Token: 0x06001832 RID: 6194 RVA: 0x000A2F84 File Offset: 0x000A1184
        public static void ExitProgram(bool AutoRestart)
        {
            Server.StartExiting();
            try
            {
                if (!Server.CLI && Window.thisWindow.notifyIcon1 != null) Window.thisWindow.notifyIcon1.Visible = false;
            }
            catch
            {
            }

            SaveAll();
            if (AutoRestart) restartMe();
            Application.Exit();
        }

        // Token: 0x06001833 RID: 6195 RVA: 0x000A2FE4 File Offset: 0x000A11E4
        public static void restartMe(bool fullRestart = true)
        {
            var thread = new Thread(delegate()
            {
                SaveAll();
                Server.shuttingDown = true;
                try
                {
                    if (!Server.CLI && Window.thisWindow.notifyIcon1 != null)
                    {
                        Window.thisWindow.notifyIcon1.Icon = null;
                        Window.thisWindow.notifyIcon1.Visible = false;
                    }
                }
                catch
                {
                }

                if (Server.listen != null) Server.listen.Close();
                if (!Server.mono || fullRestart)
                {
                    Server.restarting = true;
                    Application.Restart();
                    Server.process.Kill();
                    return;
                }

                Server.s.Start();
            });
            thread.Start();
        }

        // Token: 0x06001834 RID: 6196 RVA: 0x000A3018 File Offset: 0x000A1218
        public static bool SaveAll(bool restart = true)
        {
            try
            {
                Player.players.ForEach(delegate(Player p)
                {
                    if (restart)
                    {
                        p.Kick("Server restarts! Rejoin in a moment!");
                        return;
                    }

                    p.Kick("Server is offline.");
                });
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
                return false;
            }

            try
            {
                string level = null;
                Server.levels.ForEach(delegate(Level l)
                {
                    if (l.mapType != MapType.Lava && l.mapType != MapType.Zombie)
                    {
                        level = string.Concat(level, l.name, "=", l.physics, Environment.NewLine);
                        l.Save();
                        l.SaveChanges();
                    }
                });
                File.WriteAllText("text/autoload.txt", level);
            }
            catch (Exception ex2)
            {
                Server.ErrorLog(ex2);
                return false;
            }

            return true;
        }
    }
}