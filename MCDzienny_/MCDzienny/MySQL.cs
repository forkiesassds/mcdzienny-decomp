using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;

namespace MCDzienny
{
    // Token: 0x02000343 RID: 835
    internal static class MySQL
    {
        // Token: 0x04000C4B RID: 3147
        private static readonly string connString = string.Concat("Data Source=", Server.MySQLHost, ";Port=",
            Server.MySQLPort, ";User ID=", Server.MySQLUsername, ";Password=", Server.MySQLPassword, ";Pooling=",
            Server.MySQLPooling);

        // Token: 0x06001809 RID: 6153 RVA: 0x000A0FFC File Offset: 0x0009F1FC
        public static void ExecuteQuery(string queryString, Dictionary<string, object> parameters,
            bool createDB = false)
        {
            if (!Server.useMySQL) return;
            var num = 0;
            while (true)
                try
                {
                    using (var mySqlConnection = new MySqlConnection(connString))
                    {
                        mySqlConnection.Open();
                        if (!createDB) mySqlConnection.ChangeDatabase(Server.MySQLDatabaseName);
                        using (var mySqlCommand = new MySqlCommand(queryString, mySqlConnection))
                        {
                            foreach (var parameter in parameters)
                                mySqlCommand.Parameters.AddWithValue(parameter.Key, parameter.Value);
                            mySqlCommand.ExecuteNonQuery();
                        }

                        mySqlConnection.Close();
                    }

                    return;
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

        // Token: 0x0600180A RID: 6154 RVA: 0x000A1114 File Offset: 0x0009F314
        public static DataTable fillData(string queryString, Dictionary<string, object> parameters,
            bool skipError = false)
        {
            var dataTable = new DataTable();
            if (!Server.useMySQL) return dataTable;
            var num = 0;
            while (true)
                try
                {
                    using (var mySqlConnection = new MySqlConnection(connString))
                    {
                        mySqlConnection.Open();
                        mySqlConnection.ChangeDatabase(Server.MySQLDatabaseName);
                        using (var mySqlCommand = new MySqlCommand(queryString, mySqlConnection))
                        {
                            foreach (var parameter in parameters)
                                mySqlCommand.Parameters.AddWithValue(parameter.Key, parameter.Value);
                            using (var mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand))
                            {
                                mySqlDataAdapter.Fill(dataTable);
                            }
                        }

                        mySqlConnection.Close();
                        return dataTable;
                    }
                }
                catch (Exception)
                {
                    num++;
                    if (num > 10)
                    {
                        if (!skipError)
                        {
                            File.WriteAllText("MySQL_error.log", queryString);
                            Server.s.Log("MySQL error: " + queryString);
                            throw;
                        }

                        return dataTable;
                    }
                }
        }
    }
}