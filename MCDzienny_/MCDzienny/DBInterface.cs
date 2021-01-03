using System;
using System.Collections.Generic;
using System.Data;

namespace MCDzienny
{
    // Token: 0x02000152 RID: 338
    public class DBInterface
    {
        // Token: 0x060009C2 RID: 2498 RVA: 0x000336CC File Offset: 0x000318CC
        public static void ExecuteQuery(string queryString)
        {
            ExecuteQuery(queryString, false);
        }

        // Token: 0x060009C3 RID: 2499 RVA: 0x000336D8 File Offset: 0x000318D8
        public static void ExecuteQuery(string queryString, bool createDB)
        {
            if (Server.useMySQL)
            {
                MySQL.ExecuteQuery(queryString, new Dictionary<string, object>(), createDB);
                return;
            }

            SQLiteFront.ExecuteQuery(queryString, new Dictionary<string, object>());
        }

        // Token: 0x060009C4 RID: 2500 RVA: 0x000336FC File Offset: 0x000318FC
        public static void ExecuteQuery(string queryString, Dictionary<string, object> parameters)
        {
            if (Server.useMySQL)
            {
                MySQL.ExecuteQuery(queryString, parameters);
                return;
            }

            SQLiteFront.ExecuteQuery(queryString, parameters);
        }

        // Token: 0x060009C5 RID: 2501 RVA: 0x00033718 File Offset: 0x00031918
        public static DataTable fillData(string queryString)
        {
            return fillData(queryString, false);
        }

        // Token: 0x060009C6 RID: 2502 RVA: 0x00033724 File Offset: 0x00031924
        public static DataTable fillData(string query, Dictionary<string, object> parameters)
        {
            if (Server.useMySQL) return MySQL.fillData(query, parameters);
            return SQLiteFront.fillData(query, parameters);
        }

        // Token: 0x060009C7 RID: 2503 RVA: 0x00033740 File Offset: 0x00031940
        public static DataTable fillData(string queryString, bool skipError)
        {
            if (Server.useMySQL) return MySQL.fillData(queryString, new Dictionary<string, object>(), skipError);
            return SQLiteFront.fillData(queryString, new Dictionary<string, object>(), skipError);
        }

        // Token: 0x060009C8 RID: 2504 RVA: 0x00033764 File Offset: 0x00031964
        public static bool Transaction(string commandText, string[] parameters)
        {
            if (Server.useMySQL) throw new NotSupportedException("Transactions for MySql are not supported.");
            return SQLiteFront.Transaction(commandText, parameters);
        }

        // Token: 0x060009C9 RID: 2505 RVA: 0x00033780 File Offset: 0x00031980
        public static void BeginTransaction()
        {
            if (Server.useMySQL) throw new NotSupportedException("Not supported for MySQL.");
            SQLiteFront.BeginTransaction();
        }

        // Token: 0x060009CA RID: 2506 RVA: 0x0003379C File Offset: 0x0003199C
        public static void CommitTransaction()
        {
            if (Server.useMySQL) throw new NotSupportedException("Not supported for MySQL.");
            SQLiteFront.CommitTransaction();
        }

        // Token: 0x060009CB RID: 2507 RVA: 0x000337B8 File Offset: 0x000319B8
        public static void EndTransaction()
        {
            if (Server.useMySQL) throw new NotSupportedException("Not supported for MySQL.");
            SQLiteFront.EndTransaction();
        }

        // Token: 0x060009CC RID: 2508 RVA: 0x000337D4 File Offset: 0x000319D4
        public static void TransQuery(string query)
        {
            if (Server.useMySQL) throw new NotSupportedException("Not supported for MySQL.");
            SQLiteFront.TransQuery(query);
        }
    }
}