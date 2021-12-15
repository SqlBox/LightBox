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
    //NOT USED
    public partial class TerminalForm : Form
    {
        public TerminalForm()
        {
            InitializeComponent();
            FormUtils.setFormIcon(this);
        }

        public TerminalForm(MainHome mh) : this()
        {
            this.terminal1.SetMainHome(mh);
        }
    }
}
