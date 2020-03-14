﻿using Firedump.core.db;
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
        // one instance per tab but different execution life cycles
        private BaseThread queryThread;

        public event EventHandler<ExecutionEventArgs> StatementExecuted;

        public QueryExecutor()
        {
            this.queryThread = new ExecutorThread();
            this.queryThread.StatementExecuted += OnStatementExecuted;
        }

        internal virtual void OnStatementExecuted(object t,ExecutionEventArgs e)
        {
            StatementExecuted?.Invoke(t, e);
        }
        

        internal void Execute(List<string> statements, DbConnection con)
        {
            this.queryThread.Start(statements, con);
        }


        internal  void Cancel() { 
            this.queryThread.Stop();
        }

    }
}