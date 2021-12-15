using Lightbox.core.exceptions;
using sqlbox.commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightbox.core.sql
{
    internal sealed class ConnectionStringFactory
    {
        internal static string CreateConnectionString(sqlservers server, string database = null)
        {
            if (server.db_type == (int)DbType.MYSQL || server.db_type == (int)DbType.MARIADB)
            {
                return MySqlConnectionStringBuilder.connectionStringBuilder(server, database);
            }
            else if (server.db_type == (int)DbType.ORACLE)
            {
            }
            else if (server.db_type == (int)DbType.POSTGRES)
            {
            }
            else if (server.db_type == (int)DbType.SQLITE)
            {
                return SqliteConnectionStringFactory.ConnectionStringBuilder(server.path, server.password);
            }
            else if (server.db_type == (int)DbType.SQLSERVER)
            {
            }
            else if (server.db_type == (int)DbType.DB2)
            {
            }
            else if (server.db_type == (int)DbType.FIREBIRD)
            {
            }

            throw new SqlException("Database Not Supported!");
        }
    }
}
