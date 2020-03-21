using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Firedump.core;
using Firedump.core.db;
using Firedump.core.sql;
using Firedump.models;
using Firedump.models.events;

namespace Firedump.usercontrols
{
    public sealed partial class TabView : UserControlReference
    {
        private class FieldImageIndex
        {
            public string Value;
            public int Index;
        }

        public TabView()
        {
            InitializeComponent();
        }

        public void setServerDataToComboBox(List<string> databases)
        {
            this.comboBoxServers.BeginUpdate();
            this.comboBoxServers.Items.Clear();
            this.comboBoxServers.Items.AddRange(databases.ToArray());
            if(this.comboBoxServers.Items.Count > 0)
            {
                this.comboBoxServers.SelectedItem = this.comboBoxServers.Items[0];
            }
            this.comboBoxServers.EndUpdate();
        }

        private void ComboBoxServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.changeDatabase(comboBoxServers.SelectedItem.ToString());
            this.initTabControl(comboBoxServers.SelectedItem.ToString());
        }


        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.initTabControl();
        }

        // On tab select set the data.
        private void initTabControl(string database = null)
        {
            if (database != null && DB.IsConnectedToDatabaseAndAfterReconnect(this))
            {
                base.changeDatabase(database);
            }
            if (DB.IsConnectedToDatabaseAndAfterReconnect(this))
            {
                switch(tabControl1.SelectedTab.Text)
                {
                    case "Tables":
                        this.setDatagridviewTables();
                        break;
                    case "Indexes":
                        this.setDataGridViewIndexes();
                        break;
                    case "Triggers":
                        this.setDataGridTriggers();
                        break;
                    case "Procedures":
                        this.setDataGridProcedures();
                        break;
                    case "PKs":
                        this.setDataGridViewPKs();
                        break;
                    case "UKs":
                        this.setDataGridUniques();
                        break;
                    case "FKs":
                        this.setDataGridFKs();
                        break;
                    case "Functions":
                        this.setDataGridFunctions();
                        break;
                    case "Views":
                        this.setDatagridViews();
                        break;
                }
            }
        }

        private void setRootTablesIntoTreeView(List<string> tables)
        {
            this.treeViewTables.BeginUpdate();
            treeViewTables.Nodes.Clear();
            foreach (string t in tables)
            {
                TreeNode node = new TreeNode(t) { ImageIndex = 0 };
                node.Nodes.Add(getDummy());
                treeViewTables.Nodes.Add(node);
            }
            this.treeViewTables.EndUpdate();
        }

        private void setTableFields(List<FieldImageIndex> fields, int nodeIndex)
        {
            this.treeViewTables.BeginUpdate();
            treeViewTables.Nodes[nodeIndex].Nodes.Clear();
            if (fields != null && fields.Count > 0)
            {
                foreach (FieldImageIndex f in fields)
                {
                    treeViewTables.Nodes[nodeIndex].Nodes.Add(new TreeNode(f.Value) { ImageIndex = f.Index, SelectedImageIndex = f.Index });
                }
            }
            else
            {
                treeViewTables.Nodes[nodeIndex].Nodes.Add(getDummy());
            }
            this.treeViewTables.EndUpdate();
        }


        private void TreeViewTables_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            List<string> fields = DbUtils.getTableFields(GetSqlConnection(), e.Node.Text);
            List<string> fieldsInfo = DbUtils.getTableInfo(GetSqlConnection(),e.Node.Text);
            List <FieldImageIndex> finalFieldList = new List<FieldImageIndex>();
            foreach(string f in fields)
            {
                finalFieldList.Add(new FieldImageIndex() { Value = f, Index = 1 });
            }
            foreach (string f in fieldsInfo)
            {
                finalFieldList.Add(new FieldImageIndex() { Value = f, Index = 2 });
            }
            this.setTableFields(finalFieldList, e.Node.Index);
        }

        private TreeNode getDummy() =>
            new TreeNode() { ImageIndex = 0 };

        private void setDatagridviewTables()
        {
            setRootTablesIntoTreeView(new SqlBuilderFactory(base.GetSqlConnection())
                        .Create(null).removeSystemDatabases(DbUtils.getTables(base.GetSqlConnection()), false));
        }
    

        private void setDataGridViewIndexes()
        {
            dataGridViewIndexes.DataSource = DbUtils.getDataTableData(GetSqlConnection(),
                new SqlBuilderFactory(GetServer()).Create(GetSqlConnection().Database).createDatabaseIndexes());
        }

        private void setDataGridViewPKs()
        {
            dataGridViewPKs.DataSource = DbUtils.getDataTableData(GetSqlConnection(),
               new SqlBuilderFactory(GetServer()).Create(GetSqlConnection().Database).getDatabasePrimaryKeys());
        }

        private void setDataGridUniques()
        {
            dataGridViewUnique.DataSource = DbUtils.getDataTableData(GetSqlConnection(),
               new SqlBuilderFactory(GetServer()).Create(GetSqlConnection().Database).getDatabaseUniques());
        }

        private void setDataGridFKs()
        {
            dataGridViewFKs.DataSource = DbUtils.getDataTableData(GetSqlConnection(),
               new SqlBuilderFactory(GetServer()).Create(GetSqlConnection().Database).getDatabaseForeignKeys());
        }

        private void setDataGridTriggers()
        {
            dataGridViewTrigger.DataSource = DbUtils.getDataTableData(GetSqlConnection(), "show triggers");
        }

        private void setDataGridProcedures()
        {
            dataGridViewProcedures.DataSource = DbUtils.getDataTableData(GetSqlConnection(), "SHOW PROCEDURE STATUS WHERE Db = '"+ GetSqlConnection().Database+"'");
        }

        private void setDataGridFunctions()
        {
            dataGridViewFunctions.DataSource = DbUtils.getDataTableData(GetSqlConnection(), "SHOW FUNCTION STATUS WHERE Db = '" + GetSqlConnection().Database + "'");
        }

        private void setDatagridViews()
        {
            dataGridViewView.DataSource = DbUtils.getDataTableData(GetSqlConnection(), "SHOW FULL TABLES IN "+ GetSqlConnection().Database+" WHERE TABLE_TYPE LIKE 'VIEW';");
        }
    }
}
