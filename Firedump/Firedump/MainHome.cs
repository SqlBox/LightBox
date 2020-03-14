using Firedump.core.db;
using Firedump.core.sql;
using Firedump.Forms.mysql;
using Firedump.Forms.mysql.connect;
using Firedump.models.events;
using Firedump.usercontrols;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;

namespace Firedump
{
    public sealed partial class MainHome : Form , IParentRef
    {
        private DbConnection con;
        private sqlservers server;
        private bool showSystemDatabases = false;

        public List<UserControlReference> ChildControls;

        public MainHome()
        {
            InitializeComponent();
            Text = "LightHouse Editor";
            this.InitChildControls();
            this.InitControlEvents();
            this.InitHomeEvents();
            this.openDatabaseWindow();
        }

        private void InitChildControls()
        {
            ChildControls = new List<UserControlReference>();
            ChildControls.AddRange(new UserControlReference[] { editor1, tableView1, tabView1 });
            foreach (UserControlReference uc in ChildControls)
            {
                uc.InitComponent(this);
                if(uc is Editor)
                {
                    ((Editor)uc).StatementExecuted += OnStatementExecuted;
                    ((Editor)uc).DisableUi = DisableUI;
                }
            }
        }


        private void InitHomeEvents()
        {
            this.FormClosed += (sender,e) =>
            {
                // Disconnect/close connection when app closes
                try
                {
                    if (this.con != null)
                        if (isConnected(con))
                            DB.Rollback(con);
                    this.con.Close();
                }
                catch (Exception ex) { }
            };
            this.FormClosing += (sender, e) =>
            {
                // Inform user for lost un committed data if app closes
                // To improve this we can ask "Would you like to commit and close?" and we do the work(commit) and close
                if (this.isConnected(this.con) && e.CloseReason == CloseReason.UserClosing)
                {
                    e.Cancel = MessageBox.Show(@"Close App? Any UnCommitted changes will be lost!",
                                       Application.ProductName,
                                       MessageBoxButtons.YesNo) == DialogResult.No;
                };
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
                    SetAutoCommit(con);
                    this.pushConnection();
                    this.setHomeConnectionStatus();
                });
                uc.Send += new EventHandler<object>((sender,e) =>
                    this.DispatchEvent((ITriplet<UserControlReference, UserControlReference, object>)
                    ((IGenericEventArgs<ITriplet<UserControlReference, UserControlReference, object>>)e).GetObject())
                );
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
            SetAutoCommit(con);
            this.server = e.server;
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
            this.connectionStatusStripTextbox.Text = this.server.username+"@"+ this.server.host + ":" + this.server.port+":"+this.con.Database;
        }


        private void setConnectionAndServerToUserControls()
        {
            this.pushConnection();
            this.tabView1.setServerDataToComboBox(new SqlBuilderFactory(GetServer())
                .Create(null).removeSystemDatabases(DbUtils.getDatabases(this.server, this.con), this.showSystemDatabases));
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
            if(this.isConnected(this.con))
            {
                this.tabView1.setServerDataToComboBox(new SqlBuilderFactory(GetServer())
                .Create(null).removeSystemDatabases(DbUtils.getDatabases(this.server, this.con), this.showSystemDatabases = !this.showSystemDatabases));
            }
        }


        private void EnableDisable(bool enable)
        {
            if(enable && (isConnected(con)))
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
            if(isConnected(con))
            {
                DbSessionSettings.SetAutoCommit(con,false);
            }
        }

        private bool isConnected(DbConnection c)
        {
            return  c != null && c.State == ConnectionState.Open;
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


        private void DispatchEvent(ITriplet<UserControlReference, UserControlReference, object> triplet)
        {
            foreach (UserControlReference c in ChildControls)
            {
                if (c.GetType() == triplet.TargetType())
                {                 
                    c.dataReceived(triplet);
                    break;
                }
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
