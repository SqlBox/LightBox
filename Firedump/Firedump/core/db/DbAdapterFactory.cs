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
    public class DbAdapterFactory : AbstractDbFactory<DbDataAdapter>
    {
        private string Sql;
        private DbCommand command;

        public DbAdapterFactory(DbConnection c, string sql) : base(c)
        {
            Sql = sql;
        }

        public DbAdapterFactory(DbCommand command) : base(command.Connection)
        {
            this.command = command;
        }

        public override sealed DbDataAdapter Create()
        {
            DbType dbType = _DbUtils.GetDbTypeEnum(Connection);
            if (dbType == DbType.MYSQL || dbType == DbType.MARIADB)
            {
                if(command != null)
                    return new MySqlDataAdapter((MySqlCommand)command);
                else
                    return new MySqlDataAdapter(Sql, (MySqlConnection)Connection);
            }
            else if (dbType == DbType.ORACLE)
            {
                if(command != null)
                    return new OracleDataAdapter((OracleCommand)command);
                else
                    return new OracleDataAdapter(Sql, (OracleConnection)Connection);
            }
            else if(dbType == DbType.POSTGRES)
            {
                if (command != null)
                    return new Npgsql.NpgsqlDataAdapter((Npgsql.NpgsqlCommand)command);
                else
                    return new Npgsql.NpgsqlDataAdapter(Sql, (Npgsql.NpgsqlConnection)Connection);
            }
            else if(dbType == DbType.SQLITE)
            {
                if (command != null)
                    return new SQLiteDataAdapter((SQLiteCommand)command);
                else
                    return new SQLiteDataAdapter(Sql, (SQLiteConnection)Connection);
            }
            else if(dbType == DbType.SQLSERVER)
            {
                if (command != null)
                    return new SqlDataAdapter((SqlCommand)command);
                else
                    return new SqlDataAdapter(Sql,(SqlConnection)Connection);
            }
            else if(dbType == DbType.DB2)
            {
                if (command != null)
                    return new DB2DataAdapter((DB2Command)command);
                else
                    return new DB2DataAdapter(Sql, (DB2Connection)Connection);
            }
            else if(dbType == DbType.FIREBIRD)
            {
                if (command != null)
                    return new FbDataAdapter((FbCommand)command);
                else
                    return new FbDataAdapter(Sql, (FbConnection)Connection);
            }
            throw new Exception("Database Vendor Not Supported!");
        }

    }
}
