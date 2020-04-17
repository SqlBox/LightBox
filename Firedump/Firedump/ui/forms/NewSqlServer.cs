using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firedump.core.db;
using Firedump.core.models;
using Firedump.ui.forms;
using sqlbox.commons;

namespace Firedump.Forms.mysql
{
    public partial class NewSqlServer : BaseForm
    {
        public delegate void reloadserverdata(int id);
        public event reloadserverdata ReloadServerData;
        private void onReloadServerData(int id)
        {
            ReloadServerData?.Invoke(id);
        }

        private bool isUpdate = false;
        private firedumpdbDataSet.sql_serversRow mysqlserver; 

        public NewSqlServer()
        {
            InitializeComponent();
            initDbTypeCombobox();
            ShowPathForEmbeddedDb(false);
            this.groupBoxDetails.Enabled = false;
        }

        private void initDbTypeCombobox()
        {
            this.comboBoxDbTypes.SuspendLayout();
            for(int i =0; i < 9; i++)
            {
                this.comboBoxDbTypes.Items.Add(new ToolStripItemDbType(i));
            }
            this.comboBoxDbTypes.ResumeLayout();
        }

        public NewSqlServer(firedumpdbDataSet.sql_serversRow server) : this()
        {
            bSave.Text = "Update";
            this.isUpdate = true;
            this.mysqlserver = server;
            tbName.Text = server.name;
            tbHost.Text = server.host;
            if (server.port != 0)
                tbPort.Text = server.port.ToString();
            tbUsername.Text = server.username;
            tbPassword.Text = !string.IsNullOrEmpty(server.password) ? EncryptionUtils.sDecrypt(server.password) : null;
            tbDatabase.Text = server.database;
            checkkDbType(server.db_type);
            setSelectedItem(server.db_type);
            DbType type = Firedump.core.sql.Utils._convert(server.db_type);
            if(Firedump.core.sql.Utils.IsDbEmbedded(type) && string.IsNullOrEmpty(textBoxPath.Text))
            {
                this.textBoxPath.Text = server.path;
            }
            this.groupBoxDetails.Enabled = true;
            this.comboBoxDbTypes.Enabled = false;
        }

        private void setSelectedItem(int db_type)
        {
            foreach (var item in this.comboBoxDbTypes.Items)
            {
                if (((ToolStripItemDbType)item).db_type == db_type)
                {
                    this.comboBoxDbTypes.SelectedItem = item;
                    break;
                }
            }
        }

        private void checkkDbType(int db_type)
        {
            if (Firedump.core.sql.Utils.IsDbEmbedded(db_type))
            {
                this.tbHost.Enabled = false;
                this.tbDatabase.Enabled = false;
                this.tbUsername.Enabled = false;
                this.tbPort.Enabled = false;
                ShowPathForEmbeddedDb(true);
            }
            else
            {
                this.tbHost.Enabled = true;
                this.tbDatabase.Enabled = true;
                this.tbUsername.Enabled = true;
                this.tbPort.Enabled = true;
                ShowPathForEmbeddedDb(false);
            }
        }

        private void ShowPathForEmbeddedDb(bool hide)
        {
            this.buttonChooseDb.Visible = hide;
            this.textBoxPath.Visible = hide;
        }

