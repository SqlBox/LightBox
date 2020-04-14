using Firedump.core.sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firedump.core.db
{
    public class SqliteHelpers
    {
        internal static DataTable GetDatabasePrimaryKeysDataSource(sqlservers server,DbConnection con)
        {
            DataTable data = new DataTable();
            DataColumn c0 = new DataColumn("Column");
            DataColumn c1 = new DataColumn("Table");
            data.Columns.Add(c0);
            data.Columns.Add(c1);
            List<string> tables = DbUtils.getTables(con);
            foreach(string table in tables)
            {
                string table_info = new SqlBuilderFactory(server).Create(con.Database).describeTableSql(table);
                using (var r = new DbCommandFactory(con, table_info).Create().ExecuteReader())
                {
                    while (r.Read())
                    {
                        if(r.GetInt32(5) >= 1)
                        {
                            DataRow row = data.NewRow();
                            row["Column"] = r.GetString(1);
                            row["Table"] = table;
                            data.Rows.Add(row);
                        }
                    }
                }
            }
            return data;
        }
    }
}
