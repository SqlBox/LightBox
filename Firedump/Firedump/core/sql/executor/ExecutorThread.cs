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
                    while (reader.Read())
                    {
                        Console.WriteLine(reader[0]);
                    }
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

        public override void FetchLimit(int fetchLimit)
        {
            this._fetchLimit = fetchLimit;
        }

    }
}
