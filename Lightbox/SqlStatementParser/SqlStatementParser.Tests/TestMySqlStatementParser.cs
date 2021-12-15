﻿using com.protectsoft.SqlStatementParser;
using NUnit.Framework;
using System;


namespace SqlStatementParser.Tests
{
    [TestFixture(typeof(DbType), DbType.MARIADB)]
    [TestFixture(typeof(DbType), DbType.MYSQL)]
    [TestFixture(typeof(DbType), DbType.SQLITE)]
    public class TestMySqlStatementParser<T>
    {
        private readonly DbType dbType;
        public TestMySqlStatementParser(T t)
        {
            this.dbType = (DbType)(object)t;
        }

        [Test, TestCaseSource(typeof(SqlProvider), "statementProvider")]
        public void TestParser(string sql, int expectedStatements)
        {
            Assert.AreEqual(expectedStatements, new SqlStatementParserWrapper(sql, dbType).Parse().Count);
        }

        [Test, TestCaseSource(typeof(MySqlProvider), "mysqlStatementProvider")]
        public void TestMysqlParser(string sql, int expectedStatements)
        {
            Assert.AreEqual(expectedStatements, new SqlStatementParserWrapper(sql, dbType).Parse().Count);
        }

        [Test, TestCaseSource(typeof(SqlProvider), "statementProvider")]
        public void TestConvert(string sql, int expectedStatements)
        {
            SqlStatementParserWrapper parser = new SqlStatementParserWrapper(sql, dbType);
            Assert.AreEqual(expectedStatements, SqlStatementParserWrapper.convert(parser.sql, parser.Parse()).Count);
        }

        [Test, TestCaseSource(typeof(MySqlProvider), "mysqlStatementProvider")]
        public void TestMySqlConvert(string sql, int expectedStatements)
        {
            SqlStatementParserWrapper parser = new SqlStatementParserWrapper(sql, dbType);
            Assert.AreEqual(expectedStatements, SqlStatementParserWrapper.convert(parser.sql, parser.Parse()).Count);
        }

    }
}
