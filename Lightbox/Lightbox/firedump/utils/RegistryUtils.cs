using Microsoft.Win32;


namespace Firedump.utils
{
    class RegistryUtils
    {
        public static void registerPath()
        {
            RegistryKey key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"SOFTWARE\sqlbox");
            //store firedump exe current path under hk_currentuser/sqlbox
            //Do not change the key otherwise other apps will not find it!
            key.SetValue("firedump_path", System.Reflection.Assembly.GetEntryAssembly().Location);
            key.Close();
        }
    }
}
