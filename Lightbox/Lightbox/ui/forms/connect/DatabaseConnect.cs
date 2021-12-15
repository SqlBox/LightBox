using Lightbox.core;
using Lightbox.core.db;
using Lightbox.models.events;
using Lightbox.ui.forms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lightbox.Forms.mysql.connect
{
    public partial class DatabaseConnect : Form
    {
        private LightboxdbDataSet.sql_serversDataTable serverData;
        private LightboxdbDataSetTableAdapters.sql_serversTableAdapter mysql_serversAdapter;

        public event EventHandler<ConnectionEventArgs> onConnect;
        void OnConnectionChanged(object t, ConnectionEventArgs e)
        {
            onConnect?.Invoke(t, e);
        }

        public DatabaseConnect()
        {
            InitializeComponent();
            FormUtils.setFormIcon(this);
            this.InitDataFormData(this, null);
        }

        private void InitDataFormData(object sender, EventArgs e)
        {
            mysql_serversAdapter = new LightboxdbDataSetTableAdapters.sql_serversTableAdapter();
            serverData = new LightboxdbDataSet.sql_serversDataTable();
            mysql_serversAdapter.Fill(serverData);
            comboBox1.DataSource = serverData;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            if (serverData.Count > 0)
            {
                var server = sqlservers.CreateSqlServerFromDataTable(serverData, comboBox1);
                if (server != null)
                {
                    server.password = textBoxPassword.Text;
                    ConnectionResultSet result = DB.TestConnection(server);
                    if (result.wasSuccessful)
                    {
                        var con = DB.connect(server);
                        this.OnConnectionChanged(this, new ConnectionEventArgs(con, server));
                        Close();
                    }
                    else
                    {
                        this.UseWaitCursor = false;
                        MessageBox.Show("Connection failed: \n" + result.errorMessage, "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                WarnNoServersSaved();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (serverData.Count > 0)
            {
                var server = sqlservers.CreateSqlServerFromDataTable(serverData, comboBox1);
                if (server != null)
                {
                    server.password = textBoxPassword.Text;
                    //test connection
                    ConnectionResultSet result = DB.TestConnection(server);
                    if (result.wasSuccessful)
                    {
                        this.UseWaitCursor = false;
                        MessageBox.Show("Connection Successful", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        this.UseWaitCursor = false;
                        MessageBox.Show("Connection failed: \n" + result.errorMessage, "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                WarnNoServersSaved();
            }
        }

        private void WarnNoServersSaved()
        {
            MessageBox.Show("No saved servers. Go to Database->Add New Server", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (serverData.Count > 0)
            {
                if (comboBox1.SelectedItem != null)
                {
                    var server = sqlservers.CreateSqlServerFromDataTable(serverData, comboBox1);
                    if (server != null)
                    {
                        textBoxPassword.Text = server.password;
                    }
                }
            }
        }
    }
}
