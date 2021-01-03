using System;
using System.Collections.Generic;
using System.Data;

namespace MCDzienny
{
    // Token: 0x02000027 RID: 39
    internal static class SQLiteFront
    {
        // Token: 0x0400009E RID: 158
        private static readonly bool IsRunningMono = Type.GetType("Mono.Runtime") != null;

        // Token: 0x060000EA RID: 234 RVA: 0x000067C0 File Offset: 0x000049C0
        public static bool Transaction(string commandText, string[] parameters)
        {
            if (IsRunningMono) return SQLiteMono.Transaction(commandText, parameters);
            return SQLite.Transaction(commandText, parameters);
        }

        // Token: 0x060000EB RID: 235 RVA: 0x000067D8 File Offset: 0x000049D8
        public static bool ExecuteQuery(string queryString, Dictionary<string, object> parameters)
        {
            if (IsRunningMono) return SQLiteMono.ExecuteQuery(queryString, parameters);
            return SQLite.ExecuteQuery(queryString, parameters);
        }

        // Token: 0x060000EC RID: 236 RVA: 0x000067F0 File Offset: 0x000049F0
        public static DataTable fillData(string queryString, Dictionary<string, object> parameters,
            bool skipError = false)
        {
            if (IsRunningMono) return SQLiteMono.fillData(queryString, parameters, skipError);
            return SQLite.fillData(queryString, parameters, skipError);
        }

        // Token: 0x060000ED RID: 237 RVA: 0x0000680C File Offset: 0x00004A0C
        public static void BeginTransaction()
        {
            if (IsRunningMono)
            {
                SQLiteMono.BeginTransaction();
                return;
            }

            SQLite.BeginTransaction();
        }

        // Token: 0x060000EE RID: 238 RVA: 0x00006820 File Offset: 0x00004A20
        public static void CommitTransaction()
        {
            if (IsRunningMono)
            {
                SQLiteMono.CommitTransaction();
                return;
            }

            SQLite.CommitTransaction();
        }

        // Token: 0x060000EF RID: 239 RVA: 0x00006834 File Offset: 0x00004A34
        public static void EndTransaction()
        {
            if (IsRunningMono)
            {
                SQLiteMono.EndTransaction();
                return;
            }

            SQLite.EndTransaction();
        }

        // Token: 0x060000F0 RID: 240 RVA: 0x00006848 File Offset: 0x00004A48
        public static void TransQuery(string query)
        {
            if (IsRunningMono)
            {
                SQLiteMono.TransQuery(query);
                return;
            }

            SQLite.TransQuery(query);
        }
    }
}