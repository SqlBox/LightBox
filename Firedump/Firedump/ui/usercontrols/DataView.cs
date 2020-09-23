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
using System.Windows.Media;

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
            this.dataGridView1.ShowRowErrors = false;
            this.dataGridView1.ShowCellErrors = false;
            this.dataGridView1.CausesValidation = false;
            tabPageResult.ImageIndex = 0;
        }


        public DataView(Editor editor) : this()
        {
            this.editor = editor;
        }

        public void SetData(DataTable data,string sql)
        {
            this.SuspendLayout();
            this.SQL = sql;
            if ((data != null && data.Rows.Count > 0) || Utils.IsShowDataTypeOfCommand(sql))
            {
                this.dataGridView1.SuspendLayout();
                this.dataGridView1.DataSource = data;
                this.dataGridView1.ResumeLayout();
            }
            this.ResumeLayout();
        }

        public void AppendData(DataTable data)
        {
            if(this.dataGridView1.DataSource != null && data != null && data.Rows.Count > 0)
            {
                this.SuspendLayout();
                this.dataGridView1.SuspendLayout();
                int firstdisplayidx = this.dataGridView1.FirstDisplayedScrollingRowIndex;
                ((DataTable)this.dataGridView1.DataSource).Merge(data);
                if (firstdisplayidx > 1)
                {
                    this.dataGridView1.FirstDisplayedScrollingRowIndex = firstdisplayidx;
                }
                this.dataGridView1.ResumeLayout();
                this.ResumeLayout();
            }
        }


        internal void SetHistory(ExecutionQueryEvent e)
        {
            this.SuspendLayout();
            this.dataGridViewHistory.SuspendLayout();
            if (this.dataGridViewHistory.DataSource == null)
            {
                this.dataGridViewHistory.DataSource = new DataTable();
            }
            DataTable data = ControlBuilder.HistoryDataTableBuilder(e);
            int firstdisplayidx = this.dataGridViewHistory.FirstDisplayedScrollingRowIndex;
            ((DataTable)this.dataGridViewHistory.DataSource).Merge(data);
            if(firstdisplayidx > 1)
            {
                this.dataGridViewHistory.FirstDisplayedScrollingRowIndex = firstdisplayidx;
            }
            this.dataGridViewHistory.ResumeLayout();
            this.ResumeLayout();
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
                this.editor.Fetch(new QueryParams() { Limit = this.editor.GetMainHome().GetLimitFromToolStripComboBoxLimit(), Offset = dataGridView1.RowCount, Hash = this.GetHashCode(),Sql = SQL });
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
