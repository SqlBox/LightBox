using Firedump.core.parsers;
using Firedump.sqlitetables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace Firedump
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string sql = @"DELIMITER $$
                DROP PROCEDURE IF EXISTS insert_ten_rows $$
                CREATE PROCEDURE insert_ten_rows () 
                    BEGIN
                        DECLARE crs INT DEFAULT 0;
                        WHILE crs < 10 DO
                            INSERT INTO `continent`(`name`) VALUES ('cont'+crs);
                            SET crs = crs + 1;
                        END WHILE;
                    END $$
                DELIMITER ;";
            List<StatementRange> ranges = new SqlStatementParserWrapper(sql, DbTypeEnum.MYSQL).Parse();
            foreach(StatementRange r in ranges)
            {
                Console.WriteLine(sql.Substring((int)r.start, (int)r.end));
            }
            
            //Application.EnableVisualStyles();
            //Application.Run(new MainHome());
        }

    }
}
