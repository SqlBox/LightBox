using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Firedump.ui.forms
{
    public enum OpenExecute
    {
        CANCEL,
        OPEN,
        EXECUTE
    }

    public partial class LargeFileForm : Form
    {
        private long mbytes;
        public OpenExecute openExecute;
        public LargeFileForm(long bytes, string file)
        {
            InitializeComponent();
            //bytes to megabytes
            mbytes = (bytes / 1024) / 1024;
            label1.Text = "The file '" + file + "' is large. \nSize of " + mbytes + " mb's. \n" +
                "Note: some features will be disabled from the editor!\n";
            openExecute = 0;
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            openExecute = OpenExecute.OPEN;
            Close();
        }

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            openExecute = OpenExecute.EXECUTE;
            Close();
        }
    }
}