        private void bTestConnection_Click(object sender, EventArgs e)
        {
            if (!performChecks())
            {
                return;
            }
            this.UseWaitCursor = true;
            //test connection
            ConnectionResultSet result = DB.TestConnection(createMySqlServerInfoCreds(), tbDatabase.Text);
            if (result.wasSuccessful)
            {
                this.UseWaitCursor = false;
                MessageBox.Show("Connection Successful", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                this.UseWaitCursor = false;
                MessageBox.Show("Connection failed: \n"+result.errorMessage, "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private sqlservers createMySqlServerInfoCreds()
        {
            sqlservers s = new sqlservers();
            if (!string.IsNullOrEmpty(tbPort.Text))
                s.port = int.Parse(tbPort.Text);
            else
                s.port = 0;

            if (string.IsNullOrEmpty(tbHost.Text))
                s.host = "localhost";
            else
                s.host = tbHost.Text;

            if (string.IsNullOrEmpty(tbUsername.Text))
                s.username = "user";
            else
                s.username = tbUsername.Text;

            s.password = tbPassword.Text;
            s.db_type = ((ToolStripItemDbType)this.comboBoxDbTypes.SelectedItem).db_type;
            s.path = textBoxPath.Text;
            return s;
        }

        private Boolean performChecks()
        {
            if (this.comboBoxDbTypes.SelectedItem == null)
            {
                MessageBox.Show("Select the database type from the top combobox", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //for extra checks if db is sqlite or vistadb to be path
            DbType type = Firedump.core.sql.Utils._convert(((ToolStripItemDbType)this.comboBoxDbTypes.SelectedItem).db_type);
            //port
            if (!string.IsNullOrEmpty(tbPort.Text))
            {
                try
                {
                    int port = int.Parse(tbPort.Text);
                    if (port < 1 || port > 65535)
                    {
                        MessageBox.Show(tbPort.Text + " is not a valid port number", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    //con.port = port;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(tbPort.Text + " is not a valid port number", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            } else
            {
                if (!Firedump.core.sql.Utils.IsDbEmbedded(type))
                {
                    MessageBox.Show("Must declare Port number", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            //host
            if (!string.IsNullOrEmpty(tbHost.Text))
            {
                //con.Host = tbHost.Text;
            }
            else
            {
                if(!Firedump.core.sql.Utils.IsDbEmbedded(type))
                {
                    MessageBox.Show("Hostname is empty.", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                } 
            }
            //username
            if (!string.IsNullOrEmpty(tbUsername.Text))
            {
                //con.username = tbUsername.Text;
            }
            else
            {
                if (!Firedump.core.sql.Utils.IsDbEmbedded(type))
                {
                    MessageBox.Show("Username is empty.", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            //password
            if (!string.IsNullOrEmpty(tbPassword.Text))
            {
                //con.password = tbPassword.Text;
            }
            //database
            if (!string.IsNullOrEmpty(tbDatabase.Text))
            {
                //con.database = tbDatabase.Text;
            }

            if (Firedump.core.sql.Utils.IsDbEmbedded(type) && string.IsNullOrEmpty(textBoxPath.Text))
            {
                MessageBox.Show("In Case of SQLITE or VistaDb the db file path must be defined", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        

        private void bSave_Click(object sender, EventArgs e)
        {
            firedumpdbDataSetTableAdapters.sql_serversTableAdapter adapter = new firedumpdbDataSetTableAdapters.sql_serversTableAdapter();
            if (string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("Type a name for the new server", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if ((Int64)adapter.numberOfOccurances(tbName.Text) == 0 || isUpdate)
            {
                if(!performChecks())
                {
                    return;
                }
                sqlservers server = createMySqlServerInfoCreds();
                String passwd = EncryptionUtils.sEncrypt(server.password);
                string path = null;
                DbType type = Firedump.core.sql.Utils._convert(((ToolStripItemDbType)this.comboBoxDbTypes.SelectedItem).db_type);
                if (Firedump.core.sql.Utils.IsDbEmbedded(type))
                {
                    path = textBoxPath.Text;
                }
                if (isUpdate)
                    adapter.UpdateMySqlServerById(tbName.Text, server.port, server.host, server.username, passwd, tbDatabase.Text, mysqlserver.id);
                else
                    adapter.InsertQuery(tbName.Text, server.port, server.host, server.username, passwd, tbDatabase.Text,(int)type,path); 
                int id = Convert.ToInt32((Int64)adapter.GetIdByName(tbName.Text));
                onReloadServerData(id);
                this.Close();
                return;
            }


            MessageBox.Show("Name "+tbName.Text+ " already exists", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            tbPassword.Text = "";
            tbName.Text = "";
            isUpdate = false;
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxPath_Click(object sender, EventArgs e)
        {
            if(this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxPath.Text = openFileDialog1.FileName;
            }
        }


        private void comboBoxDbTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.groupBoxDetails.Enabled = true;
            this.checkkDbType(((ToolStripItemDbType)this.comboBoxDbTypes.SelectedItem).db_type);
        }
    }
}
