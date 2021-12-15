﻿using Firedump.models.configuration.dynamicconfig;
using Firedump.models.databaseUtils;
using Firedump.models.dump;
using Firedump.Forms.mysql.sqlviewer;
using Firedump.Forms.mysql.status;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firedump.Forms.configuration;
using Firedump.Forms.mysql;
using Firedump.Forms.location;
using Firedump.models.location;
using Firedump.Forms.sqlimport;
using Firedump.Forms.schedule;
using Firedump.Forms;
using Firedump.models.configuration.jsonconfig;

namespace Firedump
{
    public partial class Home : Form
    {
        private firedumpdbDataSet.mysql_serversDataTable serverData;
        private firedumpdbDataSetTableAdapters.mysql_serversTableAdapter mysql_serversAdapter = new firedumpdbDataSetTableAdapters.mysql_serversTableAdapter();
        private List<firedumpdbDataSet.backup_locationsRow> backuplocations;
        private LocationAdapterManager adapterLocation;
        private BinlogDumpAdapter logadapter;
        private MySqlDumpAdapter adapter;
        private List<string> databaseList;
        private List<String> tableList = new List<string>();
        private bool hideSystemDatabases = true;
        private string locationName = "";
        private ProgressFormContainer progressContainer;
        private string fnamePrefix = "";
        //form instances
        private static GeneralConfiguration genConfig;
        private GeneralConfiguration getGenConfigInstance()
        {
            if(genConfig == null)
            {
                genConfig = new GeneralConfiguration();
            }            
            return genConfig;
        }
        
