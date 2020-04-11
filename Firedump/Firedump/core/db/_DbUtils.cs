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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firedump.core.db
{
    public sealed class _DbUtils
    {
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
            else if(c is SQLiteConnection)
            {
                return DbType.SQLITE;
            }
            else if(c is Npgsql.NpgsqlConnection)
            {
                return DbType.POSTGRES;
            } 
            else if(c is SqlConnection)
            {
                return DbType.SQLSERVER;
            }
            else if(c is DB2Connection)
            {
                return DbType.DB2;
            }
            else if(c is FbConnection)
            {
                return DbType.FIREBIRD;
            }
            throw new Exception("Database Vendor Not Supported!");
        }
    }
}
