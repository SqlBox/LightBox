using Firedump.core.models.dbinfo;
using Firedump.core.models.events;
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

        private void InitEditorComponent()
        {
            this.editor1 = new Firedump.usercontrols.Editor();
            this.splitContainer3.Panel1.Controls.Add(this.editor1);
            this.editor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editor1.Name = "Editor1";
            this.editor1.TabIndex = 0;
        }

        //Disable ui elements when query is running.
        private void DisableUI()
        {
            this.Invoke((MethodInvoker)delegate {
                this.toolStripButtonExecute.Enabled = false;
            });   
        }

        //ReEnable ui elements , usualy called after query been executed
        private void EnableUI()
        {
            this.Invoke((MethodInvoker)delegate {
                this.toolStripButtonExecute.Enabled = true;
            });
        }

        private void OnStatementExecuted(object sender, ExecutionEventArgs e)
        {
            switch(e.Status)
            {
                case Status.CANCELED:
                case Status.FINISHED:
                case Status.ERROR:
                    EnableUI();
                    break;
            }
        }
    }
}
