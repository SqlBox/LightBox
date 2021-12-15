using Lightbox.core.attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightbox.core.sql
{
    // Remember If order of named sql column results change in this sql's query, remapping is needed for dataSource binding.
    class MySqlSqlBuilder : ISqlBuilder
    {
        private readonly string SCHEMA;

        public MySqlSqlBuilder(string db)
        {
            SCHEMA = db;
        }

        // Remember If order of named sql column results change in this sql query, remapping is needed for dataSource binding.
        public string getDatabaseIndexes() =>
             "SELECT DISTINCT TABLE_NAME AS 'Table', INDEX_NAME AS 'Index', COLUMN_NAME AS 'Column' FROM INFORMATION_SCHEMA.STATISTICS WHERE" +
            " TABLE_SCHEMA = '" + SCHEMA + "' ";

        // Remember If order of named sql column results change in this sql query, remapping is needed for dataSource binding.
        public string getDatabasePrimaryKeys() =>
            "SELECT  sta.index_name as Type,  GROUP_CONCAT(DISTINCT sta.column_name  ORDER BY sta.column_name) AS 'Column(s)', tab.table_name AS 'Table'" +
            " FROM information_schema.tables AS tab" +
            "  INNER JOIN information_schema.statistics AS sta ON sta.table_schema = tab.table_schema " +
            "  AND sta.table_name = tab.table_name " +
            "  AND sta.index_name = 'primary'" +
            " WHERE tab.table_schema = '" + SCHEMA + "' " +
            "  AND tab.table_type = 'BASE TABLE'" +
            " GROUP BY tab.table_name " +
            " ORDER BY tab.table_name";

        // Remember If order of named sql column results change in this sql query, remapping is needed for dataSource binding.
        public string getTableInfo(string table) =>
            "SELECT  tab.AVG_ROW_LENGTH AS 'AvgLen', " +
            "  tab.DATA_LENGTH AS 'Length', tab.DATA_FREE AS 'Free', tab.AUTO_INCREMENT 'AI', tab.TABLE_COLLATION AS 'Collation', tab.table_rows as 'rows'" +
            " FROM information_schema.tables AS tab" +
            "  LEFT OUTER JOIN information_schema.statistics AS sta ON sta.table_schema = tab.table_schema " +
            "  AND sta.index_name = 'primary'" +
            " WHERE tab.table_schema = '" + SCHEMA + "' " +
            "  AND tab.table_type = 'BASE TABLE'" +
            "  AND tab.table_name = '" + table + "' " +
            " GROUP BY tab.table_name, tab.AVG_ROW_LENGTH, tab.DATA_LENGTH, tab.DATA_FREE, tab.AUTO_INCREMENT, tab.TABLE_COLLATION, tab.table_rows " +
            " ORDER BY tab.table_name";


        public string getDatabaseUniques() =>
            "SELECT DISTINCT tab.constraint_name AS 'Name', tab.table_name AS 'Table' " +
            " FROM information_schema.TABLE_CONSTRAINTS tab " +
            " WHERE tab.CONSTRAINT_SCHEMA = '" + SCHEMA + "' " +
            " AND tab.CONSTRAINT_TYPE = 'UNIQUE'";

        public string getDatabaseForeignKeys() =>
            "SELECT DISTINCT tab.constraint_name as 'Name', tab.table_name as 'Table', tab.column_name as 'Column', tab.referenced_table_name as 'Ref Table', " +
            " tab.referenced_column_name as 'Ref Col' from INFORMATION_SCHEMA.KEY_COLUMN_USAGE tab " +
            " INNER JOIN information_schema.TABLE_CONSTRAINTS con ON con.constraint_name = tab.constraint_name AND con.constraint_type = 'FOREIGN KEY' " +
            " WHERE tab.table_schema = '" + SCHEMA + "'";

        public string getAllFieldsFromAllTablesInDb() =>
            "SELECT c.table_name , c.column_name" +
                " , c.data_type , c.is_nullable , c.character_maximum_length " +
                "   FROM information_schema.columns c " +
                "   WHERE c.table_schema = '" + SCHEMA + "' order by c.column_name,c.table_name,c.ordinal_position";


        //private string getUpperOrLower(string field) => IsUpper ? " UPPER(" + field + ") " : " LOWER(" + field + ") ";


        public string getDatabases() => "show databases;";


        public List<string> getTables()
        {
            throw new NotImplementedException();
        }

        public List<string> getTableFields()
        {
            throw new NotImplementedException();
        }

        /**
         * !Only For MySql SCHEMA!
         */
        public List<string> removeSystemDatabases(List<string> databases, bool showSystemDb = false) =>
            !showSystemDb
                ? databases.Where(i => i.ToLower() != "sys" && i.ToLower() != "performance_schema" && i.ToLower() != "mysql"
                    && i.ToLower() != "information_schema").ToList()
                : databases;

        public string showTablesSql() => "show tables from " + SCHEMA + ";";


        [Implement("Need model for describe output and implementation")]
        public string describeTableSql(string table) => "DESCRIBE " + table;


        public string ShowCreateStatement(string table)
        {
            return "SHOW CREATE TABLE " + table;
        }

        public string GetAllTriggers()
        {
            return "show triggers;";
        }

        public string GetTableTriggers(string table)
        {
            return "SELECT trigger_name, action_timing, event_manipulation FROM information_schema.triggers WHERE event_object_table = '" + table + "' order by  trigger_name,action_timing, event_manipulation";
        }

        public string GetAllViews()
        {
            return "SHOW FULL TABLES IN " + SCHEMA + " WHERE TABLE_TYPE LIKE 'VIEW';";
        }

        public string GetProcedures()
        {
            return "SHOW PROCEDURE STATUS WHERE Db = '" + SCHEMA + "'";
        }

        public string GetFunctions()
        {
            return "SHOW FUNCTION STATUS WHERE Db = '" + SCHEMA + "'";
        }

        public string GetTriggerCreateStatement(string table, string triggerName)
        {
            return "SHOW CREATE TRIGGER " + triggerName;
        }
    }

}
