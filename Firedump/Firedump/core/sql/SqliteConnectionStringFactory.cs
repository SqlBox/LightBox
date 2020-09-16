using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firedump.core.sql
{
    class SqliteConnectionStringFactory
    {
        internal static string ConnectionStringBuilder(string path,string password)
        {
            string foreignkeys = "foreignkeys=" + Properties.Settings.Default.option_sqlite_foreign_keys.ToString().ToLower() + ";";
            return "Data Source="+path+"; Version=3; "+ foreignkeys + (!string.IsNullOrEmpty(password) ? " Password="+password : "");
        }
    }
}
