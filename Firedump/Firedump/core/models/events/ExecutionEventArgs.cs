using Firedump.core.models.dbinfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firedump.core.models.events
{
    public class ExecutionEventArgs : EventArgs
    {
        public Status Status;
        public Exception Ex;
        public DataTable data;
        public string query;
        public int TAG;
        public ExecutionEventArgs(Status s):base()
        {
            this.Status = s;
        }
    }
}
