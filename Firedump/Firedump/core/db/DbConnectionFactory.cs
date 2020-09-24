using FirebirdSql.Data.FirebirdClient;
using Firedump.core.exceptions;
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
    public class DbConnectionFactory : AbstractDbFactory<DbConnection>
    {
        private sqlservers Server;

        private string ConnectionString;
        public DbConnectionFactory(sqlservers s)
        {
            Server = s;
        }

        public DbConnectionFactory(sqlservers s, string connectionString) : this(s)
        {
            ConnectionString = connectionString;
        }

        public override sealed DbConnection Create()
        {
            DbType dbType = Firedump.core.sql.Utils._convert(Server.db_type);
            if (dbType == DbType.MYSQL || dbType == DbType.MARIADB)
            {
                return string.IsNullOrEmpty(ConnectionString) ? new MySqlConnection() : new MySqlConnection(ConnectionString);
            }
            else if (dbType == DbType.ORACLE)
            {
                return string.IsNullOrEmpty(ConnectionString) ? new OracleConnection() : new OracleConnection(ConnectionString);
            }
            else if (dbType == DbType.POSTGRES)
            {
                return string.IsNullOrEmpty(ConnectionString) ? new Npgsql.NpgsqlConnection() : new Npgsql.NpgsqlConnection(ConnectionString);
            }
            else if (dbType == DbType.SQLITE)
            {
                return string.IsNullOrEmpty(ConnectionString) ? new SQLiteConnection() : new SQLiteConnection(ConnectionString);
            }
            else if (dbType == DbType.SQLSERVER)
            {
                return string.IsNullOrEmpty(ConnectionString) ? new SqlConnection() : new SqlConnection(ConnectionString);
            }
            else if (dbType == DbType.DB2)
            {
                return string.IsNullOrEmpty(ConnectionString) ? new DB2Connection() : new DB2Connection(ConnectionString);
            }
            else if (dbType == DbType.FIREBIRD)
            {
                return string.IsNullOrEmpty(ConnectionString) ? new FbConnection() : new FbConnection(ConnectionString);
            }
            throw new Exception("Database Vendor Not Supported!");
        }
    }
}
