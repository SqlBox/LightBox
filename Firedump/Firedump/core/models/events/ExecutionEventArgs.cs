using Firedump.core.models.dbinfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firedump.core.models.events
{
    public class ExecutionEventArgs<T> : EventArgs
    {
        public Status Status;
        public Exception Ex;
        public T Value;

        public ExecutionEventArgs(Status s):base()
        {
            this.Status = s;
        }

    }
}
