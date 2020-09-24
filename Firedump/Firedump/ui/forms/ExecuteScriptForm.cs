using Firedump.core.db;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Firedump.ui.forms
{
    public partial class ExecuteScriptForm : Form
    {
        private DbConnection con;
        private BackgroundWorker worker = new BackgroundWorker() { WorkerSupportsCancellation = true};
        private string file;

        public ExecuteScriptForm(DbConnection c,string file)
        {
            InitializeComponent();
            FormUtils.setFormIcon(this);
            this.con = c;
            this.file = file;
            this.label1.Text = "File: "+file; 
            worker.DoWork += loadAndExecute;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if(worker.IsBusy)
            {
                progressBar1.Enabled = false;
                progressBar1.Visible = false;
                labelStatus.Text = "Cancel";
                worker.CancelAsync();
            }
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            if(!worker.IsBusy)
            {
                progressBar1.Enabled = true;
                progressBar1.Visible = true;
                worker.RunWorkerAsync();
            }
        }


        private void loadAndExecute(object sender,DoWorkEventArgs e)
        {
            try
            {
                if (core.db.DB.IsConnectedToDatabase(con))
                {
                    List<string> list = core.sql.Utils.convert(Firedump.core.sql.Utils.GetDbTypeEnum(con), File.ReadAllText(file));
                    Invoke((MethodInvoker)delegate () {
                        progressBar1.Maximum = list.Count;
                        progressBar1.Step = 1;
                        progressBar1.Value = 0;
                        labelStatus.Text = "Running";
                    });
                    foreach (string q in list)
                    {
                        if (worker.CancellationPending == true)
                        {
                            e.Cancel = true;
                            return;
                        }
                        using (var Command = new DbCommandFactory(con, q).Create())
                        {
                            int res = Command.ExecuteNonQuery();
                        }
                        Invoke((MethodInvoker)delegate () {
                            progressBar1.PerformStep();
                        });
                    }
                }
            }
            finally {
                Invoke((MethodInvoker)delegate () {
                    progressBar1.Enabled = false;
                    progressBar1.Visible = false;
                    labelStatus.Text = "Completed";
                });
            }
        }


    }
}
