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
        private BaseThread queryThread;
        public event EventHandler<ExecutionEventArgs> StatementExecuted;

        public QueryExecutor()
        {
            this.queryThread = new ExecutorThread();
            this.queryThread.StatementExecuted += OnStatementExecuted;
        }

        internal void OnStatementExecuted(object t,ExecutionEventArgs e)
        {
            StatementExecuted?.Invoke(t, e);
        }
        

        internal void Execute(List<string> statements, DbConnection con,int hash = 0)
        {
            this.queryThread.Start(statements, con, hash);
        }


        internal  void Cancel() { 
            this.queryThread.Stop();
        }

    }
}
