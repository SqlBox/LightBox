using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firedump.core.sql
{
    class PostgreSqlBuilder : ISqlBuilder
    {
        private readonly string SCHEMA;
        
        public PostgreSqlBuilder(string schema) 
        {
            this.SCHEMA = schema;
        }

        public string createDatabaseIndexes()
        {
            throw new NotImplementedException();
        }

        public string describeTableSql(string table)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public List<string> getTables()
        {
            throw new NotImplementedException();
        }

        public List<string> removeSystemDatabases(List<string> databases, bool showSystemDb = false)
        {
            throw new NotImplementedException();
        }

        public string ShowCreateStatement(string table)
        {
            throw new NotImplementedException();
        }

        public string showTablesSql()
        {
            throw new NotImplementedException();
        }
    }
}
