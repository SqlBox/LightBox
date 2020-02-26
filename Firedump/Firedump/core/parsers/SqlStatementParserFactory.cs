using Firedump.sqlitetables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firedump.core.parsers
{
    class SqlStatementParserFactory
    {
        private SqlStatementParserFactory() { }

        internal static SqlStatementParser createSqlStatementParser(string sql,DbTypeEnum dbType)
        {
            if(dbType == DbTypeEnum.MYSQL)
            {
                return  new MySqlStatementParser(sql,false);
            } else if(dbType == DbTypeEnum.MARIADB)
            {
                return new MySqlStatementParser(sql,true);
            } else if(dbType == DbTypeEnum.SQLITE)
            {
                return new SqlLiteSqlStatementParser(sql);
            } else if(dbType == DbTypeEnum.POSTGRES)
            {
                return new PostgreSqlStatementParser(sql);
            }

            return null;
        }
    }
}
