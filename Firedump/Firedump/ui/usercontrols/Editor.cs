using com.protectsoft.SqlStatementParser;
using FastColoredTextBoxNS;
using Firedump.core;
using Firedump.core.db;
using Firedump.core.models;
using Firedump.core.models.dbinfo;
using Firedump.core.models.events;
using Firedump.core.sql;
using Firedump.core.sql.executor;
using Firedump.models;
using Firedump.models.events;
using Firedump.ui.forms;
using sqlbox.commons;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Windows.Forms;

namespace Firedump.usercontrols
{
    public partial class Editor : UserControlReference
    {
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


        internal void AddQueryTab(string sql = "  ", string tabname = null, bool isFile = false)
        {
            this.SuspendLayout();
            TabPageHolder myQueryTab = EditorAdapter.CreateQueryTab(this.tabControl1, imageList1, menuItems, this, sql, tabname, isFile);
            tabControl1.Controls.Add(myQueryTab);
            myQueryTab.GetFastColoredTextBox().KeyDown += fctb_KeyDown;
            tabControl1.SelectedTab = myQueryTab;
            this.ResumeLayout();
        }

        private void OnStatementExecuted(object sender, ExecutionQueryEvent e)
        {
            StatementExecuted?.Invoke(sender, e);
            if (e.Status == Status.HIDDEN)
            {
                return;
            }
            else if (e.Status == Status.ABORTED)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    base.GetMainHome().AbandonAndOpenNewConnection();
                });
            }
            foreach (TabPageHolder dv in tabControl1.Controls)
            {
                if (dv.GetDataView().GetHashCode() == e.TAG)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        if (e.Status == Status.FINISHED && e.QueryParams.Sql == null)
                        {
                            dv.GetDataView().SetData(e.data, e.query);
                        }
                        else if (e.Status == Status.FINISHED && e.QueryParams.Sql != null)
                        {
                            dv.GetDataView().AppendData(e.data);
                        }

                        if (e.QueryParams.Sql == null || e.Status == Status.ABORTED || e.Status == Status.CANCELED)
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
                ExecuteCurrent();
            }
            else if (e.KeyData == Keys.F7)
            {
                ExecuteCurrent(true);
            }
        }

        private FastColoredTextBox GetSelectedTabEditor()
        {
            return (FastColoredTextBox)((TabPageHolder)tabControl1.SelectedTab)?.GetFastColoredTextBox();
        }

        private void UpdateEditor(List<string> tableList, List<Table> fields)
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
            }
            else
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
                    this.queryExecutor.Execute(core.sql.Utils.convert(Firedump.core.sql.Utils.GetDbTypeEnum(base.GetSqlConnection()), query),
                        this.GetSqlConnection(), parameters, this.GetMainHome().IsContinueExecutingOnFail());
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

        internal void ExecuteCurrent(bool moveToNext = false)
        {
            var tb = GetSelectedTabEditor();
            core.sql.Utils.selectCurrent(tb, moveToNext);
            if (tb != null && !string.IsNullOrEmpty(tb.SelectedText))
            {
                ExecuteScript(null);
            }
            else
            {
                StatementExecuted?.Invoke(this, new ExecutionQueryEvent(Status.FINISHED));
            }
        }

        internal void OpenFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var fileInfo = FileIO.FileInfo(ofd.FileName);
                if(fileInfo != null)
                {
                    long bytes = fileInfo.Length;
                    if (bytes > 10_000_000)
                    {
                        var form = new LargeFileForm(bytes, ofd.FileName);
                        form.ShowDialog();
                        if (form.openExecute == OpenExecute.OPEN)
                        {
                            AddQueryTab(" ", ofd.FileName, true);
                            try
                            {
                                GetSelectedTabEditor().OpenBindingFile(ofd.FileName, System.Text.Encoding.UTF8);
                            }
                            catch (IOException ex) { /*case try to re open same binding file again or a locked opened file*/}
                        }
                        else if (form.openExecute == OpenExecute.EXECUTE)
                        {
                            if (DB.IsConnectedToDatabaseAndAfterReconnect(this))
                            {
                                new ExecuteScriptForm(GetSqlConnection(), ofd.FileName).Show();
                            }
                            else
                            {
                                StatementExecuted?.Invoke(this, new ExecutionQueryEvent(Status.FINISHED));
                            }
                        }
                    }
                    else
                    {
                        var text = FileIO.ReadAllText(ofd.FileName);
                        if(text != null)
                        {
                            AddQueryTab(text, ofd.FileName, true);
                        }
                    }
                }
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


        internal void SaveAll()
        {
            foreach (TabPageHolder dv in tabControl1.Controls)
            {
                if(dv.IsFile)
                {
                    FileIO.Save(dv.GetFastColoredTextBox(), dv.Text);
                }
            }
        }

        internal void Save()
        {
            var tp = ((TabPageHolder)tabControl1.SelectedTab);
            if(tp != null)
            {
                if(tp.IsFile)
                {
                    FileIO.Save(tp.GetFastColoredTextBox(), tp.Text);
                } else
                {
                    SaveAs();
                }
            }
        }

        internal void SaveAs()
        {
            var tb = GetSelectedTabEditor();
            if(tb != null)
            {
                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.Filter = "sql files (*.sql)|*.sql|All files (*.*)|*.*";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        FileIO.Save(tb, dialog.FileName);
                    }
                }
            }
        }
    }
}
