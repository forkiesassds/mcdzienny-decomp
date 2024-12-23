using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace MCDzienny
{
    static class PidgeonLogger
    {
        static bool NeedRestart;

        static bool _disposed;

        static bool _reportBack;

        static readonly LogFile messagePath = new LogFile("logs/logs/", ".txt");

        static readonly LogFile errorPath = new LogFile("logs/errors/", "error.txt");

        static readonly LogFile commandPath = new LogFile("logs/commands/", "cmd.txt");

        static readonly object _lockObject = new object();

        static readonly object commandLogLock = new object();

        static readonly Timer saveLogsTimer = new Timer();

        static readonly Queue<string> _messageCache = new Queue<string>();

        static readonly Queue<string> commandCache = new Queue<string>();

        static readonly Queue<string> _errorCache = new Queue<string>();

        public static string MessageLogPath { get { return messagePath.GeneratedPath; } }

        public static string ErrorLogPath { get { return errorPath.GeneratedPath; } }

        public static void Init()
        {
            _reportBack = Server.reportBack;
            if (!Directory.Exists("logs"))
            {
                Directory.CreateDirectory("logs");
            }
            if (!Directory.Exists("logs/logs"))
            {
                Directory.CreateDirectory("logs/logs");
            }
            if (!Directory.Exists("logs/errors"))
            {
                Directory.CreateDirectory("logs/errors");
            }
            if (!Directory.Exists("logs/commands"))
            {
                Directory.CreateDirectory("logs/commands");
            }
            string[] files = Directory.GetFiles("logs");
            foreach (string text in files)
            {
                try
                {
                    if (Path.GetExtension(text) == ".txt")
                    {
                        File.Move(text, "logs\\" + text);
                    }
                }
                catch {}
            }
            saveLogsTimer.Elapsed += saveLogsTimer_Elapsed;
            saveLogsTimer.AutoReset = false;
            saveLogsTimer.Interval = 1000.0;
            saveLogsTimer.Start();
        }

        static void saveLogsTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_disposed)
            {
                return;
            }
            lock (_lockObject)
            {
                if (_errorCache.Count > 0)
                {
                    FlushCache(errorPath.GeneratedPath, _errorCache);
                }
                if (_messageCache.Count > 0)
                {
                    FlushCache(messagePath.GeneratedPath, _messageCache);
                }
                if (commandCache.Count > 0)
                {
                    FlushCache(commandPath.GeneratedPath, commandCache);
                }
            }
            saveLogsTimer.Start();
        }

        public static void LogMessage(string message)
        {
            try
            {
                if (message != null && message.Length > 0)
                {
                    lock (_lockObject)
                    {
                        _messageCache.Enqueue(message);
                        Monitor.Pulse(_lockObject);
                    }
                }
            }
            catch {}
        }

        public static void LogCommand(string message)
        {
            try
            {
                if (message != null && message.Length > 0)
                {
                    lock (commandLogLock)
                    {
                        commandCache.Enqueue(message);
                        Monitor.Pulse(commandLogLock);
                    }
                }
            }
            catch {}
        }

        public static void LogError(string message)
        {
            //IL_007b: Unknown result type (might be due to invalid IL or missing references)
            try
            {
                Server.s.ErrorCase(message);
                lock (_lockObject)
                {
                    _errorCache.Enqueue(message);
                    Monitor.Pulse(_lockObject);
                }
                if (NeedRestart)
                {
                    Server.listen.Close();
                    Server.Setup();
                    NeedRestart = false;
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText("ErrorLogError.log", getErrorText(ex));
                MessageBox.Show(ex.StackTrace + " " + ex.Message);
                Server.s.Log("There was an error in the error logger!");
            }
        }

        public static void LogError(Exception ex)
        {
            //IL_0085: Unknown result type (might be due to invalid IL or missing references)
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                Exception ex2 = ex;
                stringBuilder.AppendLine(string.Concat("----", DateTime.Now, " ----"));
                while (ex2 != null)
                {
                    stringBuilder.AppendLine(getErrorText(ex2));
                    ex2 = ex2.InnerException;
                }
                stringBuilder.AppendLine(new string('-', 25));
                LogError(stringBuilder.ToString());
            }
            catch (Exception ex3)
            {
                File.AppendAllText("ErrorLogError.log", getErrorText(ex3));
                MessageBox.Show(ex3.StackTrace + " " + ex3.Message);
                Server.s.Log("There was an error in the error logger!");
            }
        }

        static void FlushCache(string path, Queue<string> cache)
        {
            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(path, FileMode.Append, FileAccess.Write);
                while (cache.Count > 0)
                {
                    byte[] bytes = Encoding.Default.GetBytes(cache.Dequeue());
                    fileStream.Write(bytes, 0, bytes.Length);
                }
                fileStream.Close();
            }
            catch {}
            fileStream.Dispose();
        }

        static string getErrorText(Exception e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Type: " + e.GetType().Name);
            stringBuilder.AppendLine("Source: " + e.Source);
            stringBuilder.AppendLine("Message: " + e.Message);
            stringBuilder.AppendLine("Target: " + e.TargetSite.Name);
            stringBuilder.AppendLine("Trace: " + e.StackTrace);
            if (e.Message.IndexOf("An existing connection was forcibly closed by the remote host") != -1)
            {
                NeedRestart = true;
            }
            return stringBuilder.ToString();
        }

        public static void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            _disposed = true;
            lock (_lockObject)
            {
                if (_errorCache.Count > 0)
                {
                    FlushCache(errorPath.GeneratedPath, _errorCache);
                }
                _messageCache.Clear();
                Monitor.Pulse(_lockObject);
            }
        }
    }
}