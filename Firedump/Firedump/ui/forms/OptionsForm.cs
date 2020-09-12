using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Firedump.ui.forms
{
    public partial class OptionsForm : Form
    {
        DbConnection con;
        public OptionsForm(System.Data.Common.DbConnection con)
        {
            InitializeComponent();
            FormUtils.setFormIcon(this);
            this.con = con;
        }
    }
}
