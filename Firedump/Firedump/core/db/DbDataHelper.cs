using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Windows.Forms;
using Firedump.core.attributes;
using Firedump.core.sql;
using Firedump.models;
using System.Text;
using System.Diagnostics;
using System.Threading;
using sqlbox.commons;
using MySql.Data.MySqlClient;
using System.Data.SQLite;
using Firedump.ui.usercontrols;

namespace Firedump.core.db
{
    public class DbDataHelper
    {
        /**
         * The user can give his own connection, that case i dont have to close it.
         * Its up to user to handle the connection flow.
         * OR if the user dont give any connection i create one and Dispose/Close it after using it to prevent connections leak/and others.
         */
        public static List<string> getDatabases(sqlservers server, DbConnection con = null)
        {
            String sql = "";
            List<string> data = null;
            try
            {
                if (con == null)
                {
                    using (con = DB.connect(server))
                    {
                        sql = new SqlBuilderFactory(con).Create(null).getDatabases();
                        data = getStringData(con, sql);
                        Terminal.MainTerminal.AppendText(sql);
                    }
                    return data;
                }
                sql = new SqlBuilderFactory(con).Create(null).getDatabases();
                data = getStringData(con, sql);
            }
            catch(DbException ex)
            {
                sql = ex.Message;
#if DEBUG
                Console.WriteLine(ex.Message);
#endif
            }
            Terminal.MainTerminal.AppendText(sql);
            return data;
        }

        public static List<string> getTables(sqlservers server, string database, DbConnection con = null)
        {
            List<string> data = null;
            string sql = "";
            try
            {
                if (con == null)
                {
                    using (con = DB.connect(server, database))
                    {
                        sql = new SqlBuilderFactory(con).Create(database).showTablesSql();
                        data = getStringData(con, sql);
                        Terminal.MainTerminal.AppendText(sql);
                    }
                    return data;
                }
                sql = new SqlBuilderFactory(con).Create(database).showTablesSql();
                data = getStringData(con, sql);
            }
            catch (DbException ex)
            {
                sql = ex.Message;
#if DEBUG
                Console.WriteLine(ex.Message);
#endif
            }
            Terminal.MainTerminal.AppendText(sql);
            return data;
        }


        public static List<string> getTables(DbConnection con)
        {
            return getStringData(con, new SqlBuilderFactory(con).Create(con.Database).showTablesSql());
        }

        internal static List<string> getTableTriggers(DbConnection con, string table)
        {
            var data = new List<string>();
            string sql = "";
            try
            {
                sql = new SqlBuilderFactory(con).Create(con.Database).GetTableTriggers(table);
                using (var reader = new DbCommandFactory(con, sql).Create().ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data.Add(reader.GetString(0));
                    }
                }
            }
            catch (DbException ex)
            {
                sql = ex.Message;
#if DEBUG
                Console.WriteLine(ex.Message);
#endif
            }
            Terminal.MainTerminal.AppendText(sql);
            return data;
        }

        internal static string GetCreateTrigger(DbConnection con, string table, string triggerName)
        {
            string triggerCreateStatement = "";
            string sql = "";
            try
            {
                sql = new SqlBuilderFactory(con).Create(con.Database).GetTriggerCreateStatement(table, triggerName);
                using (var r = new DbCommandFactory(con, sql).Create().ExecuteReader())
                {
                    while (r.Read())
                    {
                        if (con is MySqlConnection || con is SQLiteConnection)
                        {
                            triggerCreateStatement = "DELIMITER $ " + "\r\n" + r.GetString(2) + " $ " + "\r\n" + " DELIMITER ;\0";
                        }
                        else
                        {
                            triggerCreateStatement = r.GetString(2);
                        }
                        break;
                    }
                }
            }
            catch (DbException ex)
            {
                sql = ex.Message;
#if DEBUG
                Console.WriteLine(ex.Message);
#endif
            }
            Terminal.MainTerminal.AppendText(sql);
            return triggerCreateStatement;
        }

