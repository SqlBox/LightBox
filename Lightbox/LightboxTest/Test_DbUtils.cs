using Microsoft.VisualStudio.TestTools.UnitTesting;
using sqlbox.commons;

namespace LightboxTest
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
            Assert.AreEqual(DbType.MYSQL, Lightbox.core.sql.Utils._convert(0));
            Assert.AreEqual(DbType.MARIADB, Lightbox.core.sql.Utils._convert(1));
            Assert.AreEqual(DbType.ORACLE, Lightbox.core.sql.Utils._convert(2));
            Assert.AreEqual(DbType.POSTGRES, Lightbox.core.sql.Utils._convert(3));
            Assert.AreEqual(DbType.SQLSERVER, Lightbox.core.sql.Utils._convert(4));
        }
    }
}
