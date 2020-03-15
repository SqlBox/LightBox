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
            this.dataGridView1.RowPostPaint += DataGridViewRowPostPaint;
            this.dataGridView1.KeyDown += DataGridViewKeyDownEvent;
        }


        private void OnStatementExecuted(object sender,ExecutionEventArgs e)
        {
            StatementExecuted?.Invoke(sender, e);
            if(e.Status == Status.FINISHED)
            {
                this.Invoke((MethodInvoker)delegate {
                    if(e.data != null)
                    {
                        this.data = e.data;
                        this.dataGridView1.DataSource = this.data;
                    }
                    if(e.Ex != null)
                    {
                        Console.WriteLine("ERROR:" + e.Ex.Message);
                    }
                });
            }
            Console.WriteLine("QUERY EXECUTED:" + e.query);
        }


        internal void ExecuteStatement(List<string> statements, DbConnection con)
        {
            this.executor.Execute(statements, con);
        }


        internal void StopRunningQuery()
        {
            this.executor.Cancel();
        }

        private void DataGridViewKeyDownEvent(object sender,KeyEventArgs e)
        {
            //prevent control+shift+ up or down
            //that selects all table, in large data it could be very slow to ui not responding at all
            //datagridview rowheader corner click selects all table that apparently and for some unknown reason is fast even in 10 millions rows
            if((e.KeyCode == Keys.Up || e.KeyCode == Keys.Down) && (e.Shift && e.Control))
            {
                e.Handled = true;
            }
        }

        private void DataGridViewRowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dg = (DataGridView)sender;
            // Current row record
            string rowNumber = (e.RowIndex + 1).ToString();
            // Position text
            SizeF size = e.Graphics.MeasureString(rowNumber, this.Font);
            if (dg.RowHeadersWidth < (int)(size.Width + 10)) dg.RowHeadersWidth = (int)(size.Width + 10);
            // Draw row number
            e.Graphics.DrawString(rowNumber, dg.Font, SystemBrushes.WindowText, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
        }
    }
}
