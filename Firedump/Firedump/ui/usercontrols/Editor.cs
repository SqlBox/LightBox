using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using Firedump.models;
using Firedump.models.events;
using Firedump.core;
using Firedump.core.db;
using Firedump.core.sql;
using System.Data.Common;
using Firedump.core.sql.executor;
using com.protectsoft.SqlStatementParser;
using Firedump.core.models;
using Firedump.core.models.events;
using Firedump.core.models.dbinfo;
using sqlbox.commons;

namespace Firedump.usercontrols
{
    public partial class Editor : UserControlReference
    {
        private readonly ContextMenu tabMenuContext = new ContextMenu();
        // One readonly list, all tabs have reference to this list!
        private readonly List<AutocompleteItem> menuItems = new List<AutocompleteItem>();
        private readonly QueryExecutor queryExecutor = new QueryExecutor();
        public event EventHandler<ExecutionQueryEvent> StatementExecuted;
        //Run this to execute the disableUi method on mainhome.
        //necessary when the execution of query starts with F5 key stroke from this controls editor selected tab and not from the main menu run icon.
        public Action<bool> EnableUi;

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

        internal QueryExecutor GetQueryExecutor()
        {
            return this.queryExecutor;
        }

        internal void UpdateEditor(List<string> tables)
        {
            this.UpdateEditor(tables, DbDataHelper.getTablesInfo(this.GetServer(), this.GetSqlConnection()));
        }

        internal sealed override void onConnected()
        {
            if (!string.IsNullOrEmpty(GetSqlConnection().Database) && this.tabControl1.Controls.Count == 0)
            {
                this.AddQueryTab();
            }
        }


        internal void AddQueryTab(string sql = "  ",string tabname = null)
        {
            this.SuspendLayout();
            TabPageHolder myQueryTab = EditorAdapter.CreateQueryTab(this.tabControl1, imageList1, menuItems,this,sql, tabname);
            tabControl1.Controls.Add(myQueryTab);
            myQueryTab.GetFastColoredTextBox().KeyDown += fctb_KeyDown;
            tabControl1.SelectedTab = myQueryTab;
            this.ResumeLayout();
        }

        private void OnStatementExecuted(object sender,ExecutionQueryEvent e)
        {
            StatementExecuted?.Invoke(sender, e);
            if(e.Status == Status.HIDDEN)
            {
                return;
            }
            else if(e.Status == Status.ABORTED)
            {
                this.Invoke((MethodInvoker)delegate {
                    base.GetMainHome().AbandonAndOpenNewConnection();
                });
            }
            foreach (TabPageHolder dv in tabControl1.Controls) {
               if(dv.GetDataView().GetHashCode() == e.TAG)
                {
                    this.Invoke((MethodInvoker)delegate {
                        if (e.Status == Status.FINISHED && e.QueryParams.Sql == null)
                        {
                            dv.GetDataView().SetData(e.data,e.query);
                        } else if(e.Status == Status.FINISHED && e.QueryParams.Sql != null)
                        {
                            dv.GetDataView().AppendData(e.data);
                        }

                        if(e.QueryParams.Sql == null || e.Status == Status.ABORTED || e.Status == Status.CANCELED)
                        {
                            dv.GetDataView().SetHistory(e);
                        }
                    });
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
            }
            else if (e.KeyData == Keys.F5)
            {
                ExecuteScript(null);
            }
            else if (e.KeyData == Keys.F6)
            {
            }
            else if (e.KeyData == Keys.F7) 
            { 
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


        internal void ExecuteScript(QueryParams parameters)
        {
            var tb = GetSelectedTabEditor();
            if (tb != null && DB.IsConnectedToDatabaseAndAfterReconnect(this))
            {
                if (parameters == null)
                {
                    parameters = new QueryParams()
                    {
                        Limit = GetMainHome().GetLimitFromToolStripComboBoxLimit(),
                        Offset = 0,
                    };
                }
                Execute(StringUtils.SelectedTextOrTabText(tb.SelectedText, tb.Text), parameters);
            } else
            {
                StatementExecuted?.Invoke(this, new ExecutionQueryEvent(Status.FINISHED));
            }
        }

        internal void Fetch(QueryParams parameters)
        {
            this.ExecuteScript(parameters);
        }

        internal void Execute(string query, QueryParams parameters)
        {
            EnableUi(false);
            parameters.Hash = ((TabPageHolder)tabControl1.SelectedTab).GetDataView().GetHashCode();
            if (parameters.Sql == null)
            {
                if (!string.IsNullOrWhiteSpace(query))
                {
                    //At the moment parser only supports mysql,mariadb,postgresql
                    //any other db i just execute all in one statement
                    sqlbox.commons.DbType dbtype = Firedump.core.sql.Utils.GetDbTypeEnum(base.GetSqlConnection());
                    List<string> statementList = null;
                    if (dbtype == sqlbox.commons.DbType.MYSQL || dbtype == sqlbox.commons.DbType.MARIADB || dbtype == sqlbox.commons.DbType.POSTGRES)
                    {
                        var parser = new SqlStatementParserWrapper(query, (com.protectsoft.SqlStatementParser.DbType)(int)dbtype);
                        statementList = SqlStatementParserWrapper.convert(parser.sql, parser.Parse());
                    } else
                    {
                        statementList = new List<string>();
                        statementList.Add(query);
                    }
                    this.queryExecutor.Execute(statementList, this.GetSqlConnection(), parameters,this.GetMainHome().IsContinueExecutingOnFail());
                }
                else
                {
                    StatementExecuted?.Invoke(this, new ExecutionQueryEvent(Status.FINISHED));
                }
            }
            else
            {
                //case of lazy fetch
                this.queryExecutor.Execute(new List<string>() { parameters.Sql }, this.GetSqlConnection(), parameters, this.GetMainHome().IsContinueExecutingOnFail());
            }
        }

        internal void stopAnyRunningQuery()
        {
            this.queryExecutor?.Cancel();
        }

        internal void abandonRunningQuery()
        {
            this.queryExecutor?.Abort();
        }


    }
}
