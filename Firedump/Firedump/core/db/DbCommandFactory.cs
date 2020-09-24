using FirebirdSql.Data.FirebirdClient;
using IBM.Data.DB2.Core;
using MySql.Data.MySqlClient;
using Npgsql;
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
    public class DbCommandFactory : AbstractDbFactory<DbCommand>
    {
        private string Sql;

        public DbCommandFactory(DbConnection c, string sql) : base(c)
        {
            Sql = sql;
        }

        public override sealed DbCommand Create()
        {
            DbType dbType = Firedump.core.sql.Utils.GetDbTypeEnum(Connection);
            if (dbType == DbType.MYSQL || dbType == DbType.MARIADB)
            {
                var com = new MySqlCommand(Sql, (MySqlConnection)Connection);
                com.CommandTimeout = Properties.Settings.Default.option_mysql_conreadtimeout;
                return com;
            }
            else if (dbType == DbType.ORACLE)
            {
                return new OracleCommand(Sql, (OracleConnection)Connection);
            }
            else if (dbType == DbType.POSTGRES)
            {
                return new Npgsql.NpgsqlCommand(Sql, (NpgsqlConnection)Connection);
            }
            else if (dbType == DbType.SQLITE)
            {
                return new SQLiteCommand(Sql, (SQLiteConnection)Connection);
            }
            else if (dbType == DbType.SQLSERVER)
            {
                return new SqlCommand(Sql, (SqlConnection)Connection);
            }
            else if (dbType == DbType.DB2)
            {
                return new DB2Command(Sql, (DB2Connection)Connection);
            }
            else if (dbType == DbType.FIREBIRD)
            {
                return new FbCommand(Sql, (FbConnection)Connection);
            }
            throw new Exception("Database Vendor Not Supported!");
        }
    }
}
