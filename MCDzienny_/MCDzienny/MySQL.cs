using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using MySql.Data.MySqlClient;

namespace MCDzienny
{
    static class MySQL
    {
        static readonly string connString = "Data Source=" + Server.MySQLHost + ";Port=" + Server.MySQLPort + ";User ID=" + Server.MySQLUsername + ";Password=" +
            Server.MySQLPassword + ";Pooling=" + Server.MySQLPooling;

        public static void ExecuteQuery(string queryString, Dictionary<string, object> parameters, bool createDB = false)
        {
            //IL_000f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0015: Expected O, but got Unknown
            //IL_002b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0031: Expected O, but got Unknown
            if (!Server.useMySQL)
            {
                return;
            }
            int num = 0;
            while (true)
            {
                try
                {
                    MySqlConnection val = new MySqlConnection(connString);
                    try
                    {
                        ((DbConnection)(object)val).Open();
                        if (!createDB)
                        {
                            ((DbConnection)(object)val).ChangeDatabase(Server.MySQLDatabaseName);
                        }
                        MySqlCommand val2 = new MySqlCommand(queryString, val);
                        try
                        {
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
                        break;
                    }
                    finally
                    {
                        val.Dispose();
                    }
                }
                catch (Exception)
                {
                    if (!createDB)
                    {
                        num++;
                        if (num > 10)
                        {
                            File.WriteAllText("MySQL_error.log", queryString);
                            Server.s.Log("MySQL error: " + queryString);
                            throw;
                        }
                        continue;
                    }
                    throw;
                }
            }
        }

        public static DataTable fillData(string queryString, Dictionary<string, object> parameters, bool skipError = false)
        {
            //IL_0016: Unknown result type (might be due to invalid IL or missing references)
            //IL_001c: Expected O, but got Unknown
            //IL_002f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0035: Expected O, but got Unknown
            //IL_007c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0083: Expected O, but got Unknown
            DataTable dataTable = new DataTable();
            if (!Server.useMySQL)
            {
                return dataTable;
            }
            int num = 0;
            while (true)
            {
                try
                {
                    MySqlConnection val = new MySqlConnection(connString);
                    try
                    {
                        ((DbConnection)(object)val).Open();
                        ((DbConnection)(object)val).ChangeDatabase(Server.MySQLDatabaseName);
                        MySqlCommand val2 = new MySqlCommand(queryString, val);
                        try
                        {
                            foreach (KeyValuePair<string, object> parameter in parameters)
                            {
                                val2.Parameters.AddWithValue(parameter.Key, parameter.Value);
                            }
                            MySqlDataAdapter val3 = new MySqlDataAdapter(val2);
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
                catch (Exception)
                {
                    num++;
                    if (num > 10)
                    {
                        if (skipError)
                        {
                            break;
                        }
                        File.WriteAllText("MySQL_error.log", queryString);
                        Server.s.Log("MySQL error: " + queryString);
                        throw;
                    }
                    continue;
                }
                break;
            }
            return dataTable;
        }
    }
}