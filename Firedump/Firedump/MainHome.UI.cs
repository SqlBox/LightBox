using Firedump.core.models;
using Firedump.core.models.dbinfo;
using Firedump.core.models.events;
using Firedump.ui.forms;
using Firedump.ui.usercontrols;
using Firedump.usercontrols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Firedump
{
    public partial class MainHome
    {

        public int GetLimitFromToolStripComboBoxLimit()
        {
            return ((MyToolStripItem)toolStripComboBoxLimit.SelectedItem).Limit;
        }

        public bool IsContinueExecutingOnFail()
        {
            return this.toolStripButtonContExec.Checked;
        }


        private void InitMainMenuComponents()
        {
            this.tabView1 = new Firedump.usercontrols.TabView();
            this.tableView1 = new Firedump.usercontrols.TableView();
            this.terminal1 = new Firedump.ui.usercontrols.Terminal();
            this.splitContainerMainParent.Panel2.SuspendLayout();
            this.splitContainerMiddle.Panel1.SuspendLayout();
            this.splitContainerMiddleChild.Panel2.SuspendLayout();
            this.splitContainerMainParent.Panel2.Controls.Add(this.terminal1);
            this.splitContainerMiddle.Panel1.Controls.Add(this.tabView1);
            // 
            // tabView1
            // 
            this.tabView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(241)))));
            this.tabView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabView1.Location = new System.Drawing.Point(0, 0);
            this.tabView1.Name = "tabView1";
            this.tabView1.Size = new System.Drawing.Size(250, 485);
            this.tabView1.TabIndex = 0;
            this.splitContainerMiddleChild.Panel2.Controls.Add(this.tableView1);
            // 
            // tableView1
            // 
            this.tableView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(241)))));
            this.tableView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableView1.Location = new System.Drawing.Point(0, 0);
            this.tableView1.Name = "tableView1";
            this.tableView1.Size = new System.Drawing.Size(186, 485);
            this.tableView1.TabIndex = 0;
            // 
            // terminal1
            // 
            this.terminal1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(241)))));
            this.terminal1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.terminal1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.terminal1.Location = new System.Drawing.Point(0, 0);
            this.terminal1.Margin = new System.Windows.Forms.Padding(0);
            this.terminal1.Name = "terminal1";
            this.terminal1.Size = new System.Drawing.Size(1169, 90);
            this.terminal1.TabIndex = 0;
            this.splitContainerMainParent.Panel2.ResumeLayout(false);
            this.splitContainerMiddle.Panel1.ResumeLayout(false);
            this.splitContainerMiddleChild.Panel2.ResumeLayout(false);

            toolStripComboBoxLimit.Items.AddRange(new object[] { new MyToolStripItem(100) { }, new MyToolStripItem(200) { }
            ,new MyToolStripItem(500) { },new MyToolStripItem(1000) { },new MyToolStripItem(5000) { },new MyToolStripItem(10_000) { }
            ,new MyToolStripItem(50_000) { },new MyToolStripItem(100_000) { },new MyToolStripItem(0) { }});
            //Selected index should be fetch from what user last selected or options
            toolStripComboBoxLimit.SelectedIndex = 0;
            this.terminal1.SetMainHome(this);
            Terminal.MainTerminal = this.terminal1;
        }

        private void InitEditorComponent()
        {
            this.editor1 = new Firedump.usercontrols.Editor();
            this.splitContainerMiddleChild.Panel1.Controls.Add(this.editor1);
            this.editor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editor1.Name = "Editor1";
            this.editor1.TabIndex = 0;
        }

        //Disable ui elements when query is running.
        private void EnableUi(bool enable)
        {
           this.Invoke((MethodInvoker)delegate {
               this.SuspendLayout();
               GetUserControl<TabView>().GetDatabasesCombobox().Enabled = enable;
               this.toolStripProgressBar1.Enabled = !enable;
               this.toolStripProgressBar1.Visible = !enable;
               this.toolStripButtonExecute.Enabled = enable;
               this.toolStripButtonShowSysDb.Enabled = enable;
               this.toolStripButtonConnect.Enabled = enable;
               toolStripButtonDisconnect.Enabled = enable;
               toolStripButtonReconnect.Enabled = enable;
               toolStripButtonCommit.Enabled = enable;
               toolStripButtonRollback.Enabled = enable;
               toolStripButtonExecCurrent.Enabled = enable;
               toolStripButtonExecNext.Enabled = enable;
               toolStripButtonNewTab.Enabled = enable;
               toolStripButtonCloseTab.Enabled = enable;
               this.ResumeLayout();
           });   
        }


        private void OnStatementExecuted(object sender, ExecutionQueryEvent e)
        {
            if(e.Status != Status.RUNNING) {
                EnableUi(true);
            }
        }
    }
}
