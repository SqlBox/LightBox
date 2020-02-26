using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firedump.core.parsers
{
    class SqlLiteSqlStatementParser : SqlStatementParser
    {
        public SqlLiteSqlStatementParser(string originalSqlRef) : base(originalSqlRef)
        {
        }

        internal override unsafe void determineStatementRanges(char* sql, int length, string initial_delimiter, List<StatementRange> ranges, string line_break)
        {
            throw new NotImplementedException();
        }
    }
}
