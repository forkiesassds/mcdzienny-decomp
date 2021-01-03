using System;

namespace MCDzienny
{
    // Token: 0x02000352 RID: 850
    public static class Logger
    {
        // Token: 0x170008EA RID: 2282
        // (get) Token: 0x0600185D RID: 6237 RVA: 0x000A4DB8 File Offset: 0x000A2FB8
        public static string LogPath
        {
            get { return PidgeonLogger.MessageLogPath; }
        }

        // Token: 0x170008EB RID: 2283
        // (get) Token: 0x0600185E RID: 6238 RVA: 0x000A4DC0 File Offset: 0x000A2FC0
        public static string ErrorLogPath
        {
            get { return PidgeonLogger.ErrorLogPath; }
        }

        // Token: 0x06001859 RID: 6233 RVA: 0x000A4D98 File Offset: 0x000A2F98
        public static void Write(string str)
        {
            PidgeonLogger.LogMessage(str);
        }

        // Token: 0x0600185A RID: 6234 RVA: 0x000A4DA0 File Offset: 0x000A2FA0
        public static void WriteError(Exception ex)
        {
            PidgeonLogger.LogError(ex);
        }

        // Token: 0x0600185B RID: 6235 RVA: 0x000A4DA8 File Offset: 0x000A2FA8
        public static void WriteError(string message)
        {
            PidgeonLogger.LogError(message);
        }

        // Token: 0x0600185C RID: 6236 RVA: 0x000A4DB0 File Offset: 0x000A2FB0
        public static void WriteCommand(string command)
        {
            PidgeonLogger.LogCommand(command);
        }

        // Token: 0x0600185F RID: 6239 RVA: 0x000A4DC8 File Offset: 0x000A2FC8
        public static void Dispose()
        {
            PidgeonLogger.Dispose();
        }
    }
}