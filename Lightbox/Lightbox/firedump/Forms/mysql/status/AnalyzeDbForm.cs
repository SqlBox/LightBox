﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using Firedump.models.databaseUtils;
using Firedump.models.dbinfo;

namespace Firedump.Forms.mysql.status
{
    public partial class AnalyzeDbForm : Form
    {
        
        private mysql_servers server;
        private string database;
        private List<MyTable> tables;
        public AnalyzeDbForm(mysql_servers server,string database)
        {
            InitializeComponent();
            this.server = server;
            this.database = database;
            setInfoTab(database);

            setTableNamesTab();
            setColumnNamesTab();
            setIndexesTab();
        }

       

        private void setInfoTab(string database)
        {
            label1.Text += ": " + database;

            DbConnection con = DbConnection.Instance();
            con.username = server.username;
            con.password = server.password;
            con.Host = server.host;
            con.database = database;
            con.port = (int)server.port;

            try {
                if (con.testConnection().wasSuccessful)
                {
                    List<string> t = con.getTables(database);
                    tables = new List<MyTable>();
                    for (int i = 0; i < t.Count; i++)
                    {
                        tables.Add(new MyTable(t[i]));
                    }
                    int tablesCount = tables.Count;
                    label5.Text += " " + tablesCount;
                    string connectionString = DbConnection.conStringBuilder(server.host, server.username, server.password, database,(int)server.port);
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        //database size in mb
                        connection.Open();
                        string sql = "SELECT table_schema " + database +
                           " , SUM(data_length + index_length)  / (1024 * 1024) " +
                           "FROM   information_schema.tables " +
                           "GROUP  BY table_schema; ";
                        using (MySqlCommand command = new MySqlCommand(sql, connection))
                        {
                            try
                            {
                                using (MySqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        if (reader.GetString(database) == database)
                                        {
                                            int size = reader.GetInt32("SUM(data_length + index_length)  / (1024 * 1024)");
                                            label6.Text += " " + size + " MB";
                                            break;
                                        }
                                    }
                                }
                            } catch (MySqlException ex)
                            {
                                //handle exception
                            }

                        }

                        //get default database charset
                        sql = "SELECT @@character_set_database, @@collation_database;";
                        using (MySqlCommand command = new MySqlCommand(sql, connection))
                        {
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string charset = reader.GetString("@@character_set_database");
                                    label4.Text += " " + charset;
                                    string collation = reader.GetString("@@collation_database");
                                    label3.Text += " " + collation;
                                }
                            }
                        }


                    }
                }
            }catch(Exception ex)
            {
            }
           
        }

        private void setTableNamesTab()
        {
            //datagridviewTables.DataSource = tables.Select(x => new { Value = x }).ToList();
            List<TableInfo> tableInfoList = new List<TableInfo>();
            foreach(MyTable table in tables)
            {
                string connectionString = DbConnection.conStringBuilder(server.host,server.username,server.password,database,(int)server.port);
                try {
                    using (MySqlConnection con = new MySqlConnection(connectionString))
                    {
                        con.Open();
                        string sql = "SHOW TABLE STATUS WHERE NAME = '" + table.TableName + "' ";
                        using (MySqlCommand command = new MySqlCommand(sql, con))
                        {
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    TableInfo tableInfo = new TableInfo();
                                    tableInfo.Table = table.TableName;
                                    string engine = reader.GetString("Engine");
                                    int version = reader.GetInt32("Version");
                                    string rowFormat = reader.GetString("Row_format");
                                    Int64 rows = reader.GetInt32("Rows");
                                    Int64 avgrowLength = reader.GetInt32("Avg_row_length");
                                    Int64 dataLength = reader.GetInt32("Data_length");
                                    Int64 indexLength = reader.GetInt32("Index_length");
                                    string createTime = reader.GetString("Create_time");
                                    string collation = reader.GetString("Collation");

                                    tableInfo.Engine = engine;
                                    tableInfo.Version = version;
                                    tableInfo.RowFormat = rowFormat;
                                    tableInfo.Rows = rows;
                                    tableInfo.AvgRowLength = avgrowLength;
                                    tableInfo.DataLength = dataLength;
                                    tableInfo.IndexLength = indexLength;
                                    tableInfo.CreateTime = createTime;
                                    tableInfo.Collation = collation;

                                    tableInfoList.Add(tableInfo);
                                }
                            }
                        }
                    }
                }catch(Exception ex)
                {
                }
            }

            datagridviewTables.DataSource = tableInfoList;
        }

        private void setColumnNamesTab()
        {
            List<ColumnInfo> columnInfoList = new List<ColumnInfo>();
            foreach(MyTable table in tables)
            {
                string connectionString = DbConnection.conStringBuilder(server.host, server.username, server.password, database,(int)server.port);
                try {
                    using (MySqlConnection con = new MySqlConnection(connectionString))
                    {
                        con.Open();
                        string sql = "DESCRIBE " + table.TableName;
                        using (MySqlCommand command = new MySqlCommand(sql, con))
                        {
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    ColumnInfo col = new ColumnInfo();
                                    string Field = reader.GetString("Field");
                                    string Type = reader.GetString("Type");
                                    string isNull = reader.GetString("Null");

                                    col.Table = table.TableName;
                                    col.Default = "";
                                    col.Type = Type;
                                    col.Field = Field;
                                    col.IsNullable = isNull;
                                    columnInfoList.Add(col);
                                }
                            }
                        }
                    }
                }catch(Exception ex)
                {
                }            
            }

            datagridviewColumns.DataSource = columnInfoList;
        }


        private void setIndexesTab()
        {
            List<Indexes> indexesList = new List<Indexes>();
            foreach (MyTable table in tables)
            {
                try {
                    string connectionString = DbConnection.conStringBuilder(server.host, server.username, server.password, database, (int)server.port);
                    using (MySqlConnection con = new MySqlConnection(connectionString))
                    {
                        con.Open();
                        string sql = "SHOW INDEX FROM " + table.TableName;
                        using (MySqlCommand command = new MySqlCommand(sql, con))
                        {
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Indexes index = new Indexes();
                                    index.Table = table.TableName;
                                    string unique;
                                    if (reader.GetInt32("Non_unique") == 0)
                                        unique = "TRUE";
                                    else
                                        unique = "FALSE";
                                    index.Unique = unique;
                                    index.KeyName = reader.GetString("Key_name");
                                    index.SeqInIndex = reader.GetInt32("Seq_in_index");
                                    index.ColumnName = reader.GetString("Column_name");
                                    index.Cardinality = reader.GetInt32("Cardinality");
                                    index.IndexType = reader.GetString("Index_type");

                                    indexesList.Add(index);
                                }
                            }
                        }
                    }
                }catch(Exception ex)
                {
                }

            }

            datagridviewIndexes.DataSource = indexesList;
        }


        private static DataTable ConvertListToDataTable(List<string[]> list)
        {
            // New table.
            DataTable table = new DataTable();

            // Get max columns.
            int columns = 0;
            foreach (var array in list)
            {
                if (array.Length > columns)
                {
                    columns = array.Length;
                }
            }

            // Add columns.
            for (int i = 0; i < columns; i++)
            {
                table.Columns.Add();
            }

            // Add rows.
            foreach (var array in list)
            {
                table.Rows.Add(array);
            }

            return table;
        }


        class MyTable
        {
            public MyTable() { }
            public MyTable(string t)
            {
                this.TableName = t;
            }
            public string TableName { get; set; }

            public override string ToString()
            {
                return TableName;
            }
        }


       

    }
}
