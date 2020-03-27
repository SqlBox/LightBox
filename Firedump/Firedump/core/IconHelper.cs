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
            using (MemoryStream ms = new MemoryStream())
            {
                Icon.FromHandle(Properties.Resources.status_ok.GetHicon()).Save(ms);
                status_ok_arr =  ms.ToArray();
            }
            using (MemoryStream ms = new MemoryStream())
            {
                Icon.FromHandle(Properties.Resources.status_error.GetHicon()).Save(ms);
                status_error_arr = ms.ToArray();
            }
            using (MemoryStream ms = new MemoryStream())
            {
                Icon.FromHandle(Properties.Resources.status_info.GetHicon()).Save(ms);
                status_info_arr = ms.ToArray();
            }
        }

    }
}
