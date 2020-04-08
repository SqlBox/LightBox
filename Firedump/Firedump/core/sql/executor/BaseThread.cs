using Firedump.core.models.events;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Firedump.core.sql.executor
{
    abstract public class BaseThread
    {
        private Thread _thread;
        private List<string> statements;
        private DbConnection _con;
        
        public QueryParams QueryParams;

        //Event handlers
        public event EventHandler<ExecutionQueryEvent> StatementExecuted;

        public BaseThread()
        {
        }

        internal List<string> Statements() => this.statements;
        internal DbConnection Con() => this._con;

        internal void Start(List<string> statements,DbConnection con,QueryParams parameters)
        {
            if(this._thread == null || (this._thread != null && this._thread.ThreadState == ThreadState.Stopped))
            {
                this.QueryParams = parameters;
                this.statements = statements;
                this._con = con;
                this._thread = new Thread(new ThreadStart(run));
                this._thread.Start();
            } else
            {
                StatementExecuted?.Invoke(this, new ExecutionQueryEvent(models.dbinfo.Status.HIDDEN) { });
            } 
        }

        public bool IsAlive()
        {
            return this._thread != null && this._thread.ThreadState == ThreadState.Running;
        }

        public void Stop()
        {
            this._thread = new Thread(new ThreadStart(cancel));
            this._thread.Start();
        }

        public abstract void run();

        public abstract void cancel();
        
        protected virtual void OnStatementExecuted(object t, ExecutionQueryEvent e)
        {
            StatementExecuted?.Invoke(t, e);
        }

    }

}
