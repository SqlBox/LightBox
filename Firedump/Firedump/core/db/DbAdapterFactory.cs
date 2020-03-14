using Firedump.sqlitetables;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
            DbTypeEnum dbType = _DbUtils.GetDbTypeEnum(Connection);
            if (dbType == DbTypeEnum.MYSQL || dbType == DbTypeEnum.MARIADB)
            {
                if(command != null)
                    return new MySqlDataAdapter((MySqlCommand)command);
                else
                    return new MySqlDataAdapter(Sql, (MySqlConnection)Connection);
            }
            else if (dbType == DbTypeEnum.ORACLE)
            {
                if(command != null)
                    return new OracleDataAdapter(Sql, (OracleConnection)Connection);
                else
                    return new OracleDataAdapter((OracleCommand)command);
            }
            throw new Exception("Database Vendor Not Supported!");
        }

    }
}
