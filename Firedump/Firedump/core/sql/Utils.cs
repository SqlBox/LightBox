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

        public static DbType _convert(int db_type)
        {
            return (DbType)db_type;
        }

        public static DbType GetDbTypeEnum(DbConnection c)
        {
            if (c is MySqlConnection)
            {
                return DbType.MYSQL;
            }
            else if (c is OracleConnection)
            {
                return DbType.ORACLE;
            }
            else if (c is SQLiteConnection)
            {
                return DbType.SQLITE;
            }
            else if (c is Npgsql.NpgsqlConnection)
            {
                return DbType.POSTGRES;
            }
            else if (c is SqlConnection)
            {
                return DbType.SQLSERVER;
            }
            else if (c is DB2Connection)
            {
                return DbType.DB2;
            }
            else if (c is FbConnection)
            {
                return DbType.FIREBIRD;
            }
            throw new Exception("Database Vendor Not Supported!");
        }

        public static bool IsDbEmbedded(DbType type)
        {
            return type == DbType.SQLITE || type == DbType.VISTADB;
        }

        public static bool IsDbEmbedded(int type)
        {
            return IsDbEmbedded(_convert(type));
        }

    }
}
