using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Threading;

namespace MCDzienny
{
    static class SQLite
    {
        static readonly string connString = "Data Source=./Database/database.db; Version=3; Pooling=True;";

        static readonly object syncObject = new object();

        static SQLiteConnection transConnection;

        static SQLiteTransaction transaction;

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
                        SQLiteConnection val = new SQLiteConnection(connString);
                        try
                        {
                            ((DbConnection)(object)val).Open();
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
                            ((DbConnection)(object)val).Close();
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
                        SQLiteConnection val = new SQLiteConnection(connString);
                        try
                        {
                            ((DbConnection)(object)val).Open();
                            SQLiteCommand val2 = val.CreateCommand();
                            try
                            {
                                ((DbCommand)(object)val2).CommandText = queryString;
                                foreach (KeyValuePair<string, object> parameter in parameters)
                                {
                                    val2.Parameters.AddWithValue(parameter.Key, parameter.Value);
                                }
                                ((DbCommand)(object)val2).ExecuteNonQuery();
                            }
                            finally
                            {
                                val2.Dispose();
                            }
                            ((DbConnection)(object)val).Close();
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
                        SQLiteConnection val = new SQLiteConnection(connString);
                        try
                        {
                            ((DbConnection)(object)val).Open();
                            SQLiteCommand val2 = new SQLiteCommand(queryString, val);
                            try
                            {
                                foreach (KeyValuePair<string, object> parameter in parameters)
                                {
                                    val2.Parameters.AddWithValue(parameter.Key, parameter.Value);
                                }
                                SQLiteDataAdapter val3 = new SQLiteDataAdapter(val2);
                                try
                                {
                                    ((DbDataAdapter)(object)val3).Fill(dataTable);
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
                            ((DbConnection)(object)val).Close();
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
                transConnection = new SQLiteConnection(connString);
                ((DbConnection)(object)transConnection).Open();
                transaction = transConnection.BeginTransaction();
            }
        }

        public static void CommitTransaction()
        {
            lock (syncObject)
            {
                ((DbTransaction)(object)transaction).Commit();
            }
        }

        public static void EndTransaction()
        {
            lock (syncObject)
            {
                ((DbTransaction)(object)transaction).Dispose();
                ((Component)(object)transConnection).Dispose();
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