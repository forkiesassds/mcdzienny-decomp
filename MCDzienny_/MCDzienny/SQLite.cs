using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Threading;

namespace MCDzienny
{
    // Token: 0x02000153 RID: 339
    internal static class SQLite
    {
        // Token: 0x0400045C RID: 1116
        private static readonly string connString = "Data Source=./Database/database.db; Version=3; Pooling=True;";

        // Token: 0x0400045D RID: 1117
        private static readonly object syncObject = new object();

        // Token: 0x0400045E RID: 1118
        private static SQLiteConnection transConnection;

        // Token: 0x0400045F RID: 1119
        private static SQLiteTransaction transaction;

        // Token: 0x060009CE RID: 2510 RVA: 0x000337F8 File Offset: 0x000319F8
        public static bool Transaction(string commandText, string[] parameters)
        {
            bool result;
            lock (syncObject)
            {
                var num = 0;
                for (;;)
                    try
                    {
                        using (var sqliteConnection = new SQLiteConnection(connString))
                        {
                            sqliteConnection.Open();
                            using (IDbTransaction dbTransaction = sqliteConnection.BeginTransaction())
                            {
                                using (IDbCommand dbCommand = sqliteConnection.CreateCommand())
                                {
                                    dbCommand.Transaction = dbTransaction;
                                    for (var i = 0; i < parameters.Length; i++)
                                    {
                                        dbCommand.CommandText = commandText + parameters[i];
                                        dbCommand.ExecuteNonQuery();
                                    }

                                    dbTransaction.Commit();
                                }
                            }

                            sqliteConnection.Close();
                        }

                        result = true;
                        break;
                    }
                    catch (Exception ex)
                    {
                        num++;
                        if (num > 10)
                        {
                            Server.ErrorLog(ex);
                            File.WriteAllText("SQLite_error.log", commandText + " " + string.Join(",", parameters));
                            Server.s.Log("SQLite error: " + commandText + " " + string.Join(",", parameters));
                            result = false;
                            break;
                        }

                        Thread.Sleep(10);
                    }
            }

            return result;
        }

        // Token: 0x060009CF RID: 2511 RVA: 0x0003394C File Offset: 0x00031B4C
        public static bool ExecuteQuery(string queryString, Dictionary<string, object> parameters)
        {
            bool result;
            lock (syncObject)
            {
                var num = 0;
                for (;;)
                    try
                    {
                        using (var sqliteConnection = new SQLiteConnection(connString))
                        {
                            sqliteConnection.Open();
                            using (var sqliteCommand = sqliteConnection.CreateCommand())
                            {
                                sqliteCommand.CommandText = queryString;
                                foreach (var keyValuePair in parameters)
                                    sqliteCommand.Parameters.AddWithValue(keyValuePair.Key, keyValuePair.Value);
                                sqliteCommand.ExecuteNonQuery();
                            }

                            sqliteConnection.Close();
                        }

                        result = true;
                        break;
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

            return result;
        }

        // Token: 0x060009D0 RID: 2512 RVA: 0x00033A7C File Offset: 0x00031C7C
        public static DataTable fillData(string queryString, Dictionary<string, object> parameters,
            bool skipError = false)
        {
            DataTable result;
            lock (syncObject)
            {
                var num = 0;
                var dataTable = new DataTable();
                for (;;)
                {
                    try
                    {
                        using (var sqliteConnection = new SQLiteConnection(connString))
                        {
                            sqliteConnection.Open();
                            using (var sqliteCommand = new SQLiteCommand(queryString, sqliteConnection))
                            {
                                foreach (var keyValuePair in parameters)
                                    sqliteCommand.Parameters.AddWithValue(keyValuePair.Key, keyValuePair.Value);
                                using (var sqliteDataAdapter = new SQLiteDataAdapter(sqliteCommand))
                                {
                                    sqliteDataAdapter.Fill(dataTable);
                                }
                            }

                            sqliteConnection.Close();
                        }
                    }
                    catch
                    {
                        num++;
                        if (num <= 10)
                        {
                            Thread.Sleep(10);
                            continue;
                        }

                        if (!skipError)
                        {
                            File.WriteAllText("SQLite_error.log", queryString);
                            Server.s.Log("SQLite error: " + queryString);
                            throw;
                        }
                    }

                    break;
                }

                result = dataTable;
            }

            return result;
        }

        // Token: 0x060009D1 RID: 2513 RVA: 0x00033BD8 File Offset: 0x00031DD8
        public static void BeginTransaction()
        {
            lock (syncObject)
            {
                transConnection = new SQLiteConnection(connString);
                transConnection.Open();
                transaction = transConnection.BeginTransaction();
            }
        }

        // Token: 0x060009D2 RID: 2514 RVA: 0x00033C34 File Offset: 0x00031E34
        public static void CommitTransaction()
        {
            lock (syncObject)
            {
                transaction.Commit();
            }
        }

        // Token: 0x060009D3 RID: 2515 RVA: 0x00033C70 File Offset: 0x00031E70
        public static void EndTransaction()
        {
            lock (syncObject)
            {
                transaction.Dispose();
                transConnection.Dispose();
            }
        }

        // Token: 0x060009D4 RID: 2516 RVA: 0x00033CB8 File Offset: 0x00031EB8
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