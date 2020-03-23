using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firedump.ui
{
    public class IconHelper
    {
        public static Icon status_ok = Icon.FromHandle(Properties.Resources.status_ok.GetHicon());
        public static Icon status_error = Icon.FromHandle(Properties.Resources.status_error.GetHicon());
        public static Icon status_info = Icon.FromHandle(Properties.Resources.status_info.GetHicon());
    }
}
