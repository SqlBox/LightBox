using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firedump.core.db;
using sqlbox.commons;

namespace Firedump.core.sql
{
    public class SqlBuilderFactory
    {
        private sqlservers server;
        private DbConnection con;
        public SqlBuilderFactory(sqlservers s)
        {
            server = s;
        }

        public SqlBuilderFactory(DbConnection c)
        {
            con = c;
        }

        public ISqlBuilder Create(string database, bool isUpper = true)
        {
            DbType dbType = getDbTypeEnum();
            if (dbType == DbType.MYSQL || dbType == DbType.MARIADB)
            {
                return new MySqlSqlBuilder(database, isUpper);
            }
            else if (dbType == DbType.ORACLE)
            {
                return new OracleSqlBuilder(database, isUpper);
            }
            else if(dbType == DbType.POSTGRES)
            {
                return new PostgreSqlBuilder(database);
            }
            else if(dbType == DbType.SQLITE)
            {
                return new SqliteSqlBuilder(database);
            }
            else if(dbType == DbType.SQLSERVER)
            {
                return new SqlServerSqlBuilder(database);
            }
            else if(dbType == DbType.DB2)
            {
                return new Db2SqlBuilder(database);
            }
            else if(dbType == DbType.FIREBIRD)
            {
                return new FirebirdSqlBuilder(database);
            }
            throw new Exception("Wrong Database Type/Vendor!");
        }

        private DbType getDbTypeEnum()
        {
            if (server != null)
            {
                return Firedump.core.sql.Utils._convert(server.db_type);
            }
            return Firedump.core.sql.Utils.GetDbTypeEnum(con);
        }

    }
}
