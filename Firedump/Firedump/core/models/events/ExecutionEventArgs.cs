using Firedump.core.models.dbinfo;
using Firedump.core.sql.executor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firedump.core.models.events
{
    public class ExecutionQueryEvent : EventArgs
    {
        public Status Status;
        public Exception Ex;
        public DataTable data;
        public string query;
        public int TAG;
        public QueryParams QueryParams;


        //duration is the time took to execute a query
        //secondaryDuration could be the time that took to render and display the results or something else
        public TimeSpan duration;
        public TimeSpan secondaryDuration;
        public int recordsAffected;
        public ExecutionQueryEvent(Status s) : base()
        {
            this.Status = s;
        }
    }
}
