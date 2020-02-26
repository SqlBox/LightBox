using Firedump.core.db;
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
                Console.WriteLine(base.Query());
                using (var reader = new DbCommandFactory(Con(), Query()).Create().ExecuteReader())
                {
                    Console.WriteLine("READING RESULTS...................");
                    int _limit_pointer = 0;
                    _Alive = true;
                    while (_Alive && reader.Read())
                    {
                        _limit_pointer++;
                        Console.WriteLine("row:" + _limit_pointer + "," + reader[0]);
                        if (_limit_pointer >= this._fetchLimit)
                        {
                            lock (this)
                            {
                                Monitor.Wait(this);
                                _limit_pointer = 0;
                            }
                        }
                    }
                    Console.WriteLine("FINISHED READING RESULTS");
                }
                Console.WriteLine("READER CLOSED");
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
            }
            base.Join();
        }
    }
}
