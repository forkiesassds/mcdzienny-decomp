using System;

namespace MCDzienny
{
    public static class Logger
    {
        public static string LogPath { get { return PidgeonLogger.MessageLogPath; } }

        public static string ErrorLogPath { get { return PidgeonLogger.ErrorLogPath; } }

        public static void Write(string str)
        {
            PidgeonLogger.LogMessage(str);
        }

        public static void WriteError(Exception ex)
        {
            PidgeonLogger.LogError(ex);
        }

        public static void WriteError(string message)
        {
            PidgeonLogger.LogError(message);
        }

        public static void WriteCommand(string command)
        {
            PidgeonLogger.LogCommand(command);
        }

        public static void Dispose()
        {
            PidgeonLogger.Dispose();
        }
    }
}