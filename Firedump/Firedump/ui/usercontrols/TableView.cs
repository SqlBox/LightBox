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
using Firedump.models.events;
using Firedump.models;
using Firedump.core.db;
using Firedump.core.sql;

namespace Firedump.usercontrols
{
    public sealed partial class TableView : UserControlReference
    {

        public TableView() { InitializeComponent(); }

        internal sealed override void onConnected()
        {
            if (!Utils.IsDbEmbedded(GetServer().db_type))
            {
                if (DB.IsConnectedToDatabase(base.GetSqlConnection()))
                {
                    List<string> tables = new SqlBuilderFactory(base.GetSqlConnection())
                            .Create(null).removeSystemDatabases(DbDataHelper.getTables(base.GetSqlConnection()), false);
                    GetMainHome().GetUserControl<Editor>().UpdateEditor(tables);
                    this.setRootTablesIntoTreeView(tables);
                }
            } else
            {
                List<string> tables = new SqlBuilderFactory(base.GetSqlConnection())
                            .Create(null).removeSystemDatabases(DbDataHelper.getTables(base.GetSqlConnection()), false);
                GetMainHome().GetUserControl<Editor>().UpdateEditor(tables);
                this.setRootTablesIntoTreeView(tables);
            }
        }

        private void setRootTablesIntoTreeView(List<string> tables)
        {
            this.treeViewTables.BeginUpdate();
            treeViewTables.Nodes.Clear();
            foreach (string t in tables)
            {
                TreeNode node = new TreeNode(t) { ImageIndex = (int)FinalImageIndex.Table };
                node.Nodes.Add(getDummy());
                treeViewTables.Nodes.Add(node);
            }
            this.treeViewTables.EndUpdate();
        }

        private void setTableFields(List<string> fields,int nodeIndex)
        {
            this.treeViewTables.BeginUpdate();
            treeViewTables.Nodes[nodeIndex].Nodes.Clear();
            if (fields != null && fields.Count > 0)
            {
                foreach(string f in fields)
                {
                    treeViewTables.Nodes[nodeIndex].Nodes.Add(new TreeNode(f) { ImageIndex = (int) FinalImageIndex.Field,SelectedImageIndex = (int)FinalImageIndex.Field });
                }
            } else
            {
                treeViewTables.Nodes[nodeIndex].Nodes.Add(getDummy());
            }
            this.treeViewTables.EndUpdate();
        }


        private void TreeViewTables_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (!GetMainHome().GetUserControl<Editor>().GetQueryExecutor().IsAlive())
            {
                this.setTableFields(DbDataHelper.getTableFields(GetSqlConnection(), e.Node.Text), e.Node.Index);
            }
        }

        private TreeNode getDummy() =>
            new TreeNode() { ImageIndex = 1 };
    }
}
