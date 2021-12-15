using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightbox.core
{
    class RegistryUtils
    {
        public static string getLightboxPath()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\sqlbox");
            string Lightbox = null;
            if (key != null)
            {
                Lightbox = (string)key.GetValue("Lightbox_path");
                key.Close();
            }
            return Lightbox;
        }
    }
}
