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
using Firedump.core.sql.executor;
using Firedump.core;

namespace Firedump.usercontrols
{
    public partial class DataView : UserControl
    {
        private Editor editor;
        private string SQL;
        public DataView() : base()
        {
            InitializeComponent();
            this.dataGridView1.RowPostPaint += DataGridViewRowPostPaint;
            this.dataGridViewHistory.RowPostPaint += DataGridViewRowPostPaint;
            this.dataGridView1.KeyDown += DataGridViewKeyDownEvent;
            tabPageResult.ImageIndex = 0;
        }

        public DataView(Editor editor) : this()
        {
            this.editor = editor;
        }

        public void SetData(DataTable data,string sql)
        {
            this.SuspendLayout();
            this.dataGridView1.SuspendLayout();
            this.SQL = sql;
            this.dataGridView1.DataSource = data;
            this.dataGridView1.ResumeLayout();
            this.ResumeLayout();
        }

        public void AppendData(DataTable data)
        {
            this.SuspendLayout();
            this.dataGridView1.SuspendLayout();
            int firstdisplayidx = this.dataGridView1.FirstDisplayedScrollingRowIndex;
            ((DataTable)this.dataGridView1.DataSource).Merge(data);
            if(firstdisplayidx > 1)
            {
                this.dataGridView1.FirstDisplayedScrollingRowIndex = firstdisplayidx;
            }
            this.dataGridView1.ResumeLayout();
            this.ResumeLayout();
        }


        internal void SetHistory(ExecutionQueryEvent e)
        {
            this.dataGridViewHistory.SuspendLayout();
            if (this.dataGridViewHistory.DataSource == null)
            {
                this.dataGridViewHistory.DataSource = new DataTable();
            }
            DataTable data = HistoryDataTableBuilder(e);
            int firstdisplayidx = this.dataGridViewHistory.FirstDisplayedScrollingRowIndex;
            ((DataTable)this.dataGridViewHistory.DataSource).Merge(data);
            if(firstdisplayidx > 1)
            {
                this.dataGridViewHistory.FirstDisplayedScrollingRowIndex = firstdisplayidx;
            }
            this.dataGridViewHistory.ResumeLayout();
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


        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll && dataGridView1.DisplayedRowCount(false) + dataGridView1.FirstDisplayedScrollingRowIndex
                >= dataGridView1.RowCount-1 && !this.editor.GetQueryExecutor().IsAlive())
            {
                this.editor.Fetch(new QueryParams() { Limit = this.editor.GetLimitFromMenuToolStripCombobox(), Offset = dataGridView1.RowCount, Hash = this.GetHashCode(),Sql = SQL });
            }
        }


        private DataTable HistoryDataTableBuilder(ExecutionQueryEvent e)
        {
            DataTable data = new DataTable();
            DataColumn c0 = new DataColumn("Status");
            DataColumn c1 = new DataColumn("Query");
            DataColumn c2 = new DataColumn("Rows affected");
            DataColumn c3 = new DataColumn("Info");
            DataColumn c4 = new DataColumn("Millis");
            DataColumn c5 = new DataColumn("Executed At");
            c0.DataType = System.Type.GetType("System.Byte[]");
            data.Columns.Add(c0);
            data.Columns.Add(c1);
            data.Columns.Add(c2);
            data.Columns.Add(c3);
            data.Columns.Add(c4);
            data.Columns.Add(c5);
            DataRow row = data.NewRow();
            if (e.Ex != null)
            {
                row["Status"] = IconHelper.status_error_arr;
            }
            else if (e.Status == Status.CANCELED)
            {
                row["Status"] = IconHelper.status_info_arr;
            }
            else
            {
                row["Status"] = IconHelper.status_ok_arr;
            }
            row["Query"] = e.query;
            row["Rows affected"] = e.recordsAffected;
            row["Info"] = e.Ex != null ? e.Ex.Message : "";
            row["Millis"] = e.duration.TotalMilliseconds;
            row["Executed At"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            data.Rows.Add(row);
            return data;
        }
    }
}
