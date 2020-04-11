using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using Firedump.core.db;
using sqlbox.commons;

namespace FiredumpTest
{
    /// <summary>
    /// Summary description for Test_DbUtils
    /// </summary>
    [TestClass]
    public class Test_DbUtils
    {

        [TestMethod]
        public void Test_Convert()
        {
            Assert.AreEqual(DbType.MYSQL, _DbUtils._convert(0));
            Assert.AreEqual(DbType.MARIADB, _DbUtils._convert(1));
            Assert.AreEqual(DbType.ORACLE, _DbUtils._convert(2));
            Assert.AreEqual(DbType.POSTGRES, _DbUtils._convert(3));
            Assert.AreEqual(DbType.SQLSERVER, _DbUtils._convert(4));
        }
    }
}
