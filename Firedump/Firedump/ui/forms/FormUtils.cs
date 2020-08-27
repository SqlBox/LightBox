using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Firedump.ui.forms
{
    class FormUtils
    {
        public static void setFormIcon(Form f)
        {
            f.Icon = Icon.FromHandle(Properties.Resources.lighthouse_logo1.GetHicon());
        }
    }
}
