using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using Mono.Data.Sqlite;

namespace MCDzienny
{
    // Token: 0x02000028 RID: 40
    internal static class SQLiteMono
    {
        // Token: 0x0400009F RID: 159
        private const string DbPath = "./Database/database.db";

        // Token: 0x040000A0 RID: 160
        private static readonly string connString = "URI=file:Database/database.db";

        // Token: 0x040000A1 RID: 161
        private static readonly object syncObject = new object();

        // Token: 0x040000A2 RID: 162
        private static SqliteConnection Connection;

        // Token: 0x040000A3 RID: 163
        private static SqliteTransaction transaction;

        // Token: 0x060000F1 RID: 241 RVA: 0x00006860 File Offset: 0x00004A60
        public static bool Transaction(string commandText, string[] parameters)
        {
            bool result;
            lock (syncObject)
            {
                var num = 0;
                for (;;)
                    try
                    {
                        using (var sqliteConnection = new SqliteConnection(connString))
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

        // Token: 0x060000F2 RID: 242 RVA: 0x000069B4 File Offset: 0x00004BB4
        public static bool ExecuteQuery(string queryString, Dictionary<string, object> parameters)
        {
            bool result;
            lock (syncObject)
            {
                var num = 0;
                for (;;)
                    try
                    {
                        using (var sqliteConnection = new SqliteConnection(connString))
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

        // Token: 0x060000F3 RID: 243 RVA: 0x00006AE4 File Offset: 0x00004CE4
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
                        using (var sqliteConnection = new SqliteConnection(connString))
                        {
                            sqliteConnection.Open();
                            using (var sqliteCommand = new SqliteCommand(queryString, sqliteConnection))
                            {
                                foreach (var keyValuePair in parameters)
                                    sqliteCommand.Parameters.AddWithValue(keyValuePair.Key, keyValuePair.Value);
                                using (var sqliteDataAdapter = new SqliteDataAdapter(sqliteCommand))
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

        // Token: 0x060000F4 RID: 244 RVA: 0x00006C40 File Offset: 0x00004E40
        public static void BeginTransaction()
        {
            lock (syncObject)
            {
                Connection = new SqliteConnection(connString);
                Connection.Open();
                transaction = Connection.BeginTransaction();
            }
        }

        // Token: 0x060000F5 RID: 245 RVA: 0x00006C9C File Offset: 0x00004E9C
        public static void CommitTransaction()
        {
            lock (syncObject)
            {
                transaction.Commit();
            }
        }

        // Token: 0x060000F6 RID: 246 RVA: 0x00006CD8 File Offset: 0x00004ED8
        public static void EndTransaction()
        {
            lock (syncObject)
            {
                transaction.Dispose();
                Connection.Dispose();
            }
        }

        // Token: 0x060000F7 RID: 247 RVA: 0x00006D20 File Offset: 0x00004F20
        public static void TransQuery(string query)
        {
            lock (syncObject)
            {
                using (IDbCommand dbCommand = Connection.CreateCommand())
                {
                    dbCommand.Transaction = transaction;
                    dbCommand.CommandText = query;
                    dbCommand.ExecuteNonQuery();
                }
            }
        }
    }
}