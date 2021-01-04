using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using MCDzienny.Misc;
using Timer = System.Timers.Timer;

namespace MCDzienny
{
    // Token: 0x02000354 RID: 852
    internal static class PidgeonLogger
    {
        // Token: 0x04000CA0 RID: 3232
        private static bool NeedRestart;

        // Token: 0x04000CA1 RID: 3233
        private static bool _disposed;

        // Token: 0x04000CA2 RID: 3234
        private static bool _reportBack;

        // Token: 0x04000CA3 RID: 3235
        private static readonly LogFile messagePath = new LogFile("logs/logs/", ".txt");

        // Token: 0x04000CA4 RID: 3236
        private static readonly LogFile errorPath = new LogFile("logs/errors/", "error.txt");

        // Token: 0x04000CA5 RID: 3237
        private static readonly LogFile commandPath = new LogFile("logs/commands/", "cmd.txt");

        // Token: 0x04000CA6 RID: 3238
        private static readonly object _lockObject = new object();

        // Token: 0x04000CA7 RID: 3239
        private static readonly object commandLogLock = new object();

        // Token: 0x04000CA8 RID: 3240
        private static readonly Timer saveLogsTimer = new Timer();

        // Token: 0x04000CA9 RID: 3241
        private static readonly Queue<string> _messageCache = new Queue<string>();

        // Token: 0x04000CAA RID: 3242
        private static readonly Queue<string> commandCache = new Queue<string>();

        // Token: 0x04000CAB RID: 3243
        private static readonly Queue<string> _errorCache = new Queue<string>();

        // Token: 0x170008EF RID: 2287
        // (get) Token: 0x06001868 RID: 6248 RVA: 0x000A5004 File Offset: 0x000A3204
        public static string MessageLogPath
        {
            get { return messagePath.GeneratedPath; }
        }

        // Token: 0x170008F0 RID: 2288
        // (get) Token: 0x06001869 RID: 6249 RVA: 0x000A5010 File Offset: 0x000A3210
        public static string ErrorLogPath
        {
            get { return errorPath.GeneratedPath; }
        }

        // Token: 0x06001866 RID: 6246 RVA: 0x000A4E50 File Offset: 0x000A3050
        public static void Init()
        {
            _reportBack = Server.reportBack;
            if (!Directory.Exists("logs")) Directory.CreateDirectory("logs");
            if (!Directory.Exists("logs/logs")) Directory.CreateDirectory("logs/logs");
            if (!Directory.Exists("logs/errors")) Directory.CreateDirectory("logs/errors");
            if (!Directory.Exists("logs/commands")) Directory.CreateDirectory("logs/commands");
            foreach (var text in Directory.GetFiles("logs"))
                try
                {
                    if (Path.GetExtension(text) == ".txt") File.Move(text, "logs\\" + text);
                }
                catch
                {
                }

            saveLogsTimer.Elapsed += saveLogsTimer_Elapsed;
            saveLogsTimer.AutoReset = false;
            saveLogsTimer.Interval = 1000.0;
            saveLogsTimer.Start();
        }

        // Token: 0x06001867 RID: 6247 RVA: 0x000A4F58 File Offset: 0x000A3158
        private static void saveLogsTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_disposed)
            {
                lock (_lockObject)
                {
                    if (_errorCache.Count > 0) FlushCache(errorPath.GeneratedPath, _errorCache);
                    if (_messageCache.Count > 0) FlushCache(messagePath.GeneratedPath, _messageCache);
                    if (commandCache.Count > 0) FlushCache(commandPath.GeneratedPath, commandCache);
                }

                saveLogsTimer.Start();
            }
        }

        // Token: 0x0600186A RID: 6250 RVA: 0x000A501C File Offset: 0x000A321C
        public static void LogMessage(string message)
        {
            try
            {
                if (message != null && message.Length > 0)
                    lock (_lockObject)
                    {
                        _messageCache.Enqueue(message);
                        Monitor.Pulse(_lockObject);
                    }
            }
            catch
            {
            }
        }

        // Token: 0x0600186B RID: 6251 RVA: 0x000A5080 File Offset: 0x000A3280
        public static void LogCommand(string message)
        {
            try
            {
                if (message != null && message.Length > 0)
                    lock (commandLogLock)
                    {
                        commandCache.Enqueue(message);
                        Monitor.Pulse(commandLogLock);
                    }
            }
            catch
            {
            }
        }

        // Token: 0x0600186C RID: 6252 RVA: 0x000A50E4 File Offset: 0x000A32E4
        public static void LogError(string message)
        {
            try
            {
                Server.s.ErrorCase(message);
                lock (_lockObject)
                {
                    _errorCache.Enqueue(message);
                    Monitor.Pulse(_lockObject);
                }
                if (NeedRestart == true)
                {
                    Server.listen.Close();
                    Server.Setup();
                    NeedRestart = false;
                }
            }
            catch (Exception e)
            {
                File.AppendAllText("ErrorLogError.log", getErrorText(e));
                MessageBox.Show(e.StackTrace + " " + e.Message);
                Server.s.Log("There was an error in the error logger!");
            }
        }

        // Token: 0x0600186D RID: 6253 RVA: 0x000A51A0 File Offset: 0x000A33A0
        public static void LogError(Exception ex)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("----" + DateTime.Now + " ----");
                while (ex != null)
                {
                    sb.AppendLine(getErrorText(ex));
                    ex = ex.InnerException;
                }

                sb.AppendLine(new string('-', 25));

                if (!sb.ToString().IsNullOrWhiteSpaced())
                {
                    Server.s.ErrorCase(sb.ToString());
                }
                lock (_lockObject)
                {
                    _errorCache.Enqueue(sb.ToString());
                    Monitor.Pulse(_lockObject);
                }



                if (NeedRestart == true)
                {
                    Server.listen.Close();
                    Server.Setup();

                    NeedRestart = false;
                }
            }
            catch (Exception e)
            {
                File.AppendAllText("ErrorLogError.log", getErrorText(e));
                MessageBox.Show(e.StackTrace + " " + e.Message);
                Server.s.Log("There was an error in the error logger!");
            }
        }

        // Token: 0x0600186E RID: 6254 RVA: 0x000A525C File Offset: 0x000A345C
        private static void FlushCache(string path, Queue<string> cache)
        {
            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(path, FileMode.Append, FileAccess.Write);
                while (cache.Count > 0)
                {
                    var bytes = Encoding.Default.GetBytes(cache.Dequeue());
                    fileStream.Write(bytes, 0, bytes.Length);
                }

                fileStream.Close();
            }
            catch
            {
            }

            fileStream.Dispose();
        }

        // Token: 0x0600186F RID: 6255 RVA: 0x000A52BC File Offset: 0x000A34BC
        private static string getErrorText(Exception e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Type: " + e.GetType().Name);
            sb.AppendLine("Source: " + e.Source);
            sb.AppendLine("Message: " + e.Message);
            sb.AppendLine("Target: " + e.TargetSite.Name);
            sb.AppendLine("Trace: " + e.StackTrace);
            if (e.Message.IndexOf("An existing connection was forcibly closed by the remote host") != -1)
            {
                NeedRestart = true;
            }
            return sb.ToString();
        }

        // Token: 0x06001870 RID: 6256 RVA: 0x000A536C File Offset: 0x000A356C
        public static void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                lock (_lockObject)
                {
                    if (_errorCache.Count > 0) FlushCache(errorPath.GeneratedPath, _errorCache);
                    _messageCache.Clear();
                    Monitor.Pulse(_lockObject);
                }
            }
        }
    }
}