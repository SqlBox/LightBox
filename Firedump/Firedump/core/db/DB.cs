using Firedump.core.sql;
using Firedump.models;
using Firedump.usercontrols;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.Common;

namespace Firedump.core.db
{
    public sealed class DB
    {
        public DB()
        {
        }

        public static ConnectionResultSet TestConnection(sqlservers server, string database = null)
        {
            try
            {
                using (var con = new DbConnectionFactory(server, ConnectionStringFactory.CreateConnectionString(server, database)).Create())
                {
                    con.Open();
                }
            }
            catch (Exception ex)
            {
                return new ConnectionResultSet(ex);
            }
            return new ConnectionResultSet();
        }

        public static DbConnection connect(sqlservers server, string database = null)
        {
            var con = new DbConnectionFactory(server, ConnectionStringFactory.CreateConnectionString(server, database)).Create();
            con.Open();
            return con;
        }



        internal static void close(DbConnection con)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        internal static void Rollback(DbConnection con)
        {
            using (var command = new DbCommandFactory(con, "rollback").Create())
            {
                command.ExecuteNonQuery();
            }
        }

        internal static void Commit(DbConnection con)
        {
            using (var command = new DbCommandFactory(con, "commit").Create())
            {
                command.ExecuteNonQuery();
            }
        }

        internal static bool IsConnected(DbConnection con)
        {
            return con != null && con.State == System.Data.ConnectionState.Open;
        }

        internal static bool IsConnectedToDatabase(DbConnection con)
        {
            return IsConnected(con) && !string.IsNullOrEmpty(con.Database);
        }


        // This method will test the connection and if its not open will try to raise a reconnect event without affecting the running/current user query process execution
        // If the reconnect also fails then it raises a disconnect event for the parent ui components to handle accordingly
        internal static bool IsConnectedToDatabaseAndAfterReconnect(UserControlReference uc)
        {
            if (uc.GetServer() == null)
                return false;
            bool is_connected_to_db = IsConnectedToDatabase(uc.GetSqlConnection());
            if (is_connected_to_db)
                return true;
            uc.OnReconnect(uc, new EventArgs());
            if(IsConnectedToDatabase(uc.GetSqlConnection()))
                return true;
            uc.OnDisconnected(uc, new EventArgs());
            return false;
        }
    }
}
