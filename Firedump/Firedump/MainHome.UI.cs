using Firedump.core.models;
using Firedump.core.models.dbinfo;
using Firedump.core.models.events;
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

        private void InitMainMenuComponents()
        {
            toolStripComboBoxLimit.Items.AddRange(new object[] { new MyToolStripItem(100) { }, new MyToolStripItem(200) { }
            ,new MyToolStripItem(500) { },new MyToolStripItem(1000) { },new MyToolStripItem(5000) { },new MyToolStripItem(10_000) { }
            ,new MyToolStripItem(50_000) { },new MyToolStripItem(100_000) { },new MyToolStripItem(0) { }});
            //Selected index should be fetch from what user last selected or options
            toolStripComboBoxLimit.SelectedIndex = 0;
        }

        private void InitEditorComponent()
        {
            this.editor1 = new Firedump.usercontrols.Editor();
            this.splitContainer2.Panel1.Controls.Add(this.editor1);
            this.editor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editor1.Name = "Editor1";
            this.editor1.TabIndex = 0;
        }

        //Disable ui elements when query is running.
        private void EnableUi(bool enable)
        {
            Console.WriteLine("EnableUi:"+enable);
           this.Invoke((MethodInvoker)delegate {
               this.SuspendLayout();
               Console.WriteLine(enable);
               GetUserControl<TabView>().GetDatabasesCombobox().Enabled = enable;
               //toolStripProgressBar.Visible = !enable;
               //toolStripProgressBar.Enabled = !enable;
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
               toolStripButtonEnableAutoCommit.Enabled = enable;
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
