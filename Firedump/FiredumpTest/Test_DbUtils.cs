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
            Assert.AreEqual(DbType.MYSQL, Firedump.core.sql.Utils._convert(0));
            Assert.AreEqual(DbType.MARIADB, Firedump.core.sql.Utils._convert(1));
            Assert.AreEqual(DbType.ORACLE, Firedump.core.sql.Utils._convert(2));
            Assert.AreEqual(DbType.POSTGRES, Firedump.core.sql.Utils._convert(3));
            Assert.AreEqual(DbType.SQLSERVER, Firedump.core.sql.Utils._convert(4));
        }
    }
}
