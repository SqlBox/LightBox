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

        //Event handlers
        public event EventHandler Finished;
        public event EventHandler<ExecutionEventArgs> StatementExecuted;

        public BaseThread()
        {
        }

        public List<string> Statements() => this.statements;
        public DbConnection Con() => this._con;

        public void Start(List<string> statements,DbConnection con)
        {
            if(this._thread == null || (this._thread != null && this._thread.ThreadState == ThreadState.Stopped))
            {
                this.statements = statements;
                this._con = con;
                this._thread = new Thread(new ThreadStart(run));
                this._thread.Start();
            }
        }

        public bool IsAlive() => this._thread.IsAlive;

        public void Join()
        {
            if(this._thread != null)
            {
                this._thread.Join(2500);
            }
        }

        public abstract  void Stop();
        public abstract void run();
        public abstract void SetFetchLimit(int fetchLimit);


        //EVENTS
        protected virtual void OnFinished(object t, EventArgs e)
        {
            Finished?.Invoke(t, e);
        }
        protected virtual void OnStatementExecuted(object t, ExecutionEventArgs e)
        {
            StatementExecuted?.Invoke(t, e);
        }
    }

}
