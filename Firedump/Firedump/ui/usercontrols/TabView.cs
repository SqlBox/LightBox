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
using Firedump.core.models;
using static Firedump.core.models.MyTreeNode;
using Firedump.ui.usercontrols;

namespace Firedump.usercontrols
{
    public sealed partial class TabView : UserControlReference
    {
        private readonly ContextMenu TableTableMenu = new ContextMenu();
        private readonly ContextMenu TableTriggersMenu = new ContextMenu();

        private class FieldImageIndex
        {
            public string Value;
            public int Index;
        }

        public TabView()
        {
            InitializeComponent();
            var menuItemBuilder = new TabViewContextMenuBuilder(this);
            TableTableMenu.MenuItems.AddRange(menuItemBuilder.BuildeTableTableMenuItems());
            TableTriggersMenu.MenuItems.AddRange(menuItemBuilder.BuildTableTriggerMenuItems());
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
                MyTreeNode node = new MyTreeNode() { Text = t, ImageIndex = 0 ,Type = NodeType.Table };
                node.Nodes.Add(getDummy()); 
                treeViewTables.Nodes.Add(node);
            }
            this.treeViewTables.EndUpdate();
        }

        private void setTableFields(List<MyTreeNode> nodes, MyTreeNode parentNode)
        {
            this.treeViewTables.BeginUpdate();
            parentNode.Nodes.Clear();
            if (nodes != null && nodes.Count > 0)
            {
                foreach (var n in nodes)
                {
                    parentNode.Nodes.Add(n);
                }
            }
            else
            {
                parentNode.Nodes.Add(getDummy());
            }
            this.treeViewTables.EndUpdate();
        }


