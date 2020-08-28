using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firedump.core
{
    class RegistryUtils
    {
        public static string getFiredumpPath()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\sqlbox");
            string firedump = null;
            if (key != null)
            {
               firedump = (string)key.GetValue("firedump_path");
               key.Close();
            }
            return firedump;
        }
    }
}
