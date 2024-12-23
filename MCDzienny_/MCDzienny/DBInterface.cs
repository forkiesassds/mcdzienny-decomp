using System;
using System.Collections.Generic;
using System.Data;

namespace MCDzienny
{
    public class DBInterface
    {
        public static void ExecuteQuery(string queryString)
        {
            ExecuteQuery(queryString, createDB: false);
        }

        public static void ExecuteQuery(string queryString, bool createDB)
        {
            if (Server.useMySQL)
            {
                MySQL.ExecuteQuery(queryString, new Dictionary<string, object>(), createDB);
            }
            else
            {
                SQLiteFront.ExecuteQuery(queryString, new Dictionary<string, object>());
            }
        }

        public static void ExecuteQuery(string queryString, Dictionary<string, object> parameters)
        {
            if (Server.useMySQL)
            {
                MySQL.ExecuteQuery(queryString, parameters);
            }
            else
            {
                SQLiteFront.ExecuteQuery(queryString, parameters);
            }
        }

        public static DataTable fillData(string queryString)
        {
            return fillData(queryString, skipError: false);
        }

        public static DataTable fillData(string query, Dictionary<string, object> parameters)
        {
            if (Server.useMySQL)
            {
                return MySQL.fillData(query, parameters);
            }
            return SQLiteFront.fillData(query, parameters);
        }

        public static DataTable fillData(string queryString, bool skipError)
        {
            if (Server.useMySQL)
            {
                return MySQL.fillData(queryString, new Dictionary<string, object>(), skipError);
            }
            return SQLiteFront.fillData(queryString, new Dictionary<string, object>(), skipError);
        }

        public static bool Transaction(string commandText, string[] parameters)
        {
            if (Server.useMySQL)
            {
                throw new NotSupportedException("Transactions for MySql are not supported.");
            }
            return SQLiteFront.Transaction(commandText, parameters);
        }

        public static void BeginTransaction()
        {
            if (Server.useMySQL)
            {
                throw new NotSupportedException("Not supported for MySQL.");
            }
            SQLiteFront.BeginTransaction();
        }

        public static void CommitTransaction()
        {
            if (Server.useMySQL)
            {
                throw new NotSupportedException("Not supported for MySQL.");
            }
            SQLiteFront.CommitTransaction();
        }

        public static void EndTransaction()
        {
            if (Server.useMySQL)
            {
                throw new NotSupportedException("Not supported for MySQL.");
            }
            SQLiteFront.EndTransaction();
        }

        public static void TransQuery(string query)
        {
            if (Server.useMySQL)
            {
                throw new NotSupportedException("Not supported for MySQL.");
            }
            SQLiteFront.TransQuery(query);
        }
    }
}