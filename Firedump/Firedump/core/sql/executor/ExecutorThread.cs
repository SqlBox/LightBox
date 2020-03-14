using Firedump.core.db;
using Firedump.core.models.dbinfo;
using Firedump.core.models.events;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;

namespace Firedump.core.sql.executor
{
    public class ExecutorThread : BaseThread
    {
        public bool _Alive = true;
        private DbCommand Command;

        public ExecutorThread() : base()
        {
        }

        public override void run()
        {
            _Alive = true;
            List<string> statements = base.Statements();
            var data = new DataTable();
            ExecutionEventArgs eventResult = null;
            for (int i = 0; i < statements.Count; i++)
            {
                try
                {
                    if (!_Alive)
                        break;
                    //Execute with adapter if is the last statement to fill the dataTable with data
                    if (statements.Count - 1 == i)
                    {
                        using (Command = new DbCommandFactory(Con(), statements[i]).Create())
                        {
                            using (var adapter = new DbAdapterFactory(Command).Create())
                            {
                                try
                                {
                                    adapter.Fill(data);
                                    eventResult = new ExecutionEventArgs(Status.FINISHED) { data = data, query = statements[i] };
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    eventResult = new ExecutionEventArgs(Status.FINISHED) { Ex = ex, query = statements[i] };
                                }
                            }
                        }
                        FireEvent(eventResult);
                    }
                    else
                    {
                        using (Command = new DbCommandFactory(Con(), statements[i]).Create())
                        {
                            using (var reader = Command.ExecuteReader())
                            {
                                eventResult = new ExecutionEventArgs(Status.RUNNING) { query = statements[i] };
                            }
                        }
                        FireEvent(eventResult);
                    }
                }
                catch (DbException ex)
                {
                    ///log, add user option to continue or not after error
                    Console.WriteLine(ex.Message);
                    FireEvent(new ExecutionEventArgs(Status.RUNNING) { Ex = ex, query = statements[i] });
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    FireEvent(new ExecutionEventArgs(Status.RUNNING) { Ex = ex, query = statements[i] });
                }
            }
        }


        private void FireEvent(ExecutionEventArgs e)
        {
            if(this._Alive)
                OnStatementExecuted(this, e);
        }


        public override void Stop()
        {
            this._Alive = false;
            Command?.Cancel();
            OnStatementExecuted(this, new ExecutionEventArgs(Status.CANCELED));
        }


    }
}
