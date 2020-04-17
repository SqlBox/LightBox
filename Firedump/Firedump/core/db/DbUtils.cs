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

namespace Firedump.core.db
{
    public class DbUtils
    {
        /**
         * The user can give his own connection, that case i dont have to close it.
         * Its up to user to handle the connection flow.
         * OR if the user dont give any connection i create one and Dispose/Close it after using it to prevent connections leak/and others.
         */
        public static List<string> getDatabases(sqlservers server, DbConnection con = null)
        {
            if (con == null)
            {
                List<string> data = null;
                using (con = DB.connect(server))
                {
                    data = getStringData(con, new SqlBuilderFactory(con).Create(null).getDatabases());
                }
                return data;
            }
            return getStringData(con, new SqlBuilderFactory(con).Create(null).getDatabases());
        }

        public static List<string> getTables(sqlservers server, string database, DbConnection con = null)
        {
            if (con == null)
            {
                List<string> data = null;
                using (con = DB.connect(server, database))
                {
                    data = getStringData(con, new SqlBuilderFactory(con).Create(database).showTablesSql());
                }
                return data;
            }
            return getStringData(con, new SqlBuilderFactory(con).Create(database).showTablesSql());
        }


        public static List<string> getTables(DbConnection con)
        {
            return getStringData(con, new SqlBuilderFactory(con).Create(con.Database).showTablesSql());
        }

        internal static List<string> getTableTriggers(DbConnection con, string table)
        {
            var data = new List<string>();
            using (var reader = new DbCommandFactory(con,new SqlBuilderFactory(con).Create(con.Database).GetTableTriggers(table)).Create().ExecuteReader())
            {
                while(reader.Read())
                {
                    data.Add(reader.GetString(0));
                }
            }
            return data;
        }

        internal static string GetCreateTrigger(DbConnection con, string table, string triggerName)
        {
            string triggerCreateStatement = "";
            using (var r = new DbCommandFactory(con, new SqlBuilderFactory(con).Create(con.Database).GetTriggerCreateStatement(table, triggerName)).Create().ExecuteReader())
            {
                while(r.Read())
                {
                    if (sql.Utils.IsDbEmbedded(sql.Utils.GetDbTypeEnum(con)))
                    {
                        triggerCreateStatement = r.GetString(1);
                        break;
                    }
                    else
                    {
                        triggerCreateStatement = r.GetString(2);
                        break;
                    }
                }
            }
            return triggerCreateStatement;
        }

        //returns all fields from all tables
        internal static List<Table> getTablesInfo(sqlservers server, DbConnection con)
        {
            var list = new List<Table>();
            if(sql.Utils.IsDbEmbedded(server.db_type))
            {
                //case of embedded like sqlite the process to get all fields from all tables is very different.
                //first get all tables
                List<string> tables = getTables(con);
                //and then for every each one get table fields
                foreach(string table in tables)
                {
                    string table_info = new SqlBuilderFactory(server).Create(con.Database).describeTableSql(table);
                    using (var r = new DbCommandFactory(con,table_info).Create().ExecuteReader())
                    {
                        while(r.Read())
                        {
                            list.Add(new Table(table, r.GetString(1), r.GetString(2), r.GetInt32(3) == 0 ? "YES" : "NO", 0));
                        }
                    }
                }
            } else
            {
                using (var r = new DbCommandFactory(con, new SqlBuilderFactory(server).Create(con.Database).getAllFieldsFromAllTablesInDb()).Create().ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Table(r.GetString(0), r.GetString(1), r.GetString(2), r.GetString(3),
                            r.GetValue(4) != DBNull.Value ? r.GetInt64(4) : default));
                    }
                }
            }
            return list;
        }

        [Deprecated("Indeed the describe exists for most of the databases, But the results are different! A parser is needed per database")]
        internal static List<string> getTableFields(DbConnection con, string table)
        {
            var data = new List<string>();
            using (var reader = new DbCommandFactory(con, new SqlBuilderFactory(con).Create(con.Database).describeTableSql(table)).Create().ExecuteReader())
            {
                if(!sql.Utils.IsDbEmbedded(sql.Utils.GetDbTypeEnum(con)))
                {
                    while (reader.Read())
                    {
                        data.Add(reader.GetString(0) + " " + reader.GetString(1) + ", Nullable:" + reader.GetString(2));
                    }
                } else
                {
                    while (reader.Read())
                    {
                        data.Add(reader.GetString(1) + " " + reader.GetString(2) + ", Nullable:" + (reader.GetInt32(3) == 1 ? "NO" : "YES"));
                    }
                }
            }
            return data;
        }

        internal static List<string> getTableInfo(DbConnection con,string table)
        {
            var data = new List<string>();
            using (var reader = new DbCommandFactory(con, new SqlBuilderFactory(con).Create(con.Database).getTableInfo(table)).Create().ExecuteReader())
            {
                if(!sql.Utils.IsDbEmbedded(sql.Utils.GetDbTypeEnum(con)))
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
            return data;
        }

        internal static string getCreateTable(DbConnection con,string table)
        {
            string res = "";
            using (var reader = new DbCommandFactory(con,new SqlBuilderFactory(con).Create(con.Database).ShowCreateStatement(table)).Create().ExecuteReader())
            {
                while(reader.Read())
                {
                    res = reader.GetString(1);
                }
            }
            return res;
        }

        internal static int getTableRowCount(sqlservers server, string database, string tablename, DbConnection con = null)
        {
            if (con == null)
            {
                int res = 0;
                using (con = DB.connect(server, database))
                {
                    res = getIntSingleResult(con, "SELECT COUNT(*) FROM " + database + "." + tablename);
                }
                return res;
            }
            return getIntSingleResult(con, "SELECT COUNT(*) FROM " + database + "." + tablename);
        }

        internal static List<string> getStringData(DbConnection con, string sql)
        {
            var data = new List<string>();
            using (var reader = new DbCommandFactory(con, sql).Create().ExecuteReader())
            {
                while (reader.Read())
                {
                    data.Add(reader.GetString(0));
                }
            }
            return data;
        }

        internal static int getIntSingleResult(DbConnection con, string sql)
        {
            int result = 0;
            using (var reader = new DbCommandFactory(con, sql).Create().ExecuteReader())
            {
                while (reader.Read())
                {
                    result = reader.GetInt32(0);
                }
            }
            return result;
        }

        internal static DataTable getDataTableData(DbConnection con, string sql)
        {
            var data = new DataTable();
            using (var adapter = new DbAdapterFactory(con, sql).Create())
            {
                try
                {
                    adapter.Fill(data);
                }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }

            }
            return data;
        }

        
        internal static sqlservers getSqlServerFromTable(DataTable table, ListControl control)
        {
            string path = table.Rows[control.SelectedIndex]["path"] != System.DBNull.Value ? (string)table.Rows[control.SelectedIndex]["path"] : null;
            return new sqlservers((string)table.Rows[control.SelectedIndex]["host"], unchecked((int)(long)table.Rows[control.SelectedIndex]["port"]),
                (string)table.Rows[control.SelectedIndex]["username"], EncryptionUtils.sDecrypt((string)table.Rows[control.SelectedIndex]["password"]),
                (int)table.Rows[control.SelectedIndex]["db_type"], path);
        }

    }
}
