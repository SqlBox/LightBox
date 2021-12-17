using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lightbox;

namespace Firedump.Forms.schedule
{
    public partial class EmailSchedule : Form
    {
        private Lightbox.LightboxdbDataSetTableAdapters.schedulesTableAdapter scheduleAdapter = new Lightbox.LightboxdbDataSetTableAdapters.schedulesTableAdapter();
        
        public EmailSchedule()
        {
            InitializeComponent();
            LightboxdbDataSet.schedulesDataTable scheduletable = new LightboxdbDataSet.schedulesDataTable();
            scheduleAdapter.FillOrderByDate(scheduletable);
            dataGridView1.DataSource = scheduletable;
        }


    }
}