        //returns all fields from all tables
        internal static List<Table> getTablesInfo(sqlservers server, DbConnection con)
        {
            var list = new List<Table>();
            try
            {
                if (sql.Utils.IsDbEmbedded(server.db_type))
                {
                    //case of embedded like sqlite the process to get all fields from all tables is very different.
                    //first get all tables
                    List<string> tables = getTables(con);
                    //and then for every each one get table fields
                    foreach (string table in tables)
                    {
                        string table_info = new SqlBuilderFactory(server).Create(con.Database).describeTableSql(table);
                        using (var r = new DbCommandFactory(con, table_info).Create().ExecuteReader())
                        {
                            while (r.Read())
                            {
                                list.Add(new Table(table, r.GetString(1), r.GetString(2), r.GetInt32(3) == 0 ? "YES" : "NO", 0));
                            }
                        }
                        Terminal.MainTerminal.AppendText(table_info);
                    }
                }
                else
                {
                    string sql = new SqlBuilderFactory(server).Create(con.Database).getAllFieldsFromAllTablesInDb();
                    using (var r = new DbCommandFactory(con, sql).Create().ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new Table(r.GetString(0), r.GetString(1), r.GetString(2), r.GetString(3),
                                r.GetValue(4) != DBNull.Value ? r.GetInt64(4) : default));
                        }
                    }
                    Terminal.MainTerminal.AppendText(sql);
                }
            }
            catch (DbException ex)
            {
                Terminal.MainTerminal.AppendText(ex.Message);
#if DEBUG
                Console.WriteLine(ex.Message);
#endif
            }
            return list;
        }

        internal static List<string> getTableFields(DbConnection con, string table)
        {
            var data = new List<string>();
            string command = "";
            try
            {
                command = new SqlBuilderFactory(con).Create(con.Database).describeTableSql(table);
                using (var reader = new DbCommandFactory(con, command).Create().ExecuteReader())
                {
                    if (!sql.Utils.IsDbEmbedded(sql.Utils.GetDbTypeEnum(con)))
                    {
                        while (reader.Read())
                        {
                            data.Add(reader.GetString(0) + " " + reader.GetString(1) + ", Nullable:" + reader.GetString(2));
                        }
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            data.Add(reader.GetString(1) + " " + reader.GetString(2) + ", Nullable:" + (reader.GetInt32(3) == 1 ? "NO" : "YES"));
                        }
                    }
                }
            }
            catch (DbException ex)
            {
                command = ex.Message;
#if DEBUG
                Console.WriteLine(ex.Message);
#endif
            }
            Terminal.MainTerminal.AppendText(command);
            return data;
        }

        internal static List<string> getTableInfo(DbConnection con,string table)
        {
            var data = new List<string>();
            if(!sql.Utils.IsDbEmbedded(sql.Utils.GetDbTypeEnum(con)))
            {
                string command = "";
                try
                {
                    command = new SqlBuilderFactory(con).Create(con.Database).getTableInfo(table);
                    using (var reader = new DbCommandFactory(con, command).Create().ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            data.Add("~Rows:" + (reader.IsDBNull(5) ? "" : reader.GetString(5)));
                            data.Add("AvgLen:" + (reader.IsDBNull(0) ? "" : reader.GetString(0)));
                            data.Add("Length:" + (reader.IsDBNull(1) ? "" : reader.GetString(1)));
                            data.Add("Free:" + (reader.IsDBNull(2) ? "" : reader.GetString(2)));
                            data.Add("AI:" + (reader.IsDBNull(3) ? "" : reader.GetString(3)));
                            data.Add("Collation:" + (reader.IsDBNull(4) ? "" : reader.GetString(4)));
                        }
                    }
                }
                catch (DbException ex)
                {
                    command = ex.Message;
#if DEBUG
                    Console.WriteLine(ex.Message);
#endif
                }
                Terminal.MainTerminal.AppendText(command);
            }
            return data;
        }

        internal static string getCreateTable(DbConnection con,string table)
        {
            string res = "";
            string sql = "";
            try
            {
                sql = new SqlBuilderFactory(con).Create(con.Database).ShowCreateStatement(table);
                using (var reader = new DbCommandFactory(con, sql).Create().ExecuteReader())
                {
                    while (reader.Read())
                    {
                        res = reader.GetString(1);
                    }
                }
            }
            catch (DbException ex)
            {
                sql = ex.Message;
#if DEBUG
                Console.WriteLine(ex.Message);
#endif
            }
            Terminal.MainTerminal.AppendText(sql);
            return res;
        }


        internal static List<string> getStringData(DbConnection con, string sql)
        {
            var data = new List<string>();
            try
            {
                using (var reader = new DbCommandFactory(con, sql).Create().ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data.Add(reader.GetString(0));
                    }
                }
            }
            catch (DbException ex)
            {
                sql = ex.Message;
#if DEBUG
                Console.WriteLine(ex.Message);
#endif
            }
            Terminal.MainTerminal.AppendText(sql);
            return data;
        }

        internal static DataTable getDataTableData(DbConnection con, string sql)
        {
            var data = new DataTable();
            try
            {
                using (var adapter = new DbAdapterFactory(con, sql).Create())
                {
                    try
                    {
                        adapter.Fill(data);
                        Terminal.MainTerminal.AppendText(sql);
                    }
                    catch (Exception ex) { Console.WriteLine(ex.ToString()); }

                }
            }
            catch (DbException ex)
            {
                Terminal.MainTerminal.AppendText(ex.Message);
#if DEBUG
                Console.WriteLine(ex.Message);
#endif
            }
            return data;
        }

    }
}
