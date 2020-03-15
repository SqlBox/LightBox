using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Firedump.ui.forms
{
    //Base parent form with some common properties for all other forms
    public class BaseForm : Form
    {
        public BaseForm():base()
        { 
            this.Icon = Properties.Resources.lighthouse_logo;
        }
    }
}
