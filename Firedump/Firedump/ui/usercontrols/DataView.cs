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
using Firedump.core.models.dbinfo;

namespace Firedump.usercontrols
{
    public partial class DataView : UserControl
    {
        private QueryExecutor executor;
        public event EventHandler<ExecutionEventArgs> StatementExecuted;
        private DataTable data;

        public DataView() { InitializeComponent(); }

        public DataView(QueryExecutor qe) : this()
        {
            this.executor = qe;
            this.executor.StatementExecuted += OnStatementExecuted;
        }


        private void OnStatementExecuted(object sender,ExecutionEventArgs e)
        {
            StatementExecuted?.Invoke(sender, e);
            if(e.Status == Status.FINISHED)
            {
                this.Invoke((MethodInvoker)delegate {
                    this.dataGridView1.DataSource = this.data = e.data;
                    Console.WriteLine("QUERY EXECUTED:" + e.query);
                    if(e.Ex != null)
                    {
                        Console.WriteLine("ERROR:" + e.Ex.Message);
                    }
                });
            }
        }


        internal void ExecuteStatement(List<string> statements, DbConnection con)
        {
            this.executor.Execute(statements, con);
        }


        internal void StopRunningQuery()
        {
            this.executor.Cancel();
        }

    }
}
