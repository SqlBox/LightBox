using Firedump.core.db;
using Firedump.core.models.events;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;

namespace Firedump.core.sql.executor
{
    public class ExecutorThread : BaseThread
    {
        private int _fetchLimit = 200;
        public bool _Alive = true;

        public ExecutorThread() : base()
        {
        }

        public ExecutorThread(int fetchLimit) : this()
        {
            this._fetchLimit = fetchLimit;
        }

        public override void run()
        {
            try
            {
                List<string> statements = base.Statements();
                if(statements != null && statements.Count > 0)
                {
                    for(int i=0; i < statements.Count; i++)
                    {
                        string statement = statements[i];
                        using (var reader = new DbCommandFactory(Con(), statement).Create().ExecuteReader())
                        {
                            Console.WriteLine("READING RESULTS...................");
                            int _limit_pointer = 0;
                            _Alive = true;
                            while (_Alive && reader.Read())
                            {
                                if (statements.Count == i)
                                {
                                    _limit_pointer++;
                                    Console.WriteLine("row:" + _limit_pointer + "," + reader[0]);
                                    if (_limit_pointer >= this._fetchLimit)
                                    {
                                        OnFinished(this, EventArgs.Empty);
                                        lock (this)
                                        {
                                            Monitor.Wait(this);
                                            _limit_pointer = 0;
                                        }
                                    }
                                }
                                if(!_Alive)
                                {
                                    break;
                                }
                            }
                            OnStatementExecuted(this, new ExecutionEventArgs());
                        }
                    }
                } else
                {
                    OnFinished(this, EventArgs.Empty);
                }
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }


        public override void SetFetchLimit(int fetchLimit)
        {
            this._fetchLimit = fetchLimit;
        }


        public override void Stop()
        {
            this._Alive = false;
            lock (this)
            {
                Monitor.PulseAll(this);
                OnFinished(this, EventArgs.Empty);
            }
            base.Join();
        }


    }
}