        private void TreeViewTables_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (!GetMainHome().GetUserControl<Editor>().GetQueryExecutor().IsAlive())
            {
                switch((e.Node as MyTreeNode).Type)
                {
                    case NodeType.Table:
                        {
                            List<string> fields = DbDataHelper.getTableFields(GetSqlConnection(), e.Node.Text);
                            List<string> fieldsInfo = DbDataHelper.getTableInfo(GetSqlConnection(), e.Node.Text);
                            List<MyTreeNode> nodes = new List<MyTreeNode>();
                            foreach (string f in fields)
                            {
                                nodes.Add(new MyTreeNode() { Text = f, ImageIndex = 1, SelectedImageIndex = 1 });
                            }
                            foreach (string f in fieldsInfo)
                            {
                                nodes.Add(new MyTreeNode() { Text = f, ImageIndex = 2, SelectedImageIndex = 2 });
                            }
                            var triggerParentNode = new MyTreeNode() { Text = "Triggers", ImageIndex = 3, SelectedImageIndex = 3, Type = NodeType.ParentTrigger };
                            triggerParentNode.Nodes.Add(getDummy());
                            nodes.Add(triggerParentNode);
                            this.setTableFields(nodes, e.Node as MyTreeNode);
                        }
                        break;
                    case NodeType.ParentTrigger:
                        {
                            List<string> triggers = DbDataHelper.getTableTriggers(GetSqlConnection(),e.Node.Parent.Text);
                            List<MyTreeNode> nodes = new List<MyTreeNode>();
                            foreach (string t in triggers)
                            {
                                nodes.Add(new MyTreeNode() { Text = t,Type = NodeType.Trigger, ImageIndex = 3, SelectedImageIndex = 3 });
                            }
                            this.setTableFields(nodes, e.Node as MyTreeNode);
                        }
                        break;
                }
            }
        }

        private MyTreeNode getDummy() =>
            new MyTreeNode() { ImageIndex = 0 };

        private void setDatagridviewTables()
        {
            setRootTablesIntoTreeView(new SqlBuilderFactory(base.GetSqlConnection())
                        .Create(null).removeSystemDatabases(DbDataHelper.getTables(base.GetSqlConnection()), false));
        }
    

        private void setDataGridViewIndexes()
        {
            dataGridViewIndexes.DataSource = DbDataHelper.getDataTableData(GetSqlConnection(),
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
                dataGridViewPKs.DataSource = DbDataHelper.getDataTableData(GetSqlConnection(),
                    new SqlBuilderFactory(GetServer()).Create(GetSqlConnection().Database).getDatabasePrimaryKeys());
            }
        }

        private void setDataGridUniques()
        {
            if(!Utils.IsDbEmbedded(GetServer().db_type))
            {
                dataGridViewUnique.DataSource = DbDataHelper.getDataTableData(GetSqlConnection(),
                    new SqlBuilderFactory(GetServer()).Create(GetSqlConnection().Database).getDatabaseUniques());
            }
        }

        private void setDataGridFKs()
        {
            if(!Utils.IsDbEmbedded(GetServer().db_type))
            {
                dataGridViewFKs.DataSource = DbDataHelper.getDataTableData(GetSqlConnection(),
                    new SqlBuilderFactory(GetServer()).Create(GetSqlConnection().Database).getDatabaseForeignKeys());
            }
        }

        private void setDataGridTriggers()
        {
            dataGridViewTrigger.DataSource = DbDataHelper.getDataTableData(GetSqlConnection(),
                new SqlBuilderFactory(GetServer()).Create(GetSqlConnection().Database).GetAllTriggers());
        }

        private void setDataGridProcedures()
        {
            if(!Utils.IsDbEmbedded(GetServer().db_type))
            {
                dataGridViewProcedures.DataSource = DbDataHelper.getDataTableData(GetSqlConnection(),
                new SqlBuilderFactory(GetServer()).Create(GetSqlConnection().Database).GetProcedures());
            }
        }

        private void setDataGridFunctions()
        {
            if(!Utils.IsDbEmbedded(GetServer().db_type))
            {
                dataGridViewFunctions.DataSource = DbDataHelper.getDataTableData(GetSqlConnection(),
                new SqlBuilderFactory(GetServer()).Create(GetSqlConnection().Database).GetFunctions());
            }   
        }

        private void setDatagridViews()
        {
            dataGridViewView.DataSource = DbDataHelper.getDataTableData(GetSqlConnection(),
                new SqlBuilderFactory(GetServer()).Create(GetSqlConnection().Database).GetAllViews());
        }


        private void treeViewTables_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                Point p = new Point(e.X,e.Y);
                MyTreeNode node = treeViewTables.GetNodeAt(p) as MyTreeNode;
                switch(node.Type)
                {
                    case NodeType.Table:
                        {
                            treeViewTables.SelectedNode = node;
                            TableTableMenu.Show(this, this.PointToClient(treeViewTables.PointToScreen(p)));
                        }
                        break;
                    case NodeType.Trigger:
                        {
                            treeViewTables.SelectedNode = node;
                            TableTriggersMenu.Show(this, this.PointToClient(treeViewTables.PointToScreen(p)));
                        }
                        break;
                }
            }
        }


        internal void TreeViewTable_MenuItem_ShowData(object sender,EventArgs e)
        {
            if(IsNodeSelected())
            {
                string sql = "SELECT * FROM " + treeViewTables.SelectedNode.Text;
                GetMainHome().GetUserControl<Editor>().Execute(sql, new QueryParams()
                {
                    Limit = GetMainHome().GetLimitFromToolStripComboBoxLimit(),
                    Offset = 0,
                });
                Terminal.MainTerminal.AppendText(sql);
            }
        }

        internal void TreeViewTable_MenuItem_ShowCreate(object sender, EventArgs e)
        {
            if (IsNodeSelected())
            {
                var node = treeViewTables.SelectedNode as MyTreeNode;
                if (node.Type == NodeType.Table)
                {
                    sendCreateTableToEditor(treeViewTables.SelectedNode.Text);
                }
                else if(node.Type == NodeType.Trigger)
                {
                    sendCreateTriggerToEditor(node.Parent.Parent.Text, node.Text);
                }
            }
        }

        internal void TreeViewTable_Menuitem_ShowCreateWithTrigger(object sender,EventArgs e)
        {
            if(IsNodeSelected())
            {
                var node = treeViewTables.SelectedNode as MyTreeNode;
                if (node.Type == NodeType.Table)
                {
                    sendCreateTableWithTriggersToEditor(treeViewTables.SelectedNode.Text);
                }
            }
        }

        private void sendCreateTableToEditor(string table)
        {
            GetMainHome().GetUserControl<Editor>().AddQueryTab(DbDataHelper.getCreateTable(GetSqlConnection(), table),table);
        }

        private void sendCreateTableWithTriggersToEditor(string table)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(DbDataHelper.getCreateTable(GetSqlConnection(), table) + ";\0\r\n");
            List<string> triggers = DbDataHelper.getTableTriggers(GetSqlConnection(), table);
            foreach(string t in triggers)
            {
                sb.Append(DbDataHelper.GetCreateTrigger(GetSqlConnection(), table, t));
            }
            GetMainHome().GetUserControl<Editor>().AddQueryTab(sb.ToString(),table);
        }

        private void sendCreateTriggerToEditor(string table,string triggerName)
        {
            GetMainHome().GetUserControl<Editor>().AddQueryTab(DbDataHelper.GetCreateTrigger(GetSqlConnection(), table, triggerName), triggerName);
        }

        internal void TreeViewTableTriggers_MenuItem_ShowCreate(object sender, EventArgs e)
        {
            if(IsNodeSelected())
            {
                sendCreateTriggerToEditor(treeViewTables.SelectedNode.Parent.Parent.Text,treeViewTables.SelectedNode.Text);
            }
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
