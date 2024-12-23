using System;
using System.Collections.Generic;
using System.Data;

namespace MCDzienny
{
    static class SQLiteFront
    {
        static readonly bool IsRunningMono;

        static SQLiteFront()
        {
            IsRunningMono = Type.GetType("Mono.Runtime") != null;
        }

        public static bool Transaction(string commandText, string[] parameters)
        {
            if (IsRunningMono)
            {
                return SQLiteMono.Transaction(commandText, parameters);
            }
            return SQLite.Transaction(commandText, parameters);
        }

        public static bool ExecuteQuery(string queryString, Dictionary<string, object> parameters)
        {
            if (IsRunningMono)
            {
                return SQLiteMono.ExecuteQuery(queryString, parameters);
            }
            return SQLite.ExecuteQuery(queryString, parameters);
        }

        public static DataTable fillData(string queryString, Dictionary<string, object> parameters, bool skipError = false)
        {
            if (IsRunningMono)
            {
                return SQLiteMono.fillData(queryString, parameters, skipError);
            }
            return SQLite.fillData(queryString, parameters, skipError);
        }

        public static void BeginTransaction()
        {
            if (IsRunningMono)
            {
                SQLiteMono.BeginTransaction();
            }
            else
            {
                SQLite.BeginTransaction();
            }
        }

        public static void CommitTransaction()
        {
            if (IsRunningMono)
            {
                SQLiteMono.CommitTransaction();
            }
            else
            {
                SQLite.CommitTransaction();
            }
        }

        public static void EndTransaction()
        {
            if (IsRunningMono)
            {
                SQLiteMono.EndTransaction();
            }
            else
            {
                SQLite.EndTransaction();
            }
        }

        public static void TransQuery(string query)
        {
            if (IsRunningMono)
            {
                SQLiteMono.TransQuery(query);
            }
            else
            {
                SQLite.TransQuery(query);
            }
        }
    }
}