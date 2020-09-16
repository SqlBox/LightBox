using com.protectsoft.SqlStatementParser;
using Firedump.core.db;
using Firedump.core.sql;
using Firedump.Forms.mysql;
using Firedump.Forms.mysql.connect;
using Firedump.models.events;
using Firedump.Properties;
using Firedump.ui.forms;
using Firedump.usercontrols;
using Microsoft.WindowsAPICodePack.Shell.Interop;
using sqlbox.commons;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Firedump
{
    public sealed partial class MainHome : Form, IConnectionServerRef
    {
        private DbConnection con;
        private sqlservers server;
        private bool showSystemDatabases;
        public readonly List<UserControlReference> ChildControls = new List<UserControlReference>();

        public MainHome()
        {
            InitializeComponent();
            FormUtils.setFormIcon(this);
            this.SuspendLayout();
            this.InitMainMenuComponents();
            this.InitChildControls();
            this.InitControlEvents();
            this.InitHomeEvents();
            this.ResumeLayout();
            this.openDatabaseWindow();
        }

        private void InitChildControls()
        {
            this.InitEditorComponent();
            ChildControls.AddRange(new UserControlReference[] { editor1, tableView1, tabView1 });
            foreach (UserControlReference uc in ChildControls)
            {
                uc.InitComponent(this);
                if(uc is Editor)
                {
                    ((Editor)uc).StatementExecuted += OnStatementExecuted;
                    ((Editor)uc).EnableUi = EnableUi;
                }
            }
        }

        private void MainHomeLoad(object sender, EventArgs e)
        {
            // Set window location
            if (Settings.Default.WindowLocation != null)
            {
                this.Location = Settings.Default.WindowLocation;
            }
            // Set window size
            if (Settings.Default.WindowSize != null)
            {
                this.Size = Settings.Default.WindowSize;
            }
        }


        private void InitHomeEvents()
        {
            this.FormClosed += (sender,e) =>
            {
                // Disconnect/close connection when app closes
                try
                {
                    if(this.con != null)
                    {
                        if (DB.IsConnected(con))
                        {
                            DB.Rollback(con);
                        }
                        this.con.Close();
                    }
                }
                catch (Exception ex) { }
            };
            this.FormClosing += (sender, e) =>
            {
                // Inform user for lost un committed data if app closes
                // To improve this we can ask "Would you like to commit and close?" and we do the work(commit) and close
                if (DB.IsConnected(this.con) && e.CloseReason == CloseReason.UserClosing)
                {
                    e.Cancel = MessageBox.Show(@"Close App? Any UnCommitted changes will be lost!",
                                       Application.ProductName,
                                       MessageBoxButtons.YesNo) == DialogResult.No;
                };
                //save window size and location
                // Copy window location to app settings
                Settings.Default.WindowLocation = this.Location; 
                // Copy window size to app settings
                if (this.WindowState == FormWindowState.Normal)
                {
                    Settings.Default.WindowSize = this.Size;
                    if(Settings.Default.WindowSize.Width < 250 || Settings.Default.WindowSize.Height < 250)
                    {
                        Settings.Default.WindowSize = new Size(1100,800);
                    }
                }
                else
                {
                    Settings.Default.WindowSize = this.RestoreBounds.Size;
                }
                // Save settings
                Settings.Default.Save();
            };
            this.ResizeBegin += (sender, e) => this.SuspendLayout();
            this.ResizeEnd += (sender, e) => this.ResumeLayout();
        }


        private void InitControlEvents()
        {
            foreach(UserControlReference uc in ChildControls)
            {
                uc.Disconnected += new EventHandler(onDisconnected);
                uc.ConnectionChanged += new EventHandler<ConChangedEventArgs>((sender,e) => {
                    this.con = e.con;
                    //SetAutoCommit(con);
                    this.pushConnection();
                    this.setHomeConnectionStatus();
                });
                uc.Reconnect += (sender, e) => Reconnect(); 
            }
        }

        private void Reconnect()
        {
            if(server != null && con != null)
            {
                con.Close();
                ConnectionResultSet result = DB.TestConnection(server);
                if (result.wasSuccessful)
                {
                    var con = DB.connect(server,this.con.Database);
                    this.SetReconnectionStatus(con);
                }
            }
        }

        internal void AbandonAndOpenNewConnection()
        {
            if(server != null && this.con != null)
            {
                string database = this.con.Database;
                this.con = null;
                if (DB.TestConnection(server).wasSuccessful)
                {
                    this.SetReconnectionStatus(DB.connect(server, database));
                }
            }
        }

        private void DataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Home().Show();
        }


        private void ManageServersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Home().ShowDialog();
        }

        private void AddNewServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewSqlServer newMysqlServer = new NewSqlServer();
            newMysqlServer.ReloadServerData += (id) => { };
            newMysqlServer.ShowDialog();
        }

        private void ConnectToDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openDatabaseWindow();
        }

        private void ConnectToDbClick(object sender, EventArgs e)
        {
            if(this.server != null)
            {
                this.con = DB.connect(this.server);
                SetAutoCommit(this.con);
                this.setHomeConnectionStatus();
                this.setConnectionAndServerToUserControls();
                this.EnableDisable(true);
            } else
            {
                openDatabaseWindow();
            }
        }

        private void openDatabaseWindow()
        {
            var databaseConnector = new DatabaseConnect();
            databaseConnector.TopMost = true;
            databaseConnector.Show();
            this.EnableDisable(false);
            databaseConnector.onConnect += (sender, e) => SetInitialConnectionStatus(e);
            databaseConnector.FormClosed += (sender,e) => this.EnableDisable(true);
        }

        private void SetInitialConnectionStatus(ConnectionEventArgs e)
        {
            this.con = e.con;
            this.server = e.server;
            SetAutoCommit(con);
            this.setConnectionAndServerToUserControls();
            this.setHomeConnectionStatus();
        }

        private void SetReconnectionStatus(DbConnection con)
        {
            this.con = con;
            SetAutoCommit(this.con);
            this.setHomeConnectionStatus();
        }


        private void setHomeConnectionStatus()
        {
            if(!core.sql.Utils.IsDbEmbedded(this.server.db_type))
            {
                this.connectionStatusStripTextbox.Text = this.server.username + "@" + this.server.host + ":" + this.server.port + ":" + this.con.Database;
            }
            else
            {
                this.connectionStatusStripTextbox.Text = this.server.path;
            }
        }


        private void setConnectionAndServerToUserControls()
        {
            this.pushConnection();
            if(core.sql.Utils._convert(GetServer().db_type) == sqlbox.commons.DbType.SQLITE)
            {
                this.tabView1.setServerDataToComboBox(new List<string>() {"main"});
            } else
            {
                this.tabView1.setServerDataToComboBox(new SqlBuilderFactory(GetServer())
                .Create(null).removeSystemDatabases(DbDataHelper.getDatabases(this.server, this.con), this.showSystemDatabases));
            }
            
        }

        private void pushConnection()
        {
            // notify.
            foreach (UserControlReference f in ChildControls)
            {
                f.onConnected();
            }
        }

        // Called/Event fired from child/composit components/userControls when mysqlconnection is disconnected and after failed reconnect try.
        private void onDisconnected(object sender, EventArgs e)
        {
            this.con = null;
            foreach (UserControlReference f in ChildControls)
            {
                //also inform all the other child components to update the ui accordingly for offline/disconnected mode
                f.onDisconnect();
            }
            // change mainhome/parent ui according to offline/disconnected status here
        }


        private void ShowHideSystemDbEventClick(object sender, EventArgs e)
        {
            if(DB.IsConnected(this.con) && !core.sql.Utils.IsDbEmbedded(GetServer().db_type))
            {
                this.tabView1.setServerDataToComboBox(new SqlBuilderFactory(GetServer())
                .Create(null).removeSystemDatabases(DbDataHelper.getDatabases(this.server, this.con), this.showSystemDatabases = !this.showSystemDatabases));
            }
        }


        private void EnableDisable(bool enable)
        {
            if(enable && (DB.IsConnected(con)))
            {
                setConstrolEnableStatus(enable);
            } else if(!enable)
            {
                setConstrolEnableStatus(enable);
            }
        }

        // Editor should always be enable.
        private void setConstrolEnableStatus(bool enable)
        {
            foreach (var comp in this.ChildControls)
            {
                if(!(comp is Editor))
                {
                    comp.Enabled = enable;
                } else if(comp is Editor)
                {
                    comp.Enabled = true;
                }
            }
        }

        private void SetAutoCommit(DbConnection con)
        {
            if(DB.IsConnected(con))
            {
                applySettingsAfterOpen();
            }
        }

        internal void applySettingsAfterOpen()
        {
            if (core.sql.Utils._convert(server.db_type) == sqlbox.commons.DbType.SQLITE)
            {
                if (Properties.Settings.Default.option_sqlite_begintransdbopen)
                {
                    //sqlite transaction is needed to make db updates/changes
                    DbDataHelper.executeNonQuery(con, "begin transaction");
                }
                if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.option_sqlite_sqlafteropen))
                {
                    DbDataHelper.executeNonQuery(con, Properties.Settings.Default.option_sqlite_sqlafteropen);
                }
            } else
            {
                DbDataHelper.executeNonQuery(con, "set autocommit=" + (Properties.Settings.Default.option_general_autocommit == true ? "1" : "0"));
            }
        }


        private void disconnectEventClick(object sender, EventArgs e)
        {
            if(this.con != null)
            {
                this.con.Close();
                onDisconnected(null,null);
                setConstrolEnableStatus(false);
            }
        }

        public DbConnection GetConnection()
        {
            return this.con;
        }

        public sqlservers GetServer()
        {
            return this.server;
        }

    }
}
