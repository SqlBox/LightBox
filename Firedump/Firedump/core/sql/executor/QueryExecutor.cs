using Firedump.core.db;
using Firedump.core.models.dbinfo;
using Firedump.core.models.events;
using Firedump.core.sql.executor;
using Firedump.usercontrols;
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
        }

        internal void OnStatementExecuted(object t,ExecutionQueryEvent e)
        {
            StatementExecuted?.Invoke(t, e);
            if(e.Status != Status.RUNNING && this.queryThread != null)
            {
                lock(this.queryThread)
                {
                    this.queryThread.StatementExecuted -= OnStatementExecuted;
                    this.queryThread = null;
                }
            }
        }
        

        internal void Execute(List<string> statements, DbConnection con,QueryParams qp)
        {
            if(this.queryThread == null)
            {
                this.queryThread = new ExecutorThread();
                lock(this.queryThread)
                {
                    this.queryThread.StatementExecuted += OnStatementExecuted;
                    this.queryThread.Start(statements, con, qp);
                }
            }
        }

        //Abandon thread and let it close
        internal void Cancel() {
            if(this.queryThread != null)
            {
                lock (this.queryThread)
                {
                    this.queryThread.Stop();
                    
                    /*this.queryThread.StatementExecuted -= OnStatementExecuted;
                    var queryParams = this.queryThread.QueryParams;
                    this.queryThread = null;
                    OnStatementExecuted(this, new ExecutionQueryEvent(Status.CANCELED) { QueryParams = queryParams });
                    */    
                }
            }
        }

        internal bool IsAlive()
        {
            return this.queryThread != null && this.queryThread.IsAlive();
        }

    }
}
