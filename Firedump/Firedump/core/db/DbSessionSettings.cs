using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firedump.core.db
{
    public sealed class DbSessionSettings
    {
        internal static void SetAutoCommit(DbConnection con, bool autoCommit)
        {
            using (var command = new DbCommandFactory(con, "SET autocommit=" + (autoCommit == true ? "1" : "0")).Create())
            {
                command.ExecuteNonQuery();
            }
        }

        internal static void SetMaxAllowedPacketSize(DbConnection con, long killobytes)
        {
            using (var command = new DbCommandFactory(con, "SET SESSION max_allowed_packet="+killobytes).Create())
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
