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
    public static class Program
    {
        public static bool usingConsole;

        public static bool CurrentUpdate = false;

        public static Timer updateTimer = new Timer(7200000.0);

        public static void GlobalExHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            Server.ErrorLog(ex);
            if (!Server.restartOnError)
            {
                restartMe();
            }
            else
            {
                restartMe(fullRestart: false);
            }
        }

        public static void ThreadExHandler(object sender, ThreadExceptionEventArgs e)
        {
            Exception exception = e.Exception;
            Server.ErrorLog(exception);
            if (!Server.restartOnError)
            {
                restartMe();
            }
            else
            {
                restartMe(fullRestart: false);
            }
        }

        public static void Main(string[] args)
        {
            //IL_0080: Unknown result type (might be due to invalid IL or missing references)
            //IL_013f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0172: Unknown result type (might be due to invalid IL or missing references)
            //IL_0163: Unknown result type (might be due to invalid IL or missing references)
            if (Process.GetProcessesByName("MCDziennyLava").Length != 1)
            {
                Process[] processesByName = Process.GetProcessesByName("MCDziennyLava");
                foreach (Process process in processesByName)
                {
                    if (process.MainModule.BaseAddress == Process.GetCurrentProcess().MainModule.BaseAddress && process.Id != Process.GetCurrentProcess().Id)
                    {
                        process.Kill();
                    }
                }
            }
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
                Server server = new Server();
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
                FormWindowState startState = 0;
                foreach (string text in args)
                {
                    if (text.ToLower().Contains("totray"))
                    {
                        startState = (FormWindowState)1;
                    }
                }
                Application.Run(new Window(startState));
            }
            catch (Exception ex2)
            {
                Server.ErrorLog(ex2);
            }
        }

        public static void handleComm(string s)
        {
            string text = "";
            string text2 = "";
            if (s.IndexOf(' ') != -1)
            {
                text = s.Split(' ')[0];
                text2 = s.Substring(s.IndexOf(' ') + 1);
            }
            else
            {
                if (!(s != ""))
                {
                    goto IL_00f5;
                }
                text = s;
            }
            if (s.Length > 1 && s[0] == '/')
            {
                try
                {
                    Command command = Command.all.Find(text.Substring(1));
                    if (command != null)
                    {
                        if (!command.ConsoleAccess)
                        {
                            Console.WriteLine("You can't use this command from console.");
                            handleComm(Console.ReadLine());
                        }
                        else
                        {
                            command.Use(null, text2);
                            Console.WriteLine("CONSOLE: USED " + text + " " + text2);
                            handleComm(Console.ReadLine());
                        }
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
            }
            IL_00f5:
            handleComm("/say " + Group.findPerm(LevelPermission.Admin).color + "Console: &f" + s);
            handleComm(Console.ReadLine());
        }

        public static void ExitProgram(bool AutoRestart)
        {
            Server.StartExiting();
            try
            {
                if (!Server.CLI && Window.thisWindow.notifyIcon1 != null)
                {
                    Window.thisWindow.notifyIcon1.Visible = false;
                }
            }
            catch {}
            SaveAll();
            if (AutoRestart)
            {
                restartMe();
            }
            Application.Exit();
        }

        public static void restartMe(bool fullRestart = true)
        {
            Thread thread = new Thread((ThreadStart)delegate
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
                catch {}
                if (Server.listen != null)
                {
                    Server.listen.Close();
                }
                if (!Server.mono || fullRestart)
                {
                    Server.restarting = true;
                    Application.Restart();
                    Server.process.Kill();
                }
                else
                {
                    Server.s.Start();
                }
            });
            thread.Start();
        }

        public static bool SaveAll(bool restart = true)
        {
            try
            {
                Player.players.ForEach(delegate(Player p)
                {
                    if (restart)
                    {
                        p.Kick("Server restarts! Rejoin in a moment!");
                    }
                    else
                    {
                        p.Kick("Server is offline.");
                    }
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
                        level = level + l.name + "=" + l.physics + Environment.NewLine;
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