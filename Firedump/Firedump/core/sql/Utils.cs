using com.protectsoft.SqlStatementParser;
using FastColoredTextBoxNS;
using FirebirdSql.Data.FirebirdClient;
using IBM.Data.DB2.Core;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using sqlbox.commons;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firedump.core.sql
{
    public class Utils
    {
        public static bool IsShowDataTypeOfCommand(string sql)
        {
            if (sql == null)
            {
                return false;
            }
            string q = sql.Trim().ToLower();
            return q.Contains("select ") || q.Contains("show ") || q.Contains("describe ") || q.Contains("explain ");
        }

        public static sqlbox.commons.DbType _convert(int db_type)
        {
            return (sqlbox.commons.DbType)db_type;
        }

        public static sqlbox.commons.DbType GetDbTypeEnum(DbConnection c)
        {
            if (c is MySqlConnection)
            {
                return sqlbox.commons.DbType.MYSQL;
            }
            else if (c is OracleConnection)
            {
                return sqlbox.commons.DbType.ORACLE;
            }
            else if (c is SQLiteConnection)
            {
                return sqlbox.commons.DbType.SQLITE;
            }
            else if (c is Npgsql.NpgsqlConnection)
            {
                return sqlbox.commons.DbType.POSTGRES;
            }
            else if (c is SqlConnection)
            {
                return sqlbox.commons.DbType.SQLSERVER;
            }
            else if (c is DB2Connection)
            {
                return sqlbox.commons.DbType.DB2;
            }
            else if (c is FbConnection)
            {
                return sqlbox.commons.DbType.FIREBIRD;
            }
            throw new Exception("Database Vendor Not Supported!");
        }

        public static bool IsDbEmbedded(sqlbox.commons.DbType type)
        {
            return type == sqlbox.commons.DbType.SQLITE || type == sqlbox.commons.DbType.VISTADB;
        }

        public static bool IsDbEmbedded(int type)
        {
            return IsDbEmbedded(_convert(type));
        }


        //At the moment parser only supports mysql,mariadb,postgresql
        //any other db i just execute all in one statement
        internal static List<string> convert(sqlbox.commons.DbType dbtype, string query)
        {
            List<string> statementList = null;
            if (dbtype == sqlbox.commons.DbType.MYSQL || dbtype == sqlbox.commons.DbType.MARIADB || dbtype == sqlbox.commons.DbType.POSTGRES)
            {
                var parser = new SqlStatementParserWrapper(query, (com.protectsoft.SqlStatementParser.DbType)(int)dbtype);
                statementList = SqlStatementParserWrapper.convert(parser.sql, parser.Parse());
            }
            else
            {
                statementList = new List<string>();
                statementList.Add(query);
            }
            return statementList;
        }

        internal static void selectCurrent(FastColoredTextBox tb, bool moveToNext = false)
        {
            var sel = tb.Selection;
            int line = 0;
            int ch = 0;
            char[] textarr = tb.Text.ToCharArray();
            var endPlace = new Place();
            var startPlace = new Place();
            bool endsemifound = false;
            for (int i = 0; i < textarr.Length; i++)
            {
                if (textarr[i] == '\n')
                {
                    line++;
                    ch = 0;
                }
                else
                {
                    ch++;
                }
                if (textarr[i] == ';')
                {
                    if ((sel.Start.iLine == line && ch > sel.Start.iChar) || (sel.Start.iLine < line))
                    {
                        endsemifound = true;
                        endPlace = new Place(ch, line);
                        break;
                    }
                    else
                    {
                        startPlace = new Place(ch, line);
                    }
                }
            }
            if (moveToNext)
            {
                sel.Start = !endsemifound ? new Place(ch, line) : endPlace;
                sel.End = startPlace;
            }
            else
            {
                sel.Start = startPlace;
                sel.End = !endsemifound ? new Place(ch, line) : endPlace;
            }
            tb.DoSelectionVisible();
        }

    }
}
