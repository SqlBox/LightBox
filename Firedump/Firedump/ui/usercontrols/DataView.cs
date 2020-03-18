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
        public DataView() : base()
        {
            InitializeComponent();
            this.dataGridView1.RowPostPaint += DataGridViewRowPostPaint;
            this.dataGridView1.KeyDown += DataGridViewKeyDownEvent;
            tabPageResult.ImageIndex = 0;
        }

        public void SetData(DataTable data)
        {
            this.dataGridView1.DataSource = data;
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
