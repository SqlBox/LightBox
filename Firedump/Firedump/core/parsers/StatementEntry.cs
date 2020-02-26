using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firedump.core.parsers
{
    unsafe public struct StatementRange
    {
        public long start;
        public long end;
        public StatementRange(long start, long end)
        {
            this.start = start;
            this.end = end;
        }
    }
}
