using Lightbox.core.db;
using Lightbox.core.models.dbinfo;
using Lightbox.core.models.events;
using Lightbox.core.sql.executor;
using Lightbox.usercontrols;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lightbox.core.sql
{

    public class QueryExecutor
    {
        private BaseThread queryThread;

        public event EventHandler<ExecutionQueryEvent> StatementExecuted;

        public QueryExecutor()
        {
        }

        internal void OnStatementExecuted(object t, ExecutionQueryEvent e)
        {
            StatementExecuted?.Invoke(t, e);
            if (e.Status != Status.RUNNING && this.queryThread != null)
            {
                lock (this.queryThread)
                {
                    this.queryThread.StatementExecuted -= OnStatementExecuted;
                    this.queryThread = null;
                }
            }
        }


        internal void Execute(List<string> statements, DbConnection con, QueryParams qp, bool contExecutingOnFail)
        {
            if (this.queryThread == null)
            {
                this.queryThread = new ExecutorThread() { ContinueExecutingNextOnFail = contExecutingOnFail };
                lock (this.queryThread)
                {
                    this.queryThread.StatementExecuted += OnStatementExecuted;
                    this.queryThread.Start(statements, con, qp);
                }
            }
        }

        //Abandon thread and let it close
        internal void Cancel()
        {
            if (this.queryThread != null)
            {
                lock (this.queryThread)
                {
                    this.queryThread.Stop();
                }
            }
        }

        internal bool IsAlive()
        {
            return this.queryThread != null;
        }

        internal void Abort()
        {
            if (this.queryThread != null)
            {
                var queryParams = this.queryThread.QueryParams;
                string query = this.queryThread.CurrentQuery;
                lock (this.queryThread)
                {
                    this.queryThread.StatementExecuted -= OnStatementExecuted;
                    this.queryThread.Abort();
                    this.queryThread = null;
                }
                StatementExecuted?.Invoke(this, new ExecutionQueryEvent(Status.ABORTED) { QueryParams = queryParams, TAG = queryParams.Hash, query = query });
            }
        }
    }
}
