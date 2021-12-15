using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lightbox.ui.forms
{
    public enum DbData
    {
        COMMIT,
        ROLLBACK
    }
    public partial class CommitRollbackForm : Form
    {
        public DbData action;

        public CommitRollbackForm()
        {
            InitializeComponent();
        }

        private void buttonRollback_Click(object sender, EventArgs e)
        {
            action = DbData.ROLLBACK;
            Close();
        }

        private void buttonCommit_Click(object sender, EventArgs e)
        {
            action = DbData.COMMIT;
            Close();
        }
    }
}
