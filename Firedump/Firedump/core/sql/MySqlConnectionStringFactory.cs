using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firedump.core.sql
{
    sealed class MySqlConnectionStringBuilder
    {
        internal static string connectionStringBuilder(sqlservers server, string database = null) =>
            connectionStringBuilder(server.host, server.username, server.password, database, server.port,Properties.Settings.Default.option_mysql_contimeout);

        // string params order matters!
        private static string connectionStringBuilder(string host, string username, string password, string database, long port = 3306, int timeout = 120, string SslMode = "none")
            =>
            "Server=" + host + "; " + (string.IsNullOrEmpty(database) ? "" : "database=" + database
                + "; Convert Zero Datetime=true; Connection Timeout=" + timeout + "") + "; UID=" + username
                + ";" + (!string.IsNullOrEmpty(password) ? " password=" + password : "") + "; port=" + port + "; Keepalive="+ Properties.Settings.Default.option_mysql_keepalive+ "; SslMode=" + SslMode;

    }
}
