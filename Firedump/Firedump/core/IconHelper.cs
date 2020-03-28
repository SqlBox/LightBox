using Firedump.core.sql;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firedump.core
{
    public class IconHelper
    {
        public static byte[] status_ok_arr;
        public static byte[] status_error_arr;
        public static byte[] status_info_arr;

        internal static void Init() {}

        static IconHelper()
        {
            status_ok_arr = Utils.IconToBytes(Icon.FromHandle(Properties.Resources.status_ok.GetHicon()));
            status_error_arr = Utils.IconToBytes(Icon.FromHandle(Properties.Resources.status_error.GetHicon()));
            status_info_arr = Utils.IconToBytes(Icon.FromHandle(Properties.Resources.status_info.GetHicon()));
        }

    }
}
