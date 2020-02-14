﻿using System;
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
        private string _query;
        private DbConnection _con;
        
        public BaseThread()
        {
        }

        public string Query() => this._query;
        public DbConnection Con() => this._con;

        public void Start(string query,DbConnection con)
        {
            this._query = query;
            this._con = con;
            this._thread = new Thread(new ThreadStart(run));
            this._thread.Start();
        }

        public bool IsAlive() => this._thread.IsAlive;
        public abstract void run();
        public abstract void FetchLimit(int fetchLimit);
    }

}
