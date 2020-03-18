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
using Firedump.core.models.dbinfo;

namespace Firedump.usercontrols
{
    public partial class Editor : UserControlReference
    {
        // One readonly list, all tabs have reference to this list!
        private readonly List<AutocompleteItem> menuItems = new List<AutocompleteItem>();
        private readonly QueryExecutor queryExecutor = new QueryExecutor();
        public event EventHandler<ExecutionEventArgs> StatementExecuted;
        //Run this to execute the disableUi method on mainhome.
        //necessary when the execution of query starts with F5 key stroke from this controls editor selected tab and not from the main menu run icon.
        public Action DisableUi;

        public Editor()
        {
            InitializeComponent();
            this.queryExecutor.StatementExecuted += OnStatementExecuted;
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
            if (!string.IsNullOrEmpty(GetSqlConnection().Database) && this.tabControl1.Controls.Count == 0)
            {
                this.AddQueryTab();
            }
        }


        internal void AddQueryTab(string sql = "  ")
        {
            this.SuspendLayout();
            TabPageHolder myQueryTab = EditorAdapter.CreateQueryTab(this.tabControl1, imageList1, menuItems,sql);
            tabControl1.Controls.Add(myQueryTab);
            myQueryTab.GetFastColoredTextBox().KeyDown += fctb_KeyDown;
            this.ResumeLayout();
        }

        private void OnStatementExecuted(object sender,ExecutionEventArgs e)
        {
            StatementExecuted?.Invoke(sender, e);
            foreach (TabPageHolder dv in tabControl1.Controls) {
               if(dv.GetFastColoredTextBox().GetHashCode() == e.TAG)
                {
                    if (e.Status == Status.FINISHED)
                    {
                        this.Invoke((MethodInvoker)delegate {
                            dv.GetDataView().SetData(e.data);
                            if (e.Ex != null)
                            {
                            }
                        });
                    }
                    break;
                }
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
            } else if(e.KeyData == Keys.F5)
            {
                DisableUi();
                ExecuteScript();
            }
        }       

        private FastColoredTextBox GetSelectedTabEditor()
        {
            return (FastColoredTextBox)((TabPageHolder)tabControl1.SelectedTab)?.GetFastColoredTextBox();
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
            if (tb != null && DB.IsConnectedToDatabaseAndAfterReconnect(this))
            {
                Execute(StringUtils.SelectedTextOrTabText(tb.SelectedText, tb.Text), tb.GetHashCode());
            } else
            {
                StatementExecuted?.Invoke(this, new ExecutionEventArgs(Status.FINISHED));
            }
        }

        private void Execute(string query,int hash)
        {
            if (!string.IsNullOrWhiteSpace(query))
            {
                var parser = new SqlStatementParserWrapper(query, (DbType)(int)_DbUtils.GetDbTypeEnum(base.GetSqlConnection()));
                List<string> statementList = SqlStatementParserWrapper.convert(parser.sql, parser.Parse());
                this.queryExecutor.Execute(statementList, this.GetSqlConnection(),hash);
            } else
            {
                StatementExecuted?.Invoke(this, new ExecutionEventArgs(Status.FINISHED));
            }
        }


        internal void stopAnyRunningQuery()
        {
            this.queryExecutor?.Cancel();
        }


    }
}
