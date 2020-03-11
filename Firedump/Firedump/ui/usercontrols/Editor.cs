using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using Firedump.models;
using Firedump.models.events;
using Firedump.core;
using Firedump.core.db;
using Firedump.core.parsers;
using Firedump.core.sql;
using System.Data.Common;
using Firedump.core.sql.executor;
using com.protectsoft.SqlStatementParser;
using Firedump.core.models;
using Firedump.core.models.events;

namespace Firedump.usercontrols
{
    public partial class Editor : UserControlReference
    {
        // One readonly list, all tabs have reference to this list!
        private readonly List<AutocompleteItem> menuItems = new List<AutocompleteItem>();
        public event EventHandler<ExecutionEventArgs<string>> StatementExecuted;

        public Editor()
        {
            InitializeComponent();
        }

        internal override void Init()
        {
            //Sample Dummy sql data
            this.AddQueryTab(Properties.Resources.sample);
        }

        internal sealed override void dataReceived(ITriplet<UserControlReference, UserControlReference, object> triplet)
        {
            if (triplet.SourceType() == typeof(TableView) && triplet.DataType() == typeof(List<string>))
            {
                this.UpdateEditor((List<string>)triplet.GetData(), DbUtils.getTablesInfo(this.GetServer(),this.GetSqlConnection()));
            }
        }

        internal sealed override void onConnected()
        {
            base.onConnected();
            if (!string.IsNullOrEmpty(GetSqlConnection().Database) && this.tabControl1.Controls.Count == 0)
            {
                this.AddQueryTab();
            }
        }


        internal void AddQueryTab(string sql = "  ")
        {
            this.SuspendLayout();
            TabPageHolder myQueryTab = EditorAdapter.CreateQueryTab(this.tabControl1, imageList1, menuItems,new QueryExecutor(),sql);
            tabControl1.Controls.Add(myQueryTab);
            myQueryTab.GetFastColoredTextBox().KeyDown += fctb_KeyDown;
            myQueryTab.GetDataView().StatementExecuted += OnStatementExecuted;
            this.ResumeLayout();
        }

        private void OnStatementExecuted(object sender,ExecutionEventArgs<string> e)
        {
            Console.WriteLine("Editor:OnStatementExecuted:" + e.Status);
            Console.WriteLine("Editor:OnStatementExecuted:" + e.Value);
            if (e.Value != null)
            {
                Console.WriteLine("Execute");
                Execute(e.Value);
            } else
            {
                StatementExecuted?.Invoke(sender, e);
            }
        }

        private void fctb_KeyDown(object sender, KeyEventArgs e)
        {
            // cntrol and space show popup
            if ((e.KeyData == (Keys.Space | Keys.Control)) && tabControl1.SelectedTab != null)
            {
                // forced show (MinFragmentLength will be ignored)
                ((TabPageHolder)tabControl1.SelectedTab).GetAutocompleteMenu().Show(true);
                e.Handled = true;
            }
        }       

        private FastColoredTextBox GetSelectedTabEditor()
        {
            if(tabControl1.SelectedTab != null)
            {
               return (FastColoredTextBox)((TabPageHolder)tabControl1.SelectedTab).GetFastColoredTextBox();
            }
            return null;
        }

        private void UpdateEditor(List<string> tableList,List<Table> fields)
        {
            this.SuspendLayout();
            this.menuItems.Clear();
            this.menuItems.AddRange(AutoMenuItemBuilder.CreateTableItems(tableList));
            this.menuItems.AddRange(AutoMenuItemBuilder.CreateFieldItems(fields));
            this.menuItems.AddRange(AutoMenuItemBuilder.GetAllDbCommands());
            this.ResumeLayout();
        }


        internal void ExecuteScript()
        {
            var tb = GetSelectedTabEditor();
            if (tb != null && DbUtils.IsConnectedToDatabase(base.GetSqlConnection()))
            {
                string query = StringUtils.SelectedTextOrTabText(tb.SelectedText, tb.Text);
                if (!string.IsNullOrWhiteSpace(query) && tabControl1.SelectedTab != null)
                {
                    stopAnyRunningQuery(query);
                }
            }
        }

        private void Execute(string query)
        {
            var parser = new SqlStatementParserWrapper(query, (DbType)(int)_DbUtils.GetDbTypeEnum(base.GetSqlConnection()));
            List<string> statementList = SqlStatementParserWrapper.convert(parser.sql, parser.Parse());
            ((TabPageHolder)tabControl1.SelectedTab).GetDataView().ExecuteStatement(statementList, base.GetSqlConnection());
        }


        //Stop the open read from whatever tab it is
        private void stopAnyRunningQuery(string query)
        {
            foreach (TabPageHolder tab in tabControl1.Controls)
            {
                tab.GetDataView().StopRunningQuery(query);
            }
        }

    }
}
