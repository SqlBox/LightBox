using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firedump.core.db
{
    // dont know if this will be here in future
    public sealed class DbSessionSettings
    {
        internal static void SetAutoCommit(DbConnection con, bool autoCommit)
        {
            using (var command = new DbCommandFactory(con, "SET autocommit=" + (autoCommit == true ? "1" : "0")).Create())
            {
                command.ExecuteNonQuery();
            }
        }

        internal static void BeginTransaction(DbConnection con)
        {
            using (var command = new DbCommandFactory(con, "begin transaction").Create())
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