        public Home()
        {
            InitializeComponent();
            //configuration initialization
            ConfigurationManager.getInstance().initializeConfig();
            adapter = new MySqlDumpAdapter();
            logadapter = new BinlogDumpAdapter();
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {
             
        }


        private void btAddDestClick(object sender, EventArgs e)
        {
            LocationSwitchboard locswitch = new LocationSwitchboard();
            locswitch.AddSaveLocation += addToLbSaveLocation;
            locswitch.SaveLocationDeleted += deleteSaveLocation;
            locswitch.ShowDialog();
        }

        private void miConfiguration_Click(object sender, EventArgs e)
        {
            try
            {
                getGenConfigInstance().Show();
            }
            catch (ObjectDisposedException ex)
            {
                genConfig = new GeneralConfiguration();
                miConfiguration_Click(null,null);
            }
        }

        private void bAddServer_Click(object sender, EventArgs e)
        {
            NewMySQLServer newMysqlServer = new NewMySQLServer();
            newMysqlServer.ReloadServerData += reloadserverData;
            newMysqlServer.ShowDialog();
        }

        

        private void loadServerData()
        {
            serverData = new firedumpdbDataSet.mysql_serversDataTable();
            mysql_serversAdapter = new firedumpdbDataSetTableAdapters.mysql_serversTableAdapter();
            mysql_serversAdapter.Fill(serverData);
            cmbServers.DataSource = serverData;           
            cmbServers.DisplayMember = "name";
            cmbServers.ValueMember = "id";
            if(cmbServers.Items.Count > 0)
            {
                cmbServers.SelectedIndex = 0;
            }
            
        }

        private void fillTreeView()
        {
           
            if (cmbServers.Items.Count == 0) { return; } //ama den iparxei kanenas server den to kanei
            DbConnection con = new DbConnection();

            this.Invoke((MethodInvoker)delegate ()
            {
                con.Host = (string)serverData.Rows[cmbServers.SelectedIndex]["host"];
                con.port = unchecked((int)(long)serverData.Rows[cmbServers.SelectedIndex]["port"]);
                con.username = (string)serverData.Rows[cmbServers.SelectedIndex]["username"];
                con.password = (string)serverData.Rows[cmbServers.SelectedIndex]["password"];
            });
          
            //edw prepei na bei to database kai mia if then else apo katw analoga ama kanei connect se server i se database
            ConnectionResultSet result = con.testConnection();
            if (result.wasSuccessful)
            {
                List<string> databases = con.getDatabases();
                if (hideSystemDatabases)
                {
                    databases.Remove("information_schema");
                    databases.Remove("mysql");
                    databases.Remove("performance_schema");
                    databases.Remove("sys");
                }              
                foreach (string database in databases)
                {
                    this.Invoke((MethodInvoker)delegate () {
                        TreeNode node = new TreeNode(database);
                        node.ImageIndex = 0;
                        List<string> tables = con.getTables(database);
                        foreach (string table in tables)
                        {
                            TreeNode tablenode = new TreeNode(table);
                            tablenode.ImageIndex = 1;
                            node.Nodes.Add(tablenode);
                        }
                        tvDatabases.Nodes.Add(node);
                    });                   
                }

                this.Invoke((MethodInvoker)delegate () {
                    ToolStripMenuItem opendb = new ToolStripMenuItem();
                    ToolStripMenuItem analyzedb = new ToolStripMenuItem();
                    opendb.Text = "browse data";
                    opendb.Tag = "sql";
                    opendb.Click += new EventHandler(menuClick);
                    analyzedb.Text = "inspect database";
                    analyzedb.Click += new EventHandler(menuClick);
                    ContextMenuStrip menu = new ContextMenuStrip();
                    menu.Items.AddRange(new ToolStripMenuItem[] { opendb,analyzedb});                    
                    tvDatabases.ContextMenuStrip = menu;
                });
            }
            else
            {
                this.Invoke((MethodInvoker)delegate () {
                    MessageBox.Show("Connection failed: \n" + result.errorMessage, "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
            }
        }

        private void menuClick(object sender,EventArgs e)
        {
            string Host = (string)serverData.Rows[cmbServers.SelectedIndex]["host"];
            int port = unchecked((int)(long)serverData.Rows[cmbServers.SelectedIndex]["port"]);
            string username = (string)serverData.Rows[cmbServers.SelectedIndex]["username"];
            string password = (string)serverData.Rows[cmbServers.SelectedIndex]["password"];

            mysql_servers server = new mysql_servers();
            server.host = Host;
            server.port = port;
            server.username = username;
            server.password = password;
            if (tvDatabases.SelectedNode != null && tvDatabases.SelectedNode.Parent == null)
            {
                string database = tvDatabases.SelectedNode.Text;
                if(sender.ToString() == "browse data")
                {
                    SqlDbViewerForm sqlform = new SqlDbViewerForm(server, database);
                    sqlform.Show();
                } else if(sender.ToString() == "inspect database")
                {
                    AnalyzeDbForm adbf = new AnalyzeDbForm(server, database);
                    adbf.Show();
                }
                
            } else
            {
                if(tvDatabases.SelectedNode != null && tvDatabases.SelectedNode.Parent != null)
                {
                    string database = tvDatabases.SelectedNode.Parent.Text;
                    if (sender.ToString() == "browse data")
                    {
                        SqlDbViewerForm sqlform = new SqlDbViewerForm(server, database);
                        sqlform.Show();
                    }
                    else if (sender.ToString() == "inspect database")
                    {
                        AnalyzeDbForm adbf = new AnalyzeDbForm(server, database);
                        adbf.Show();
                    }
                }
               
            }
        }

        public void addToLbSaveLocation(BackupLocation loc)
        {
            firedumpdbDataSet.backup_locationsRow row = (firedumpdbDataSet.backup_locationsRow)loc.Tag;          
            int imageindex;
            switch (row.service_type)
            {
                case 0: //local
                    imageindex = 0;
                    break;
                case 1: //ftp
                    imageindex = 1;
                    break;
                case 2: //dropbox
                    imageindex = 2;
                    break;
                case 3: //google drive
                    imageindex = 3;
                    break;
                default:
                    imageindex = 0;
                    break;

            }
            ListViewItem item = new ListViewItem(row.name,imageindex);
            item.SubItems.Add(loc.path);
            item.Tag = (firedumpdbDataSet.backup_locationsRow)loc.Tag;
            ListViewItem saveItem = findItemSaveLoc(loc);
            if (lbSaveLocations.Items.Contains(saveItem))
            {
                return;
            }
            lbSaveLocations.Items.Add(item);         
               
        }

       

        private void Home_Load(object sender, EventArgs e)
        {
            loadServerData();

            ImageList imagelist = new ImageList();
            imagelist.Images.Add(Bitmap.FromFile("resources/icons/databaseimage.bmp"));
            imagelist.Images.Add(Bitmap.FromFile("resources/icons/tableimage.bmp"));
            tvDatabases.ImageList = imagelist;

            imagelist = new ImageList();
            imagelist.Images.Add(Bitmap.FromFile("resources/icons/thispc.bmp"));
            imagelist.Images.Add(Bitmap.FromFile("resources/icons/ftpimage.bmp"));
            imagelist.Images.Add(Bitmap.FromFile("resources/icons/dropboximage.bmp"));
            imagelist.Images.Add(Bitmap.FromFile("resources/icons/googledriveicon.bmp"));
            lbSaveLocations.SmallImageList = imagelist;
            

            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.DoWork += treeview_work;
            backgroundWorker1.RunWorkerAsync();           
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            if (cmbServers.Items.Count == 0)
            {
                MessageBox.Show("There are no servers to delete","Server Delete",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            DialogResult result = MessageBox.Show("Are you sure you want to delete server: " + ((DataRowView)cmbServers.Items[cmbServers.SelectedIndex])["name"], "Server Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                serverData.Rows[cmbServers.SelectedIndex].Delete();
                mysql_serversAdapter.Update(serverData); //fernei to table sto database stin katastasi tou datatable
                cmbServers_SelectionChangeCommitted(null,null);
            }
        }


        private void tvDatabases_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //an kapio child ginei checked na ginei kai o parent
            //an ginei unchecked kai to telefteo pedi na ginei unchecked kai o parent
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Parent != null)
                {
                    if (!e.Node.Checked)
                    {

                        bool found = false;
                        foreach (TreeNode n in e.Node.Parent.Nodes)
                        {
                            if (n.Checked)
                            {
                                e.Node.Parent.Checked = true;
                                found = true;
                                break;
                            }                           
                        }
                        if (!found)
                            e.Node.Parent.Checked = false;
                    }
                    else
                    {
                        e.Node.Parent.Checked = true;
                    }
                }
                else
                {                   
                    if(e.Node.Checked)
                    {
                        this.checkAllChildNodes(true, e.Node);
                    } else
                    {
                        this.checkAllChildNodes(false, e.Node);
                    }                   
                }
            }
            

            /*
            // The code only executes if the user caused the checked state to change.
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count > 0)
                {
                   
                    //Calls the CheckAllChildNodes method, passing in the current 
                    //Checked value of the TreeNode whose checked state changed. 
                    this.checkAllChildNodes(e.Node.Checked, e.Node);
                }
            }
            */           
        }

        /// <summary>
        /// Recursively checks or unchecks all child nodes of a node
        /// </summary>
        /// <param name="nodeChecked">True to check nodes or false to uncheck</param>
        /// <param name="treeNode">the starting node</param>
        private void checkAllChildNodes(bool nodeChecked, TreeNode treeNode)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                /* akiro to recursion giati stin periptwsi mas den exoume pote parapanw apo 1 epipedo tsampa tha trwei porous
                if (node.Nodes.Count > 0)
                {
                    this.checkAllChildNodes(nodeChecked, node); 
                }*/
            }
        }

        private bool performChecks()
        {
            //elenxoi edw logika tha einai arketoi
            //<elenxoi>
            if (lbSaveLocations.Items.Count==0)
            {
                MessageBox.Show("No save locations. Add at least one save location and try again.", "MySQL Dump", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //</elenxoi>

            return true;
        }

        private void bStartDump_Click(object sender, EventArgs e)
        {
            if (!performChecks())
            {
                return;
            }
            if(adapter.isDumpRunning())
            {
                MessageBox.Show("dump is running...");
                return;
            }
            if (logadapter.isDumpRunning())
            {
                MessageBox.Show("dump is running...");
                return;
            }
            

            List<string> databases = new List<string>();
            List<string> excludedTables = new List<string>();
            tableList = new List<string>();
            foreach(TreeNode node in tvDatabases.Nodes)
            {
                if (node.Checked)
                {
                    databases.Add(node.Text);
                    string tables = "";
                    foreach(TreeNode childNode in node.Nodes)
                    {
                        if (!childNode.Checked)
                        {
                            tables += childNode.Text + ",";
                        } else
                        {
                            tableList.Add(childNode.Text);
                        }
                    }
                    if (tables != "")
                    {
                        tables = tables.Substring(0, tables.Length - 1); //vgazei to teleutaio comma
                    }
                    excludedTables.Add(tables);
                }
            }
            
            //testing
            /*
            IncrementalUtils testutils = new IncrementalUtils();
            List<string> chain = testutils.calculateChain("D:\\MyStuff\\Backups\\dumps\\test\\testdumpinc_IDB_0.2.2_2019-10-30 17,50,46", -1);
            foreach (string chainfname in chain)
            {
                Console.WriteLine("chain: "+chainfname);
            }
            bool b = true; //gia na mi vgazei unreachable code apo katw
            if (b) return;*/
            /*
            BinlogDumpCredentialsConfig configtest1 = new BinlogDumpCredentialsConfig();
            configtest1.host = (string)serverData.Rows[cmbServers.SelectedIndex]["host"];
            configtest1.port = unchecked((int)(long)serverData.Rows[cmbServers.SelectedIndex]["port"]);
            configtest1.username = (string)serverData.Rows[cmbServers.SelectedIndex]["username"];
            configtest1.password = (string)serverData.Rows[cmbServers.SelectedIndex]["password"];
            
            backuplocations = new List<firedumpdbDataSet.backup_locationsRow>();
            List<int> locationIds1 = new List<int>();
            foreach (ListViewItem item in lbSaveLocations.Items)
            {
                Object loc = item.Tag;
                backuplocations.Add((firedumpdbDataSet.backup_locationsRow)loc);
                locationIds1.Add((int)((firedumpdbDataSet.backup_locationsRow)loc).id);
            }
            configtest1.locationIds = locationIds1.ToArray();
            configtest1.isIncrementalDelta = true;
            IncrementalUtils iutils = new IncrementalUtils(configtest1);
            configtest1 = iutils.calculateDumpConfig();

            foreach (string logfile in configtest1.logfiles)
            {
                Console.WriteLine(logfile);
            }
            Console.WriteLine("Next prefix: " + configtest1.prefix);
            Console.WriteLine("StartDateTime: " + configtest1.startDateTime);
            bool a = true; //gia na mi vgazei unreachable code apo katw
            if (a) return;
            // /testing*/

            if (cIncrementalFormat.Checked && databases.Count() > 1)
            {
                MessageBox.Show("Only one database may be selected with incremental format enabled.", "MySQL dump", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (databases.Count == 0)
            {
                MessageBox.Show("No database selected", "MySQL Dump", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!cIncrementalFormat.Checked || rbFull.Checked)
            {
                adapter = new MySqlDumpAdapter();
                DumpCredentialsConfig config = new DumpCredentialsConfig();
                config.host = (string)serverData.Rows[cmbServers.SelectedIndex]["host"];
                config.port = unchecked((int)(long)serverData.Rows[cmbServers.SelectedIndex]["port"]);
                config.username = (string)serverData.Rows[cmbServers.SelectedIndex]["username"];
                config.password = (string)serverData.Rows[cmbServers.SelectedIndex]["password"];

                
                if (databases.Count == 1)
                {
                    config.database = databases[0];
                    if (excludedTables[0] != "")
                    {
                        config.excludeTablesSingleDatabase = excludedTables[0];
                    }
                }
                else
                {
                    databaseList = databases;
                    config.database = databases[0];
                    config.databases = databases.ToArray();
                    config.excludeTables = excludedTables.ToArray();
                }

                pbDumpExec.Value = 0;

                bStartDump.Enabled = false;
                adapter.setTableList(tableList);

                adapter.Cancelled += onCancelledHandler;
                adapter.Completed += onCompletedHandler;
                adapter.CompressProgress += compressProgressHandler;
                adapter.CompressStart += onCompressStartHandler;
                adapter.Error += onErrorHandler;
                adapter.InitDumpTables += initDumpTablesHandler;
                adapter.Progress += onProgressHandler;
                adapter.TableRowCount += tableRowCountHandler;
                adapter.TableStartDump += onTableDumpStartHandler;

                this.UseWaitCursor = true;
                adapter.startDump(config);
            }
            else
            {
                logadapter = new BinlogDumpAdapter();
                BinlogDumpCredentialsConfig config = new BinlogDumpCredentialsConfig();
                config.host = (string)serverData.Rows[cmbServers.SelectedIndex]["host"];
                config.port = unchecked((int)(long)serverData.Rows[cmbServers.SelectedIndex]["port"]);
                config.username = (string)serverData.Rows[cmbServers.SelectedIndex]["username"];
                config.password = (string)serverData.Rows[cmbServers.SelectedIndex]["password"];
                config.database = databases[0]; //if here only one database is selected due to above checks
                config.isIncrementalDelta = rbIncDelta.Checked;

                backuplocations = new List<firedumpdbDataSet.backup_locationsRow>();
                List<int> locationIds = new List<int>();
                foreach (ListViewItem item in lbSaveLocations.Items)
                {
                    Object loc = item.Tag;
                    backuplocations.Add((firedumpdbDataSet.backup_locationsRow)loc);
                    locationIds.Add((int)((firedumpdbDataSet.backup_locationsRow)loc).id);
                }
                config.locationIds = locationIds.ToArray();

                pbDumpExec.Value = 0;
                bStartDump.Enabled = false;

                logadapter.config = config;

                //handlers
                logadapter.Cancelled += onCancelledHandler;
                logadapter.Completed += onCompletedHandlerBinlog;
                logadapter.CompressProgress += compressProgressHandler;
                logadapter.CompressStart += onCompressStartHandler;
                logadapter.Progress += onProgressHandler;
                logadapter.Error += onErrorHandler;
                //handlers

                this.UseWaitCursor = true;
                logadapter.startDump();
            }

            

            
            
        }


        private void cmbServers_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //edw prepei na bei elenxos ean trexei eidh to filltreeview thread kai an trexei na ginei interrupt kai destroy
            tvDatabases.Nodes.Clear();
            if(!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
            
        }

        private void treeview_work(object sender, DoWorkEventArgs e)
        {
            fillTreeView();
        }


        private void resetPbarValue()
        {
            pbDumpExec.Invoke((MethodInvoker)delegate () {
                pbDumpExec.Value = 0;
                pbDumpExec.Refresh();
            });
        }

        private void maximizeProgressBar()
        {
            pbDumpExec.Invoke((MethodInvoker)delegate () {
                pbDumpExec.Value = pbDumpExec.Maximum;
            });
        }

        private void increaseProgressBarStep()
        {
            pbDumpExec.Invoke((MethodInvoker)delegate () {
                pbDumpExec.PerformStep();
            });
        }

        private void initProgressBar(List<string> tables,int max)
        {
            pbDumpExec.Invoke((MethodInvoker)delegate () {
                if(tables != null)
                {
                    pbDumpExec.Maximum = (tables.Count);
                } else
                {
                    pbDumpExec.Maximum = max;
                }
                pbDumpExec.Step = 1;
            });
        }

        private void setProgressValue(int progress)
        {
            pbDumpExec?.Invoke((MethodInvoker)delegate () {
                try
                {
                    pbDumpExec.Value = progress;
                }
                catch(ArgumentOutOfRangeException ex)
                {
                }
                
            });
        }

        private void cancelDumpClick(object sender, EventArgs e)
        {
            this.UseWaitCursor = false;
            if (adapter != null)
            {
                adapter.cancelDump();
                Task task = new Task(resetProgressBarAfterCancel);
                task.Start();
                lStatus.Text = "Cancelled";               
                resetPbarValue();
                tableList = new List<string>();
                bStartDump.Enabled = true;

                //cancel all the other locations
                if(backuplocations != null && backuplocations.Count > 0)
                {
                    foreach(firedumpdbDataSet.backup_locationsRow row in backuplocations)
                    {
                        if (!adapterLocation.isLocationFinished(row))
                        {
                            adapterLocation.cancelSaveLocation(row);
                        }
                    }
                }
            }

        }



        /// <summary>
        /// Callbacks are still comming from compress and process.
        /// We cant stop them because its exe
        /// so just wait a second after proc kill for all callbacks to come and then reset the progressbar
        /// </summary>
        async void resetProgressBarAfterCancel()
        {
            Thread.Sleep(1000);
            if (pbDumpExec != null)
            {
                resetPbarValue();
            }
        }

        
        private void btEditServer_Click(object sender, EventArgs e)
        {
            if (cmbServers.Items.Count > 0 && cmbServers.SelectedIndex >= 0)
            {
                firedumpdbDataSet.mysql_serversRow server = ((firedumpdbDataSet.mysql_serversDataTable)cmbServers.DataSource).ElementAt(cmbServers.SelectedIndex);
                NewMySQLServer newServer = new NewMySQLServer(true, server);
                newServer.ReloadServerData += reloadserverData;
                newServer.Show();
               
            }
        }

        private void reloadserverData(int id)
        {            
            mysql_serversAdapter.Fill(serverData);
            int i = 0;
            foreach(firedumpdbDataSet.mysql_serversRow row in serverData)
            {
                if(row.id == id)
                {
                    cmbServers.SelectedIndex = i;
                    cmbServers_SelectionChangeCommitted(null, null);
                    break;
                }
                i++;
            }
            
        }

        //
        //-------------------------------------------------------------------------
        //------->INTERFACE EVENT-CALLBACK METHODS START---------------------------
        private void onProgressHandler(string progress)
        {
            lStatus.Invoke((MethodInvoker)delegate () {
                lStatus.Text = progress;
            });
        }


        private void onErrorHandler(int error)
        {
            this.UseWaitCursor = false;
            lStatus.Invoke((MethodInvoker)delegate () {
                //to error pernei sigkekrimenes times
                //opote mporoume na to diksoume kalitera more info error
                lStatus.Text = "Error:"+error.ToString();
            });
            resetPbarValue();
        }


        private void onCancelledHandler()
        {
            this.UseWaitCursor = false;
            lStatus.Invoke((MethodInvoker)delegate () {              
                lStatus.Text = "Cancelled";
            });
            resetPbarValue();
        }


        private void onCompletedHandler(DumpResultSet status)
        {
            if(status != null)
            {
                lStatus.Invoke((MethodInvoker)delegate () {
                    lStatus.Text = "Completed";
                });

                pbDumpExec.Invoke((MethodInvoker)delegate () {
                    pbDumpExec.Value = pbDumpExec.Maximum;
                });           

                if (status.wasSuccessful)
                {
                    string prefix = null;
                    if (cIncrementalFormat.Checked)
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            List<int> locations = new List<int>();
                            foreach (ListViewItem item in lbSaveLocations.Items)
                            {
                                Object loc = item.Tag;
                                locations.Add(Convert.ToInt32(((firedumpdbDataSet.backup_locationsRow)loc).id));
                            }
                            BinlogDumpCredentialsConfig config = new BinlogDumpCredentialsConfig();
                            config.host = (string)serverData.Rows[cmbServers.SelectedIndex]["host"];
                            config.port = unchecked((int)(long)serverData.Rows[cmbServers.SelectedIndex]["port"]);
                            config.username = (string)serverData.Rows[cmbServers.SelectedIndex]["username"];
                            config.password = (string)serverData.Rows[cmbServers.SelectedIndex]["password"];

                            List<string> databases = new List<string>();
                            List<string> excludedTables = new List<string>();
                            tableList = new List<string>();
                            foreach (TreeNode node in tvDatabases.Nodes)
                            {
                                if (node.Checked)
                                {
                                    databases.Add(node.Text);
                                    string tables = "";
                                    foreach (TreeNode childNode in node.Nodes)
                                    {
                                        if (!childNode.Checked)
                                        {
                                            tables += childNode.Text + ",";
                                        }
                                        else
                                        {
                                            tableList.Add(childNode.Text);
                                        }
                                    }
                                    if (tables != "")
                                    {
                                        tables = tables.Substring(0, tables.Length - 1); //vgazei to teleutaio comma
                                    }
                                    excludedTables.Add(tables);
                                }
                            }
                            config.database = databases[0];
                            config.locationIds = locations.ToArray();
                            IncrementalUtils iutils = new IncrementalUtils(config);
                            prefix = iutils.calculatePrefix(0)[1];
                        });
                    }

                    saveToLocations(status.fileAbsPath, prefix);
                }
                else
                {
                    this.UseWaitCursor = false;
                    string errorMessage = "";
                    switch (status.errorNumber)
                    {
                        case -1:
                            errorMessage = "Connection credentials not set correctly:\n"+status.errorMessage;
                            Console.WriteLine(errorMessage);
                            break;
                        case -2:
                            errorMessage = "MySQL dump failed:\n" + status.mysqldumpexeStandardError;
                            Console.WriteLine(errorMessage);
                            break;
                        case -3:
                            errorMessage = "Compression failed:\n" + status.mysqldumpexeStandardError;
                            Console.WriteLine(errorMessage);
                            break;
                        default:
                            break;
                    }
                    MessageBox.Show(errorMessage, "Dump failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                    bStartDump.Invoke((MethodInvoker)delegate ()
                    {
                        bStartDump.Enabled = true;
                    });
                }

                //kiala pramata na kanei edo, afta pou meleges
                //
                //gia ui components xriazete Invoke
                
            } else
            {
                this.UseWaitCursor = false;
            }
        }

        private void onCompletedHandlerBinlog(BinlogDumpResultset status)
        {
            if (status != null)
            {

                lStatus.Invoke((MethodInvoker)delegate ()
                {
                    lStatus.Text = "Completed";
                });

                pbDumpExec.Invoke((MethodInvoker)delegate ()
                {
                    pbDumpExec.Value = pbDumpExec.Maximum;
                });

                if (status.wasSuccessful)
                {
                    saveToLocations(status.fileAbsPath, status.incrementalFormatPrefix);
                }
                else
                {
                    this.UseWaitCursor = false;
                    string errorMessage = "";
                    switch (status.errorNumber)
                    {
                        case -1:
                            errorMessage = "Connection credentials not set correctly:\n" + status.errorMessage;
                            Console.WriteLine(errorMessage);
                            break;
                        case -2:
                            errorMessage = "Binlog dump failed:\n" + status.mysqlbinlogexeStandardError;
                            Console.WriteLine(errorMessage);
                            break;
                        case -3:
                            errorMessage = "Compression failed:\n" + status.mysqlbinlogexeStandardError;
                            Console.WriteLine(errorMessage);
                            break;
                        default:
                            break;
                    }
                    MessageBox.Show(errorMessage, "Dump failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    bStartDump.Invoke((MethodInvoker)delegate ()
                    {
                        bStartDump.Enabled = true;
                    });
                }
            }
            else
            {
                this.UseWaitCursor = false;
            }
        }

        private void saveToLocations(string fileAbsPath, string prefix)
        {
            //EDW KALEITAI TO SAVE STA LOCATIONS
            List<int> locations = new List<int>();
            backuplocations = new List<firedumpdbDataSet.backup_locationsRow>();
            dataGridView1.Invoke((MethodInvoker)delegate ()
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
            });

            this.Invoke((MethodInvoker)delegate ()
            {
                progressContainer = new ProgressFormContainer();
                //progressContainer.Show();
                foreach (ListViewItem item in lbSaveLocations.Items)
                {
                    Object loc = item.Tag;
                    locations.Add(Convert.ToInt32(((firedumpdbDataSet.backup_locationsRow)loc).id));
                    backuplocations.Add((firedumpdbDataSet.backup_locationsRow)loc);
                    addToGridView(loc);
                }

            });


            adapterLocation = new LocationAdapterManager(locations, fileAbsPath, prefix); //fix auto na mpei to prefix
            adapterLocation.SaveInit += onSaveInitHandler;
            adapterLocation.InnerSaveInit += onInnerSaveInitHandler;
            adapterLocation.LocationProgress += onLocationProgressHandler;
            adapterLocation.SaveProgress += setSaveProgressHandler;
            adapterLocation.SaveComplete += onSaveCompleteHandler;
            adapterLocation.SaveError += onSaveErrorHandler;
            adapterLocation.setProgress();

            adapterLocation.startSave();
        }


        private void onTableDumpStartHandler(string table)
        {
            lStatus.Invoke((MethodInvoker)delegate () {
                lStatus.Text = "dumping table " + table;
            });

            increaseProgressBarStep();
        }

        private void initDumpTablesHandler(List<string> tables)
        {
            initProgressBar(tables,0);
        }

        private void tableRowCountHandler(int rowcount)
        {
            ltable.Invoke((MethodInvoker)delegate () {
                if(rowcount == -1)
                {
                    ltable.Text = "";
                } else
                {
                    ltable.Text = "Table rows:"+rowcount;
                }
            });
        }

        private void compressProgressHandler(int progress)
        {
            setProgressValue(progress);
        }

        private void onCompressStartHandler()
        {
            lStatus.Invoke((MethodInvoker)delegate () {
                lStatus.Text = "Compressing...";
                initProgressBar(null,100);
                tableRowCountHandler(-1);
            });
        }

        private void cbShowSysDB_CheckedChanged(object sender, EventArgs e)
        {
            hideSystemDatabases = !cbShowSysDB.Checked;
            cmbServers_SelectionChangeCommitted(null, null); //ksanakalei to fillTreeView
        }

        private void bDeleteSaveLocation_Click(object sender, EventArgs e)
        {
            if (lbSaveLocations.Items.Count == 0 || lbSaveLocations.SelectedItems.Count == 0) 
            {
                return;
            }
            foreach(ListViewItem selecteditem in lbSaveLocations.SelectedItems)
            {
                lbSaveLocations.Items.Remove(selecteditem);
            }
        }

        public void deleteSaveLocation(BackupLocation loc)
        {
            ListViewItem item = findItemSaveLoc(loc);
            if(lbSaveLocations.Items.Contains(item))
                lbSaveLocations.Items.Remove(item);

        }

        private ListViewItem findItemSaveLoc(BackupLocation loc)
        {
            ListViewItem item = new ListViewItem();
            int i = 0;
            bool foundflag = false;
            
          
            while (!foundflag && i < lbSaveLocations.Items.Count)
            {
                firedumpdbDataSet.backup_locationsRow tag = (firedumpdbDataSet.backup_locationsRow)lbSaveLocations.Items[i].Tag;
                tag.BeginEdit();
                if (tag.id == loc.id)
                {
                    item = lbSaveLocations.Items[i];
                    foundflag = true;
                }
                i++;
                tag.CancelEdit();
            }
            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="progress">Progress int 1-100</param>
        /// <param name="speed">Speed in B/s -1 to ignore</param>
        private void setSaveProgressHandler(int progress, int speed)
        {
            setProgressValue(progress);
            //Console.WriteLine(speed);
            if(speed == -1) { return; }
            //Console.WriteLine(speed);
            string speedlabelext = "B/s";
            double tspeed = 0;
            if(speed <= 1050)
            {
                speedlabelext = "B/s";
                tspeed = speed;
            }
            else if (speed <= 1050000)
            {
                speedlabelext = "kB/s";
                tspeed = speed / 1000;
            }
            else
            {
                speedlabelext = "mB/s";
                tspeed = speed / 1000000;
            }
            string printedspeed = "";
            if (tspeed < 10)
            {
                //kanei format to double se ena dekadiko psifio se morfi string alliws den ginete stin c#
                printedspeed = string.Format("{0:0.0}", tspeed);
            }
            else
            {
                printedspeed = Convert.ToInt32(tspeed).ToString();
            }
            ltable.Invoke((MethodInvoker)delegate () {                          
                ltable.Text = printedspeed+" "+speedlabelext;
            });

            
        }

       

        private void onSaveInitHandler(int maxprogress)
        {
            lStatus?.Invoke((MethodInvoker)delegate () {
                lStatus.Text = "Saving to locations...";
                initProgressBar(null, maxprogress);
                tableRowCountHandler(-1);
            });
        }

        private void onSaveCompleteHandler(List<LocationResultSet> results)
        {

            pbDumpExec.Invoke((MethodInvoker)delegate ()
            {
                pbDumpExec.Value = pbDumpExec.Maximum;
            });

            this.UseWaitCursor = false;
            lStatus?.Invoke((MethodInvoker)delegate () {
                lStatus.Text = "Save complete!";
                tableRowCountHandler(-1);
            });
            string errorsToOutput = "";
            bool koble = true;
            int errorcounter = 0;
            foreach (LocationResultSet result in results)
            {
                if (!result.wasSuccessful)
                {
                    errorcounter++;
                    koble = false;
                    errorsToOutput += result.errorMessage + "\n";
                }
            }

            bStartDump?.Invoke((MethodInvoker)delegate ()
            {
                bStartDump.Enabled = true;
            });
            backuplocations = new List<firedumpdbDataSet.backup_locationsRow>();

            firedumpdbDataSetTableAdapters.logsTableAdapter logAdapter = new firedumpdbDataSetTableAdapters.logsTableAdapter();

            if (koble)
            {
                logAdapter.Insert(0,1, "Dump was completed successfully."+results.ToString(),DateTime.Now,0);
                MessageBox.Show("Dump was completed successfully.", "MySQL Dump", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                logAdapter.Insert(0, 1, "Saving to " + errorcounter + " out of " + results.Count + " save location(s) failed:\n" + errorsToOutput, DateTime.Now, 1);
                MessageBox.Show("Saving to "+errorcounter+" out of "+results.Count+" save location(s) failed:\n"+errorsToOutput, "MySQL Dump", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            dataGridView1.Invoke((MethodInvoker)delegate ()
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
            });

        }

        private void onSaveErrorHandler(string message)
        {
            this.UseWaitCursor = false;
            MessageBox.Show("Save to locations failed:\n"+message,"Locations Save",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        private void onInnerSaveInitHandler(string location_name, int location_type)
        {
            string location = "";
            switch (location_type)
            {
                case 0: //file system
                    location = "FileSystem";
                    break;
                case 1: //ftp
                    location = "FTP";
                    break;
                case 2: //dropbox
                    location = "Dropbox";
                    break;
                case 3: //google drive
                    location = "Google Drive";
                    break;
                default:
                    break;
            }
            this.locationName = location_name;
            if (location_name.Length>20) //ama einai poli megalo to name to kovei
            {
                location_name = location_name.Substring(0, 17) + "...";
            }
            lStatus.Invoke((MethodInvoker)delegate () {
                lStatus.Text = "Saving to: "+location_name + " ("+location+")";
            });
        }
        

        private void onLocationProgressHandler(int progress,int speed)
        {
            updateGridView(progress);
        }

        //-----------------------------------------------------------------------
        //------------END INTERFACE METHODS--------------------------------------
        //

        private void importSQLFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportSQL importinstance = new ImportSQL();
            importinstance.Show();
        }


        /// <summary>
        /// gets called for every cell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //set cancel on buttons
            dataGridView1.Rows[e.RowIndex].Cells[2].Value = "Cancel";
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //click on the cancel button
            if(e.ColumnIndex == 2)
            {
                
                if ((string)dataGridView1.Rows[e.RowIndex].Cells[1].Value != "Cancelled")
                {
                    firedumpdbDataSet.backup_locationsRow row = (firedumpdbDataSet.backup_locationsRow)dataGridView1.Rows[e.RowIndex].Tag;
                    if (row != null)
                    {
                        if (!adapterLocation.isLocationFinished(row))
                        {
                            adapterLocation.cancelSaveLocation(row);
                            dataGridView1.Rows[e.RowIndex].Cells[1].Value = "Cancelled";
                        }
                    }
                    dataGridView1.Rows[e.RowIndex].Cells[1].Value = "Cancelled";

                }
                
            }
        }


        private void addToGridView(object tag)
        {           
            if (progressContainer != null && progressContainer.Visible)
            {
                progressContainer.addProgress(tag);
            }
           
            /*
            DataGridViewRow dataRow = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            dataRow.Tag = tag;
            firedumpdbDataSet.backup_locationsRow row = (firedumpdbDataSet.backup_locationsRow)tag;
            dataRow.Cells[0].Value = row.name;
            dataGridView1.Invoke((MethodInvoker)delegate ()
            {
                dataGridView1.Rows.Add(dataRow);
            });
            */

        }

        private void updateGridView(int progress)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                if (progressContainer != null && progressContainer.Visible)
                {
                    progressContainer.updateProgress(progress, locationName);
                }
            });
            
            /*
            for(int i =0; i < dataGridView1.RowCount; i++)
            {
                firedumpdbDataSet.backup_locationsRow row = (firedumpdbDataSet.backup_locationsRow) dataGridView1.Rows[i].Tag;
                if(row.name == locationName)
                {
                    dataGridView1.Invoke((MethodInvoker)delegate ()
                    {
                        dataGridView1.Rows[i].Cells[1].Value = progress+"% ";
                    });                    
                    break;
                }
            }
            */
        }

        private void schedulerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SchedulerForm schedulerForm = new SchedulerForm();
            schedulerForm.Show();
        }

        private void show_logs_click(object sender, EventArgs e)
        {
            Logs logs = new Logs();
            logs.Show();
        }

        private void about_form_click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.Show();
        }

        private void emailsetupformclick(object sender, EventArgs e)
        {
            EmailSchedule emailform = new EmailSchedule();
            emailform.Show();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProgressFormContainer form = new ProgressFormContainer();
            form.Show();
        }

        private void bshowuploads_Click(object sender, EventArgs e)
        {
            if(progressContainer != null && !progressContainer.IsDisposed)
            {
                progressContainer.Visible = true;
                progressContainer.Show();
            }
        }

        private void cIncrementalFormat_CheckedChanged(object sender, EventArgs e)
        {
            rbFull.Enabled = cIncrementalFormat.Checked;
            rbInc.Enabled = cIncrementalFormat.Checked;
            rbIncDelta.Enabled = cIncrementalFormat.Checked;
        }
    }
}
