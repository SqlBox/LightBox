using Firedump.core.db;
using Firedump.core.sql.executor;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Firedump.core.sql
{
    public class QueryExecutor
    {
        // one instance per tab but different executions
        private BaseThread queryThread;

        public QueryExecutor()
        {
            this.queryThread = new ExecutorThread();
        }

        internal void StartExecution(string query, DbConnection con)
        {
            this.queryThread.Start(query, con);
        }

        internal void FetchNext(int limit)
        {
            this.queryThread.SetFetchLimit(limit);
            lock(this.queryThread)
            {
                Monitor.Pulse(this.queryThread);
            }
        }

        internal  void Stop() { 
            this.queryThread.Stop();
        }

    }
}
