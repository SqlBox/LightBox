﻿using Firedump.models.databaseUtils;
using Firedump.models.pojos;
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
    public partial class NewJobForm : Form
    {
        public delegate void onSetJobDetails(JobDetail jobDetail);
        public event onSetJobDetails setJobDetails;
        private List<string> tables;
        private LightboxdbDataSet.backup_locationsDataTable loctable;

        private void OnSetJobDetails(JobDetail jobDetail)
        {
            setJobDetails?.Invoke(jobDetail);
        }

        private Lightbox.LightboxdbDataSet.sql_serversDataTable serverData;
        private Lightbox.LightboxdbDataSetTableAdapters.sql_serversTableAdapter mysql_serversAdapter = new Lightbox.LightboxdbDataSetTableAdapters.sql_serversTableAdapter();

        public NewJobForm()
        {
            InitializeComponent();
            backgroundWorker1.DoWork += fillDatabaseCmb;
            loadComboBoxServers();
            loadlocationsCombobox();      
        }

        private void loadlocationsCombobox()
        {
            Lightbox.LightboxdbDataSetTableAdapters.backup_locationsTableAdapter locAdapter = new Lightbox.LightboxdbDataSetTableAdapters.backup_locationsTableAdapter();
            loctable = new Lightbox.LightboxdbDataSet.backup_locationsDataTable();
            locAdapter.Fill(loctable);
            cblocation.DataSource = loctable;
            cblocation.DisplayMember = "name";
            cblocation.ValueMember = "id";        
            if(cblocation.Items.Count > 0)
            {
                cblocation.SelectedIndex = 0;
            }
        }

        private void loadComboBoxServers()
        {
            serverData = new Lightbox.LightboxdbDataSet.sql_serversDataTable();
            mysql_serversAdapter = new Lightbox.LightboxdbDataSetTableAdapters.sql_serversTableAdapter();
            mysql_serversAdapter.Fill(serverData);
            cmbServers.DataSource = serverData;
            cmbServers.DisplayMember = "name";
            cmbServers.ValueMember = "id";
            if (cmbServers.Items.Count > 0)
            {
                cmbServers.SelectedIndex = 0;
                //backgroundWorker1.RunWorkerAsync();
                fillDatabaseCmb(null,null);
            }
        }


        private void setjobclick(object sender, EventArgs e)
        {
            
            //validate inputs
            //check if other schedule is in same minute.
            string jobname = tbjobname.Text;
            if (String.IsNullOrEmpty(jobname))
            {
                MessageBox.Show("Job must have a Name!");
                return;
            }

            if(cblocation.Items.Count <= 0)
            {
                MessageBox.Show("No destination location set!");
                return;
            }

            int day = (int)numericDay.Value;
            int hour = (int)numericHour.Value;
            int minute = (int)numericMinute.Value;
            if (!isTimeValid(day, hour, minute))
            {
                MessageBox.Show("Cant set this!Other Job is with Same Date/Time");
                return;
            }

            int ibinterval = (int)numericIBInterval.Value;
            int idbinterval = (int)numericIDBInterval.Value;

            DbConnection con = new DbConnection();           
            con.Host = (string)serverData.Rows[cmbServers.SelectedIndex]["host"];
            con.port = unchecked((int)(long)serverData.Rows[cmbServers.SelectedIndex]["port"]);
            con.username = (string)serverData.Rows[cmbServers.SelectedIndex]["username"];
            con.password = (string)serverData.Rows[cmbServers.SelectedIndex]["password"];
            

            try {
                if (con.testConnection().wasSuccessful)
                {
                    tables = con.getTables(cmbdatabase.SelectedItem.ToString());
                    string locname = (string)loctable.Rows[cblocation.SelectedIndex]["name"];
                    int id = int.Parse(loctable.Rows[cblocation.SelectedIndex]["id"].ToString());

                    
                    JobDetail jobdetails = new JobDetail();
                    jobdetails.DayOfWeek = day;
                    jobdetails.Hour = hour;
                    jobdetails.Minute = minute;
                    jobdetails.Name = jobname;
                    jobdetails.Database = cmbdatabase.SelectedItem.ToString();
                    jobdetails.Tables = tables;
                    if (cbIncremental.Checked)
                    {
                        jobdetails.Tables.Insert(0, idbinterval.ToString());
                        jobdetails.Tables.Insert(0, ibinterval.ToString());
                        jobdetails.Tables.Insert(0, "inc_enabled");
                    }
                    jobdetails.Server = (Lightbox.LightboxdbDataSet.sql_serversRow)serverData.Rows[cmbServers.SelectedIndex];
                    jobdetails.LocationId = id;
                    jobdetails.LocationName = locname;
                    int activate = 0;
                    if (!cbactivate.Checked)
                        activate = 1;
                    jobdetails.Activate = activate;

                    OnSetJobDetails(jobdetails);
                    this.Close();
                    
                } else
                {
                    MessageBox.Show("Error connection to server");
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }    

        }


        private bool isTimeValid(int day, int hour, int minute)
        {

            Lightbox.LightboxdbDataSetTableAdapters.schedulesTableAdapter scheduleAdapter = new Lightbox.LightboxdbDataSetTableAdapters.schedulesTableAdapter();
            Lightbox.LightboxdbDataSet.schedulesDataTable scheduletable = new Lightbox.LightboxdbDataSet.schedulesDataTable();
            scheduleAdapter.FillOrderByDate(scheduletable);
            if(scheduletable.Count > 0)
            {
                foreach(Lightbox.LightboxdbDataSet.schedulesRow row in scheduletable)
                {
                    if (isScheduleOverLap(row, day, hour, minute))
                        return false;
                }
            }

            return true;
        }

        private bool isScheduleOverLap(Lightbox.LightboxdbDataSet.schedulesRow row,int day,int hour,int minute)
        {
            if (row.day == day && row.hours == hour && row.minutes == minute)
                return true;
            return false;
        }

        private void fillDatabaseCmb(object sender, DoWorkEventArgs args)
        {
            
            if (cmbServers.Items.Count == 0) { return; } //ama den iparxei kanenas server den to kanei           
            DbConnection con = new DbConnection();

            con.Host = (string)serverData.Rows[cmbServers.SelectedIndex]["host"];
            con.port = unchecked((int)(long)serverData.Rows[cmbServers.SelectedIndex]["port"]);
            con.username = (string)serverData.Rows[cmbServers.SelectedIndex]["username"];
            con.password = (string)serverData.Rows[cmbServers.SelectedIndex]["password"];

            Console.WriteLine("cmbServers.Items.Count" + cmbServers.Items.Count);
            //edw prepei na bei to database kai mia if then else apo katw analoga ama kanei connect se server i se database
            try {
                ConnectionResultSet result = con.testConnection();
                if (result.wasSuccessful)
                {
                    List<string> databases = con.getDatabases();

                    databases.Remove("information_schema");
                    databases.Remove("mysql");
                    databases.Remove("performance_schema");
                    databases.Remove("sys");

                    cmbdatabase.Items.Clear();

                    foreach (string database in databases)
                    {
                        TreeNode node = new TreeNode(database);
                        node.ImageIndex = 0;
                        cmbdatabase.Items.Add(database);
                        Console.WriteLine(database);
                    }

                    if (cmbdatabase.Items.Count > 0)
                        cmbdatabase.SelectedIndex = 0;

                }
                else
                {
                    MessageBox.Show("Connection failed: \n" + result.errorMessage, "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void cmbServers_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fillDatabaseCmb(null, null);

            /*
            if (!backgroundWorker1.IsBusy)
            {
                if(cmbServers.Items.Count > 0)
                    backgroundWorker1.RunWorkerAsync();
            }
            */

        }
    }
}
