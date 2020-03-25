using Firedump.core.db;
using Firedump.core.models.events;
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
        private BaseThread queryThread;
        public event EventHandler<ExecutionQueryEvent> StatementExecuted;

        public QueryExecutor()
        {
            this.queryThread = new ExecutorThread();
            this.queryThread.StatementExecuted += OnStatementExecuted;
        }

        internal void OnStatementExecuted(object t,ExecutionQueryEvent e)
        {
            StatementExecuted?.Invoke(t, e);
        }
        

        internal void Execute(List<string> statements, DbConnection con,QueryParams qp)
        {
            this.queryThread.Start(statements, con, qp);
        }


        internal  void Cancel() { 
            this.queryThread.Stop();
        }

        internal bool IsAlive()
        {
            return this.queryThread.IsAlive();
        }

    }
}
