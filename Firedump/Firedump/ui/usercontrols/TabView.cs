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
using Firedump.core.sql.executor;

namespace Firedump.usercontrols
{
    public sealed partial class TabView : UserControlReference
    {
        private readonly ContextMenu treeTableMenu = new ContextMenu();
        private class FieldImageIndex
        {
            public string Value;
            public int Index;
        }

        
        public TabView()
        {
            InitializeComponent();
            this.BuildTreeTableMenu();
        }

        private void BuildTreeTableMenu()
        {
            treeTableMenu.MenuItems.AddRange(ControlBuilder.TreeTableMenuItemsBuilder(this));
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
            if(!Utils.IsDbEmbedded(GetServer().db_type))
            {
                base.changeDatabase(comboBoxServers.SelectedItem.ToString());
            }
            this.initTabControl(comboBoxServers.SelectedItem.ToString());
        }


        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!GetMainHome().GetUserControl<Editor>().GetQueryExecutor().IsAlive())
            {
                this.initTabControl();
            }
        }

        // On tab select set the data.
        private void initTabControl(string database = null)
        {
            if (!Utils.IsDbEmbedded(GetServer().db_type) && database != null && DB.IsConnectedToDatabaseAndAfterReconnect(this))
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
            if (!GetMainHome().GetUserControl<Editor>().GetQueryExecutor().IsAlive())
            {
                List<string> fields = DbUtils.getTableFields(GetSqlConnection(), e.Node.Text);
                List<string> fieldsInfo = DbUtils.getTableInfo(GetSqlConnection(), e.Node.Text);
                List<FieldImageIndex> finalFieldList = new List<FieldImageIndex>();
                foreach (string f in fields)
                {
                    finalFieldList.Add(new FieldImageIndex() { Value = f, Index = 1 });
                }
                foreach (string f in fieldsInfo)
                {
                    finalFieldList.Add(new FieldImageIndex() { Value = f, Index = 2 });
                }
                this.setTableFields(finalFieldList, e.Node.Index);
            }
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
                    new SqlBuilderFactory(GetServer()).Create(GetSqlConnection().Database).getDatabaseIndexes());
        }

        private void setDataGridViewPKs()
        {
            if(Utils.IsDbEmbedded(GetServer().db_type))
            {
                sqlbox.commons.DbType type = Utils._convert(GetServer().db_type);
                if(type == sqlbox.commons.DbType.SQLITE)
                {
                    dataGridViewPKs.DataSource = SqliteHelpers.GetDatabasePrimaryKeysDataSource(GetServer(),GetSqlConnection());
                }
            } else
            {
                dataGridViewPKs.DataSource = DbUtils.getDataTableData(GetSqlConnection(),
                    new SqlBuilderFactory(GetServer()).Create(GetSqlConnection().Database).getDatabasePrimaryKeys());
            }
        }

        private void setDataGridUniques()
        {
            if(!Utils.IsDbEmbedded(GetServer().db_type))
            {
                dataGridViewUnique.DataSource = DbUtils.getDataTableData(GetSqlConnection(),
                    new SqlBuilderFactory(GetServer()).Create(GetSqlConnection().Database).getDatabaseUniques());
            }
        }

        private void setDataGridFKs()
        {
            if(!Utils.IsDbEmbedded(GetServer().db_type))
            {
                dataGridViewFKs.DataSource = DbUtils.getDataTableData(GetSqlConnection(),
                    new SqlBuilderFactory(GetServer()).Create(GetSqlConnection().Database).getDatabaseForeignKeys());
            }
        }

        private void setDataGridTriggers()
        {
            dataGridViewTrigger.DataSource = DbUtils.getDataTableData(GetSqlConnection(),
                new SqlBuilderFactory(GetServer()).Create(GetSqlConnection().Database).GetAllTriggers());
        }

        private void setDataGridProcedures()
        {
            if(!Utils.IsDbEmbedded(GetServer().db_type))
            {
                dataGridViewProcedures.DataSource = DbUtils.getDataTableData(GetSqlConnection(),
                new SqlBuilderFactory(GetServer()).Create(GetSqlConnection().Database).GetProcedures());
            }
        }

        private void setDataGridFunctions()
        {
            if(!Utils.IsDbEmbedded(GetServer().db_type))
            {
                dataGridViewFunctions.DataSource = DbUtils.getDataTableData(GetSqlConnection(),
                new SqlBuilderFactory(GetServer()).Create(GetSqlConnection().Database).GetFunctions());
            }   
        }

        private void setDatagridViews()
        {
            dataGridViewView.DataSource = DbUtils.getDataTableData(GetSqlConnection(),
                new SqlBuilderFactory(GetServer()).Create(GetSqlConnection().Database).GetAllViews());
        }


        private void treeViewTables_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                Point p = new Point(e.X,e.Y);
                TreeNode node = treeViewTables.GetNodeAt(p);
                if (node == null || node.Parent != null) return;
                treeViewTables.SelectedNode = node;
                treeTableMenu.Show(this, this.PointToClient(treeViewTables.PointToScreen(p)));
            }
        }


        internal void TreeViewTable_MenuItem_ShowData(object sender,EventArgs e)
        {
            if(IsNodeSelected())
            {
                GetMainHome().GetUserControl<Editor>().Execute("SELECT * FROM " + treeViewTables.SelectedNode.Text, new QueryParams()
                {
                    Limit = GetMainHome().GetLimitFromToolStripComboBoxLimit(),
                    Offset = 0,
                });
            }
        }

        internal void TreeViewTable_MenuItem_ShowCreate(object sender, EventArgs e)
        {
            if(IsNodeSelected())
            {
                if (sender is TreeView)
                {
                    if (((TreeView)sender).SelectedNode != null && ((TreeView)sender).SelectedNode.Parent == null)
                    {
                        sendCreateToEditor(treeViewTables.SelectedNode.Text);
                    }
                }
                else
                {
                    sendCreateToEditor(treeViewTables.SelectedNode.Text);
                }
            }
        }

        private void sendCreateToEditor(string table)
        {
            GetMainHome().GetUserControl<Editor>().AddQueryTab(DbUtils.getCreateTable(GetSqlConnection(), table),table);
        }

        internal void TreeViewTable_MenuItem_DropTable(object sender, EventArgs e)
        {
        }

        internal void TreeViewTable_MenuItem_TruncateTable(object sender,EventArgs e)
        {
        }

        internal void TreeViewTable_MenuItem_RefreshTable(object sender,EventArgs e)
        {
        }

        internal void TreeViewTable_MenuItem_Inspect(object sender,EventArgs e)
        {
        }

        private bool IsNodeSelected()
        {
            return treeViewTables.SelectedNode != null && treeViewTables.SelectedNode.Text != null;
        }

        internal ComboBox GetDatabasesCombobox()
        {
            return this.comboBoxServers;
        }

    }
}
