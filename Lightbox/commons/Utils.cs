using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace sqlbox.commons
{
    public class Utils
    {
        public static byte[] IconToBytes(Icon icon)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                icon.Save(ms);
                return ms.ToArray();
            }
        }
    }
}
