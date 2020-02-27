using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firedump.core.parsers
{
    // At the moment Mysql is a superSet for sqlite and the parser has a completly coverage
    class SqlLiteSqlStatementParser : MySqlStatementParser
    {
        public SqlLiteSqlStatementParser(string originalSqlRef) : base(originalSqlRef,false)
        {
        }
    }
}
