using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using Mono.Data.Sqlite;

namespace MCDzienny
{
    static class SQLiteMono
    {
        const string DbPath = "./Database/database.db";

        static readonly string connString = "URI=file:Database/database.db";

        static readonly object syncObject = new object();

        static SqliteConnection transConnection;

        static SqliteTransaction transaction;

        public static bool Transaction(string commandText, string[] parameters)
        {
            //IL_0015: Unknown result type (might be due to invalid IL or missing references)
            //IL_001b: Expected O, but got Unknown
            lock (syncObject)
            {
                int num = 0;
                while (true)
                {
                    try
                    {
                        SqliteConnection val = new SqliteConnection(connString);
                        try
                        {
                            val.Open();
                            using (IDbTransaction dbTransaction = val.BeginTransaction())
                            {
                                using (IDbCommand dbCommand = val.CreateCommand())
                                {
                                    dbCommand.Transaction = dbTransaction;
                                    for (int i = 0; i < parameters.Length; i++)
                                    {
                                        dbCommand.CommandText = commandText + parameters[i];
                                        dbCommand.ExecuteNonQuery();
                                    }
                                    dbTransaction.Commit();
                                }
                            }
                            val.Close();
                        }
                        finally
                        {
                            val.Dispose();
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        num++;
                        if (num > 10)
                        {
                            Server.ErrorLog(ex);
                            File.WriteAllText("SQLite_error.log", commandText + " " + string.Join(",", parameters));
                            Server.s.Log("SQLite error: " + commandText + " " + string.Join(",", parameters));
                            return false;
                        }
                        Thread.Sleep(10);
                    }
                }
            }
        }

        public static bool ExecuteQuery(string queryString, Dictionary<string, object> parameters)
        {
            //IL_0015: Unknown result type (might be due to invalid IL or missing references)
            //IL_001b: Expected O, but got Unknown
            lock (syncObject)
            {
                int num = 0;
                while (true)
                {
                    try
                    {
                        SqliteConnection val = new SqliteConnection(connString);
                        try
                        {
                            val.Open();
                            SqliteCommand val2 = val.CreateCommand();
                            try
                            {
                                val2.CommandText = queryString;
                                foreach (KeyValuePair<string, object> parameter in parameters)
                                {
                                    val2.Parameters.AddWithValue(parameter.Key, parameter.Value);
                                }
                                val2.ExecuteNonQuery();
                            }
                            finally
                            {
                                if (val2 != null)
                                {
                                    val2.Dispose();
                                }
                            }
                            val.Close();
                        }
                        finally
                        {
                            val.Dispose();
                        }
                        return true;
                    }
                    catch
                    {
                        num++;
                        if (num > 10)
                        {
                            File.WriteAllText("SQLite_error.log", queryString);
                            Server.s.Log("SQLite error: " + queryString);
                            throw;
                        }
                        Thread.Sleep(10);
                    }
                }
            }
        }

        public static DataTable fillData(string queryString, Dictionary<string, object> parameters, bool skipError = false)
        {
            //IL_001b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0021: Expected O, but got Unknown
            //IL_0029: Unknown result type (might be due to invalid IL or missing references)
            //IL_002f: Expected O, but got Unknown
            //IL_0076: Unknown result type (might be due to invalid IL or missing references)
            //IL_007d: Expected O, but got Unknown
            lock (syncObject)
            {
                int num = 0;
                DataTable dataTable = new DataTable();
                while (true)
                {
                    try
                    {
                        SqliteConnection val = new SqliteConnection(connString);
                        try
                        {
                            val.Open();
                            SqliteCommand val2 = new SqliteCommand(queryString, val);
                            try
                            {
                                foreach (KeyValuePair<string, object> parameter in parameters)
                                {
                                    val2.Parameters.AddWithValue(parameter.Key, parameter.Value);
                                }
                                SqliteDataAdapter val3 = new SqliteDataAdapter(val2);
                                try
                                {
                                    val3.Fill(dataTable);
                                }
                                finally
                                {
                                    val3.Dispose();
                                }
                            }
                            finally
                            {
                                val2.Dispose();
                            }
                            val.Close();
                        }
                        finally
                        {
                            val.Dispose();
                        }
                    }
                    catch
                    {
                        num++;
                        if (num > 10)
                        {
                            if (skipError)
                            {
                                break;
                            }
                            File.WriteAllText("SQLite_error.log", queryString);
                            Server.s.Log("SQLite error: " + queryString);
                            throw;
                        }
                        Thread.Sleep(10);
                        continue;
                    }
                    break;
                }
                return dataTable;
            }
        }

        public static void BeginTransaction()
        {
            //IL_0011: Unknown result type (might be due to invalid IL or missing references)
            //IL_001b: Expected O, but got Unknown
            lock (syncObject)
            {
                transConnection = new SqliteConnection(connString);
                transConnection.Open();
                transaction = transConnection.BeginTransaction();
            }
        }

        public static void CommitTransaction()
        {
            lock (syncObject)
            {
                transaction.Commit();
            }
        }

        public static void EndTransaction()
        {
            lock (syncObject)
            {
                transaction.Dispose();
                transConnection.Dispose();
            }
        }

        public static void TransQuery(string query)
        {
            lock (syncObject)
            {
                using (IDbCommand dbCommand = transConnection.CreateCommand())
                {
                    dbCommand.Transaction = transaction;
                    dbCommand.CommandText = query;
                    dbCommand.ExecuteNonQuery();
                }
            }
        }
    }
}