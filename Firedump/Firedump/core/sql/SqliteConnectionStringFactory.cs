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
            return "Data Source="+path+"; Version=3;"+(!string.IsNullOrEmpty(password) ? " Password="+password : "");
        }
    }
}
