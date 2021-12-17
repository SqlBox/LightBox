﻿using Firedump.Forms.mysql;
using Firedump.models.pojos;
using Firedump.service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Firedump.Forms.schedule
{
    public partial class SchedulerForm : Form
    {
        private Lightbox.LightboxdbDataSetTableAdapters.sql_serversTableAdapter serverAdapter = new Lightbox.LightboxdbDataSetTableAdapters.sql_serversTableAdapter();
        private Lightbox.LightboxdbDataSet.sql_serversRow server;
        private Lightbox.LightboxdbDataSet.sql_serversDataTable serverdataTable = new Lightbox.LightboxdbDataSet.sql_serversDataTable();
        
        private List<JobDetail> newJobs = new List<JobDetail>();
        
        public SchedulerForm()
        {   
            InitializeComponent();
            Lightbox.LightboxdbDataSetTableAdapters.schedulesTableAdapter scheduleAdapter = new Lightbox.LightboxdbDataSetTableAdapters.schedulesTableAdapter();
            Lightbox.LightboxdbDataSet.schedulesDataTable scheduleTable = new Lightbox.LightboxdbDataSet.schedulesDataTable();
            scheduleAdapter.FillOrderByDate(scheduleTable);

            dataGridView1.DataSource = scheduleTable;
        }
        
        private void SchedulerForm_Load(object sender, EventArgs e)
        {
            //this.schedulesTableAdapter.Fill(this.Lightbox.LightboxdbDataSet.schedules);
            ServiceController sc = serviceManager1.GetServiceStatus();
            if(sc == null)
            {
                serviceManager1.setPictureIcon(false);
                serviceManager1.setStatusText("Firedump Schedule service is not installed");
            }
            else if (sc.Status == ServiceControllerStatus.Running)
            {
                serviceManager1.setPictureIcon(true);
                serviceManager1.setStatusText("Firedump Schedule service is Running");
            } else if(sc.Status == ServiceControllerStatus.Stopped)
            {
                serviceManager1.setPictureIcon(false);
                serviceManager1.setStatusText("Firedump Schedule service is Stopped");
            }
                
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //change activated value witch is 0 or 1 to true false, true for 0 false for 1           
            if(e.RowIndex > 0)
            {
                dataGridView1.Rows[e.RowIndex - 1].Cells[1].Value = "Delete";
                int num = 0;
                if (int.TryParse(dataGridView1.Rows[e.RowIndex - 1].Cells[6].Value.ToString(), out num))
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex - 1].Cells[0];
                    if (num == 0)
                    {
                        chk.Value = true;
                        chk.Style.BackColor = Color.Green;
                    }
                    else
                    {
                        chk.Value = false;
                        chk.Style.BackColor = Color.Red;
                    }
                        

                }                  
            }
            
        }


        private void newJobClick(object sender, EventArgs e)
        {
            //run as admin
            //stop service

            //use adds new server-database
            NewJobForm jobForm = new NewJobForm();
            jobForm.setJobDetails += onJobSetDetails;
            jobForm.ShowDialog();
            //service will start at applyChanges button
        }


        private void onJobSetDetails(JobDetail jobDetails)
        {

            Random rnd = new Random();
            int sec = rnd.Next(10,50);
            jobDetails.Second = sec;

            newJobs.Add(jobDetails);
            Console.WriteLine(jobDetails.DayOfWeek);
            
            Lightbox.LightboxdbDataSetTableAdapters.schedulesTableAdapter scheduleAdapter = new Lightbox.LightboxdbDataSetTableAdapters.schedulesTableAdapter();
            string tmptables = "-";
            if (jobDetails.Tables[0] == "inc_enabled")
            {
                tmptables = jobDetails.Tables[1] + "?>?" + jobDetails.Tables[2];
                jobDetails.Tables.RemoveAt(0);
                jobDetails.Tables.RemoveAt(0);
                jobDetails.Tables.RemoveAt(0);
            }
            
            scheduleAdapter.Insert((int)jobDetails.Server.id, jobDetails.Name,DateTime.Now,jobDetails.Activate, jobDetails.Hour, jobDetails.Database,tmptables,jobDetails.Minute,jobDetails.Second,jobDetails.DayOfWeek);           
            Lightbox.LightboxdbDataSet.schedulesDataTable scheduleTable = new Lightbox.LightboxdbDataSet.schedulesDataTable();
           
            scheduleAdapter.FillIdByName(scheduleTable,jobDetails.Name);
            int scheduleId = (int)scheduleTable[0].id;
            int locId = jobDetails.LocationId;
            
            Lightbox.LightboxdbDataSetTableAdapters.schedule_save_locationsTableAdapter savelocAdapter = new Lightbox.LightboxdbDataSetTableAdapters.schedule_save_locationsTableAdapter();
            savelocAdapter.Insert(scheduleId, locId);
            
            Lightbox.LightboxdbDataSetTableAdapters.backup_locationsTableAdapter backAdapter = new Lightbox.LightboxdbDataSetTableAdapters.backup_locationsTableAdapter();
            Lightbox.LightboxdbDataSet.backup_locationsDataTable backuptable = new Lightbox.LightboxdbDataSet.backup_locationsDataTable();
            backuptable = backAdapter.GetDataByID(locId);
            dataGridViewlocs.DataSource = backuptable;

            scheduleAdapter.FillOrderByDate(scheduleTable);
            dataGridView1.DataSource = scheduleTable;
        }


        private void reloadserverData(int id)
        {           
            /*
            serverAdapter.Fill(serverdataTable);
            int i = 0;
            foreach (Lightbox.LightboxdbDataSet.sql_serversRow row in serverdataTable)
            {
                if (row.id == id)
                {
                    //
                    break;
                }
                i++;
            }
            */
        }

        private void newmysqlserver_click(object sender, EventArgs e)
        {
            NewMySQLServer newMysqlServer = new NewMySQLServer();
            newMysqlServer.ReloadServerData += reloadserverData;
            newMysqlServer.ShowDialog();
        }



        private void applychangesClick(object sender, EventArgs e)
        {
            ServiceController sc = GetServiceStatus();
            if (sc == null)
            {
                MessageBox.Show("Install the Service First!");
                serviceManager1.setStatusText("Install the Service First!");
                return;
            }

            try
            {
                //Start  the service
                Process proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "servicerestart.bat",
                        UseShellExecute = true,
                        Verb = "runas",
                        CreateNoWindow = true
                    }
                };

                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception ex) { }

            //after that , check to see if the service started successfully 
            sc = GetServiceStatus();
            if (sc == null)
            {
                MessageBox.Show("Error, cant start the service");
                serviceManager1.setStatusText("Error, cant start the service");
                serviceManager1.setPictureIcon(false);
            }
            else
            {
                if (sc.Status == ServiceControllerStatus.Running)
                {
                    serviceManager1.setStatusText("Service started");
                    serviceManager1.setPictureIcon(true);
                }

            }

        }

        /// <summary>
        /// See ServiceControllerStatus for enum info
        /// </summary>
        /// <returns>serviceController.Status enum, null if service is not installed</returns>
        public ServiceController GetServiceStatus()
        {
            ServiceController sc = ServiceController.GetServices()
               .FirstOrDefault(s => s.ServiceName == Consts.SERVICE_NAME);
            if (sc == null)
                return null;

            return sc;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            if(e.ColumnIndex == 1)
            {
                int scheduleId = 0;
                if(int.TryParse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString(),out scheduleId))
                {
                   if(scheduleId != -1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete this Job?", "Delete Schedule-Job", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            //delete from userinfo, schedule_save_location and logs first
                            //userinfo
                            Lightbox.LightboxdbDataSetTableAdapters.userinfoTableAdapter userAdapter = new Lightbox.LightboxdbDataSetTableAdapters.userinfoTableAdapter();
                            userAdapter.DeleteQueryByScheduleid(scheduleId);
                            
                            //logs
                            Lightbox.LightboxdbDataSetTableAdapters.logsTableAdapter logAdapter = new Lightbox.LightboxdbDataSetTableAdapters.logsTableAdapter();
                            logAdapter.DeleteQueryByScheduleid(scheduleId);
                            
                            //save_locations
                            Lightbox.LightboxdbDataSetTableAdapters.schedule_save_locationsTableAdapter saveLocAdapter = new Lightbox.LightboxdbDataSetTableAdapters.schedule_save_locationsTableAdapter();
                            saveLocAdapter.DeleteQueryByScheduleId(scheduleId);

                            //last delete from schedule
                            schedulesTableAdapter.DeleteQueryById(scheduleId);

                            Lightbox.LightboxdbDataSet.schedulesDataTable scheduleTable = new Lightbox.LightboxdbDataSet.schedulesDataTable();
                            schedulesTableAdapter.FillOrderByDate(scheduleTable);
                            dataGridView1.DataSource = scheduleTable;
                        }
                    } 
                }
            } else
            {
                int scheduleId = 0;
                if (int.TryParse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString(), out scheduleId))
                {
                    if (scheduleId != -1)
                    {
                        Lightbox.LightboxdbDataSetTableAdapters.schedule_save_locationsTableAdapter savelocAdapter = new Lightbox.LightboxdbDataSetTableAdapters.schedule_save_locationsTableAdapter();
                        Lightbox.LightboxdbDataSetTableAdapters.backup_locationsTableAdapter backAdapter = new Lightbox.LightboxdbDataSetTableAdapters.backup_locationsTableAdapter();
                        Lightbox.LightboxdbDataSet.schedule_save_locationsDataTable saveloctable = new Lightbox.LightboxdbDataSet.schedule_save_locationsDataTable();
                        savelocAdapter.FillByScheduleId(saveloctable, scheduleId);
                        Lightbox.LightboxdbDataSet.backup_locationsDataTable backuptable = new Lightbox.LightboxdbDataSet.backup_locationsDataTable();
                        if(saveloctable.Count > 0)
                        {
                            backuptable = backAdapter.GetDataByID(saveloctable[0].backup_location_id);
                            dataGridViewlocs.DataSource = backuptable;
                        }
                        //MessageBox.Show(scheduleId+" "+saveloctable.Count.ToString());

                    }
                }
            }
        }

        private void emailScheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmailSchedule emailform = new EmailSchedule();
            emailform.Show();
        }
    }
}
