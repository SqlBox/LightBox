using System;
using Firedump.core.parsers;
using Firedump.sqlitetables;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace FiredumpTest
{

    [TestFixture(typeof(DbTypeEnum), DbTypeEnum.MARIADB)]
    [TestFixture(typeof(DbTypeEnum), DbTypeEnum.MYSQL)]
    public class TestMySqlStatementParser<T>
    {
        private readonly DbTypeEnum dbType;
        public TestMySqlStatementParser(T t) {
            Console.WriteLine(t);
            this.dbType = (DbTypeEnum)(object)t;
        }

        [Test,TestCaseSource(typeof(MySqlRepoProvider),"statementProvider")]
        public void TestParser(string sql,int expectedStatements)
        {
            Assert.AreEqual(expectedStatements, new SqlStatementParserWrapper(sql, dbType).Parse().Count);
        }

        [Test, TestCaseSource(typeof(MySqlRepoProvider), "statementProvider")]
        public void TestConvert(string sql, int expectedStatements)
        {
            SqlStatementParserWrapper parser = new SqlStatementParserWrapper(sql, dbType);
            Assert.AreEqual(expectedStatements, parser.convert(parser.Parse()).Count);
        }

    }
}
