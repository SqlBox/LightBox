using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Firedump.core.sql;
using Firedump.models.events;
using System.Data.Common;
using Firedump.core.db;
using Firedump.core.models.events;

namespace Firedump.usercontrols
{
    public partial class DataView : UserControl
    {
        private QueryExecutor executor;

        public DataView() { InitializeComponent(); }

        public DataView(QueryExecutor qe) : this()
        {
            this.executor = qe;
            this.executor.Finished += OnFinished;
            this.executor.StatementExecuted += OnStatementExecuted;
        }

        private void OnFinished(object sender,EventArgs e)
        {
            //enable action buttons
        }

        private void OnStatementExecuted(object sender,ExecutionEventArgs e)
        {
        }


        internal void ExecuteStatement(List<string> statements, DbConnection con)
        {
            if(DbUtils.IsConnectedToDatabase(con))
            {
                this.executor.Execute(statements, con);
            }
        }

        private void FetchNext(object sender, EventArgs e)
        {
            this.executor.FetchNext(100);
        }

        internal void StopRunningQuery()
        {
            this.executor.Cancel();
        }
    }
}
