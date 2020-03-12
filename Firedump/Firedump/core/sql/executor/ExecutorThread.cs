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

        public ExecutorThread() : base()
        {
        }

        public override void run()
        {
            _Alive = true;
            List<string> statements = base.Statements();
            var data = new DataTable();
            for (int i = 0; i < statements.Count; i++)
            {
                try
                {
                    if (!_Alive)
                        break;
                    //Execute with adapter if is the last statement to fill the dataTable with data
                    if (statements.Count - 1 == i)
                    {
                        using (var adapter = new DbAdapterFactory(Con(), statements[i]).Create())
                        {
                            try
                            {
                                adapter.Fill(data);
                                OnStatementExecuted(this, new ExecutionEventArgs(Status.FINISHED) { data = data, query = statements[i] });
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                OnStatementExecuted(this, new ExecutionEventArgs(Status.FINISHED) { Ex = ex, query = statements[i] });
                            }
                        }
                    }
                    else
                    {
                        using (var reader = new DbCommandFactory(Con(), statements[i]).Create().ExecuteReader())
                        {
                            OnStatementExecuted(this, new ExecutionEventArgs(Status.RUNNING) { query = statements[i] });
                        }
                    }
                }
                catch (DbException ex)
                {
                    ///log, add user option to continue or not after error
                    Console.WriteLine(ex.Message);
                    OnStatementExecuted(this, new ExecutionEventArgs(Status.RUNNING) { Ex = ex, query = statements[i] });
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    OnStatementExecuted(this, new ExecutionEventArgs(Status.RUNNING) { Ex = ex, query = statements[i] });
                }
            }
        }


        public override void Stop()
        {
            this._Alive = false;
            OnStatementExecuted(this, new ExecutionEventArgs(Status.CANCELED));
        }


    }
}
