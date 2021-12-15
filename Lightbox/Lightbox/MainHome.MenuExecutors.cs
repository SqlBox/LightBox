using Lightbox.core;
using Lightbox.core.db;
using Lightbox.core.models;
using Lightbox.core.sql;
using Lightbox.ui.forms;
using Lightbox.usercontrols;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lightbox
{
    // This Partial Class Consists most of the menu click events.
    // And the purpose its just to clear down the boilerplate code form MainHome class
    public partial class MainHome
    {
        private bool findTextBoxInputChange = false;


        internal T GetUserControl<T>() where T : UserControlReference
        {
            foreach (UserControlReference c in ChildControls)
            {
                if (c is T)
                {
                    return (T)c;
                }
            }
            return null;
        }

        private TabView GetTabView()
        {
            foreach (UserControlReference c in ChildControls)
                if (c is TabView)
                    return (c as TabView);
            return null;
        }

        private void reconnectEventClick(object sender, EventArgs e)
        {
            if (this.server != null)
            {
                if (this.con != null)
                    this.con.Close();
                onDisconnected(null, null);
                setConstrolEnableStatus(false);
                this.ConnectToDbClick(null, null);
            }
        }

        private void Undo(object sender, EventArgs e)
        {
            GetUserControl<Editor>().Undo();
        }

        private void Redo(object sender, EventArgs e)
        {
            GetUserControl<Editor>().Redo();
        }

        private void PrettifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetUserControl<Editor>().formatSelectedTab();
        }

        private void CommitBtnEventClick(object sender, EventArgs e)
        {
            if (DB.IsConnected(this.con))
            {
                DB.Commit(this.con);
                if (Utils._convert(server.db_type) == sqlbox.commons.DbType.SQLITE && Properties.Settings.Default.option_sqlite_begintranscommit)
                {
                    DbDataHelper.executeNonQuery(con, "begin transaction");
                }
            }
        }

        private void RollbackBtnEventClick(object sender, EventArgs e)
        {
            if (DB.IsConnected(this.con))
            {
                DB.Rollback(this.con);
            }
        }

        private void addNewQueryTab(object sender, EventArgs e)
        {
            GetUserControl<Editor>().AddQueryTab();
        }



        // Closes selected open tab
        private void closeTabClick(object sender, EventArgs e)
        {
            GetUserControl<Editor>().CloseSelectedTab();
        }

        private void OnSearchBoxEnterKey(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == '\r')
            {
                this.handleFindClickEvent(this.toolStripTextBoxSearch.Text, this.findTextBoxInputChange, true);
                this.findTextBoxInputChange = false;
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                this.findTextBoxInputChange = true;
            }
        }

        private void FindNextClick(object sender, EventArgs e)
        {
            this.handleFindClickEvent(this.toolStripTextBoxSearch.Text, this.findTextBoxInputChange, true);
            this.findTextBoxInputChange = false;
        }

        private void FindPrevClick(object sender, EventArgs e)
        {
            this.handleFindClickEvent(this.toolStripTextBoxSearch.Text, this.findTextBoxInputChange, false);
            this.findTextBoxInputChange = false;
        }

        private void OnSearchClick(object sender, EventArgs e)
        {
            GetUserControl<Editor>().ShowFindDialog();
        }

        private void OnGoToLineClick(object sender, EventArgs e)
        {
            GetUserControl<Editor>().ShowGoToLineDialog();
        }

        private void OnReplaceClick(object sender, EventArgs e)
        {
            GetUserControl<Editor>().ShowReplaceDialog();
        }

        private void handleFindClickEvent(string text, bool hasChanged, bool isNext)
        {
            GetUserControl<Editor>().FindText(text, hasChanged, isNext);
        }

        private void ToUpperClick(object sender, EventArgs e)
        {
            GetUserControl<Editor>().ToUpper();
        }

        private void ToLowerClick(object sender, EventArgs e)
        {
            GetUserControl<Editor>().ToLower();
        }

        private void ExportHtmlClick(object sender, EventArgs e)
        {
            GetUserControl<Editor>().ExportHtml();
        }

        private void PrintSelected(object sender, EventArgs e)
        {
            GetUserControl<Editor>().PrintSelected();
        }

        private void ExecuteScript(object sender, EventArgs e)
        {
            EnableUi(false);
            try
            {
                GetUserControl<Editor>().ExecuteScript(null);
            }
            catch (Exception ex)
            {
                //LOG
                //If something out of control goes wrong at least re enable the ui
                EnableUi(false);
            }
        }

        private void StopRunningQuery(object sender, EventArgs e)
        {
            GetUserControl<Editor>().stopAnyRunningQuery();
        }

        private void ContOnErrorClick(object sender, EventArgs e)
        {
            ((ToolStripButton)sender).Checked = !((ToolStripButton)sender).Checked;
        }


        private void AbandonClick(object sender, EventArgs e)
        {
            GetUserControl<Editor>().abandonRunningQuery();
        }


        private void startLightboxprocess(object sender, EventArgs e)
        {
            //Firedump.Program.Main(new string[] { });
            new Firedump.Home().Show();
        }

        private void refreshdbdata(object sender, EventArgs e)
        {
            this.setConnectionAndServerToUserControls();
        }

        private void ExecuteCurrent(object sender, EventArgs e)
        {
            EnableUi(false);
            try
            {
                GetUserControl<Editor>().ExecuteCurrent();
            }
            catch (Exception ex)
            {
                //LOG
                //If something out of control goes wrong at least re-enable the ui
                EnableUi(false);
            }
        }

        private void ExecuteNext(object sender, EventArgs e)
        {
            EnableUi(false);
            try
            {
                GetUserControl<Editor>().ExecuteCurrent(true);
            }
            catch (Exception ex)
            {
                //LOG
                //If something out of control goes wrong at least re-enable the ui
                EnableUi(false);
            }
        }

        private void MenuOptionsClick(object sender, EventArgs e)
        {
            new OptionsForm(this.con).ShowDialog();
        }

        private void toolStripButtonOpenFile_Click(object sender, EventArgs e)
        {
            GetUserControl<Editor>().OpenFile();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new AboutForm().Show();
        }

        private void toolStripButtonOpenExecuteFileForm_Click(object sender, EventArgs e)
        {
            if (DB.IsConnectedToDatabase(this.con))
            {
                OpenFileDialog ofd = new OpenFileDialog();
                var res = ofd.ShowDialog();
                if (res == DialogResult.OK)
                {
                    new ExecuteScriptForm(this.con, ofd.FileName).Show();
                }
            }
        }

        private void saveScriptAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetUserControl<Editor>().SaveAs();
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GetUserControl<Editor>().Save();
        }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetUserControl<Editor>().SaveAll();
        }
    }
}
