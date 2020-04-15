using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firedump.core.sql
{
    class SqliteSqlBuilder : ISqlBuilder
    {
        public SqliteSqlBuilder() 
        {
        }
        public string getDatabaseIndexes()
        {
            return "SELECT name AS 'INDEX', tbl_name AS 'TABLE' FROM sqlite_master WHERE type = 'index'";
        }

        public string describeTableSql(string table)
        {
            return "pragma table_info('"+table+"')";
        }

        public string getAllFieldsFromAllTablesInDb()
        {
            throw new NotImplementedException();
        }

        public string getDatabaseForeignKeys()
        {
            throw new NotImplementedException();
        }

        public string getDatabasePrimaryKeys()
        {
            throw new NotImplementedException();
        }

        public string getDatabases()
        {
            return "";
        }

        public string getDatabaseUniques()
        {
            throw new NotImplementedException();
        }

        public List<string> getTableFields()
        {
            throw new NotImplementedException();
        }

        public string getTableInfo(string table)
        {
            return describeTableSql(table);
        }

        public List<string> getTables()
        {
            throw new NotImplementedException();
        }

        public List<string> removeSystemDatabases(List<string> databases, bool showSystemDb = false)
        {
            return databases;
        }

        public string ShowCreateStatement(string table)
        {
            return "SELECT name, sql FROM sqlite_master WHERE tbl_name = '"+table +"' ";
        }

        public string showTablesSql()
        {
            return "SELECT name FROM sqlite_master WHERE type = 'table'";
        }

        public string GetAllTriggers()
        {
            return "SELECT name as 'Trigger', tbl_name as 'Table', sql FROM sqlite_master WHERE type = 'trigger'";
        }

        public string GetTableTriggers(string table)
        {
            return "SELECT name as 'Trigger', tbl_name as 'Table', sql FROM sqlite_master WHERE type = 'trigger' AND tbl_name = '"+table +"' ";
        }

        public string GetAllViews()
        {
            return "SELECT name as 'View', sql FROM sqlite_master WHERE type = 'view'";
        }

        public string GetProcedures()
        {
            throw new NotImplementedException();
        }

        public string GetFunctions()
        {
            throw new NotImplementedException();
        }

        public string GetTriggerCreateStatement(string table, string triggerName)
        {
            return "SELECT name, sql FROM sqlite_master WHERE type = 'trigger' AND tbl_name = '"+table+"' AND name = '"+triggerName+"' ";
        }
    }
}
