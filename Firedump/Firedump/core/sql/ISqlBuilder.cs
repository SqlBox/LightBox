using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firedump.core.sql
{
    public interface ISqlBuilder
    {
        string getDatabases();

        List<string> getTables();

        List<string> getTableFields();

        string getDatabaseIndexes();

        string getDatabasePrimaryKeys();

        string getTableInfo(string table);

        string getDatabaseUniques();

        string getDatabaseForeignKeys();

        //gets all table from schema and all columns from all tables for the  database
        string getAllFieldsFromAllTablesInDb();

        string showTablesSql();

        string describeTableSql(string table);

        List<string> removeSystemDatabases(List<string> databases, bool showSystemDb = false);

        string ShowCreateStatement(string table);

        string GetAllTriggers();

        string GetTableTriggers(string table);

        string GetAllViews();

        string GetProcedures();

        string GetFunctions();
    }
}
