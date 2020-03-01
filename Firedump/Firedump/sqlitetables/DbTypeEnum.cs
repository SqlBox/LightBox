using com.protectsoft.SqlStatementParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firedump.sqlitetables
{
    public enum DbTypeEnum : int
    {
        MYSQL = DbType.MYSQL,
        MARIADB = DbType.MARIADB,
        ORACLE = DbType.ORACLE,
        POSTGRES = DbType.POSTGRES,
        SQLSERVER = DbType.SQLSERVER,
        SQLITE = DbType.SQLITE
    }
}
