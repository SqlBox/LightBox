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

        private void OnStatementExecuted(object sender, ExecutionEventArgs<string> e)
        {
            switch(e.Status)
            {
                case Status.CANCELED:
                case Status.FINISHED:
                case Status.STOPPED:
                case Status.ERROR:
                case Status.WAITING:
                    EnableUI();
                    break;
            }
        }
    }
}
